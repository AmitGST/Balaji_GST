using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using businessAccessLayer;
using com.B2B.GST.ExcelFunctionality;
using com.B2B.GST.ExceptionHandling;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using DataAccessLayer;
using GST.Utility;
using BusinessLogic.Repositories;

namespace UserInterface
{
    public partial class UpdateSaleRegister : System.Web.UI.Page
    {
        cls_PurchaseRegister register = new cls_PurchaseRegister();
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

        #region VariableDeclarationSection
        UnitOfWork unitOfwork = new UnitOfWork();


        protected void Page_Load(object sender, EventArgs e)
        {



        #endregion
            if (!IsPostBack)
            {
                BindPurchageItems();
                BindStateCode();
                BindMonth();
                BindSaleItems();
                SqlDataSource1.SelectParameters["USERID"].DefaultValue = Common.LoggedInUserID();
                lvPurchaseStock.DataSourceID = "SqlDataSource1";
                lvPurchaseStock.DataBind();
            }

        }

        protected void btnSaveRegister_Click(object sender, EventArgs e)
        {

            if (ddlState.SelectedValue == "0")
            {
                uc_sucess.ErrorMessage = "Please Select State Name.";
                return;
            }
            try
            {
                var ITEM = unitOfwork.PurchaseRegisterDataRepositry.Find(F => F.SupplierInvoiceNo == txtSupplierInvoiceNumber.Text.Trim());
                GST_MST_PURCHASE_REGISTER register = new GST_MST_PURCHASE_REGISTER();
                if (ITEM == null)
                {
                    int count = 0;
                    register.SellerGSTN = txtSellerGSTIN.Text.Trim();
                    register.SellerName = txtSellerName.Text.Trim();
                    register.SellerAddress = txtSellerAddress.Text.Trim();
                    register.ReceiverName = txtRecieverName.Text.Trim();
                    register.ReceiverAddress = txtRecieverName.Text.Trim();
                    register.ConsigneeName = txtConsigneeName.Text.Trim();
                    register.ConsigneeAddress = txtConsigneeAddress.Text.Trim();
                    DateTime SOrderDate = DateTime.ParseExact(txtStockOrderDate.Text, "dd/MM/yyyy", null);
                    register.StockOrderDate = SOrderDate;
                    DateTime SInwardDate = DateTime.ParseExact(txtStockInwardDate.Text, "dd/MM/yyyy", null);
                    register.StockInwardDate = SInwardDate;
                    register.OrderPo = txtOrderPo.Text.Trim();
                    DateTime SOrderpoDate = DateTime.ParseExact(txtOrderPoDate.Text, "dd/MM/yyyy", null);
                    register.OrderPoDate = SOrderpoDate;
                    register.SupplierInvoiceNo = txtSupplierInvoiceNumber.Text.Trim();
                    DateTime SInvoiceDate = DateTime.ParseExact(txtSupplierInvoiceDate.Text, "dd/MM/yyyy", null);
                    register.SupplierInvoiceDate = SInvoiceDate;
                    register.SupplierInvoiceMonth = Convert.ToByte(ddlmonth.SelectedIndex.ToString());
                    register.StateCode = ddlState.SelectedItem.Value;
                    register.Insurance = Convert.ToDecimal(txtInsurance.Text.Trim());
                    register.Freight = Convert.ToDecimal(txtFreight.Text.Trim());
                    register.Status = true;
                    register.PackingAndForwadingCharges = Convert.ToDecimal(txtcharges.Text.Trim());
                    if (register.StockInwardDate >= register.StockOrderDate)
                    {
                        uc_sucess2.ErrorMessage = "Stock inward date should be less then stock order date.";
                        return;
                    }
                    register.OrderPo = txtOrderPo.Text.Trim();
                    // register.SupplierInvoiceNo = txtSupplierInvoiceNo.Text.Trim();
                    register.UserID = Common.LoggedInUserID();
                    // register.Status = true;
                    register.CreatedBy = Common.LoggedInUserID();
                    register.CreatedDate = DateTime.Now;
                    unitOfwork.PurchaseRegisterDataRepositry.Create(register);
                    unitOfwork.Save();
                    count = count + 1;

                    if (count > 0)
                    {
                        this.Master.SuccessMessage = "Data submitted successfully !";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                        uc_Purchase_Data.BindInvoiceNum();
                        BindPurchageItems();
                    }
                    else
                    {
                        this.Master.WarningMessage = "Enter valid data !.";
                        //uc_sucess.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    }

                    //uc_sucess.SuccessMessage = "Data Submited Successfully.";
                    ClearControl();

                }
                else
                {
                    uc_sucess.ErrorMessage = "Supplier Invoice No. Already Exist!";
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                this.Master.ErrorMessage = ex.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
            }
        }

        public void BindMonth()
        {
            try
            {
                ddlmonth.DataSource = typeof(EnumConstants.FinYear).ToList();
                ddlmonth.DataTextField = "Value";
                ddlmonth.DataValueField = "Key";
                ddlmonth.DataBind();
                ddlmonth.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        private void ClearControl()
        {
            txtSellerGSTIN.Text = string.Empty;
            txtSellerName.Text = string.Empty;
            txtSellerAddress.Text = string.Empty;
            txtRecieverAddress.Text = string.Empty;
            txtRecieverName.Text = string.Empty;
            txtConsigneeName.Text = string.Empty;
            txtConsigneeAddress.Text = string.Empty;
            txtInsurance.Text = string.Empty;
            txtOrderPo.Text = string.Empty;
            txtFreight.Text = string.Empty;
            txtcharges.Text = string.Empty;
            //ddlmonth.SelectedValue = string.Empty;
            // txtSupplierInvoiceMonth.Text = string.Empty;
            txtSupplierInvoiceNumber.Text = string.Empty;

        }



        //protected void lvItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        //{
        //   // DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        //    BindPurchageItems();
        //   // DataPager1.DataBind();
        //}
        private void BindStateCode()
        {

            ddlState.DataSource = unitOfwork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateCode = s.StateCode }).ToList();
            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        private void BindPurchageItems()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    unitOfwork = new UnitOfWork();
                    var purchitms = unitOfwork.PurchaseRegisterDataRepositry.Filter(f => f.UserID == loggedinUserId && f.Status == true).OrderByDescending(o => o.CreatedDate).ToList();
                    //var PurItms = unitOfwork.SaleRegisterDataRepositry.Filter(f => f. == loggedinUserId && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                    lvPurchageItems.DataSource = purchitms.ToList();
                    lvPurchageItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }


        private void BindSaleItems()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    lvSaleItems.DataSource = register.GetSaleItemsInvoices(loggedinUserId).OrderByDescending(o => o.InvoiceDate).ToList();
                    lvSaleItems.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }


        protected void lvPurchageItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dppurchaseregister.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindPurchageItems();
            dppurchaseregister.DataBind();
        }

