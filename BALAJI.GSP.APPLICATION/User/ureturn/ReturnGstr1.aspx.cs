using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//new
using DataAccessLayer;
using GST.Utility;
using BusinessLogic.Repositories;

namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class ReturnGstr1 : System.Web.UI.Page
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Cookies["PreviousPage"] != null)
                    Response.Write(Request.Cookies["PreviousPage"].Value);
                BindFinyear();
                // BindInvoiceMonth();

            }

            uc_Gstr_Tileview.Info_Click += uc_Gstr_Tileview_Info_Click; //AddMoreClick += uc_B2B_Invoices_AddMoreClick;
            uc_invoiceMonth.SelectedIndexChange += uc_invoiceMonth_SelectIndexChange;
        }

        private void uc_invoiceMonth_SelectIndexChange(object sender, EventArgs e)
        {
            var month = uc_invoiceMonth.GetIndexValue;
            Session["Month"] = month;
        }

        private void uc_Gstr_Tileview_Info_Click(object sender, EventArgs e)
        {
            var year = ddlfinYear.SelectedValue;
            // var month = Convert.ToByte(ddlmonth.SelectedValue);
            var month = uc_invoiceMonth.GetIndexValue;
            Session["Month"] = month;

            // var invoiceAudit = unitOfwork.InvoiceAuditTrailRepositry.Filter(f => f.AuditTrailStatus == 0 && ).OrderByDescending(o => o.InvoiceDate).ToList();
            var invoiceList = unitOfwork.InvoiceRepository.Filter(f => f.InvoiceMonth == month && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
            
            //amits for gstr 3b condition 
            Response.Redirect("~/User/ureturn/GSTR1Details.aspx");
        }

        protected void ddlfinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Year"] = Convert.ToInt16(this.ddlfinYear.SelectedItem.Value);

        }
        public void BindFinyear()
        {
            try
            {
                ddlfinYear.DataSource = unitOfwork.FinYearRepository.All().OrderBy(o => o.Finyear).ToList();
                ddlfinYear.DataTextField = "Finyear_Format";
                ddlfinYear.DataValueField = "Fin_ID";
                ddlfinYear.DataBind();
                ddlfinYear.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
                string Year = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString().Substring((DateTime.Now.Year + 1).ToString().Length - 2);
                ddlfinYear.Items.FindByText(Year).Selected = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        ////Bind Month
        //public void BindInvoiceMonth()
        //{
        //    try
        //    {
        //        Array Months = Enum.GetValues(typeof(EnumConstants.FinYear));
        //        ddlmonth.Items.Clear();
        //        ddlmonth.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
        //        foreach (EnumConstants.FinYear month in Months)
        //        {
        //            ddlmonth.Items.Add(new ListItem(month.ToString(), ((byte)month).ToString()));
        //        }
        //        ddlmonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}

        //private List<GST_TRN_RETURN_STATUS> GetFilterInvoiceReturn(int Year, int SelectedMonth)
        //{
        //    unitOfwork = new UnitOfWork();
        //    List<GST_TRN_RETURN_STATUS> invoiceList = new List<GST_TRN_RETURN_STATUS>();
        //    var loggedinUser = Common.LoggedInUserID();
        //    var return1 = (byte)EnumConstants.Return.Gstr1;
        //    var return2 = (byte)EnumConstants.Return.Gstr3B;
        //    var invstatusfile = (byte)EnumConstants.ReturnFileStatus.FileGstr1;
        //    var invstatussave = (byte)EnumConstants.ReturnFileStatus.Save;
        //    var invstatussubmit = (byte)EnumConstants.ReturnFileStatus.Submit;
        //    //var FindMax = unitOfwork.ReturnStatusRepository.Filter(f => f.ReturnStatus = return1).Max(a => a.Action);
        //    var FindMax = unitOfwork.ReturnStatusRepository.All().Max(a => a.Action);
        //    invoiceList = unitOfwork.ReturnStatusRepository.Filter(f => f.User_id == loggedinUser && f.Period == SelectedMonth && f.Status == 1 && f.Action == 2).ToList();
        //    return invoiceList;
        //}


        //private List<GST_TRN_INVOICE> GetFilterInvoice(int Year, int SelectedMonth)
        //{
        //    unitOfwork = new UnitOfWork();
        //    List<GST_TRN_INVOICE> invoiceList = new List<GST_TRN_INVOICE>();
        //    var loggedinUser = Common.LoggedInUserID();

        //    invoiceList = unitOfwork.InvoiceRepository.Filter(f => f.InvoiceUserID == loggedinUser && f.InvoiceMonth == SelectedMonth).ToList();
        //    return invoiceList;
        //}
        private List<GST_TRN_RETURN_STATUS> GetFilterInvoice(int Year, int SelectedMonth)
        {
            unitOfwork = new UnitOfWork();
            List<GST_TRN_RETURN_STATUS> invoiceList = new List<GST_TRN_RETURN_STATUS>();
            var loggedinUser = Common.LoggedInUserID();
            var return1 = (byte)EnumConstants.Return.Gstr1;
            var return2 = (byte)EnumConstants.Return.Gstr3B;

            var invstatusfile = (byte)EnumConstants.ReturnFileStatus.FileGstr1;
            var invstatussave = (byte)EnumConstants.ReturnFileStatus.Save;
            var invstatussubmit = (byte)EnumConstants.ReturnFileStatus.Submit;

            var FindMax = unitOfwork.ReturnStatusRepository.Filter(f => f.Period == SelectedMonth && f.FinYear_ID == Year).Max(a => a.Action);
            invoiceList = unitOfwork.ReturnStatusRepository.Filter(f => f.User_id == loggedinUser && f.Period == SelectedMonth && f.FinYear_ID == Year && f.Action == FindMax).ToList();
            return invoiceList;
        }


        private void PopulateTileViewInvoices(int Year, int SelectedMonth, int Status)
        {

            var ddlmonth = uc_invoiceMonth.GetIndexValue;
            var ddlyear = ddlfinYear.SelectedValue;
            var invstatusfile = EnumConstants.ReturnFileStatus.FileGstr1;
            var invstatussave = EnumConstants.ReturnFileStatus.Save;
            var invstatussubmit = (byte)EnumConstants.ReturnFileStatus.Submit;
            var invoicesB2B = GetFilterInvoice(Convert.ToInt16(ddlyear), Convert.ToInt16(ddlmonth));
            var dataB2b = invoicesB2B;
            uc_Gstr_Tileview.InvoiceList = dataB2b;
           
            //if (Status == invstatusfile || Status == invstatussave || Status == invstatussubmit)
            //{
            //    var invoicesB2BA = GetFilterInvoiceReturn(Convert.ToInt16(ddlyear), Convert.ToInt16(ddlmonth));
            //    var dataB2bA = invoicesB2BA;
            //    //uc_Gstr_Tileview.InvoiceList = dataB2bA;
            //}
            //else 
            //{ 
            //    var invoicesB2B = GetFilterInvoice(Convert.ToInt16(ddlyear), Convert.ToInt16(ddlmonth));
            //    var dataB2bc = invoicesB2B;
            //    uc_Gstr_Tileview.InvoiceList = dataB2bc;
            //}


            
         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            var year = ddlfinYear.SelectedValue;
            var month = uc_invoiceMonth.GetIndexValue;
            var CurrentMonth = DateTime.Now.Month;
            if (month != CurrentMonth)
            {
                // uc_Gstr_Tileview.Re.Visible = true;
                PopulateTileViewInvoices(Convert.ToInt16(year), Convert.ToInt16(month), 3);
            }
        }
    }
}