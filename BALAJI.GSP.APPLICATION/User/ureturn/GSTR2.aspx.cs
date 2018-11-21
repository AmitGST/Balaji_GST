using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Globalization;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR2 : System.Web.UI.Page
    {
        cls_Invoice invoiceItems = new cls_Invoice();
        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                invoiceItems.MisMatchInvoice(Common.LoggedInUserID());//MisMatchInvoice
                //BindViewInvoice(); 
                BindMisMatchInvoice();
                lvRegularInvoice.Visible = false;
                divMain.Visible = true;
                BindViewInvoice(Convert.ToByte(DateTime.Now.Month - 1));
            }
            uc_InvoiceEdit.UpdateInvoiceClick += uc_InvoiceEdit_UpdateInvoiceClick;
            uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
            // uc_invoiceMonth2.SelectedIndexChange += uc_InvoiceMonth2_SelectedIndexChanged;
        }
        //private void uc_InvoiceMonth2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ViewState["CommandName"].ToString() != null)
        //        {
        //            PopulateInvoices(ViewState["CommandName"].ToString(), Convert.ToByte(uc_invoiceMonth2.GetValue));
        //        }
        //    }
        //    catch(Exception ex){
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        private void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //private void BindViewInvoice(byte SelectedMonth)
        //{

        //    var loggedinUserId = Common.LoggedInUserID();
        //    if (loggedinUserId != null)
        //    {
        //        unitOfWork = new UnitOfWork();
        //        DateTime firstdate = DateTime.Now.FirstDayOfMonth();
        //        DateTime lastDate = DateTime.Now.LastDayOfMonth();
        //        //  var invoice = unitOfWork.InvoiceRepository.Filter(f => f.CreatedBy == loggedinUserId && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.CreatedDate).ToList();

        //        var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.CreatedDate.Value.Month == SelectedMonth
        //          && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
        //            //&& !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true
        //          && (f.GST_TRN_INVOICE.CreatedDate >= firstdate && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(o => o.CreatedDate).ToList();

        //        var existItems = invoices.Select(s => s.InvoiceID);

        //        var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId

        //            && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1 && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
        //            && f.CreatedDate.Value.Month == SelectedMonth
        //            && !existItems.Contains(f.InvoiceID)
        //            && f.GST_TRN_INVOICE.Status == true && (f.GST_TRN_INVOICE.CreatedDate >= firstdate
        //            && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(f => f.GST_TRN_INVOICE.CreatedDate).ToList();

        //        var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;

        //        lvInvoices.DataSource = items.OrderByDescending(o => o.CreatedDate).ToList();
        //        lvInvoices.DataBind();

        //    }

        //}
        void uc_InvoiceEdit_UpdateInvoiceClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["CommandName"].ToString() != null)
                {
                    //PopulateInvoices(ViewState["CommandName"].ToString(), Convert.ToByte(uc_invoiceMonth2.GetValue));
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindMisMatchInvoice()
        {

        }
        protected void lkcImportConsolidated_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (count > 0)
                {
                    invoiceItems.MisMatchInvoice(Common.LoggedInUserID());//MisMatchInvoice

                    this.Master.SuccessMessage = "Purchase Re-Conciliation Successfully.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                }
                else
                {
                    this.Master.WarningMessage = "There is no file for Re-Conciliation.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                //uc_sucess.Visible = true;

                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindViewInvoice(byte SelectMonth)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    unitOfWork = new UnitOfWork();
                    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                    DateTime lastDate = DateTime.Now.LastDayOfMonth();

                    //-------------------
                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == SelectMonth
                    && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh // (f.GST_TRN_INVOICE.InvoiceStatus!= (byte)EnumConstants.InvoiceStatus.Amended || f.GST_TRN_INVOICE.InvoiceStatus != (byte)EnumConstants.InvoiceStatus.Modified)
                    && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();

                    var existItems = invoices.Select(s => s.InvoiceID);
                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == SelectMonth
                        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1
                        && !existItems.Contains(f.InvoiceID)
                        && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
                        && f.CreatedDate <= lastDate)).ToList();

                    var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;
                    //------------------

                    var receiverInvoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == SelectMonth
                        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Upload && (f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import || f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)
                        && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh // (f.GST_TRN_INVOICE.InvoiceStatus!= (byte)EnumConstants.InvoiceStatus.Amended || f.GST_TRN_INVOICE.InvoiceStatus != (byte)EnumConstants.InvoiceStatus.Modified)
                    && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.CreatedDate).ToList();

                    var receiverExistItems = receiverInvoices.Select(s => s.InvoiceID);

                    var fileReceiverInvoice = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == SelectMonth
                       && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2
                       && (f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import || f.GST_TRN_INVOICE.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)
                       && !receiverExistItems.Contains(f.InvoiceID)
                       && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
                       && f.CreatedDate <= lastDate)).ToList();

                    var filterItems = items.Where(w => w.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh && w.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A).ToList();

                    var joinItems = (fileReceiverInvoice != null || fileReceiverInvoice.Count() > 0) ? ((receiverInvoices != null || receiverInvoices.Count() > 0) ? receiverInvoices.Union(fileReceiverInvoice) : fileReceiverInvoice) : receiverInvoices;

                    var allItems = (joinItems != null || joinItems.Count() > 0) ? ((filterItems != null || filterItems.Count() > 0) ? filterItems.Union(joinItems) : joinItems) : filterItems;

                    // var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && (f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A || f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.ReceiverUserID == loggedinUserId && f.Status == true && f.GST_TRN_INVOICE_AUDIT_TRAIL.Where(w => w.AuditTrailStatus == 1 && w.InvoiceID == f.InvoiceID).ToList())).OrderByDescending(o => o.InvoiceDate).ToList();

                    // lvInvoices.DataSource = items.ToList();
                    // lvInvoices.DataBind();

                    //----------START HERE NEW CODE-----------
                    var mismatchItems = invoiceItems.GetConsolidatetdInvoices(Common.LoggedInUserID(), SelectMonth);
                    var itemsMismatch = from data in mismatchItems
                                        group data by new { data.INVOICESPECIALCONDITION, data.InvoiceID } into g
                                        select new
                                        {
                                            INVOICESPECIALCONDITION = g.Key.INVOICESPECIALCONDITION,
                                            InvoiceID = g.Key.InvoiceID,
                                            TotalInvoice = g.Count(),
                                            QTY = g.Sum(s => s.QTY),
                                            TAXABLEAMOUNT = g.Sum(s => s.TAXABLEAMOUNT)
                                        };
                    var invoiceSum = from item in itemsMismatch
                                     group item by new { item.INVOICESPECIALCONDITION } into g
                                     select new
                                     {
                                         INVSPLCONDITION = g.Key.INVOICESPECIALCONDITION,
                                         TotalInvoice = g.Count(),
                                         QTY = g.Sum(s => s.QTY),
                                         TAXABLEAMOUNT = g.Sum(s => s.TAXABLEAMOUNT)
                                     };
                    rptMisMatch.DataSource = invoiceSum.ToList();
                    rptMisMatch.DataBind();
                    var matchItem = mismatchItems.Select(s => s.InvoiceID).Distinct().ToList();

                    var matchedInvoices = allItems.Where(w => !matchItem.Contains(w.InvoiceID)).ToList();
                    lvMatchInvoice.DataSource = matchedInvoices; //items.Where(w => !matcheItem.Contains(w.InvoiceID) && w.GST_TRN_INVOICE.InvoiceStatus==(byte)EnumConstants.InvoiceStatus.Fresh && w.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A).ToList();// items // invoiceItems.MatchedInvoices(matcheItem);
                    lvMatchInvoice.DataBind();
                    //TODO:OPTIMISE
                    //   var invid = filterItems.Select(s => s.InvoiceID).ToList();
                    //  var existConsolidated = unitOfWork.ConsolidatedInvoiceRepository.Filter(f => f.InvoiceID == invid).FirstOrDefault();

                    var mismatchInvoicesID = mismatchItems.Select(s => s.InvoiceID).ToList();
                    var acceptedInvoice = matchedInvoices.Where(w => !mismatchInvoicesID.Contains(w.InvoiceID));

                    UpdatedMatchedInvoiceRecieverAction(acceptedInvoice);


                    //----------END

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        private void UpdatedMatchedInvoiceRecieverAction(IEnumerable<GST_TRN_INVOICE_AUDIT_TRAIL> auditTrail)
        {
            //var item = auditTrail.Where(w => w.ReceiverInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.Accept).ToList();
            try
            {
                foreach (GST_TRN_INVOICE_AUDIT_TRAIL trail in auditTrail)
                {
                    trail.ReceiverInvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Accept;
                    trail.AuditTrailID = trail.AuditTrailID;
                    trail.ReceiverInvoiceActionDate = DateTime.Now;
                    var isSave = unitOfWork.InvoiceAuditTrailRepositry.Update(trail);
                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        public bool IsActionInvoice(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var result = unitOfWork.InvoiceAuditTrailRepositry.Contains(c => c.InvoiceID == InvoicID && (c.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2 || c.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Modify));
            //TODO:Need  to change here 
            return !result;
        }

        public bool IsEditable(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var result = unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);
            //TODO:Need  to change here 
            return true;
        }
        // GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkvGSTR2A_Click(object sender, EventArgs e)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                LinkButton lkb = (LinkButton)sender;
                if (lkb.CommandName == "Import")
                {
                    int count = 0;
                    foreach (ListViewDataItem item in lvRegularInvoice.Items)
                    {
                        Int64 invoiceID = Convert.ToInt64(lvRegularInvoice.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                        CheckBox chk = (CheckBox)item.FindControl("chkImport");
                        if (chk.Checked)
                        {
                            var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId
                                && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.InvoiceID == invoiceID && f.GST_TRN_INVOICE.Status == true);
                            if (filedItem == null)
                            {
                                audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                                audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import2A);
                                audittrail.UserIP = Common.IP;
                                audittrail.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                                audittrail.CreatedDate = DateTime.Now;
                                audittrail.CreatedBy = loggedinUserId;
                                unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                                unitOfWork.Save();
                                count = count + 1;
                            }
                        }
                        else
                        {

                        }
                    }
                    if (count > 0)
                    {
                        this.Master.SuccessMessage = count.ToString() + " Invoice " + lkb.CommandName + "ed successfully.";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    }
                    else
                    {
                        this.Master.WarningMessage = "There are no invoices to import.";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    }
                }

                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        protected void lkbEditInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument))
                {
                    uc_InvoiceEdit.EditFrom = "GSTR2";
                    uc_InvoiceEdit.BindInvoice(Convert.ToInt32(lkbItem.CommandArgument.ToString()));
                    //bool status = uc_InvoiceEdit.DataSuccess;
                    //if (status)
                    //{

                    //}
                    uc_InvoiceEdit.Focus();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        protected void lkb_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkb = (LinkButton)sender;
                int count = 0;
                List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                List<string> mailsToList = new List<string>();
                List<string> invID = new List<string>();
                foreach (ListViewDataItem item in lvRegularInvoice.Items)
                {
                    string auditTrailID = lvRegularInvoice.DataKeys[item.DisplayIndex].Values["AuditTrailID"].ToString();
                    //string invoiceID = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString();
                    Int64 invoiceID = Convert.ToInt64(lvRegularInvoice.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();
                        var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                        Int64 id = Convert.ToInt64(auditTrailID);
                        // int invoiceid = Convert.ToInt32(audittrail.InvoiceID);
                        var itemAudit = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailID == id);
                        itemAudit.ReceiverInvoiceAction = (byte)(EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), lkb.CommandName);
                        itemAudit.UpdatedDate = DateTime.Now;
                        itemAudit.UpdatedBy = Common.LoggedInUserID(); ;
                        unitOfWork.InvoiceAuditTrailRepositry.Update(itemAudit);
                        unitOfWork.Save();
                        count = count + 1;
                        if (!mailsToList.Contains(invoiceDetail.AspNetUser.Email))
                        {
                            mailsToList.Add(invoiceDetail.AspNetUser.Email);
                        }
                        attribute.UserName = invoiceDetail.AspNetUser.OrganizationName;
                        //attribute.MailsTo.Add();
                        //attribute.InvoiceNo = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceNo"].ToString();
                        attribute.InvoiceDate = DateTimeAgo.GetFormatDate(invoiceDetail.InvoiceDate);
                        attribute.InvoiceTotalAmount = invoiceDetail.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                        invAttributes.Add(attribute);
                        //invID.Add(invoiceID);
                        cls_PurchaseRegister register = new cls_PurchaseRegister();
                        if (lkb.CommandName == "Accept")
                        {
                            register.UpdatePurchaseDataItemFromInvoice(invoiceDetail);
                        }

                    }
                }
                if (count > 0)
                {

                    string mailString = string.Empty;
                    string sellerMail = string.Empty;
                    clsMessageAttribute mailData = new clsMessageAttribute();
                    foreach (clsMessageAttribute iId in invAttributes)
                    {
                        //Int64 id = Convert.ToInt64(iId);
                        //mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                        mailString += "<tr><td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                        mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                        mailData.UserName = iId.UserName;
                    }

                    this.Master.SuccessMessage = count.ToString() + " Invoice " + lkb.CommandName + " successfully.";

                    SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));
                    if (ViewState["CommandName"].ToString() != null)
                    {
                        PopulateInvoices(ViewState["CommandName"].ToString(), Convert.ToByte(uc_invoiceMonth.GetValue));
                    }
                    invoiceItems.MisMatchInvoice(Common.LoggedInUserID()); //for refresh re-conciliation on accept or pending button
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);



                }
                else
                {
                    this.Master.WarningMessage = "There are no invoices.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //uc_sucess.SuccessMessage = "Data uploaded successfully.";
                    // BindAllInvoices();

                }

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                this.Master.ErrorMessage = ex.Message;
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                //uc_sucess.SuccessMessage = "Data uploaded successfully.";

                // BindAllInvoices();

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

                if (lkbAccept.CommandName == "Accept")
                {
                    string mailBody = message.GetMessage(EnumConstants.Message.Accept, replaceItem);
                    msg.Body = mailBody;   //"hi body.....";
                    msg.Destination = Common.UserProfile.Email;//sellerEmail;
                    msg.Subject = "GSTR - 2 Accepted.";
                }
                else if (lkbAccept.CommandName == "Reject")
                {
                    string mailBody = message.GetMessage(EnumConstants.Message.Reject, replaceItem);
                    msg.Body = mailBody;   //"hi body.....";
                    msg.Destination = Common.UserProfile.Email;//sellerEmail;
                    msg.Subject = "GSTR - 2 Rejected.";
                }
                else
                {
                    string mailBody = message.GetMessage(EnumConstants.Message.Pending, replaceItem);
                    msg.Body = mailBody;   //"hi body.....";
                    msg.Destination = Common.UserProfile.Email;//sellerEmail;
                    msg.Subject = "GSTR - 2 Pending.";
                }
                email.Send(msg);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lvItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpInvoice.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
            dpInvoice.DataBind();
        }

        protected void lkbImport_Click(object sender, EventArgs e)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                var loggedinUserId = Common.LoggedInUserID();
                int count = 0;

                LinkButton lkb = (LinkButton)sender;
                foreach (ListViewDataItem item in lvRegularInvoice.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        string invoiceID = lvRegularInvoice.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString();
                        audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                        audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR2);
                        audittrail.UserIP = Common.IP;
                        audittrail.CreatedDate = DateTime.Now;
                        audittrail.CreatedBy = loggedinUserId;
                        unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                        unitOfWork.Save();
                        count = count + 1;
                    }

                    if (count > 0)
                    {

                        this.Master.SuccessMessage = "Data Imported Successfully .";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                        BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
                        lvRegularInvoice.DataBind();
                    }
                    else
                    {
                        this.Master.WarningMessage = "There are no invoices.";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        //uc_sucess.SuccessMessage = "Data uploaded successfully.";

                        // BindAllInvoices();

                    }
                }
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

        public LinkButton sender { get; set; }

        protected void lbinfo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkb = (LinkButton)sender;
                // Application["InvoiceID"]=lkb.CommandName
                // this.ViewInvoiceID = lkb.CommandName;
                ViewState["CommandName"] = lkb.CommandName;
                PopulateInvoices(lkb.CommandName, Convert.ToByte(uc_invoiceMonth.GetValue));
                //var item = invoiceItems.GetConsolidatetdInvoices(Common.LoggedInUserID(), lkb.CommandName);
                //lvRegularInvoice.DataSource = item.ToList();
                //lvRegularInvoice.DataBind();
                //lvRegularInvoice.Visible = true;
                divMain.Visible = false;
                divViewInvoiceList.Visible = true;
                //Server.Transfer("GSTR2Invoices.aspx");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void PopulateInvoices(string specialCondition, byte SelectedMonth)
        {
            try
            {
                var item = invoiceItems.GetConsolidatetdInvoices(Common.LoggedInUserID(), specialCondition);
                item = item.FindAll(x => x.GST_TRN_INVOICE.InvoiceMonth == SelectedMonth);
                lvRegularInvoice.DataSource = item.OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();
                lvRegularInvoice.DataBind();
                lvRegularInvoice.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public string ViewInvoiceID { get; set; }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            try
            {
                lvRegularInvoice.DataSource = null;
                lvRegularInvoice.DataBind();
                divMain.Visible = true;
                divViewInvoiceList.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        protected void lkbAddInvoice_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/User/uinvoice/GSTinvoice.aspx");
        }

        public string CommandName
        {
            get
            {
                return ViewState["CommandName"].ToString();
            }
            set
            {
                ViewState["CommandName"] = value;
            }
        }

        protected void lvRegularInvoice_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //Dpmissinvoice.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            //lvRegularInvoice.DataBind();
            //Dpmissinvoice.DataBind();
            Dpmissinvoice.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
           
            PopulateInvoices(CommandName, Convert.ToByte(uc_invoiceMonth.GetValue));  //Ankita

            Dpmissinvoice.DataBind();
            
    }
    }
}