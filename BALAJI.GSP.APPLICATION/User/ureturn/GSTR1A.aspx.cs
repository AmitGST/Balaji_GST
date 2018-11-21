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
using DataAccessLayer;
using BusinessLogic.Repositories;
using GST.Utility;
using Microsoft.AspNet.Identity;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR1A : System.Web.UI.Page
    {

        cls_Invoice clsInvoice = new cls_Invoice();

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
            //DateTime firstdate = DateTime.Now.FirstDayOfMonth();
            //DateTime lastDate = DateTime.Now.LastDayOfMonth();
            //var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
            //var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUser && f.CreatedDate.Value.Month==SelectedMonth
            //    && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import1A
            //&& (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.CreatedDate).ToList();

            //var existItems = invoices.Select(s => s.InvoiceID).ToList();

            //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUser && f.CreatedDate.Value.Month == SelectedMonth
            //        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2
            //        && !existItems.Contains(f.InvoiceID)
            //        && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
            //        && f.CreatedDate <= lastDate)).ToList();
            //litItemToImport.Text = filedItem.Count().ToString();

            //if (filedItem.Count() > 0)
            //{
            //    lkcImportConsolidated.Enabled = true;
            //    lkcImportConsolidated.CssClass = "btn btn-primary";
            //}
            //else
            //{
            //    lkcImportConsolidated.Enabled = false;
            //    lkcImportConsolidated.CssClass = "btn btn-primary disabled";
            //    //CssClass="btn btn-primary"
            //}
            //var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;

            //lvAccept.DataSource = invoices.Where(w => w.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Accept).OrderByDescending(o => o.CreatedDate).ToList();// items.ToList();
            //lvAccept.DataBind();
            //lvInvoices.DataSource = invoices.Where(w => w.ReceiverInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.Accept).OrderByDescending(o => o.CreatedDate).ToList();// items.ToList();
            //lvInvoices.DataBind();
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

                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.CreatedDate.Value.Month == SelectedMonth
                    && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import1A
                        //&& !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true
                    && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();

                    var existItems = invoices.Select(s => s.InvoiceID).ToList();

                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId
                        && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2
                        && !existItems.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.InvoiceMonth == SelectedMonth
                        && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
                        && f.CreatedDate <= lastDate  )).ToList();

                    litItemToImport.Text = filedItem.Count().ToString();

                    if (filedItem.Count() > 0)
                    {
                        lkcImportConsolidated.Enabled = true;
                        lkcImportConsolidated.CssClass = "btn btn-primary";
                    }
                    else
                    {
                        lkcImportConsolidated.Enabled = false;
                        lkcImportConsolidated.CssClass = "btn btn-primary disabled";
                        //CssClass="btn btn-primary"
                    }


                    var items = (filedItem != null || filedItem.Count() > 0) ? ((invoices != null || invoices.Count() > 0) ? invoices.Union(filedItem) : filedItem) : invoices;


                    //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId 
                    //    && (f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A || f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1) 
                    //    && f.GST_TRN_INVOICE.Status == true 
                    //    && (f.CreatedDate >= firstdate 
                    //    && f.CreatedDate <= lastDate)).ToList();

                    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.ReceiverUserID == loggedinUserId && f.Status == true && f.GST_TRN_INVOICE_AUDIT_TRAIL.Where(w => w.AuditTrailStatus == 1 && w.InvoiceID == f.InvoiceID).ToList())).OrderByDescending(o => o.InvoiceDate).ToList();


                    lvAccept.DataSource = invoices.Where(w => w.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Accept).OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();// items.ToList();
                    lvAccept.DataBind();
                    lvInvoices.DataSource = invoices.Where(w => w.ReceiverInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.Accept).OrderByDescending(o => o.GST_TRN_INVOICE.InvoiceDate).ToList();// items.ToList();
                    lvInvoices.DataBind();
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

        public bool IsActionInvoice(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var result = unitOfWork.InvoiceAuditTrailRepositry.Contains(c => c.InvoiceID == InvoicID && c.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import1A && (c.InvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Accept || c.InvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Reject));
            //TODO:Need  to change here 
            return !result;
        }

        protected void lkb_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                LinkButton lkb = (LinkButton)sender;
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    string auditTrailID = lvInvoices.DataKeys[item.DisplayIndex].Values["AuditTrailID"].ToString();
                    string invoiceID = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString();
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {

                        Int64 id = Convert.ToInt64(auditTrailID);
                        // int invoiceid = Convert.ToInt32(audittrail.InvoiceID);
                        var itemAudit = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailID == id);
                        itemAudit.InvoiceAction = (byte)(EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), lkb.CommandName);
                        itemAudit.SellerInvoiceAction = (byte)(EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), lkb.CommandName);
                        itemAudit.SellerInvoiceActionDate = DateTime.Now;
                        itemAudit.UpdatedDate = DateTime.Now;
                        audittrail.InvoiceAction = (byte)(EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), lkb.CommandName); ;
                        // itemAudit.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Accept;
                        itemAudit.UpdatedBy = Common.LoggedInUserID();
                        unitOfWork.InvoiceAuditTrailRepositry.Update(itemAudit);
                        unitOfWork.Save();

                        Int64 invoiceid = Convert.ToInt64(invoiceID);
                        var invoice = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceID == invoiceid).FirstOrDefault();

                        if (itemAudit.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Modify)
                        {
                            if (lkb.CommandName == "Accept")
                            {
                              
                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSave = insertSaleRegsiter.SaleRegister(invoice);

                                cls_ITC itc = new cls_ITC();
                                itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.TaxInvoice;
                                itc.SaveItc(invoice);
                               
                            }
                            else
                            {
                                //cls_CreditDebit_Note noteItem = new cls_CreditDebit_Note();
                                ////  EnumConstants.NoteType invAction = (EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), ddlNoteType.SelectedValue.ToString());

                                //// noteItem.NoteType = (byte)invAction;
                                ////  noteItem.Description = txtDescription.Text.Trim();
                                //bool isSave = noteItem.SaveNote(invoice);

                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSaveSaleReg = insertSaleRegsiter.SaleRegister(invoice);
                            }
                           
                        }
                        else if (itemAudit.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Pending)
                        {
                            if (lkb.CommandName == "Accept")
                            {
                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSave = insertSaleRegsiter.SaleRegister(invoice);

                                cls_PurchaseRegister insertPurchaseRegsiter = new cls_PurchaseRegister();
                                insertPurchaseRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSavePurchaseReg = insertPurchaseRegsiter.SaveInvoiveDataInPurchaseRegister(invoice);

                                cls_ITC itc = new cls_ITC();
                                itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.TaxInvoice;
                                itc.SaveItc(invoice);
                            }
                            else
                            {
                                // cls_CreditDebit_Note noteItem = new cls_CreditDebit_Note();
                                ////  EnumConstants.NoteType invAction = (EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), ddlNoteType.SelectedValue.ToString());

                                //// noteItem.NoteType = (byte)invAction;
                                ////  noteItem.Description = txtDescription.Text.Trim();
                                //bool isSave = noteItem.SaveNote(invoice);

                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSaveSaleReg = insertSaleRegsiter.SaleRegister(invoice);
                            }
                        }
                        else if (itemAudit.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Add)
                        {
                           
                            if (lkb.CommandName == "Accept")
                            {
                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSave = insertSaleRegsiter.SaleRegister(invoice);

                                cls_PurchaseRegister insertPurchaseRegsiter = new cls_PurchaseRegister();
                                insertPurchaseRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSavePurchaseReg = insertPurchaseRegsiter.SaveInvoiveDataInPurchaseRegister(invoice);

                                cls_ITC itc = new cls_ITC();
                                itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.TaxInvoice;
                                itc.SaveItc(invoice);
                               
                            }
                            else
                            {
                                // cls_CreditDebit_Note noteItem = new cls_CreditDebit_Note();
                                ////  EnumConstants.NoteType invAction = (EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), ddlNoteType.SelectedValue.ToString());

                                //// noteItem.NoteType = (byte)invAction;
                                ////  noteItem.Description = txtDescription.Text.Trim();
                                //bool isSave = noteItem.SaveNote(invoice);

                                cls_PurchaseRegister insertSaleRegsiter = new cls_PurchaseRegister();
                                insertSaleRegsiter.LoggedinUserID = Common.LoggedInUserID();
                                bool isSaveSaleReg = insertSaleRegsiter.SaleRegister(invoice);
                            }
                        }
                        count = count + 1;
                    }
                    
                }
                if (count > 0)
                {
                    this.Master.SuccessMessage = count.ToString() + " Invoice " + lkb.CommandName + "ed successfully.";
                   // this.Master.SuccessMessage = "Data accepted successfully";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //uc_sucess.SuccessMessage = "Data uploaded successfully.";
                }
                else
                {
                    this.Master.WarningMessage = "There is no data to " + lkb.CommandName;
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
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
            BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
        }



        protected void lkbImport_Click(object sender, EventArgs e)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                var loggedinUserId = Common.LoggedInUserID();
                int count = 0;
                //if (loggedinUserId != null)
                //{
                //    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                //    DateTime lastDate = DateTime.Now.LastDayOfMonth();
                //    byte FileGSTR2 = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR2);
                //    byte Import1A = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import1A);

                //    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == FileGSTR2 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                //    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == Import1A && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();
                List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                List<string> mailsToList = new List<string>();
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    Int64 invoiceID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                    CheckBox chk = (CheckBox)item.FindControl("chkImport");
                    if (chk.Checked)
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();
                        var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                        string invoiceNo = invoiceDetail.InvoiceNo;
                        Int64 invoiceAction = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceAction"].ToString());
                        Int64 auditTrailID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["AuditTrailID"].ToString());
                        var getfile2Item = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailID == auditTrailID);
                        audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                        audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import1A);
                        audittrail.UserIP = Common.IP;
                        audittrail.CreatedDate = DateTime.Now;
                        audittrail.InvoiceAction = getfile2Item.InvoiceAction;// Convert.ToByte(invoiceAction);
                        audittrail.CreatedBy = loggedinUserId;
                        unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                        unitOfWork.Save();
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
                else
                {
                    this.Master.WarningMessage = "There are no invoices.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
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
                string mailBody = message.GetMessage(EnumConstants.Message.ImportGSTR1A, replaceItem);
                msg.Body = mailBody;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "GSTR - 1A Imported.";
                email.Send(msg);
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lvInvoices_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpInvoice.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
                dpInvoice.DataBind();
            }
            catch(Exception ex)
            {
            cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());}
        }

        protected void lkcImportConsolidated_Click(object sender, EventArgs e)
        {
            try
            {
                clsMessageAttribute clsMessage = new clsMessageAttribute();
                clsMessage = clsInvoice.ImportAllInvoices(Common.LoggedInUserID());
                SendHTMLMail(clsMessage, clsMessage.MailString, String.Join(";", ""));//clsMessage.MailsTo.ToArray()
                BindViewInvoice(Convert.ToByte(uc_invoiceMonth.GetValue));
                this.Master.SuccessMessage = clsMessage.CustomMessage;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lvAccept_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

    }
}