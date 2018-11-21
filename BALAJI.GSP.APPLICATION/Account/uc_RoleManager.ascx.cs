using BALAJI.GSP.APPLICATION.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
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
using BusinessLogic.Repositories;


namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_RoleManager : System.Web.UI.UserControl
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Roles.CreateRole(txtRoleCreate.Text.Trim());
                var role = new ApplicationRole(txtRoleCreate.Text.Trim());//, txtRoleCreate.Text.Trim() + " Role");               
                if (RoleManager.RoleExists(txtRoleCreate.Text.Trim()))
                {
                    uc_sucess.SuccessMessage = "Role exist.";
                }
                else
                {
                    RoleManager.Create(role);
                    uc_sucess.SuccessMessage = "Role created.";
                }

                txtRoleCreate.Text = string.Empty;

                lvRoles.DataBind();
                uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public IQueryable<ApplicationRole> GetRoles()
        {
            var accounts = RoleManager.Roles.OrderBy(o => o.Name); // Roles.GetAllRoles().AsQueryable();
            return accounts;
        }

        protected void RemoveRoles(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnDelete = (LinkButton)sender;
                string roleName = btnDelete.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(roleName))
                {
                    var roleStore = new RoleStore<IdentityRole>();
                    var manager = new RoleManager<IdentityRole>(roleStore);
                    // var role = manager.Delete(new IdentityRole(roleName));

                    var role = RoleManager.Roles.SingleOrDefault(r => r.Name == roleName);
                    // ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                    RoleManager.Delete(role);
                    // _db.DeleteRole(_db, userManager, role.Id);

                    uc_sucess.SuccessMessage = "Role deleted.";
                    lvRoles.DataBind();
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}