        protected void lvSaleItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpsaleItems.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindSaleItems();
            dpsaleItems.DataBind();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void UpdatePurchaseRegister_Click(object sender, EventArgs e)
        //{
        //    // Uptil here Session contains data for valid Seller GSIN+Reciever GSTIN+ Consignee GSTIN
        //        if (Session["Seller"] != null)
        //            seller = (Seller)Session["seller"];

        //        // data in linecollection will always b there if the seller creates the first line item  of which he has quantity
        //        // second line item 
        //        // case 1 : hsn he does not have the stock or
        //        // case 2 : it is a first time seller is dealing in it
        //        if (Session["LineEntryCollections"] != null)
        //            lineCollection = (List<LineEntry>)Session["LineEntryCollections"];

        //    // 
        //    bool stockLineEntryUpdatedStatus = false;

        //    // count is used to check whether all the input fields value r provided by the user.
        //    int count = 0;

        //    // initilizing the class 
        //    addStockLineEntry = new StockRegisterLineEntry();

        //    if (!string.IsNullOrEmpty(txtGSTIN.Text))
        //    {
        //        addStockLineEntry.GstinNumber = txtGSTIN.Text;
        //        count++;
        //    }

        //    if (!string.IsNullOrEmpty(txtLineID.Text))
        //    {
        //        addStockLineEntry.LineID = Int32.Parse(txtLineID.Text);
        //        count++;
        //    }

        //    if (!string.IsNullOrEmpty(txtUserName.Text))
        //    {
        //        addStockLineEntry.UserName = txtUserName.Text;
        //        count++;
        //    }

        //    if (!string.IsNullOrEmpty(txtHSN.Text))
        //    {
        //        addStockLineEntry.Hsn = txtHSN.Text;
        //        count++;
        //    }

        //    if (!string.IsNullOrEmpty(txtQty.Text))
        //    {
        //        addStockLineEntry.Qty = Decimal.Parse(txtQty.Text);
        //        count++;
        //    }
        //    else
        //    {
        //        txtQty.Focus();
        //        Response.Write(@"<script language='javascript'>alert('Please enter quantity');</script>");
        //    }

        //    if (!string.IsNullOrEmpty(txtRate.Text))
        //    {
        //        addStockLineEntry.PerUnitRate = Decimal.Parse(txtRate.Text);
        //        count++;
        //    }
        //    else
        //    {
        //        txtRate.Focus();
        //        Response.Write(@"<script language='javascript'>alert('Please enter Rate');</script>");
        //    }

        //    //if (!string.IsNullOrEmpty(txtStockInwardDate.Text))
        //    //{
        //    //    addStockLineEntry.StockInwardDate = DateTime.Parse(txtStockInwardDate.Text);
        //    //    count++;
        //    //}
        //    //else
        //    //{
        //    //    txtStockInwardDate.Focus();
        //    //    Response.Write(@"<script language='javascript'>alert('Please enter Stock Inward Date');</script>");
        //    //}

