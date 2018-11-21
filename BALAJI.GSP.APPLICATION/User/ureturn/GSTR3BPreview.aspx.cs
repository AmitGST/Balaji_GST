using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogic.Repositories.GSTN;
using DataAccessLayer;

namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR3BPreview : System.Web.UI.Page
    {
        int MonthName;
        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var loggedInUser = Common.LoggedInUserID();
                //Getprocdata_3B_1(loggedInUser, DateTime.Now.Month - 1);
                BindAllGstr_3B(loggedInUser, DateTime.Now.Month - 1);
                var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Enabled = false;
            }
            //Event Handler amits
            uc_GSTNUsers.addInvoiceRedirect += uc_GSTNUsers_addInvoiceRedirect;
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
            uc_GSTNUsers.addInvoicechkRedirect += uc_GSTNUsers_addInvoicechkRedirect;
        }

        private void uc_GSTNUsers_addInvoicechkRedirect(object sender, EventArgs e)
        {

            var ddlvalue = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
            var chkvalue= uc_GSTNUsers.GetchkValue;
            var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Enabled = false;
            if (ddlvalue == 0)
            {
                ddlenable = false;
                var loggedInUser = Common.LoggedInUserID();
                //for turnover start
                Label mpLabel = (Label)uc_GSTR_Taxpayer.FindControl("lblTurnoverAMT");
                int Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);
                List<string> userLists = new List<string>();
                if (uc_GSTNUsers.AssociatedUsersIds != null)
                {
                    userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
                }
                userLists.Add(loggedInUser);
                mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                //End

                BindAllGstr_3B(loggedInUser, DateTime.Now.Month - 1);
            }
            else
            {
                var SellerUserId = uc_GSTNUsers.GetSellerProfile;
                BindAllGstr_3B(SellerUserId, DateTime.Now.Month - 1);
            }
        }

        private void uc_GSTNUsers_addInvoiceRedirect(object sender, EventArgs e)
        {
           var SellerUserId = uc_GSTNUsers.GetSellerProfile;
           int Month = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);
           int Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);
           int ddlselected = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
           var loggedinUser = Common.LoggedInUserID();
           Label mpLabel = (Label)uc_GSTR_Taxpayer.FindControl("lblTurnoverAMT");
            //for turnover amits
           List<string> userLists = new List<string>();
           if (uc_GSTNUsers.AssociatedUsersIds != null)
           {
               userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
           }
           userLists.Add(loggedinUser);
           //var abc = ReturnSelf;
           //this.ReturnSelf = uc_GSTNUsers.GetItem;
           if (ddlselected > 0)
           {
               // lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
               mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == SellerUserId).Sum(s => s.TaxableAmount).ToString();
           }
           else
           {
               mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
               //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
           }
          // Label mpLabel = (Label)uc_GSTR_Taxpayer.FindControl("lblTurnoverAMT");
           //pLabel.Text = "Amit";
           BindAllGstr_3B(SellerUserId, Month);
        }

        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {
            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            BindAllGstr_3B(SellerUserId,mName);
        }

        int mName;
        public void BindAllGstr_3B(string SellerUserId, int mName)
        {
            Getprocdata_3B_3_1(SellerUserId, mName);
            Getprocdata_3B_3_2(SellerUserId, mName);
            Getprocdata_3B_4A(SellerUserId, mName);
            Getprocdata_3B_4B(SellerUserId, mName);
            Getprocdata_3B_4C(SellerUserId, mName);
            Getprocdata_3B_4D(SellerUserId, mName);
            Getprocdata_3B_5(SellerUserId, mName);
            Getprocdata_3B_6A(SellerUserId, mName);
            Getprocdata_3B_6B(SellerUserId, mName);
        }


        //GSTR-3B_1 
        public DataTable Getprocdata_3B_3_1(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR_3B_3_1", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_3_1_A.DataSource = table;
                lv_GSTR3B_3_1_A.DataBind();
                return table;
            }
        }

        //GSTR-3B_2 
        public DataTable Getprocdata_3B_3_2(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_3_2", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_3_2.DataSource = table;
                lv_GSTR3B_3_2.DataBind();
                return table;
            }
        }

        //GSTR-3B_4A 
        public DataTable Getprocdata_3B_4A(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_4A", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_4A.DataSource = table;
                lv_GSTR3B_4A.DataBind();
                return table;
            }
        }

        //GSTR-3B_4B 
        public DataTable Getprocdata_3B_4B(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_4B", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_4B.DataSource = table;
                lv_GSTR3B_4B.DataBind();
                return table;
            }
        }

        //GSTR-3B_4C 
        public DataTable Getprocdata_3B_4C(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_4C", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_4C.DataSource = table;
                lv_GSTR3B_4C.DataBind();
                return table;
            }
        }

        //GSTR-3B_4D 
        public DataTable Getprocdata_3B_4D(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_4D", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_4D.DataSource = table;
                lv_GSTR3B_4D.DataBind();
                return table;
            }
        }

        //GSTR-3B_5 
        public DataTable Getprocdata_3B_5(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_5", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lv_GSTR3B_5.DataSource = table;
                lv_GSTR3B_5.DataBind();
                return table;
            }
        }

        //GSTR-3B_6.1 
        public DataTable Getprocdata_3B_6A(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_6_1", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lvGSTR1_6A.DataSource = table;
                lvGSTR1_6A.DataBind();
                return table;
            }
        }

        //GSTR-3B_6.2 
        public DataTable Getprocdata_3B_6B(string SellerUserId, int month)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var cmd = new SqlCommand("PROC_FILE_GSTR3B_6_2", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@SELLERUSERID", SellerUserId);
                cmd.Parameters.AddWithValue("@MONTH", month);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
                lvGSTR1_6B.DataSource = table;
                lvGSTR1_6B.DataBind();
                return table;
            }
        }

        public void lkbDownload3B_Click(object sender, EventArgs e)
        {


            cls_Invoice_JSON jsonData = new cls_Invoice_JSON();
            List<GST_TRN_INVOICE> invoicelist = new List<GST_TRN_INVOICE>();
            var loggedInUser = Common.LoggedInUserID();
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);

            ////for all data
            var InvoiceJson = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId).Where(x => x.InvoiceUserID == SellerUserId && x.InvoiceMonth == mName).ToList();

            string text = jsonData.GetInvoiceJSONData(InvoiceJson);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("content-disposition", "attachment;filename=\"data.json\"");
            Response.Write(text);
            Response.End();
            //try
            //{
            //    //LinkButton lkbdownload = (LinkButton)sender;
            //    ////String UserID = Convert.ToString(lkbdownload.CommandArgument.ToString());
            //    //var userId = Common.LoggedInUserID();
            //    ////var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
            //    //int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            //    //var SelectedMonth = mName;
            //    ///// var invoice = unitOfWork.OfflineAudittrailRepository.Filter(f => f.UserID == userId).FirstOrDefault();

            //    //ReportGenerate.DownloadGstr_3B(userId, SelectedMonth);
            //}
            //catch (Exception ex)
            //{
            //    cls_ErrorLog ob = new cls_ErrorLog();
            //    cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            //}
        }

        protected void lkbFileGSTR3B_Click(object sender, EventArgs e)
        {
           this.Master.SuccessMessage = "Data Filed Successfully .";
            //SendHTMLMail(mailString, sellerMail.Remove(0, 1));
            //uc_sucess.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
        }
    }
}