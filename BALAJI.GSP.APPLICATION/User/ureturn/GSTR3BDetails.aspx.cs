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
    public partial class GSTR3BDetails : System.Web.UI.Page
    {
        UnitOfWork unitofwork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();
        protected void Page_Load(object sender, EventArgs e)
        {
            var userProfile = GetUserUserProfile;
            var SellerUserId = GetSellerProfile;
            //lblGSTIN.Text = userProfile.GSTNNo;
            //lbllegal.Text = userProfile.OrganizationName;
            //lblTrade.Text = userProfile.OrganizationName;
            //lblFinYear.Text = DateTime.Now.Year.ToString();
            //lblmonth.Text =Convert.ToString(uc_invoiceMonth.GetValue);//Convert.ToString(DateTime.Now.Month - 1);
             uc_invoiceMonth.SelectedIndexChange += uc_invoiceMonth_SelectedIndexChange;
             var loogedinUser=Common.LoggedInUserID();
            BindlistView(loogedinUser, 9,3);
           
            //var invoice = unitofwork.GetGSTR_3B_3_1(loogedinUser, 9);
            //lvGstr3B_1.DataSource = invoice.ToList(); 
            // lvGstr3B_1.DataBind();
        }

        private void uc_invoiceMonth_SelectedIndexChange(object sender, EventArgs e)
        {
            var month = uc_invoiceMonth.GetValue;

        }
        private void BindlistView(string SellerUserId, int month , int year)
        {
            var loogedinUser = Common.LoggedInUserID();
            GetFile_GSTR_3B_1(loogedinUser,9,3);
            //GetFile_GSTR_3B_2(loogedinUser,9);
            //GetFile_GSTR_3B_5(loogedinUser, 9);
               
        }
        public void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }
        public void GetFile_GSTR_3B_1(string SellerUserId, int month, byte year)
        {
            try
            {

                BindListView(lvGstr3B_1, _invoice.GetGSTR_3B_1(SellerUserId, month,year));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //public void GetFile_GSTR_3B_2(string SellerUserId, int month)
        //{
        //    try
        //    {

        //        BindListView(lv_Gstr3B_3_2, _invoice.GetGSTR_3B_2(SellerUserId, month));
        //    }
        //    catch (Exception ex)
        //    {
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
        //public void GetFile_GSTR_3B_5(string SellerUserId, int month)
        //{
        //    try
        //    {

        //        BindListView(lv_Gstr3B_5, _invoice.GetGSTR_3B_5(SellerUserId, month));
        //    }
        //    catch (Exception ex)
        //    {
        //        cls_ErrorLog ob = new cls_ErrorLog();
        //        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
        //    }
        //}
                
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
    }
}