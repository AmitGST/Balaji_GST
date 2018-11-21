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
//
namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR2APreviewB2B : System.Web.UI.Page
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();
        #region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllList((DateTime.Now.Month-1));
                BindViewInvoice();
            }
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
        }
        #endregion
        int mName;
        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {

            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            BindAllList(mName);
        }
        private void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }

        public void BindAllList(int MonthName)
        {
            GetGSTR_2A_3(MonthName);
            GetGSTR_2A_4(MonthName);
            GetGSTR_2A_5(MonthName);
            GetGSTR_2A_6C(MonthName);
            GetGSTR_2A_6I(MonthName);
            GetGSTR_2A_7A(MonthName);
            GetGSTR_2A_7B(MonthName);
           
        }
        #region BindGrid
        private void GetGSTR_2A_3(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_3, _invoice.GetGSTR_2A_3(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        private void GetGSTR_2A_4(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_4, _invoice.GetGSTR_2A_4(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        private void GetGSTR_2A_5(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_5, _invoice.GetGSTR_2A_5(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        private void GetGSTR_2A_6C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_6_1, _invoice.GetGSTR_2A_6C(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetGSTR_2A_6I(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_6_2, _invoice.GetGSTR_2A_6I(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        private void GetGSTR_2A_7A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_7A, _invoice.GetGSTR_2A_7A(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetGSTR_2A_7B(int MonthName)
        {
            try
            {
                var loggedUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR2A_7B, _invoice.GetGSTR_2A_7B(loggedUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        #endregion
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkvGSTR2A_Click(object sender, EventArgs e)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                    DateTime lastDate = DateTime.Now.LastDayOfMonth();
                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.AuditTrailStatus == 2 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                    var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.AuditTrailStatus == 1 && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();
                    foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in invoices)
                    {
                        audittrail.InvoiceID = inv.InvoiceID;
                        audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import2A);
                        audittrail.UserIP = Common.IP;
                        audittrail.CreatedDate = DateTime.Now;
                        audittrail.CreatedBy = loggedinUserId;
                        unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                        unitOfWork.Save();
                    }
                    this.Master.SuccessMessage = "Data Imported Successfully .";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    BindAllList(mName);
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
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
                    var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.ReceiverUserID == loggedinUserId && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.ReceiverUserID == loggedinUserId && f.Status == true && f.GST_TRN_INVOICE_AUDIT_TRAIL.Where(w => w.AuditTrailStatus == 1 && w.InvoiceID == f.InvoiceID).ToList())).OrderByDescending(o => o.InvoiceDate).ToList();

                    // lvViewInvoice.DataSource = filedItem;
                    //lvViewInvoice.DataBind();

                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

    }
}