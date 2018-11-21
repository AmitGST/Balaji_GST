
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        //string strGSTIN = string.Empty;
        //ExcelDB excelDB = new ExcelDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsAuthenticate())
            {               
                Response.Redirect("~/Account/Login");
            }
            //if (!Common.IsTaxConsultant())       
            //{
            //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //    authenticationManager.SignOut();
            //    Response.Redirect("~/Account/Login");
            //}
            //else if (!Common.IsUser())
            //{
            //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //    authenticationManager.SignOut();
            //    Response.Redirect("~/Account/Login");
            //}
        }
        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {


            //strGSTIN = (string)(Session["GSTIN"]); 

            //int result = excelDB.UpdateLogOut(strGSTIN);

            //if (result > 0)
            //{
            //    /**/
            //    Session.Clear();
            //    Session.Abandon();
            //    FormsAuthentication.SignOut();
            //    Response.Redirect("GSTLogin.aspx");
            //    /**/

            //    //Response.Redirect("~/homePage.aspx");
            //}
        }

        //public void ShowModalPopup()
        //{
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    this.INVOICEModalPopupExtender.Show();
        //}
        //public void ShowModalPopupSuccess()
        //{
        //    lblMsg.ForeColor = System.Drawing.Color.Green;
        //    this.INVOICEModalPopupExtender.Show();
        //}
        public string ErrorMessage
        {
            get
            {
                return lblError.Text;
            }
            set
            {
                lblError.Text = value;
            }
        }

        public string SuccessMessage
        {
            get
            {
                return lblSuccess.Text;
            }
            set
            {
                lblSuccess.Text = value;
            }
        }
        public string WarningMessage
        {
            get
            {
                return lblWarning.Text;
            }
            set
            {
                lblWarning.Text = value;
            }
        }

        //protected void loginUserStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        //{
        //    strGSTIN = (string)(Session["GSTIN"]);

        //    int result = excelDB.UpdateLogOut(strGSTIN);

        //    if (result > 0)
        //    {
        //        /**/
        //        Session.Clear();
        //        Session.Abandon();
        //        FormsAuthentication.SignOut();
        //        Response.Redirect("GSTLogin.aspx");
        //        /**/

        //        //Response.Redirect("~/homePage.aspx");
        //    }
        //}
    }
}