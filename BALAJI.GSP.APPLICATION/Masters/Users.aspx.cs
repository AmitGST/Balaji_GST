using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using GST.Utility;
using BusinessLogic;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System.Data.Entity.Validation;
using System.Security.Permissions;

namespace BALAJI.GSP.APPLICATION.Masters
{
    // [PrincipalPermission(SecurityAction.Deny, Role = "User")]
    public partial class Users : System.Web.UI.Page
    {

        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindStateCode();
                BoolUser_type();
                BindUsers();
                RegisteredWithUs();
                BindDesignation();
                //uc_modalUserList.Refresh();
            }
            //ddlRolesList.Items.Insert(0, new ListItem(" [ Select Role ] ", "0"));


            //if (Common.IsTaxConsultant())
            //{
            //    ddlRolesList.Items.Clear();
            //    ddlRolesList.DataSource = null;
            //    ddlRolesList.DataBind();
            //    string userRoleName = EnumConstants.RoleName.User.ToString();
            //    //var roleName = RoleManager.Roles.Where(r => r.Name == userRoleName).FirstOrDefault();
            //    ddlRolesList.Items.Add(new ListItem(roleName.Name, roleName.Id));
            //    // ddlRolesList.Items.Insert(0, new ListItem(" [ Select Role ] ", "0"));
            //}
        }


        protected void btnCreateUsers_Click(object sender, EventArgs e)
        {
            try
            {
                // ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                var isPersistData = Common.UserManager.Users.Where(u => u.Email.Contains(txtEmailID.Text.Trim()) || u.GSTNNo.Contains(txtGSTNNo.Text.Trim()));
                if (isPersistData != null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = txtGSTNNo.Text.Trim(),
                        Email = txtEmailID.Text.Trim(),
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        GSTNNo = txtGSTNNo.Text.Trim(),
                        GSTINUserId = txtGstId.Text.Trim(),
                        //OrganizationName = txtOrganization.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        PhoneNumber = txtPhoneNo.Text.Trim(),
                        StateCode = ddlStateCode.SelectedItem.Value,
                        Designation = Convert.ToString(ddlDesig.SelectedIndex),
                        GrossTurnOver = Convert.ToDecimal(txt_Grossturnover.Text.Trim()),
                        ITC = Convert.ToDecimal(txt_ITC.Text.Trim()),
                        UserType = (byte)(EnumConstants.UserType)Enum.Parse(typeof(EnumConstants.UserType), ddluser_type.SelectedItem.Value),
                        RegisterWithUs = Convert.ToBoolean((EnumConstants.RegisteredWithUs)Enum.Parse(typeof(EnumConstants.RegisteredWithUs), ddlRegistered.SelectedItem.Value)),
                        ParentUserID = HttpContext.Current.User.Identity.GetUserId()// !Common.UserManager.IsInRole(HttpContext.Current.User.Identity.GetUserId(), EnumConstants.RoleName.Admin.ToString()) ? HttpContext.Current.User.Identity.GetUserId() : string.Empty
                    };
                    //if (ddlRolesList.SelectedIndex > -1)
                    //{
                    string text = txtOrganization.Text;
                    var organizationname = unitOfwork.AspnetRepository.Find(c => c.OrganizationName == text);
                    if (organizationname == null)
                    {
                        user.OrganizationName = txtOrganization.Text.Trim();

                        IdentityResult result = Common.UserManager.Create(user, "Pass@123");
                        // IdentityResult result = manager.Create(user, txtPassword.Text.Trim());

                        if (result.Succeeded)
                        {
                            //if (ddlRolesList.SelectedIndex > -1)
                            //{
                            //    UserManager.AddToRole(user.Id, ddlRolesList.SelectedItem.Text);
                            //}
                            GST_MST_PRESENT_USER parentuser = new GST_MST_PRESENT_USER();
                            //parentuser.ParentUserID = Common.UserManager.FindById(Common.LoggedInUserID()).ParentUserID;
                            parentuser.ParentUserID = HttpContext.Current.User.Identity.GetUserId();// Common.LoggedInUserID();
                            parentuser.Id = user.Id;
                            unitOfwork.PresentUserRepository.Create(parentuser);
                            unitOfwork.Save();
                            ClearField();
                            BindUsers();
                            uc_sucess.SuccessMessage = "User Created Successfully.";
                            uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                        }
                        else
                        {
                            uc_sucess.ErrorMessage = result.Errors.FirstOrDefault();// "Unable to create or username already exist.";//result.Errors.Where(e=>e.);// 
                            uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                        }
                        //}
                        //else
                        //{
                        //    uc_sucess.SuccessMessage = "Kindly select role from role  list.";
                        //    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                        //}

                    }

                    else
                    {
                        uc_sucess.ErrorMessage = "Organization Name Already Exist!";
                        uc_sucess.Visible = true;
                    }
                }

                else
                {
                    uc_sucess.ErrorMessage = "Alert! Email-Id or GSTIN is already exists.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);

                }
            }


            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                // uc_sucess.ErrorMessage = ex.Message;
                // uc_sucess.VisibleError = true;
            }
        }

        private void ClearField()
        {
            //txtUserName.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtGSTNNo.Text = string.Empty;
            //txtPassword.Text = string.Empty;
            txt_ITC.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txt_Grossturnover.Text = string.Empty;
            txtGstId.Text = string.Empty;
            ddluser_type.SelectedValue = "0";
            txtPhoneNo.Text = string.Empty;
            txtOrganization.Text = string.Empty;
            ddlDesig.SelectedValue = "0";
            ddlStateCode.SelectedValue = "0";
            //txtDesig.Text = string.Empty;
        }

        public string UserStateName(object stateName)
        {
            var Name = unitOfwork.StateRepository.Find(f => f.StateCode == stateName.ToString()).StateName;
            return Name;
        }

        private void BoolUser_type()
        {
            ddluser_type.DataSource = typeof(EnumConstants.UserType).ToList();// Enumeration.ToList(typeof(EnumConstants.UserType));
            ddluser_type.DataTextField = "Value";
            ddluser_type.DataValueField = "Key";
            ddluser_type.DataBind();
            ddluser_type.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            // ddlNotified.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void RegisteredWithUs()
        {
            ddlRegistered.DataSource = typeof(EnumConstants.RegisteredWithUs).ToList();
            ddlRegistered.DataTextField = "Value";
            ddlRegistered.DataValueField = "Key";
            ddlRegistered.DataBind();
            ddlRegistered.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindStateCode()
        {
            var statedata = unitOfwork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateCode = s.StateCode }).ToList();
            ddlStateCode.DataSource = statedata;

            ddlStateCode.DataValueField = "StateCode";
            ddlStateCode.DataTextField = "StateName";
            ddlStateCode.DataBind();
            ddlStateCode.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlState.DataSource = statedata;

            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        protected void lkbAdd_Click(object sender, EventArgs e)
        {
            try
            {

                GST_MST_USER_SIGNATORY user = new GST_MST_USER_SIGNATORY();
                user.SignatoryName = txtSignatory.Text.Trim();
                //user.CertificateKey = txtCertKey.Text.Trim();
                user.Id = ddlUserList.SelectedValue.ToString();
                user.CreatedBy = Common.LoggedInUserID();
                user.CreatedDate = DateTime.Now;
                //user.OrgAddress = txtOrgAddress.Text.Trim();
                user.PhoneNumber = txtphone.Text.Trim();
                user.Email = txtemail.Text.Trim();
                user.GSTNNo = txtGSTIN.Text.Trim();
                user.Pincode = Convert.ToInt32(txtcode.Text.Trim());
                user.Status = true;
                user.State = ddlState.SelectedItem.Value;
                unitOfwork.UserSignatoryRepository.Create(user);
                string text = txtOrgAddress.Text;
                var organame = unitOfwork.UserSignatoryRepository.Find(c => c.OrgAddress == text);
                if (organame == null)
                {
                    user.OrgAddress = txtOrgAddress.Text.Trim();
                    unitOfwork.Save();
                    uc_sucess1.SuccessMessage = "Signatory Added successfully.";
                    uc_sucess1.Visible = true;
                    ClearControl();
                }
                else
                {
                    uc_sucess1.ErrorMessage = "Organization name already exist";
                    uc_sucess1.Visible = true;
                }

                // IdentityResult result= 
                //var result= this.G
                //if (result.Succeeded)
                //{
                //    uc_sucess1.SuccessMessage = "Signatory Added successfully.";
                //    uc_sucess1.Visible = true;
                //}

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess1.ErrorMessage = ex.Message;// "Alert! Signatory is already exists.";
                uc_sucess1.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
            }
        }

        private void ClearControl()
        {
            txtSignatory.Text = string.Empty;
            txtcode.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtGSTIN.Text = string.Empty;
            txtOrgAddress.Text = string.Empty;
            txtphone.Text = string.Empty;
            ddlUserList.SelectedIndex = -1;
            ddlState.SelectedIndex = 0;
        }
        private void BindUsers()
        {
            //var stateN = unitOfWork.StateRepository.Find(f => f.StateCode == stateN.StateCode.ToString()).StateName.ToString();
            lvUsers.DataSource = GetUsers();
            lvUsers.DataBind();

        }

        private void BindDesignation()
        {
            ddlDesig.DataSource = typeof(EnumConstants.Designation).ToList();
            ddlDesig.DataTextField = "Value";
            ddlDesig.DataValueField = "Key";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        public List<ApplicationUser> GetUsers()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            //if (UserManager.IsInRole(userId, EnumConstants.RoleName.User.ToString()))
            //{
            //    return null;
            //}
            var loggedUsers = unitOfwork.PresentUserRepository.Filter(f => f.ParentUserID == userId).Select(s => s.Id).ToList();
            var accounts = Common.UserManager.Users.Where(s => loggedUsers.Contains(s.Id)).ToList();// //Common.UserManager.IsInRole(userId, EnumConstants.RoleName.Admin.ToString()) ? Common.UserManager.Users.ToList() : Common.UserManager.Users.Where(u => u.ParentUserID == userId).ToList();// Roles.GetAllRoles().AsQueryable();
            ddlUserList.DataSource = accounts;
            ddlUserList.DataTextField = "OrganizationName";
            ddlUserList.DataValueField = "ID";
            ddlUserList.DataBind();
            ddlUserList.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            return accounts;
        }

        protected void lvUsers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindUsers();
            DataPager1.DataBind();
        }
    }
}