using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST.Utility;
//using Microsoft.AspNet.Identity;

namespace BusinessLogic.Repositories
{
     
    public class cls_Invoice
    {
        UnitOfWork unitOfWork;

        public cls_Invoice()
        {
            unitOfWork = new UnitOfWork();
        }


        public int GetCurrentFinYear()
        {
            string finYearfomrat = DateTimeDayOfMonthExtensions.GenerateFinancialPeriod();
            return unitOfWork.FinYearRepository.Filter(f => f.Finyear_Format == finYearfomrat).FirstOrDefault().Fin_ID;

        }

        public GST_TRN_INVOICE GetInvoice(Int64 invoiceID)
        {
            return unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
        }

        /// <summary>
        /// All Invoice Data
        /// </summary>
        /// <param name="invoiceDataIDs"></param>
        /// <returns></returns>
        public List<GST_TRN_INVOICE_DATA> GetInvoiceData(List<Int64> invoiceDataIDs)
        {
            return unitOfWork.InvoiceDataRepository.Filter(f => invoiceDataIDs.Contains(f.InvoiceDataID)).ToList();
        }

        #region "GSTR-2A"

        public void AutoCorrectInvoice(Int64 newInvoice)
        {
            //var newInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
            var misMatchedInvoice = MatchInvoiceWithChildAndParent(newInvoice);
        }

        public List<GST_TRN_INVOICE_DATA> MatchInvoiceWithChildAndParent(Int64 invoiceID)
        {
            Int64 invoiceId = Convert.ToInt64(invoiceID);
            var modifiedInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceId);
            List<GST_TRN_INVOICE_DATA> itemData = new List<GST_TRN_INVOICE_DATA>();
            if (modifiedInvoice.ParentInvoiceID.HasValue)
            {

                var compareInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == modifiedInvoice.ParentInvoiceID);

                var purchaseRegister = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.SupplierInvoiceNo == compareInvoice.InvoiceNo);

                //var modifiedItem = modifiedInvoice.GST_TRN_INVOICE_DATA.Select(s => new { s.Qty, s.Rate, s.TaxableAmount, s.GST_TRN_INVOICE.AspNetUser1.GSTNNo }).ToList();

