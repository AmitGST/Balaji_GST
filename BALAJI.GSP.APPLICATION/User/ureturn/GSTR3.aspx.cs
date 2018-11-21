using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR3 : System.Web.UI.Page
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindViewInvoice();
            }

        }
        private void BindViewInvoice()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                    DateTime lastDate = DateTime.Now.LastDayOfMonth();

                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId
                    && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A
                        //&& !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true
                    && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    ///var existItems = invoices.Select(s => s.InvoiceID);

                    //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId
                    //    && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR2
                    //    && !existItems.Contains(f.InvoiceID)
                    //    && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate
                    //    && f.CreatedDate <= lastDate)).ToList();

                    // var items = (filedItem != null || filedItem.Count() > 0) ? invoices.Union(filedItem) : invoices;// invoices.Union(filedItem ?? Enumerable.Empty<TResult>());



                    // var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && (f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A || f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.ReceiverUserID == loggedinUserId && f.Status == true && f.GST_TRN_INVOICE_AUDIT_TRAIL.Where(w => w.AuditTrailStatus == 1 && w.InvoiceID == f.InvoiceID).ToList())).OrderByDescending(o => o.InvoiceDate).ToList();

                    lvInvoices.DataSource = invoices.ToList();
                    lvInvoices.DataBind();

                }
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
            var result=unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);
            //TODO:Need  to change here 
            return true;
        }
        protected void lkb_Click(object sender, EventArgs e)
        {
            try
            {
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
                        itemAudit.UpdatedDate = DateTime.Now;
                        itemAudit.UpdatedBy = Common.LoggedInUserID(); ;
                        unitOfWork.InvoiceAuditTrailRepositry.Update(itemAudit);
                        unitOfWork.Save();
                    }
                }
                this.Master.SuccessMessage = "Data " + lkb.CommandName + " successfully";
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                //uc_sucess.SuccessMessage = "Data uploaded successfully.";

                BindViewInvoice();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                this.Master.SuccessMessage = ex.Message;
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                //uc_sucess.SuccessMessage = "Data uploaded successfully.";

                // BindAllInvoices();
               
            }
        }

        protected void lkbImport_Click(object sender, EventArgs e)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                var loggedinUserId = Common.LoggedInUserID();

                //if (loggedinUserId != null)
                //{
                //    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                //    DateTime lastDate = DateTime.Now.LastDayOfMonth();
                //    byte FileGSTR2 = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR2);
                //    byte Import1A = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import1A);

                //    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == FileGSTR2 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                //    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == Import1A && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                LinkButton lkb = (LinkButton)sender;
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    string invoiceID = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString();
                    audittrail.InvoiceID = Convert.ToInt64(invoiceID);// item.InvoiceID;
                    audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.ImportGSTR3);
                    audittrail.UserIP = Common.IP;
                    audittrail.CreatedDate = DateTime.Now;
                    audittrail.CreatedBy = loggedinUserId;
                    unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                    unitOfWork.Save();
                }
                BindViewInvoice();
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            }
    }
}