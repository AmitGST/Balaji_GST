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
    public partial class GSTR2Invoices : System.Web.UI.Page
    {
        cls_Invoice invoiceItems = new cls_Invoice();
        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(PreviousPage.ViewInvoiceID))
            //{
            //    Response.Redirect(Request.UrlReferrer.ToString());
            //}
            if (!IsPostBack)
            {
               
                if (string.IsNullOrEmpty(PreviousPage.ViewInvoiceID) || PreviousPage.ViewInvoiceID == null)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    this.ViewInvoiceID = PreviousPage.ViewInvoiceID;
                    BindInvoices();
                }
            }
        }

        public string ViewInvoiceID
        {
            get { return (string)ViewState["ViewInvoiceID"]; }
            set { ViewState["ViewInvoiceID"] = value; }
        }
        private void BindInvoices()
        {
            try
            {
                if (!string.IsNullOrEmpty(ViewInvoiceID))
                {
                    var item = invoiceItems.GetConsolidatetdInvoices(Common.LoggedInUserID(), ViewInvoiceID);
                    lvRegularInvoice.DataSource = item.ToList();
                    lvRegularInvoice.DataBind();
                    lvRegularInvoice.Visible = true;
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public bool IsEditable(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var result = unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);
            //TODO:Need  to change here 
            return true;
        }
        protected void lkbEditInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument))
                {
                    uc_InvoiceEdit.BindInvoice(Convert.ToInt32(lkbItem.CommandArgument.ToString()));
                    uc_InvoiceEdit.Focus();
                }
            }
            catch(Exception ex){
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
                this.Master.ErrorMessage = ex.Message;
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                //uc_sucess.SuccessMessage = "Data uploaded successfully.";
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
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
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
      
    }
}