                //var purchaseRegisterItems = purchaseRegister.GST_MST_PURCHASE_DATA.Select(s => new { s.Qty, s.Rate, s.TaxableAmount, s.GST_MST_PURCHASE_REGISTER.SellerGSTN }).ToList().ToList();
                if (purchaseRegister != null)
                {
                    var result = modifiedInvoice.GST_TRN_INVOICE_DATA.Select(s => new { Qty = s.Qty, Rate = s.Rate, TaxableAmount = s.TaxableAmount, GSTNNo = s.GST_TRN_INVOICE.AspNetUser1.GSTNNo }).ToList().Except(purchaseRegister.GST_MST_PURCHASE_DATA.Select(s => new { Qty = s.Qty, Rate = s.Rate, TaxableAmount = s.TaxableAmount, GSTNNo = s.GST_MST_PURCHASE_REGISTER.SellerGSTN }).ToList()).ToList();

                    if (result.ToList().Count == 0)
                    {
                        //GST_TRN_CONSOLIDATED_INVOICE consolidateInvoice = new GST_TRN_CONSOLIDATED_INVOICE();
                        var itemforDelete = unitOfWork.ConsolidatedInvoiceRepository.Filter(f => f.InvoiceID == invoiceId).ToList();
                        foreach (GST_TRN_CONSOLIDATED_INVOICE consolidateInvoice in itemforDelete)
                        {
                            unitOfWork.ConsolidatedInvoiceRepository.Delete(consolidateInvoice);
                        }
                        unitOfWork.Save();
                    }
                    else
                    {
                        foreach (GST_TRN_INVOICE_DATA data in modifiedInvoice.GST_TRN_INVOICE_DATA)
                        {
                            var dataExist = unitOfWork.ConsolidatedInvoiceRepository.Find(f => f.InvoiceDataID == data.InvoiceDataID);
                            if (dataExist != null)
                            {
                                dataExist.QTY = data.Qty;
                                dataExist.Rate = data.Rate;
                                dataExist.TAXABLEAMOUNT = data.TaxableAmount;
                                unitOfWork.ConsolidatedInvoiceRepository.Update(dataExist);
                            }
                            GST_TRN_CONSOLIDATED_INVOICE consolidateInvoiceCreate = new GST_TRN_CONSOLIDATED_INVOICE();
                            consolidateInvoiceCreate.UserID = modifiedInvoice.AspNetUser1.Id;
                            consolidateInvoiceCreate.InvoiceDataID = data.InvoiceDataID;
                            consolidateInvoiceCreate.InvoiceID = modifiedInvoice.InvoiceID;
                            consolidateInvoiceCreate.INVOICESPECIALCONDITION = modifiedInvoice.InvoiceSpecialCondition;
                            consolidateInvoiceCreate.GSTNNO = modifiedInvoice.AspNetUser1.GSTNNo;
                            consolidateInvoiceCreate.QTY = data.Qty;
                            consolidateInvoiceCreate.Rate = data.Rate;
                            consolidateInvoiceCreate.TAXABLEAMOUNT = data.TaxableAmount;
                            unitOfWork.ConsolidatedInvoiceRepository.Create(consolidateInvoiceCreate);
                        }
                        unitOfWork.Save();

                    }
                }
                //Mismatch item whic r not match with item
                //itemData = result.ToList();// modifiedInvoice.GST_TRN_INVOICE_DATA.Where(b => compareInvoice.GST_TRN_INVOICE_DATA.Any(a => a.Qty != b.Qty && a.Item_ID == b.Item_ID)).ToList();

            }
            return itemData;
        }

        /// <summary>
        ///   Reconsilation invoive with purchage register and give mismatch report
        /// </summary>
        /// <param name="userID"></param>
        public void MisMatchInvoice(string userID)
        {
            var result = unitOfWork.GENERATE_MISMATCH_INVOICE(userID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_CONSOLIDATED_INVOICE> GetConsolidatetdInvoices(string userID,byte month)
        {
            //var invoice=unitOfWork.InvoiceAuditTrailRepositry.Filter()
            var item = unitOfWork.ConsolidatedInvoiceRepository.Filter(f => f.UserID == userID && f.GST_TRN_INVOICE.InvoiceMonth == month).ToList();
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="invoiceSplCondition"></param>
        /// <returns></returns>
        public List<GST_TRN_INVOICE_AUDIT_TRAIL> GetConsolidatetdInvoices(string userID, string invoiceSplCondition)
        {
            var splCondition = (EnumConstants.InvoiceSpecialCondition)Convert.ToInt32(invoiceSplCondition);
            //var invoice=unitOfWork.InvoiceAuditTrailRepositry.Filter()
            var consoInvoice = unitOfWork.ConsolidatedInvoiceRepository.Filter(f => f.UserID == userID && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh && f.INVOICESPECIALCONDITION == (byte)splCondition).Select(s => s.InvoiceID).Distinct().ToList();

            var item = ViewInvoicesType(consoInvoice);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_INVOICE_AUDIT_TRAIL> ViewInvoicesType(List<Int64?> listInvoices)
        {
            //var invoice=unitOfWork.InvoiceAuditTrailRepositry.Filter()
            var item = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => listInvoices.Contains(f.InvoiceID) && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A).ToList();
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_INVOICE> MatchedInvoices(List<Int64?> listInvoices)
        {
            //var invoice=unitOfWork.InvoiceAuditTrailRepositry.Filter()
            var item = unitOfWork.InvoiceRepository.Filter(f => !listInvoices.Contains(f.InvoiceID)).ToList();
            return item;
        }


        #endregion


        #region "GSTR-1A"

        public clsMessageAttribute ImportAllInvoices(string userID)
        {
            clsMessageAttribute mailData = new clsMessageAttribute();

            DateTime firstdate = DateTime.Now.FirstDayOfMonth();
            DateTime lastDate = DateTime.Now.LastDayOfMonth();

            var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == userID
            && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import1A
                //&& !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true
            && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.CreatedDate).ToList();

            var existItems = invoices.Select(s => s.InvoiceID).ToList();

            var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == userID
                && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2
                && !existItems.Contains(f.InvoiceID)
                && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
                && f.CreatedDate <= lastDate)).ToList();

            var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;

            int count = 0;
            List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
            List<string> mailsToList = new List<string>();
            foreach (GST_TRN_INVOICE_AUDIT_TRAIL item in filedItem)
            {
                GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();

                Int64? invoiceID = item.InvoiceID;
                clsMessageAttribute attribute = new clsMessageAttribute();
                var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);

                var getfile2Item = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailID == item.AuditTrailID);
                audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import1A);
                audittrail.UserIP = HelperUtility.IP;
                audittrail.CreatedDate = DateTime.Now;
                audittrail.InvoiceAction = item.InvoiceAction;// Convert.ToByte(invoiceAction);
                audittrail.ReceiverInvoiceAction = item.ReceiverInvoiceAction;// Convert.ToByte(invoiceAction);
                audittrail.ReceiverInvoiceActionDate = item.ReceiverInvoiceActionDate;// Convert.ToByte(invoiceAction);
                audittrail.CreatedBy = userID;
                unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                unitOfWork.Save();
                count = count + 1;
                if (!mailsToList.Contains(invoiceDetail.AspNetUser.Email))
                {
                    mailsToList.Add(invoiceDetail.AspNetUser.Email);
                }
                attribute.UserName = invoiceDetail.AspNetUser.OrganizationName;

                attribute.InvoiceDate = invoiceDetail.InvoiceDate.Value.ToString();// DateTimeAgo.GetFormatDate(invoiceDetail.InvoiceDate);
                attribute.InvoiceTotalAmount = invoiceDetail.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                invAttributes.Add(attribute);

            }

            if (count > 0)
            {
                string mailString = string.Empty;
                string sellerMail = string.Empty;


                foreach (clsMessageAttribute iId in invAttributes)
                {
                    //mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                    mailString += "<tr><td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                    mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                    mailData.UserName = iId.UserName;
                }
                mailData.CustomMessage = "Data imported successfully.";
                mailData.MailsTo = mailsToList;
                //SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                //BindViewInvoice();
            }
            else
            {
                mailData.CustomMessage = "There are no invoices.";
                ////uc_sucess.Visible = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
            }

            return mailData;

        }

        //public void SendHTMLMail(clsMessageAttribute mailData, string mailString, string sellerEmail)
        //{
        //    cls_Message message = new cls_Message();
        //   // EmailService email = new EmailService();
        //    IdentityMessage msg = new IdentityMessage();
        //    Dictionary<string, string> replaceItem = new Dictionary<string, string>();
        //    replaceItem.Add("@User", mailData.UserName);
        //    replaceItem.Add("@InvoiceData", mailString);
        //    string mailBody = message.GetMessage(EnumConstants.Message.ImportGSTR1A, replaceItem);
        //    msg.Body = mailBody;   //"hi body.....";
        //    msg.Destination = Common.UserProfile.Email;//sellerEmail;
        //    msg.Subject = "GSTR - 1A Imported.";
        //    email.Send(msg);
        //}
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public static string InvoiceColor(object invoiceType)
        {

            string InvoiveColorBox;// = Convert.ToInt32(invoicID);
            EnumConstants.InvoiceSpecialCondition InvoiceType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType != null ? invoiceType.ToString() : "-1");
            switch (InvoiceType)
            {
                case EnumConstants.InvoiceSpecialCondition.Advance:
                    InvoiveColorBox = "small-box bg-blue";
                    break;
                case EnumConstants.InvoiceSpecialCondition.B2CL:
                    InvoiveColorBox = "small-box label-danger";
                    break;
                case EnumConstants.InvoiceSpecialCondition.B2CS:
                    InvoiveColorBox = "small-box bg-aqua";
                    break;

                case EnumConstants.InvoiceSpecialCondition.DeemedExport:
                    InvoiveColorBox = "small-box bg-orange";
                    break;

                case EnumConstants.InvoiceSpecialCondition.ECommerce:
                    InvoiveColorBox = "small-box bg-red";
                    break;

                case EnumConstants.InvoiceSpecialCondition.Export:
                    InvoiveColorBox = "small-box bg-green";
                    break;

                case EnumConstants.InvoiceSpecialCondition.Import:
                    InvoiveColorBox = "small-box bg-blue";
                    break;
                case EnumConstants.InvoiceSpecialCondition.JobWork:
                    InvoiveColorBox = "small-box  label-primary";
                    break;
                case EnumConstants.InvoiceSpecialCondition.Regular:
                    InvoiveColorBox = "small-box bg-purple";
                    break;
                case EnumConstants.InvoiceSpecialCondition.ReverseCharges:
                    InvoiveColorBox = "small-box bg-black";
                    break;
                case EnumConstants.InvoiceSpecialCondition.SEZDeveloper:
                    InvoiveColorBox = "small-box bg-yellow";
                    break;
                case EnumConstants.InvoiceSpecialCondition.SEZUnit:
                    InvoiveColorBox = "small-box bg-teal";
                    break;
                default:
                    InvoiveColorBox = "small-box bg-aqua";
                    break;
            }

            return InvoiveColorBox;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public static string GetInvoiveNoWithPreFix(string invoiceNo, EnumConstants.InvoiceSpecialCondition invoiceType)
        {
            string newInvoiceNo = string.Empty;
            newInvoiceNo = InvoiceNoPreFix(invoiceType) + invoiceNo;
            return newInvoiceNo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public static string InvoiceNoPreFix(EnumConstants.InvoiceSpecialCondition invoiceType)
        {

            string InvoiveColorBox;// = Convert.ToInt32(invoicID);
            EnumConstants.InvoiceSpecialCondition InvoiceType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType != null ? invoiceType.ToString() : "-1");
            switch (InvoiceType)
            {
                case EnumConstants.InvoiceSpecialCondition.Advance:
                    InvoiveColorBox = null; //"VC";
                    break;
                case EnumConstants.InvoiceSpecialCondition.B2CL:
                    InvoiveColorBox = "CL";
                    break;
                case EnumConstants.InvoiceSpecialCondition.B2CS:
                    InvoiveColorBox = "CS";
                    break;
                case EnumConstants.InvoiceSpecialCondition.DeemedExport:
                    InvoiveColorBox = null;//"DE";
                    break;
                case EnumConstants.InvoiceSpecialCondition.ECommerce:
                    InvoiveColorBox = null;//"EC";
                    break;
                case EnumConstants.InvoiceSpecialCondition.Export:
                    InvoiveColorBox = null;
                    break;
                case EnumConstants.InvoiceSpecialCondition.Import:
                    InvoiveColorBox = null;//"IM";
                    break;
                case EnumConstants.InvoiceSpecialCondition.JobWork:
                    InvoiveColorBox = null;//"CH";
                    break;
                case EnumConstants.InvoiceSpecialCondition.Regular:
                    InvoiveColorBox = "RG";
                    break;
                case EnumConstants.InvoiceSpecialCondition.ReverseCharges:
                    InvoiveColorBox = "RC";
                    break;
                case EnumConstants.InvoiceSpecialCondition.SEZDeveloper:
                    InvoiveColorBox = null;//"SD";
                    break;
                case EnumConstants.InvoiceSpecialCondition.SEZUnit:
                    InvoiveColorBox = null;//"SU";
                    break;
                case EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice:
                    InvoiveColorBox = null;//"MI";
                    break;
                case EnumConstants.InvoiceSpecialCondition.RegularRCM:
                    InvoiveColorBox = "RC";
                    break;
                default:
                    InvoiveColorBox = "NA";
                    break;
            }

            return InvoiveColorBox;

        }

        #region
        /// <summary>
        /// Get All Invoices throught web api
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GST_TRN_INVOICE> GetAllInvoice()
        {
            var invice = unitOfWork.InvoiceRepository.Filter(f => f.Status == true);
            return invice;
        }

        public IEnumerable<GST_TRN_INVOICE> GetAllInvoicebyId(int invoiceid)
        {
            var invice = unitOfWork.InvoiceRepository.Filter(f => f.Status == true && f.InvoiceID == invoiceid);
            return invice;
        }

        public bool Save_GSTR1(GST_TRN_INVOICE _RETSAVE_GSTR)
        {
            GST_TRN_INVOICE invoice = new GST_TRN_INVOICE();
            GST_TRN_INVOICE_DATA invoiceData;

            //UnitOfWork localUnitOfWork = new UnitOfWork();
            //UnitOfWork unitOfWork = new UnitOfWork();
            //gstapilog logs = new gstapilog
            //{
            //    actionname = this.controllercontext.routedata.values["action"].tostring(),
            //    controllername = this.controllercontext.routedata.values["controller"].tostring(),
            //    hitdatetime = datetime.now
            //};
            //localUnitOfWork.LogRepository.Create(logs);
            //localUnitOfWork.Save();
            ////     log code end
            try
            {

                //string JsonData = EncryptionUtils.DecryptString(EncryptedData.Data);//EncryptedData.Data;//.Data;
                //Save_GSTR_Model.SAVEGSTR1 _RETSAVE_GSTR = JsonConvert.DeserializeObject<Save_GSTR_Model.SAVEGSTR1>(JsonData);


                invoice.InvoiceNo = _RETSAVE_GSTR.InvoiceNo;
                invoice.InvoiceDate = _RETSAVE_GSTR.InvoiceDate;
                invoice.InvoiceType = Convert.ToByte(_RETSAVE_GSTR.InvoiceType);
                //   invoice.InvoiceUserId = _RETSAVE_GSTR.InvoiceUserId;
                invoice.InvoiceMonth = _RETSAVE_GSTR.InvoiceMonth;
                invoice.SellerUserID = _RETSAVE_GSTR.SellerUserID;
                invoice.ReceiverUserID = _RETSAVE_GSTR.ReceiverUserID;
                invoice.ConsigneeUserID = _RETSAVE_GSTR.ConsigneeUserID;
                invoice.OrderDate = _RETSAVE_GSTR.OrderDate;
                invoice.VendorID = _RETSAVE_GSTR.VendorID;
                invoice.TransShipment_ID = _RETSAVE_GSTR.TransShipment_ID;
                invoice.Freight = _RETSAVE_GSTR.Freight;
                invoice.Insurance = _RETSAVE_GSTR.Insurance;
                //    invoice.SectionType = Convert.ToByte((OfflineExcelSection)Enum.Parse(typeof(OfflineExcelSection), _RETSAVE_GSTR.SectionType));
                invoice.PackingAndForwadingCharges = _RETSAVE_GSTR.PackingAndForwadingCharges;
                invoice.ElectronicReferenceNo = _RETSAVE_GSTR.ElectronicReferenceNo;
                invoice.ElectronicReferenceNoDate = _RETSAVE_GSTR.ElectronicReferenceNoDate;
                invoice.FinYear_ID = _RETSAVE_GSTR.FinYear_ID;
                //invoice.InvoiceMode = _RETSAVE_GSTR.InvoiceMode;
                invoice.IsInter = _RETSAVE_GSTR.IsInter;
                invoice.ReceiverFinYear_ID = _RETSAVE_GSTR.ReceiverFinYear_ID;
                invoice.ParentInvoiceID = _RETSAVE_GSTR.ParentInvoiceID;
                invoice.InvoiceStatus = _RETSAVE_GSTR.InvoiceStatus;
                //   invoice.TaxBenefitingState = _RETSAVE_GSTR.TaxBenefitingState;
                invoice.Status = _RETSAVE_GSTR.Status;
                invoice.InvoiceSpecialCondition = _RETSAVE_GSTR.InvoiceSpecialCondition;
                invoice.CreatedBy = _RETSAVE_GSTR.CreatedBy;
                invoice.CreatedDate = _RETSAVE_GSTR.CreatedDate;
                invoice.UpdatedBy = _RETSAVE_GSTR.UpdatedBy;
                invoice.UpdatedDate = _RETSAVE_GSTR.UpdatedDate;

                unitOfWork.InvoiceRepository.Create(invoice);

                unitOfWork.Save();
                foreach (var item2 in invoice.GST_TRN_INVOICE_DATA)
                {
                    invoiceData = new GST_TRN_INVOICE_DATA();
                    invoiceData.InvoiceID = item2.InvoiceID;
                    invoiceData.LineID = item2.LineID;
                    invoiceData.Item_ID = item2.Item_ID;
                    invoiceData.Qty = item2.Qty;
                    invoiceData.TotalAmount = item2.TotalAmount;
                    invoiceData.Discount = item2.Discount;
                    invoiceData.TaxableAmount = item2.TaxableAmount;
                    invoiceData.TotalAmountWithTax = item2.TotalAmountWithTax;
                    invoiceData.CessAmt = Convert.ToDecimal(item2.CessAmt);
                    invoiceData.CessRate = Convert.ToDecimal(item2.CessRate);
                    invoiceData.CGSTAmt = Convert.ToDecimal(item2.CGSTAmt);
                    invoiceData.CGSTRate = Convert.ToDecimal(item2.CGSTRate);
                    invoiceData.IGSTAmt = Convert.ToDecimal(item2.IGSTAmt);
                    invoiceData.IGSTRate = Convert.ToDecimal(item2.IGSTRate);
                    invoiceData.SGSTAmt = Convert.ToDecimal(item2.SGSTAmt);
                    invoiceData.SGSTRate = Convert.ToDecimal(item2.SGSTRate);
                    invoiceData.TaxableAmount = Convert.ToDecimal(item2.TaxableAmount);
                    invoiceData.Status = item2.Status;
                    invoiceData.InvoiceDataStatus = item2.InvoiceDataStatus;
                    invoiceData.CreatedBy = item2.CreatedBy;
                    invoiceData.CreatedDate = item2.CreatedDate;
                    invoiceData.UpdatedBy = item2.UpdatedBy;
                    invoiceData.UpdatedDate = item2.UpdatedDate;
                    unitOfWork.InvoiceDataRepository.Create(invoiceData);
                    unitOfWork.Save();
                }
                //return Json(new Dictionary<string, string>() { { "ref_id", "test" } });

            }
            catch (Exception ex)
            {
                return false; // return Json(new Dictionary<string, string>() { { "Error", "Some Error occured on server" } });
            }
            return true;
        }
        #endregion

        //otpnewamit
        #region "Get  GSTR1-4A details"
        /// <summary>
        /// Get All GSTR1 Summary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        //FileReturnGSTR1 Amits
        public List<PROC_FILERETURN_GSTR1_4A4B4C6B6C_B2B_Result> GetGSTR1_FileReturn_4(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_4(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_5A5B_B2C_Result> GetGSTR1_FileReturn_5(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_5(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_6A_EXPORT_Result> GetGSTR1_FileReturn_6(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_6(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_7C_B2C_OTHER_Result> GetGSTR1_FileReturn_7(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_7(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_8_NILRATE_EXEMPTED_NONGST_Result> GetGSTR1_FileReturn_8(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_8(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_B2C_LARGE_Result> GetGSTR1_FileReturn_9A_AMD_B2CL(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9A_LARGE_B2C(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_EXPORT_Result> GetGSTR1_FileReturn_9A_AMD_EXP(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9A_AMD_EXP(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_10_AMENDED_B2C_OTHER_Result> GetGSTR1_FileReturn_1_10_AMND(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_10_AMD_B2C(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_11A_AME_TAXLIABILITY_ADV_Result> GetGSTR1_FileReturn_11A_AMD(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_11A_AMD(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_11B1_11B2_ADJUSTMENT_ADV_Result> GetGSTR1_FileReturn_11B1_11B2(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_11B1_11B2(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_11A111A2_TAXLIABILITY_ADV_Result> GetGSTR1_FileReturn_11A1A2(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_11A1_11A2(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_11B_AMENDED_ADJ_ADV_Result> GetGSTR1_FileReturn_11B(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_11B(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9B_CRDR_REGISTER_Result> GetGSTR1_FileReturn_9B_CRDR_REG(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9B_CRDR_REG(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9B_CRDR_UNREGISTER_Result> GetGSTR1_FileReturn_9B_CRDRUNREG(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9B_CRDR_UNREG(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_REGISTER_Result> GetGSTR1_FileReturn_9C_AME_CRDRREG(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9C_AME_CRDR_REG(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_UNREGISTER_Result> GetGSTR1_FileReturn_9C_AMd_CRDRUNREG(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9C_AMd_CRDR_UNREG(SellerUserID, month).ToList();
            return result.ToList();
        }
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_B2B_INVOICES_Result> GetGSTR1_FileReturn_9A_AMDB2B(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR1_FileReturn_9A_AMD_B2B(SellerUserID, month).ToList();
            return result.ToList();
        }
        
        //181018
        //public List<PROC_FILERETURN_GSTR_1_HEADER_Result> GetGSTR_1_HeaderDetail(string SellerUserID, int month)
        //{
        //    var result = unitOfWork.GetGSTR_1_HeaderDetails(SellerUserID, month).ToList();
        //    return result;
        //}


        //testAMIT
      
        #endregion
        public List<PROC_FILE_GSTR_1_Test4A_details_Result> GetGSTR_1_Test4A_Details(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_1_Test4A_Details(SellerUserID, month).ToList();
            return result;
        }
        //TestAnkitaGSTR3B
        public List<PROC_FILERETURN_GSTR3B_3_1_Result> GetGSTR_3B_1(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_3B_3_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_3_2_Result> GetGSTR_3B_2(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_3B_3_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_5_Result> GetGSTR_3B_5(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_3B_3_5(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_5_1_Result> GetGSTR_3B_5_1(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_3B_5_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_ITC_Result> GetGSTR_3B_ITC(string SellerUserID, int month)
        {
            var result = unitOfWork.GetGSTR_3B_ITC(SellerUserID, month).ToList();
            return result;
        }




        #region "Get  GSTR1-4A data"
        /// <summary>
        /// Get All GSTR1 Summary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_4A_Result> BindFile_GSTR_4A(string userId, int monthName)
        {

            var invoice = unitOfWork.GetGSTR_1_4A(userId, monthName);
            return invoice.ToList();

        }

        #endregion
        #region "Get  GSTR1-4B data"
        /// <summary>
        /// Get All GSTR1-4B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_4B_Result> GetFile_GSTR_4B(string userId, int monthName)
        {

            var invoice = unitOfWork.GetGSTR_1_4B(userId, monthName);
            return invoice.ToList();

        }

        #endregion
        #region "Get  GSTR1-4C data"
        /// <summary>
        /// Get All GSTR1-4B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_4C_Result> GetFile_GSTR_4C(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_4C(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Invoice Return methods list"
        public List<PROC_FILE_GSTR_1_5A_Result> GetFile_GSTR_5A(string userId, int monthName)
        {

            var invoice = unitOfWork.GetGSTR_1_5A(userId, monthName);
            return invoice.ToList();

        }
        #endregion
        #region "Invoice Return methods list"
        public List<PROC_FILE_GSTR_1_5A_2_Result> GetFile_GSTR_5A_2(string userId, int monthName)
        {

            var invoice = unitOfWork.GetGSTR_1_5A1(userId, monthName);
            return invoice.ToList();

        }
        #endregion
        #region "Get  GSTR1-6A data"
        /// <summary>
        /// Get All GSTR1-6A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR1_6A_Result> GetFile_GSTR_1_6A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_6A(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-6B data"
        /// <summary>
        /// Get All GSTR1-6A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR1_6B_Result> GetFile_GSTR_1_6B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_6B(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-6C data"
        /// <summary>
        /// Get All GSTR1-6C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR1_6C_Result> GetFile_GSTR_1_6C(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_6C(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-7A_1 data"
        /// <summary>
        /// Get  GSTR1-7A_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_7A_1_Result> GetFile_GSTR_1_7A_1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_7A1(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-7A_2 data"
        /// <summary>
        /// Get  GSTR1-7A_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_7A_2_Result> GetFile_GSTR_1_7A_2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_7A2(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-7B_1 data"
        /// <summary>
        /// Get  GSTR1-7A_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_7B_1_Result> GetFile_GSTR_1_7B_1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_7B1(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-7B_2 data"
        /// <summary>
        /// Get  GSTR1-7A_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_7B_2_Result> GetFile_GSTR_1_7B_2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_7B2(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-8A data"
        /// <summary>
        /// Get  GSTR1-8A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_8A_Result> GetFile_GSTR_1_8A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_8A(userId, monthName);
            return invoice.ToList();
        }

        #endregion
        #region "Get  GSTR1-8B data"
        /// <summary>
        /// Get  GSTR1-8B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_8B_Result> GetFile_GSTR_1_8B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_8B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-8C data"
        /// <summary>
        /// Get  GSTR1-8C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_8C_Result> GetFile_GSTR_1_8C(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_8C(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-8D data"
        /// <summary>
        /// Get  GSTR1-8D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_8D_Result> GetFile_GSTR_1_8D(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_8D(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-9B data"
        /// <summary>
        /// Get  GSTR1-8D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_9B_Result> GetFile_GSTR_1_9B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_9B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-9C data"
        /// <summary>
        /// Get  GSTR1-9C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_9C_Result> GetFile_GSTR_1_9C(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_9C(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-10A data"
        /// <summary>
        /// Get  GSTR1-10A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_10AA_Result> GetFile_GSTR_1_10AA(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_10AA(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-10B data"
        /// <summary>
        /// Get  GSTR1-10A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_10BB_Result> GetFile_GSTR_1_10BB(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_10BB(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-11A data"
        /// <summary>
        /// Get  GSTR1-11A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_11A1_Result> GetFile_GSTR_1_11A1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_11A1(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-11A data"
        /// <summary>
        /// Get  GSTR1-11A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_11A2_Result> GetFile_GSTR_1_11A2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_11A2(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-11A data"
        /// <summary>
        /// Get  GSTR1-11A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_11B1_Result> GetFile_GSTR_1_11B1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_11B1(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-11B data"
        /// <summary>
        /// Get  GSTR1-11A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_11B2_Result> GetFile_GSTR_1_11B2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_11B2(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1-12 data"
        /// <summary>
        /// Get  GSTR1-11A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1_12_Result> GetGSTR_1_12(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_1_12(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-3 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2_3_Result> GetFile_GSTR2_3(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR2_3(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-4A data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2_4A_Result> GetFile_GSTR2_4A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR2_4A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-4B data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2_4B_Result> GetFile_GSTR2_4B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR2_4B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-6A data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2_6A_Result> GetFile_GSTR2_6A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR2_6A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-6B data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2_6B_Result> GetFile_GSTR2_6B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR2_6B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-8A data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_8A_Result> GetFile_GSTR2_8A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_8A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-8B data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_8B_Result> GetFile_GSTR2_8B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_8B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-9A data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_9A_Result> GetFile_GSTR2_9A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_9A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-9B data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_9B_Result> GetFile_GSTR2_9B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_9B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-10A_1 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_10A_1_Result> GetFile_GSTR2_10A_1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_10A_1(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-10A_2 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_10A_2_Result> GetFile_GSTR2_10A_2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_10A_2(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-10B_1 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_10B_1_Result> GetFile_GSTR2_10B_1(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_10B_1(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-10B_2 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_10B_2_Result> GetFile_GSTR2_10B_2(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_10B_2(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-11_A data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_11A_Result> GetFile_GSTR2_11_A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_11A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-11_B data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_11B_Result> GetFile_GSTR2_11_B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_11B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-12 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_12_Result> GetFile_GSTR2_12(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_12(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2-13 data"
        /// <summary>
        /// Get  GSTR2-1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2_13_Result> GetFile_GSTR2_13(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2_13(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Invoice Return methods list"

        /// <summary>
        /// Get All invoice list which falles under GSTR1 4A as per GSTN guidelines
        /// </summary>
        /// <param name="userId">User ID or Requested Seller ID </param>
        /// <param name="monthName">Month Name of Invoice</param>
        /// <returns></returns>
        //public List<PROC_FILE_GSTR_1_4A_Result> BindFile_GSTR_4B(string userId, int monthName)
        //{

        //    var invoice = unitOfWork.GetGSTR_1_4A(userId, monthName);
        //    return invoice.ToList();

        //}

        #endregion

        #region "Get All GSTR1 HSN Summary"
        /// <summary>
        /// Get All GSTR1 HSN Summary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_GSTR1_HSN_SUMMARY_Result> BindFile_GSTR1hsnSummary(string GSTNNO, int MONTH)
        {
            var GSTR1hsnSummary = unitOfWork.GetGSTR1hsnSummary(GSTNNO, MONTH);
            return GSTR1hsnSummary.ToList();
        }
        #endregion

        #region "Get All GSTR1 Summary"
        /// <summary>
        /// Get All GSTR1 Summary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_GSTR1_SUMMARY_Result> BindFile_GSTR1_Summary(string GSTNNO, int MONTH)
        {
            var GSTRSummary = unitOfWork.GetGSTR1_Summary(GSTNNO, MONTH);
            return GSTRSummary.ToList();
        }
        //public List<PROC_GSTR1_SUMMARY_Result> BindFile_GSTR1_Summary(string GSTNNO)
        //{
        //    var GSTRSummary = unitOfWork.GetGSTR1_Summary(GSTNNO, MONTH);
        //    return GSTRSummary.ToList();
        //}
        #endregion

        #region "Get  GSTR2A-3 data"
        /// <summary>
        /// Get  GSTR2A-3 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR2A_3_Result> GetGSTR_2A_3(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_3(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-4 data"
        /// <summary>
        /// Get  GSTR2A-4 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_FILE_GSTR2A_4_Result> GetGSTR_2A_4(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_4(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-5 data"
        /// <summary>
        /// Get  GSTR2A-5 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>


        public List<PROC_IMPORT_GSTR_2A_5_Result> GetGSTR_2A_5(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_5(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-6C data"
        /// <summary>
        /// Get  GSTR2A-6C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR_2A_6_CREDIT_Result> GetGSTR_2A_6C(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_6C(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-6I data"
        /// <summary>
        /// Get  GSTR2A-6C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR_2A_6_INVOICE_Result> GetGSTR_2A_6I(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_6I(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-7A data"
        /// <summary>
        /// Get  GSTR2A-7A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR_2A_7A_Result> GetGSTR_2A_7A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_7A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR2A-7B data"
        /// <summary>
        /// Get  GSTR2A-7B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR_2A_7B_Result> GetGSTR_2A_7B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR_2A_7B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        //1A
        #region "Get  GSTR1A-3A data"
        /// <summary>
        /// Get  GSTR1A-3A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR1A_3A_Result> GetGSTR1A_3A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR1A_3A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1A-3B data"
        /// <summary>
        /// Get  GSTR1A-3B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_FILE_GSTR1A_3B_Result> GetGSTR1A_3B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR1A_3B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1A-4A data"
        /// <summary>
        /// Get  GSTR1A-4A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_FILE_GSTR_1A_4A_Result> GetGSTR1A_4A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR1A_4A(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1A-4B data"
        /// <summary>
        /// Get  GSTR1A-4B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_FILE_GSTR_1A_4B_Result> GetGSTR1A_4B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR1A_4B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR1A-5 data"
        /// <summary>
        /// Get  GSTR1A-5 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_FILE_GSTR_1A_5_Result> GetGSTR1A_5(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR1A_5(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_1A data"
        /// <summary>
        /// Get  GSTR3_4_1A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>


        public List<PROC_IMPORT_GSTR3_4_1_A_Result> GetGSTR3_4_1_A(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_1_A(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_1_B data"
        /// <summary>
        /// Get  GSTR3_4_1_B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_4_1_B_Result> GetGSTR3_4_1_B(string userId, int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_1_B(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_1_C data"
        /// <summary>
        /// Get  GSTR3_4_1_C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_4_1_C_Result> GetGSTR3_4_1_C(string userId,int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_1_C(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_1_D data"
        /// <summary>
        /// Get  GSTR3_4_1_D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        
        public List<PROC_IMPORT_GSTR3_4_1_D_Result> GetGSTR3_4_1_D(string userId,int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_1_D(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_2_A data"
        /// <summary>
        /// Get  GSTR3_4_2_A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        
        public List<PROC_IMPORT_GSTR3_4_2_A_Result> GSTR3_4_2_A(string userId,int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_2_A(userId, monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_4_2_B data"
        /// <summary>
        /// Get  GSTR3_4_2_B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_4_2_B_Result> GetGSTR3_4_2_B(string userId,int monthName)
        {
            var invoice = unitOfWork.GetGSTR3_4_2_B(userId, monthName);
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR3_4_2_C data"
        /// <summary>
        /// Get  GSTR3_4_2_C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_4_2_C_Result> GetGSTR3_4_2_C(string userId,int monthName)
        {
            var invoice=unitOfWork.GetGSTR3_4_2_C(userId,monthName);
            return invoice.ToList();

        }
        #endregion
        #region "Get  GSTR3_5A_1 data"
        /// <summary>
        /// Get  GSTR3_5A_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_5A_1_Result> GetGSTR3_5A_1 (string userId,int monthName)
        {
            var invoice =unitOfWork.GetGSTR3_5A_1(userId,monthName);
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_5A_2 data"
        /// <summary>
        /// Get  GSTR3_5A_2 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_5A_2_Result> GetGSTR3_5A_2(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_5A_2(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_5B_1 data"
        /// <summary>
        /// Get  GSTR3_5B_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_5B_1_Result> GetGSTR3_5B_1(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_5B_1(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_5B_2 data"
        /// <summary>
        /// Get  GSTR3_5B_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_5B_2_Result> GetGSTR3_5B_2(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_5B_2(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion


        #region "Get  GSTR3_6_1 data"
        /// <summary>
        /// Get  GSTR3_6_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_6_1_Result> GetGSTR3_6_1(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_6_1(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_6_2 data"
        /// <summary>
        /// Get  GSTR3_6_2 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_6_2_Result> GetGSTR3_6_2(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_6_2(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_6_2 data"
        /// <summary>
        /// Get  GSTR3_6_2 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_7_Result> GetGSTR3_7(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_7(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_8A data"
        /// <summary>
        /// Get  GSTR3_8A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_8A_Result> GetGSTR3_8A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_8A(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion
        #region "Get  GSTR3_8B data"
        /// <summary>
        /// Get  GSTR3_8B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_8B_Result> GetGSTR3_8B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_8B(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion
        #region "Get  GSTR3_8B data"
        /// <summary>
        /// Get  GSTR3_8B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_8C_Result> GetGSTR3_8C(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_8C(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_8D data"
        /// <summary>
        /// Get  GSTR3_8D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_8D_Result> GetGSTR3_8D(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_8D(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_9A data"
        /// <summary>
        /// Get  GSTR3_9A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_9A_Result> GetGSTR3_9A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_9A(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion

        #region "Get  GSTR3_9A data"
        /// <summary>
        /// Get  GSTR3_9A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_9B_Result> GetGSTR3_9B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_9B(userId, month).ToList();
            return invoice.ToList(); 
        }
        #endregion
        #region "Get  GSTR3_10A data"
        /// <summary>
        /// Get  GSTR3_10A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_10A_Result> GetGSTR3_10A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_10A(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

        #region "Get  GSTR3_10B data"
        /// <summary>
        /// Get  GSTR3_10B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_10B_Result> GetGSTR3_10B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_10B(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get  GSTR3_10C data"
        /// <summary>
        /// Get  GSTR3_10C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_10C_Result> GetGSTR3_10C(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_10C(userId, month).ToList();
            return  invoice.ToList();
        }
        #endregion
        #region "Get  GetGSTR3_10D data"
        /// <summary>
        /// Get  GetGSTR3_10D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_10D_Result> GetGSTR3_10D(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_10D(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_10D data"
        /// <summary>
        /// Get GSTR3_10D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_11_Result> GetGSTR3_11(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_11(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_12A data"
        /// <summary>
        /// Get GSTR3_12A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_12A_Result> GetGSTR3_12A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_12A(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_12B data"
        /// <summary>
        /// Get GSTR3_12B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_12B_Result> GetGSTR3_12B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_12B(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion


        #region "Get GSTR3_12C data"
        /// <summary>
        /// Get GSTR3_12C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_12C_Result> GetGSTR3_12C(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_12C(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_12D data"
        /// <summary>
        /// Get GSTR3_12D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_12D_Result> GetGSTR3_12D(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_12D(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_13_1 data"
        /// <summary>
        /// Get GSTR3_13_1 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_13_1_Result> GetGSTR3_13_1(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_13_1(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_13_2 data"
        /// <summary>
        /// Get GSTR3_13_2 data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_13_2_Result> GetGSTR3_13_2(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_13_2(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_14A data"
        /// <summary>
        /// Get GSTR3_14A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_14_A_Result> GetGSTR3_14A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_14A(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

        #region "Get GSTR3_14B data"
        /// <summary>
        /// Get GSTR3_14B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>

        public List<PROC_IMPORT_GSTR3_14_B_Result> GetGSTR3_14B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_14B(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion


        #region "Get GSTR3_14C data"
        /// <summary>
        /// Get GSTR3_14C data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_14_C_Result> GetGSTR3_14C(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_14C(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_14D data"
        /// <summary>
        /// Get GSTR3_14D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_14_D_Result> GetGSTR3_14D(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_14D(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

        #region "Get GSTR3_15A data"
        /// <summary>
        /// Get GSTR3_15A data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_15_A_Result> GetGSTR3_15A(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_15A(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion
        #region "Get GSTR3_15B data"
        /// <summary>
        /// Get GSTR3_15B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_15_B_Result> GetGSTR3_15B(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_15B(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion


        #region "Get GSTR3_15B data"
        /// <summary>
        /// Get GSTR3_15B data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_15_C_Result> GetGSTR3_15C(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_15C(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

        #region "Get GSTR3_15D data"
        /// <summary>
        /// Get GSTR3_15D data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="monthName"></param>
        /// <returns></returns>
        public List<PROC_IMPORT_GSTR3_15_D_Result> GetGSTR3_15D(string userId, int month)
        {
            var invoice = unitOfWork.GetGSTR3_15D(userId, month).ToList();
            return invoice.ToList();
        }
        #endregion

    }


}