        //    //if (!string.IsNullOrEmpty(txtStockOrderDate.Text))
        //    //{
        //    //    addStockLineEntry.StockOrderDate = DateTime.Parse(txtStockOrderDate.Text);
        //    //    count++;
        //    //}
        //    //else
        //    //{
        //    //    txtStockOrderDate.Focus();
        //    //    Response.Write(@"<script language='javascript'>alert('Please enter Stock Order Date');</script>");
        //    //}
        //    //if (!string.IsNullOrEmpty(txtOrderPONumber.Text))
        //    //{
        //    //    addStockLineEntry.OrderPO = txtOrderPONumber.Text;
        //    //    count++;
        //    //}
        //    //else
        //    //{
        //    //    txtOrderPONumber.Focus();
        //    //    Response.Write(@"<script language='javascript'>alert('Please enter Order PO');</script>");
        //    //}

        //    //// count ensures that all the values in the field r there in the UI , so that there is an atomic representation in the DBss
        //    //if (addStockLineEntry != null && count==9 )
        //    //{
        //    //    stockLineEntryUpdatedStatus = seller.UpdateSellerSaleRegisterData(addStockLineEntry);

        //    //}               

        //    //if(stockLineEntryUpdatedStatus)
        //    //{
        //    //    // if the sale ledger is getting intialized at the firs tline entry 
        //    //    if (seller.SaleLedger == null)
        //    //    {
        //    //        seller.SaleLedger = new SaleLedger();
        //    //        seller.SaleLedger.StockLineEntry = new List<StockRegisterLineEntry>();
        //    //        seller.SaleLedger.StockLineEntry.Add(addStockLineEntry);
        //    //        Response.Write(@"<script language='javascript'>alert('Purchase Register Updated');</script>");

        //    //        if (seller != null)
        //    //            Session["seller"]=seller ;

        //    //        // data in linecollection will always b there if the seller creates the first line item  of which he has quantity
        //    //        // second line item 
        //    //        // case 1 : hsn he does not have the stock or
        //    //        // case 2 : it is a first time seller is dealing in it
        //    //        if (lineCollection != null)
        //    //            Session["LineEntryCollections"] = lineCollection;

        //    //        Response.Redirect(prevPage, false);
        //    //        this.Context.ApplicationInstance.CompleteRequest();
        //    //    }
        //    //    else
        //    //    {
        //    //        // if the sale ledger is getting intialized after  first line entry 
        //    //        seller.SaleLedger.StockLineEntry.Add(addStockLineEntry);
        //    //        Response.Write(@"<script language='javascript'>alert('Purchase Register Updated');</script>");
        //    //        Response.Redirect(prevPage, false);
        //    //        this.Context.ApplicationInstance.CompleteRequest();
        //    //    }

        //    //}

        //    //else
        //    //    Response.Write(@"<script language='javascript'>alert('Purchase Not Register Updated');</script>");
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ////////protected void StockInwardDate_SelectionChanged(object sender, EventArgs e)
        ////////{
        ////////    DateTime selectedInwardDate = StockInwardDate.SelectedDate;
        ////////    DateTime today = DateTime.Today;

        ////////    if (selectedInwardDate <= today)
        ////////        txtStockInwardDate.Text = StockInwardDate.SelectedDate.ToShortDateString();
        ////////    else
        ////////    {
        ////////        txtStockInwardDate.Text = "";
        ////////        Response.Write(@"<script language='javascript'>alert('Invalid Date Selected');</script>");
        ////////    }
        ////////}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ////////protected void StockOrderDateCalendar_SelectionChanged(object sender, EventArgs e)
        ////////{
        ////////    DateTime todayDate = DateTime.Now;
        ////////    Calendar selectedOrderDate = (Calendar)sender;

        ////////    if (!string.IsNullOrEmpty(txtStockInwardDate.Text))
        ////////    {
        ////////        if (selectedOrderDate.SelectedDate <= StockInwardDate.SelectedDate)
        ////////        {
        ////////            txtStockOrderDate.Text = StockOrderDateCalendar.SelectedDate.ToShortDateString();
        ////////        }
        ////////        else
        ////////        {
        ////////            txtStockOrderDate.Text = "";
        ////////            Response.Write(@"<script language='javascript'>alert('Stock Order date has to be less than equal to stock inward date');</script>");
        ////////        }
        ////////    }
        ////////    else
        ////////        Response.Write(@"<script language='javascript'>alert('Please select stock inward date first');</script>");


        ////////}

        //protected void GoBack_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect(prevPage,false);
        //    //this.Context.ApplicationInstance.CompleteRequest();

        //}
    }
}