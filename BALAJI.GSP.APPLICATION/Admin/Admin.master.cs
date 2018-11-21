using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
          
            //Response.("Master Page Pre Init event called <br/> ");
        }  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.IsAdmin() && !Common.IsTaxConsultant())
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                Response.Redirect("~/Account/Login");
            }
        }

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
        
    }
}