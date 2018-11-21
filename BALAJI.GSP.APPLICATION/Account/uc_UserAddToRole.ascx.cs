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
using System.Data.Entity.Validation;
using System.Diagnostics;
using BusinessLogic.Repositories;
namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_UserAddToRole : System.Web.UI.UserControl
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnGetUsers_Click(sender, e);
            }
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
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected void btnGetUsers_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFindUsers.Text.Trim()))
                {
                    //amits oct

                    //ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                    //IQueryable<ApplicationUser> users=new ;
                    List<ApplicationUser> users = new List<ApplicationUser>();
                    users.Add(_db.Users.SingleOrDefault(u => u.UserName == txtFindUsers.Text.Trim() && u.RegisterWithUs==true));
                    lvRoles.DataSource = users;
                    lvRoles.DataBind();
                }
                else
                {
                    //amits oct
                    lvRoles.DataSource = _db.Users.Where(w=>w.RegisterWithUs==true && w.ParentUserID != null).OrderBy(o=>o.GSTNNo).ToList();
                    lvRoles.DataBind();
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
           
        }

        protected void lkbAssingRoles_Click(object sender, EventArgs e)
        {
            chkRoleList.DataSource = _db.Roles.ToList();
            chkRoleList.DataBind();
            LinkButton btnAddRoletoUser = (LinkButton)sender;
            litUserName.Text = btnAddRoletoUser.Attributes["UserName"].ToString();
            UserName = btnAddRoletoUser.CommandArgument.ToString();
            ////phUserAddRole.Visible = !String.IsNullOrEmpty(RoleName);
        }
        //[ViewStateProperty("UserID")]
        protected string UserName
        {
            get
            {
                object o = ViewState["UserName"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                ViewState["UserName"] = value;
            }
        }

        protected void btnAddUserToRole_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //bool roleStatus;
            if (btn.CommandName == "addRole")
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    List<string> roles = new List<string>();
                   
                    
                    foreach (ListItem li in chkRoleList.Items)
                    {
                        if (li.Selected)
                        {
                            try
                            {
                                // ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                                var result = this.UserManager.AddToRole(UserName, li.Text);

                                if (!result.Succeeded)
                                {
                                   uc_sucess.SuccessMessage = "Unable added to specified " + li.Text + " role(s).";
                                   uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                                    return;
                                }
                                else
                                {
                                    uc_sucess.SuccessMessage = "User added to specified role(s).";
                                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                                }
                                roles.Add(li.Text);
                                chkRoleList.ClearSelection();
                            }
                            catch (Exception ex)
                            {
                                cls_ErrorLog ob = new cls_ErrorLog();
                                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                                //{
                                //    foreach (var validationError in validationErrors.ValidationErrors)
                                //    {
                                //        //Trace.TraceInformation(
                                //        //      "Class: {0}, Property: {1}, Error: {2}",
                                //        //      validationErrors.Entry.Entity.GetType().FullName,
                                //        //      validationError.PropertyName,
                                //        //      validationError.ErrorMessage);
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
            else if (btn.CommandName == "removeRole")
            {
                var result = UserManager.RemoveFromRole(UserName, chkRoleList.SelectedItem.Text);
                if (result.Succeeded)
                {
                    uc_sucess.SuccessMessage = "User remove from " + chkRoleList.SelectedItem.Text + " role.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
                else
                {
                    uc_sucess.SuccessMessage = "Unable to remove user from " + chkRoleList.SelectedItem.Text + " role.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
            chkRoleList.ClearSelection();
        }

        protected void lvRoles_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dproles.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            btnGetUsers_Click(sender, e);
            dproles.DataBind();
           
        }
       
    }
}