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

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_Purchase_Data : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        cls_PurchaseRegister purchaseRegisterObj =new cls_PurchaseRegister();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();
                BindInvoiceNum();
            }

        }
        public void BindInvoiceNum()
        {
             var userid = Common.LoggedInUserID();
             ddlInvoiceNum.DataSource = unitOfwork.PurchaseRegisterDataRepositry.Filter(f => f.Status == true && f.UserID == userid).OrderByDescending(o => o.CreatedDate).Select(s => new { SInvoiceName = s.SupplierInvoiceNo, PurchageRegisterID = s.PurchageRegisterID }).ToList();
            ddlInvoiceNum.DataValueField = "PurchageRegisterID";
            ddlInvoiceNum.DataTextField = "SInvoiceName";
            ddlInvoiceNum.DataBind();
            ddlInvoiceNum.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }
        
        private List<GST_MST_PURCHASE_DATA> CreateBlankItem()
        {
            List<GST_MST_PURCHASE_DATA> objList = new List<GST_MST_PURCHASE_DATA>();
            for (int i = 1; i <= 5; i++)
            {
                GST_MST_PURCHASE_DATA obj = new GST_MST_PURCHASE_DATA();
                objList.Add(obj);
            }
            return objList;
        }

        private void BindItems()
        {
            List<GST_MST_PURCHASE_DATA> objList = new List<GST_MST_PURCHASE_DATA>();
            for (int i = 1; i <= 10; i++)
            {
                GST_MST_PURCHASE_DATA obj = new GST_MST_PURCHASE_DATA();
                objList.Add(obj);
            }
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[8] { 
                //                new DataColumn("ItemCode", typeof(string)),
                //                new DataColumn("Description",typeof(string)),
                //new DataColumn("Qty",typeof(string)) ,
                //new DataColumn("Unit",typeof(string)) ,
                //new DataColumn("Rate",typeof(string)) ,
                //new DataColumn("TotalAmount",typeof(string)) ,
                //new DataColumn("Discount",typeof(string)) ,
                //new DataColumn("TaxableAmount",typeof(string)) });

                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
                //dt.Rows.Add("", "", "", "", "", "", "", "");
            lv_purchasedata.DataSource = objList;
            lv_purchasedata.DataBind();
          //  lv_purchasedata.UseAccessibleHeader = true;
         //   lv_purchasedata.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnPurchaseData_Click(object sender, EventArgs e)
        {
            GST_MST_PURCHASE_DATA PD = new GST_MST_PURCHASE_DATA();
            try
            {

                foreach (GST_MST_PURCHASE_DATA item in GetGVData())
                {

                    if (item.PurchaseDataID != 0)
                    {
                        unitOfwork.PurchaseDataRepositry.Update(item);
                    }
                    else
                    {
                        unitOfwork.PurchaseDataRepositry.Create(item);
                    }



                    // count = count + 1;
                    //if (count > 0)
                    //{
                    //    this.Master.SuccessMessage = "Data submitted successfully !";
                    //    //uc_sucess.Visible = true;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //}
                    //else
                    //{
                    //    this.Master.WarningMessage = "Enter valid data !.";
                    //    //uc_sucess.Visible = true;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //}
                    uc_sucess.SuccessMessage = "Data Submited Successfully.";
                    uc_sucess.Visible = true;

                }
                unitOfwork.Save();
                BindItems();
                ddlInvoiceNum.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private List<GST_MST_PURCHASE_DATA> GetGVData()
        {
            List<GST_MST_PURCHASE_DATA> listObject = new List<GST_MST_PURCHASE_DATA>();
            foreach (ListViewDataItem item in lv_purchasedata.Items)
            {
                GST_MST_PURCHASE_DATA PD = new GST_MST_PURCHASE_DATA();
                string purchasedataid = lv_purchasedata.DataKeys[item.DisplayIndex].Values["PurchaseDataID"].ToString();
                TextBox txtItem = (TextBox)item.FindControl("txtItem");
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    return listObject;
                }
                TextBox txtQty = (TextBox)item.FindControl("txtQty");
                TextBox txtRate = (TextBox)item.FindControl("txtRate");
                TextBox txttotalAmount = (TextBox)item.FindControl("txttotalAmount");
                TextBox txtdiscount = (TextBox)item.FindControl("txtdiscount");
                TextBox txttaxableAmt = (TextBox)item.FindControl("txttaxableAmt");
                TextBox txtAmountTax = (TextBox)item.FindControl("txtAmountTax");
                TextBox txtIGSTRate = (TextBox)item.FindControl("txtIGSTRate");
                TextBox txtIGSTAmount = (TextBox)item.FindControl("txtIGSTAmount");
                TextBox txtCGSTRate = (TextBox)item.FindControl("txtCGSTRate");
                TextBox txtCGSTAmount = (TextBox)item.FindControl("txtCGSTAmount");
                TextBox txtSGSTAmount = (TextBox)item.FindControl("txtSGSTAmount");
                TextBox txtSGSTRate = (TextBox)item.FindControl("txtSGSTRate");
                TextBox txtUTGSTRate = (TextBox)item.FindControl("txtUTGSTRate");
                TextBox txtUTGSTAmount = (TextBox)item.FindControl("txtUTGSTAmount");
                TextBox txtCessRate = (TextBox)item.FindControl("txtCessRate");
                TextBox txtCessAmount = (TextBox)item.FindControl("txtCessAmount");
                var itemID = unitOfwork.ItemRepository.Find(f => f.ItemCode == txtItem.Text.Trim()).Item_ID;
                var Lineid = unitOfwork.InvoiceDataRepository.Find(f => f.Item_ID == itemID).LineID;
                PD.PurchageRegisterID = Convert.ToInt32(ddlInvoiceNum.SelectedItem.Value);
                PD.Item_ID = itemID;
                PD.PurchaseDataID = Convert.ToInt32(purchasedataid);
                PD.LineID = Convert.ToString(Lineid);
                PD.Qty = Convert.ToDecimal(txtQty.Text.Trim());
                PD.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                PD.TotalAmount = Convert.ToDecimal(txttotalAmount.Text.Trim());
                PD.Discount = Convert.ToDecimal(txtdiscount.Text.Trim());
                PD.TotalAmountWithTax = Convert.ToDecimal(txttaxableAmt.Text.Trim());
                PD.TaxableAmount = Convert.ToDecimal(txtAmountTax.Text.Trim());
                PD.IGSTRate = Convert.ToDecimal(txtIGSTRate.Text.Trim());
                PD.IGSTAmt = Convert.ToDecimal(txtIGSTAmount.Text.Trim());
                PD.CGSTRate = Convert.ToString(txtCGSTRate.Text.Trim());
                PD.CGSTAmt = Convert.ToDecimal(txtCGSTAmount.Text.Trim());
                PD.SGSTAmt = Convert.ToDecimal(txtSGSTAmount.Text.Trim());
                PD.SGSTRate = Convert.ToDecimal(txtSGSTRate.Text.Trim());
                PD.UGSTRate = Convert.ToDecimal(txtUTGSTRate.Text.Trim());
                PD.UGSTAmt = Convert.ToDecimal(txtUTGSTAmount.Text.Trim());
                PD.CessRate = Convert.ToDecimal(txtCessRate.Text.Trim());
                PD.CessAmt = Convert.ToDecimal(txtCessAmount.Text.Trim());
                PD.InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                PD.Status = true;
                PD.CreatedBy = Common.LoggedInUserID();
                PD.CreatedDate = DateTime.Now;
                listObject.Add(PD);
            }
            return listObject;
        }

        protected void ddlInvoiceNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            var purchaseRegisterID = ddlInvoiceNum.SelectedItem.Value;
            var data = purchaseRegisterObj.GetPurchaseRegister(purchaseRegisterID);
            BindPurchaseData(data);
        }

        private void BindPurchaseData(GST_MST_PURCHASE_REGISTER registerData)
        {
            List<GST_MST_PURCHASE_DATA> listObject = new List<GST_MST_PURCHASE_DATA>();
            listObject = registerData.GST_MST_PURCHASE_DATA.ToList();
            foreach (GST_MST_PURCHASE_DATA item in CreateBlankItem())
            {
                listObject.Add(item);
            }

            lv_purchasedata.DataSource = listObject;
            lv_purchasedata.DataBind();
        }
    }
}