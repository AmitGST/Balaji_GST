using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION
{
    public partial class NotAuthorized : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (!Common.IsAdmin())
            {
                this.MasterPageFile = "~/User/User.master";
            }
            if (Common.IsAdmin())
            {
                this.MasterPageFile = "~/Admin/Admin.master";
            }
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}