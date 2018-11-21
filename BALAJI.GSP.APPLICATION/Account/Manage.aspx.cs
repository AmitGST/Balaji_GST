using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.UI;
using System.Web.UI.WebControls;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.Account
{
    [Authorize("Admin,TaxConsultant")]
    public partial class Manage : System.Web.UI.Page
    {
        //protected override void OnPreInit(EventArgs e)
        //{
        //    if (!Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/User/User.master";
        //    }
        //    if (Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/Admin/Admin.master";
        //    }
        //    base.OnPreInit(e);
        //}
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (!Common.IsAdmin())
        //    { this.MasterPageFile = "~/User/User.master"; }
        //    if (Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/Admin/Admin.master";
        //    }
        //}

        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                if (!IsPostBack)
                {
                   
                    LoadQuestionEntryUI((EnumConstants.Master)Enum.Parse(typeof(EnumConstants.Master), Name));
                    ControlLoaded = true;
                    return;
                }
                if (ControlLoaded)
                {
                    LoadQuestionEntryUI((EnumConstants.Master)Enum.Parse(typeof(EnumConstants.Master), Name));
                }

            }
            ShowButton();
            //else
            //{
            //    Response.Redirect("~/Admin/Master/Default.aspx");
            //}

        }

        private void ShowButton()
        {
            if(Common.IsAdmin())
            {
                lkbRole.Visible = true;
                lkbUsers.Visible = true;
                lkbGroup.Visible = true;
                lkbAssignRole.Visible = true;
                lkbUserNew.Visible = true;
                lkbbBuisnessType.Visible = true;
            }
            else if(Common.IsTaxConsultant())
            {
                //lkbUsers.Visible = true;             
                 lkbUserNew.Visible = true;
            }
            else
            {}
        }
        public bool ControlLoaded
        {
            get
            {
                Object s = ViewState["ControlLoaded"];
                return ((s == null) ? false : (bool)s);
            }

            set
            {
                ViewState["ControlLoaded"] = value;
            }
        }

        public string Name
        {
            get
            {
                Object s = ViewState["Name"];
                return ((s == null) ? "" : (string)s);
            }

            set
            {
                ViewState["Name"] = value;
            }
        }

        private void LoadQuestionEntryUI(EnumConstants.Master masterFormat)
        {

            //string mg = questionText.message;    
            switch (masterFormat)
            {
                case EnumConstants.Master.RoleNew:
                    phDynamicControls.Controls.Clear();
                    Control _categoryEntry = (Control)LoadControl("~/Account/uc_RoleManager.ascx");
                    _categoryEntry.ID = "uc_RoleManager_ID";
                    phDynamicControls.Controls.Add(_categoryEntry);
                    break;
                case EnumConstants.Master.RoleAssign:
                    phDynamicControls.Controls.Clear();
                    Control _subCategoryEntry = (Control)LoadControl("~/Account/uc_UserAddToRole.ascx");
                    _subCategoryEntry.ID = "uc_UserAddToRole_ID";
                    phDynamicControls.Controls.Add(_subCategoryEntry);
                    break;
                case EnumConstants.Master.GroupNew:
                    phDynamicControls.Controls.Clear();
                    Control _merchantgroup = (Control)LoadControl("~/Account/uc_CreateGroup.ascx");
                    _merchantgroup.ID = "uc_CreateGroup_ID_group";
                    phDynamicControls.Controls.Add(_merchantgroup);
                    break;
                case EnumConstants.Master.UserNew:
                    phDynamicControls.Controls.Clear();
                    Control _user = (Control)LoadControl("~/Account/uc_CreateUser.ascx");
                    _user.ID = "uc_CreateUser_ID";
                    phDynamicControls.Controls.Add(_user);
                    break;
                case EnumConstants.Master.Users:
                    phDynamicControls.Controls.Clear();
                    Control _usersListNewUsers = (Control)LoadControl("~/Account/uc_UserList.ascx");
                    _usersListNewUsers.ID = "uc_UserList_ID__usersListNewUsers";
                    phDynamicControls.Controls.Add(_usersListNewUsers);
                    break;
                case EnumConstants.Master.BusinessType:
                     phDynamicControls.Controls.Clear();
                     Control uc_CreateBuisnessType = (Control)LoadControl("~/Account/uc_CreateBuisnessType.ascx");
                     uc_CreateBuisnessType.ID = "uc_UserList_ID__usersListNewUsers";
                     phDynamicControls.Controls.Add(uc_CreateBuisnessType);
                    break;
                default:
                    break;

            }

        }

        //protected void Page_Load()
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        if (!IsPostBack)
        //        {
        //            LoadQuestionEntryUI((EnumConstants.Master)Enum.Parse(typeof(EnumConstants.Master), name));
        //            ControlLoaded = true;
        //            return;
        //        }
        //        if (ControlLoaded)
        //        {

        //            LoadQuestionEntryUI((EnumConstants.Master)Enum.Parse(typeof(EnumConstants.Master), name));
        //            // ControlLoaded = true;
        //        }

        //    }

        //}

        protected void setPassword_Click(object sender, EventArgs e)
        {
            //if (IsValid)
            //{
            //    var result = OpenAuth.AddLocalPassword(User.Identity.Name, password.Text);
            //    if (result.IsSuccessful)
            //    {
            //        Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
            //    }
            //    else
            //    {

            //        ModelState.AddModelError("NewPassword", result.ErrorMessage);

            //    }
            //}
        }


        public IEnumerable<OpenAuthAccountData> GetExternalLogins()
        {
            var accounts = OpenAuth.GetAccountsForUser(User.Identity.Name);
            CanRemoveExternalLogins = CanRemoveExternalLogins || accounts.Count() > 1;
            return accounts;
        }

        public void RemoveExternalLogin(string providerName, string providerUserId)
        {
            var m = OpenAuth.DeleteAccount(User.Identity.Name, providerName, providerUserId)
                ? "?m=RemoveLoginSuccess"
                : String.Empty;
            Response.Redirect("~/Account/Manage" + m);
        }


        protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
        {
            // You can change this method to convert the UTC date time into the desired display
            // offset and format. Here we're converting it to the server timezone and formatting
            // as a short date and a long time string, using the current thread culture.
            return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[never]";
        }

        protected void lkbOpen_Click(object sender, EventArgs e)
        {
            LinkButton lkb = (LinkButton)sender;
            Name = lkb.CommandName;
            LoadQuestionEntryUI((EnumConstants.Master)Enum.Parse(typeof(EnumConstants.Master), Name));
            ControlLoaded = true;
            return;
        }

       
    }
}