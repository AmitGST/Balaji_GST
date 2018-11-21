using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic.Repositories;
using GST.Utility;
using DataAccessLayer;
using System.Net;
using System.Globalization;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using BusinessLogic.Repositories.GSTN;

//using BusinessLogic.Repositories.GSTN;



namespace BALAJI.GSP.APPLICATION.User.uinvoice
{
    public partial class ViewInvoice : System.Web.UI.Page
    {
        ExcelDB excelDB = new ExcelDB();
        string flag = string.Empty;
        string UniqueNo = string.Empty;
        string strSupplyType = string.Empty;
        string SellerGSTIN = string.Empty;
        DataSet ds = new DataSet();

        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //   BindAllInvoices();
                    // int i=(int)EnumConstants.InvoiceType.B2B;
                    //uc_GSTNUsers.BindUsers();
                    //var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Visible = false;
                    //if (Session["pageno"] != null) /// set datapager to page 2
                    //    DataPager1.SetPageProperties(Convert.ToInt32(Session["pageno"]), 10, false); 
                    PopulateTileViewInvoices(null, null, DateTime.Now.Month - 1);
                    ShowButton();
                    // GetInvoiceType("6418");

                }

                // uc_invoiceMonth.SetValue = DateTime.Now.Month.ToString();
                // uc_InvoiceView.UpdateInvoiceClick += uc_InvoiceView_UpdateInvoiceClick;  
                uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
                uc_GSTNUsers.addInvoiceRedirect += uc_GSTNUsers_addInvoiceRedirect;
                uc_GSTNUsers.addInvoicechkRedirect += uc_GSTNUsers_addInvoicechkRedirect;
                uc_TileViewB2B.Info_Click += uc_TileView_InfoClick;
                uc_TileViewB2C.Info_Click += uc_TileView_InfoClick;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void uc_GSTNUsers_addInvoicechkRedirect(object sender, EventArgs e)
        {
            try
            {
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                if (divMain.Visible == true)
                {
                    PopulateTileViewInvoices(null, null, Month);
                }
                else
                {
                    PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
                }
                var loggedinUser = Common.LoggedInUserID();
                var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
                if (InvoiceSplCondn == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)
                {
                    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                }
                else if (InvoiceSplCondn == (byte)EnumConstants.InvoiceSpecialCondition.Import)
                {
                    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void uc_TileView_InfoClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkb = (LinkButton)sender;
                ViewState["CommandName"] = Convert.ToByte(lkb.CommandName);
                ViewState["invoiceType"] = Convert.ToByte(lkb.CommandArgument);
                uc_invoiceMonth.Visible = false;
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(lkb.CommandName);
                var invoiceType = Convert.ToInt16(lkb.CommandArgument);
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
                //ViewState[""] = lkb.CommandName;
                divViewTypeInvoices.Visible = true;
                divInvoiceReturn.Visible = false;
                divMain.Visible = false;
                lkbBack.Visible = true;
                lkbUpdateInvoice.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //int? specialCondition = null;
        private List<GST_TRN_INVOICE> GetFilterInvoice(int invoiceType, int? specialCondition, int SelectedMonth)
        {
            unitOfWork = new UnitOfWork();
            List<GST_TRN_INVOICE> invoiceList = new List<GST_TRN_INVOICE>();
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            var loggedinUser = Common.LoggedInUserID();
            List<string> userLists = new List<string>();
            try
            {
                if (uc_GSTNUsers.AssociatedUsersIds != null)
                {
                    userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
                }
                userLists.Add(loggedinUser);
                if (uc_GSTNUsers.ddlGSTNUsers.SelectedIndex > 0)
                {
                    if (specialCondition.HasValue)
                    {
                        invoiceList = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && (f.InvoiceStatus == 0) && f.InvoiceType == invoiceType && f.InvoiceSpecialCondition == specialCondition && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                    }
                    else
                    {
                        invoiceList = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && (f.InvoiceStatus == 0) && f.InvoiceType == invoiceType && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                        // invoiceList = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && f.InvoiceType == invoiceType  && userLists.Contains(f.InvoiceUserID) && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                    }
                }
                else
                {
                    if (specialCondition.HasValue)
                    {
                        //Apply Or Condition for Editing the Invoice Issue Left(Amended)issueAnkita
                        invoiceList = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && f.InvoiceType == invoiceType && f.InvoiceSpecialCondition == specialCondition && userLists.Contains(f.InvoiceUserID) && (f.InvoiceStatus == 0) && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                    }
                    else
                    {
                        invoiceList = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && f.InvoiceType == invoiceType && userLists.Contains(f.InvoiceUserID) && (f.InvoiceStatus == 0) && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            return invoiceList;
        }

        private void PopulateTileViewInvoices(int? invoiceType, int? specialCondition, int SelectedMonth)
        {
            try
            {
                var invoicetypeB2B = (Byte)EnumConstants.InvoiceType.B2B;
                var invoicetypeB2C = (Byte)EnumConstants.InvoiceType.B2C;
                var invoicesB2B = GetFilterInvoice(invoicetypeB2B, specialCondition, SelectedMonth);
                var invoicesB2C = GetFilterInvoice(invoicetypeB2C, specialCondition, SelectedMonth);
                var dataB2b = invoicesB2B;
                var dataB2c = invoicesB2C;//.ToList().Where(w => w.InvoiceType == invoicetypeB2C).ToList();
                uc_TileViewB2B.InvoiceList = dataB2b;
                uc_TileViewB2C.InvoiceList = dataB2c;
                //
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void PopulateInvoiceTypeInvoices(int invoiceType, int specialCondition, int SelectedMonth)
        {
            try
            {
                var invoices = GetFilterInvoice(invoiceType, specialCondition, SelectedMonth);
                lvInvoices.DataSource = invoices.ToList();
                lvInvoices.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //private void PopulateB2BTypeInvoices(int invoiceType,int specialCondition, int SelectedMonth)
        //{
        //    //if (uc_GSTNUsers.ddlGSTNUsers.SelectedIndex > 0)
        //    //{
        //    //    var SellerUserId = uc_GSTNUsers.GetSellerProfile;
        //    //    var loggedinUser = Common.LoggedInUserID();
        //    //    var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
        //    //    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //    //    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.InvoiceSpecialCondition == InvoiceSplCondn && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //    //    lvInvoices.DataSource = invoices.ToList();
        //    //    lvInvoices.DataBind();
        //    //}
        //    //else
        //    //{
        //    //    var loggedinUser = Common.LoggedInUserID();
        //    //    List<string> userLists = new List<string>();
        //    //    if (uc_GSTNUsers.AssociatedUsersIds != null)
        //    //        userLists = uc_GSTNUsers.AssociatedUsersIds;

        //    //    userLists.Add(loggedinUser);
        //    //    var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);

        //    //    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && f.InvoiceSpecialCondition == InvoiceSplCondn && userLists.Contains(f.InvoiceUserID) && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //    //    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && userLists.Contains(f.InvoiceUserID) && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //    //    // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //    //    lvInvoices.DataSource = invoices.ToList();
        //    //    lvInvoices.DataBind();
        //    //}
        //    try
        //    {

        //        var SellerUserId = uc_GSTNUsers.GetSellerProfile;

        //        //var loggedinUser = Common.LoggedInUserID();pu
        //        //var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
        //        ////var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //        //var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == Month && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //        if (uc_GSTNUsers.ddlGSTNUsers.SelectedIndex > 0)
        //        {


        //            //var SellerUserId = uc_GSTNUsers.GetSellerProfile;
        //            var loggedinUser = Common.LoggedInUserID();
        //            //var SelectedMontha = Convert.ToByte(uc_invoiceMonth.GetValue);
        //            //var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.InvoiceType == invoiceType && f.InvoiceSpecialCondition == specialCondition && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            lvInvoices.DataSource = invoices.ToList();
        //            lvInvoices.DataBind();
        //        }
        //        else
        //        {
        //            var loggedinUser = Common.LoggedInUserID();
        //            List<string> userLists = new List<string>();
        //            if (uc_GSTNUsers.AssociatedUsersIds != null)
        //                userLists = uc_GSTNUsers.AssociatedUsersIds;
        //            userLists.Add(loggedinUser);
        //            //var SelectedMontha = Convert.ToByte(uc_invoiceMonth.GetValue);
        //            var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && f.InvoiceType == invoiceType && f.InvoiceSpecialCondition == specialCondition && userLists.Contains(f.InvoiceUserID) && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            lvInvoices.DataSource = invoices.ToList();
        //            lvInvoices.DataBind();
        //        }

        //    }
        //    catch (Exception ex) { }
        //}

        //Get All Invoice related to seller
        private void uc_GSTNUsers_addInvoiceRedirect(object sender, EventArgs e)
        {
            try
            {
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                if (divMain.Visible == true)
                {
                    PopulateTileViewInvoices(null, null, Month);
                }
                else
                {
                    PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
                }
                //PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        private void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                if (Common.IsTaxConsultant())
                {
                    if (divMain.Visible == true)
                    {
                        PopulateTileViewInvoices(null, null, Month);
                    }
                    else
                    {
                        PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
                    }
                }
                if (Common.IsUser())
                {
                    uc_GSTNUsers.ddlGSTNUsers.SelectedIndex = -1;
                    if (divMain.Visible == true)
                    {
                        PopulateTileViewInvoices(null, null, Month);
                    }
                    else
                    {
                        PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        void uc_InvoiceView_UpdateInvoiceClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                unitOfWork = new UnitOfWork();

                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //private void BindAllInvoices()
        //{
        //    unitOfWork = new UnitOfWork();
        //    var SellerUserId = uc_GSTNUsers.GetSellerProfile;
        //    var loggedinUserId = Common.LoggedInUserID();
        //    var SelectedMonth = DateTime.Now.Month - 1;
        //    //var InvoiceType = EnumConstants.InvoiceType;
        //    if (IsPostBack)
        //    {
        //        SelectedMonth = Int32.Parse(uc_invoiceMonth.GetValue);
        //    }
        //    if (loggedinUserId != null)
        //    {
        //        if (uc_GSTNUsers.ddlGSTNUsers.SelectedIndex > 0)
        //        {


        //            //var SellerUserId = uc_GSTNUsers.GetSellerProfile;
        //            var loggedinUser = Common.LoggedInUserID();
        //            //var SelectedMontha = Convert.ToByte(uc_invoiceMonth.GetValue);
        //            //var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedDate.Value.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            var invoicetypeB2B = (Byte)EnumConstants.InvoiceType.B2B;
        //            var invoicetypeB2C = (Byte)EnumConstants.InvoiceType.B2C;
        //            var dataB2b = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2B).ToList();
        //            var dataB2c = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2C).ToList();
        //            uc_TileViewB2B.InvoiceList = dataB2b;
        //            uc_TileViewB2C.InvoiceList = dataB2c;

        //            lvInvoices.DataSource = invoices.ToList();
        //            lvInvoices.DataBind();
        //        }
        //        else
        //        {
        //            var loggedinUser = Common.LoggedInUserID();
        //            List<string> userLists = new List<string>();
        //            if (uc_GSTNUsers.AssociatedUsersIds != null)
        //                userLists = uc_GSTNUsers.AssociatedUsersIds;

        //            userLists.Add(loggedinUser);
        //            //var SelectedMontha = Convert.ToByte(uc_invoiceMonth.GetValue);
        //            var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceMonth == SelectedMonth && userLists.Contains(f.InvoiceUserID) && f.InvoiceStatus == 0 && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            // var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == SelectedMonth && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
        //            var invoicetypeB2B = (Byte)EnumConstants.InvoiceType.B2B;
        //            var invoicetypeB2C = (Byte)EnumConstants.InvoiceType.B2C;
        //            var dataB2b = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2B).ToList();
        //            var dataB2c = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2C).ToList();
        //            uc_TileViewB2B.InvoiceList = dataB2b;
        //            uc_TileViewB2C.InvoiceList = dataB2c;
        //            lvInvoices.DataSource = invoices.ToList();
        //            lvInvoices.DataBind();
        //        }
        //    }

        //    //if (loggedinUserId != null)
        //    //{
        //    //    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId && f.InvoiceStatus == 0 && f.InvoiceMonth == SelectedMonth && f.Status == true).OrderByDescending(o => o.CreatedDate);

        //    //var invoicetypeB2B = (Byte)EnumConstants.InvoiceType.B2B;
        //    //var invoicetypeB2C = (Byte)EnumConstants.InvoiceType.B2C;
        //    //var dataB2b = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2B).ToList();
        //    //var dataB2c = invoices.ToList().Where(w => w.InvoiceType == invoicetypeB2C).ToList();
        //    //uc_TileViewB2B.InvoiceList = dataB2b;0 

        //    //uc_TileViewB2C.InvoiceList = dataB2c;
        //    //    lvInvoices.DataSource = invoices.ToList();
        //    //    lvInvoices.DataBind();
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "lvItemsDataTable", "$('#lvItems').dataTable();", true);
        //    //}
        //}



        protected void lkb_action_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lkbItem = (LinkButton)sender;

                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    Int64 invoiceId = Convert.ToInt64(lkbItem.CommandArgument.ToString());
                    var invoice = unitOfWork.InvoiceDataRepository.Filter(f => f.InvoiceID == invoiceId).ToList();

                    uc_InvoiceView.InvoiceData = invoice;

                    // uc_InvoiceView.SellerData = invoice;
                }

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        /// <summary>
        /// Commented all code below here --ashish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgFromDate_Click(object sender, ImageClickEventArgs e)
        {
            //calFromDt.Visible = true;
        }
        protected void calFromDt_SelectionChanged(object sender, EventArgs e)
        {
            //txtFromDt.Text = calFromDt.SelectedDate.ToShortDateString();
            //calFromDt.Visible = false;
        }
        protected void imgToDt_Click(object sender, ImageClickEventArgs e)
        {
            // calToDt.Visible = true;
        }
        protected void calToDt_SelectionChanged(object sender, EventArgs e)
        {
            // txtToDt.Text = calToDt.SelectedDate.ToShortDateString();
            // calToDt.Visible = false;
            ///  SellerGSTIN = (string)Session["GSTN"];
            populateInvoiceddl(SellerGSTIN, txtFromDt.Text, txtToDt.Text);
        }

        public void populateInvoiceddl(string SellerGSTIN, string Fromdt, string Todt)
        {
            try
            {
                ds = excelDB.populateInvoiceddl(SellerGSTIN, Fromdt, Todt);

                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlInvoiceNo.DataSource = ds.Tables[0];
                        ddlInvoiceNo.DataTextField = "InvoiceNo";
                        ddlInvoiceNo.DataValueField = "InvoiceNo";
                        ddlInvoiceNo.DataBind();
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[1].Rows[0][0]);
                        lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[1].Rows[0][1]);


                        ddlInvoiceNo.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
                        ddlInvoiceNo.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UniqueNo = ddlInvoiceNo.SelectedValue;
                BindGrid(UniqueNo);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindGrid(string UniqueNo)
        {
            ds = excelDB.FetchInvoicePreviewData(UniqueNo, "B");

            #region B2B
            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Contains("SellerDtls"))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //  // lblInvoiceNumber.Text = "Serial No. of Invoice : " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                            //  //  lblInvoiceDate.Text = "Invoice Date : " + Convert.ToString(ds.Tables[0].Rows[0][1]);
                            //  lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[0].Rows[0][2]);
                            //  lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[0].Rows[0][3]);
                            //  //  lblSellerAddress.Text = "Address : " + Convert.ToString(ds.Tables[0].Rows[0][4]);
                            ////  hdnSellerGSTN.Value = Convert.ToString(ds.Tables[0].Rows[0][2]);

                            lblFreight.Text = "Freight : " + Convert.ToString(ds.Tables[0].Rows[0][14]);
                            lblInsurance.Text = "Insurance : " + Convert.ToString(ds.Tables[0].Rows[0][15]);
                            lblPackingAndForwadingCharges.Text = "PackingAndForwadingCharges : " + Convert.ToString(ds.Tables[0].Rows[0][16]);


                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of SellerDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("ReceiverDtls"))
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            lblRecieverGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[1].Rows[0][1]);
                            lblRecieverName.Text = "Name : " + Convert.ToString(ds.Tables[1].Rows[0][2]);
                            lblRecieverAddress.Text = "Address : " + Convert.ToString(ds.Tables[1].Rows[0][3]);
                            lblRecieverStateCode.Text = "State Code : " + Convert.ToString(ds.Tables[1].Rows[0][6]);
                            lblReceiverState.Text = "State : " + Convert.ToString(ds.Tables[1].Rows[0][5]);

                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of ReceiverDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("ConsigneeDtls"))
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            lblConsigneeGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[2].Rows[0][1]);
                            lblConsigneeName.Text = "Name : " + Convert.ToString(ds.Tables[2].Rows[0][2]);
                            lblConsigneeAddress.Text = "Address : " + Convert.ToString(ds.Tables[2].Rows[0][3]);
                            lblConsigneeStateCode.Text = "State Code : " + Convert.ToString(ds.Tables[2].Rows[0][6]);
                            lblConsigneeState.Text = "State : " + Convert.ToString(ds.Tables[2].Rows[0][5]);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of ConsigneeDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("InvoiceDtls"))
                    {
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            int k = ds.Tables[3].Rows.Count;

                            for (int i = 0; i <= k - 1; i++)
                            {
                                HtmlTableRow tRow = new HtmlTableRow();
                                for (int j = 0; j <= 14; j++)
                                {
                                    HtmlTableCell tb = new HtmlTableCell();
                                    tb.InnerText = Convert.ToString(ds.Tables[3].Rows[i][j]);
                                    tRow.Controls.Add(tb);
                                }
                                tblPreview.Rows.Add(tRow);
                            }

                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of InvoiceDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("AmountDtls"))
                    {
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            HtmlTableRow tRow = new HtmlTableRow();
                            for (int j = 0; j <= 14; j++)
                            {
                                HtmlTableCell tb = new HtmlTableCell();
                                tb.InnerText = Convert.ToString(ds.Tables[4].Rows[0][j]);
                                tRow.Controls.Add(tb);
                            }
                            tblPreview.Rows.Add(tRow);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of AmountDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("AmountInWords"))
                    {
                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            AmtInFigureVal.Text = Convert.ToString(ds.Tables[5].Rows[0][0]);
                            AmtInWordsVal.Text = Convert.ToString(ds.Tables[5].Rows[0][1]);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of AmountInWords table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }

                }
                else
                {
                    //lblMsg.Text = "System Error !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            #endregion

        }

        #region BACK_TO_PREVIOUS_PAGE
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                //No need to send here gstn no--Ashish
                SellerGSTIN = (string)Session["GSTN"];
                SellerGSTIN = HttpUtility.UrlEncode(excelDB.Encrypt(SellerGSTIN));
                Response.Redirect("~/GSTInvoiceDashBoard.aspx", true);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        #endregion

        protected void btnGetInvoice_Click(object sender, EventArgs e)
        {
            populateInvoiceddl(Page.User.Identity.Name, txtFromDt.Text.Trim(), txtToDt.Text.Trim());
        }

        private Seller BindInvoice(DataSet dsInvoice)
        {
            Seller obj = new Seller();
            // Invoice inv = new Invoice();
            obj.CreateInvoice(obj, InvoiceType.B2BInvoice.ToString());
            #region B2B
            try
            {
                if (dsInvoice.Tables.Count > 0)
                {
                    if (dsInvoice.Tables.Contains("SellerDtls"))
                    {
                        if (dsInvoice.Tables[0].Rows.Count > 0)
                        {
                            //  // lblInvoiceNumber.Text = "Serial No. of Invoice : " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                            //  //  lblInvoiceDate.Text = "Invoice Date : " + Convert.ToString(ds.Tables[0].Rows[0][1]);
                            //  lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[0].Rows[0][2]);
                            //  lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[0].Rows[0][3]);
                            //  //  lblSellerAddress.Text = "Address : " + Convert.ToString(ds.Tables[0].Rows[0][4]);
                            ////  hdnSellerGSTN.Value = Convert.ToString(ds.Tables[0].Rows[0][2]);
                            obj.SellerInvoice = Convert.ToString(ds.Tables[0].Rows[0][0]);
                            obj.DateOfInvoice = Convert.ToString(ds.Tables[0].Rows[0][1]);

                            obj.GSTIN = Convert.ToString(ds.Tables[0].Rows[0][2]);
                            obj.NameAsOnGST = Convert.ToString(ds.Tables[0].Rows[0][3]);
                            obj.Address = Convert.ToString(ds.Tables[0].Rows[0][8]);
                            // obj.SellerStateCode = Convert.ToString(ds.Tables[0].Rows[0][6]);
                            obj.SellerStateName = Convert.ToString(ds.Tables[0].Rows[0][10]);


                            obj.Invoice.Freight = Convert.ToDecimal(ds.Tables[0].Rows[0][14]);
                            obj.Invoice.PackingAndForwadingCharges = Convert.ToDecimal(ds.Tables[0].Rows[0][16]);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of SellerDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("ReceiverDtls"))
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            Reciever rec = new Reciever();
                            rec.GSTIN = Convert.ToString(ds.Tables[1].Rows[0][1]);
                            rec.NameAsOnGST = Convert.ToString(ds.Tables[1].Rows[0][2]);
                            rec.Address = Convert.ToString(ds.Tables[1].Rows[0][3]);
                            rec.StateCode = Convert.ToString(ds.Tables[1].Rows[0][6]);
                            rec.StateName = Convert.ToString(ds.Tables[1].Rows[0][5]);
                            obj.Reciever = rec;
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of ReceiverDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("ConsigneeDtls"))
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            Consignee con = new Consignee();
                            con.GSTIN = Convert.ToString(ds.Tables[2].Rows[0][1]);
                            con.NameAsOnGST = Convert.ToString(ds.Tables[2].Rows[0][2]);
                            con.Address = Convert.ToString(ds.Tables[2].Rows[0][3]);
                            con.StateCode = Convert.ToString(ds.Tables[2].Rows[0][6]);
                            con.StateName = Convert.ToString(ds.Tables[2].Rows[0][5]);
                            obj.Consignee = con;
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of ConsigneeDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("InvoiceDtls"))
                    {
                        //LineID,Description,HSN,Qty,Unit,Rate,Total,Discount,AmountWithTax,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,IGSTRate,IGSTAmt
                        DataTable dt = ds.Tables["InvoiceDtls"];
                        HSN hsn = new HSN();
                        var leItem = (from item in dt.AsEnumerable()
                                      select new LineEntry
                                      {
                                          LineID = Convert.ToInt32(item.Field<string>("LineID")),
                                          //HSN=item..Select(new HSN{Description})
                                          //HSN = new HSN().Description = item.Field<string>("Description"),                                     
                                          //HSN = new HSN().HSNNumber = item.Field<string>("HSN"),
                                          HSN = ConvertItemToHSN(item),
                                          Qty = Convert.ToInt32(item.Field<string>("Qty")),
                                          //Unit = Convert.ToInt32(item.Field<string>("Unit")),
                                          PerUnitRate = Convert.ToDecimal(item.Field<string>("Rate")),
                                          TotalLineIDWise = Convert.ToDecimal(item.Field<string>("Total")),
                                          Discount = Convert.ToDecimal(item.Field<string>("Discount")),
                                          TaxValue = Convert.ToDecimal(item.Field<string>("TaxableValue")),
                                          AmountWithTax = Convert.ToDecimal(item.Field<string>("AmountWithTax")),
                                          AmtCGSTLineIDWise = Convert.ToDecimal(item.Field<string>("CGSTAmt")),
                                          AmtSGSTLineIDWise = Convert.ToDecimal(item.Field<string>("SGSTAmt")),
                                          AmtIGSTLineIDWise = Convert.ToDecimal(item.Field<string>("IGSTAmt"))
                                      }).ToList();


                        obj.Invoice.LineEntry = leItem;
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of InvoiceDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("AmountDtls"))
                    {
                        obj.TotalAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][6]);
                        obj.TotalDiscount = Convert.ToDecimal(ds.Tables[4].Rows[0][7]);
                        obj.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[4].Rows[0][8]);
                        obj.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][10]);
                        obj.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][12]);
                        obj.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][14]);

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            HtmlTableRow tRow = new HtmlTableRow();
                            for (int j = 0; j <= 14; j++)
                            {
                                HtmlTableCell tb = new HtmlTableCell();
                                tb.InnerText = Convert.ToString(ds.Tables[4].Rows[0][j]);
                                tRow.Controls.Add(tb);
                            }
                            tblPreview.Rows.Add(tRow);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of AmountDtls table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }
                    if (ds.Tables.Contains("AmountInWords"))
                    {
                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            obj.Invoice.TotalInvoiceValue = Convert.ToDecimal(ds.Tables[5].Rows[0][0]);
                            obj.Invoice.TotalInvoiceWords = Convert.ToString(ds.Tables[5].Rows[0][1]);
                        }
                    }
                    else
                    {
                        //lblMsg.Text = "System error occured during data population of AmountInWords table !!!";
                        //this.InvoicePreviewModalPopupExtender.Show();
                        //return;
                    }

                }
                else
                {
                    //lblMsg.Text = "System Error !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            return obj;
            #endregion

        }
        private void ShowButton()
        {
            if (Common.IsTaxConsultant())
            {
                divInvoiceReturn.Visible = true;
            }
            if (Common.IsUser())
            { divInvoiceReturn.Visible = false; }
        }


        private HSN ConvertItemToHSN(DataRow item)
        {
            HSN hsn = new HSN();
            hsn.HSNNumber = item.Field<string>("HSN");
            hsn.Description = item.Field<string>("Description");
            hsn.RateCGST = Convert.ToDecimal(item.Field<string>("CGSTRate"));
            hsn.RateIGST = Convert.ToDecimal(item.Field<string>("IGSTRate"));
            hsn.RateSGST = Convert.ToDecimal(item.Field<string>("SGSTRate"));
            //hsn.PerUnitRate = Convert.ToDecimal(item.Field<string>("Rate"));
            hsn.UnitOfMeasurement = item.Field<string>("Unit");

            return hsn;
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {
                // Get the IP
                // string myIP = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();// Dns.GetHostEntry(hostName).AddressList[0].ToString();
                int count = 0;
                List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                List<string> mailsToList = new List<string>();
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    Int64 invoiceID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();
                        var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                        var invoiceDetaila = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.InvoiceID == invoiceDetail.InvoiceID).Where(w => w.AuditTrailStatus == 8);
                        var invoiceAuditTrailstatus = unitOfWork.InvoiceAuditTrailRepositry.Find(f => f.AuditTrailStatus == 8 && f.InvoiceID == invoiceDetail.InvoiceID);

                        if (invoiceAuditTrailstatus != null)
                        {
                            audittrail.InvoiceID = invoiceID;
                            int invoiceid = Convert.ToInt32(audittrail.InvoiceID);
                            audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Upload);
                            audittrail.UserIP = Common.IP;
                            audittrail.InvoiceID = invoiceid;
                            audittrail.CreatedDate = DateTime.Now;
                            audittrail.CreatedBy = Common.LoggedInUserID();
                            audittrail.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                            unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                            unitOfWork.Save();
                            if (!mailsToList.Contains(invoiceDetail.AspNetUser.Email))
                                //{
                                mailsToList.Add(invoiceDetail.AspNetUser.Email);
                            //}
                            attribute.UserName = invoiceDetail.AspNetUser.OrganizationName;
                            //attribute.MailsTo.Add();
                            attribute.InvoiceNo = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceNo"].ToString();
                            attribute.InvoiceDate = DateTimeAgo.GetFormatDate(invoiceDetail.InvoiceDate);
                            attribute.InvoiceTotalAmount = invoiceDetail.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                            invAttributes.Add(attribute);
                            count = count + 1;

                            if (count > 0)
                            {
                                string mailString = string.Empty;
                                string sellerMail = string.Empty;
                                clsMessageAttribute mailData = new clsMessageAttribute();


                                foreach (clsMessageAttribute iId in invAttributes)
                                {
                                    //    mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                                    //    mailString += "<td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                                    //    mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                                    //    mailData.UserName = iId.UserName;
                                }
                                if (count > 0)
                                {
                                    this.Master.SuccessMessage = "Data uploaded successfully.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                                }
                                //BindAllInvoices();
                                //var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                                //var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                                //var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                                //PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
                            }

                        }

                        else
                        {
                            this.Master.WarningMessage = "Please Freeze the invoice before uploading.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        }

                    }
                    else if (item.DataItemIndex == 0)
                    {
                        this.Master.WarningMessage = "Please select the checkbox First.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    }

                }

                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);



            }
            //SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));

                //BindAllInvoices();
            //ankita work on alert Message


            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
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
                string mailBody = message.GetMessage(EnumConstants.Message.UploadInvoice, replaceItem);
                msg.Body = mailBody;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "Uploaded Invoices.";
                email.Send(msg);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        GST_TRN_INVOICE GTI = new GST_TRN_INVOICE();
        // dynamic invoice;
        protected void lkb_Click(object sender, EventArgs e)
        {
            try
            {
                gvInvoice_Items.Visible = true;
                EditInvoiceSection.Visible = true;
                LinkButton lkbItem = (LinkButton)sender;

                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    Int64 invoiceId = Convert.ToInt64(lkbItem.CommandArgument.ToString());
                    var freshInvoice = (byte)EnumConstants.InvoiceStatus.Fresh;
                    Invoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceId && f.InvoiceStatus == freshInvoice);
                    //Invoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceId);
                    BindInvoice(Invoice);
                    gvInvoice_Items.Focus();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindInvoice(GST_TRN_INVOICE invoice)
        {
            //List<GST_TRN_INVOICE> listInvoice = new List<GST_TRN_INVOICE>();
            //listInvoice.Add(invoice);

            // fvInvoice.DataSource = listInvoice;
            //fvInvoice.DataBind();
            //GridView gv = (GridView)fvInvoice.FindControl("gvItems");
            gvInvoice_Items.DataSource = invoice.GST_TRN_INVOICE_DATA;
            gvInvoice_Items.DataBind();
            lkbUpdateInvoice.Visible = true;
        }

        private GST_TRN_INVOICE _invoice;
        public GST_TRN_INVOICE Invoice
        {
            get
            {
                _invoice = (GST_TRN_INVOICE)Session["Invoice"];
                return _invoice != null ? _invoice : (new GST_TRN_INVOICE());
            }
            set
            {
                Session["Invoice"] = value;
            }
        }
        public event EventHandler UpdateInvoiceClick;
        protected void lkbUpdateInvoice_Click(object sender, EventArgs e)
        {
            var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
            var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
            var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
            try
            {
                // GridView gv = (GridView)fvInvoice.FindControl("gvItems");
                List<GST_TRN_INVOICE_DATA> items = GetGVData();

                ////var it = Invoice;
                if (Invoice != null)
                {
                    //    var tempQty = items
                    //Seller seller = new Seller();

                    foreach (GridViewRow row in gvInvoice_Items.Rows)
                    {
                        Label txtTotal = (Label)row.FindControl("txtTotal");
                        if (!string.IsNullOrEmpty(txtTotal.Text))
                        {
                            Decimal? tempQty = Convert.ToDecimal(txtTotal.Text.Trim());

                            if (Invoice.InvoiceSpecialCondition == (byte)(EnumConstants.InvoiceSpecialCondition.B2CL) && tempQty <= 250000)
                            {

                                this.Master.WarningMessage = "Total amount should be greater than 250000.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);

                                return;
                            }
                            else if (Invoice.InvoiceSpecialCondition == (byte)(EnumConstants.InvoiceSpecialCondition.B2CS) && tempQty >= 250000)
                            {
                                this.Master.WarningMessage = "Total amount should be less than 250000.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);

                                return;
                            }
                        }
                    }
                    GST_TRN_INVOICE inv = new GST_TRN_INVOICE();
                    inv.InvoiceDate = DateTime.Now;
                    inv.InvoiceMonth = Convert.ToByte(DateTime.Now.Month);
                    // var CurrentSrlNo = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == Invoice.AspNetUser.Id).Count() + 1;

                    if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Advance ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport ||
                        Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import)
                    {
                        inv.InvoiceNo = Invoice.InvoiceNo;
                    }
                    else
                    {
                        inv.InvoiceNo = cls_Invoice.GetInvoiveNoWithPreFix(UniqueNoGenerate.RandomValue(), (EnumConstants.InvoiceSpecialCondition)Invoice.InvoiceSpecialCondition);
                    }//InvoiceOperation.InvoiceNo(Invoice.AspNetUser, Invoice.FinYear_ID.ToString(), CurrentSrlNo.ToString());  

                    //if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export ||
                    //     Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges ||
                    //     Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import)
                    //{
                    //    inv.InvoiceUserID = Invoice.ReceiverUserID;
                    //}
                    //inv.InvoiceUserID != null ?Invoice.InvoiceUserID:null;

                    inv.InvoiceUserID = Invoice.InvoiceUserID;
                    inv.SellerUserID = Invoice.SellerUserID;
                    inv.ReceiverUserID = Invoice.ReceiverUserID;
                    inv.ConsigneeUserID = Invoice.ConsigneeUserID;
                    inv.OrderDate = Invoice.OrderDate;
                    inv.VendorID = Invoice.VendorID;
                    inv.TransShipment_ID = Invoice.TransShipment_ID;
                    inv.Freight = Invoice.Freight;
                    inv.Insurance = Invoice.Insurance;
                    inv.PackingAndForwadingCharges = Invoice.PackingAndForwadingCharges;
                    inv.ElectronicReferenceNo = Invoice.ElectronicReferenceNo;
                    inv.ElectronicReferenceNoDate = Invoice.ElectronicReferenceNoDate;
                    inv.InvoiceType = Invoice.InvoiceType;
                    inv.FinYear_ID = Invoice.FinYear_ID;
                    inv.IsInter = Invoice.IsInter;
                    inv.ReceiverFinYear_ID = Invoice.ReceiverFinYear_ID;
                    inv.ParentInvoiceID = Invoice.InvoiceID;
                    if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export)
                    {
                        inv.TaxBenefitingState = null;
                    }
                    else
                    {
                        inv.TaxBenefitingState = Invoice.AspNetUser2.StateCode;
                    }
                    inv.Status = true;
                    //  var invoicePeriod=unitOfWork.FinYearRepository.Find(f=>f.Fin_ID== Invoice.FinYear_ID).Finyear_Format;

                    //  GST_TRN_INVOICE updateInvoice = new GST_TRN_INVOICE();

                    inv.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    inv.InvoiceSpecialCondition = Invoice.InvoiceSpecialCondition;
                    inv.CreatedDate = DateTime.Now;
                    inv.UpdatedDate = DateTime.Now;
                    inv.CreatedBy = Common.LoggedInUserID();
                    inv.UpdatedBy = Common.LoggedInUserID();
                    dynamic invoiceCreate = null;

                    //Update old invoice status that is A or M---------------Start-------------
                    if (Invoice.InvoiceMonth == (byte)DateTime.Now.Month)
                    {

                        var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                        if (oldInvoice.GetHashCode() != inv.GetHashCode())
                        {
                            oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Amended);
                            // Invoice.InvoiceID = updateInvoice.InvoiceID;
                            var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                            unitOfWork.Save();
                            //
                            invoiceCreate = unitOfWork.InvoiceRepository.Create(inv);
                            unitOfWork.Save();
                        }
                    }
                    else
                    {
                        var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                        if (oldInvoice.GetHashCode() != inv.GetHashCode())
                        {
                            oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Modified);
                            //  updateInvoice.InvoiceID = updateInvoice.InvoiceID;
                            var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                            unitOfWork.Save();
                            //
                            invoiceCreate = unitOfWork.InvoiceRepository.Create(inv);
                            unitOfWork.Save();
                        }
                    }
                    //-------------End--------
                    // bool isInter =InvoiceOperation.GetConsumptionDestinationOfGoodsOrServices(Invoice.AspNetUser.StateCode, Invoice.AspNetUser2.StateCode, Invoice.AspNetUser1.StateCode);
                    bool isInter = Invoice.IsInter.Value;
                    //amits
                    if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export)
                    {
                        bool isStateExampted = false;
                    }
                    else
                    {
                        bool isStateExampted = unitOfWork.StateRepository.Find(f => f.StateCode == Invoice.AspNetUser2.StateCode).IsExempted.Value;
                    }
                    //  bool isExport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport);
                    bool isExported = false;
                    if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export || (byte)Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport)
                    {
                        isExported = true;
                    }

                    bool isJobwork = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork);
                    bool isImport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import);
                    var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == Invoice.AspNetUser.StateCode);
                    var isUTState = stateData.UT.Value;
                    var isExempted = stateData.IsExempted.Value;
                    var isEcom = false;
                    var isUn = false;
                    var invLineItem = from invo in items
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          InvoiceID = invoiceCreate.InvoiceID,
                                          LineID = invo.LineID,
                                          // GST_MST_ITEM = invo.Item,
                                          Item_ID = invo.GST_MST_ITEM.Item_ID,
                                          Qty = invo.Qty,
                                          Rate = invo.Rate,
                                          TotalAmount = invo.TotalAmount,
                                          Discount = invo.Discount,
                                          TaxableAmount = invo.TaxableAmount,
                                          IGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value), //isJobwork ? 0 : (isUTState ? 0 : (isInter ? invo.Item.IGST : (isExport ? invo.Item.IGST : (isImport ? invo.Item.IGST : 0)))),
                                          IGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value),// isJobwork ? 0 : (isUTState ? 0 : (isInter ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : 0)))),
                                          CGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value),
                                          CGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value),
                                          SGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value),
                                          SGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value),
                                          UGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value),
                                          UGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value),
                                          CessRate = isJobwork ? 0 : invo.GST_MST_ITEM.CESS,
                                          CreatedDate = DateTime.Now,

                                          //IGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? invo.GST_MST_ITEM.IGST : (isImport ? invo.GST_MST_ITEM.IGST : 0)))),
                                          //IGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value) : 0)))),
                                          //CGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : invo.GST_MST_ITEM.CGST) : 0)),
                                          //CGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value)) : 0)),
                                          //SGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : invo.GST_MST_ITEM.SGST))),
                                          //SGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value)))),
                                          //UGSTRate = isJobwork ? 0 : (isExport ? 0 : (isUTState ? invo.GST_MST_ITEM.UGST.Value : 0)),
                                          //UGSTAmt = isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value) : 0)),
                                          //CessRate = isJobwork ? 0 : invo.GST_MST_ITEM.CESS,
                                          CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CESS.Value)
                                          //TotalAmountWithTax = invo.TaxableAmount + IGSTAmt,
                                      };


                    foreach (GST_TRN_INVOICE_DATA item in invLineItem)
                    {
                        item.TotalAmountWithTax = item.TaxableAmount + item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;

                        unitOfWork.InvoiceDataRepository.Create(item);
                    }
                    unitOfWork.Save();

                    //START SAVE ITC 
                    cls_ITC itc = new cls_ITC();
                    itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.TaxInvoice;
                    if (invoiceCreate != null)
                        itc.SaveItc(invoiceCreate);
                    //END
                    //Sale Register
                    cls_PurchaseRegister purchaseRegister = new cls_PurchaseRegister();
                    if (inv.InvoiceSpecialCondition == (byte)(EnumConstants.InvoiceSpecialCondition.Advance))
                    {
                        //No data will be save in sale and purchase.
                    }
                    else if (inv.InvoiceSpecialCondition != (byte)(EnumConstants.InvoiceSpecialCondition.Import))  //enumm
                    {
                        purchaseRegister.SaleRegister(invoiceCreate);
                    }
                    else
                    {
                        purchaseRegister.UpdatePurchaseDataItemFromInvoice(invoiceCreate);
                    }
                    //END
                    gvInvoice_Items.DataSource = null;
                    gvInvoice_Items.DataBind();
                    lkbUpdateInvoice.Visible = false;
                    //    Invoice = new GST_TRN_INVOICE();
                    this.Master.SuccessMessage = "Data updated successfully.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //  UpdateInvoiceClick(sender, e);
                    PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
                    //BindAllInvoices();
                }
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
                //BindAllInvoices();
            }

            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
        }

        public bool IsEditable(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            return unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);

        }

        public bool IsValidEditable(string invoiceID)
        {
            int InvoiceID = Convert.ToInt32(invoiceID);
            bool isEditable = false;

            var invoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == InvoiceID); //.Contains(c => c.ParentInvoiceID == InvoicID);

            if (invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Advance ||
                invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork)
            {
                isEditable = false;
            }

            return isEditable;
        }

        public bool IsDataFreezed(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            return unitOfWork.InvoiceAuditTrailRepositry.Contains(c => c.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.DataFreez && c.InvoiceID == InvoicID);
        }

        public string GetInvoiceType(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            var dfItem = (byte)EnumConstants.InvoiceAuditTrailSatus.DataFreez;
            var uploadItem = (byte)EnumConstants.InvoiceAuditTrailSatus.Upload;
            //run
            var invoice = unitOfWork.InvoiceAuditTrailRepositry.Filter(c => (c.AuditTrailStatus == dfItem || c.AuditTrailStatus == uploadItem) && c.InvoiceID == InvoicID).Select(s => s.AuditTrailStatus).ToList();
            string auditTrailSatus;
            if (invoice.Contains(uploadItem))
            {
                auditTrailSatus = "<span class='label label-success'>" + EnumConstants.InvoiceAuditTrailSatus.Upload.ToDescription() + "</span>";
            }
            else if (invoice.Contains(dfItem))
            {
                auditTrailSatus = "<span class='label label-warning'>" + EnumConstants.InvoiceAuditTrailSatus.DataFreez.ToDescription() + "</span>";
            }

            else
            {
                auditTrailSatus = "<span class='label label-danger'>Pending</span>";
            }


            return auditTrailSatus;

        }
        public bool IsUpload(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            return unitOfWork.InvoiceAuditTrailRepositry.Contains(c => c.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.Upload && c.InvoiceID == InvoicID);
        }
        private List<GST_TRN_INVOICE_DATA> GetGVData()
        {
            List<GST_TRN_INVOICE_DATA> lineCollection = new List<GST_TRN_INVOICE_DATA>();
            foreach (GridViewRow row in gvInvoice_Items.Rows)
            {
                try
                {
                    //  GridView gvSizePrice = (GridView)fvProduct.FindControl("gdvSizePrice");
                    TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                    TextBox txtGoodService = (TextBox)row.FindControl("txtGoodService");
                    TextBox txtQty = (TextBox)row.FindControl("txtQty");
                    TextBox txtRate = (TextBox)row.FindControl("txtRate");
                    Label txtTotal = (Label)row.FindControl("txtTotal");
                    TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                    Label txtTaxableValue = (Label)row.FindControl("txtTaxableValue");

                    if (!string.IsNullOrEmpty(txtGoodService.Text.Trim()))
                    {
                        GST_TRN_INVOICE_DATA le = new GST_TRN_INVOICE_DATA();
                        le.LineID = row.RowIndex;
                        le.Qty = Convert.ToDecimal(txtQty.Text.Trim());
                        le.GST_MST_ITEM = unitOfWork.ItemRepository.Find(f => f.ItemCode == txtItemCode.Text.Trim());
                        le.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                        le.TotalAmount = Convert.ToDecimal(txtTotal.Text.Trim());
                        if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                            le.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                        //le.AmountWithTaxLineIDWise = Convert.ToDecimal(((Label)row.FindControl("txtTaxableValue")).Text.Trim());
                        le.TaxableAmount = Convert.ToDecimal(txtTaxableValue.Text.Trim());
                        // Grand total of all line items
                        // le.TotalAmount += le.TotalAmount;
                        // grand total of all line items with tax
                        /// le.TotalAmountWithTax += le.TotalAmountWithTax;

                        lineCollection.Add(le);
                    }
                }
                catch (Exception ex)
                {
                    cls_ErrorLog ob = new cls_ErrorLog();
                    cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                }
            }
            return lineCollection;
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            int rowIndex = currentRow.RowIndex;
            TextBox textBox = (sender as TextBox);

            var itemData = unitOfWork.ItemRepository.Find(f => f.ItemCode == textBox.Text.Trim());//seller.GetItemInformation(textBox.Text.Trim());
            int result;

            if (itemData != null)
            {
                #region Code is working fine for HSN search , but now need to get the logic of Purchase register
                if (int.TryParse(textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) && !string.IsNullOrEmpty(textBox.Text.Trim()) && textBox.Text.Length == 8)
                {
                    try
                    {
                        string type = string.Empty;
                        // added to check whether new HSN IS NOTIFIED OR NOT
                        if (itemData.IsNotified.Value)
                        {
                            //BindNotifiedHSN(itemData.GST_MST_ITEM_NOTIFIED, lvHSNData);
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalhsn", "$('#myModalhsn').modal();", true);
                            //upModal.Update();
                        }
                        if (itemData != null)
                        {
                            TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                            TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                            txtDescription.Text = itemData.Description;
                            txtUnit.Text = itemData.Unit;
                        }
                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");

                    }
                    catch (Exception ex)
                    {
                        cls_ErrorLog ob = new cls_ErrorLog();
                        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                    }

                }

            }
            else
            {
                textBox.Text = string.Empty;
                TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");
                Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");
                TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
                Label txtTotal = (Label)currentRow.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");


                txtDescription.Text = string.Empty;
                txtUnit.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtTaxableValue.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtTotal.Text = string.Empty;
                //TODO:DISPLAY MESSAGE thet item does not exist.
            }

                #endregion

        }


        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            // reassigning the value of seller
            //if (Session["seller"] != null)
            //    seller = (Seller)Session["seller"];

            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");

            // getting the text box id
            string HSHtxtID = txtQty.ID;

            int rowIndex = currentRow.RowIndex;
            TextBox txtHsn = (TextBox)currentRow.FindControl("txtItemCode");
            TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
            Label txtTotal = (Label)currentRow.FindControl("txtTotal");
            TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
            Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");
            try
            {
                if ((txtQty.Text.ToString() != ""))
                {
                    // decimal? purchaseLedger = seller.GetSellerStockInventory(txtHsn.Text.Trim(), sellerProfile.Id.ToString());

                    //if (purchaseLedger.HasValue)
                    //{
                    #region to check qty entered is there in saleRegister or not
                    //if (purchaseLedger.Value >= Convert.ToInt32(txtQty.Text.Trim()))
                    //{
                    //    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                    //    if ((txtRate.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate.Text.ToString())) && (Convert.ToDecimal(txtRate.Text.ToString()) < Decimal.MaxValue))
                    //    {
                    // caluculate total 
                    decimal totalRate = Common.CalculateTotal(Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToDecimal(txtRate.Text.Trim()));
                    txtTotal.Text = totalRate.ToString();
                    if (totalRate < Decimal.MaxValue)
                    {
                        txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, !string.IsNullOrEmpty(txtDiscount.Text) ? Convert.ToDecimal(txtDiscount.Text) : 0).ToString();

                        if (!string.IsNullOrEmpty(txtDiscount.Text))
                        {
                            // Calculating the tax value, 
                            // for that discount given should be there , if not then else part will b called
                            if (Convert.ToDecimal(txtDiscount.Text) > 0.0m)
                            {  // tax value , unit * rate per unit * tax applicable
                                txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, Convert.ToDecimal(txtDiscount.Text)).ToString();
                            }
                            else
                            // get the focus to txt rate column with a message 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                // Due to abnormal terminationn of 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter Discount";
                                txtRate.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    txtTotal.Text = string.Empty;
                    txtTaxableValue.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            //}
            //else
            //{
            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //    //TODO:I NEED TO APPY MODEL POP UP HERE TO UPDATE PURCHASE REGISTER
            //}
                #endregion
            //}
            //else
            //{

            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //}
            //}

        }

        protected void lkbJson_Click(object sender, EventArgs e)
        {
            try
            {
                cls_Invoice_JSON jsonData = new cls_Invoice_JSON();
                List<GST_TRN_INVOICE> invoicelist = new List<GST_TRN_INVOICE>();
                bool check = false;
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    Int64 invoiceID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");



                    if (chk.Checked)
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();
                        var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                        invoicelist.Add(invoiceDetail);
                        check = true;
                    }
                }
                if (check)
                {
                    string text = jsonData.GetInvoiceJSONData(invoicelist);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Length", text.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("content-disposition", "attachment;filename=\"data.json\"");
                    Response.Write(text);
                    Response.End();
                }
                else
                {
                    this.Master.WarningMessage = "Please select atleast one checkbox first";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbdownload = (LinkButton)sender;
                Int64 invoiceId = Convert.ToInt64(lkbdownload.CommandArgument.ToString());
                var invoice = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceID == invoiceId).FirstOrDefault();

                string InvoiceNo = invoice.InvoiceNo;
                string InvoiceId = Convert.ToString(invoice.InvoiceID);
                ReportGenerate.DownloadPDFSERVER(InvoiceId);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbMail_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbmail = (LinkButton)sender;
                string userid = Convert.ToString(lkbmail.CommandArgument.ToString());
                var existUser = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == userid).FirstOrDefault();
                string inviceid = Convert.ToString(existUser.InvoiceID);
                ReportGenerate.DownloadPDFSERVERTaxInvoice(inviceid);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lvInvoices_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);//Ankita
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
                DataPager1.DataBind();
                //Session["pageno"] = e.MaximumRows;//Ankita 
                lvInvoices.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            try
            {
                lvInvoices.DataSource = null;
                lvInvoices.DataBind();
                divMain.Visible = true;
                //EditInvoiceSection.Visible = false;
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var loggedinUser=Common.LoggedInUserID();
                
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                if (divMain.Visible == true)
                {
                    lkbBack.Visible = false;//Ankita
                    DataPager1.SetPageProperties(0, 10, false);

                    PopulateTileViewInvoices(null, null, Month);
                    //DataPager pgr = lvInvoices.FindControl("DataPager1") as DataPager;
                    //if (pgr != null)
                    //{
                    //    pgr.SetPageProperties(0, pgr.MaximumRows, false);
                    //}
                    //lvInvoices.SelectedIndex = -1;
                    //if (Session["pageno"] != null) /// set datapager to page 2
                    //    DataPager1.SetPageProperties(Convert.ToInt32(Session["pageno"]), 10, false); //Ankita
                }
                else
                {
                    PopulateTileViewInvoices(invoiceType, InvoiceSplCondn, Month);
                }
                if(Common.IsTaxConsultant())
                {
                    uc_GSTNUsers.Visible = true;
                }
                else
                {
                    uc_GSTNUsers.Visible=false;
                }

                gvInvoice_Items.Visible = false;
                divViewTypeInvoices.Visible = false;
                divInvoiceReturn.Visible = true;
                uc_invoiceMonth.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbFreez_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the IP
                // string myIP = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();// Dns.GetHostEntry(hostName).AddressList[0].ToString();
                int count = 0;
                List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                List<string> mailsToList = new List<string>();
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    Int64 invoiceID = Convert.ToInt64(lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceID"].ToString());
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");

                    if (chk.Checked)//|| audittrail.AuditTrailStatus == Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.DataFreez))
                    {
                        clsMessageAttribute attribute = new clsMessageAttribute();
                        var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                        var audittrailinvoice = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.AuditTrailStatus == (Byte)8).Where(w => w.InvoiceID == invoiceDetail.InvoiceID).ToList();


                        if (audittrailinvoice.Count == 0)
                        {
                            audittrail.InvoiceID = invoiceID;
                            int invoiceid = Convert.ToInt32(audittrail.InvoiceID);
                            audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.DataFreez);
                            audittrail.UserIP = Common.IP;
                            audittrail.InvoiceID = invoiceid;
                            audittrail.CreatedDate = DateTime.Now;
                            audittrail.CreatedBy = Common.LoggedInUserID();
                            audittrail.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.NA;
                            unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                            unitOfWork.Save();
                            if (!mailsToList.Contains(invoiceDetail.AspNetUser.Email))
                            {
                                mailsToList.Add(invoiceDetail.AspNetUser.Email);
                            }
                            attribute.UserName = invoiceDetail.AspNetUser.OrganizationName;
                            //attribute.MailsTo.Add();
                            attribute.InvoiceNo = lvInvoices.DataKeys[item.DisplayIndex].Values["InvoiceNo"].ToString();
                            attribute.InvoiceDate = DateTimeAgo.GetFormatDate(invoiceDetail.InvoiceDate);
                            attribute.InvoiceTotalAmount = invoiceDetail.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                            invAttributes.Add(attribute);

                            count = count + 1;
                            if (count > 0)
                            {
                                string mailString = string.Empty;
                                string sellerMail = string.Empty;
                                clsMessageAttribute mailData = new clsMessageAttribute();


                                foreach (clsMessageAttribute iId in invAttributes)
                                {
                                    //    mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                                    //    mailString += "<td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                                    //    mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                                    //    mailData.UserName = iId.UserName;
                                }
                                if (count > 0)
                                {
                                    this.Master.SuccessMessage = "Data Freezed successfully.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                                    //LinkButton lkbButton = new LinkButton.FindControl("lkb");
                                    //lkbButton.Visible = false;
                                }
                            }
                        }


                            else
                            {
                                this.Master.WarningMessage = "Invoice Already Freezed.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                            }
                        
                    }

                    else if (item.DataItemIndex == 0)
                    {
                        this.Master.WarningMessage = "Please select the checkbox First.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    }


                }


                //if (count > 0)
                //{


                //string mailString = string.Empty;
                //string sellerMail = string.Empty;
                //clsMessageAttribute mailData = new clsMessageAttribute();


                foreach (clsMessageAttribute iId in invAttributes)
                {
                    //    mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                    //    mailString += "<td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                    //    mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                    //    mailData.UserName = iId.UserName;
                }

                //BindAllInvoices();
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);

                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);

                // }
            }

            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            try
            {
                var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var InvoiceSplCondn = Convert.ToInt16(ViewState["CommandName"]);
                var invoiceType = Convert.ToInt16(ViewState["invoiceType"]);
                PopulateInvoiceTypeInvoices(invoiceType, InvoiceSplCondn, Month);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}