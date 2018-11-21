using BALAJI.GSP.APPLICATION.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GST.Utility;
using BusinessLogic.Repositories;
namespace BALAJI.GSP.APPLICATION.UC
{

    public partial class uc_GSTNUsers : System.Web.UI.UserControl
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
            //    ShowInvoiceReturn();
                //   ckbSelfGSTN_CheckedChanged(sender, e);
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

        public string GetUserID
        {
            get
            {
                if (ddlGSTNUsers.SelectedIndex > 0)
                    return ddlGSTNUsers.SelectedItem.Value.ToString();
                else if (ckbSelfGSTN.Checked)
                    return HttpContext.Current.User.Identity.GetUserId();
                else
                    return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    if (HttpContext.Current.User.Identity.GetUserId() == value)
                        ckbSelfGSTN.Checked = true;
                    else
                        ddlGSTNUsers.SelectedItem.Value = value;
                }
            }
        }

        public string GetSellerProfile
        {
            get
            {
                if (ddlGSTNUsers.SelectedIndex > 0)
                {
                    return ddlGSTNUsers.SelectedItem.Value.ToString();
                }
                else if (ckbSelfGSTN.Checked)
                {
                    return HttpContext.Current.User.Identity.GetUserId();
                }
                else
                { return string.Empty; }
            }
            set
            {
                if (value != null)
                {
                    if (HttpContext.Current.User.Identity.GetUserId() == value)
                    { ckbSelfGSTN.Checked = true; }
                    else
                    { ddlGSTNUsers.SelectedItem.Value = value; }
                }
            }
        }



        public ApplicationUser GetUserUserProfile
        {
            get
            {

                if (ddlGSTNUsers.SelectedIndex > 0)
                {

                    var ddlvalue = ddlGSTNUsers.SelectedItem.Text.ToString();
                    var ddlvalueid = ddlGSTNUsers.SelectedItem.Value.ToString();
                    var profile = UserManager.Users.Where(w => w.GSTNNo == ddlvalue && w.Id == ddlvalueid).FirstOrDefault();
                    return profile;
                }
                else if (ckbSelfGSTN.Checked)
                {
                    var userID = Common.LoggedInUserID();
                    var profile = UserManager.Users.Where(w => w.Id == userID).FirstOrDefault();
                    return profile;
                }
                else
                    return null;
            }
        }


        //public ApplicationUser GetViewInvoiceId
        //{
        //    get
        //    {

        //        if (ddlGSTNUsers.SelectedIndex > 0)
        //        {

        //            var ddlvalue = ddlGSTNUsers.SelectedItem.Text.ToString();
        //            var ddlvalueid = ddlGSTNUsers.SelectedItem.Value.ToString();
        //            var profile = UserManager.Users.Where(w => w.GSTNNo == ddlvalue && w.Id == ddlvalueid).FirstOrDefault();
        //            return profile;
        //        }
        //        else if (ckbSelfGSTN.Checked)
        //        {
        //            var userID = Common.LoggedInUserID();
        //            var profile = UserManager.Users.Where(w => w.Id == userID).FirstOrDefault();
        //            return profile;
        //        }
        //        else
        //            return null;
        //    }
        //}


        public void BindUsers()
        {
            ddlGSTNUsers.DataSource = GetUsers();
            ddlGSTNUsers.DataTextField = "GSTNNo";
            ddlGSTNUsers.DataValueField = "ID";
            ddlGSTNUsers.DataBind();

            ddlGSTNUsers.Items.Insert(0, new ListItem(" [ Select Seller GSTIN No. ] ", "0"));
        }
        public List<ApplicationUser> GetUsers()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (UserManager.IsInRole(userId, EnumConstants.RoleName.User.ToString()))
            {
                return null;
            }
            var accounts = UserManager.Users.Where(u => u.ParentUserID == userId && u.RegisterWithUs == true).ToList();
            AssociatedUsersIds = accounts.Select(s => s.Id).ToList();
            return accounts;
        }
        //private List<string> _associatedUsersIds=null;
        public List<string> AssociatedUsersIds
        {
            get
            {
                if (ViewState["AssociatedUsersIds"] != string.Empty)
                {
                    var ids = (List<string>)ViewState["AssociatedUsersIds"];
                    return ids;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ViewState["AssociatedUsersIds"] != string.Empty)
                {
                    ViewState["AssociatedUsersIds"] = string.Empty;
                }
                ViewState["AssociatedUsersIds"] = value;
                //_associatedUsersIds = ViewState["AssociatedUsersIds"];
            }
        }

       // public EventHandler addInvoiceUnchkRedirectaa;
        public EventHandler addInvoicechkRedirect;
        public void ckbSelfGSTN_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbSelfGSTN.Checked)
            {
                ddlGSTNUsers.SelectedIndex = 0;
                ddlGSTNUsers.Enabled = false;
                addInvoicechkRedirect(sender, e);
                //ViewInvoicechkRedirect(sender, e);

            }
            else
            {
                if (this.GetValue > 0)
                {
                    addInvoicechkRedirect(sender, e);
                }
                ddlGSTNUsers.Enabled = true;
                //addInvoicechkRedirect(sender, e);
                //addInvoiceUnchkRedirectaa(sender, e);
                //ViewInvoicechkRedirect(sender, e);
            }
        }


        public bool GetchkValue
        {
            get
            {
                return this.ckbSelfGSTN.Checked;
            }
        }

        public int GetItem
        {
            get
            {
                //  var ddlreturnvalue=string.IsNullOrEmpty(ViewState[""])
                return Convert.ToInt32(this.ddlGSTNUsers.SelectedValue);
            }
        }
        public int GetValue
        {
            get
            {
              //  var ddlreturnvalue=string.IsNullOrEmpty(ViewState[""])
                return this.ddlGSTNUsers.SelectedIndex;
            }
        }

        public int GetselValue
        {
            get
            {
                return this.ddlGSTNUsers.SelectedIndex;
            }
        }

        
      
        public EventHandler addInvoiceRedirect;
        protected void ddlGSTNUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            addInvoiceRedirect(sender, e);
        }

        //public void ShowInvoiceReturn()
        //{
        //    if (Common.IsUser())
        //    {
        //        InvoiceReturn.Visible = false;
        //    }
        //    else if (Common.IsAdmin())
        //    {
        //        InvoiceReturn.Visible = false;
        //    }
       // }
    }
}