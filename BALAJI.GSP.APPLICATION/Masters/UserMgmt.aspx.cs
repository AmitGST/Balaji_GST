using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class UserMgmt : System.Web.UI.Page
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

       

        protected void lkbHistory_Click(object sender, EventArgs e)
        {
            divSecondary.Visible = true;
            divMain.Visible = false;
        }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            divSecondary.Visible = false;
            divMain.Visible = true;
        }
    }
}