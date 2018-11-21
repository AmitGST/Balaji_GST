using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Security.Principal;
//using System.Web.Providers.Entities;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using BALAJI.GSP.APPLICATION.Infrastructure;
using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using BusinessLogic.Repositories;

namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_ChangePassword : System.Web.UI.UserControl
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpBrowserCapabilities httpBrowser = Request.Browser;

            bool enableJavascript = httpBrowser.JavaScript;
            //System.Web.UI.ScriptManager sm = new System.Web.UI.ScriptManager.GetCurrent(this.Page); 
        }
        protected string SuccessMessage
        {
            get;
            private set;
        }

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

        protected void  btnResetPwd_Click(object sender, EventArgs e)
        {
            try
            {
               var result = UserManager.ChangePassword(HttpContext.Current.User.Identity.GetUserId(), txtOldPwd.Text.Trim(), txtNewPwd.Text.Trim());
               // IdentityResult result = await UserManager.ChangePasswordAsync(HttpContext.Current.User.Identity.GetUserId(), txtOldPwd.Text.Trim(), txtNewPwd.Text.Trim());
                if (result.Succeeded)
                {
                    ClearField();
                    uc_sucess.SuccessMessage = "Password successfully changed.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);


                    //var user = await UserManager.FindByIdAsync(HttpContext.Current.User.Identity.GetUserId());
                    //await SignInAsync(user, isPersistent: false);
                    // return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                else
                {
                    uc_sucess.ErrorMessage = "There is some error in your password.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            
        }

        private void ClearField()
        {
            txtOldPwd.Text = string.Empty;
            txtNewPwd.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        //}
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.Current.GetOwinContext().Authentication;
        //    }
        //}
    }
}
