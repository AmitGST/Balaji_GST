using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALAJI.GSP.APPLICATION.Model;
using Owin;
using Microsoft.AspNet.Identity.Owin;

namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class Registration1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                BindDesignation();
                BindUsertype();
            }
         
        }
        protected void btnSigBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnSigNext_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btnBackOrg_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        private void BindDesignation()
        {
            ddlDesig.DataSource = typeof(EnumConstants.Designation).ToList();
            ddlDesig.DataTextField = "Key";
            ddlDesig.DataValueField = "Value";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindUsertype()
        {
            ddlUserType.DataSource = typeof(EnumConstants.UserType).ToList();
            ddlUserType.DataTextField = "Key";
            ddlUserType.DataValueField = "Value";
            ddlUserType.DataBind();
           ddlUserType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;

            //var designation = Convert.ToString(ddlDesig.SelectedItem.Text);
            Label1.Text = txtPan.Text;
            Label2.Text = txtAadhar.Text;
            Label3.Text = txtLogin.Text;
            Label4.Text = txtFirst.Text;
            Label5.Text = txtLast.Text;
            Label6.Text = Convert.ToString(ddlDesig.SelectedItem.Text);
            Label7.Text = txtMobile.Text;
            Label8.Text = txtAdd1.Text + " " + txtAdd2.Text;
            Label9.Text = txtEmail.Text;
            Label10.Text = ddlRole.Text;
            Label11.Text = txtOrganization.Text;
            Label12.Text = txtBusiness.Text;
            Label13.Text = Convert.ToString(ddlUserType.SelectedValue.ToString());
            Label14.Text = txtGstin.Text;
            Label15.Text = txtState.Text;
            Label16.Text = txtGstinName.Text;
            Label17.Text = txtAddress.Text + " " + txtAddress1.Text + " " + txtAdd3.Text;
            Label18.Text = txtCity.Text;
            Label19.Text = txtPincode.Text;
        }
        protected void btnBackPreview_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
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




        //public void BindData()
        //{
        //    dt= new DataTable();
        //    dt.Columns.Add("UserName");
        //    dt.Columns.Add("PasswordHash");
        //    dt.Columns.Add("FirstName");
        //    dt.Columns.Add("LastName");
        //    dt.Columns.Add("Designation");
        //    dt.Columns.Add("PhoneNumber");
        //    dt.Columns.Add("Address");//length 100
        //    dt.Columns.Add("Email");
        //    Session["Data"] = dt;

        //    DataRow dr = dt.NewRow();
        //    dt.Rows.Add(dr);
        //    dr[0] = txtLogin.Text;
        //    dr[1] = txtPassword.Text;
        //    dr[2] = txtFirst.Text;
        //    dr[3] = txtLast.Text;
        //    dr[4] = ddlDesig.SelectedItem.Text;
        //    dr[5] = txtMobile.Text;
        //    dr[6] = txtAdd1.Text+","+txtAdd2.Text;
        //    dr[7] = txtEmail.Text;
        //}
    }
}