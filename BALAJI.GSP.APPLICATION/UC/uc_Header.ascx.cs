using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //uc_GSTNUsers.SelectedIndexChange += uc_GSTNUsers_SelectedIndexChanged;
            if (!IsPostBack)
            {
                //if(Common.IsUser())
                //{
                //   // liUsers.Visible = false;
                //}

                if (Common.UserProfile.SmallImage != null)
                {

                    UserImage.ImageUrl = Common.GetImageFromByte(Common.UserProfile.SmallImage);
                    imgUser.ImageUrl = Common.GetImageFromByte(Common.UserProfile.SmallImage);
                    //UserImage.Visible = true;
                }
                else
                {
                    imgUser.ImageUrl = "~/dist/img/avatar5.png";
                    UserImage.ImageUrl = "~/dist/img/avatar5.png";
                }

            }
            ShowButton();
        }

        public void uc_GSTNUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.SellerUserID = Convert.ToString(uc_GSTNUsers.GetValue);
            AddMoreClick(sender, e);
        }
        public EventHandler AddMoreClick;

        public string SellerUserID
        {
            get
            {
                var mName = string.IsNullOrEmpty(ViewState["SellerUserID"].ToString()) ? "" : ViewState["SellerUserID"].ToString();
                return Convert.ToString(mName);
            }
            set
            {
                ViewState["SellerUserID"] = value;
            }
        }



        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            try
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                Response.Redirect("~/Account/Login");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void ShowButton()
        {
            if (Common.IsAdmin())
            {
                ligroup.Visible = true;
                liSubGroup.Visible = true;
                liClass.Visible = true;
                liSubClass.Visible = true;
                liState.Visible = true;
                liHSN.Visible = true;
                liSAC.Visible = true;
                liUser.Visible = true;
                liPerchaseRegister.Visible = true;
                liVendorRegister.Visible = true;
                //liUserMgmt.Visible = true;
                liReports.Visible = true;
                liMapUserBusiness.Visible = true;
                //logManagement.Visible = true;
            }
            else if (Common.IsTaxConsultant())
            {
                liPerchaseRegister.Visible = true;
                liVendorRegister.Visible = true;
                liUser.Visible = true;
                //liGSTNNO.Visible = true;
                 liUsers.Visible = true;
            }
            else
            {
                liPerchaseRegister.Visible = true;
                liVendorRegister.Visible = true;

            }
        }
    }
}