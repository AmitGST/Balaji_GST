using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using businessAccessLayer;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using Microsoft.AspNet.Identity;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR2PreviewB2B : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();
        #region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindAllGSTN2List(MonthName);
                BindAllGSTN2List((DateTime.Now.Month-1));
            }
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
        }
        #endregion
        //int mName;
        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {

            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            BindAllGSTN2List(mName);
        }


        private void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }
        int MonthName;
        public void BindAllGSTN2List(int MonthName)
        {
            GetFile_GSTR2_3(MonthName);
            GetFile_GSTR2_4A(MonthName);
            GetFile_GSTR2_4B(MonthName);
            //lv_GSTR2_6A
            GetFile_GSTR2_6B(MonthName);
            GetFile_GSTR2_8A(MonthName);
            GetFile_GSTR2_8B(MonthName);
            GetFile_GSTR2_9A(MonthName);
            GetFile_GSTR2_9B(MonthName);
            GetFile_GSTR2_11_A(MonthName);
            GetFile_GSTR2_11_B(MonthName);
            GetFile_GSTR2_12(MonthName);
            GetFile_GSTR2_13(MonthName);

            //
        }
        private void GetFile_GSTR2_3(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_3, _invoice.GetFile_GSTR2_3(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_4A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_4A, _invoice.GetFile_GSTR2_4A(loggedinUserId, MonthName));
               
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_4B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_4B, _invoice.GetFile_GSTR2_4B(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_6A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_6A, _invoice.GetFile_GSTR2_6A(loggedinUserId, MonthName));
            }

            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_6B(int MonthName)
        {
            //try
            //{
            //    var loggedinUserId = Common.LoggedInUserID();
            //    BindListView(lv_GSTR2_6B, _invoice.GetFile_GSTR2_6B(loggedinUserId, MonthName));
            //}
            //catch(Exception ex)
            //{
            //    cls_ErrorLog ob = new cls_ErrorLog();
            //    cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            //}
        }

        //binding of listview (null)
        private void GetFile_GSTR2_8A(int MonthName)
        {
            try {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_8A, _invoice.GetFile_GSTR2_8A(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        private void GetFile_GSTR2_8B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_8B, _invoice.GetFile_GSTR2_8B(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_9A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_9A, _invoice.GetFile_GSTR2_9A(loggedinUserId, MonthName));
               
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_9B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_9B, _invoice.GetFile_GSTR2_9B(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_10A_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_10A1, _invoice.GetFile_GSTR2_10A_1(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_10A_2(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_10A2, _invoice.GetFile_GSTR2_10A_2(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_10B_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_10B1, _invoice.GetFile_GSTR2_10B_1(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_10B_2(int MonthName)
        {
            try
            {

                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_10B2, _invoice.GetFile_GSTR2_10B_2(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_11_A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_11A, _invoice.GetFile_GSTR2_11_A(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetFile_GSTR2_11_B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_11B, _invoice.GetFile_GSTR2_11_B(loggedinUserId, MonthName));
               
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_12(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_12, _invoice.GetFile_GSTR2_12(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR2_13(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2_13, _invoice.GetFile_GSTR2_13(loggedinUserId, MonthName));
                
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //End
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkvGSTR2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                    DateTime lastDate = DateTime.Now.LastDayOfMonth();
                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                    //var modifiedInvoice = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.InvoiceStatus != (byte)EnumConstants.InvoiceStatus.Fresh && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && (f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A || (f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Upload && (f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import || f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)))
                        && (f.ReceiverInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.NA || f.ReceiverInvoiceAction != null) && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    //again filter due to non working code:TODO optimise
                    var filterInvoice = invoices.Where(w => w.ReceiverInvoiceAction != null).ToList().Where(f=>f.ReceiverInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.NA).ToList();


                    List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                    List<string> mailsToList = new List<string>();

                    List<string> invID = new List<string>();
                    foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in filterInvoice)
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();//For both line for Email process
                        string invoiceNo = inv.GST_TRN_INVOICE.InvoiceNo;//

                        string id = inv.InvoiceID.ToString();
                        audittrail.InvoiceID = inv.InvoiceID;
                        audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR2);
                        audittrail.UserIP = Common.IP;
                        audittrail.InvoiceAction = inv.InvoiceAction;
                        audittrail.ReceiverInvoiceAction = inv.ReceiverInvoiceAction;
                        audittrail.ReceiverInvoiceActionDate = inv.ReceiverInvoiceActionDate;
                        audittrail.CreatedDate = DateTime.Now;
                        audittrail.CreatedBy = loggedinUserId;
                        unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                        unitOfWork.Save();
                        count = count + 1;
                        if (!mailsToList.Contains(inv.GST_TRN_INVOICE.AspNetUser.Email))
                        {
                            mailsToList.Add(inv.GST_TRN_INVOICE.AspNetUser.Email);
                        }
                        attribute.UserName = inv.GST_TRN_INVOICE.AspNetUser.OrganizationName;
                        //attribute.MailsTo.Add();
                        attribute.InvoiceNo = invoiceNo;
                        attribute.InvoiceDate = DateTimeAgo.GetFormatDate(inv.GST_TRN_INVOICE.InvoiceDate);
                        attribute.InvoiceTotalAmount = inv.GST_TRN_INVOICE.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                        invAttributes.Add(attribute);
                        invID.Add(id);
                    }
                    if (count > 0)
                    {

                        string mailString = string.Empty;
                        string sellerMail = string.Empty;
                        clsMessageAttribute mailData = new clsMessageAttribute();

                        foreach (clsMessageAttribute iId in invAttributes)
                        {
                            mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                            mailString += "<td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                            mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                            mailData.UserName = iId.UserName;
                        }


                        this.Master.SuccessMessage = "Data Filed Successfully.";
                        SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    }
                    else
                    {
                        this.Master.WarningMessage = "There is no Data to File.";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    }
                
                }
                //var month = DateTime.Now.Month-1;
                //BindAllGSTN2List(month);
                BindAllGSTN2List((DateTime.Now.Month));
              
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                this.Master.ErrorMessage = ex.Message;
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
            }
        }
        public void SendHTMLMail(clsMessageAttribute mailData, string mailString, string sellerEmail)
        {
            try
            {
                cls_Message message = new cls_Message();
                EmailService email = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                Dictionary<string, string> replaceItem = new Dictionary<string, string>();
                replaceItem.Add("@User", mailData.UserName);
                replaceItem.Add("@InvoiceData", mailString);
                string mailBody = message.GetMessage(EnumConstants.Message.FileGSTR2, replaceItem);
                msg.Body = mailBody;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "GSTR-2 Filed";
                email.Send(msg);
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //        #region PopulaterReceiverDtls
        //        public void PopulaterReceiverDtls(string strReceiverGSTN)
        //        {
        //            ds = excelDB.PopulateGSTR2ADtls(strReceiverGSTN);

        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    lblGSTIN.Text =  Convert.ToString(ds.Tables[0].Rows[0][0]).Trim();
        //                    lblGSTINVal.Text = Convert.ToString(ds.Tables[0].Rows[0][0]).Trim();
        //                    lbltaxpayerName.Text =  Convert.ToString(ds.Tables[0].Rows[0][1]).Trim();
        //                    period = Convert.ToString(ds.Tables[0].Rows[0][2]).Trim();
        //                    month = period.Substring(0, 2);
        //                    year = period.Substring(period.Length - 4, 4);
        //                    lblPeriod.Text =  "Month: " + month + "      " + "Year: " + year;
        //                }
        //            }
        //        }
        //        #endregion

        //        #region BindGrid
        //        protected void BindGrid()
        //        {
        //            DataSet ds = new DataSet();
        //            ds = ViewGSTR2(lblGSTINVal.Text.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
        //            GVGSTR2.DataSource = ds.Tables[1];
        //            GVGSTR2.DataBind();
        //        }
        //        #endregion       

        //        #region ViewGSTR2
        //        private DataSet ViewGSTR2(string SellerGSTN, string FromDt, String Todate)
        //        {
        //            int p = 0; int m = 0;
        //            DataTable dt = new DataTable();
        //            string strInvoiceNo = string.Empty;
        //            string strInvoiceType = "B2BInvoice";

        //            int result = 0;

        //            /* NEWLY ADDED */
        //            Reciever reciever = new Reciever();
        //            reciever.Seller = new Seller();
        //            reciever.Consignee = new Consignee();
        //            reciever.RecieverData = new List<Reciever>();
        //            /* NEWLY ADDED */

        //            //TEMPORARY SOLUTION.NEED TO CHNAGE LATER. 
        //            // when directly applying B2BInvoice error occured in next step. 
        //            // unable to new seller invoice ,updated with last data .
        //            // need to discuss with TL
        //            #region INVOICE_TYPE
        //            switch (strInvoiceType)
        //            {
        //                case "B2BInvoice":
        //                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
        //                    reciever.Invoice = b2bInvoice;
        //                    break;

        //            }
        //            #endregion

        //            /* NEWLY ADDED */
        //            reciever.Invoice.LineEntry = new List<LineEntry>();
        //            LineEntry line;

        //            ds = excelDB.ViewGSTR2(strReceiverGSTN, FromDt, Todate);

        //            if (ds.Tables.Count > 0)
        //            {
        //                #region B2B
        //                if (ds.Tables.Contains("SellerDtls"))
        //                {
        //                    if (ds.Tables["SellerDtls"].Rows.Count > 0)
        //                    {
        //                        int j = ds.Tables[0].Rows.Count;

        //                        for (int i = 0; i <= j - 1; i++)
        //                        {
        ////InvoiceNo	Invoicedate	UploadedInvoicedate	SellerGSTN	SellerName	ReceiverGSTN	ReceiverName	ConsigneeGSTN	ConsigneeName	
        ////SellerAddress	SellerStateCode	SellerStateName	SellerStateCodeID	ReceiverStateCode	ConsigneeStateCode	Freight	Insurance	
        ////PackingAndForwadingCharges	IsElectronicReferenceNoGenerated	ElectronicReferenceNoGenerated	ElectronicReferenceNoGeneratedDate	
        ////IsMatched	IsAdvance	InvoiceType	IsExport	SellerGrossTurnOver	SellerFinancialPeriod	ReceiverGrossTurnOver	ReceiverFinancialPeriod	
        ////ConsigneeGrossTurnOver	ConsigneeFinancialPeriod	FileGSTR2ADate	IsUploadGSTR2	IsEditedByReceiver	IsAddedByReceiver	IsAcceptedByReceiver	
        ////IsInter	ReceiverStateName	ConsigneeStateName	IsDeletedByReceiver


        //                            reciever.Seller.SellerInvoice = (ds.Tables[0].Rows[i]["InvoiceNo"]).ToString();
        //                            reciever.Seller.DateOfInvoice = (ds.Tables[0].Rows[i]["Invoicedate"]).ToString();
        //                            reciever.Seller.GSTIN = (ds.Tables[0].Rows[i]["SellerGSTN"]).ToString();
        //                            reciever.Seller.NameAsOnGST = (ds.Tables[0].Rows[i]["SellerName"]).ToString();
        //                            reciever.GSTIN = (ds.Tables[0].Rows[i]["ReceiverGSTN"]).ToString();
        //                            reciever.NameAsOnGST = (ds.Tables[0].Rows[i]["ReceiverName"]).ToString();
        //                            reciever.Consignee.GSTIN = (ds.Tables[0].Rows[i]["ConsigneeGSTN"]).ToString();
        //                            reciever.Consignee.NameAsOnGST = (ds.Tables[0].Rows[i]["ConsigneeName"]).ToString();
        //                            reciever.Seller.Address = (ds.Tables[0].Rows[i]["SellerAddress"]).ToString();
        //                            reciever.Seller.SellerStateCode = (ds.Tables[0].Rows[i]["SellerStateCode"]).ToString();
        //                            reciever.Seller.SellerStateName = (ds.Tables[0].Rows[i]["SellerStateName"]).ToString();
        //                            reciever.Seller.SellerStateCodeID = (ds.Tables[0].Rows[i]["SellerStateCodeID"]).ToString();
        //                            reciever.StateCode = (ds.Tables[0].Rows[i]["ReceiverStateCode"]).ToString();
        //                            reciever.Consignee.StateCode = (ds.Tables[0].Rows[i]["ConsigneeStateCode"]).ToString();
        //                            reciever.Invoice.Freight = Convert.ToInt32(ds.Tables[0].Rows[i]["Freight"]);
        //                            reciever.Invoice.Insurance = Convert.ToInt32(ds.Tables[0].Rows[i]["Insurance"]);
        //                            reciever.Invoice.PackingAndForwadingCharges = Convert.ToInt32(ds.Tables[0].Rows[i]["PackingAndForwadingCharges"]);

        //                            reciever.Seller.SellerGrossTurnOver = Convert.ToDecimal(ds.Tables[0].Rows[i]["SellerGrossTurnOver"]);
        //                            reciever.Seller.SellerFinancialPeriod = (ds.Tables[0].Rows[i]["SellerFinancialPeriod"]).ToString();

        //                            reciever.FinancialPeriod = (ds.Tables[0].Rows[i]["ReceiverFinancialPeriod"]).ToString();

        //                            reciever.StateName = (ds.Tables[0].Rows[i]["ReceiverStateName"]).ToString();
        //                            reciever.Consignee.StateName = (ds.Tables[0].Rows[i]["ConsigneeStateName"]).ToString();

        //                            reciever.Invoice.IsAdvancePaymentChecked = false;
        //                            reciever.Invoice.IsExportChecked = false;

        //                            if ((ds.Tables[0].Rows[i]["IsEditedByReceiver"]).ToString() == "False")
        //                            {
        //                                reciever.Seller.IsEditedByReceiver = false;
        //                            }
        //                            else
        //                            {
        //                                reciever.Seller.IsEditedByReceiver = true;
        //                            }
        //                            if ((ds.Tables[0].Rows[i]["IsAddedByReceiver"]).ToString() == "False")
        //                            {
        //                                reciever.Seller.IsAddedByReceiver = false;
        //                            }
        //                            else
        //                            {
        //                                reciever.Seller.IsAddedByReceiver = true;
        //                            }
        //                            if ((ds.Tables[0].Rows[i]["IsAcceptedByReceiver"]).ToString() == "False")
        //                            {
        //                                reciever.Seller.IsAcceptedByReceiver = false;
        //                            }
        //                            else
        //                            {
        //                                reciever.Seller.IsAcceptedByReceiver = true;

        //                            }
        //                            if ((ds.Tables[0].Rows[i]["IsDeletedByReceiver"]).ToString() == "False")
        //                            {
        //                                reciever.Seller.IsDeletedByReceiver = false;
        //                            }
        //                            else
        //                            {
        //                                reciever.Seller.IsDeletedByReceiver = true;

        //                            }
        //                            if ((ds.Tables[0].Rows[i]["IsRejectedByReceiver"]).ToString() == "False")
        //                            {
        //                                reciever.Seller.IsRejectedByReceiver = false;
        //                            }
        //                            else
        //                            {
        //                                reciever.Seller.IsRejectedByReceiver = true;

        //                            }

        //                            int n = ds.Tables[1].Rows.Count;
        //                            for (m = p; m <= n - 1; m++)
        //                            {

        //                                if ((ds.Tables[0].Rows[i]["InvoiceNo"]).ToString() == (ds.Tables[1].Rows[m]["InvoiceNo"]).ToString())
        //                                {
        //                                    line = new LineEntry();

        //                                    line.HSN = new com.B2B.GST.GSTInvoices.HSN();


        //                                    line.LineID = Convert.ToInt32(ds.Tables[1].Rows[m]["LineID"]);
        //                                    line.HSN.Description = (ds.Tables[1].Rows[m]["Description"]).ToString();
        //                                    line.HSN.HSNNumber = (ds.Tables[1].Rows[m]["HSN"]).ToString();
        //                                    line.Qty = Convert.ToDecimal(ds.Tables[1].Rows[m]["Qty"]);
        //                                    line.HSN.UnitOfMeasurement = (ds.Tables[1].Rows[m]["Unit"]).ToString();
        //                                    line.PerUnitRate = Convert.ToDecimal(ds.Tables[1].Rows[m]["Rate"]);
        //                                    line.TotalLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["Total"]);
        //                                    line.Discount = Convert.ToDecimal(ds.Tables[1].Rows[m]["Discount"]);
        //                                    line.TaxValue = Convert.ToDecimal(ds.Tables[1].Rows[m]["TaxableValue"]);
        //                                    line.AmountWithTax = Convert.ToDecimal(ds.Tables[1].Rows[m]["AmountWithTax"]);
        //                                    line.HSN.RateIGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["IGSTRate"]);
        //                                    line.AmtIGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["IGSTAmt"]);
        //                                    line.HSN.RateCGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["CGSTRate"]);
        //                                    line.AmtCGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["CGSTAmt"]);
        //                                    line.HSN.RateSGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["SGSTRate"]);
        //                                    line.AmtSGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["SGSTAmt"]);
        //                                    line.HSN.Cess = Convert.ToDecimal(ds.Tables[1].Rows[m]["Cess"]);

        //                                    reciever.Seller.TotalQty = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalQty"]);
        //                                    reciever.Seller.TotalRate = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalRate"]);
        //                                    reciever.Seller.TotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalAmount"]);
        //                                    reciever.Seller.TotalDiscount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalDiscount"]);
        //                                    reciever.Seller.TotalTaxableAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalTaxableAmount"]);
        //                                    reciever.Seller.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalCGSTAmount"]);
        //                                    reciever.Seller.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalSGSTAmount"]);
        //                                    reciever.Seller.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalIGSTAmount"]);
        //                                    reciever.Seller.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalAmountWithTax"]);
        //                                    reciever.Seller.GrandTotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["GrandTotalAmount"]);
        //                                    reciever.Seller.GrandTotalAmountInWord = Convert.ToString(ds.Tables[1].Rows[m]["GrandTotalAmountInWord"]);

        //                                    if (((ds.Tables[1].Rows[m]["IsInter"]).ToString()) == "True")
        //                                    {
        //                                        line.IsInter = true;
        //                                    }
        //                                    else
        //                                    {
        //                                        line.IsInter = false;
        //                                    }

        //                                    reciever.Invoice.LineEntry.Add(line);
        //                                    p = 1 + p;
        //                                }
        //                                else
        //                                {
        //                                    break;
        //                                }
        //                            }
        //                            reciever.RecieverData.Add(reciever);
        //                            Session["GSTR2ReceiverDaTa"] = reciever.RecieverData;

        //                            reciever = new Reciever();
        //                            reciever.Seller = new Seller();
        //                            reciever.Consignee = new Consignee();
        //                            reciever.RecieverData = new List<Reciever>();

        //                            //TEMPORARY SOLUTION.NEED TO CHNAGE LATER                        
        //                            Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
        //                            reciever.Invoice = b2bInvoice;

        //                            reciever.Invoice.LineEntry = new List<LineEntry>();

        //                            if ((List<Reciever>)Session["GSTR2ReceiverDaTa"] != null)
        //                                reciever.RecieverData = (List<Reciever>)Session["GSTR2ReceiverDaTa"];


        //                        }



        //                    }
        //                }
        //                else
        //                {                   
        //                      BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
        //                    ////masterPage.ShowModalPopup();
        //                    ////masterPage.ErrorMessage = "System error occured during data population of B2B Invoice !!!";

        //                }
        //                #endregion
        //            }
        //            else
        //            {

        //                  BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
        //                ////masterPage.ShowModalPopup();
        //                ////masterPage.ErrorMessage = "System Error !";
        //            }
        //            if (ds.Tables["SellerDtls"].Rows.Count == 0 && ds.Tables["InvoiceDtls"].Rows.Count == 0 && ds.Tables["SummeryDtls"].Rows.Count == 0)
        //            {
        //                btnFileGSTR2.Attributes.Add("style", "display:none");

        //                  BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
        //                ////masterPage.ShowModalPopup();
        //                ////masterPage.ErrorMessage = "No data to view !!!";
        //            }
        //            return ds;
        //        }
        //        #endregion


        //        #region btnFileGSTR2_Click
        //        protected void btnFileGSTR2_Click(object sender, EventArgs e)
        //        {
        //            fileAll();
        //        }

        //        protected void fileAll()
        //        {
        //            int result = 0;
        //            Reciever reciever = new Reciever();
        //            reciever.RecieverData = new List<Reciever>();

        //            if ((List<Reciever>)Session["GSTR2ReceiverDaTa"] != null)
        //                reciever.RecieverData = (List<Reciever>)Session["GSTR2ReceiverDaTa"];

        //            result = excelDB.SaveGSTR2(reciever.RecieverData, strInvoiceNo);

        //            if (result > 0)
        //            {
        //                if (Session["GSTR2ReceiverDaTa"] != null)
        //                    Session["GSTR2ReceiverDaTa"] = null;

        //                BindGrid();

        //                  BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
        //                //masterPage.ShowModalPopupSuccess();
        //                ////masterPage.ErrorMessage = "Data saved Successfully !!!";

        //            }
        //            else
        //            {
        //                if (Session["GSTR2ReceiverDaTa"] != null)
        //                    Session["GSTR2ReceiverDaTa"] = null;

        //                  BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
        //                ////masterPage.ShowModalPopup();
        //                ////masterPage.ErrorMessage = "Error !!! Unable to save Data!";

        //            }

        //        }
        //        #endregion

        //        #region BACK_TO_PREVIOUS_PAGE
        //        protected void btnBack_Click(object sender, EventArgs e)
        //        {
        //            strReceiverGSTN = HttpUtility.UrlEncode(excelDB.Encrypt(strReceiverGSTN));
        //            Response.Redirect("~/fileReturn.aspx", true);
        //        }
        //        #endregion
    
    }
}