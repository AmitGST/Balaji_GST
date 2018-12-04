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
using System.IO;


namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_CreateUser : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
   //         string javaScriptFunction =
   //"function pageLoad() {" +
   //    "$(function () {" +
   //        "$('#btnClick').click(function () {" +
   //            "alert('Alert: Hello from jQuery!');" +
   //        "});" +
   //    "});" +
   //"}";
   //         ClientScript.RegisterStartupScript(this.GetType(), "myScript", javaScriptFunction, true);
            // BindUsers();
            ddlRolesList.DataSource = GetRoles().ToList();
            ddlRolesList.DataBind();
            BindStateCode();
            BoolUser_type();
            RegisteredWithUs();
            BindDesignation();
            ddlRolesList.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            BindBusinessType();
            if (Common.IsTaxConsultant())
            {

                ddlRolesList.Items.Clear();
                ddlRolesList.DataSource = null;
                ddlRolesList.DataBind();
                string userRoleName = EnumConstants.RoleName.User.ToString();
                var roleName = RoleManager.Roles.Where(r => r.Name == userRoleName).FirstOrDefault();
                ddlRolesList.Items.Add(new ListItem(roleName.Name, roleName.Id));
                ddlRolesList.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            registerPostBackControlBtnUpload();
        }

        private void registerPostBackControlBtnUpload()
        {

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(btnCreateUsers);

        }
        private IQueryable<ApplicationRole> GetRoles()
        {

            var accounts = RoleManager.Roles;// Roles.GetAllRoles().AsQueryable();
            return accounts;
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
        //amit--
        protected string SuccessMessage
        {
            get;
            private set;
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
            set
            {
                _userManager = value;
            }
        }
        protected void btnCreateUsers_Click(object sender, EventArgs e)
        {
            try
            {




                Byte[] ImgByte = null;
                if (FiSmallImage.HasFile)
                {
                    ImgByte = Common.GetImageByte(FiSmallImage);
                    decimal size = Math.Round(((decimal)FiSmallImage.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 2048)
                    {
                        uc_sucess.ErrorMessage = "Size of the image to be uploaded cannot exceed two mb.";
                        return;
                    }
                }
                // ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                var isPersistData = UserManager.Users.Where(u => u.Email.Contains(txtEmailID.Text.Trim()) || u.GSTNNo.Contains(txtGSTNNumber.Text.Trim()));
                if (isPersistData != null)
                {
                    var user = new ApplicationUser()
                    {
                        SmallImage = ImgByte,
                        UserName = txtUserName.Text.Replace(" ", ""),
                        Email = txtEmailID.Text.Replace(" ", ""),
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        GSTNNo = txtGSTNNumber.Text.Replace(" ", ""),
                        GSTINUserId = txtGstUser.Text.Trim(),
                        //OrganizationName = txtOrganization.Text.Trim(),
                        PhoneNumber = txtPhoneNumber.Text.Trim(),
                        StateCode = ddlStateCode.SelectedItem.Value,
                        NameOfSignatory = txtnamesignatory.Text.Trim(),
                        Designation = Convert.ToString(ddlDesig.SelectedIndex),
                        GrossTurnOver = Convert.ToDecimal(txt_Grossturnover.Text.Trim()),
                        Address = txtCreateAdd.Text.Trim(),
                        ITC = Convert.ToDecimal(txt_ITC.Text.Trim()),
                        UserType = (byte)(EnumConstants.UserType)Enum.Parse(typeof(EnumConstants.UserType), ddluser_type.SelectedItem.Value),
                        RegisterWithUs = Convert.ToBoolean((EnumConstants.RegisteredWithUs)Enum.Parse(typeof(EnumConstants.RegisteredWithUs), ddlRegistered.SelectedItem.Value)),
                        ParentUserID = UserManager.IsInRole(HttpContext.Current.User.Identity.GetUserId(), EnumConstants.RoleName.Admin.ToString()) ? HttpContext.Current.User.Identity.GetUserId() : string.Empty
                    };

                    string test = txtOrganizationName.Text;
                    var getitemcode = unitOfwork.AspnetRepository.Find(c => c.OrganizationName == test);
                    if (getitemcode == null)
                    {
                        user.OrganizationName = txtOrganizationName.Text;

                        //if (ddlRolesList.SelectedIndex > -1)
                        //{
                        IdentityResult result = UserManager.Create(user, txtPassword.Text.Trim());
                        // IdentityResult result = manager.Create(user, txtPassword.Text.Trim());

                        if (result.Succeeded)
                        {
                            if (ddlRolesList.SelectedIndex > -1)
                            {
                                UserManager.AddToRole(user.Id, ddlRolesList.SelectedItem.Text);
                            }
                            foreach (int i in lbBuisnessType.GetSelectedIndices())
                            {
                                GST_MST_USER_BUSINESSTYPE ob = new GST_MST_USER_BUSINESSTYPE();
                                ob.BusinessID = Convert.ToInt32(lbBuisnessType.Items[i].Value);
                                ob.UserID = user.Id;
                                ob.CreatedBy = Common.LoggedInUserID();
                                ob.CreatedDate = DateTime.Now;
                                unitOfwork.UserBuisnessTypeRepository.Create(ob);
                                unitOfwork.Save();
                            }
                            //var code = UserManager.GenerateEmailConfirmationToken(user.Id);
                            //userManager.EmailService();
                            //var callbackUrl = Url.Action("ConfirmEmail", "Account",new { userId = user.Id, code = code },protocol: Request.Url.Scheme);
                            ClearField();
                            // BindUsers();
                            uc_sucess.SuccessMessage = "User Created Successfully.";
                            uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                        }

                        else
                        {
                            uc_sucess.SuccessMessage = result.Errors.FirstOrDefault();// "Unable to create or username already exist.";//result.Errors.Where(e=>e.);// 
                            uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                        }
                    }
                    else
                    {
                        uc_sucess.ErrorMessage = "Organization Name Already Exist!";
                        uc_sucess.Visible = true;
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
                    uc_sucess.SuccessMessage = "Alert! Email-Id or GSTIN is already exists.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);

                }
            }
            catch (DbEntityValidationException ex)
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

        private void RegisteredWithUs()
        {
            ddlRegistered.DataSource = typeof(EnumConstants.RegisteredWithUs).ToList();
            ddlRegistered.DataTextField = "Value";
            ddlRegistered.DataValueField = "Key";
            ddlRegistered.DataBind();
            ddlRegistered.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }


        private void ClearField()
        {
            txtUserName.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            ddlDesig.SelectedValue = "0";
            txtLastName.Text = string.Empty;
            txtGSTNNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txt_ITC.Text = string.Empty;
            txtCreateAdd.Text = string.Empty;
            txt_Grossturnover.Text = string.Empty;
            ddlStateCode.SelectedValue = "0";
            txtGstUser.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            ddluser_type.SelectedValue = "0";
            txtOrganizationName.Text = string.Empty;
            ddlRolesList.SelectedValue = "0";
           
           
            txtnamesignatory.Text = string.Empty;
        }

        private void BoolUser_type()
        {
            ddluser_type.DataSource = typeof(EnumConstants.UserType).ToList();// Enumeration.ToList(typeof(EnumConstants.UserType));
            ddluser_type.DataTextField = "Value";
            ddluser_type.DataValueField = "Key";
            ddluser_type.DataBind();
            //ddluser_type.Items.Insert(0, new ListItem("[ UserType ] ", "0"));
            ddluser_type.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindStateCode()
        {
            ddlStateCode.DataSource = unitOfwork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateCode = s.StateCode }).ToList();

            ddlStateCode.DataValueField = "StateCode";
            ddlStateCode.DataTextField = "StateName";
            ddlStateCode.DataBind();
            ddlStateCode.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        private void BindDesignation()
        {
            ddlDesig.DataSource = typeof(EnumConstants.Designation).ToList();
            ddlDesig.DataTextField = "Value";
            ddlDesig.DataValueField = "Key";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindBusinessType()
        {
            lbBuisnessType.DataSource = unitOfwork.BuisnessTypeRepository.All().Select(f => new { BuisnessID = f.BusinessID, BuisnessType = f.BusinessType }).ToList();

            lbBuisnessType.DataValueField = "BuisnessID";
            lbBuisnessType.DataTextField = "BuisnessType";
            lbBuisnessType.DataBind();
            // lbBuisnessType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        //protected void btnAsyncUpload_Click(object sender, EventArgs e)
        //{
        //        bool HasFile = FiSmallImage.HasFile;
        //}

    }
}