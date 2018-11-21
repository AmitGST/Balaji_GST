using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindInvoiceType();
        }
        private void BindInvoiceType()
        {
            //DropDownList1.DataSource = typeof(EnumConstants.InvoiceType).ToList();// Enumeration.ToList(typeof(EnumConstants.InvoiceSpecialCondition));
            //DropDownList1.DataTextField = "Value";
            //DropDownList1.DataValueField = "Key";
            //DropDownList1.DataBind();
            //ddlInvoiceType.SelectedValue = EnumConstants.InvoiceSpecialCondition.None.ToString();
        }
    }
}