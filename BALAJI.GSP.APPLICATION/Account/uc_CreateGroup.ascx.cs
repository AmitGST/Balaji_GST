using BALAJI.GSP.APPLICATION.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using BusinessLogic.Repositories;
namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_CreateGroup : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<ApplicationGroup> Groups()
        {
            var groupAll = this.GroupManager.Groups.OrderBy(o => o.Name);
            return groupAll;
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

        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            CreateGroup(new ApplicationGroup(txtGroupName.Text.Trim(), txtDescription.Text.Trim()));
        }

        private void CreateGroup(ApplicationGroup applicationgroup, params string[] selectedRoles)
        {
            try
            {
                var result = this.GroupManager.CreateGroup(applicationgroup);
                if (result.Succeeded)
                {
                    selectedRoles = selectedRoles ?? new string[] { };
                    // Add the roles selected:
                    // this.GroupManager.SetGroupRoles(applicationgroup.Id, selectedRoles);
                    ClearControl();
                    uc_sucess.SuccessMessage = "Group Created.";
                    lvGroups.DataBind();
                    //lvGroups.DataSource = unitOfwork.GroupRepository.All().OrderBy(o => o.GroupType).ToList();
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                }
                else
                {
                    uc_sucess.SuccessMessage = result.Errors.FirstOrDefault(); ;
                    lvGroups.DataBind();
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void ClearControl()
        {
            txtGroupName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void RemoveGroup(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnDelete = (LinkButton)sender;
                string groupID = btnDelete.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(groupID))
                {
                    var result = this.GroupManager.DeleteGroup(groupID);

                    if (result.Succeeded)
                    {
                        uc_sucess.SuccessMessage = "Group deleted.";
                        uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                        lvGroups.DataBind();
                    }
                    else
                    {
                        uc_sucess.SuccessMessage = result.Errors.FirstOrDefault();
                        uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                    }
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