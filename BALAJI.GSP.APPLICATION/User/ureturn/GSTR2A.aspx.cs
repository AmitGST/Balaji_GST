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


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR2A : System.Web.UI.Page
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindViewInvoice(Convert.ToByte(DateTime.Now.Month - 1));
            }
            uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
        }
        private void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
           BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
            //var loggedinUser = Common.LoggedInUserID();
            //var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
            //DateTime firstdate = DateTime.Now.FirstDayOfMonth();
            //DateTime lastDate = DateTime.Now.LastDayOfMonth();
            //var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUser && f.CreatedDate.Value.Month == SelectedMonth
            //   && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
            //   && (f.GST_TRN_INVOICE.CreatedDate >= firstdate && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(o => o.CreatedDate).ToList();

            //var existItems = invoices.Select(s => s.InvoiceID);
            //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUser && f.CreatedDate.Value.Month == SelectedMonth
            //        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1 && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
            //        && !existItems.Contains(f.InvoiceID)
            //        && f.GST_TRN_INVOICE.Status == true && (f.GST_TRN_INVOICE.CreatedDate >= firstdate
            //        && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(f => f.GST_TRN_INVOICE.CreatedDate).ToList();

            //var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;

            //lvInvoices.DataSource = items.OrderByDescending(o => o.CreatedDate).ToList();
           // lvInvoices.DataBind();
        }
        private void BindViewInvoice(byte SelectedMonth)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    unitOfWork = new UnitOfWork();
                    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                    DateTime lastDate = DateTime.Now.LastDayOfMonth();
                    //  var invoice = unitOfWork.InvoiceRepository.Filter(f => f.CreatedBy == loggedinUserId && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.CreatedDate).ToList();

                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.CreatedDate.Value.Month == SelectedMonth
                      && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
                        //&& !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true
                      && (f.GST_TRN_INVOICE.CreatedDate >= firstdate && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();

                    var existItems = invoices.Select(s => s.InvoiceID);

                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId

                        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1 && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh
                        && f.CreatedDate.Value.Month == SelectedMonth
                        && !existItems.Contains(f.InvoiceID)
                        && f.GST_TRN_INVOICE.Status == true && (f.GST_TRN_INVOICE.CreatedDate >= firstdate
                        && f.GST_TRN_INVOICE.CreatedDate <= lastDate)).OrderByDescending(f => f.GST_TRN_INVOICE.CreatedDate).ToList();

                    var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;

                    lvInvoices.DataSource = items.OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();
                    lvInvoices.DataBind();

                }
            }
            catch(Exception ex)
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

        // GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkvGSTR2A_Click(object sender, EventArgs e)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                LinkButton lkb = (LinkButton)sender;
                if (lkb.CommandName == "Import")
                {
                    List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                    List<string> mailsToList = new List<string>();
                    int count = 0;
                    foreach (ListViewDataItem item in lvInvoices.Items)
                    {
                        Int64 invoiceID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                        CheckBox chk = (CheckBox)item.FindControl("chkImport");
                        if (chk.Checked)
                        {
                            //amits start
                            clsMessageAttribute attribute = new clsMessageAttribute();
                            var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                            string invoiceNo = invoiceDetail.InvoiceNo;
                            //End


                            var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId
                                && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.InvoiceID == invoiceID && f.GST_TRN_INVOICE.Status == true);
                            if (filedItem == null)
                            {
                                audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                                audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import2A);
                                audittrail.UserIP = Common.IP;
                                audittrail.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                                audittrail.ReceiverInvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                                audittrail.SellerInvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                                audittrail.CreatedDate = DateTime.Now;
                                audittrail.CreatedBy = loggedinUserId;
                                unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                                unitOfWork.Save();
                                //count = count + 1;
                                //amits start
                                count = count + 1;
                                if (!mailsToList.Contains(invoiceDetail.AspNetUser.Email))
                                {
                                    mailsToList.Add(invoiceDetail.AspNetUser.Email);
                                }
                                attribute.UserName = invoiceDetail.AspNetUser.OrganizationName;

                                //attribute.MailsTo.Add();
                                attribute.InvoiceNo = invoiceNo;
                                attribute.InvoiceDate = DateTimeAgo.GetFormatDate(invoiceDetail.InvoiceDate);
                                attribute.InvoiceTotalAmount = invoiceDetail.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                                invAttributes.Add(attribute);
                                //End

                            }
                            var invoice = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceID == invoiceID).FirstOrDefault();
                            cls_PurchaseRegister insertPurchaseRegsiter = new cls_PurchaseRegister();
                            insertPurchaseRegsiter.LoggedinUserID = Common.LoggedInUserID();
                            bool isSave = insertPurchaseRegsiter.SaveInvoiveDataInPurchaseRegister(invoice);
                            cls_ITC itc = new cls_ITC();
                            itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.Purchase;
                            itc.SaveItcReceiver(invoice);
                        }
                        else
                        {

                        }
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
                        this.Master.SuccessMessage = "Data imported successfully.";
                        SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                        BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
                    }
                    //if (count > 0)
                    //{
                    //    this.Master.SuccessMessage = count.ToString() + " Invoice " + lkb.CommandName + "ed successfully.";
                    //    //uc_sucess.Visible = true;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //}
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

        public bool IsEditable(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var result = unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);
            //TODO:Need  to change here 
            return true;
        }
        protected void lkb_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lkb = (LinkButton)sender;
                int count = 0;

                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    string auditTrailID = lvInvoices.DataKeys[item.DisplayIndex].Values["AuditTrailID"].ToString();
                    string invoiceID = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString();
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        // getinvocdtls.
                        Int64 id = Convert.ToInt64(auditTrailID);
                        // int invoiceid = Convert.ToInt32(audittrail.InvoiceID);
                        var itemAudit = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailID == id);
                        itemAudit.InvoiceAction = (byte)(EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), lkb.CommandName);
                        itemAudit.UpdatedDate = DateTime.Now;
                        itemAudit.UpdatedBy = Common.LoggedInUserID(); ;
                        unitOfWork.InvoiceAuditTrailRepositry.Update(itemAudit);
                        unitOfWork.Save();
                        count = count + 1;
                    }


                }
                if (count > 0)
                {


                    this.Master.SuccessMessage = count.ToString() + " Invoice " + lkb.CommandName + "ed successfully.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
                }
                else
                {
                    this.Master.WarningMessage = "Their is no invoices.";
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
                string mailBody = message.GetMessage(EnumConstants.Message.ImportGSTR2A, replaceItem);
                msg.Body = mailBody;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "GSTR - 2A Imported.";
                email.Send(msg);
            }
            catch(Exception ex){ 
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}