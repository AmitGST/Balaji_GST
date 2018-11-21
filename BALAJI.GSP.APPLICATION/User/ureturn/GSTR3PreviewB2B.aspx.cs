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

namespace BALAJI.GSP.APPLICATION.User.ureturn
{ 
    public partial class GSTR3PreviewB2B : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();

        //#region VARIABLE_DECLARETION
        //string PID = string.Empty;
        //string SellerGSTIN = string.Empty;
        //ExcelDB excelDB = new ExcelDB();
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        //string strReceiverGSTN = string.Empty;
        //string period = string.Empty;
        //string month = string.Empty;
        //string year = string.Empty;
        //#endregion

        #region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            // if (Request.QueryString["XD"] != null)
            // {
            //     PID = Request.QueryString["XD"];
            // }

            // SellerGSTIN = Page.User.Identity.Name;
            // //No need to use--Ashish

            //// take the previous month 1st n last date 
            // txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            // DateTime now = DateTime.Now;
            // var stratdt = new DateTime(now.Year, now.Month, 1);
            // txtFromDate.Text = stratdt.ToString("dd/MM/yyyy");

            //     SellerGSTIN = "33GSPTN0231G1ZM";

            if (!IsPostBack)
            {
                //PopulateSellerDtls(SellerGSTIN);
                //BindGrid();  
                BindAllGSTR3((DateTime.Now.Month - 1));
                
            }
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
        }
        #endregion
        int mName;
        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {

            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            BindAllGSTR3(mName);
        }
        private void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }
        public void BindAllGSTR3(int MonthName)
        {
            GetGSTR3_4_1_A(MonthName);
            GetGSTR3_4_1_B(MonthName);
            GetGSTR3_4_1_C(MonthName);
            GetGSTR3_4_1_D(MonthName);
            GetGSTR3_4_2_A(MonthName);
            GetGSTR3_4_2_B(MonthName);
            GetGSTR3_4_2_C(MonthName);
            GetGSTR3_5A_1(MonthName);
            GetGSTR3_5A_2(MonthName);
            GetGSTR3_5B_1(MonthName);
            GetGSTR3_5B_2(MonthName);
            GetGSTR3_6_1(MonthName);
            GetGSTR3_6_2(MonthName);
            GetGSTR3_7(MonthName);
            GetGSTR3_8A(MonthName);
            GetGSTR3_8B(MonthName);
            GetGSTR3_8C(MonthName);
            GetGSTR3_8D(MonthName);
            GetGSTR3_9A(MonthName);
            GetGSTR3_9B(MonthName);
            GetGSTR3_10A(MonthName);
            GetGSTR3_10B(MonthName);
            GetGSTR3_10C(MonthName);
            GetGSTR3_10D(MonthName);
            GetGSTR3_11(MonthName);
            GetGSTR3_12A(MonthName);
            GetGSTR3_12B(MonthName);
            GetGSTR3_12C(MonthName);
            GetGSTR3_12D(MonthName);
            GetGSTR3_13_1(MonthName);
            GetGSTR3_13_2(MonthName);
            GetGSTR3_14A(MonthName);
            GetGSTR3_14B(MonthName);
            GetGSTR3_14C(MonthName);
            GetGSTR3_14D(MonthName);
            GetGSTR3_15A(MonthName);
            GetGSTR3_15B(MonthName);
            GetGSTR3_15C(MonthName);
            GetGSTR3_15D(MonthName);
          
        }

        private void GetGSTR3_4_1_A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_4_1_A, _invoice.GetGSTR3_4_1_A(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_1_B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_4_1_B, _invoice.GetGSTR3_4_1_B(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_1_C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_4_1_C, _invoice.GetGSTR3_4_1_B(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_1_D(int MonthName)
        {
            try { 
            var loggedinUserId = Common.LoggedInUserID();
            BindListView(lv_GSTR3_4_1_D, _invoice.GetGSTR3_4_1_D(loggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_2_A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_4_2_A, _invoice.GSTR3_4_2_A(loggedinUserId, MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_2_B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_4_2_B, _invoice.GetGSTR3_4_2_B(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_4_2_C(int MonthName)
        {
            try
            {
            var loggedinUserId = Common.LoggedInUserID();
            BindListView(lv_GSTR3_4_2_C, _invoice.GetGSTR3_4_2_C(loggedinUserId, MonthName));
        }
             catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_5A_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_5_A_1, _invoice.GetGSTR3_5A_1(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_5A_2(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_5_A_2, _invoice.GetGSTR3_5A_2(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_5B_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_5_B_1, _invoice.GetGSTR3_5B_1(loggedinUserId, MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_5B_2(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_5_B_2,_invoice.GetGSTR3_5B_2(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_6_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView( lv_GSTR3_6_1,_invoice.GetGSTR3_6_1(loggedinUserId,MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_6_2(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_6_2,_invoice.GetGSTR3_6_2(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_7(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView( lv_GSTR3_7,_invoice.GetGSTR3_7(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_8A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView( lv_GSTR3_8A,_invoice.GetGSTR3_8A(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_8B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_8_B,_invoice.GetGSTR3_8B(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_8C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView( lv_GSTR3_8_C,_invoice.GetGSTR3_8C(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_8D(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_8_D, _invoice.GetGSTR3_8D(loggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_9A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_9_A,_invoice.GetGSTR3_9A(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_9B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
           }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_10A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_10A,_invoice.GetGSTR3_10A(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_10B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_10B,_invoice.GetGSTR3_10B(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_10C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_10C,_invoice.GetGSTR3_10C(loggedinUserId,MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_10D(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_10D,_invoice.GetGSTR3_10D(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_11(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_11,_invoice.GetGSTR3_11(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_12A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_12A,_invoice.GetGSTR3_12A(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_12B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_12B,_invoice.GetGSTR3_12B(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_12C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_12C,_invoice.GetGSTR3_12C(loggedinUserId,MonthName));
            }
            catch(Exception ex){
            cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());}
        }
        private void GetGSTR3_12D(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_12D,_invoice.GetGSTR3_12D(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_13_1(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_13_1,_invoice.GetGSTR3_13_1(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_13_2(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_13_2,_invoice.GetGSTR3_13_2(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_14A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_14A,_invoice.GetGSTR3_14A(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_14B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_14B,_invoice.GetGSTR3_14B(loggedinUserId,MonthName));
            }
            catch(Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_14C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_14C,_invoice.GetGSTR3_14C(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_14D(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_14D,_invoice.GetGSTR3_14D(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_15A(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                 BindListView(lv_GSTR3_15A,_invoice.GetGSTR3_15A(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_15B(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_15B,_invoice.GetGSTR3_15B(loggedinUserId,MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_15C(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_15C,_invoice.GetGSTR3_15C(loggedinUserId,MonthName));
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR3_15D(int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR3_15D,_invoice.GetGSTR3_15D(loggedinUserId,MonthName));
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //private void Bindlist_GSTR3_8A()
        //{
        //    try
        //    {
        //        var loggedinUserId = Common.LoggedInUserID();
        //        if (loggedinUserId != null)
        //        {
        //            string SellerUserID = Common.LoggedInUserID();
        //            var invoice = unitOfWork.GetGSTR3_8A(SellerUserID);
        //            lv_GSTR3_8A.DataSource = invoice.ToList();
        //            lv_GSTR3_8A.DataBind();

        //        }
        //    }
        //    catch(Exception ex){
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        //private void Bindlist_GSTR3_8B()
        //{
        //    try
        //    {
        //        var loggedinUserId = Common.LoggedInUserID();
        //        if (loggedinUserId != null)
        //        {
        //            string SellerUserID = Common.LoggedInUserID();
        //            var invoice = unitOfWork.GetGSTR3_8B(SellerUserID);
        //            lv_GSTR3_8_B.DataSource = invoice.ToList();
        //            lv_GSTR3_8_B.DataBind();

        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        //private void Bindlist_GSTR3_8C()
        //{
        //    try
        //    {
        //        var loggedinUserId = Common.LoggedInUserID();
        //        if (loggedinUserId != null)
        //        {
        //            string SellerUserID = Common.LoggedInUserID();
        //            var invoice = unitOfWork.GetGSTR3_8C(SellerUserID);
        //            lv_GSTR3_8_C.DataSource = invoice.ToList();
        //            lv_GSTR3_8_C.DataBind();

        //        }
        //    }
        //    catch(Exception ex){
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        //private void Bindlist_GSTR3_8D()
        //{
        //    try
        //    {
        //        var loggedinUserId = Common.LoggedInUserID();
        //        if (loggedinUserId != null)
        //        {
        //            string SellerUserID = Common.LoggedInUserID();
        //            var invoice = unitOfWork.GetGSTR3_8D(SellerUserID);
        //            lv_GSTR3_8_D.DataSource = invoice.ToList();
        //            lv_GSTR3_8_D.DataBind();

        //        }
        //    }
        //    catch (Exception ex) {
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkvGSTR3_Click(object sender, EventArgs e)
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                DateTime lastDate = DateTime.Now.LastDayOfMonth();

                byte FileGSTR3 = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR3);
                byte FileGSTR1A = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A);

                var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == FileGSTR3 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == FileGSTR1A && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();
                foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in invoices)
                {
                    audittrail.InvoiceID = inv.InvoiceID;
                    audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR3);
                    audittrail.UserIP = Common.IP;
                    audittrail.CreatedDate = DateTime.Now;
                    audittrail.CreatedBy = loggedinUserId;
                    unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                    unitOfWork.Save();
                }
                BindAllGSTR3(mName);
            }

            //#region PopulateSellerDtls
            //public void PopulateSellerDtls(string SellerGSTIN)
            //{
            //    ds = excelDB.PopulateGSTR1Dtls(SellerGSTIN);

            //    if (ds.Tables.Count > 0)
            //    {               
            //        if (ds.Tables[1].Rows.Count > 0)
            //        {
            //            lblGSTIN.Text =  Convert.ToString(ds.Tables[1].Rows[0][0]).Trim();
            //            lblGSTINVal.Text = Convert.ToString(ds.Tables[1].Rows[0][0]).Trim();
            //            lbltaxpayerName.Text =  Convert.ToString(ds.Tables[1].Rows[0][1]).Trim();               
            //             period = Convert.ToString(ds.Tables[1].Rows[0][3]).Trim();
            //             month = period.Substring(0, 2);             
            //             year = period.Substring(period.Length - 4, 4);

            //            lblPeriod.Text = "Month: " + month + "      " +  "Year: " + year;
            //        }
            //    }
            //}
            //#endregion


            ////PATCH
            //#region BindGrid
            //protected void BindGrid()
            //{
            //    DataSet ds = new DataSet();
            //    ds = ViewGSTR3(lblGSTINVal.Text.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());

            //    /* start--OUTWARE SUPPLY  */
            //    GVInterStateRegisteredTaxpayer.DataSource = ds.Tables[0];
            //    GVInterStateRegisteredTaxpayer.DataBind();

            //    GVIntraStateRegisteredTaxpayer.DataSource = ds.Tables[1];
            //    GVIntraStateRegisteredTaxpayer.DataBind();

            //    GVExport.DataSource = ds.Tables[2];
            //    GVExport.DataBind();

            //    GVRevision.DataSource = ds.Tables[3];
            //    GVRevision.DataBind();

            //    GVTotalTaxLiability.DataSource = ds.Tables[4];
            //    GVTotalTaxLiability.DataBind();
            //    /* end--OUTWARE SUPPLY */

            //    /* start-- INWARED SUPPLY */
            //    GVInwardInterState.DataSource = ds.Tables[5];
            //    GVInwardInterState.DataBind();

            //    GVInwardIntraState.DataSource = ds.Tables[6];
            //    GVInwardIntraState.DataBind();

            //    GVImport.DataSource = ds.Tables[7];
            //    GVImport.DataBind();

            //    GVPurchaseInvoice.DataSource = ds.Tables[8];
            //    GVPurchaseInvoice.DataBind();

            //    GVReverseCharge.DataSource = ds.Tables[9];
            //    GVReverseCharge.DataBind();

            //    //GVTaxLiabilityForMonth.DataSource = ds.Tables[10];
            //    //GVTaxLiabilityForMonth.DataBind();

            //    /* end-- INWARED SUPPLY */

            //    #region DUMMY_TABLE
            //     /* this table is required to show the data whose data are not saved into DB  */
            //    DataTable dtDummyTable = new DataTable("DummyTable");
            //    dtDummyTable.Columns.Add("ConsigneeStateCode");
            //    dtDummyTable.Columns.Add("IGSTRate");
            //    dtDummyTable.Columns.Add("IGSTAmt");
            //    dtDummyTable.Columns.Add("TaxableValue");
            //    dtDummyTable.Columns.Add("CGSTRate");
            //    dtDummyTable.Columns.Add("CGSTAmt");
            //    dtDummyTable.Columns.Add("SGSTRate");
            //    dtDummyTable.Columns.Add("SGSTAmt");
            //    dtDummyTable.Columns.Add("InvoiceNo");
            //    dtDummyTable.Columns.Add("Invoicedate");
            //    dtDummyTable.Columns.Add("DifferentialValue");
            //    dtDummyTable.Columns.Add("AdditionalTax");


            //    var row = dtDummyTable.NewRow();
            //    row["ConsigneeStateCode"] = "NA";
            //    row["IGSTRate"] = "NA";
            //    row["IGSTAmt"] = "NA";
            //    row["TaxableValue"] = "NA";
            //    row["CGSTRate"] = "NA";
            //    row["CGSTAmt"] = "NA";
            //    row["SGSTRate"] = "NA";
            //    row["SGSTAmt"] = "NA";
            //    row["InvoiceNo"] = "NA";
            //    row["Invoicedate"] = "NA";
            //    row["DifferentialValue"] = "NA";
            //    row["AdditionalTax"] = "NA";

            //    dtDummyTable.Rows.Add(row);

            //    #endregion

            //    GVInterStateConsumer.DataSource = dtDummyTable;
            //    GVInterStateConsumer.DataBind();

            //    GVIntraStateConsumer.DataSource = dtDummyTable;
            //    GVIntraStateConsumer.DataBind();

            //}
            //#endregion

            //#region ViewGSTR3
            //private DataSet ViewGSTR3(string SellerGSTN, string FromDt, String Todate)
            //{

            //    DataSet ds = new DataSet();


            //    ds = excelDB.ViewGSTR3(SellerGSTN, FromDt, Todate);          
            //    return ds;
            //}
            //#endregion

            //#region BACK_TO_PREVIOUS_PAGE
            //protected void btnBack_Click(object sender, EventArgs e)
            //{
            //    SellerGSTIN = HttpUtility.UrlEncode(excelDB.Encrypt(SellerGSTIN));
            //    Response.Redirect("~/fileReturn.aspx", true);
            //}
            //#endregion

        }
    }
}