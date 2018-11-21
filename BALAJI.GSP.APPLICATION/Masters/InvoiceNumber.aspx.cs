using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class InvoiceNumber : System.Web.UI.Page
    {

        UnitOfWork unitOfwork = new UnitOfWork();
        cls_Invoice clsInvoiceobj = new cls_Invoice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInvoice();
            }
        }

        private void BindInvoice()
        {
            lv_InvoiceType.DataSource = typeof(EnumConstants.InvoiceSpecialCondition).ToList();
            lv_InvoiceType.DataBind();

        }

        protected void lv_InvoiceType_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hFInvoiceKey = (HiddenField)e.Item.FindControl("hFInvoiceKey") as HiddenField;
                TextBox txtInvoiceType = (TextBox)e.Item.FindControl("txtInvoiceType") as TextBox;
                EnumConstants.InvoiceSpecialCondition InvoiceType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), hFInvoiceKey.Value);
                var cDate = DateTime.Now.Year;
                var pDate = DateTime.Now.Year + 1;
                var year = cDate.ToString().Substring(2, 2) + "-" + pDate.ToString().Substring(2, 2);
                Label lblYear = (Label)e.Item.FindControl("lblYear") as Label;
                lblYear.Text = year;
                txtInvoiceType.Text = cls_Invoice.InvoiceNoPreFix(InvoiceType);
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte InvoiceType;
                foreach (ListViewDataItem item in lv_InvoiceType.Items)
                {
                    TextBox txtInvoiceType = (TextBox)item.FindControl("txtInvoiceType");
                    TextBox txtInvoiceNo = (TextBox)item.FindControl("txtInvoiceNo");
                    HiddenField hdn = (HiddenField)item.FindControl("hFInvoiceKey");
                    InvoiceType = (byte)(EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), hdn.Value);
                    var data = unitOfwork.InvoiceNoRepository.Filter(x => x.InvoiceType == InvoiceType).SingleOrDefault();
                    if (data != null)
                    {
                        data.CustomInvoiceNo = (txtInvoiceType.Text.Trim()) + "/17-18 /" + (txtInvoiceNo.Text.Trim());
                        unitOfwork.InvoiceNoRepository.Update(data);
                    }
                    else
                    {
                        GST_MST_CUSTOM_INVOICE PD = new GST_MST_CUSTOM_INVOICE();
                        var userid = Common.LoggedInUserID();
                        var Userid = unitOfwork.AspnetRepository.Find(f => f.Id == userid).Id;
                        PD.UserId = userid;
                        //PD.InvNoId= Convert.ToInt32(invoicenoID);
                        PD.UserId = Convert.ToString(userid);
                        PD.InvoiceType = (byte)(EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), hdn.Value);
                        PD.CustomInvoiceNo = (txtInvoiceType.Text.Trim()) + "/17-18 /" + (txtInvoiceNo.Text.Trim());
                        PD.CreatedBy = Common.LoggedInUserID();
                        PD.CreatedDate = DateTime.Now;
                        unitOfwork.InvoiceNoRepository.Create(PD);

                    }
                    unitOfwork.Save();
                    //uc_sucess.SuccessMessage = "Data Submited Successfully.";
                    //uc_sucess.Visible = true;

                }

            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}