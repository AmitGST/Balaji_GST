using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_PerchaseRegister : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SellerUserID 
        {
            get
            {
                return ViewState["SellerUserID"].ToString();
            }
            set
            {
                ViewState["SellerUserID"] = value;
            }
        }

        public string _itemCode;
        public string ItemCode
        {
            get
            {
                return ViewState["ItemCode"].ToString();
            }
            set
            {
                txtItem.Text = value;
                txtItem.ReadOnly = true;
                ViewState["ItemCode"] = value;
              
            }
        }

        public bool SavePurchaseRegister
        {
            get
            {
                SaveQtyUpdate();
                return true;
            }
           
        }

        UnitOfWork unitOfwork = new UnitOfWork();

        private void SaveQtyUpdate()
        {

            try
            {
                GST_MST_PURCHASE_REGISTER register = new GST_MST_PURCHASE_REGISTER();
            //    register.Item_ID = unitOfwork.ItemRepository.Find(f => f.ItemCode == txtItem.Text.Trim()).Item_ID;
            //    register.Qty = Convert.ToDecimal(txtQty.Text.Trim());
            //    register.PerUnitRate = Convert.ToDecimal(txtPerUnitRate.Text.Trim());


                DateTime InwardDate = DateTime.ParseExact(txtStockInwardDate.Text.Trim(), "dd/MM/yyyy", null);
                register.StockInwardDate = InwardDate;
                DateTime OrderdDate = DateTime.ParseExact(txtStockOrderDate.Text.Trim(), "dd/MM/yyyy", null);
                register.StockInwardDate = OrderdDate;
                DateTime orderPoDate = DateTime.ParseExact(txtOrderPoDate.Text.Trim(), "dd/MM/yyyy", null);
                register.OrderPoDate = orderPoDate;
                //register.StockInwardDate = Convert.ToDateTime(txtStockInwardDate.Text.Trim());
                //register.StockOrderDate = Convert.ToDateTime(txtStockOrderDate.Text.Trim());
                //register.OrderPoDate = Convert.ToDateTime(txtOrderPoDate.Text.Trim());
                register.OrderPo = txtOrderPo.Text.Trim();

                register.SupplierInvoiceNo = txtSupplierInvoiceNo.Text.Trim();
                register.UserID = SellerUserID;
                register.Status = true;
                register.CreatedBy = Common.LoggedInUserID();
                register.CreatedDate = DateTime.Now;

                unitOfwork.PurchaseRegisterDataRepositry.Create(register);
                unitOfwork.Save();
                ClearControl();
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }


        }
        private void ClearControl()
        {
            txtQty.Text = string.Empty;
            txtPerUnitRate.Text = string.Empty;
            txtOrderPo.Text = string.Empty;
            txtSupplierInvoiceNo.Text = string.Empty;
            //txtGSTIN.Text = string.Empty;
        }
        protected void btnAddQty_Click(object sender, EventArgs e)
        {
            //GST_MST_PURCHASE_REGISTER register = new GST_MST_PURCHASE_REGISTER();
            //register.Item_ID = unitOfwork.ItemRepository.Find(f => f.ItemCode == txtItem.Text.Trim()).Item_ID;
            //register.Qty = Convert.ToDecimal(txtQty.Text.Trim());
            //register.PerUnitRate = Convert.ToDecimal(txtQty.Text.Trim());

            //register.StockInwardDate = Convert.ToDateTime(txtQty.Text.Trim());
            //register.StockOrderDate=Convert.ToDateTime(txtQty.Text.Trim());
            //register.OrderPo = txtQty.Text.Trim();
            //register.OrderPoDate = Convert.ToDateTime(txtQty.Text.Trim());
            //register.SupplierInvoiceNo = txtQty.Text.Trim();
            //register.UserID = SellerUserID;

            //unitOfwork.PurchaseRegisterDataRepositry.Create(register);
            //unitOfwork.Save();
        }
    }
}