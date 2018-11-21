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
using GST.Utility;
using BusinessLogic.Repositories;
using DataAccessLayer;
namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_UserList : System.Web.UI.UserControl
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        //private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    uc_modalUserList.Refresh();

            //    //ddlRolesList.DataSource = GetRoles().ToList();              
            //    //ddlRolesList.DataBind();                
            //    //ddlRolesList.Items.Insert(0, new ListItem(" [ Select Role ] ", "0"));
            //} 
            BindUsers();
        }
        private ApplicationGroupManager _groupManager;
        public ApplicationGroupManager GroupManager
        {
            get
            {
                return _groupManager ?? new ApplicationGroupManager();
            }
            private set
            {
                _groupManager = value;
            }
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
        protected string UserID
        {
            get
            {
                object o = ViewState["UserID"];
                return (o == null) ? String.Empty : (string)o;
            }
            set
            {
                ViewState["UserID"] = value;
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

        private void BindUsers()
        {
            try
            {
                //var stateN = unitOfWork.StateRepository.Find(f => f.StateCode == stateN.StateCode.ToString()).StateName.ToString();
                lvUsers.DataSource = GetUsers();
                lvUsers.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public List<ApplicationUser> GetUsers()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            //if (UserManager.IsInRole(userId, EnumConstants.RoleName.User.ToString()))
            //{
            //    return null;
            //}
            var accounts = UserManager.IsInRole(userId, EnumConstants.RoleName.Admin.ToString()) ? UserManager.Users.Where(u => u.RegisterWithUs == true && u.ParentUserID != null).OrderBy(o => o.GSTNNo).ToList() : UserManager.Users.Where(u => u.ParentUserID == userId && u.RegisterWithUs == Convert.ToBoolean(1)).OrderBy(o => o.GSTNNo).ToList();// Roles.GetAllRoles().AsQueryable();

            return accounts;
        }

        public string UserStateName(object stateName)
        {
            var Name = unitOfWork.StateRepository.Find(f => f.StateCode == stateName.ToString()).StateName;
            return Name;
        }
        protected void lkb_Click(object sender, EventArgs e)
        {
            uc_Groups.BindGroups(true);
            LinkButton lkb = (LinkButton)sender;
            UserID = lkb.CommandArgument.ToString();
            var result = this.GroupManager.GetUserGroups(UserID).ToList();
            uc_Groups.SetGroup = result;

        }
        protected void lkbSubmit_Click()
        {


        }

        protected void btnAssingGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var result = this.GroupManager.SetUserGroups(UserID, uc_Groups.SelectedGroupList().ToArray());
                if (result.Succeeded)
                {
                    uc_sucess.SuccessMessage = "User added to group(s).";
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                }
                else
                {
                    uc_sucess.ErrorMessage = result.Errors.FirstOrDefault();
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        protected void lvUsers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindUsers();
                DataPager1.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}