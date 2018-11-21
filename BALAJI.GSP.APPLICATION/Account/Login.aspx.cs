using BALAJI.GSP.APPLICATION.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using BALAJI.GSP.APPLICATION.Model;
using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using BALAJI.GSP.APPLICATION.Model;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using GST.Utility;
using BusinessLogic.Repositories;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class Login : System.Web.UI.Page
    {
        UnitOfWork unitofwork = new UnitOfWork();
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
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

        protected void Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (FailureText.Visible == true)
                {
                    var result = SignInManager.PasswordSignIn(UserName.Text.Trim(), Password.Text.Trim(), RememberMe.Checked, shouldLockout: false);
                    var hashPassword = Crypto.HashPassword(Password.Text.Trim());
                    switch (result)
                    {
                        case SignInStatus.Success:
                            var user = UserManager.FindByName(UserName.Text.Trim());
                            var roles = UserManager.GetRoles(user.Id); FailureText.Text = "sucess.";
                            if (roles.Count > 0)
                            {
                                FailureText.Text = "sucess11.";
                                if (roles.FirstOrDefault().Contains(EnumConstants.RoleName.Admin.ToString()))
                                {
                                    System.Web.HttpContext.Current.Response.Redirect("~/Admin/Dashboard");
                                }
                                else if (roles.FirstOrDefault().Contains(EnumConstants.RoleName.TaxConsultant.ToString()) || roles.FirstOrDefault().Contains(EnumConstants.RoleName.User.ToString()))
                                {
                                    System.Web.HttpContext.Current.Response.Redirect("~/User/Dashboard");
                                }
                            }

                            else
                            {
                                FailureText.Text = "Invalid login attempt.";
                            }
                            return;

                        case SignInStatus.Failure:
                            FailureText.Text = "Invalid login attempt.";
                            return;
                        default:
                            FailureText.Text = "Invalid login attempt.";
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void RedirectLogin(string username)
        {
            try
            {
                LoginRedirectByRoleSection roleRedirectSection = (LoginRedirectByRoleSection)ConfigurationManager.GetSection("loginRedirectByRole");
                var user = UserManager.FindByName(username);
                var rolesForUser = UserManager.GetRoles(user.Id);
                foreach (RoleRedirect roleRedirect in roleRedirectSection.RoleRedirects)
                {
                    if (rolesForUser.Contains(roleRedirect.Role))
                    {
                        // Response.Redirect(roleRedirect.Url);
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        protected void ValidateCaptcha_ServerValidate(object source, ServerValidateEventArgs e)
        {
            Captcha1.ValidateCaptcha(txtcaptcha.Text.Trim());
            e.IsValid = Captcha1.UserValidated;
            if (e.IsValid)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Captcha!');", true);
                FailureText.Visible = true;
            }
            else
            {
                FailureText.Visible = false;
            }
        }
    }
    public class LoginRedirectByRoleSection : ConfigurationSection
    {
        [ConfigurationProperty("roleRedirects")]
        public RoleRedirectCollection RoleRedirects
        {
            get
            {
                return (RoleRedirectCollection)this["roleRedirects"];
            }
            set
            {
                this["roleRedirects"] = value;
            }
        }
    }

    public class RoleRedirectCollection : ConfigurationElementCollection
    {
        public RoleRedirect this[int index]
        {
            get
            {
                return (RoleRedirect)BaseGet(index);
            }
        }

        public RoleRedirect this[object key]
        {
            get
            {
                return (RoleRedirect)BaseGet(key);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RoleRedirect();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RoleRedirect)element).Role;
        }
    }

    public class RoleRedirect : ConfigurationElement
    {
        [ConfigurationProperty("role", IsRequired = true)]
        public string Role
        {
            get
            {
                return (string)this["role"];
            }
            set
            {
                this["role"] = value;
            }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }
    }
}