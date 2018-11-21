using GST.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_invoiceMonth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInvoiceMonth();
            }
        }
        
        public void BindInvoiceMonth()
        {
            try
            {
                Array Months = Enum.GetValues(typeof(EnumConstants.FinYear));
                ddlInvoiceMonth.Items.Clear();
                ddlInvoiceMonth.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
                foreach (EnumConstants.FinYear month in Months)
                {
                    ddlInvoiceMonth.Items.Add(new ListItem(month.ToString(), ((byte)month).ToString()));
                }
                ddlInvoiceMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

            //ddlInvoiceMonth.DataSource = typeof(EnumConstants.FinYear).ToList();
            //ddlInvoiceMonth.DataTextField = "Value";
            //ddlInvoiceMonth.DataValueField = "Key";
            //ddlInvoiceMonth.DataBind();
            //ddlInvoiceMonth.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
        }
        //public string SetValue
        //{
        //    set
        //    {
        //        this.ddlInvoiceMonth.SelectedValue = value;
        //    }
        //}

        public string GetTextValue
        {
            get
            {
                //return this.ddlInvoiceMonth.SelectedIndex;
                return this.ddlInvoiceMonth.SelectedItem.Text;
            }
        }

        public string GetValue
        {
            get
            {
                //return this.ddlInvoiceMonth.SelectedIndex;
                return this.ddlInvoiceMonth.SelectedValue;
            }
        }


        public int GetIndexValue
        {
            get
            {
                return this.ddlInvoiceMonth.SelectedIndex;
            }
        }

        public EventHandler SelectedIndexChange;
        protected void ddlInvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        
         {
            SelectedIndexChange(sender, e);
        }



        //public string ddlMonthValue
        //{
        //    get
        //    {
        //        //return this.ddlInvoiceMonth.SelectedIndex;
        //        var ddlvalue=ddlInvoiceMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
        //        return ddlvalue;
        //    }
        //}
     


        //public event EventHandler OnChangeValue;
        //protected void ddlInvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (OnChangeValue != null)
        //        OnChangeValue(sender, e);
        //}
        
    }
}