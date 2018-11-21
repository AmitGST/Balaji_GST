using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_seller : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string FirstName
        {
            get { return txtSelletGSTIN.Text; }
            set { txtSelletGSTIN.Text = value; }
        }
    }
}