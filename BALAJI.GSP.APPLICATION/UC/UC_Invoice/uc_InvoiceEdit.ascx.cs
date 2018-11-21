using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using GST.Utility;
using DataAccessLayer;
using System.Globalization;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_InvoiceEdit : System.Web.UI.UserControl
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindInvoice(GST_TRN_INVOICE invoice)
        {
            try
            {
                gvInvoice_Items.DataSource = invoice.GST_TRN_INVOICE_DATA;
                gvInvoice_Items.DataBind();
                lkbUpdateInvoice.Visible = true;
            }catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void BindInvoice(int invoiceID)
        {
            try
            {
                var items = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceID == invoiceID).FirstOrDefault();
                Invoice = items;
                gvInvoice_Items.DataSource = items.GST_TRN_INVOICE_DATA.ToList();
                gvInvoice_Items.DataBind();
                lkbUpdateInvoice.Visible = true;
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            
        }
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            int rowIndex = currentRow.RowIndex;
            TextBox textBox = (sender as TextBox);

            var itemData = unitOfWork.ItemRepository.Find(f => f.ItemCode == textBox.Text.Trim());//seller.GetItemInformation(textBox.Text.Trim());
            int result;

            if (itemData != null)
            {
                #region Code is working fine for HSN search , but now need to get the logic of Purchase register
                if (int.TryParse(textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) && !string.IsNullOrEmpty(textBox.Text.Trim()) && textBox.Text.Length == 8)
                {
                    try
                    {
                        string type = string.Empty;
                        // added to check whether new HSN IS NOTIFIED OR NOT
                        if (itemData.IsNotified.Value)
                        {
                            //BindNotifiedHSN(itemData.GST_MST_ITEM_NOTIFIED, lvHSNData);
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalhsn", "$('#myModalhsn').modal();", true);
                            //upModal.Update();
                        }
                        if (itemData != null)
                        {
                            TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                            TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                            txtDescription.Text = itemData.Description;
                            txtUnit.Text = itemData.Unit;
                        }
                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");

                    }


                }

            }
            else
            {
                textBox.Text = string.Empty;
                TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");
                Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");
                TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
                Label txtTotal = (Label)currentRow.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");


                txtDescription.Text = string.Empty;
                txtUnit.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtTaxableValue.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtTotal.Text = string.Empty;
                //TODO:DISPLAY MESSAGE thet item does not exist.
            }

                #endregion

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            // reassigning the value of seller
            //if (Session["seller"] != null)
            //    seller = (Seller)Session["seller"];

            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");

            // getting the text box id
            string HSHtxtID = txtQty.ID;

            int rowIndex = currentRow.RowIndex;
            TextBox txtHsn = (TextBox)currentRow.FindControl("txtItemCode");
            TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
            Label txtTotal = (Label)currentRow.FindControl("txtTotal");
            TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
            Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");

            if ((txtQty.Text.ToString() != ""))
            {
                // decimal? purchaseLedger = seller.GetSellerStockInventory(txtHsn.Text.Trim(), sellerProfile.Id.ToString());

                //if (purchaseLedger.HasValue)
                //{
                #region to check qty entered is there in saleRegister or not
                //if (purchaseLedger.Value >= Convert.ToInt32(txtQty.Text.Trim()))
                //{
                //    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                //    if ((txtRate.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate.Text.ToString())) && (Convert.ToDecimal(txtRate.Text.ToString()) < Decimal.MaxValue))
                //    {
                // caluculate total 
                decimal totalRate = Common.CalculateTotal(Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToDecimal(txtRate.Text.Trim()));
                txtTotal.Text = totalRate.ToString();
                if (totalRate < Decimal.MaxValue)
                {
                    txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, !string.IsNullOrEmpty(txtDiscount.Text) ? Convert.ToDecimal(txtDiscount.Text) : 0).ToString();

                    if (!string.IsNullOrEmpty(txtDiscount.Text))
                    {
                        // Calculating the tax value, 
                        // for that discount given should be there , if not then else part will b called
                        if (Convert.ToDecimal(txtDiscount.Text) > 0.0m)
                        {  // tax value , unit * rate per unit * tax applicable
                            txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, Convert.ToDecimal(txtDiscount.Text)).ToString();
                        }
                        else
                        // get the focus to txt rate column with a message 
                        {
                            // TO DO ::  Use some other option to do this-- Aashis 
                            // Due to abnormal terminationn of 
                            //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                            //masterPage.ErrorMessage = "Please enter Discount";
                            txtRate.Focus();
                            return;
                        }
                    }
                }
            }
            else
            {
                txtTotal.Text = string.Empty;
                txtTaxableValue.Text = string.Empty;
            }

            //}
            //else
            //{
            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //    //TODO:I NEED TO APPY MODEL POP UP HERE TO UPDATE PURCHASE REGISTER
            //}
                #endregion
            //}
            //else
            //{

            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //}
            //}

        }
        private GST_TRN_INVOICE _invoice;
        public GST_TRN_INVOICE Invoice
        {
            get
            {
                _invoice = (GST_TRN_INVOICE)Session["Invoice"];
                return _invoice != null ? _invoice : (new GST_TRN_INVOICE());
            }
            set
            {
                Session["Invoice"] = value;
            }
        }

         //  private bool _invoiceSuccess;
        //public bool DataSuccess
        //{
        //    get
        //    {
        //        _invoiceSuccess = (bool)ViewState["invoiceSuccess"];
        //        return _invoiceSuccess != null ? _invoiceSuccess : false;
        //    }
        //    set
        //    {
        //        ViewState["invoiceSuccess"] = value;
        //    }
        //}

        cls_Invoice invoiceItem = new cls_Invoice();
        public event EventHandler UpdateInvoiceClick;
        protected void lkbUpdateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // GridView gv = (GridView)fvInvoice.FindControl("gvItems");
                List<GST_TRN_INVOICE_DATA> items = GetGVData();
                //var it = Invoice;
                if (Invoice != null)
                {

                    GST_TRN_INVOICE inv = new GST_TRN_INVOICE();


                    inv.InvoiceDate = DateTime.Now;
                    inv.InvoiceMonth = Convert.ToByte(DateTime.Now.Month);
                    //var totalInv=unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == Invoice.AspNetUser.Id).Count();// is
                    //var CurrentSrlNo = totalInv + 1;
                    //if (Invoice.ParentInvoiceID == null)
                    //{
                    inv.InvoiceNo = Invoice.InvoiceNo;// UniqueNoGenerate.RandomValue();//InvoiceOperation.InvoiceNo(Invoice.AspNetUser, Invoice.FinYear_ID.ToString(), CurrentSrlNo.ToString());
                   // }
                    inv.SellerUserID = Invoice.SellerUserID;
                    inv.ReceiverUserID = Invoice.ReceiverUserID;
                    inv.ConsigneeUserID = Invoice.ConsigneeUserID;
                    inv.OrderDate = Invoice.OrderDate;
                    inv.VendorID = Invoice.VendorID;
                    inv.TransShipment_ID = Invoice.TransShipment_ID;
                    inv.Freight = Invoice.Freight;
                    inv.Insurance = Invoice.Insurance;
                    inv.PackingAndForwadingCharges = Invoice.PackingAndForwadingCharges;
                    inv.ElectronicReferenceNo = Invoice.ElectronicReferenceNo;
                    inv.ElectronicReferenceNoDate = Invoice.ElectronicReferenceNoDate;
                    inv.InvoiceType = Invoice.InvoiceType;
                    inv.FinYear_ID = Invoice.FinYear_ID;
                    inv.IsInter = Invoice.IsInter;
                    inv.ReceiverFinYear_ID = Invoice.ReceiverFinYear_ID;
                    //  inv.ParentInvoiceID = Invoice.InvoiceID;
                    inv.TaxBenefitingState = Invoice.AspNetUser2.StateCode;
                    inv.Status = true;
                    //  var invoicePeriod=unitOfWork.FinYearRepository.Find(f=>f.Fin_ID== Invoice.FinYear_ID).Finyear_Format;

                    //  GST_TRN_INVOICE updateInvoice = new GST_TRN_INVOICE();

                    inv.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    inv.InvoiceSpecialCondition = Invoice.InvoiceSpecialCondition;
                    inv.CreatedDate = DateTime.Now;
                    inv.UpdatedDate = DateTime.Now;
                    inv.CreatedBy = Common.LoggedInUserID();
                    inv.UpdatedBy = Common.LoggedInUserID();
                    cls_PurchaseRegister insertPurchaseRegsiter = new cls_PurchaseRegister();
                    insertPurchaseRegsiter.LoggedinUserID = Common.LoggedInUserID();



                    //-------------End--------
                    // bool isInter =InvoiceOperation.GetConsumptionDestinationOfGoodsOrServices(Invoice.AspNetUser.StateCode, Invoice.AspNetUser2.StateCode, Invoice.AspNetUser1.StateCode);
                    bool isInter = Invoice.IsInter.Value;
                    bool isStateExampted = unitOfWork.StateRepository.Find(f => f.StateCode == Invoice.AspNetUser2.StateCode).IsExempted.Value;
                    // bool isExport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport);

                    bool isExported = false;
                    if (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export || (byte)Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport)
                    {
                        isExported = true;
                    }
                    bool isJobwork = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork);
                    bool isImport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import);
                    var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == Invoice.AspNetUser.StateCode);
                    var isUTState = stateData.UT.Value;
                    var isExempted = stateData.IsExempted.Value;
                    var isEcom = false;
                    var isUn = false;

                    var invLineItem = from invo in items
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          // InvoiceID = invoiceCreate.InvoiceID,
                                          InvoiceDataID = invo.InvoiceDataID,
                                          LineID = invo.LineID,
                                          // GST_MST_ITEM = invo.Item,
                                          Item_ID = invo.GST_MST_ITEM.Item_ID,
                                          Qty = invo.Qty,
                                          Rate = invo.Rate,
                                          TotalAmount = invo.TotalAmount,
                                          Discount = invo.Discount,
                                          TaxableAmount = invo.TaxableAmount,
                                          IGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value), //isJobwork ? 0 : (isUTState ? 0 : (isInter ? invo.Item.IGST : (isExport ? invo.Item.IGST : (isImport ? invo.Item.IGST : 0)))),
                                          IGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value),// isJobwork ? 0 : (isUTState ? 0 : (isInter ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : 0)))),
                                          CGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value),
                                          CGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value),
                                          SGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value),
                                          SGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value),
                                          UGSTRate = Calculate.TaxRate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value),
                                          UGSTAmt = Calculate.TaxCalculate(invo.GST_MST_ITEM, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value),
                                          CessRate = isJobwork ? 0 : invo.GST_MST_ITEM.CESS,
                                          CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CESS.Value)
                                          //TotalAmountWithTax = invo.TaxableAmount + IGSTAmt,
                                      };

                    bool invoiceHasParent = Invoice.ParentInvoiceID != null ? true : false;

                    if (!invoiceHasParent)
                    {
                        //Update old invoice status that is A or M---------------Start-------------
                        if (Invoice.InvoiceMonth == (byte)DateTime.Now.Month)
                        {
                            var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                            oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Amended);
                            // Invoice.InvoiceID = updateInvoice.InvoiceID;
                            var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                            unitOfWork.Save();
                        }
                        else
                        {
                            var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                            oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Modified);
                            //  updateInvoice.InvoiceID = updateInvoice.InvoiceID;
                            var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                            unitOfWork.Save();
                        }

                        inv.ParentInvoiceID = Invoice.InvoiceID;
                        var invoiceCreate = unitOfWork.InvoiceRepository.Create(inv);
                        unitOfWork.Save();
                        foreach (GST_TRN_INVOICE_DATA item in invLineItem)
                        {
                            item.InvoiceID = invoiceCreate.InvoiceID;
                            item.TotalAmountWithTax = item.TaxableAmount + item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;
                            unitOfWork.InvoiceDataRepository.Create(item);
                        }
                        unitOfWork.Save();
                        invoiceItem.AutoCorrectInvoice(invoiceCreate.InvoiceID);

                       
                        bool isSave = insertPurchaseRegsiter.UpdatePurchaseDataItemFromInvoice(inv);

                        GST_TRN_INVOICE_AUDIT_TRAIL auditStatus = new GST_TRN_INVOICE_AUDIT_TRAIL();
                        auditStatus.InvoiceID = invoiceCreate.InvoiceID;
                        auditStatus.AuditTrailStatus = (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A;
                        auditStatus.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Modify;
                        auditStatus.ReceiverInvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Modify;
                        auditStatus.ReceiverInvoiceActionDate = DateTime.Now;                        //
                        auditStatus.CreatedDate = DateTime.Now;
                        auditStatus.CreatedBy = Common.LoggedInUserID();
                        unitOfWork.InvoiceAuditTrailRepositry.Create(auditStatus);
                        unitOfWork.Save();
                    }
                    else
                    {

                        foreach (GST_TRN_INVOICE_DATA item in invLineItem)
                        {
                            //  item.InvoiceDataID = invp

                            item.InvoiceID = Invoice.InvoiceID;
                            item.TotalAmountWithTax = item.TaxableAmount + item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.InvoiceDataRepository.Update(item);
                        }
                        unitOfWork.Save();
                        invoiceItem.AutoCorrectInvoice(Invoice.InvoiceID);
                      
                        bool isSave = insertPurchaseRegsiter.UpdatePurchaseDataItemFromInvoice(Invoice);

                    }

                    gvInvoice_Items.DataSource = null;
                    gvInvoice_Items.DataBind();
                    lkbUpdateInvoice.Visible = false;
                    Invoice = new GST_TRN_INVOICE();
                    uc_sucess.SuccessMessage = "Data updated successfully.";
                    if (!string.IsNullOrEmpty(EditFrom))
                    {
                        if (EditFrom == "GSTR2")
                        {
                            cls_ITC itc = new cls_ITC();
                            itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.Purchase;
                            itc.SaveItcReceiver(inv);
                        }
                    }
                    UpdateInvoiceClick(sender, e);
                }
            }

            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public string EditFrom
        {
            get
            {

                return ViewState["invoiceSuccess"].ToString();
            }
            set
            {
                ViewState["invoiceSuccess"] = value;
            }
        }

        private List<GST_TRN_INVOICE_DATA> GetGVData()
        {
            List<GST_TRN_INVOICE_DATA> lineCollection = new List<GST_TRN_INVOICE_DATA>();
            foreach (GridViewRow row in gvInvoice_Items.Rows)
            {
                //  GridView gvSizePrice = (GridView)fvProduct.FindControl("gdvSizePrice");
                TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                TextBox txtGoodService = (TextBox)row.FindControl("txtGoodService");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                Label txtTotal = (Label)row.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                Label txtTaxableValue = (Label)row.FindControl("txtTaxableValue");

                if (!string.IsNullOrEmpty(txtGoodService.Text.Trim()))
                {
                    GST_TRN_INVOICE_DATA le = new GST_TRN_INVOICE_DATA();
                    le.InvoiceDataID = Convert.ToInt64(gvInvoice_Items.DataKeys[row.RowIndex].Values["InvoiceDataID"].ToString());
                    le.LineID = row.RowIndex;
                    le.Qty = Convert.ToDecimal(txtQty.Text.Trim());
                    le.GST_MST_ITEM = unitOfWork.ItemRepository.Find(f => f.ItemCode == txtItemCode.Text.Trim());
                    le.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                    le.TotalAmount = Convert.ToDecimal(txtTotal.Text.Trim());
                    if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                        le.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                    //le.AmountWithTaxLineIDWise = Convert.ToDecimal(((Label)row.FindControl("txtTaxableValue")).Text.Trim());
                    le.TaxableAmount = Convert.ToDecimal(txtTaxableValue.Text.Trim());
                    // Grand total of all line items
                    // le.TotalAmount += le.TotalAmount;
                    // grand total of all line items with tax
                    /// le.TotalAmountWithTax += le.TotalAmountWithTax;

                    lineCollection.Add(le);
                }
            }
            return lineCollection;
        }
    }
}