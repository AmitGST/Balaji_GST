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

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_modalUserList : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                BindStateCode();
                GetUsers();
            //}
            

        }

        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}


        public void Refresh()
        {
            BindStateCode();
            GetUsers();
        }
        public void GetUsers()
        {

            var userId =Common.LoggedInUserID();
            //if (Common.UserManager.IsInRole(userId, EnumConstants.RoleName.User.ToString()))
            //{
            //    return ;
            //}
            var accounts = Common.UserManager.IsInRole(userId, EnumConstants.RoleName.Admin.ToString()) ? Common.UserManager.Users.Select(s => new { OrganizationName = s.OrganizationName, Id = s.Id }).ToList() : Common.UserManager.Users.Where(u => u.ParentUserID == userId).Select(s => new { OrganizationName = s.OrganizationName, Id = s.Id }).ToList();// Roles.GetAllRoles().AsQueryable();
            ddlUserList.DataSource = accounts;
            ddlUserList.DataTextField = "OrganizationName";
            ddlUserList.DataValueField = "ID";
            ddlUserList.DataBind();
            ddlUserList.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        public string UserID
        {
            get
            {
                return ViewState["UserID"].ToString();
            }
            set
            {
                ViewState["UserID"] = value;
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
            ddlUserList.SelectedIndex =-1;
            ddlState.SelectedIndex = 0;
            //txtCertKey.Text = string.Empty;
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
                user.OrgAddress = txtOrgAddress.Text.Trim();
                user.PhoneNumber = txtphone.Text.Trim();
                user.Email = txtemail.Text.Trim();
                user.GSTNNo = txtGSTIN.Text.Trim();
                user.Pincode = Convert.ToInt32(txtcode.Text.Trim());
                user.Status = true;
                user.State= ddlState.SelectedItem.Value;
                unitOfwork.UserSignatoryRepository.Create(user);
                unitOfwork.Save();
             
                uc_sucess.SuccessMessage = "Signatory Added successfully.";
                uc_sucess.Visible = true;  
                ClearControl();
               // IdentityResult result= 
                 //var result= this.G
               // if (result.Succeeded)
               // {
               //    //user.SuccessMessage = "Signatory Added successfully.";
               //    //user.Visible = !String.IsNullOrEmpty(SuccessMessage);
               // }
              
            }
            catch( Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess.ErrorMessage = ex.Message;// "Alert! Signatory is already exists.";
                uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
            }
        }

      //protected string SuccessMessage
      //{
      //    get;
      //    private set;
      //}

      private void BindStateCode()
      {
          ddlState.DataSource = unitOfwork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateCode = s.StateCode }).ToList();
          ddlState.DataValueField = "StateCode";
          ddlState.DataTextField = "StateName";
          ddlState.DataBind();
          ddlState.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

      }
    }
}