using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using GST.Utility;
using BusinessLogic.Repositories;
using DataAccessLayer;

namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR1Details : System.Web.UI.Page
    {
        cls_Invoice _invoice = new cls_Invoice();
        UnitOfWork unitofwork = new UnitOfWork();
        int displayIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            //lkbSubmit.Enabled = false;
            var userProfile = GetUserUserProfile;
            var SellerUserId = GetSellerProfile;
            lblGSTIN.Text = userProfile.GSTNNo;
            lbllegal.Text = userProfile.OrganizationName;
            lblTrade.Text = userProfile.OrganizationName;
            lblFinYear.Text = DateTime.Now.Year.ToString();
            lblmonth.Text = uc_invoiceMonth.GetValue;//Convert.ToString(DateTime.Now.Month - 1);
            BindSignatory();


            //BindMonth();


            uc_Gstr1_Details_Tileview.Info_Click += uc_Gstr1_Details_Tileview_Info_Click;
            uc_invoiceMonth.SelectedIndexChange += uc_invoiceMonth_SelectedIndexChange;
        }

        private void uc_invoiceMonth_SelectedIndexChange(object sender, EventArgs e)
        {
            var month = uc_invoiceMonth.GetValue;

        }

        private void BindSignatory()
        {
            var userId = Common.LoggedInUserID();
            ddlSinatory.DataSource = unitofwork.UserSignatoryRepository.Filter(f => f.Id == userId).OrderBy(o => o.SignatoryName).ToList();
            ddlSinatory.DataTextField = "SignatoryName";
            ddlSinatory.DataValueField = "Signatory_Id";
            ddlSinatory.DataBind();
            ddlSinatory.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        protected void lkbOtpVerify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOtp.Text))
            {
                var data = BusinessLogic.GSTNAPI.GSTNAPI_Data.VerifyOTP("33GSPTN0231G1ZM", "balaji.tn.1", txtOtp.Text);
                if (data.HttpStatusCode == 200)
                {
                    //btnUpload.Visible = true;
                    uc_sucess.SuccessMessage = "OTP Accepted";
                    uc_sucess.Visible = true;
                    //lkbSubmit.Visible = true;
                    //lkbSubmit.Enabled = true;
                }
                else
                    uc_sucess.ErrorMessage = "Please Enter Correct OTP";
                uc_sucess.Visible = true;
            }
            else
                uc_sucess.ErrorMessage = "Please Enter Correct OTP";
            uc_sucess.Visible = true;
        }
        //Get seller User id start
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationUser GetUserUserProfile
        {
            get
            {

                var loggedinUser = Common.LoggedInUserID();

                var profile = UserManager.Users.Where(w => w.Id == loggedinUser).FirstOrDefault();
                return profile;
            }

        }
        public string GetSellerProfile
        {
            get
            {


                return HttpContext.Current.User.Identity.GetUserId();
            }
            set
            {
                //return HttpContext.Current.User.Identity.GetUserId();
            }
        }
        //End


        private void BindListView(string SellerUserId, int month)
        {
            GetFile_GSTR_4A(SellerUserId, month);
            GetFile_GSTR1_5A_5B(SellerUserId, month);
            //GetFile_GSTR1_9B_CRDR_REG(SellerUserId, month);
            //GetFile_GSTR1_9B_CRDR_UNREG(SellerUserId, month);
            //GetFile_GSTR1_6A_Export(SellerUserId, month);
            //GetFile_GSTR1_8_NILRATED(SellerUserId, month);
            //GetGSTR1_FileReturn_11A1A2_TAXLIABILITY(SellerUserId, month);
            //GetGSTR1_FileReturn_11B1_11B2_Advances(SellerUserId, month);
            //GetGSTR1_FileReturn_9A_AMDB2B(SellerUserId, month);
            //GetGSTR1_FileReturn_9A_AMD_B2CL(SellerUserId, month);
            //GetGSTR1_FileReturn_9C_AME_CRDRREG(SellerUserId, month);
            //GetGSTR1_FileReturn_9C_AMd_CRDRUNREG(SellerUserId, month);
            //GetGSTR1_FileReturn_9A_AMD_EXP(SellerUserId, month);
            //GetGSTR1_FileReturn__11A_AMD(SellerUserId, month);
            //GetGSTR1_FileReturn___11B_Adv_Advance(SellerUserId, month);
            //GetFile_GSTR1_7(SellerUserId, month);


        }
        private void uc_Gstr1_Details_Tileview_Info_Click(object sender, EventArgs e)
        {
            var month = uc_invoiceMonth.GetIndexValue;
            LinkButton lkb = (LinkButton)sender;
            string a = Convert.ToString(lkb.CommandArgument);
            if (a == "4A, 4B, 4C, 6B, 6C (B2B) Invoices")
            {
                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR_4A(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "5A, 5B-B2CL")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_5A_5B(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "6A Export Invoices")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_6A_Export(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "8 Nil Exempted Non GST")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_8_NILRATED(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9A Amended B2B Invoices")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_9A_AMDB2B(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9A Amended B2CL Invoices")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_9A_AMD_B2CL(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9A Amended Export Invoices")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_9A_AMD_EXP(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "7 B2C Others")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_7(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9B Registered CR/DR Note")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_9B_CRDR_REG(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9B Unregistered CR/DR Note")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetFile_GSTR1_9B_CRDR_UNREG(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "9C-Amended CR/DR Notes(Unregistered)")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_9C_AMd_CRDRUNREG(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "11A Amended Tax Liability Advance")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn__11A_AMD(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "11A(1),11A(2) Tax Liability(Adv Received)")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_11A1A2_TAXLIABILITY(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "11B Amendment Adjustment Of Adv")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn___11B_Adv_Advance(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else if (a == "11B(1),11B(2) Adjustment Of Adv")
            {

                var loggedinUser = Common.LoggedInUserID();
                GetGSTR1_FileReturn_11B1_11B2_Advances(loggedinUser, month);
                viewdetails.Visible = true;
            }
            else
            {
                var loggedinUser = Common.LoggedInUserID();
                viewdetails.Visible = true;
            }
            //viewdetails.Visible = true;
            GstrdetailsTileview.Visible = false;
        }
        //View List view
        public void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }

        /// <summary>
        /// Get Return Methods using SP Amits/Ankita
        /// </summary>
        /// <param name="SellerUserId"></param>
        /// <param name="month"></param>

        public void GetFile_GSTR_4A(string SellerUserId, int month)
        {
            try
            {

                BindListView(lvInvoices, _invoice.GetGSTR1_FileReturn_4(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_5A_5B(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_5, _invoice.GetGSTR1_FileReturn_5(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_9B_CRDR_REG(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_9B_CRDR_REG, _invoice.GetGSTR1_FileReturn_9B_CRDR_REG(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_9B_CRDR_UNREG(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_crunreg, _invoice.GetGSTR1_FileReturn_9B_CRDRUNREG(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_6A_Export(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_6AExport, _invoice.GetGSTR1_FileReturn_6(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_8_NILRATED(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_nill, _invoice.GetGSTR1_FileReturn_8(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_11A1A2_TAXLIABILITY(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_11Ataxlability, _invoice.GetGSTR1_FileReturn_11A1A2(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_11B1_11B2_Advances(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_11Badvance, _invoice.GetGSTR1_FileReturn_11B1_11B2(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_9A_AMDB2B(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_9A_AMDB2B, _invoice.GetGSTR1_FileReturn_9A_AMDB2B(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_9C_AME_CRDRREG(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_9C_ame_crdrreg, _invoice.GetGSTR1_FileReturn_9C_AME_CRDRREG(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_9C_AMd_CRDRUNREG(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_9C_uncrdr, _invoice.GetGSTR1_FileReturn_9C_AMd_CRDRUNREG(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_9A_AMD_EXP(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_amd_exp, _invoice.GetGSTR1_FileReturn_9A_AMD_EXP(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn_9A_AMD_B2CL(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_amd_b2cl, _invoice.GetGSTR1_FileReturn_9A_AMD_B2CL(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn__11A_AMD(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_11A_AMD, _invoice.GetGSTR1_FileReturn_11A_AMD(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetGSTR1_FileReturn___11B_Adv_Advance(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_11B_Advance, _invoice.GetGSTR1_FileReturn_11B(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void GetFile_GSTR1_7(string SellerUserId, int month)
        {
            try
            {

                BindListView(lv_gstr1_7, _invoice.GetGSTR1_FileReturn_7(SellerUserId, month));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        /// <summary>
        /// amit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbInvBack_Click(object sender, EventArgs e)
        {
            GstrdetailsTileview.Visible = true;
            viewdetails.Visible = false;
        }
        protected void lkbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ureturn/ReturnGstr1.aspx");
        }
        protected void lkbFile_Click(object sender, EventArgs e)
        {


            //string gstin = "33GSPTN0231G1ZM", userid = "balaji.tn.1";
            //BusinessLogic.GSTNAPI.GSTNAPI_Data.GetOTP(gstin, userid);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#modal1').modal('show');</script>", false);
            BindSignatory();
        }
        protected void lkbEVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkreturn.Checked)
                {
                    string gstin = "33GSPTN0231G1ZM", userid = "balaji.tn.1";
                    BusinessLogic.GSTNAPI.GSTNAPI_Data.GetOTP(gstin, userid);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#modal1').modal('show');</script>", false);

                }
                else
                {
                    uc_sucess.ErrorMessage = "Select Checkbox First";
                }
            }
            catch (Exception ex)
            {

            }
        }
        GST_TRN_RETURN_STATUS Returnstatus = new GST_TRN_RETURN_STATUS();
        int count = 0;
        /// <summary>
        /// amit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbSubmit_Click(object sender, EventArgs e)
        {
            var userid = Common.LoggedInUserID();
            var Finyear = Session["Year"];
            var month = Session["Month"];
            var months = Convert.ToByte(month);
            var status = (byte)EnumConstants.InvoiceAuditTrailSatus.DataFreez;
            var invoicesSubmit = unitofwork.InvoiceAuditTrailRepositry.Filter(f => f.AuditTrailStatus == status && f.GST_TRN_INVOICE.InvoiceMonth == months && f.GST_TRN_INVOICE.InvoiceUserID == userid).ToList();

            List<string> invID = new List<string>();
            Returnstatus.Period = Convert.ToInt16(month);
            Returnstatus.FinYear_ID = Convert.ToInt16(Finyear);
            Returnstatus.Signatory_Id = null;
            Returnstatus.User_id = userid;
            Returnstatus.ReturnStatus = Convert.ToByte(EnumConstants.Return.Gstr1);
            // var a = unitofwork.ReturnStatusRepository.Filter(f => f.Status == 1).ToList();
            Returnstatus.Action = Convert.ToByte(EnumConstants.ReturnFileStatus.Submit);
            Returnstatus.Status = 1;
            Returnstatus.NoofInvoices = invoicesSubmit.Count();
            Returnstatus.CreatedDate = DateTime.Now;
            Returnstatus.CreatedBy = userid;
            unitofwork.ReturnStatusRepository.Create(Returnstatus);
            unitofwork.Save();
        }

        protected void lkbFile_Click1(object sender, EventArgs e)
        {
            main.Visible = false;
            secondary.Visible = true;
            BindSignatory();
        }

        protected void lkbBackInv_Click(object sender, EventArgs e)
        {
            main.Visible = true;
            secondary.Visible = false;
        }

        protected void chkreturn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkreturn.Checked == true)
            {
                innersecondary.Visible = true;
            }
            if (chkreturn.Checked == false)
            {
                innersecondary.Visible = false;
            }
        }

        protected void lkbSave_Click(object sender, EventArgs e)
        {
            var userid = Common.LoggedInUserID();
            var Finyear = Session["Year"];
            var month = Session["Month"];
            var months = Convert.ToByte(month);
            var status = (byte)EnumConstants.InvoiceAuditTrailSatus.DataFreez;
            var invoicesSubmit = unitofwork.InvoiceAuditTrailRepositry.Filter(f => f.AuditTrailStatus == status && f.GST_TRN_INVOICE.InvoiceMonth == months && f.GST_TRN_INVOICE.InvoiceUserID == userid).ToList();

            List<string> invID = new List<string>();
            Returnstatus.Period = Convert.ToInt16(month);
            Returnstatus.FinYear_ID = Convert.ToInt16(Finyear);
            Returnstatus.Signatory_Id = null;
            Returnstatus.User_id = userid;
            Returnstatus.ReturnStatus = Convert.ToByte(EnumConstants.Return.Gstr1);
            // var a = unitofwork.ReturnStatusRepository.Filter(f => f.Status == 1).ToList();
            Returnstatus.Action = Convert.ToByte(EnumConstants.ReturnFileStatus.Save);
            Returnstatus.Status = 0;
            Returnstatus.NoofInvoices = invoicesSubmit.Count();
            Returnstatus.CreatedDate = DateTime.Now;
            Returnstatus.CreatedBy = userid;
            unitofwork.ReturnStatusRepository.Create(Returnstatus);
            unitofwork.Save();
        }


        protected void lkbEdit_Click(object sender, EventArgs e)
        {
            try
            {

               

                LinkButton lkbItem = (LinkButton)sender;

                ViewState["CommandArgument"] = lkbItem.CommandArgument.ToString();

                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    Int64 invoiceId = Convert.ToInt64(lkbItem.CommandArgument.ToString());
                    var invoice = unitofwork.InvoiceDataRepository.Filter(f => f.InvoiceID == invoiceId).ToList();
                    uc_FileReturn_Edit.Visible = true;
                }



            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }



        // //ankita gstr1 4a 

        //protected void lvInvoices_ItemEditing(object sender, ListViewEditEventArgs e)
        //{
        //    lvInvoices.EditIndex = e.NewEditIndex;
        //    Session["displayIndex"] = e.NewEditIndex;
        //    var SellerUserId = Common.LoggedInUserID();
        //    var month = uc_invoiceMonth.GetIndexValue;
        //    BindListView(SellerUserId, month);
        //    lvInvoices.Visible = true;


        //}

        //protected void lvInvoices_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        //{
        //    var SellerUserId = Common.LoggedInUserID();
        //    var month = uc_invoiceMonth.GetIndexValue;
        //    LinkButton lkbUpdate = (lvInvoices.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
        //    if (lkbUpdate.CommandName == "Update")
        //    {
        //        int invoiceid = Convert.ToInt32(lkbUpdate.CommandArgument);
        //        GST_TRN_INVOICE_DATA invoiceData = unitofwork.InvoiceDataRepository.Filter(x => x.InvoiceID == invoiceid).SingleOrDefault();
        //        if (invoiceData != null)
        //        {
        //            TextBox txtTotalValue = (lvInvoices.Items[e.ItemIndex].FindControl("txtTotalValue")) as TextBox;
        //            if (txtTotalValue.Text != null || txtTotalValue.Text != "")
        //            {
        //                invoiceData.TotalAmount = Convert.ToDecimal(txtTotalValue.Text.Trim());
        //            }


        //        }
        //        invoiceData.UpdatedBy = Common.LoggedInUserID();
        //        invoiceData.UpdatedDate = DateTime.Now;
        //        unitofwork.InvoiceDataRepository.Update(invoiceData);
        //        unitofwork.Save();
        //    }
        //    lvInvoices.EditIndex = -1;
        //    BindListView(SellerUserId, month);
        //}

        //protected void lv_gstr1_5_ItemEditing(object sender, ListViewEditEventArgs e)
        //{
        //    lv_gstr1_5.EditIndex = e.NewEditIndex;
        //    Session["displayIndex"] = e.NewEditIndex;
        //    var SellerUserId = Common.LoggedInUserID();
        //    var month = uc_invoiceMonth.GetIndexValue;
        //    BindListView(SellerUserId, month);
        //    lv_gstr1_5.Visible = true;

        //}

        //protected void lv_gstr1_5_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        //{
        //    var SellerUserId = Common.LoggedInUserID();
        //    var month = uc_invoiceMonth.GetIndexValue;
        //    LinkButton lkbUpdate = (lv_gstr1_5.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
        //    if (lkbUpdate.CommandName == "Update")
        //    {

        //        //amiterror
        //        int invoiceid = Convert.ToInt32(lkbUpdate.CommandArgument);
        //        //var lineid = Convert.ToInt32(lkbUpdate.CommandArgument);
        //        //GST_TRN_INVOICE_DATA invoiceData = unitofwork.InvoiceDataRepository.Filter(x => x.InvoiceID == invoiceid && x.LineID == lineid).SingleOrDefault();
        //        GST_TRN_INVOICE_DATA invoiceData = unitofwork.InvoiceDataRepository.Filter(x => x.InvoiceID == invoiceid).SingleOrDefault();
        //        if (invoiceData != null)
        //        {
        //            TextBox txtTotalValue = (lv_gstr1_5.Items[e.ItemIndex].FindControl("txtTotalValue")) as TextBox;
        //            if (txtTotalValue.Text != null || txtTotalValue.Text != "")
        //            {
        //                invoiceData.TotalAmount = Convert.ToDecimal(txtTotalValue.Text.Trim());
        //            }


        //        }
        //        invoiceData.UpdatedBy = Common.LoggedInUserID();
        //        invoiceData.UpdatedDate = DateTime.Now;
        //        unitofwork.InvoiceDataRepository.Update(invoiceData);
        //        unitofwork.Save();
        //    }
        //    lv_gstr1_5.EditIndex = -1;
        //    BindListView(SellerUserId, month);
    }

}
