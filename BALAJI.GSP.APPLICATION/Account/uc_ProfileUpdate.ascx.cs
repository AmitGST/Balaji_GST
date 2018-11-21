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
    public partial class uc_ProfileUpdate : System.Web.UI.UserControl
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindStateCode();
                var currentUser = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                // CurrentUserData = currentUser;
                lblUserName.Text = HttpContext.Current.User.Identity.GetUserName();
                txtFirstName.Text = currentUser.FirstName;
                txtLastName.Text = currentUser.LastName;
                txtEmailID.Text = currentUser.Email;
                txtPhoneno.Text = currentUser.PhoneNumber;
                txtGSTNNo.Text = currentUser.GSTNNo;
                //txt_StateCode.Text = unitOfWork.StateRepository.Find(f => f.StateCode == currentUser.StateCode.ToString()).StateName.ToString();
                ddl_StateCode.SelectedValue = currentUser.StateCode;
                txtDesignation.Text = currentUser.Designation;
                txtOrganization.Text = currentUser.OrganizationName;
                Txt_GrossTurnover.Text = currentUser.GrossTurnOver.ToString();
                txt_address.Text = currentUser.Address;
                txtGSTNid.Text = currentUser.GSTINUserId;
                //Txt_GrossTurnover = Convert.ToDecimal(currentUser.GrossTurnOver) ;
                //txtSigName.Text = currentUser.NameOfSignatory;
                //Txt_ITC.Text = currentUser.ITC.ToString();
                if (currentUser.EmailConfirmed == true)
                    chkConfirmed.Checked = true;

                BindCurrentTurnOver();
            }
        }

        private void BindCurrentTurnOver()
        {
            string userId = Common.LoggedInUserID();
            var item = unitOfWork.CurrentTurnoverRepositry.Filter(f => f.User_ID == userId).OrderByDescending(o => o.CreatedDate).ToList();
            lv_CurrentTurnover.DataSource = item;
            lv_CurrentTurnover.DataBind();
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
        private void BindStateCode()
        {
            ddl_StateCode.DataSource = unitOfWork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateCode = s.StateCode }).ToList();

            ddl_StateCode.DataValueField = "StateCode";
            ddl_StateCode.DataTextField = "StateName";
            ddl_StateCode.DataBind();
            ddl_StateCode.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

        }
        //protected ApplicationUser CurrentUserData
        //{
        //    get
        //    {
        //        ApplicationUser o = (ApplicationUser)ViewState["CurrentUserData"];
        //        return (o == null) ? new ApplicationUser() : o;
        //    }
        //    set
        //    {
        //        ViewState["CurrentUserData"] = value;
        //    }
        //}
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                // var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                var currentUser = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                Byte[] ImgByte = null;
                if (FileUpload1.HasFile)
                {
                    ImgByte = Common.GetImageByte(FileUpload1);



                    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 2048)
                    {
                        uc_sucess.ErrorMessage = "Size of the image to be uploaded cannot exceed two mb.";
                        return;
                    }
                    // File.WriteAllBytes(Server.MapPath(filePath), ImgByte);
                    //Display the Image File.
                    // UserImage.ImageUrl = FileUpload1;
                    //  UserImage.Visible = true;
                    // ImgByte;
                }
                currentUser.SmallImage = ImgByte;
                //currentUser.FirstName = txtFirstName.Text.Trim();
                //currentUser.LastName = txtLastName.Text.Trim();
                //currentUser.Email = txtEmailID.Text.Trim();
                currentUser.PhoneNumber = txtPhoneno.Text;
                currentUser.Address = txt_address.Text.Trim();
                //currentUser.StateCode = txt_StateCode.Text.Trim();
                currentUser.GrossTurnOver = Convert.ToDecimal(Txt_GrossTurnover.Text.Trim());
                currentUser.Designation = txtDesignation.Text.Trim();
                //currentUser.ITC = Convert.ToDecimal(Txt_ITC.Text.Trim());
                currentUser.GSTINUserId = txtGSTNid.Text.Trim();
                currentUser.StateCode = ddl_StateCode.SelectedItem.Value;

                var result = UserManager.Update(currentUser);
                if (result.Succeeded)
                {
                    uc_sucess.SuccessMessage = "Profile saved successfully.";
                    uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
                else
                {
                    uc_sucess.ErrorMessage = "Profile not Saved Successfully";

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        protected string SuccessMessage
        {
            get;
            private set;
        }

    }
}