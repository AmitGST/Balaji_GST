using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class Testing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList1.DataSource = Enumeration.GetAll<EnumConstants.Status>();
            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataBind();
        }
    }
}