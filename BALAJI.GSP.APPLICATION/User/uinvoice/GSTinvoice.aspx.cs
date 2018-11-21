using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using businessAccessLayer;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Text;
using System.Security.Cryptography;
using com.B2B.GST.ExceptionHandling;
using System.Globalization;
using DataAccessLayer;
using GST.Utility;
using BusinessLogic.Repositories;
using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Web.Services;

namespace BALAJI.GSP.APPLICATION.User.uinvoice
{

    public partial class GSTinvoice : System.Web.UI.Page
    {

        #region VariableSection
        ExcelDB excelDB = new ExcelDB();
        List<LineEntry> lineCollection = new List<LineEntry>();
        Seller seller;
        string strInvoiceType = string.Empty;
        string SellerGSTN = string.Empty;
        string Mode = string.Empty;
        string SellerName = string.Empty;
        #endregion

        #region PAGE_LOAD
        public void Page_Load(object sender, EventArgs e)
        {
            uc_GSTNUsers.addInvoiceRedirect += uc_GSTNUsers_addInvoiceRedirect;
            uc_GSTNUsers.addInvoicechkRedirect += uc_GSTNUsers_addInvoicechkRedirect;
            //uc_GSTNUsers.addInvoiceUnchkRedirect += uc_GSTNUsers_addInvoiceUnchkRedirect;

            strInvoiceType = ddlInvoiceType.SelectedValue;
            // below code runs only once , during the page load
            if (!IsPostBack)
            {

                BindItems();
                BindInvoiceType();
                BindSpecialInvoiceType();
                BindUserVendor();
                ShowButton();
                var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Enabled = false;
                txtSellerGSTIN.Focus();
                txtSellerName.Focus();
                txtSellerAddress.Focus();
                txtRecieverGSTIN.Focus();
                txtRecieverName.Focus();
                txtRecieverAddress.Focus();
                txtRecieverState.Focus();
                txtRecieverStateCode.Focus();
                txtConsigneeGSTIN.Focus();
                txtConsigneeName.Focus();
                txtConsigneeAddress.Focus();
                txtConsigneeState.Focus();
                txtConsigneeStateCode.Focus();


                // THIS VARIABLE IS TAKEN TO POPULATE DATE INTO THE COLUMN AT EDIT MODE
                if (Request.QueryString["Mode"] != null)
                {
                    #region EDIT_AND_BACK
                    #endregion
                }
                else
                {

                    // seller is the instance , who will be created 
                    // when a user clicks on the Invoice section
                    if (seller == null)
                    {
                        seller = new Seller();

                        // as of now keeping in session to have it available across various postback
                        Session["seller"] = seller;

                        // keeping the invoice type in session , so as not to get lost in postback
                        Session["InvoiceType"] = strInvoiceType;


                        // check the impact 
                        Session["LineEntryCollections"] = lineCollection;

                        // read only value getting assigned of the seller
                        txtSellerGSTIN.Text = SellerGSTN;

                        //populate seller details based on userID or gsting ,fetch details from DB/Excel
                        GetSellerDetails();
                        //GetSellerPurchaseRegisterDetails();
                    }

                    else
                    {
                        GetSellerDetails();
                    }
                }

            }
        }

        //private void uc_GSTNUsers_addInvoiceUnchkRedirect(object sender, EventArgs e)
        //{
        //    var chkvalue = uc_GSTNUsers.GetchkValue;
        //    if (chkvalue == false)
        //    {
        //        this.WarningMessageAdd = "Want to clear all the Data !!";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addInvoiceModelWarningMessage", "$('#addInvoiceModelWarningMessage').modal();", true);
        //    }
        //}

        private void uc_GSTNUsers_addInvoicechkRedirect(object sender, EventArgs e)
        {

            if (uc_GSTNUsers.GetValue == 0)
            {
                BindSpecialInvoiceType();
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");

                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;

                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
                ResetFormData();
                HsnGridClearItem();

            }

        }


        //For clearing Grid Data 
        private void HsnGridClearItem()
        {
            foreach (GridViewRow gr in gvItems.Rows)
            {
                // find your all textbox
                TextBox txtGood = (TextBox)gr.FindControl("txtGoodService");
                TextBox txtGoodDesciption = (TextBox)gr.FindControl("txtGoodServiceDesciption");
                TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                TextBox txtUnit = (TextBox)gr.FindControl("txtUnit");
                TextBox txtRate = (TextBox)gr.FindControl("txtRate");
                Label txtTotal = (Label)gr.FindControl("txtTotal");
                TextBox txtDis = (TextBox)gr.FindControl("txtDiscount");
                Label txtTaxValue = (Label)gr.FindControl("txtTaxableValue");
                //Check they are not null
                if (txtGood != null)
                {
                    //Assign empty string
                    txtGood.Text = string.Empty;
                }
                if (txtGoodDesciption != null)
                {
                    //Assign empty string
                    txtGoodDesciption.Text = string.Empty;
                }
                if (txtQty != null)
                {
                    //Assign empty string
                    txtQty.Text = string.Empty;
                }
                if (txtUnit != null)
                {
                    //Assign empty string
                    txtUnit.Text = string.Empty;
                }
                if (txtRate != null)
                {
                    //Assign empty string
                    txtRate.Text = string.Empty;
                }
                if (txtTotal != null)
                {
                    //Assign empty string
                    txtTotal.Text = string.Empty;
                }
                if (txtDis != null)
                {
                    //Assign empty string
                    txtDis.Text = string.Empty;
                }
                if (txtTaxValue != null)
                {
                    //Assign empty string
                    txtTaxValue.Text = string.Empty;
                }
            }
        }

        //for tax consaltant
        public void uc_GSTNUsers_addInvoiceRedirect(object sender, EventArgs e)
        {
            var ddlSelectedSellerUserId = uc_GSTNUsers.GetUserUserProfile;
            txtSellerGSTIN.Text = ddlSelectedSellerUserId.GSTNNo;
            txtSellerName.Text = ddlSelectedSellerUserId.OrganizationName;
            txtSellerAddress.Text = ddlSelectedSellerUserId.Address;
            txtInvoiceDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == ddlSelectedSellerUserId.GSTNNo).FirstOrDefault();
                if (txtRecieverGSTIN.Text == profile.GSTNNo)
                {
                    this.Master.WarningMessage = "Seller and Reciever can not be same.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    txtSellerGSTIN.Text = "";
                    txtSellerName.Text = "";
                    txtSellerAddress.Text = "";
                }
            }
            //if (ddlSelectedSellerUserId.SelectedValue == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            //{
            //    var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
            //    if (GetSellerProfile.GSTNNo == profile.GSTNNo)
            //    {
            //        this.Master.WarningMessage = "Seller and Reciever can not be same.";
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
            //    }
            //}



            // HsnGridClearItem(


        }

        private void BindUserVendor()
        {
            var loggedUserID = uc_GSTNUsers.GetUserID;// 
            var createdBy = Common.LoggedInUserID();
            ddlVendor.DataSource = unitOfWork.VendorRepository.Filter(f => (f.UserID == loggedUserID && f.Status == true) || (f.CreatedBy == createdBy)).Select(s => new { s.VendorID, s.VendorName }).ToList();
            ddlVendor.DataTextField = "VendorName";
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("[ SELECT ]", "0"));
            txtFreight.Text = string.Empty;
            txtFreight.ReadOnly = false;
            ddlTransShipment.SelectedValue = "0";
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vendorID = Convert.ToInt32(ddlVendor.SelectedValue.ToString());// uc_GSTNUsers.GetUserID;// 
            var createdBy = Common.LoggedInUserID();
            var items = unitOfWork.TransShipmentRepositry.Filter(f => f.VendorID == vendorID && f.Status == true).Select(s => new { s.TransShipment_ID, s.TransShipmentNo }).ToList();
            ddlTransShipment.DataSource = items;
            ddlTransShipment.DataTextField = "TransShipmentNo";
            ddlTransShipment.DataValueField = "TransShipment_ID";
            ddlTransShipment.DataBind();
            ddlTransShipment.Items.Insert(0, new ListItem("[ SELECT ]", "0"));
            txtFreight.Text = String.Empty; //For clearing when select option is selected

        }

        protected void ddlTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransShipment.SelectedIndex > 0)
            {
                var transID = Convert.ToInt32(ddlTransShipment.SelectedValue.ToString());// uc_GSTNUsers.GetUserID;// 

                var freight = unitOfWork.TransShipmentRepositry.Find(f => f.TransShipment_ID == transID && f.Status == true).BillAmount;
                txtFreight.Text = freight.ToString();
                txtFreight.ReadOnly = true;
            }
        }

        private void ShowAdvanceVoucher()
        {
            if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Advance.ToString())
            {
                lblinvoiceNo.Text = "Voucher No. :";
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                ChangeImporterUIChange();
            }
        }


        private void BindSpecialInvoiceType()
        {
            var enumItems = typeof(EnumConstants.InvoiceSpecialCondition).ToList().Cast<EnumConstants.InvoiceSpecialCondition>().Except(new EnumConstants.InvoiceSpecialCondition[] { EnumConstants.InvoiceSpecialCondition.Advance });//.Where(w => w != EnumConstants.InvoiceSpecialCondition.Advance);
            rblInvoicePriority.DataSource = typeof(EnumConstants.InvoiceSpecialCondition).ToList();// Enumeration.ToList(typeof(EnumConstants.InvoiceSpecialCondition));
            rblInvoicePriority.DataTextField = "Value";
            rblInvoicePriority.DataValueField = "Key";
            rblInvoicePriority.DataBind();

            //rblInvoicePriority.Items.Insert(0, new ListItem(" [ Select Invoice Type ] ", "-1"));
            //TODO:OPTIMIZE THIS GIVEN BELOW CODE
            if (ddlInvoiceType.SelectedValue == EnumConstants.InvoiceType.B2B.ToString())
            {
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.B2CL.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.B2CS.ToString()));
                lblRegularMapped.Visible = false; //for show Map Advance label
                lboxRegularMapped.Visible = false; //for show Map Advance dropdown
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
            }
            else if (ddlInvoiceType.SelectedValue == EnumConstants.InvoiceType.B2C.ToString())
            {
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.Regular.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.Export.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.Import.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.JobWork.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.ReverseCharges.ToString()));
                rblInvoicePriority.Items.Remove(rblInvoicePriority.Items.FindByValue(EnumConstants.InvoiceSpecialCondition.RegularRCM.ToString()));
                lblRegularMapped.Visible = false; //for hide Map Advance label
                lboxRegularMapped.Visible = false; //for hide Map Advance dropdown
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
            }
            else if (ddlInvoiceType.SelectedValue == "-1")
            {
                lboxRegularChallanMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblRegularMapped.Visible = false;
                txtOrderDate.Visible = false;
                lblinvoiceNo.Visible = false;
            }
            else
            {

            }
        }
        protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSpecialInvoiceType();
            if (ddlInvoiceType.SelectedValue == EnumConstants.InvoiceType.B2B.ToString())
            {
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "Voucher No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;

                //if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Regular.ToString())
                //{
                //    lboxRegularMapped.Visible = false;
                //    lboxRegularChallanMapped.Visible = false;

                //}
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (ddlInvoiceType.SelectedValue == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (GetSellerProfile.GSTNNo == profile.GSTNNo)
                {
                    this.Master.WarningMessage = "Seller and Reciever can not be same.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
            }
            else
            {
                litRecieverGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                litConsigneeGSTIN.Text = "GSTIN";
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "Voucher No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            ShowAdvanceVoucher();
        }

        private void BindInvoiceType()
        {
            ddlInvoiceType.DataSource = typeof(EnumConstants.InvoiceType).ToList();// Enumeration.ToList(typeof(EnumConstants.InvoiceSpecialCondition));
            ddlInvoiceType.DataTextField = "Value";
            ddlInvoiceType.DataValueField = "Key";
            ddlInvoiceType.DataBind();
            //ddlInvoiceType.Items.Insert(0, new ListItem(" [ Select Invoice ] ", "-1"));
            ddlInvoiceType.Items.Remove(ddlInvoiceType.Items.FindByValue(EnumConstants.InvoiceType.Deemed_Exp.ToString()));
            ddlInvoiceType.Items.Remove(ddlInvoiceType.Items.FindByValue(EnumConstants.InvoiceType.Regular.ToString()));
            ddlInvoiceType.Items.Remove(ddlInvoiceType.Items.FindByValue(EnumConstants.InvoiceType.SEZsupplies_WithoutPayment.ToString()));
            ddlInvoiceType.Items.Remove(ddlInvoiceType.Items.FindByValue(EnumConstants.InvoiceType.SEZsupplies_WithPayment.ToString()));
        }

        private void BindRegularChallan(string RecGstn)
        {
            var createdBy = Common.LoggedInUserID();
            var items = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork && f.Status == true && f.CreatedBy == createdBy && f.AspNetUser1.GSTNNo == RecGstn).Select(s => new { s.InvoiceID, s.InvoiceNo }).ToList();
            lboxRegularChallanMapped.DataSource = items;
            lboxRegularChallanMapped.DataTextField = "InvoiceNo";
            lboxRegularChallanMapped.DataValueField = "InvoiceID";
            lboxRegularChallanMapped.DataBind();
            if (items.Count == 0)
            {
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
            }
        }

        private void BindRegularInv(string RecGstn)
        {
            var createdBy = Common.LoggedInUserID();
            var items = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Advance && f.Status == true && f.CreatedBy == createdBy && f.AspNetUser1.GSTNNo == RecGstn).Select(s => new { s.InvoiceID, s.InvoiceNo }).ToList();
            lboxRegularMapped.DataSource = items;
            lboxRegularMapped.DataTextField = "InvoiceNo";
            lboxRegularMapped.DataValueField = "InvoiceID";
            lboxRegularMapped.DataBind();
            if (items.Count == 0)
            {
                lboxRegularMapped.Visible = false;
                lblRegularMapped.Visible = false;
            }
        }
        private void BindItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { 
                            new DataColumn("HSN", typeof(string)),
                            new DataColumn("Description",typeof(string)),
            new DataColumn("Qty",typeof(string)) ,
            new DataColumn("Unit",typeof(string)) ,
            new DataColumn("Rate",typeof(string)) ,
            new DataColumn("Total",typeof(string)) ,
            new DataColumn("Discount",typeof(string)) ,
            new DataColumn("Taxable",typeof(string)) });

            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            dt.Rows.Add("", "", "", "", "", "", "", "");
            gvItems.DataSource = dt;
            gvItems.DataBind();

            gvItems.UseAccessibleHeader = true;
            gvItems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        #endregion

        // first pass
        #region Populate seller details based on GSTIN number/UserName

        private void BindImportInvoice()
        {
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString() || rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                txtSellerGSTIN.Focus();
                txtSellerGSTIN.Text = string.Empty;
                txtSellerAddress.Text = string.Empty;
                txtSellerName.Text = string.Empty;
                var logginID = Common.LoggedInUserID();
                var info = Common.UserManager.Users.Where(u => u.Id == logginID).FirstOrDefault();
                if (info != null)
                {
                    //amits change for tax consultant  ( txtRecieverGSTIN.Text = info.GSTNNo;)
                    var ddlSelectedSellerUserId = uc_GSTNUsers.GetUserUserProfile;
                    txtSellerGSTIN.ReadOnly = false;
                    txtSellerAddress.ReadOnly = false;
                    txtSellerName.ReadOnly = false;
                    txtSellerGSTIN.Text = string.Empty;
                    txtSellerAddress.Text = string.Empty;
                    txtSellerName.Text = string.Empty;


                    txtRecieverGSTIN.Text = ddlSelectedSellerUserId.GSTNNo;
                    txtRecieverAddress.Text = ddlSelectedSellerUserId.Address;
                    txtRecieverName.Text = ddlSelectedSellerUserId.OrganizationName;
                    txtRecieverStateCode.Text = ddlSelectedSellerUserId.StateCode;

                    txtRecieverGSTIN.ReadOnly = true;
                    txtRecieverAddress.ReadOnly = true;
                    txtRecieverName.ReadOnly = true;
                    txtRecieverAddress.ReadOnly = true;

                    txtConsigneeGSTIN.Text = ddlSelectedSellerUserId.GSTNNo;
                    txtConsigneeAddress.Text = ddlSelectedSellerUserId.Address;
                    txtConsigneeName.Text = ddlSelectedSellerUserId.OrganizationName;
                    txtConsigneeStateCode.Text = ddlSelectedSellerUserId.StateCode;

                    txtConsigneeGSTIN.ReadOnly = true;
                    txtConsigneeAddress.ReadOnly = true;
                    txtConsigneeName.ReadOnly = true;
                    txtConsigneeAddress.ReadOnly = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.ReverseCharges.ToString())
            {
                txtSellerGSTIN.Text = string.Empty;
                txtSellerAddress.Text = string.Empty;
                txtSellerName.Text = string.Empty;
                var logginID = Common.LoggedInUserID();
                var info = Common.UserManager.Users.Where(u => u.Id == logginID).FirstOrDefault();
                if (info != null)
                {
                    //amits change for tax consultant  ( txtRecieverGSTIN.Text = info.GSTNNo;)
                    var ddlSelectedSellerUserId = uc_GSTNUsers.GetUserUserProfile;

                    txtSellerGSTIN.ReadOnly = false;
                    txtSellerAddress.ReadOnly = false;
                    txtSellerName.ReadOnly = false;

                    txtRecieverGSTIN.Text = ddlSelectedSellerUserId.GSTNNo;
                    txtRecieverAddress.Text = ddlSelectedSellerUserId.Address;
                    txtRecieverName.Text = ddlSelectedSellerUserId.OrganizationName;
                    txtRecieverStateCode.Text = ddlSelectedSellerUserId.StateCode;

                    txtRecieverGSTIN.ReadOnly = true;
                    txtRecieverAddress.ReadOnly = true;
                    txtRecieverName.ReadOnly = true;
                    txtRecieverAddress.ReadOnly = true;

                    txtConsigneeGSTIN.Text = ddlSelectedSellerUserId.GSTNNo;
                    txtConsigneeAddress.Text = ddlSelectedSellerUserId.Address;
                    txtConsigneeName.Text = ddlSelectedSellerUserId.OrganizationName;
                    txtConsigneeStateCode.Text = ddlSelectedSellerUserId.StateCode;

                    txtConsigneeGSTIN.ReadOnly = true;
                    txtConsigneeAddress.ReadOnly = true;
                    txtConsigneeName.ReadOnly = true;
                    txtConsigneeAddress.ReadOnly = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                txtRecieverGSTIN.ReadOnly = false;
                txtRecieverAddress.ReadOnly = false;
                txtRecieverName.ReadOnly = false;
                txtConsigneeGSTIN.ReadOnly = false;
                txtConsigneeAddress.ReadOnly = false;
                txtConsigneeName.ReadOnly = false;
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CS.ToString())
            {
                txtRecieverGSTIN.ReadOnly = false;
                txtRecieverAddress.ReadOnly = false;
                txtRecieverName.ReadOnly = false;
                txtConsigneeGSTIN.ReadOnly = false;
                txtConsigneeAddress.ReadOnly = false;
                txtConsigneeName.ReadOnly = false;
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CL.ToString())
            {
                txtRecieverGSTIN.ReadOnly = false;
                txtRecieverAddress.ReadOnly = false;
                txtRecieverName.ReadOnly = false;
                txtConsigneeGSTIN.ReadOnly = false;
                txtConsigneeAddress.ReadOnly = false;
                txtConsigneeName.ReadOnly = false;
            }
            else
            {
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();

                txtRecieverGSTIN.ReadOnly = false;
                txtRecieverStateCode.ReadOnly = true;
                txtRecieverAddress.ReadOnly = true;
                txtRecieverName.ReadOnly = true;

                txtConsigneeGSTIN.ReadOnly = false;
                txtConsigneeName.ReadOnly = true;
                txtConsigneeAddress.ReadOnly = true;
                txtConsigneeStateCode.ReadOnly = true;

                txtSellerGSTIN.ReadOnly = true;
                txtSellerAddress.ReadOnly = true;
                txtSellerName.ReadOnly = true;
            }
        }
        private void ChangeImporterUIChange()
        {
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                txtSellerGSTIN.Focus();
                txtSellerGSTIN.MaxLength = 15;
                litSelletGSTIN.Text = "Vendor Name";
                litSellerName.Text = "Email ID";

            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.ReverseCharges.ToString())
            {
                txtSellerGSTIN.Focus();
                txtSellerGSTIN.MaxLength = 10;
                litSelletGSTIN.Text = "PAN No.";
                litSellerName.Text = "Name ";
            }
            else
            {
                txtSellerGSTIN.MaxLength = 15;
                litSelletGSTIN.Text = "GSTIN No.";
                litSellerName.Text = "Name ";
            }
        }

        public void GetSellerDetails()
        {
            try
            {
                var userProfile = uc_GSTNUsers.GetUserUserProfile;
                if (userProfile != null)
                {

                    // TO DO : 
                    // Implement 8/52 of Report_On_GSTRegistration
                    // on client as well as server side
                    // Implement and call ValidateStructureGSTIN on server side

                    try
                    {
                        if (seller == null)
                        {
                            seller = new Seller();
                        }
                        txtSellerGSTIN.Text = userProfile.GSTNNo;
                        txtSellerName.Text = userProfile.OrganizationName;
                        txtSellerAddress.Text = userProfile.Address;
                        txtInvoiceDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    }
                    catch (NullReferenceException Nullex)
                    {
                        NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //BalajiGSPLogger logger = new //BalajiGSPLogger();
                        //logger.LogError(Request.Path, Nullex);
                    }
                    catch (Exception ex)
                    {
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                        // check in event viewer -TO DO
                        //BalajiGSPLogger logger = new //BalajiGSPLogger();
                        ////logger.LogError(Request.Path, ex);
                    }

                    // Seller data strored in session
                    if (seller != null)
                        Session["seller"] = seller;

                    // if user selects to invoke export or advance option, this option is import
                    if (!string.IsNullOrEmpty(strInvoiceType))
                        Session["InvoiceType"] = strInvoiceType;

                    //if correct data is entered and then only this event will b fired 
                    txtRecieverGSTIN.ReadOnly = false;
                    Page.Form.DefaultFocus = txtRecieverGSTIN.ClientID;
                }
                else
                {
                    txtSellerName.Text = string.Empty;
                    txtSellerAddress.Text = string.Empty;
                    txtInvoiceNumber.Text = string.Empty;
                    txtInvoiceDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region PopulateSellerDetailsOnUI

        private string GetSellerUserID
        {
            get
            { return uc_GSTNUsers.GetUserID; }
        }
        private ApplicationUser GetSellerProfile
        {
            get
            { return uc_GSTNUsers.GetUserUserProfile; }
        }
        #endregion

        #region Populate_SellerData

        public void txtSellerName_TextChanged(object sender, EventArgs e)
        {
            txtSellerAddress.Focus();
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.Email == txtSellerName.Text.Trim()).FirstOrDefault();

                // This checks whether we have value or not in txtRecieverGSTIN
                // below function checks for null and "" and the next condition is whether the GSTIN is valid or not
                if (!string.IsNullOrEmpty(txtSellerName.Text.Trim()))
                {
                    if (profile == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                        return;
                    }
                    else
                    {
                        txtSellerGSTIN.Text = profile.FirstName;
                        txtSellerName.Text = profile.Email;
                        txtSellerAddress.Text = profile.Address;
                    }
                }
            }
        }
        #endregion

        // control passed here after seller details r populated and only field available 'n' activated is Reciever gstin- Second pass
        #region Populate_RecieverData
        public void txtRecieverGSTIN_TextChanged(object sender, EventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtRecieverGSTIN.Text.Trim()).FirstOrDefault();
                if (!string.IsNullOrEmpty(txtRecieverGSTIN.Text.Trim()))
                {
                    if (profile == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                        return;
                    }
                    else
                    {
                        txtRecieverGSTIN.Text = profile.GSTNNo;
                        txtRecieverName.Text = profile.OrganizationName;
                        txtRecieverAddress.Text = profile.Address;
                    }
                }
            }

            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Regular.ToString())
            {
                lblRegularMapped.Visible = true;
                lboxRegularMapped.Visible = true;
                lblRegularChallanMapped.Visible = true;
                lboxRegularMapped.Visible = true;
                lboxRegularChallanMapped.Visible = true;
                lblRegularChallanMapped.Visible = true;
                BindRegularInv(txtRecieverGSTIN.Text);//by pass receiver gstn for populate advance voucher no respected to gstn no.
                BindRegularChallan(txtRecieverGSTIN.Text);//by pass receiver gstn for populate job work challan no respected to gstn no.
            }
            else
            {
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
            }
            // This checks whether we have value or not in txtRecieverGSTIN
            // below function checks for null and "" and the next condition is whether the GSTIN is valid or not
            if (!string.IsNullOrEmpty(txtRecieverGSTIN.Text.Trim()) && rblInvoicePriority.SelectedValue.ToString() != EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {

                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtRecieverGSTIN.Text.Trim()).FirstOrDefault();
                if (profile == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    return;
                }
                ClearConsigneeFieldData();
                if (GetSellerProfile.GSTNNo == profile.GSTNNo)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    txtRecieverGSTIN.Text = string.Empty;
                    return;
                }
                // check receiver GSTIN is valid or not
                // TO DO: NEED TO TALK TO REST API OF GSTIN IN FUTURE TO VALIDATE THE GSTIN 
                // IFF NOT REGISTED WITH US                
                if (txtRecieverGSTIN.Text.Trim() != txtSellerGSTIN.Text.Trim())
                {
                    try
                    {
                        txtRecieverName.Text = profile.OrganizationName;
                        txtRecieverAddress.Text = profile.Address;
                        txtRecieverStateCode.Text = profile.StateCode;
                    }
                    catch (NullReferenceException Nullex)
                    {
                        NullReferenceException nullEx = new NullReferenceException("Receiver Object Creation Issues");
                    }
                    catch (Exception ex)
                    {
                        cls_ErrorLog ob = new cls_ErrorLog();
                        cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();

                    }

                    //if correct data is entered and then only this event will b fired 
                    txtConsigneeGSTIN.ReadOnly = false;


                    // setting the focus, where user needs to input data
                    Page.Form.DefaultFocus = txtConsigneeGSTIN.ClientID;

                }
                else if (txtRecieverGSTIN.Text == GetSellerProfile.GSTNNo)
                {
                    //TODO:MESSAGE HERE
                    // uc_sucess.ErrorMessage = "You(seller) and Reciever can not be same.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    txtRecieverGSTIN.Text = string.Empty;
                    return;
                }
                // Ensure seller GSTIN and receiver GSTIN number is same or not-if block ends
                else if (txtRecieverGSTIN.Text == "")
                {  // Below part is user enters blank gstin ,after first entry of valid gstin - V imp code
                    ClearRecieverField();
                    // invalid data entered case 
                    txtGoodService1.ReadOnly = true;
                    // if this condition is false in the main if block ,
                    // then logically this value will be true in all else's
                    txtConsigneeGSTIN.ReadOnly = true;
                }
            }
            else
            {
                if (rblInvoicePriority.SelectedValue.ToString() != EnumConstants.InvoiceSpecialCondition.Export.ToString())
                {
                    ClearRecieverField();
                    //txtGoodService1.ReadOnly = true;

                    // if this condition is false in the main if block ,
                    // then logically this value will be true in all else's
                    txtConsigneeGSTIN.ReadOnly = true;
                    //TODO:POP uPsHOwn HERE FOR MESSSAGE --ashish
                    //Response.Write("HttpUtility.HtmlEncode(<script>alert('Error !!! Reciever GSTIN Not Found!');</script>)");
                    //return;
                }
            }

        }

        private void ClearRecieverField()
        {
            txtRecieverGSTIN.Text = string.Empty;
            txtRecieverName.Text = string.Empty;
            txtRecieverAddress.Text = string.Empty;
            txtRecieverState.Text = string.Empty;
            txtRecieverStateCode.Text = string.Empty;
        }
        #endregion

        // control passed here after Reciever details r populated and only field available 'n' activated is Consignee gstin- Third pass
        #region Populate_ConsigneeData
        public void txtConsigneeGSTIN_TextChanged(object sender, EventArgs e)
        {
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
                if (!string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()))
                {
                    if (profile == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                        return;
                    }
                    else
                    {
                        txtConsigneeGSTIN.Text = profile.GSTNNo;
                        txtConsigneeName.Text = profile.FirstName;
                        txtConsigneeAddress.Text = profile.Address;
                    }
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CS.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
                if (!string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()))
                {
                    if (profile == null)
                    {
                        //uc_sucess.ErrorMessage = "Their is no reciever found.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                        //txtRecieverGSTIN.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        txtConsigneeGSTIN.Text = profile.GSTNNo;
                        txtConsigneeName.Text = profile.FirstName;
                        txtConsigneeAddress.Text = profile.Address;
                    }
                }
            }

            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CL.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
                if (!string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()))
                {
                    if (profile == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                        return;
                    }
                    else
                    {
                        txtConsigneeGSTIN.Text = profile.GSTNNo;
                        txtConsigneeName.Text = profile.FirstName;
                        txtConsigneeAddress.Text = profile.Address;
                    }
                }
            }

            // This checks whether we have value or not in txtConsigneeGSTIN
            if (!string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()) && rblInvoicePriority.SelectedValue.ToString() != EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                // check receiver GSTIN is valid or not
                // TO DO: NEED TO TALK TO REST API OF GSTIN IN FUTURE TO VALIDATE THE GSTIN 
                // IFF NOT REGISTED WITH US

                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
                if (profile == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    txtConsigneeGSTIN.Text = string.Empty;
                    return;
                }

                if (GetSellerUserID == profile.GSTNNo)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    txtConsigneeGSTIN.Text = string.Empty;
                    return;
                }
                if (profile != null)
                {
                    // Ensure seller GSTIN and Consignee GSTIN number is same or not
                    // last condittion is there , to put in place the functionality does not break, that is last else gets executed in this case
                    // in this if elese block
                    if (txtConsigneeGSTIN.Text.Trim() != txtSellerGSTIN.Text.Trim() && txtConsigneeGSTIN.Text != "")
                    {
                        if (txtRecieverGSTIN.Text.Trim() == txtConsigneeGSTIN.Text.Trim())
                        {
                            try
                            {
                                txtConsigneeGSTIN.Text = profile.GSTNNo;
                                txtConsigneeName.Text = profile.OrganizationName;
                                txtConsigneeAddress.Text = profile.Address;
                                txtConsigneeStateCode.Text = profile.StateCode;
                            }
                            catch (NullReferenceException Nullex)
                            {
                                NullReferenceException nullEx = new NullReferenceException("Consignee Object Creation Issues");
                                GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                                // TO DO- Pramod , make this code work
                                //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);
                            }
                            catch (Exception ex)
                            {
                                GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                            }
                        }
                        else
                        {
                            try
                            {

                                txtConsigneeGSTIN.Text = profile.GSTNNo;
                                txtConsigneeName.Text = profile.OrganizationName;
                                txtConsigneeAddress.Text = profile.Address;
                                //txtConsigneeState.Text = seller.Consignee.StateName;
                                txtConsigneeStateCode.Text = profile.StateCode;
                            }
                            catch (NullReferenceException Nullex)
                            {
                                NullReferenceException nullEx = new NullReferenceException("Consignee Object Creation Issues");
                                GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                            }
                            catch (Exception ex)
                            {
                                GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                            }
                        }
                    }
                    else if (txtConsigneeGSTIN.Text == "")
                    {

                        // Below part is user enters blank gstin ,after first entry of valid gstin - V imp code
                        ClearConsigneeFieldData();
                        // TO DO ::  Use some other option to do this-- Aashis 
                        return;
                    }
                } // check receiver GSTIN is valid or not-if block ends
                else
                {
                    ClearConsigneeFieldData();
                }
            }// This checks whether we have value or not in txtConsigneeGSTIN- if block ends
            else
            {
                if (rblInvoicePriority.SelectedValue.ToString() != EnumConstants.InvoiceSpecialCondition.Export.ToString())
                {

                    ClearConsigneeFieldData();
                    txtGoodService1.ReadOnly = true;
                }
            }

        }
        #region GoodsServiceData
        protected void GetGoodsOrServiceInfo(object sender, EventArgs e)
        {
            if (Session["Seller"] != null)
                seller = (Seller)Session["seller"];

            //if (Session["LineEntryCollections"] != null)
            //    lineCollection = (List<LineEntry>)Session["LineEntryCollections"];

            // result is used as out parameter in TryParse
            int result;

            // to figure the max rows in purchase ledger of the seller
            int maxCount = 0;

            // new type of grid introduced to simplify
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            int rowIndex = currentRow.RowIndex;

            // to figure out the current row index of stockLineEntry 
            int currentStockLineEntryIndex = 0;
            // getting the value from the sender obj
            TextBox textBox = (sender as TextBox);

            // getting the text box id
            string HSHtxtID = textBox.ID;

            // visibility is kept public , 
            // coz this value checks whether line id is exist and edit/modify happened on the same id
            // in fact , inputStatusGoodServiceCode is requird for Scenario-3
            bool status;
            int chkLineIDVal;
            bool inputStatusGoodServiceCode = int.TryParse(textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
            int goodServiceCode = result;
            string strgoodServiceCode = string.Empty;

            // if goodServiceCode =0 then intializes the strgoodServiceCode to ""
            if (goodServiceCode == 0)
                strgoodServiceCode = "";
            //TO DO   - Very Important
            // NEED TO IMPLEMENT hsn or sac DESCRIPTION  via search WITH AUTO COMPLETE 

            // this is the lightwieght check
            // GST_MST_ITEM itemData = new GST_MST_ITEM();
            var itemData = unitOfWork.ItemRepository.Find(f => f.ItemCode == textBox.Text.Trim());//seller.GetItemInformation(textBox.Text.Trim());

            if (itemData != null)
            {
                #region Code is working fine for HSN search , but now need to get the logic of Purchase register
                if (int.TryParse(textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) && !string.IsNullOrEmpty(textBox.Text.Trim()) && textBox.Text.Length >= 2 && textBox.Text.Length <= 8)
                {
                    try
                    {
                        string type = string.Empty;
                        // added to check whether new HSN IS NOTIFIED OR NOT
                        if (itemData.IsNotified.Value)
                        {
                            BindNotifiedHSN(itemData.GST_MST_ITEM_NOTIFIED, lvHSNData);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalhsn", "$('#myModalhsn').modal();", true);
                            upModal.Update();
                        }
                        if (itemData != null)
                        {
                            TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodServiceDesciption");
                            TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");
                            txtDescription.Text = itemData.Description;
                            txtUnit.Text = itemData.Unit;
                            txtQty.Focus();
                        }

                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    }
                    catch (System.FormatException formatEx)
                    {
                        System.FormatException formatErr = new System.FormatException("Converting HSN into Int, if alphanum val is entered.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    }
                    catch (System.OverflowException overFlw)
                    {
                        System.OverflowException overFlow = new System.OverflowException("Value supplied exceed the range of datatype.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    }
                    catch (Exception ex)
                    {
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                        // check in event viewer -TO DO
                        //BalajiGSPLogger logger = new //BalajiGSPLogger();
                        ////logger.LogError(Request.Path, ex);
                    }

                }

            }
            else
            {
                textBox.Text = string.Empty;
                TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodServiceDesciption");
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
                txtQty.Focus();
                //TODO:DISPLAY MESSAGE thet item does not exist.
            }

                #endregion
        }
        #endregion
        private void ClearConsigneeFieldData()
        {
            txtConsigneeGSTIN.Text = string.Empty;
            txtConsigneeName.Text = string.Empty;
            txtConsigneeAddress.Text = string.Empty;
            txtConsigneeState.Text = string.Empty;
            txtConsigneeStateCode.Text = string.Empty;
            hdnSConsigneeStateName.Value = string.Empty;
        }

        private void CreateLineEntry()
        {
            //if (seller.SellerStateCode != null && seller.Reciever.StateCode != null && seller.Consignee.StateCode != null)
            //    // At this point all the relevant data is recieved from seller, reciever and consignee. 
            //    txtGoodService1.ReadOnly = false;
        }
        #endregion

        #region CreateInvoice
        /// <summary>
        /// Appropriate Invoice will be created , 
        /// If correct HSN no is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Seller CreateInvoice()
        {
            seller = seller.CreateInvoice(seller, EnumConstants.InvoiceType.B2B.ToString());
            return seller;
        }


        #endregion

        // When the Map2Ui is succesfull, then 3 columns gets disabled, 4.3 pass
        #region DisableReadOnly
        private void DisableReadOnlyLineEntry(List<LineEntry> lines, int lineID)
        {
            foreach (var l in lines)
            {
                // line id 1 will be for the first row of invoice
                switch (lineID)
                {
                    case 1:
                        txtGoodService1.ReadOnly = false;
                        txtQty1.ReadOnly = false;
                        txtRate1.ReadOnly = false;
                        txtDiscount1.ReadOnly = false;
                        break;

                    case 2:
                        txtGoodService2.ReadOnly = false;
                        txtQty2.ReadOnly = false;
                        txtRate2.ReadOnly = false;
                        txtDiscount2.ReadOnly = false;
                        break;

                    case 3:
                        txtGoodService3.ReadOnly = false;
                        txtQty3.ReadOnly = false;
                        txtRate3.ReadOnly = false;
                        txtDiscount3.ReadOnly = false;
                        break;
                    case 4:
                        txtGoodService4.ReadOnly = false;
                        txtQty4.ReadOnly = false;
                        txtRate4.ReadOnly = false;
                        txtDiscount4.ReadOnly = false;
                        break;

                    case 5:
                        txtGoodService5.ReadOnly = false;
                        txtGoodService1.ReadOnly = false;
                        txtQty5.ReadOnly = false;
                        txtRate5.ReadOnly = false;
                        txtDiscount5.ReadOnly = false;
                        break;

                    case 6:
                        txtGoodService6.ReadOnly = false;
                        txtQty6.ReadOnly = false;
                        txtRate6.ReadOnly = false;
                        txtDiscount6.ReadOnly = false;
                        break;

                    case 7:
                        txtGoodService7.ReadOnly = false;
                        txtQty7.ReadOnly = false;
                        txtRate7.ReadOnly = false;
                        txtDiscount7.ReadOnly = false;
                        break;

                    case 8:
                        txtGoodService8.ReadOnly = false;
                        txtQty8.ReadOnly = false;
                        txtRate8.ReadOnly = false;
                        txtDiscount8.ReadOnly = false;
                        break;

                    case 9:
                        txtGoodService9.ReadOnly = false;
                        txtQty9.ReadOnly = false;
                        txtRate9.ReadOnly = false;
                        txtDiscount9.ReadOnly = false;
                        break;

                    case 10:
                        txtGoodService10.ReadOnly = false;
                        txtQty10.ReadOnly = false;
                        txtRate10.ReadOnly = false;
                        txtDiscount10.ReadOnly = false;
                        break;

                }
                break;


            }
        }
        #endregion

        // 5 th pass- ideally
        #region QtyCal

        protected void QtyCalculate(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");

            var sellerProfile = uc_GSTNUsers.GetUserUserProfile;

            // getting the text box id
            string HSHtxtID = txtQty.ID;

            int rowIndex = currentRow.RowIndex;
            TextBox txtHsn = (TextBox)currentRow.FindControl("txtGoodService");
            TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
            Label txtTotal = (Label)currentRow.FindControl("txtTotal");
            TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
            Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");

            if (seller == null)
            { seller = new Seller(); }

            if ((txtQty.Text.ToString() != ""))
            {
                #region to check qty entered is there in saleRegister or not
                //      if (purchaseLedger.Value >= Convert.ToDecimal(txtQty.Text.Trim()))
                //     {
                //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                if ((txtRate.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate.Text.ToString())) && (Convert.ToDecimal(txtRate.Text.ToString()) < Decimal.MaxValue))
                {
                    // caluculate total 
                    decimal totalRate = Common.CalculateTotal(Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToDecimal(txtRate.Text.Trim()));
                    txtTotal.Text = totalRate.ToString("0.00");
                    if (totalRate < Decimal.MaxValue)
                    {
                        txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, !string.IsNullOrEmpty(txtDiscount.Text) ? Convert.ToDecimal(txtDiscount.Text) : 0).ToString();

                        if (!string.IsNullOrEmpty(txtDiscount.Text))
                        {
                            // Calculating the tax value, 
                            // for that discount given should be there , if not then else part will b called
                            if (Convert.ToDecimal(txtDiscount.Text) > 0.0m)
                            {  // tax value , unit * rate per unit * tax applicable
                                txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, Convert.ToDecimal(txtDiscount.Text)).ToString("0.00");
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
                #endregion

            }
            switch (((TextBox)sender).ID)
            {
                case "txtQty":
                    txtRate.Focus();
                    break;
                case "txtRate":
                    txtDiscount.Focus();
                    break;
            }
        }

        private decimal? GetItemTotalQtyFromInvoiceGrid(string itemCode)
        {
            IEnumerable<GridViewRow> rows = gvItems.Rows.Cast<GridViewRow>();
            var item = from row in rows
                       select new
                       {
                           ItemCode = ((TextBox)row.FindControl("txtGoodService")).Text,
                           Qty = !string.IsNullOrEmpty(((TextBox)row.FindControl("txtQty")).Text) ? Convert.ToInt32(((TextBox)row.FindControl("txtQty")).Text.Trim()) : 0
                       };
            return item.Where(s => s.ItemCode == itemCode).Sum(w => w.Qty);
        }
        protected void QtyCal(object sender, EventArgs e)
        {
            // getting the complete state of LineEntries from the 
            // explicit casting is required
            if (Session["LineEntryCollections"] != null)
            {

                lineCollection = (List<LineEntry>)Session["LineEntryCollections"];
            }

            // reassigning the value of seller
            if (Session["seller"] != null)
                seller = (Seller)Session["seller"];



            // get the id of text box that caused this event fired and subsequent post back
            // getting the value from the sender obj
            TextBox textBox = (sender as TextBox);

            // getting the text box id
            string HSHtxtID = textBox.ID;

            int chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
            // status tells us whether we need to go for update of line collection or not
            // this is to keep the sancity of the object
            bool status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

            //based on above id of the control, pick the specific line id 
            // do BVA on rate, dicoount(also less than 100)
            // do BVA on total, taxable value and all
            // get the values stored in qty, assign it to qty 
            // get the values strored in the rate , check not null, assign it to perUnitRate
            // get the value of discount, assign it to discount
            // Calcualte total=qty * rate -discount 

            switch (HSHtxtID)
            {

                #region txt qty 1 for line id --1
                case "txtQty1":


                    for (int i = 0; i < lineCollection.Count; i++)
                    {
                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    #region to check qty entered is there in saleRegister or not
                                    //if (seller.SaleLedger.StockLineEntry[i].Qty >= lineCollection[i].Qty)
                                    //{
                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate1.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount1.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount1.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount1.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount1.Text.ToString())) && (Convert.ToDecimal(txtDiscount1.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount1.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }

                                    else
                                    // get the focus to txt rate column ( with a message 
                                    {
                                        Response.Write(@"<script language='javascript'>alert('Please enter Rate');</script>");
                                        Page.Form.DefaultFocus = txtRate1.ClientID;
                                        return;
                                    }

                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate1.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                //masterPage.ErrorMessage = "Please enter Discount";
                                                Page.Form.DefaultFocus = txtRate1.ClientID;
                                                //masterPage.ShowModalPopup();
                                                return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount1.Text.ToString()) && (status))
                                        {
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            if ((txtDiscount1.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount1.Text.ToString())) && (Convert.ToDecimal(txtDiscount1.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount1.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            //// TO DO ::  Use some other option to do this-- Aashis 
                                            //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            ////masterPage.ErrorMessage = "Please enter Rate";
                                            ////masterPage.ShowModalPopup();
                                            //Page.Form.DefaultFocus = txtRate1.ClientID;
                                            //return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }
                        }
                        //assign it to UI
                        Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/UpdateSaleRegister.aspx", false);
                        //    this.Context.ApplicationInstance.CompleteRequest();
                        //}
                                    #endregion
                    }
                    break;
                #endregion

                #region txt qty for line id--2
                case "txtQty2":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate2.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate2.Text.ToString())) && (Convert.ToDecimal(txtRate2.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount2.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount2.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount2.Text.ToString())) && (Convert.ToDecimal(txtDiscount2.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate2.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate2.Text.ToString())) && (Convert.ToDecimal(txtRate2.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate2.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount2.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount2.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount2.Text.ToString())) && (Convert.ToDecimal(txtDiscount2.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }


                    break;
                #endregion

                #region txt qty for line id-3
                case "txtQty3":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate3.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate3.Text.ToString())) && (Convert.ToDecimal(txtRate3.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount3.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount3.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount3.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount3.Text.ToString())) && (Convert.ToDecimal(txtDiscount3.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount3.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate3.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate3.Text.ToString())) && (Convert.ToDecimal(txtRate3.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate3.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount3.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount3.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount3.Text.ToString())) && (Convert.ToDecimal(txtDiscount3.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount3.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }


                    break;
                #endregion

                #region txt qty for line id-4
                case "txtQty4":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate4.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate4.Text.ToString())) && (Convert.ToDecimal(txtRate4.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount4.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount4.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount4.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount4.Text.ToString())) && (Convert.ToDecimal(txtDiscount4.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount4.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate4.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate4.Text.ToString())) && (Convert.ToDecimal(txtRate4.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate4.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount4.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount4.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount4.Text.ToString())) && (Convert.ToDecimal(txtDiscount4.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount4.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-5
                case "txtQty5":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate5.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate5.Text.ToString())) && (Convert.ToDecimal(txtRate5.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount5.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount2.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount2.Text.ToString())) && (Convert.ToDecimal(txtDiscount2.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate5.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate5.Text.ToString())) && (Convert.ToDecimal(txtRate5.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate5.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount5.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount5.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount5.Text.ToString())) && (Convert.ToDecimal(txtDiscount5.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount5.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-6
                case "txtQty6":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate6.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate6.Text.ToString())) && (Convert.ToDecimal(txtRate6.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount2.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount6.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount6.Text.ToString())) && (Convert.ToDecimal(txtDiscount6.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount6.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate6.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate6.Text.ToString())) && (Convert.ToDecimal(txtRate6.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate6.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount6.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount6.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount6.Text.ToString())) && (Convert.ToDecimal(txtDiscount6.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount6.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-7
                case "txtQty7":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate7.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate7.Text.ToString())) && (Convert.ToDecimal(txtRate7.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount7.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount7.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount7.Text.ToString())) && (Convert.ToDecimal(txtDiscount7.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount7.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate7.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate7.Text.ToString())) && (Convert.ToDecimal(txtRate7.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate7.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount7.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount7.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount7.Text.ToString())) && (Convert.ToDecimal(txtDiscount7.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-8
                case "txtQty8":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate8.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate8.Text.ToString())) && (Convert.ToDecimal(txtRate8.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount8.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount8.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount8.Text.ToString())) && (Convert.ToDecimal(txtDiscount8.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate8.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate8.Text.ToString())) && (Convert.ToDecimal(txtRate8.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate2.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount8.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount8.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount8.Text.ToString())) && (Convert.ToDecimal(txtDiscount8.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount8.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-9
                case "txtQty9":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate9.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate9.Text.ToString())) && (Convert.ToDecimal(txtRate9.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount9.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount9.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount9.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount9.Text.ToString())) && (Convert.ToDecimal(txtDiscount9.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount9.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate9.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate9.Text.ToString())) && (Convert.ToDecimal(txtRate9.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate2.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount9.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount9.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount9.Text.ToString())) && (Convert.ToDecimal(txtDiscount9.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount9.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                #region txt qty for line id-10
                case "txtQty10":
                    for (var i = 0; i < lineCollection.Count; i++)
                    {

                        if (lineCollection[i].LineID == chkLineIDVal)
                        {


                            // decimal.tryparse , some issues , remove it --TO DO :Pramod
                            // if null/empty/"" --> Msg to enter qty
                            // scenario when the line.qty has some value and UI text box is blank , user changing the value of values
                            if ((textBox.Text.ToString() != ""))
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((lineCollection[i].Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // update qty
                                    lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);

                                    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                                    if ((txtRate10.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate10.Text.ToString())) && (Convert.ToDecimal(txtRate10.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        // update rate 
                                        lineCollection[i].PerUnitRate = GetUpdatedRate(lineCollection[i]);

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                // Due to abnormal terminationn of 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }
                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }

                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount10.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, txtDiscount2.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount10.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount10.Text.ToString())) && (Convert.ToDecimal(txtDiscount10.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount10.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    //// wash away the irregular input
                                                    //textBox.Text = "";
                                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    ////masterPage.ShowModalPopup();
                                                }
                                            }
                                        }


                                    }
                                    else
                                    // get the focus to txt rate column with a message 
                                    {
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Please enter Rate";
                                        ////masterPage.ShowModalPopup();
                                        //Page.Form.DefaultFocus = txtRate1.ClientID;
                                        //return;
                                    }
                                }
                                else
                                #region normal line creation
                                { // when qty is entered for the first time
                                    // value in the text box gets asssigned
                                    lineCollection[i].Qty = Convert.ToDecimal(textBox.Text.ToString());

                                    // if user has not entered ,then msg to  enter rate and focus will be there 
                                    // if entered then BVA is done
                                    //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                    if ((txtRate10.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate10.Text.ToString())) && (Convert.ToDecimal(txtRate10.Text.ToString()) < Decimal.MaxValue))
                                    {
                                        lineCollection[i].PerUnitRate = Convert.ToDecimal(txtRate10.Text.ToString());

                                        // caluculate total 
                                        lineCollection[i].TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(lineCollection[i].Qty, lineCollection[i].PerUnitRate);

                                        if (lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                        {
                                            // Calculating the tax value, 
                                            // for that discount given should be there , if not then else part will b called
                                            if (lineCollection[i].Discount > 0.0m)
                                                // tax value , unit * rate per unit * tax applicable
                                                lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);
                                            else
                                            // get the focus to txt rate column with a message 
                                            {
                                                // TO DO ::  Use some other option to do this-- Aashis 
                                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                ////masterPage.ErrorMessage = "Please enter Discount";
                                                //Page.Form.DefaultFocus = txtRate1.ClientID;
                                                ////masterPage.ShowModalPopup();
                                                //return;
                                            }


                                            // Figure out destination of consumption for the HSN concerned
                                            lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                            }

                                            if (lineCollection[i].IsIntra)
                                            {
                                                lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                                            }
                                            else
                                            {
                                                lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                                                lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                                            }
                                        }
                                        else
                                        {
                                            // wash away the irregular input
                                            textBox.Text = "";
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                            //masterPage.ShowModalPopup();
                                        }

                                        chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                                        // status tells us whether we need to go for update of line collection or not
                                        // this is to keep the sancity of the object
                                        status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                                        if ((lineCollection[i].Discount.ToString() != txtDiscount10.Text.ToString()) && (status))
                                        {
                                            // call the update function, when there is diff of value in the obj and UI relevant contrl
                                            // passing the third argument as the current value of qty 
                                            lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetDiscount);
                                            //if user has entered discount then do the above step
                                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                            // 79228162514264337593543950335m , put this value to 
                                            if ((txtDiscount10.Text.ToString() != "") && (!string.IsNullOrEmpty(txtDiscount10.Text.ToString())) && (Convert.ToDecimal(txtDiscount10.Text.ToString()) < Decimal.MaxValue))
                                            {
                                                lineCollection[i].Discount = Convert.ToDecimal(txtDiscount10.Text.ToString());
                                                if (lineCollection[i].Discount < 100 && lineCollection[i].Discount > 0.0m && lineCollection[i].TotalLineIDWise < Decimal.MaxValue)
                                                {
                                                    // Calculating the tax value, 
                                                    // for that discount given should be there , if not then else part will b called
                                                    // tax value , unit * rate per unit * tax applicable
                                                    lineCollection[i].TaxValue = seller.Invoice.GetTaxableValueLineIDWise(lineCollection[i].TotalLineIDWise, lineCollection[i].Discount);

                                                }
                                                else
                                                {
                                                    // wash away the irregular input
                                                    textBox.Text = "";
                                                    // TO DO ::  Use some other option to do this-- Aashis 
                                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                                    //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                                    //masterPage.ShowModalPopup();
                                                }
                                            }


                                        }
                                        else
                                        // get the focus to txt rate column with a message 
                                        {
                                            // TO DO ::  Use some other option to do this-- Aashis 
                                            BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                            //masterPage.ErrorMessage = "Please enter Rate";
                                            //masterPage.ShowModalPopup();
                                            Page.Form.DefaultFocus = txtRate1.ClientID;
                                            return;
                                        }
                                    }

                                }
                                #endregion

                            }
                            // this is when , user inputs a blank qty in the second attempt
                            else
                            {
                                lineCollection = GetUpdatedQuantity(textBox, lineCollection[i], (int)ControlType.QtyCal);
                                //// TO DO ::  Use some other option to do this-- Aashis 
                                //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                ////masterPage.ErrorMessage = "Error - Please enter quanttiy. ";
                                ////masterPage.ShowModalPopup();
                            }

                            //assign it to UI
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);
                        }

                        // do the impact anyalysis
                        ////assign it to UI
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.QtyCal);


                        // Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetDiscount);

                    }

                    break;
                #endregion

                default:

                    break;
            }


            // Putting the line entries back to session
            if (lineCollection != null)
            {
                Session["LineEntryCollections"] = lineCollection;
            }

            // reassigning the value of seller
            if (seller != null)
                Session["seller"] = seller;

        }

        private decimal GetUpdatedRate(LineEntry line)
        {
            line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
            return line.PerUnitRate;
        }


        #endregion

        #region PreviewInvoice
        public void PreviewInvoice(object sender, EventArgs e)
        {
            // Finalization routine
            // Putting the line entries back to session
            if (lineCollection != null)
            {
                Session["LineEntryCollections"] = lineCollection;
            }

            // reassigning the value of seller
            if (seller != null)
                Session["seller"] = seller;


            if (Session["LineEntryCollections"] != null && Session["seller"] != null)
            {
                Response.Redirect("~/InvoicePreview.aspx.cs", false);
                this.Context.ApplicationInstance.CompleteRequest();
            }



        }
        #endregion

        #region SaveInvoice
        UnitOfWork unitOfWork = new UnitOfWork();

        private bool IsFormValidate()
        {
            bool isValidate = false;
            string invoceno = Convert.ToString(txtInvoiceNumber.Text.Trim());
            bool getinvoceno = unitOfWork.InvoiceRepository.Contains(c => c.InvoiceNo == invoceno);

            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                if (txtInvoiceNumber.Text.Length > 7 && txtInvoiceNumber.Text.Length <= 8)
                {

                }
                //amits
                //txtInvoiceNumber.Text.Length.ToString() > 7
            }

            if (getinvoceno && !string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()))
            {
                if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
                {
                    this.Master.WarningMessage = "Shipping Bill No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Advance.ToString())
                {
                    this.Master.WarningMessage = "Voucher No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.JobWork.ToString())
                {
                    this.Master.WarningMessage = "Challan No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString())
                {
                    this.Master.WarningMessage = "BOE/SB. No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString())
                {
                    this.Master.WarningMessage = "BOE/SB. No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString())
                {
                    this.Master.WarningMessage = "Invoice/BOE/SB. No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.ECommerce.ToString())
                {
                    this.Master.WarningMessage = "Order No. Already Exist!";
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
                {
                    this.Master.WarningMessage = "Bill of Entry Already Exist!";
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                isValidate = false;
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Bill of Entry!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }

            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Advance.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Voucher No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }

            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Shipping Bill No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.JobWork.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Challan No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Missing Invoice No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter BOE/SB. No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString())
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Invoice/BOE/SB. No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.ECommerce.ToString() && txtOrderDate.ReadOnly == false && !string.IsNullOrEmpty(txtOrderDate.Text.Trim()))
            {
                if (txtInvoiceNumber.ReadOnly == false && string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    this.Master.WarningMessage = "Please Enter Order No.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    isValidate = true;
                }
            }
            //else if (txtInvoiceNumber.ReadOnly == false && !string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
            //{
            //    //TODO:MEssage here that it should be here
            //     isValidate=true;
            //}
            //else if (string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()))
            //{
            //    isValidate=false;
            //    //TODO:MEssage here that it should be here
            //}
            else
            {
                isValidate = true;
            }

            return isValidate;
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
        private object ReadyDataForSave()
        {
            if (!IsFormValidate())
            {
                return null;
            }

            if (seller == null)
            {
                seller = new Seller();
            }
            if (seller.Invoice == null)
            {
                seller = CreateInvoice();
            }
            EnumConstants.InvoiceSpecialCondition InvoiceType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), rblInvoicePriority.SelectedValue);

            bool isImportInvoice = false;
            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {

                // TODO# 1
                /* In case of Import we will ask the details for Importer and register him first( otb)
                 * rather than aspuser 
                 */
                // if the importer is registered with us , then the details will be fetched
                // otherwise it will insert the data
                var importerData = Common.UserManager.Users.Where(w => w.OrganizationName == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (importerData == null)
                {
                    try
                    {
                        var user = new ApplicationUser()
                        {
                            //changes by amits vendor name
                            UserName = txtSellerGSTIN.Text.Replace(" ", "").Trim(),
                            //VendorName = txtSellerGSTIN.Text.Trim(),
                            OrganizationName = txtSellerGSTIN.Text.Replace(" ", "").Trim(),
                            Email = txtSellerName.Text.Trim(),
                            Address = txtSellerAddress.Text.Trim(),
                            FirstName = txtSellerGSTIN.Text.Replace(" ", "").Trim(),
                            LastName = txtSellerGSTIN.Text.Replace(" ", "").Trim(),
                            // import gstn no. is 15 digits
                            GSTNNo = "NA",//(Guid.NewGuid()).ToString().Replace("-", "").Substring(0, 15),
                            UserType = (byte)EnumConstants.UserType.Importer,
                            StateCode = "00",
                            ParentUserID = Common.LoggedInUserID(),
                            RegisterWithUs = false,

                            //amits invoicedatastatus = (byte)EnumConstants.Importer
                        };
                        IdentityResult result = Common.UserManager.Create(user, "Test@123"); //Gives error on this when click on preview without entering any detail
                        if (result.Succeeded)
                        {
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        // uc_sucess.ErrorMessage = ex.Message;
                        // uc_sucess.VisibleError = true;
                    }
                }
                isImportInvoice = true;
            }

            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.ReverseCharges.ToString())
            {
                var importerData = Common.UserManager.Users.Where(w => w.OrganizationName == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (importerData == null)
                {
                    try
                    {
                        var user = new ApplicationUser()
                        {
                            //changes by amits vendor name
                            UserName = Guid.NewGuid().ToString(), //txtSellerGSTIN.Text.Replace(" ", "").Trim(),
                            // PANNo= txtSellerGSTIN.Text.Trim(),
                            OrganizationName = txtSellerGSTIN.Text.Trim(),
                            Email = Guid.NewGuid().ToString() + "@recChr.com",//txtSellerName.Text.Trim()+"@na.com",
                            Address = txtSellerAddress.Text.Trim(),
                            FirstName = txtSellerName.Text.Replace(" ", "").Trim(),
                            LastName = txtSellerName.Text.Replace(" ", "").Trim(),
                            // import gstn no. is 15 digits
                            GSTNNo = "NA",//txtSellerGSTIN.Text.Trim(),
                            UserType = (byte)EnumConstants.UserType.NonGSTRegisteredUser,
                            StateCode = "00",
                            ParentUserID = Common.LoggedInUserID(),
                            RegisterWithUs = false,

                            //amits invoicedatastatus = (byte)EnumConstants.Importer
                        };
                        IdentityResult result = Common.UserManager.Create(user, "Test@123"); ;
                        if (!result.Succeeded)
                        {
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        // uc_sucess.ErrorMessage = ex.Message;
                        // uc_sucess.VisibleError = true;
                    }
                }
                isImportInvoice = true;
            }


            else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {

                var importerData = unitOfWork.AspnetRepository.Find(w => w.GSTNNo == txtSellerGSTIN.Text.Trim());
                // var importerData = Common.UserManager.Users.Where(w => w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (importerData == null)
                {
                    try
                    {
                        var user = new ApplicationUser()
                        {
                            UserName = Guid.NewGuid().ToString(),
                            OrganizationName = txtSellerGSTIN.Text.Trim(),
                            Email = Guid.NewGuid().ToString() + "@smi.com",
                            Address = txtSellerAddress.Text.Trim(),
                            FirstName = txtSellerName.Text.Replace(" ", "").Trim(),
                            LastName = txtSellerName.Text.Replace(" ", "").Trim(),
                            // import gstn no. is 15 digits
                            GSTNNo = txtSellerGSTIN.Text.Trim(),
                            UserType = (byte)EnumConstants.UserType.RegularDealerRD,
                            StateCode = "00",
                            ParentUserID = Common.LoggedInUserID(),
                            RegisterWithUs = false,

                            //amits invoicedatastatus = (byte)EnumConstants.Importer
                        };
                        IdentityResult result = Common.UserManager.Create(user, "Test@123"); ;
                        if (!result.Succeeded)
                        {
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        // uc_sucess.ErrorMessage = ex.Message;
                        // uc_sucess.VisibleError = true;
                    }
                }
                isImportInvoice = false;
            }
            // var sellerData = Common.UserManager.Users.Where(w => isImportInvoice ? w.GSTNNo == txtSellerGSTIN.Text.Trim() : w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();

            var sellerData = Common.UserManager.Users.Where(w => isImportInvoice ? w.OrganizationName == txtSellerGSTIN.Text.Trim() : w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
            // var sellerInvoiceData = seller.GenerateSellerInvoiceData(sellerData.GSTNNo, sellerData.Id);
            seller.DateOfInvoice = DateTime.Now.ToString("dd-MM-yyyy");//sellerInvoiceData.DateOfInvoice;
            if (!string.IsNullOrEmpty(txtOrderDate.Text.Trim()) && txtOrderDate.Visible == true)
            {
                try
                {
                    DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", null);
                    seller.OrderDate = OrderDate;
                }
                catch (Exception ex)
                {
                    // TODO:ERROR HERE MESSAGE POPUP
                }
            }
            //seller.SerialNoInvoice = sellerInvoiceData.SerialNoInvoice;
            var ddMMyyyyFormat = DateTime.Now.ToString("ddMMyyyy");
            if (ViewState["sellerinvoice"] == null || ViewState["sellerinvoice"] == "")
            {
                ViewState["sellerinvoice"] = cls_Invoice.GetInvoiveNoWithPreFix(UniqueNoGenerate.RandomValue(), InvoiceType);
            }//sellerData.StateCode + "_" + sellerInvoiceData.SerialNoInvoice.PanID + "_" + ddMMyyyyFormat + "_" + sellerInvoiceData.SerialNoInvoice.FinancialYear + "_" + sellerInvoiceData.SerialNoInvoice.CurrentSrlNo;

            seller.SellerInvoice = ViewState["sellerinvoice"].ToString();


            List<Int64> advanceInvoice = new List<long>();
            foreach (ListItem ids in lboxRegularMapped.Items)
            {
                if (ids.Selected)
                {
                    advanceInvoice.Add(Convert.ToInt64(ids.Value));
                }
            }
            seller.AdvanceInvoiceIds = advanceInvoice;



            List<Int64> challanInvoice = new List<long>();
            foreach (ListItem id in lboxRegularChallanMapped.Items)
            {
                if (id.Selected)
                {
                    challanInvoice.Add(Convert.ToInt64(id.Value));
                }
            }
            seller.ChallanInvoiceIds = challanInvoice;

            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                seller.SellerUserID = sellerData.Id;
                seller.GSTIN = sellerData.GSTNNo;
                seller.SellerStateCode = sellerData.StateCode;
                seller.NameAsOnGST = sellerData.OrganizationName;
                seller.Address = sellerData.Address;
                seller.SellerStateName = unitOfWork.StateRepository.Find(f => f.StateCode == sellerData.StateCode).StateName;
            }
            else
            {
                seller.SellerUserID = sellerData.Id;
                seller.GSTIN = sellerData.GSTNNo;
                seller.SellerStateCode = sellerData.StateCode;
                seller.NameAsOnGST = sellerData.OrganizationName;
                seller.Address = sellerData.Address;
                seller.SellerStateName = unitOfWork.StateRepository.Find(f => f.StateCode == sellerData.StateCode).StateName;
            }
            //if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            //{
            //    seller.Consignee = new Consignee();
            var consinee = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
            var reciever = Common.UserManager.Users.Where(w => w.GSTNNo == txtRecieverGSTIN.Text.Trim()).FirstOrDefault();
            if (reciever == null)
            {
                var user = new ApplicationUser()
                      {
                          UserName = Guid.NewGuid().ToString() + "Receiver",
                          OrganizationName = txtRecieverName.Text.Trim(),
                          Email = Guid.NewGuid().ToString() + "@rec.com",
                          Address = txtRecieverAddress.Text.Trim(),
                          FirstName = txtRecieverName.Text.Replace(" ", "").Trim(),
                          LastName = txtRecieverName.Text.Replace(" ", "").Trim(),
                          // import gstn no. is 15 digits
                          GSTNNo = txtRecieverGSTIN.Text.Trim(),
                          UserType = (byte)EnumConstants.UserType.Exporter,
                          StateCode = "00",
                          ParentUserID = Common.LoggedInUserID(),
                          RegisterWithUs = false,

                          //amits invoicedatastatus = (byte)EnumConstants.Importer
                      };
                IdentityResult result = Common.UserManager.Create(user, "Test@123");
                if (!result.Succeeded)
                {
                    this.Master.ErrorMessage = "Receiver Detais not saved.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                }
            }
            if (consinee == null)
            {
                var userCon = new ApplicationUser()
                       {
                           UserName = Guid.NewGuid().ToString() + "Consignee",//To be implement later bcoz receiver user name and consignee user cant be same.
                           OrganizationName = txtConsigneeName.Text.Trim(),
                           Email = Guid.NewGuid().ToString() + "@con.com",
                           Address = txtConsigneeAddress.Text.Trim(),
                           FirstName = txtConsigneeName.Text.Replace(" ", "").Trim(),
                           LastName = txtConsigneeName.Text.Replace(" ", "").Trim(),
                           GSTNNo = txtConsigneeGSTIN.Text.Trim(),
                           UserType = (byte)EnumConstants.UserType.Exporter,
                           StateCode = "00",
                           ParentUserID = Common.LoggedInUserID(),
                           RegisterWithUs = false,
                       };
                IdentityResult result1 = Common.UserManager.Create(userCon, "Test@123"); ;
                if (!result1.Succeeded)
                {
                    this.Master.ErrorMessage = "Consign Details not saved.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                }
            }
            //else
            //{
            seller.Consignee = new Consignee();
            var consineeExit = Common.UserManager.Users.Where(w => w.GSTNNo == txtConsigneeGSTIN.Text.Trim()).FirstOrDefault();
            seller.Consignee.ConsigneeUserID = consineeExit.Id;
            seller.Consignee.StateCode = consineeExit.StateCode;
            seller.Consignee.NameAsOnGST = consineeExit.OrganizationName;
            seller.Consignee.Address = consineeExit.Address;
            seller.Consignee.StateName = unitOfWork.StateRepository.Find(f => f.StateCode == consineeExit.StateCode).StateName;
            seller.Consignee.StateCode = consineeExit.StateCode;
            // seller.Consignee.ConsigneeUserID = consinee.StateCode;

            seller.Reciever = new Reciever();
            var recieverExit = Common.UserManager.Users.Where(w => w.GSTNNo == txtRecieverGSTIN.Text.Trim()).FirstOrDefault();
            seller.Reciever.RecieveruserID = recieverExit.Id;
            seller.Reciever.NameAsOnGST = recieverExit.OrganizationName;
            seller.Reciever.Address = recieverExit.Address;
            seller.Reciever.StateName = unitOfWork.StateRepository.Find(f => f.StateCode == recieverExit.StateCode).StateName;
            seller.Reciever.StateCode = recieverExit.StateCode;

            //    seller.SerialNoInvoice.FinancialYear = seller.SerialNoInvoice.FinancialYear;//DateTime.UtcNow.Year + "-" + DateTime.UtcNow.AddYears(1);//
            seller.Invoice.InvoiceType = ddlInvoiceType.SelectedValue.ToString();// strInvoiceType;

            seller.DateOfInvoice = txtInvoiceDate.Text.Trim();
            //     }
            if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Advance.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Import.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Export.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.JobWork.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.ECommerce.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                if (string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    //TODO:POP Here erroe message that plz enter 
                    this.Master.ErrorMessage = "Kindly enter voucher no./challan no.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                }
                else
                {
                    // int Invoiceid = Convert.ToInt32(txtInvoiceNumber.Text.Trim());
                    // var SInvoice = unitOfWork.InvoiceRepository.Contains(c => c.InvoiceID == Invoiceid);

                    seller.SellerInvoice = cls_Invoice.GetInvoiveNoWithPreFix(txtInvoiceNumber.Text.Trim(), InvoiceType);
                }
            }
            else
            {
                //seller.SellerInvoice = txtInvoiceNumber.Text.Trim();
            }

            seller.Invoice.InvoiceSpecialCondition = rblInvoicePriority.SelectedValue;
            seller.Invoice.Freight = !string.IsNullOrEmpty(txtFreight.Text.Trim()) ? Convert.ToDecimal(txtFreight.Text.Trim()) : 0;
            seller.Invoice.Insurance = !string.IsNullOrEmpty(txtInsurance.Text.Trim()) ? Convert.ToDecimal(txtInsurance.Text.Trim()) : 0;
            seller.Invoice.PackingAndForwadingCharges = !string.IsNullOrEmpty(txtPackingCharges.Text.Trim()) ? Convert.ToDecimal(txtPackingCharges.Text.Trim()) : 0;
            if (ddlVendor.SelectedIndex > 0)
                seller.Invoice.VendorID = Convert.ToInt32(ddlVendor.SelectedValue.ToString());

            if (ddlTransShipment.SelectedIndex > 0)
                seller.Invoice.TransShipment_ID = Convert.ToInt32(ddlTransShipment.SelectedValue.ToString());


            bool isInter = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

            if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                bool isStateExampted = false;
            }
            else
            {
                bool isStateExampted = unitOfWork.StateRepository.Find(f => f.StateCode == seller.Consignee.StateCode).IsExempted.Value;
            }
            bool isExported = false;
            if (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Export.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString())
            {
                isExported = true;
            }

            bool isJobwork = (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.JobWork.ToString());
            bool isImport = (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Import.ToString());
            var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == seller.SellerStateCode);
            var isUTState = stateData.UT.Value;
            var isExempted = stateData.IsExempted.Value;
            var isEcom = false;
            var isUn = false;

            seller.Invoice.LineEntry = GetGVData();
            var invLineItem = from invo in seller.Invoice.LineEntry
                              select new GST_TRN_INVOICE_DATA
                              {
                                  //InvoiceID = invoiceCreate.InvoiceID,
                                  LineID = invo.LineID,
                                  GST_MST_ITEM = invo.Item,
                                  Item_ID = invo.Item.Item_ID,
                                  Qty = invo.Qty,
                                  Rate = invo.PerUnitRate,
                                  TotalAmount = invo.TotalLineIDWise,
                                  Discount = invo.Discount,
                                  TaxableAmount = invo.TaxableValueLineIDWise,
                                  TotalAmountWithTax = invo.TaxValue,
                                  IGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableValueLineIDWise, invo.Item.IGST.Value), //isJobwork ? 0 : (isUTState ? 0 : (isInter ? invo.Item.IGST : (isExport ? invo.Item.IGST : (isImport ? invo.Item.IGST : 0)))),
                                  IGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableValueLineIDWise, invo.Item.IGST.Value),// isJobwork ? 0 : (isUTState ? 0 : (isInter ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : 0)))),
                                  CGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableValueLineIDWise, invo.Item.CGST.Value),
                                  CGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableValueLineIDWise, invo.Item.CGST.Value),
                                  SGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableValueLineIDWise, invo.Item.SGST.Value),
                                  SGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableValueLineIDWise, invo.Item.SGST.Value),
                                  UGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableValueLineIDWise, invo.Item.UGST.Value),
                                  UGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableValueLineIDWise, invo.Item.UGST.Value),
                                  CessRate = isJobwork ? 0 : invo.Item.CESS,
                                  CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CESS.Value)
                              };

            seller.TotalDiscount = invLineItem.Average(s => s.Discount).Value;
            seller.TotalAmountWithTax = seller.Invoice.InvoiceSpecialCondition != EnumConstants.InvoiceSpecialCondition.RegularRCM.ToString() ? invLineItem.Sum(s => s.TotalAmountWithTax).Value + invLineItem.Sum(s => s.CGSTAmt).Value + invLineItem.Sum(s => s.IGSTAmt).Value + invLineItem.Sum(s => s.UGSTAmt).Value + invLineItem.Sum(s => s.SGSTAmt).Value + invLineItem.Sum(s => s.CessAmt).Value :
                invLineItem.Sum(s => s.TotalAmountWithTax).Value;
            seller.TotalAmount = invLineItem.Sum(s => s.TotalAmount).Value;
            seller.TotalCGSTAmount = invLineItem.Sum(s => s.CGSTAmt).Value;
            seller.TotalIGSTAmount = invLineItem.Sum(s => s.IGSTAmt).Value;
            seller.TotalSGSTAmount = invLineItem.Sum(s => s.SGSTAmt).Value + invLineItem.Sum(s => s.UGSTAmt).Value;
            //seller.t = invLineItem.Sum(s => s.SGSTAmt).Value;
            //seller.GrandTotalAmount
            //seller.GrandTotalAmount = invLineItem.Sum(s => s.TotalAmount).value;  

            seller.CreatedBy = Common.LoggedInUserID();
            seller.TotalCessAmount = invLineItem.Sum(s => s.CessAmt).Value;
            //seller.TotalTaxableAmount = invLineItem.Sum(s => s.TotalAmountWithTax).Value;
            seller.TotalTaxableAmount = invLineItem.Sum(s => s.TotalAmountWithTax).Value;// seller.Invoice.InvoiceSpecialCondition != EnumConstants.InvoiceSpecialCondition.ReverseCharges.ToString() ? invLineItem.Sum(s => s.TotalAmountWithTax).Value : invLineItem.Sum(s => s.TotalAmount).Value;
            seller.Invoice.LineEntryDBType = invLineItem.ToList();

            // Calculate sum of all advance type invoices in case of regular

            decimal? totalamt = 0; decimal? TotalAmtWithTax = 0; decimal? Totaltaxble = 0; decimal? Totalcgst = 0; decimal? Totalsgst = 0; decimal? Totaligst = 0; decimal? Totalcess = 0; decimal? Totaldis = 0;
            if (seller.AdvanceInvoiceIds.Count != 0)
            {
                foreach (Int64 advid in seller.AdvanceInvoiceIds)
                {
                    var getInvoiceData = unitOfWork.InvoiceDataRepository.Filter(f => f.InvoiceID == advid);

                    Totalcgst += getInvoiceData.Sum(o => o.CGSTAmt);
                    Totalsgst += getInvoiceData.Sum(o => o.SGSTAmt);
                    Totaligst += getInvoiceData.Sum(o => o.IGSTAmt);
                    Totalcess += getInvoiceData.Sum(o => o.CessAmt);
                    Totaldis += getInvoiceData.Average(o => o.Discount);
                    Totaltaxble += getInvoiceData.Sum(o => o.TaxableAmount);
                    totalamt += getInvoiceData.Sum(o => o.TotalAmount);
                    TotalAmtWithTax += getInvoiceData.Sum(o => o.TotalAmountWithTax);
                }
                if (seller.TotalTaxableAmount >= Totaltaxble)
                {
                    seller.TotalCGSTAmount = seller.TotalCGSTAmount - Convert.ToDecimal(Totalcgst);
                    seller.TotalIGSTAmount = seller.TotalIGSTAmount - Convert.ToDecimal(Totaligst);
                    seller.TotalSGSTAmount = seller.TotalSGSTAmount - Convert.ToDecimal(Totalsgst);
                    seller.TotalCessAmount = seller.TotalCessAmount - Convert.ToDecimal(Totalcess);
                    seller.TotalDiscount = seller.TotalDiscount - Convert.ToDecimal(Totaldis);
                    seller.TotalTaxableAmount = seller.TotalTaxableAmount - Convert.ToDecimal(Totaltaxble);
                    seller.TotalAmount = seller.TotalAmount - Convert.ToDecimal(totalamt);
                    seller.TotalAmountWithTax = seller.TotalAmountWithTax - Convert.ToDecimal(TotalAmtWithTax);
                    


                }
                else if (seller.TotalTaxableAmount < Totaltaxble)
                {
                    Totalcgst = Totalcgst - Convert.ToDecimal(seller.TotalCGSTAmount);
                    Totaligst = Totaligst - Convert.ToDecimal(seller.TotalIGSTAmount);
                    Totalsgst = Totalsgst - Convert.ToDecimal(seller.TotalSGSTAmount);
                    Totalcess = Totalcess - Convert.ToDecimal(seller.TotalCessAmount);
                    Totaldis = Totaldis - Convert.ToDecimal(seller.TotalDiscount);
                    Totaltaxble = Totaltaxble - Convert.ToDecimal(seller.TotalTaxableAmount);
                    totalamt = totalamt - Convert.ToDecimal(seller.TotalAmount);
                    TotalAmtWithTax = TotalAmtWithTax - Convert.ToDecimal(seller.TotalAmountWithTax);
                }
            }


            ////

            return seller;

        }

        public void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormValidate())
                {

                    return;
                    //amits
                    //uc_sucess.Errormeaa = "Invoice created successfully.";
                    //uc_sucess.Visible = true;
                }
                if (txtSellerGSTIN.ReadOnly == false && string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()) || txtRecieverGSTIN.ReadOnly == false && string.IsNullOrEmpty(txtRecieverGSTIN.Text.Trim()) || txtConsigneeGSTIN.ReadOnly == false && string.IsNullOrEmpty(txtConsigneeGSTIN.Text.Trim()))
                {
                    this.Master.WarningMessage = "Seller Details, Receiver Details and Consignee Details should not be left blank!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    return;
                }
                var sellerData = (Seller)ReadyDataForSave();

                if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CL.ToString())
                {
                    if (seller.TotalAmount <= 250000)
                    {
                        this.Master.WarningMessage = "Total amount should be greater than 250000.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        return;
                    }
                }
                else if (rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.B2CS.ToString())
                {
                    if (seller.TotalAmount >= 250000)
                    {
                        this.Master.WarningMessage = "Total amount should be less than 250000.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        return;
                    }
                }

                // var items=seller.Invoice.LineEntryDBType.GroupBy(g=>g.Item_ID).Select(s=>new LineEntry{Item=s})
                var query = from bs in sellerData.Invoice.LineEntryDBType
                            group bs by bs.Item_ID into g
                            orderby g.Sum(x => x.Qty)
                            select new GST_TRN_INVOICE_DATA
                            {
                                Item_ID = g.Key,
                                Qty = g.Sum(x => x.Qty)
                            };
                cls_PurchaseRegister cls = new cls_PurchaseRegister();

                //purchase reg
                foreach (GST_TRN_INVOICE_DATA itemData in query)
                {
                    var item = unitOfWork.ItemRepository.Find(f => f.Item_ID == itemData.Item_ID).ItemType;
                    if (item == (byte)EnumConstants.ItemType.HSN)
                    {
                        string uId = Common.LoggedInUserID();
                        int iTemid = Convert.ToInt32(itemData.Item_ID);
                        decimal LeftQty = cls.GetLeftItemQty(iTemid, uId);
                        if (itemData.Qty > LeftQty)
                        {
                            this.Master.WarningMessage = "Please update purchase register.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                            return;
                        }
                    }
                }

                seller.Invoice.SaveInvoiceData(seller);


                txtRecieverGSTIN.Text = string.Empty;
                //TODO:I need to send mail from here. of invoice generation
                ClearAllSession();
                BindItems();
                ClearConsigneeFieldData();
                ClearRecieverField();
                ResetFormData();
                rblInvoicePriorityIndex_Changed(sender, e);
                //System.Threading.Thread.Sleep(3000);
                if (seller != null)
                    Session["seller"] = seller;
                this.Master.SuccessMessage = "Invoice created successfully.";
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                ViewState["sellerinvoice"] = String.Empty;
                txtInvoiceNumber.Text = "";


            }
            catch (Exception ex)
            {
                this.Master.ErrorMessage = ex.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
            }
        }
        private void ResetFormData()
        {
            BindInvoiceType();
            BindSpecialInvoiceType();
            BindUserVendor();
            txtFreight.Text = string.Empty;
            txtInsurance.Text = string.Empty;
            txtPackingCharges.Text = string.Empty;
        }

        private void ResetField()
        {
            if (txtInvoiceDate.Visible)
                txtInvoiceDate.Text = string.Empty;

            txtFreight.Text = string.Empty;
            txtInsurance.Text = string.Empty;
            txtPackingCharges.Text = string.Empty;
            BindSpecialInvoiceType();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            // if recivver text bo nulll and consignee null 
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //TextBox TextBox1 = row.FindControl("txtGoodService") as TextBox;

            ////Access TextBox1 here.
            //string myString = TextBox1.Text;
            if (!String.IsNullOrEmpty(txtConsigneeGSTIN.Text) && !String.IsNullOrEmpty(txtRecieverGSTIN.Text))
            {
                string invoceno = Convert.ToString(txtInvoiceNumber.Text.Trim());
                bool getinvoceno = unitOfWork.InvoiceRepository.Contains(c => c.InvoiceNo == invoceno);

                if (getinvoceno && !string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()))
                {
                    this.Master.WarningMessage = "Voucher No. already used!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    return;
                }

                seller = (Seller)ReadyDataForSave();
                if (seller == null)
                {
                    this.Master.WarningMessage = "Please enter the Invoice Data.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                }
                else
                {
                    uc_invoiceR.SellerData = seller;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModel", "$('#viewInvoiceModel').modal();", true);
                    upModal.Update();
                }
            }
            else
            {
                this.Master.WarningMessage = "Please Enter Receiver GSTIN and Seller GSTIN NO";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                return;
            }

        }
        private List<LineEntry> GetGVData()
        {
            List<LineEntry> lineCollection = new List<LineEntry>();
            foreach (GridViewRow row in gvItems.Rows)
            {
                TextBox txtGoodService = (TextBox)row.FindControl("txtGoodService");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                Label txtTotal = (Label)row.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                Label txtTaxableValue = (Label)row.FindControl("txtTaxableValue");

                if (!string.IsNullOrEmpty(txtGoodService.Text.Trim()))
                {
                    LineEntry le = new LineEntry();
                    le.LineID = row.RowIndex;
                    Seller sell = new Seller();
                    //le.HSN.RateCGST = sell.GetHSNInformation("").RateSGST;
                    GST_MST_ITEM item = new GST_MST_ITEM();
                    item = sell.GetItemInformation(txtGoodService.Text.Trim());

                    le.Item = item;
                    le.Qty = Convert.ToDecimal(txtQty.Text.Trim());

                    le.PerUnitRate = Convert.ToDecimal(txtRate.Text.Trim());
                    le.TotalLineIDWise = Convert.ToDecimal(txtTotal.Text.Trim());
                    if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                        le.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                    //le.AmountWithTaxLineIDWise = Convert.ToDecimal(((Label)row.FindControl("txtTaxableValue")).Text.Trim());

                    le.TaxValue = Convert.ToDecimal(txtTaxableValue.Text.Trim());

                    // Grand total of all line items

                    seller.TotalAmount += le.TotalLineIDWise;

                    // grand total of all line items with tax
                    seller.TotalAmountWithTax += le.TaxValue;

                    lineCollection.Add(le);
                }
            }
            return lineCollection;
        }

        #endregion
        private void ShowButton()
        {
            if (Common.IsTaxConsultant())
            {
                InvoiceReturn.Visible = true;
            }
            if (Common.IsUser())
            { InvoiceReturn.Visible = false; }
        }



        protected void rblInvoicePriorityIndex_Changed(object sender, EventArgs e)
        {
            //     ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "change",
            //"<script type='text/javascript'>change();</script>", true);
            //rblInvoicePriority.Attributes.Add("onselectedindexchanged", "change();");
            // seller.Invoice.InvoiceSpecialCondition = rblInvoicePriority.SelectedValue;

            if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.B2CS.ToString())
            {
                litRecieverGSTIN.Text = "PAN/Aadhaar No.";
                litConsigneeGSTIN.Text = "PAN/Aadhaar No.";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's PAN/Aadhaar No.");


                txtRecieverGSTIN.MaxLength = 12;
                txtInvoiceNumber.MaxLength = 12;
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's PAN/Aadhaar No.");
                txtConsigneeGSTIN.MaxLength = 12;
                txtInvoiceNumber.ReadOnly = false;
                //txtRecieverName.ReadOnly = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtSellerGSTIN.ReadOnly = true;
                txtSellerName.ReadOnly = true;
                txtSellerAddress.ReadOnly = true;
                txtRecieverName.ReadOnly = false;
                txtRecieverAddress.ReadOnly = false;
                txtConsigneeName.ReadOnly = false;
                txtConsigneeAddress.ReadOnly = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                //ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.B2CL.ToString())
            {
                litRecieverGSTIN.Text = "PAN/Aadhaar No.";
                litConsigneeGSTIN.Text = "PAN/Aadhaar No.";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's PAN/Aadhaar No.");


                txtRecieverGSTIN.MaxLength = 12;
                txtInvoiceNumber.MaxLength = 12;
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's PAN/Aadhaar No.");
                txtConsigneeGSTIN.MaxLength = 12;
                txtInvoiceNumber.ReadOnly = false;
                //txtRecieverName.ReadOnly = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtRecieverName.ReadOnly = false;
                txtRecieverAddress.ReadOnly = false;
                txtConsigneeName.ReadOnly = false;
                txtConsigneeAddress.ReadOnly = false;
                txtSellerGSTIN.ReadOnly = true;
                txtSellerName.ReadOnly = true;
                txtSellerAddress.ReadOnly = true;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                //ChangeImporterUIChange();
            }

            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Advance.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "Voucher No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.JobWork.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "Challan No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                lblinvoiceNo.Text = "Shipping Bill No. :";
                litRecieverGSTIN.Text = "IEC No.";
                litConsigneeGSTIN.Text = "IEC No.";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's IEC No.");
                txtRecieverGSTIN.MaxLength = 8;
                txtInvoiceNumber.MaxLength = 8;
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's IEC No.");
                txtConsigneeGSTIN.MaxLength = 8;
                txtInvoiceNumber.ReadOnly = false;
                //txtRecieverName.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtSellerGSTIN.ReadOnly = true;
                txtSellerName.ReadOnly = true;
                txtSellerAddress.ReadOnly = true;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                lblinvoiceNo.Text = "Invoice/BOE/SB. No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "BOE/SB. No. :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                BindImportInvoice();
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();

                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.ECommerce.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Text = "Order No. :";
                txtInvoiceNumber.MaxLength = 16;
                lblOrderDate.Visible = true;
                txtOrderDate.Visible = true;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                txtInvoiceNumber.Text = "";
                txtOrderDate.Text = "";
                BindImportInvoice();
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();

                ChangeImporterUIChange();

            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtSellerName.TextMode = TextBoxMode.Email;
                lblinvoiceNo.Text = "Bill of Entry :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                BindImportInvoice();
                ChangeImporterUIChange();

            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.Regular.ToString())
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");

                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;

                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }
            else if (rblInvoicePriority.SelectedValue == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {

                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                lblinvoiceNo.Text = "Missing Invoice :";
                txtInvoiceNumber.MaxLength = 16;
                txtInvoiceNumber.ReadOnly = false;
                lblinvoiceNo.Visible = true;
                txtInvoiceNumber.Visible = true;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                BindImportInvoice();
                ChangeImporterUIChange();
                //var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                //if (GetSellerProfile.GSTNNo == profile.GSTNNo)
                //{
                //    this.Master.WarningMessage = "Seller and Reciever can not be same.";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                //}
            }

            else
            {
                litRecieverGSTIN.Text = "GSTIN";
                litConsigneeGSTIN.Text = "GSTIN";
                txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
                txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");
                txtRecieverGSTIN.MaxLength = 16;
                txtConsigneeGSTIN.MaxLength = 16;
                lblinvoiceNo.Visible = false;
                txtInvoiceNumber.Visible = false;
                lblOrderDate.Visible = false;
                txtOrderDate.Visible = false;
                lblRegularMapped.Visible = false;
                lboxRegularMapped.Visible = false;
                lboxRegularChallanMapped.Visible = false;
                lblRegularChallanMapped.Visible = false;
                GetSellerDetails();
                ClearRecieverField();
                ClearConsigneeFieldData();
                BindImportInvoice();
                ChangeImporterUIChange();
            }


        }
        //-----------NOT IN USED FROM HERE---------
        public enum ControlType
        {

            GetGoodsOrServiceInfo = 1,
            QtyCal,
            GetTotalAmnt,
            GetDiscount,
            MapAll
        }

        private void DisplayMessage(string msg)
        {
            string message = msg;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            Response.Write("<script>alert('" + msg + "');</script>");



        }

        #region EnableNextLineItem
        /// <summary>
        /// to check to enable the next line 
        /// </summary>
        /// <param name="lineCollections"></param>
        /// <param name="lineID"></param>
        private void EnableNextLineItem(List<LineEntry> lineCollections, int lineID)
        {
            // 
            int lineNumner = 0;
            foreach (LineEntry line in lineCollection)
            {
                if (line.LineID == lineID)
                {
                    lineNumner = ((line.TaxValue > 0) ? ++lineID : 0);
                }


            }

            switch (lineNumner)
            {
                case 2:
                    {
                        txtGoodService2.ReadOnly = false;
                        txtFreight.ReadOnly = false;
                        txtInsurance.ReadOnly = false;
                        txtPackingCharges.ReadOnly = false;
                        // btnPreview1.Enabled = true;
                        break;
                    }

                case 3:
                    {
                        txtGoodService3.ReadOnly = false;
                        break;
                    }

                case 4:
                    {
                        txtGoodService4.ReadOnly = false;
                        break;
                    }

                case 5:
                    {
                        txtGoodService5.ReadOnly = false;
                        break;
                    }

                case 6:
                    {
                        txtGoodService6.ReadOnly = false;
                        break;
                    }

                case 7:
                    {
                        txtGoodService7.ReadOnly = false;
                        break;
                    }

                case 8:
                    {
                        txtGoodService8.ReadOnly = false;
                        break;
                    }

                case 9:
                    {
                        txtGoodService9.ReadOnly = false;
                        break;
                    }
                case 10:
                    {
                        txtGoodService10.ReadOnly = false;
                        break;
                    }

            }
        }
        #endregion
        // 6 th pass- ideally
        #region UnitPrice
        /// <summary>
        /// on text change of txtRate1 , total amount value is taken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetTotalAmnt(object sender, EventArgs e)
        {
            // getting the complete state of LineEntries from the 
            // explicit casting is required
            if (Session["LineEntryCollections"] != null)
            {

                lineCollection = (List<LineEntry>)Session["LineEntryCollections"];
            }

            // reassigning the value of seller
            if (Session["seller"] != null)
                seller = (Seller)Session["seller"];



            // get the id of text box that caused this event fired and subsequent post back
            // getting the value from the sender obj
            TextBox textBox = (sender as TextBox);

            // getting the text box id
            string HSHtxtID = textBox.ID;

            // two variable would be used across this control , that is why the scope is here 
            int chkLineIDVal = -1; // default value assigned
            bool status = false; // default value assigned

            //based on above id of the control, pick the specific line id 
            // do BVA on rate, dicoount(also less than 100)
            // do BVA on total, taxable value and all
            // get the values stored in qty, assign it to qty 
            // get the values strored in the rate , check not null, assign it to perUnitRate
            // get the value of discount, assign it to discount
            // Calcualte total=qty * rate -discount 

            switch (HSHtxtID)
            {
                #region tx Rate --1
                case "txtRate1":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 1)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value , with or without discount
                                    line.TaxableValueLineIDWise = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount1.Text.ToString())) && (Convert.ToDecimal(txtDiscount1.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount1.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxableValueLineIDWise = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }

                        }
                        //assign it to UI , total tax value
                        Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                        EnableNextLineItem(lineCollection, chkLineIDVal);
                    }
                    break;
                #endregion

                #region tx Rate --2
                case "txtRate2":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 2)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty2.Text))
                                    line.Qty = Convert.ToDecimal(txtQty2.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }

                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount2.Text.ToString())) && (Convert.ToDecimal(txtDiscount2.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount2.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }

                    break;
                #endregion

                #region tx Rate--3
                case "txtRate3":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 3)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty3.Text))
                                    line.Qty = Convert.ToDecimal(txtQty3.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79338163514364337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount3.Text.ToString())) && (Convert.ToDecimal(txtDiscount3.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount3.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }

                    break;
                #endregion

                #region tx Rate --4
                case "txtRate4":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 4)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty4.Text))
                                    line.Qty = Convert.ToDecimal(txtQty4.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79448164514464447594544950445m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount4.Text.ToString())) && (Convert.ToDecimal(txtDiscount4.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount4.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;

                #endregion

                #region tx Rate--5
                case "txtRate5":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 5)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty5.Text))
                                    line.Qty = Convert.ToDecimal(txtQty5.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79558165514564557595545950555m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount5.Text.ToString())) && (Convert.ToDecimal(txtDiscount5.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount5.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate --6
                case "txtRate6":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 6)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty6.Text))
                                    line.Qty = Convert.ToDecimal(txtQty6.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79668166514664667596546950665m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount6.Text.ToString())) && (Convert.ToDecimal(txtDiscount6.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount6.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate--7
                case "txtRate7":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 7)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty7.Text))
                                    line.Qty = Convert.ToDecimal(txtQty7.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79778167514764777597547950775m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount7.Text.ToString())) && (Convert.ToDecimal(txtDiscount7.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount7.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate --8
                case "txtRate8":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 8)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty8.Text))
                                    line.Qty = Convert.ToDecimal(txtQty8.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79888168514864887598548950885m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount8.Text.ToString())) && (Convert.ToDecimal(txtDiscount8.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount8.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate--9
                case "txtRate9":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 9)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty9.Text))
                                    line.Qty = Convert.ToDecimal(txtQty9.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79998169514964997599549950995m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount9.Text.ToString())) && (Convert.ToDecimal(txtDiscount9.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount9.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate -10
                case "txtRate10":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 10)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty10.Text))
                                    line.Qty = Convert.ToDecimal(txtQty10.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter Quantity";
                                    //Page.Form.DefaultFocus = txtQty1.ClientID;
                                    ////masterPage.ShowModalPopup();
                                    //return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                            {

                                line.PerUnitRate = Convert.ToDecimal(textBox.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    //textBox.Text = "";
                                    //// TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    ////masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 791010816105141064101075910541095010105m , put this value to 
                                if ((!string.IsNullOrEmpty(txtDiscount10.Text.ToString())) && (Convert.ToDecimal(txtDiscount10.Text.ToString()) < Decimal.MaxValue))
                                {
                                    line.Discount = Convert.ToDecimal(txtDiscount10.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);

                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        //textBox.Text = "";
                                        //// TO DO ::  Use some other option to do this-- Aashis 
                                        //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        ////masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        ////masterPage.ShowModalPopup();
                                    }
                                }
                                else // if user invokes the Discount text change without entering 
                                {
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }

                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // value line.qty is not equal to current value of control , that means user has changed the value 
                                // so we need to update the ,  && (line.Qty !=0) 
                                if ((line.Qty.ToString() != textBox.Text.ToString()) && (status))
                                {
                                    // call the update function, when there is diff of value in the obj and UI relevant contrl
                                    // passing the third argument as the current value of qty 
                                    lineCollection = seller.UpdateLineCollections(lineCollection, chkLineIDVal, textBox.Text.ToString(), (int)ControlType.GetTotalAmnt);
                                }

                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total tax value
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);
                        }
                        // do the impact anyalysis
                        ////assign it to UI , total tax value
                        //Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                    }
                    break;
                #endregion

                #region tx Rate -default
                default:

                    break;
                #endregion
            }
            // Putting the line entries back to session
            if (lineCollection != null)
            {
                Session["LineEntryCollections"] = lineCollection;
            }

            // reassigning the value of seller
            if (seller != null)
                Session["seller"] = seller;
        }
        #endregion
        private List<LineEntry> GetUpdatedQuantity(TextBox textBox, LineEntry line, int controlType)
        {
            // call the update function, when there is diff of value in the obj and UI relevant contrl
            // passing the third argument as the current value of qty , 4 is for the update control
            lineCollection = seller.UpdateLineCollections(lineCollection, line.LineID, textBox.Text.ToString(), controlType);
            return lineCollection;
        }
        // Function gets the information specific to the HSN/SAC from DB -4.2 pass
        #region Map2UI

        // collection of line entries
        private void Map2UI(List<LineEntry> lines, int lineID, int controlType)
        {

            #region when control type is 0,1,2,3,4
            // if control goes to one block, mustang will not allow the control to go into any other block
            int mustangStopper = 0;

            // int jockey
            int jockey;
            for (int i = 0; i < lineCollection.Count; i++)
            {
                // line id 1 will be for the first row of invoice
                #region Line ID --1

                if (lineID == 1 && mustangStopper == 0)
                {
                    // assign it 
                    jockey = lineID;
                    // 
                    i = --jockey;
                    if (lineCollection[i].LineID == lineID && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService1.Text == null)
                                    {
                                        txtGoodService1.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService1.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService1.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption1.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal1.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal1.Text == "0" || txtTotal1.Text == "0.0")
                                                txtTotal1.Text = "";
                                        }
                                        else
                                            txtTotal1.Text = "";

                                        // initalized to ""
                                        txtUnit1.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue1.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue1.Text == "0" || txtTaxableValue1.Text == "0.0")
                                                txtTaxableValue1.Text = "";
                                        }
                                        else
                                            txtTaxableValue1.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount1.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount1.Text == "0" || txtDiscount1.Text == "0.0")
                                                txtDiscount1.Text = "";
                                        }
                                        else
                                            txtDiscount1.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty1.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty1.Text == "0" || txtQty1.Text == "0.0")
                                                txtQty1.Text = "";
                                        }
                                        else
                                            txtQty1.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate1.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate1.Text == "0" || txtRate1.Text == "0.0")
                                                txtRate1.Text = "";
                                        }
                                        else
                                            txtRate1.Text = "";

                                        //if (txtGoodService1.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService1.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService1.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService1.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption1.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal1.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal1.Text == "0" || txtTotal1.Text == "0.0")
                                                txtTotal1.Text = "";
                                        }
                                        else
                                            txtTotal1.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit1.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue1.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue1.Text == "0" || txtTaxableValue1.Text == "0.0")
                                                txtTaxableValue1.Text = "";
                                        }
                                        else
                                            txtTaxableValue1.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount1.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount1.Text == "0" || txtDiscount1.Text == "0.0")
                                                txtDiscount1.Text = "";
                                        }
                                        else
                                            txtDiscount1.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty1.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty1.Text == "0" || txtQty1.Text == "0.0")
                                                txtQty1.Text = "";
                                        }
                                        else
                                            txtQty1.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate1.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate1.Text == "0" || txtRate1.Text == "0.0")
                                                txtRate1.Text = "";
                                        }
                                        else
                                            txtRate1.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService1.Text == "" || (txtGoodService1.Text.Length > 8 && txtGoodService1.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption1.Text = "";
                                            txtUnit1.Text = "";
                                            txtTotal1.Text = "";
                                            txtDiscount1.Text = "";
                                            txtTaxableValue1.Text = "";
                                            txtQty1.Text = "";
                                            txtRate1.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty1.Text == null || ((txtQty1.Text == "") && (txtQty1.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty1.Text = "";
                                        txtTotal1.Text = "";
                                        txtDiscount1.Text = "";
                                        txtTaxableValue1.Text = "";
                                        txtRate1.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty1.Text = "";
                                            txtQty1.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty1.Text == "0" || txtQty1.Text == "0.0")
                                                txtQty1.Text = "";
                                        }
                                        else
                                            txtQty1.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal1.Text = "";
                                            txtTotal1.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal1.Text == "0" || txtTotal1.Text == "0.0")
                                                txtTotal1.Text = "";
                                        }
                                        else
                                            txtTotal1.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue1.Text = "";
                                            txtTaxableValue1.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue1.Text == "0" || txtTaxableValue1.Text == "0.0" || lineCollection[i].Qty.ToString() == "0.00")
                                                txtTaxableValue1.Text = "";
                                        }
                                        else
                                            txtTaxableValue1.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate1.Text = "";
                                            txtRate1.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate1.Text == "0" || txtRate1.Text == "0.0")
                                                txtRate1.Text = "";
                                        }
                                        else
                                            txtRate1.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount1.Text = "";
                                            txtDiscount1.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount1.Text == "") || (txtDiscount1.Text == "0") || (txtDiscount1.Text == "0.0"))
                                                txtDiscount1.Text = "";
                                        }
                                        else
                                            txtDiscount1.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate1.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate1.Text == "0" || (txtRate1.Text == "0.0"))
                                            txtRate1.Text = "";
                                    }
                                    else
                                        txtRate1.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty1.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty1.Text == "0" || (txtQty1.Text == "0.0"))
                                            txtQty1.Text = "";
                                    }
                                    else
                                        txtQty1.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal1.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal1.Text == "0" || (txtTotal1.Text == "0.0"))
                                            txtTotal1.Text = "";
                                    }
                                    else
                                        txtTotal1.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue1.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue1.Text == "0" || (txtTaxableValue1.Text == "0.00") || (txtTaxableValue1.Text == "0.0"))
                                            txtTaxableValue1.Text = "";
                                    }
                                    else
                                        txtTaxableValue1.Text = "";

                                    if (txtDiscount1.Text != "" && (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null))
                                    {
                                        txtDiscount1.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount1.Text == "") || (txtDiscount1.Text == "0") || (txtDiscount1.Text == "0.0"))
                                            txtDiscount1.Text = "";
                                    }
                                    else
                                        txtDiscount1.Text = "";


                                    break;
                                }
                        }
                        mustangStopper = 1;

                    }
                }
                #endregion

                #region Line ID--2
                if (lineID == 2 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == lineID && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService2.Text == null)
                                    {
                                        txtGoodService2.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService2.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService2.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption2.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal2.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal2.Text == "0" || txtTotal2.Text == "0.0")
                                                txtTotal2.Text = "";
                                        }
                                        else
                                            txtTotal2.Text = "";

                                        // initalized to ""
                                        txtUnit2.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue2.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue2.Text == "0" || txtTaxableValue2.Text == "0.0")
                                                txtTaxableValue2.Text = "";
                                        }
                                        else
                                            txtTaxableValue2.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount2.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount2.Text == "0" || txtDiscount2.Text == "0.0")
                                                txtDiscount2.Text = "";
                                        }
                                        else
                                            txtDiscount2.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty2.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty2.Text == "0" || txtQty2.Text == "0.0")
                                                txtQty2.Text = "";
                                        }
                                        else
                                            txtQty2.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate2.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate2.Text == "0" || txtRate2.Text == "0.0")
                                                txtRate2.Text = "";
                                        }
                                        else
                                            txtRate2.Text = "";

                                        //if (txtGoodService1.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService2.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService2.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService2.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption2.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal2.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal2.Text == "0" || txtTotal2.Text == "0.0")
                                                txtTotal2.Text = "";
                                        }
                                        else
                                            txtTotal2.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit2.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue2.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue2.Text == "0" || txtTaxableValue2.Text == "0.0")
                                                txtTaxableValue2.Text = "";
                                        }
                                        else
                                            txtTaxableValue2.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount2.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount2.Text == "0" || txtDiscount2.Text == "0.0")
                                                txtDiscount2.Text = "";
                                        }
                                        else
                                            txtDiscount2.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty2.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty2.Text == "0" || txtQty2.Text == "0.0")
                                                txtQty2.Text = "";
                                        }
                                        else
                                            txtQty2.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate2.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate2.Text == "0" || txtRate2.Text == "0.0")
                                                txtRate2.Text = "";
                                        }
                                        else
                                            txtRate2.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService2.Text == "" || (txtGoodService2.Text.Length > 8 && txtGoodService2.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption2.Text = "";
                                            txtUnit2.Text = "";
                                            txtTotal2.Text = "";
                                            txtDiscount2.Text = "";
                                            txtTaxableValue2.Text = "";
                                            txtQty2.Text = "";
                                            txtRate2.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty2.Text == null || ((txtQty2.Text == "") && (txtQty2.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty2.Text = "";
                                        txtTotal2.Text = "";
                                        txtDiscount2.Text = "";
                                        txtTaxableValue2.Text = "";
                                        txtRate2.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty2.Text = "";
                                            txtQty2.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty2.Text == "0" || txtQty2.Text == "0.0")
                                                txtQty2.Text = "";
                                        }
                                        else
                                            txtQty2.Text = "";


                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal2.Text = "";
                                            txtTotal2.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal2.Text == "0" || txtTotal2.Text == "0.0")
                                                txtTotal2.Text = "";
                                        }
                                        else
                                            txtTotal2.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue2.Text = "";
                                            txtTaxableValue2.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue2.Text == "0" || txtTaxableValue2.Text == "0.0")
                                                txtTaxableValue2.Text = "";
                                        }
                                        else
                                            txtTaxableValue2.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate2.Text = "";
                                            txtRate2.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate2.Text == "0" || txtRate2.Text == "0.0")
                                                txtRate2.Text = "";
                                        }
                                        else
                                            txtRate2.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount2.Text = "";
                                            txtDiscount2.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount2.Text == "") || (txtDiscount2.Text == "0") || (txtDiscount2.Text == "0.0"))
                                                txtDiscount2.Text = "";
                                        }
                                        else
                                            txtDiscount2.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate2.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate2.Text == "0" || (txtRate2.Text == "0.0"))
                                            txtRate2.Text = "";
                                    }
                                    else
                                        txtRate2.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty2.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty2.Text == "0" || (txtQty2.Text == "0.0"))
                                            txtQty2.Text = "";
                                    }
                                    else
                                        txtQty2.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal2.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal2.Text == "0" || (txtTotal2.Text == "0.0"))
                                            txtTotal2.Text = "";
                                    }
                                    else
                                        txtTotal2.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue2.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue2.Text == "0" || (txtTaxableValue2.Text == "0.00") || (txtTaxableValue2.Text == "0.0"))
                                            txtTaxableValue2.Text = "";
                                    }
                                    else
                                        txtTaxableValue2.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount2.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount2.Text == "") || (txtDiscount2.Text == "0") || (txtDiscount2.Text == "0.0"))
                                            txtDiscount2.Text = "";
                                    }
                                    else
                                        txtDiscount2.Text = "";


                                    break;
                                }
                        }
                        ++mustangStopper;
                    }
                }
                #endregion

                #region Line ID--3
                if (lineID == 3 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 3 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService3.Text == null)
                                    {
                                        txtGoodService3.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService3.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService3.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption3.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal3.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal3.Text == "0" || txtTotal3.Text == "0.0")
                                                txtTotal3.Text = "";
                                        }
                                        else
                                            txtTotal1.Text = "";

                                        // initalized to ""
                                        txtUnit3.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue3.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue3.Text == "0" || txtTaxableValue3.Text == "0.0")
                                                txtTaxableValue3.Text = "";
                                        }
                                        else
                                            txtTaxableValue3.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount3.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount3.Text == "0" || txtDiscount3.Text == "0.0")
                                                txtDiscount3.Text = "";
                                        }
                                        else
                                            txtDiscount3.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty3.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty3.Text == "0" || txtQty3.Text == "0.0")
                                                txtQty3.Text = "";
                                        }
                                        else
                                            txtQty3.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate3.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate3.Text == "0" || txtRate3.Text == "0.0")
                                                txtRate3.Text = "";
                                        }
                                        else
                                            txtRate3.Text = "";

                                        //if (txtGoodService1.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService3.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService3.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService3.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption3.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal3.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal3.Text == "0" || txtTotal3.Text == "0.0")
                                                txtTotal3.Text = "";
                                        }
                                        else
                                            txtTotal3.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit3.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue3.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue3.Text == "0" || txtTaxableValue3.Text == "0.0")
                                                txtTaxableValue3.Text = "";
                                        }
                                        else
                                            txtTaxableValue3.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount3.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount3.Text == "0" || txtDiscount3.Text == "0.0")
                                                txtDiscount3.Text = "";
                                        }
                                        else
                                            txtDiscount3.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty3.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty3.Text == "0" || txtQty3.Text == "0.0")
                                                txtQty3.Text = "";
                                        }
                                        else
                                            txtQty3.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate3.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate3.Text == "0" || txtRate3.Text == "0.0")
                                                txtRate3.Text = "";
                                        }
                                        else
                                            txtRate3.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService3.Text == "" || (txtGoodService3.Text.Length > 8 && txtGoodService3.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption3.Text = "";
                                            txtUnit3.Text = "";
                                            txtTotal3.Text = "";
                                            txtDiscount3.Text = "";
                                            txtTaxableValue3.Text = "";
                                            txtQty3.Text = "";
                                            txtRate3.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty3.Text == null || ((txtQty3.Text == "") && (txtQty3.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty3.Text = "";
                                        txtTotal3.Text = "";
                                        txtDiscount3.Text = "";
                                        txtTaxableValue3.Text = "";
                                        txtRate3.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty3.Text = "";
                                            txtQty3.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty3.Text == "0" || txtQty3.Text == "0.0")
                                                txtQty3.Text = "";
                                        }
                                        else
                                            txtQty3.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal3.Text = "";
                                            txtTotal3.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal3.Text == "0" || txtTotal3.Text == "0.0")
                                                txtTotal3.Text = "";
                                        }
                                        else
                                            txtTotal3.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue3.Text = "";
                                            txtTaxableValue3.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue3.Text == "0" || txtTaxableValue3.Text == "0.0")
                                                txtTaxableValue3.Text = "";
                                        }
                                        else
                                            txtTaxableValue3.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate3.Text = "";
                                            txtRate3.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate3.Text == "0" || txtRate3.Text == "0.0")
                                                txtRate3.Text = "";
                                        }
                                        else
                                            txtRate3.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount3.Text = "";
                                            txtDiscount3.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount3.Text == "") || (txtDiscount3.Text == "0") || (txtDiscount3.Text == "0.0"))
                                                txtDiscount3.Text = "";
                                        }
                                        else
                                            txtDiscount3.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate3.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate3.Text == "0" || (txtRate3.Text == "0.0"))
                                            txtRate3.Text = "";
                                    }
                                    else
                                        txtRate3.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty3.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty3.Text == "0" || (txtQty3.Text == "0.0"))
                                            txtQty3.Text = "";
                                    }
                                    else
                                        txtQty3.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal3.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal3.Text == "0" || (txtTotal3.Text == "0.0"))
                                            txtTotal3.Text = "";
                                    }
                                    else
                                        txtTotal3.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue3.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue3.Text == "0" || (txtTaxableValue3.Text == "0.00") || (txtTaxableValue3.Text == "0.0"))
                                            txtTaxableValue3.Text = "";
                                    }
                                    else
                                        txtTaxableValue3.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount3.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount3.Text == "") || (txtDiscount3.Text == "0") || (txtDiscount3.Text == "0.0"))
                                            txtDiscount3.Text = "";
                                    }
                                    else
                                        txtDiscount3.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }
                #endregion

                #region Line ID--4
                if (lineID == 4 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 4 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService4.Text == null)
                                    {
                                        txtGoodService4.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService4.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService4.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption4.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal4.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal4.Text == "0" || txtTotal4.Text == "0.0")
                                                txtTotal4.Text = "";
                                        }
                                        else
                                            txtTotal4.Text = "";

                                        // initalized to ""
                                        txtUnit4.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue4.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue4.Text == "0" || txtTaxableValue4.Text == "0.0")
                                                txtTaxableValue4.Text = "";
                                        }
                                        else
                                            txtTaxableValue4.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount4.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount4.Text == "0" || txtDiscount4.Text == "0.0")
                                                txtDiscount4.Text = "";
                                        }
                                        else
                                            txtDiscount4.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty4.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty4.Text == "0" || txtQty4.Text == "0.0")
                                                txtQty4.Text = "";
                                        }
                                        else
                                            txtQty4.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate4.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate4.Text == "0" || txtRate4.Text == "0.0")
                                                txtRate4.Text = "";
                                        }
                                        else
                                            txtRate4.Text = "";

                                        //if (txtGoodService4.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService4.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService4.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService4.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption4.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal4.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal4.Text == "0" || txtTotal4.Text == "0.0")
                                                txtTotal4.Text = "";
                                        }
                                        else
                                            txtTotal4.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit4.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue4.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue4.Text == "0" || txtTaxableValue4.Text == "0.0")
                                                txtTaxableValue4.Text = "";
                                        }
                                        else
                                            txtTaxableValue4.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount4.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount4.Text == "0" || txtDiscount4.Text == "0.0")
                                                txtDiscount4.Text = "";
                                        }
                                        else
                                            txtDiscount4.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty4.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty4.Text == "0" || txtQty4.Text == "0.0")
                                                txtQty4.Text = "";
                                        }
                                        else
                                            txtQty4.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate4.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate4.Text == "0" || txtRate4.Text == "0.0")
                                                txtRate4.Text = "";
                                        }
                                        else
                                            txtRate4.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService4.Text == "" || (txtGoodService4.Text.Length > 8 && txtGoodService4.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption4.Text = "";
                                            txtUnit4.Text = "";
                                            txtTotal4.Text = "";
                                            txtDiscount4.Text = "";
                                            txtTaxableValue4.Text = "";
                                            txtQty4.Text = "";
                                            txtRate4.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty4.Text == null || ((txtQty4.Text == "") && (txtQty4.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty4.Text = "";
                                        txtTotal4.Text = "";
                                        txtDiscount4.Text = "";
                                        txtTaxableValue4.Text = "";
                                        txtRate4.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty4.Text = "";
                                            txtQty4.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty4.Text == "0" || txtQty4.Text == "0.0")
                                                txtQty4.Text = "";
                                        }
                                        else
                                            txtQty4.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal4.Text = "";
                                            txtTotal4.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal4.Text == "0" || txtTotal4.Text == "0.0")
                                                txtTotal4.Text = "";
                                        }
                                        else
                                            txtTotal4.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue4.Text = "";
                                            txtTaxableValue4.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue4.Text == "0" || txtTaxableValue4.Text == "0.0")
                                                txtTaxableValue4.Text = "";
                                        }
                                        else
                                            txtTaxableValue4.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate4.Text = "";
                                            txtRate4.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate4.Text == "0" || txtRate4.Text == "0.0")
                                                txtRate4.Text = "";
                                        }
                                        else
                                            txtRate4.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount4.Text = "";
                                            txtDiscount4.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount4.Text == "") || (txtDiscount4.Text == "0") || (txtDiscount4.Text == "0.0"))
                                                txtDiscount4.Text = "";
                                        }
                                        else
                                            txtDiscount4.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate4.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate4.Text == "0" || (txtRate4.Text == "0.0"))
                                            txtRate4.Text = "";
                                    }
                                    else
                                        txtRate4.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty4.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty4.Text == "0" || (txtQty4.Text == "0.0"))
                                            txtQty4.Text = "";
                                    }
                                    else
                                        txtQty4.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal4.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal4.Text == "0" || (txtTotal4.Text == "0.0"))
                                            txtTotal4.Text = "";
                                    }
                                    else
                                        txtTotal4.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue4.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue4.Text == "0" || (txtTaxableValue4.Text == "0.00") || (txtTaxableValue4.Text == "0.0"))
                                            txtTaxableValue4.Text = "";
                                    }
                                    else
                                        txtTaxableValue4.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount4.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount4.Text == "") || (txtDiscount4.Text == "0") || (txtDiscount4.Text == "0.0"))
                                            txtDiscount4.Text = "";
                                    }
                                    else
                                        txtDiscount4.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }

                #endregion

                #region LIne ID--5
                if (lineID == 5 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 5 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService5.Text == null)
                                    {
                                        txtGoodService5.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService5.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService5.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption5.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal5.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal5.Text == "0" || txtTotal5.Text == "0.0")
                                                txtTotal5.Text = "";
                                        }
                                        else
                                            txtTotal5.Text = "";

                                        // initalized to ""
                                        txtUnit5.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue5.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue5.Text == "0" || txtTaxableValue5.Text == "0.0")
                                                txtTaxableValue5.Text = "";
                                        }
                                        else
                                            txtTaxableValue5.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount5.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount5.Text == "0" || txtDiscount5.Text == "0.0")
                                                txtDiscount5.Text = "";
                                        }
                                        else
                                            txtDiscount5.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty5.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty5.Text == "0" || txtQty5.Text == "0.0")
                                                txtQty5.Text = "";
                                        }
                                        else
                                            txtQty5.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate5.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate5.Text == "0" || txtRate5.Text == "0.0")
                                                txtRate5.Text = "";
                                        }
                                        else
                                            txtRate5.Text = "";

                                        //if (txtGoodService5.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService5.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService5.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService5.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption5.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal5.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal5.Text == "0" || txtTotal5.Text == "0.0")
                                                txtTotal5.Text = "";
                                        }
                                        else
                                            txtTotal5.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit5.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue5.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue5.Text == "0" || txtTaxableValue5.Text == "0.0")
                                                txtTaxableValue5.Text = "";
                                        }
                                        else
                                            txtTaxableValue5.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount5.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount5.Text == "0" || txtDiscount5.Text == "0.0")
                                                txtDiscount5.Text = "";
                                        }
                                        else
                                            txtDiscount5.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty5.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty5.Text == "0" || txtQty5.Text == "0.0")
                                                txtQty5.Text = "";
                                        }
                                        else
                                            txtQty5.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate5.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate5.Text == "0" || txtRate5.Text == "0.0")
                                                txtRate5.Text = "";
                                        }
                                        else
                                            txtRate5.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService5.Text == "" || (txtGoodService5.Text.Length > 8 && txtGoodService5.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption5.Text = "";
                                            txtUnit5.Text = "";
                                            txtTotal5.Text = "";
                                            txtDiscount5.Text = "";
                                            txtTaxableValue5.Text = "";
                                            txtQty5.Text = "";
                                            txtRate5.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty5.Text == null || ((txtQty5.Text == "") && (txtQty5.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty5.Text = "";
                                        txtTotal5.Text = "";
                                        txtDiscount5.Text = "";
                                        txtTaxableValue5.Text = "";
                                        txtRate5.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty5.Text = "";
                                            txtQty5.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty5.Text == "0" || txtQty5.Text == "0.0")
                                                txtQty5.Text = "";
                                        }
                                        else
                                            txtQty5.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal5.Text = "";
                                            txtTotal5.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal5.Text == "0" || txtTotal5.Text == "0.0")
                                                txtTotal5.Text = "";
                                        }
                                        else
                                            txtTotal5.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue5.Text = "";
                                            txtTaxableValue5.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue5.Text == "0" || txtTaxableValue5.Text == "0.0")
                                                txtTaxableValue5.Text = "";
                                        }
                                        else
                                            txtTaxableValue5.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate5.Text = "";
                                            txtRate5.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate5.Text == "0" || txtRate5.Text == "0.0")
                                                txtRate5.Text = "";
                                        }
                                        else
                                            txtRate5.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount5.Text = "";
                                            txtDiscount5.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount5.Text == "") || (txtDiscount5.Text == "0") || (txtDiscount5.Text == "0.0"))
                                                txtDiscount5.Text = "";
                                        }
                                        else
                                            txtDiscount5.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate5.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate5.Text == "0" || (txtRate5.Text == "0.0"))
                                            txtRate5.Text = "";
                                    }
                                    else
                                        txtRate5.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty5.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty5.Text == "0" || (txtQty5.Text == "0.0"))
                                            txtQty5.Text = "";
                                    }
                                    else
                                        txtQty5.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal5.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal5.Text == "0" || (txtTotal5.Text == "0.0"))
                                            txtTotal5.Text = "";
                                    }
                                    else
                                        txtTotal5.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue5.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue5.Text == "0" || (txtTaxableValue5.Text == "0.00") || (txtTaxableValue5.Text == "0.0"))
                                            txtTaxableValue5.Text = "";
                                    }
                                    else
                                        txtTaxableValue5.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount5.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount5.Text == "") || (txtDiscount5.Text == "0") || (txtDiscount5.Text == "0.0"))
                                            txtDiscount5.Text = "";
                                    }
                                    else
                                        txtDiscount5.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }

                #endregion

                #region  Line ID--6
                if (lineID == 6 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 6 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService6.Text == null)
                                    {
                                        txtGoodService6.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService6.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService6.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption6.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal6.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal6.Text == "0" || txtTotal6.Text == "0.0")
                                                txtTotal6.Text = "";
                                        }
                                        else
                                            txtTotal6.Text = "";

                                        // initalized to ""
                                        txtUnit6.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue6.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue6.Text == "0" || txtTaxableValue6.Text == "0.0")
                                                txtTaxableValue6.Text = "";
                                        }
                                        else
                                            txtTaxableValue6.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount6.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount6.Text == "0" || txtDiscount6.Text == "0.0")
                                                txtDiscount6.Text = "";
                                        }
                                        else
                                            txtDiscount6.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty6.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty6.Text == "0" || txtQty6.Text == "0.0")
                                                txtQty6.Text = "";
                                        }
                                        else
                                            txtQty6.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate6.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate6.Text == "0" || txtRate6.Text == "0.0")
                                                txtRate6.Text = "";
                                        }
                                        else
                                            txtRate6.Text = "";

                                        //if (txtGoodService6.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService6.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService6.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService6.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption6.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal6.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal6.Text == "0" || txtTotal6.Text == "0.0")
                                                txtTotal6.Text = "";
                                        }
                                        else
                                            txtTotal6.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit6.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue6.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue6.Text == "0" || txtTaxableValue6.Text == "0.0")
                                                txtTaxableValue6.Text = "";
                                        }
                                        else
                                            txtTaxableValue6.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount6.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount6.Text == "0" || txtDiscount6.Text == "0.0")
                                                txtDiscount6.Text = "";
                                        }
                                        else
                                            txtDiscount6.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty6.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty6.Text == "0" || txtQty6.Text == "0.0")
                                                txtQty6.Text = "";
                                        }
                                        else
                                            txtQty6.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate6.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate6.Text == "0" || txtRate6.Text == "0.0")
                                                txtRate6.Text = "";
                                        }
                                        else
                                            txtRate6.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService6.Text == "" || (txtGoodService6.Text.Length > 8 && txtGoodService6.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption6.Text = "";
                                            txtUnit6.Text = "";
                                            txtTotal6.Text = "";
                                            txtDiscount6.Text = "";
                                            txtTaxableValue6.Text = "";
                                            txtQty6.Text = "";
                                            txtRate6.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty6.Text == null || ((txtQty6.Text == "") && (txtQty6.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty6.Text = "";
                                        txtTotal6.Text = "";
                                        txtDiscount6.Text = "";
                                        txtTaxableValue6.Text = "";
                                        txtRate6.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty6.Text = "";
                                            txtQty6.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty6.Text == "0" || txtQty6.Text == "0.0")
                                                txtQty6.Text = "";
                                        }
                                        else
                                            txtQty6.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal6.Text = "";
                                            txtTotal6.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal6.Text == "0" || txtTotal6.Text == "0.0")
                                                txtTotal6.Text = "";
                                        }
                                        else
                                            txtTotal6.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue6.Text = "";
                                            txtTaxableValue6.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue6.Text == "0" || txtTaxableValue6.Text == "0.0")
                                                txtTaxableValue6.Text = "";
                                        }
                                        else
                                            txtTaxableValue6.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate6.Text = "";
                                            txtRate6.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate6.Text == "0" || txtRate6.Text == "0.0")
                                                txtRate6.Text = "";
                                        }
                                        else
                                            txtRate6.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount6.Text = "";
                                            txtDiscount6.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount6.Text == "") || (txtDiscount6.Text == "0") || (txtDiscount6.Text == "0.0"))
                                                txtDiscount6.Text = "";
                                        }
                                        else
                                            txtDiscount6.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate6.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate6.Text == "0" || (txtRate6.Text == "0.0"))
                                            txtRate6.Text = "";
                                    }
                                    else
                                        txtRate6.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty6.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty6.Text == "0" || (txtQty6.Text == "0.0"))
                                            txtQty6.Text = "";
                                    }
                                    else
                                        txtQty6.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal6.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal6.Text == "0" || (txtTotal6.Text == "0.0"))
                                            txtTotal6.Text = "";
                                    }
                                    else
                                        txtTotal6.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue6.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue6.Text == "0" || (txtTaxableValue6.Text == "0.00") || (txtTaxableValue6.Text == "0.0"))
                                            txtTaxableValue6.Text = "";
                                    }
                                    else
                                        txtTaxableValue6.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount6.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount6.Text == "") || (txtDiscount6.Text == "0") || (txtDiscount6.Text == "0.0"))
                                            txtDiscount6.Text = "";
                                    }
                                    else
                                        txtDiscount6.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }

                #endregion


                if (lineID == 7 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 7 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService7.Text == null)
                                    {
                                        txtGoodService7.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService7.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService7.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption7.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal7.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal7.Text == "0" || txtTotal7.Text == "0.0")
                                                txtTotal7.Text = "";
                                        }
                                        else
                                            txtTotal7.Text = "";

                                        // initalized to ""
                                        txtUnit7.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue7.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue7.Text == "0" || txtTaxableValue7.Text == "0.0")
                                                txtTaxableValue7.Text = "";
                                        }
                                        else
                                            txtTaxableValue7.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount7.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount7.Text == "0" || txtDiscount7.Text == "0.0")
                                                txtDiscount7.Text = "";
                                        }
                                        else
                                            txtDiscount7.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty7.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty7.Text == "0" || txtQty7.Text == "0.0")
                                                txtQty7.Text = "";
                                        }
                                        else
                                            txtQty7.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate7.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate7.Text == "0" || txtRate7.Text == "0.0")
                                                txtRate7.Text = "";
                                        }
                                        else
                                            txtRate7.Text = "";

                                        //if (txtGoodService7.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService7.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService7.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService7.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption7.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal7.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal7.Text == "0" || txtTotal7.Text == "0.0")
                                                txtTotal7.Text = "";
                                        }
                                        else
                                            txtTotal7.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit7.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue7.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue7.Text == "0" || txtTaxableValue7.Text == "0.0")
                                                txtTaxableValue7.Text = "";
                                        }
                                        else
                                            txtTaxableValue7.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount7.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount7.Text == "0" || txtDiscount7.Text == "0.0")
                                                txtDiscount7.Text = "";
                                        }
                                        else
                                            txtDiscount7.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty7.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty7.Text == "0" || txtQty7.Text == "0.0")
                                                txtQty7.Text = "";
                                        }
                                        else
                                            txtQty7.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate7.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate7.Text == "0" || txtRate7.Text == "0.0")
                                                txtRate7.Text = "";
                                        }
                                        else
                                            txtRate7.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService7.Text == "" || (txtGoodService7.Text.Length > 8 && txtGoodService7.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption7.Text = "";
                                            txtUnit7.Text = "";
                                            txtTotal7.Text = "";
                                            txtDiscount7.Text = "";
                                            txtTaxableValue7.Text = "";
                                            txtQty7.Text = "";
                                            txtRate7.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty7.Text == null || ((txtQty7.Text == "") && (txtQty7.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty7.Text = "";
                                        txtTotal7.Text = "";
                                        txtDiscount7.Text = "";
                                        txtTaxableValue7.Text = "";
                                        txtRate7.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty7.Text = "";
                                            txtQty7.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty7.Text == "0" || txtQty7.Text == "0.0")
                                                txtQty7.Text = "";
                                        }
                                        else
                                            txtQty7.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal7.Text = "";
                                            txtTotal7.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal7.Text == "0" || txtTotal7.Text == "0.0")
                                                txtTotal7.Text = "";
                                        }
                                        else
                                            txtTotal7.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue7.Text = "";
                                            txtTaxableValue7.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue7.Text == "0" || txtTaxableValue7.Text == "0.0")
                                                txtTaxableValue7.Text = "";
                                        }
                                        else
                                            txtTaxableValue7.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate7.Text = "";
                                            txtRate7.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate7.Text == "0" || txtRate7.Text == "0.0")
                                                txtRate7.Text = "";
                                        }
                                        else
                                            txtRate7.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount7.Text = "";
                                            txtDiscount7.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount7.Text == "") || (txtDiscount7.Text == "0") || (txtDiscount7.Text == "0.0"))
                                                txtDiscount7.Text = "";
                                        }
                                        else
                                            txtDiscount7.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate7.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate7.Text == "0" || (txtRate7.Text == "0.0"))
                                            txtRate7.Text = "";
                                    }
                                    else
                                        txtRate7.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty7.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty7.Text == "0" || (txtQty7.Text == "0.0"))
                                            txtQty7.Text = "";
                                    }
                                    else
                                        txtQty7.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal7.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal7.Text == "0" || (txtTotal7.Text == "0.0"))
                                            txtTotal7.Text = "";
                                    }
                                    else
                                        txtTotal7.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue7.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue7.Text == "0" || (txtTaxableValue7.Text == "0.00") || (txtTaxableValue7.Text == "0.0"))
                                            txtTaxableValue7.Text = "";
                                    }
                                    else
                                        txtTaxableValue7.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount7.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount7.Text == "") || (txtDiscount7.Text == "0") || (txtDiscount7.Text == "0.0"))
                                            txtDiscount7.Text = "";
                                    }
                                    else
                                        txtDiscount7.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }

                if (lineID == 8 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 8 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService8.Text == null)
                                    {
                                        txtGoodService8.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService8.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService8.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption8.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal8.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal8.Text == "0" || txtTotal8.Text == "0.0")
                                                txtTotal8.Text = "";
                                        }
                                        else
                                            txtTotal8.Text = "";

                                        // initalized to ""
                                        txtUnit8.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue8.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue8.Text == "0" || txtTaxableValue8.Text == "0.0")
                                                txtTaxableValue8.Text = "";
                                        }
                                        else
                                            txtTaxableValue8.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount8.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount8.Text == "0" || txtDiscount8.Text == "0.0")
                                                txtDiscount8.Text = "";
                                        }
                                        else
                                            txtDiscount8.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty8.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty8.Text == "0" || txtQty8.Text == "0.0")
                                                txtQty8.Text = "";
                                        }
                                        else
                                            txtQty8.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate8.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate8.Text == "0" || txtRate8.Text == "0.0")
                                                txtRate8.Text = "";
                                        }
                                        else
                                            txtRate8.Text = "";

                                        //if (txtGoodService8.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService8.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService8.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService8.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption8.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal8.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal8.Text == "0" || txtTotal8.Text == "0.0")
                                                txtTotal8.Text = "";
                                        }
                                        else
                                            txtTotal8.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit8.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue8.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue8.Text == "0" || txtTaxableValue8.Text == "0.0")
                                                txtTaxableValue8.Text = "";
                                        }
                                        else
                                            txtTaxableValue8.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount8.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount8.Text == "0" || txtDiscount8.Text == "0.0")
                                                txtDiscount8.Text = "";
                                        }
                                        else
                                            txtDiscount8.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty8.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty8.Text == "0" || txtQty8.Text == "0.0")
                                                txtQty8.Text = "";
                                        }
                                        else
                                            txtQty8.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate8.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate8.Text == "0" || txtRate8.Text == "0.0")
                                                txtRate8.Text = "";
                                        }
                                        else
                                            txtRate8.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService8.Text == "" || (txtGoodService8.Text.Length > 8 && txtGoodService8.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption8.Text = "";
                                            txtUnit8.Text = "";
                                            txtTotal8.Text = "";
                                            txtDiscount8.Text = "";
                                            txtTaxableValue8.Text = "";
                                            txtQty8.Text = "";
                                            txtRate8.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty8.Text == null || ((txtQty8.Text == "") && (txtQty8.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty8.Text = "";
                                        txtTotal8.Text = "";
                                        txtDiscount8.Text = "";
                                        txtTaxableValue8.Text = "";
                                        txtRate8.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty8.Text = "";
                                            txtQty8.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty8.Text == "0" || txtQty8.Text == "0.0")
                                                txtQty8.Text = "";
                                        }
                                        else
                                            txtQty8.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal8.Text = "";
                                            txtTotal8.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal8.Text == "0" || txtTotal8.Text == "0.0")
                                                txtTotal8.Text = "";
                                        }
                                        else
                                            txtTotal8.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue8.Text = "";
                                            txtTaxableValue8.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue8.Text == "0" || txtTaxableValue8.Text == "0.0")
                                                txtTaxableValue8.Text = "";
                                        }
                                        else
                                            txtTaxableValue8.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate8.Text = "";
                                            txtRate8.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate8.Text == "0" || txtRate8.Text == "0.0")
                                                txtRate8.Text = "";
                                        }
                                        else
                                            txtRate8.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount8.Text = "";
                                            txtDiscount8.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount8.Text == "") || (txtDiscount8.Text == "0") || (txtDiscount8.Text == "0.0"))
                                                txtDiscount8.Text = "";
                                        }
                                        else
                                            txtDiscount8.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate8.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate8.Text == "0" || (txtRate8.Text == "0.0"))
                                            txtRate8.Text = "";
                                    }
                                    else
                                        txtRate8.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty8.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty8.Text == "0" || (txtQty8.Text == "0.0"))
                                            txtQty8.Text = "";
                                    }
                                    else
                                        txtQty8.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal8.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal8.Text == "0" || (txtTotal8.Text == "0.0"))
                                            txtTotal8.Text = "";
                                    }
                                    else
                                        txtTotal8.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue8.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue8.Text == "0" || (txtTaxableValue8.Text == "0.00") || (txtTaxableValue8.Text == "0.0"))
                                            txtTaxableValue8.Text = "";
                                    }
                                    else
                                        txtTaxableValue8.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount8.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount8.Text == "") || (txtDiscount8.Text == "0") || (txtDiscount8.Text == "0.0"))
                                            txtDiscount8.Text = "";
                                    }
                                    else
                                        txtDiscount8.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }

                if (lineID == 9 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 9 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService9.Text == null)
                                    {
                                        txtGoodService9.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService9.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService9.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption9.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal9.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal9.Text == "0" || txtTotal9.Text == "0.0")
                                                txtTotal9.Text = "";
                                        }
                                        else
                                            txtTotal9.Text = "";

                                        // initalized to ""
                                        txtUnit9.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue9.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue9.Text == "0" || txtTaxableValue9.Text == "0.0")
                                                txtTaxableValue9.Text = "";
                                        }
                                        else
                                            txtTaxableValue9.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount9.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount9.Text == "0" || txtDiscount9.Text == "0.0")
                                                txtDiscount9.Text = "";
                                        }
                                        else
                                            txtDiscount9.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty9.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty9.Text == "0" || txtQty9.Text == "0.0")
                                                txtQty9.Text = "";
                                        }
                                        else
                                            txtQty9.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate9.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate9.Text == "0" || txtRate9.Text == "0.0")
                                                txtRate9.Text = "";
                                        }
                                        else
                                            txtRate9.Text = "";

                                        //if (txtGoodService9.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService9.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService9.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService9.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption9.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal9.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal9.Text == "0" || txtTotal9.Text == "0.0")
                                                txtTotal9.Text = "";
                                        }
                                        else
                                            txtTotal9.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit9.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue9.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue9.Text == "0" || txtTaxableValue9.Text == "0.0")
                                                txtTaxableValue9.Text = "";
                                        }
                                        else
                                            txtTaxableValue9.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount9.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount9.Text == "0" || txtDiscount9.Text == "0.0")
                                                txtDiscount9.Text = "";
                                        }
                                        else
                                            txtDiscount9.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty9.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty9.Text == "0" || txtQty9.Text == "0.0")
                                                txtQty9.Text = "";
                                        }
                                        else
                                            txtQty9.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate9.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate9.Text == "0" || txtRate9.Text == "0.0")
                                                txtRate9.Text = "";
                                        }
                                        else
                                            txtRate9.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService9.Text == "" || (txtGoodService9.Text.Length > 8 && txtGoodService9.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption9.Text = "";
                                            txtUnit9.Text = "";
                                            txtTotal9.Text = "";
                                            txtDiscount9.Text = "";
                                            txtTaxableValue9.Text = "";
                                            txtQty9.Text = "";
                                            txtRate9.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty9.Text == null || ((txtQty9.Text == "") && (txtQty9.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty9.Text = "";
                                        txtTotal9.Text = "";
                                        txtDiscount9.Text = "";
                                        txtTaxableValue9.Text = "";
                                        txtRate9.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty9.Text = "";
                                            txtQty9.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty9.Text == "0" || txtQty9.Text == "0.0")
                                                txtQty9.Text = "";
                                        }
                                        else
                                            txtQty9.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal9.Text = "";
                                            txtTotal9.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal9.Text == "0" || txtTotal9.Text == "0.0")
                                                txtTotal9.Text = "";
                                        }
                                        else
                                            txtTotal9.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue9.Text = "";
                                            txtTaxableValue9.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue9.Text == "0" || txtTaxableValue9.Text == "0.0")
                                                txtTaxableValue9.Text = "";
                                        }
                                        else
                                            txtTaxableValue9.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate9.Text = "";
                                            txtRate9.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate9.Text == "0" || txtRate9.Text == "0.0")
                                                txtRate9.Text = "";
                                        }
                                        else
                                            txtRate9.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount9.Text = "";
                                            txtDiscount9.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount9.Text == "") || (txtDiscount9.Text == "0") || (txtDiscount9.Text == "0.0"))
                                                txtDiscount9.Text = "";
                                        }
                                        else
                                            txtDiscount9.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate9.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate9.Text == "0" || (txtRate9.Text == "0.0"))
                                            txtRate9.Text = "";
                                    }
                                    else
                                        txtRate9.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty9.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty9.Text == "0" || (txtQty9.Text == "0.0"))
                                            txtQty9.Text = "";
                                    }
                                    else
                                        txtQty9.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal9.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal9.Text == "0" || (txtTotal9.Text == "0.0"))
                                            txtTotal9.Text = "";
                                    }
                                    else
                                        txtTotal9.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue9.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue9.Text == "0" || (txtTaxableValue9.Text == "0.00") || (txtTaxableValue9.Text == "0.0"))
                                            txtTaxableValue9.Text = "";
                                    }
                                    else
                                        txtTaxableValue9.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount9.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount9.Text == "") || (txtDiscount9.Text == "0") || (txtDiscount9.Text == "0.0"))
                                            txtDiscount9.Text = "";
                                    }
                                    else
                                        txtDiscount9.Text = "";


                                    break;
                                }
                        }

                    }
                    ++mustangStopper;
                }

                if (lineID == 10 && mustangStopper == 0)
                {
                    // assign it
                    jockey = lineID;
                    i = --jockey;
                    if (lineCollection[i].LineID == 10 && mustangStopper == 0)
                    {
                        switch (controlType)
                        {
                            // this is for all user fiddling in HSN code 
                            case 1:
                                {

                                    if (txtGoodService10.Text == null)
                                    {
                                        txtGoodService10.Text = (lineCollection[i].HSN.HSNNumber).ToString();
                                    }

                                    // scenario handled user entered a valid hsn and populated the HSN class in the first go
                                    // then he makes the txtgoodservice code field blank
                                    // A) Field blank
                                    else if ((txtGoodService10.Text == "") && ((lineCollection[i].HSN.HSNNumber).ToString() != ""))
                                    {
                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        // initalized to ""
                                        txtGoodService10.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // initalized to ""
                                        txtGoodServiceDesciption10.Text = lineCollection[i].HSN.Description;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TotalLineIDWise.ToString() == "0" && lineCollection[i].TotalLineIDWise.ToString() != "")
                                        {
                                            txtTotal10.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal10.Text == "0" || txtTotal10.Text == "0.0")
                                                txtTotal10.Text = "";
                                        }
                                        else
                                            txtTotal10.Text = "";

                                        // initalized to ""
                                        txtUnit10.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].TaxValue.ToString() == "0" && lineCollection[i].TaxValue.ToString() == "")
                                        {
                                            txtTaxableValue10.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue10.Text == "0" || txtTaxableValue10.Text == "0.0")
                                                txtTaxableValue10.Text = "";
                                        }
                                        else
                                            txtTaxableValue10.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Discount.ToString() == "0" && lineCollection[i].Discount.ToString() == "")
                                        {
                                            txtDiscount10.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount10.Text == "0" || txtDiscount10.Text == "0.0")
                                                txtDiscount10.Text = "";
                                        }
                                        else
                                            txtDiscount10.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty10.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty10.Text == "0" || txtQty10.Text == "0.0")
                                                txtQty10.Text = "";
                                        }
                                        else
                                            txtQty10.Text = "";

                                        // obj update to 0 , coz of decimal type ,but mapping that value to 0 means presence of value in a 
                                        // financial applicaiton
                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate10.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate10.Text == "0" || txtRate10.Text == "0.0")
                                                txtRate10.Text = "";
                                        }
                                        else
                                            txtRate10.Text = "";

                                        //if (txtGoodService10.Text == "")
                                        //    DisplayMessage("HSN/SAC is blank.Enter a Valid HSN/SAC");

                                    }
                                    //B ) Enters a valid HSN ,in the third attempt , that is 
                                    // first attempt - valid HSN/SAC
                                    // second attempt- emply HSN/SAC or invalid HSN
                                    // third attempt- valid HSN
                                    // OR 
                                    // It comes in the whenever there is valid hsn 

                                    else if ((txtGoodService10.Text != "") && ((lineCollection[i].HSN.HSNNumber).ToString() != "") && txtGoodService10.Text.Equals(lineCollection[i].HSN.HSNNumber.ToString()))
                                    {

                                        // Update UI component -- remove the hardcoding-TO DO
                                        // in the top most if , line id sancity is maintained
                                        txtGoodService10.Text = lineCollection[i].HSN.HSNNumber.ToString();

                                        // readonly field as of now , so direct assign
                                        txtGoodServiceDesciption10.Text = lineCollection[i].HSN.Description;

                                        if (lineCollection[i].TotalLineIDWise.ToString().Equals("0") && lineCollection[i].TotalLineIDWise.ToString().Equals(""))
                                        {
                                            txtTotal10.Text = lineCollection[i].TotalLineIDWise.ToString();
                                            if (txtTotal10.Text == "0" || txtTotal10.Text == "0.0")
                                                txtTotal10.Text = "";
                                        }
                                        else
                                            txtTotal10.Text = "";

                                        // readonly field as of now , so direct assign
                                        txtUnit10.Text = lineCollection[i].HSN.UnitOfMeasurement;

                                        if (lineCollection[i].TaxValue.ToString().Equals("0") && lineCollection[i].TaxValue.ToString().Equals(""))
                                        {
                                            txtTaxableValue10.Text = lineCollection[i].TaxValue.ToString();
                                            if (txtTaxableValue10.Text == "0" || txtTaxableValue10.Text == "0.0")
                                                txtTaxableValue10.Text = "";
                                        }
                                        else
                                            txtTaxableValue10.Text = "";

                                        if (lineCollection[i].Discount.ToString().Equals("0") && lineCollection[i].Discount.ToString().Equals(""))
                                        {
                                            txtDiscount10.Text = lineCollection[i].Discount.ToString();
                                            if (txtDiscount10.Text == "0" || txtDiscount10.Text == "0.0")
                                                txtDiscount10.Text = "";
                                        }
                                        else
                                            txtDiscount10.Text = "";

                                        if (lineCollection[i].Qty.ToString() == "0" && lineCollection[i].Qty.ToString() == "")
                                        {
                                            txtQty10.Text = lineCollection[i].Qty.ToString();
                                            if (txtQty10.Text == "0" || txtQty10.Text == "0.0")
                                                txtQty10.Text = "";
                                        }
                                        else
                                            txtQty10.Text = "";

                                        if (lineCollection[i].PerUnitRate.ToString() == "0" && lineCollection[i].PerUnitRate.ToString() == "")
                                        {
                                            txtRate10.Text = lineCollection[i].PerUnitRate.ToString();
                                            if (txtRate10.Text == "0" || txtRate10.Text == "0.0")
                                                txtRate10.Text = "";
                                        }
                                        else
                                            txtRate10.Text = "";
                                    }
                                    //Default condition
                                    else
                                    {
                                        if (txtGoodService10.Text == "" || (txtGoodService10.Text.Length > 8 && txtGoodService10.Text.Length > 8))
                                        {

                                            txtGoodServiceDesciption10.Text = "";
                                            txtUnit10.Text = "";
                                            txtTotal10.Text = "";
                                            txtDiscount10.Text = "";
                                            txtTaxableValue10.Text = "";
                                            txtQty10.Text = "";
                                            txtRate10.Text = "";
                                        }
                                    }
                                    break;
                                }
                            // this is for all user fiddling in qty code 
                            case 2:
                                {
                                    if (txtQty10.Text == null || ((txtQty10.Text == "") && (txtQty10.Text.ToString() != lineCollection[i].Qty.ToString())))
                                    {
                                        txtQty10.Text = "";
                                        txtTotal10.Text = "";
                                        txtDiscount10.Text = "";
                                        txtTaxableValue10.Text = "";
                                        txtRate10.Text = "";
                                    }
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    else
                                    {
                                        if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                        {
                                            txtQty10.Text = "";
                                            txtQty10.Text = (lineCollection[i].Qty.ToString());

                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtQty10.Text == "0" || txtQty10.Text == "0.0")
                                                txtQty10.Text = "";
                                        }
                                        else
                                            txtQty10.Text = "";

                                        if (lineCollection[i].TotalLineIDWise.ToString() != "0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                        {
                                            txtTotal10.Text = "";
                                            txtTotal10.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTotal10.Text == "0" || txtTotal10.Text == "0.0")
                                                txtTotal10.Text = "";
                                        }
                                        else
                                            txtTotal10.Text = "";

                                        if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                        {
                                            txtTaxableValue10.Text = "";
                                            txtTaxableValue10.Text = (lineCollection[i].TaxValue.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtTaxableValue10.Text == "0" || txtTaxableValue10.Text == "0.0")
                                                txtTaxableValue10.Text = "";
                                        }
                                        else
                                            txtTaxableValue10.Text = "";

                                        if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                        {
                                            txtRate10.Text = "";
                                            txtRate10.Text = (lineCollection[i].PerUnitRate.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if (txtRate10.Text == "0" || txtRate10.Text == "0.0")
                                                txtRate10.Text = "";
                                        }
                                        else
                                            txtRate10.Text = "";

                                        if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                        {
                                            txtDiscount10.Text = "";
                                            txtDiscount10.Text = (lineCollection[i].Discount.ToString());
                                            // 0.0m is the value assigned when there is no value 
                                            // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                            if ((txtDiscount10.Text == "") || (txtDiscount10.Text == "0") || (txtDiscount10.Text == "0.0"))
                                                txtDiscount10.Text = "";
                                        }
                                        else
                                            txtDiscount10.Text = "";

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    // scenario handled user entered a valid qty and populated rate 
                                    // then he made the qty field blank
                                    // A) Field blankbreak;
                                    if ((lineCollection[i].PerUnitRate).ToString() != "" || (lineCollection[i].PerUnitRate).ToString() != null)
                                    {
                                        txtRate10.Text = (lineCollection[i].PerUnitRate.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtRate10.Text == "0" || (txtRate10.Text == "0.0"))
                                            txtRate10.Text = "";
                                    }
                                    else
                                        txtRate10.Text = "";


                                    if ((lineCollection[i].Qty).ToString() != "" || (lineCollection[i].Qty).ToString() != null)
                                    {
                                        txtQty10.Text = (lineCollection[i].Qty.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtQty10.Text == "0" || (txtQty10.Text == "0.0"))
                                            txtQty10.Text = "";
                                    }
                                    else
                                        txtQty10.Text = "";

                                    if (lineCollection[i].TotalLineIDWise.ToString() != "0.0" || lineCollection[i].TotalLineIDWise.ToString() != "" || lineCollection[i].TotalLineIDWise.ToString() != null)
                                    {
                                        txtTotal10.Text = (lineCollection[i].TotalLineIDWise.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTotal10.Text == "0" || (txtTotal10.Text == "0.0"))
                                            txtTotal10.Text = "";
                                    }
                                    else
                                        txtTotal10.Text = "";

                                    if (lineCollection[i].TaxValue.ToString() != "" || lineCollection[i].TaxValue.ToString() != null)
                                    {
                                        txtTaxableValue10.Text = (lineCollection[i].TaxValue.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if (txtTaxableValue10.Text == "0" || (txtTaxableValue10.Text == "0.00") || (txtTaxableValue10.Text == "0.0"))
                                            txtTaxableValue10.Text = "";
                                    }
                                    else
                                        txtTaxableValue10.Text = "";

                                    if (lineCollection[i].Discount.ToString() != "" || lineCollection[i].Discount.ToString() != null)
                                    {
                                        txtDiscount10.Text = (lineCollection[i].Discount.ToString());
                                        // 0.0m is the value assigned when there is no value 
                                        // but 0 in rate means a a qty , not absence of it . So 0 will give ambiguity
                                        if ((txtDiscount10.Text == "") || (txtDiscount10.Text == "0") || (txtDiscount10.Text == "0.0"))
                                            txtDiscount10.Text = "";
                                    }
                                    else
                                        txtDiscount10.Text = "";


                                    break;
                                }
                        }
                    }
                    ++mustangStopper;
                }
            }

            #endregion

        }

        /// <summary>
        /// this funcition is there to show 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="seller"></param>
        private void Map2UI(List<LineEntry> lines, Seller seller)
        {
            // rblInvoicePriority
            if (seller != null)
            {
                txtSellerGSTIN.Text = seller.GSTIN;
                txtSellerName.Text = seller.NameAsOnGST;
                txtSellerAddress.Text = seller.Address;
                txtInvoiceNumber.Text = seller.SellerInvoice;
                txtInvoiceDate.Text = seller.DateOfInvoice;

            }

            if (seller.Reciever != null)
            {
                txtRecieverGSTIN.ReadOnly = false;
                txtRecieverGSTIN.Text = seller.Reciever.GSTIN;
                txtRecieverName.Text = seller.Reciever.NameAsOnGST;
                txtRecieverAddress.Text = seller.Reciever.Address;
                txtRecieverState.Text = seller.Reciever.StateName;
                txtRecieverStateCode.Text = seller.Reciever.StateCode;
            }

            if (seller.Consignee != null)
            {
                txtConsigneeGSTIN.ReadOnly = false;
                txtConsigneeGSTIN.Text = seller.Consignee.GSTIN;
                txtConsigneeName.Text = seller.Consignee.NameAsOnGST;
                txtConsigneeAddress.Text = seller.Consignee.Address;
                txtConsigneeState.Text = seller.Consignee.StateName;
                txtConsigneeStateCode.Text = seller.Consignee.StateCode;


            }

            if (seller.Invoice != null)
            {
                if (!string.IsNullOrEmpty(seller.Invoice.InvoiceType))
                {
                    switch (seller.Invoice.InvoiceType)
                    {
                        case "":
                            {
                                break;
                            }
                        //case "":
                        //    {
                        //        break;
                        //    }
                        //case "":
                        //    {
                        //        break;
                        //    }
                    }
                }
            }
            else
                // this is the case , when first line entry hsn is not in the purchase legder 
                // then the user updates the register and UpdatePurchaseRegister.aspx.cs page redirects to the 
                rblInvoicePriority.SelectedIndex = 0;

            if (Session["LineEntryCollections"] != null && seller.SaleLedger != null)
            {
                lineCollection = (List<LineEntry>)Session["LineEntryCollections"];

                // if lineCollection is equal to 0 then we will get 
                if (lineCollection.Count > 0)
                {

                    #region when we have lineCollection duly populated-it means the HSN entered were there in sale ledger

                    foreach (var line in lineCollection)
                    {
                        if (line.LineID != 0 && line.LineID == 1)
                        {
                            // assing the hsn in the in the control back to the invoice page from the update register  page 
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService1.ReadOnly = false;
                                txtGoodService1.Text = line.HSN.HSNNumber;
                            }

                            // for description
                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption1.Text = line.HSN.Description;
                            }

                            // for qty
                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption1.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 2)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService2.ReadOnly = false;
                                txtGoodService2.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption2.Text = line.HSN.Description;
                            }
                        }

                        if (line.LineID != 0 && line.LineID == 3)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService3.ReadOnly = false;
                                txtGoodService3.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption3.Text = line.HSN.Description;
                            }
                        }

                        if (line.LineID != 0 && line.LineID == 4)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService4.ReadOnly = false;
                                txtGoodService4.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption4.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 5)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService5.ReadOnly = false;
                                txtGoodService5.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption5.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 6)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService6.ReadOnly = false;
                                txtGoodService6.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption6.Text = line.HSN.Description;
                            }
                        }

                        if (line.LineID != 0 && line.LineID == 7)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService7.ReadOnly = false;
                                txtGoodService7.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption7.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 8)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService8.ReadOnly = false;
                                txtGoodService8.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption8.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 9)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService9.ReadOnly = false;
                                txtGoodService9.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption9.Text = line.HSN.Description;
                            }

                        }

                        if (line.LineID != 0 && line.LineID == 10)
                        {
                            if (!string.IsNullOrEmpty(line.HSN.HSNNumber))
                            {
                                txtGoodService10.ReadOnly = false;
                                txtGoodService10.Text = line.HSN.HSNNumber;
                            }

                            if (!string.IsNullOrEmpty(line.HSN.Description))
                            {
                                txtGoodServiceDesciption10.Text = line.HSN.Description;
                            }

                        }


                    }
                    #endregion
                }
                else
                {
                    // this case is applicable only when we have the hsn not present in the sale register iff hsn entered is in the line entry -1

                    // this part is for HSN
                    TextBox textBxHSN = new TextBox();
                    // we will get the latest entry in the sale ledger
                    int index = seller.SaleLedger.StockLineEntry.Count;
                    textBxHSN.Text = seller.SaleLedger.StockLineEntry[--index].Hsn;
                    int lineCollectionCount = lineCollection.Count;
                    lineCollectionCount = (lineCollectionCount > 0 ? --lineCollectionCount : 1);

                    textBxHSN.ID = "txtGoodService" + lineCollectionCount;
                    textBxHSN.ReadOnly = false;
                    Object sender = textBxHSN;
                    EventArgs e = new EventArgs();
                    //GetGoodsOrServiceInfo(sender, e);

                    // this part is for checking qty in HSN
                    // TO DO
                }

            }

        }

        public static string ConvertNumbertoWords(decimal number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return "MINUS " + ConvertNumbertoWords(Math.Abs(number));

            string words = String.Empty;

            long intPortion = (long)number;
            decimal fraction = (number - intPortion);
            int decimalPrecision = GetDecimalPrecision(number);

            fraction = CalculateFraction(decimalPrecision, fraction);

            long decPortion = (long)fraction;

            words = IntToWords(intPortion);
            if (decPortion > 0)
            {
                words += " POINT ";
                words += IntToWords(decPortion);
            }

            return words.Trim();
        }

        public static string IntToWords(long number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return "MINUS " + IntToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000000000) > 0)
            {
                words += IntToWords(number / 1000000000000000) + " QUADRILLION ";
                number %= 1000000000000000;
            }

            if ((number / 1000000000000) > 0)
            {
                words += IntToWords(number / 1000000000000) + " TRILLION ";
                number %= 1000000000000;
            }

            if ((number / 1000000000) > 0)
            {
                words += IntToWords(number / 1000000000) + " BILLION ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += IntToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += IntToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += IntToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != String.Empty)
                    words += "AND ";

                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        private static int GetDecimalPrecision(decimal number)
        {
            return (Decimal.GetBits(number)[3] >> 16) & 0x000000FF;
        }

        private static decimal CalculateFraction(int decimalPrecision, decimal fraction)
        {
            switch (decimalPrecision)
            {
                case 1:
                    return fraction * 10;
                case 2:
                    return fraction * 100;
                case 3:
                    return fraction * 1000;
                case 4:
                    return fraction * 10000;
                case 5:
                    return fraction * 100000;
                case 6:
                    return fraction * 1000000;
                case 7:
                    return fraction * 10000000;
                case 8:
                    return fraction * 100000000;
                case 9:
                    return fraction * 1000000000;
                case 10:
                    return fraction * 10000000000;
                case 11:
                    return fraction * 100000000000;
                case 12:
                    return fraction * 1000000000000;
                case 13:
                    return fraction * 10000000000000;
                default:
                    return fraction * 10000000000000;
            }
        }

        #region GetSellerPurchaseRegisterDetails
        public void GetSellerPurchaseRegisterDetails()
        {
            // GETING THE OBJECT PROPERTY FROM SESSION
            if (Session["seller"] != null)
                seller = (Seller)Session["seller"];

            if (!string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()))
            {
                try
                {
                    // we seller.GetSellerPurchaseRegisterData(seller.gstin);
                    // Inside the above method , a new instance of seller was getting created which was setting the attributes of seller to null
                    // an issue surfaced after injecting the code for Purchase register
                    seller = seller.GetSellerPurchaseRegisterData(seller);
                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Purhcase Register Object Creation Issues");
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //BalajiGSPLogger logger = new //BalajiGSPLogger();
                    //logger.LogError(Request.Path, Nullex);
                }
                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //BalajiGSPLogger logger = new //BalajiGSPLogger();
                    ////logger.LogError(Request.Path, ex);
                }

                // Seller data strored in session
                if (seller != null)
                    Session["seller"] = seller;

            }

        }



        #endregion


        #region BACK_TO_PREVIOUS_PAGE
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAllSession();

            Response.Redirect("~/GSTInvoiceDashBoard.aspx", false);
            this.Context.ApplicationInstance.CompleteRequest();
        }
        #endregion

        #region REFRESH
        public void refresh()
        {

            strInvoiceType = HttpUtility.UrlEncode(excelDB.Encrypt(strInvoiceType));

            Response.Redirect("~/GSTinvoice.aspx?BID=" + strInvoiceType);

        }
        #endregion

        #region Discard
        protected void btnDiscardClick(object sender, EventArgs e)
        {
            // clear all objects in the session
            ClearAllSession();

            // why false nd complete request refer to BalajiGSP Error Code
            Response.Redirect("~/GSTInvoiceDashBoard.aspx", false);
            this.Context.ApplicationInstance.CompleteRequest();

        }
        #endregion

        #region ClearAllSession
        private void ClearAllSession()
        {
            //removes all the objects stored in a Session
            Session.Abandon();

        }
        #endregion

        public void BindNotifiedHSN(dynamic listNotitfiedItems, ListView gv)
        {

            //DataTable dt = new DataTable();

            //dt.Columns.Add("SerialNo");
            //dt.Columns.Add("HSNNumber");
            //dt.Columns.Add("NotificationNo");
            //dt.Columns.Add("NotificationSNo");
            ////dt.Columns.Add("Description");
            //dt.Columns.Add("Tarrif");

            //foreach (var item in notify)
            //{
            //    var row = dt.NewRow();

            //    row["SerialNo"] = item.SerialNo;
            //    row["HSNNumber"] = HSN;
            //    row["NotificationNo"] = Convert.ToString(item.NotificationNo);
            //    row["NotificationSNo"] = Convert.ToString(item.NotificationSerialNo);
            //    //row["Description"] = Desc;
            //    row["Tarrif"] = item.Tax;

            //    dt.Rows.Add(row);
            //}

            gv.DataSource = listNotitfiedItems;
            gv.DataBind();
            // HSNModalPopupExtender.Show();

            //  return dt;
        }

        // 7th pass - ideally
        #region Total, TotalTax Value per lineitem.
        protected void GetTaxValue(object sender, EventArgs e)
        {
            // getting the complete state of LineEntries from the 
            // explicit casting is required
            if (Session["LineEntryCollections"] != null)
            {

                lineCollection = (List<LineEntry>)Session["LineEntryCollections"];
            }

            // reassigning the value of seller
            if (Session["seller"] != null)
                seller = (Seller)Session["seller"];




            // get the id of text box that caused this event fired and subsequent post back
            // getting the value from the sender obj
            TextBox textBox = (sender as TextBox);

            // getting the text box id
            string HSHtxtID = textBox.ID;

            // two variable would be used across this control , that is why the scope is here 
            int chkLineIDVal = -1; // default value provided
            bool status = false;// default value provided

            //based on above id of the control, pick the specific line id 
            // do BVA on rate, dicoount(also less than 100)
            // do BVA on total, taxable value and all
            // get the values stored in qty, assign it to qty 
            // get the values strored in the rate , check not null, assign it to perUnitRate
            // get the value of discount, assign it to discount
            // Calcualte total=qty * rate -discount 

            switch (HSHtxtID)
            {
                #region dis--1
                case "txtDiscount1":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 1)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region disc---2
                case "txtDiscount2":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 2)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty2.Text))
                                    line.Qty = Convert.ToDecimal(txtQty2.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate2.Text.ToString())) && (Convert.ToDecimal(txtRate2.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate2.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }

                    break;
                #endregion

                #region dis-- 3
                case "txtDiscount3":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 3)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis--4
                case "txtDiscount4":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 4)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis--5
                case "txtDiscount5":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 5)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis--6
                case "txtDiscount6":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 6)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis--7
                case "txtDiscount7":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 7)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis-8
                case "txtDiscount8":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 8)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis-9
                case "txtDiscount9":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 9)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                #region dis-10
                case "txtDiscount10":
                    foreach (var line in lineCollection)
                    {
                        if (line.LineID == 10)
                        {
                            chkLineIDVal = seller.GetLineIDOfInvoice(HSHtxtID);
                            status = seller.CheckLineIDExist(lineCollection, chkLineIDVal);

                            // Try parse instead
                            // below if checks , whetehr qty is null in the line.qty , then no need to do anything 
                            if (string.IsNullOrEmpty(line.Qty.ToString()))
                            {
                                if (!string.IsNullOrEmpty(txtQty1.Text))
                                    line.Qty = Convert.ToDecimal(txtQty1.Text);
                                else
                                {

                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Please enter Quantity";
                                    Page.Form.DefaultFocus = txtQty1.ClientID;
                                    //masterPage.ShowModalPopup();
                                    return;

                                }
                            }



                            // if user has not entered anything nothing will happen
                            // if entered then BVA is done
                            //decimal.tryparse , some issues , remove it --TO DO :Pramod
                            if ((!string.IsNullOrEmpty(txtRate1.Text.ToString())) && (Convert.ToDecimal(txtRate1.Text.ToString()) < Decimal.MaxValue))
                            {
                                line.PerUnitRate = Convert.ToDecimal(txtRate1.Text.ToString());
                                // caluculate total 
                                //// Getting the total
                                line.TotalLineIDWise = seller.Invoice.GetTotalLineIDWise(line.Qty, line.PerUnitRate);

                                if (line.TotalLineIDWise < Decimal.MaxValue)
                                {
                                    // Calculating the tax value
                                    line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);


                                    // Figure out destination of consumption for the HSN concerned
                                    line.IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                                    if (line.IsIntra)
                                    {
                                        line.TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                                    }

                                    if (line.IsIntra)
                                    {
                                        line.AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(line.TaxValue, line.HSN.RateIGST);
                                    }
                                    else
                                    {
                                        line.AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(line.TaxValue, line.HSN.RateCGST);
                                        line.AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(line.TaxValue, line.HSN.RateSGST);
                                    }
                                }
                                else
                                {
                                    // wash away the irregular input
                                    textBox.Text = "";
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    //masterPage.ErrorMessage = "Error - Overflow in the total, Cannot bite more than I chew. ";
                                    //masterPage.ShowModalPopup();
                                }

                                //if user has entered discount then do the above step
                                //decimal.tryparse , some issues , remove it --TO DO :Pramod
                                // 79228162514264337593543950335m , put this value to 
                                if ((!string.IsNullOrEmpty(textBox.Text.ToString())) && (Convert.ToDecimal(textBox.Text.ToString()) < Decimal.MaxValue))
                                {
                                    // discount is one item that needs to captured on real time , based on user input
                                    line.Discount = Convert.ToDecimal(textBox.Text.ToString());
                                    if (line.Discount < 100)
                                    {
                                        // 
                                        // Calculating the tax value after discount is offered by the seller
                                        line.TaxValue = seller.Invoice.GetTaxableValueLineIDWise(line.TotalLineIDWise, line.Discount);
                                    }
                                    else
                                    {
                                        // wash away the irregular input
                                        textBox.Text = "";
                                        // TO DO ::  Use some other option to do this-- Aashis 
                                        BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                        //masterPage.ErrorMessage = "Error - Mate give it for free. ";
                                        //masterPage.ShowModalPopup();
                                    }
                                }
                                else// if user invokes the Discount text change without entering 
                                {
                                    lineCollection = GetUpdatedQuantity(textBox, line, (int)ControlType.GetDiscount);
                                    // TO DO ::  Use some other option to do this-- Aashis 
                                    //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                    ////masterPage.ErrorMessage = "Please enter discount(%)";
                                    //Page.Form.DefaultFocus = textBox.ClientID;
                                    ////masterPage.ShowModalPopup();
                                }
                            }
                            else
                            // if user invokes the unit rate text change without entering 
                            {
                                // TO DO ::  Use some other option to do this-- Aashis 
                                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                                //masterPage.ErrorMessage = "Please enter the Unit Rate. ";
                                Page.Form.DefaultFocus = textBox.ClientID;
                                //masterPage.ShowModalPopup();
                            }
                            //assign it to UI , total 
                            Map2UI(lineCollection, chkLineIDVal, (int)ControlType.GetTotalAmnt);
                            EnableNextLineItem(lineCollection, chkLineIDVal);

                        }

                    }
                    break;
                #endregion

                default:

                    break;
            }
            // Putting the line entries back to session
            if (lineCollection != null)
            {
                Session["LineEntryCollections"] = lineCollection;
            }

            // reassigning the value of seller
            if (seller != null)
                Session["seller"] = seller;
        }
        #endregion


        protected void GetHSNDetails(object sender, EventArgs e)
        {

        }
        protected void imageEdit(object sender, ImageClickEventArgs e)
        { }

        protected void rblHSNID_CheckedChanged(object sender, EventArgs e)
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            //int rowIndex = ((sender as Button).NamingContainer as ListView).RowIndex;
            Button btnTariff = (Button)sender;
            int tariff = Convert.ToInt32(btnTariff.CommandArgument.ToString());
            //Get the value of column from the DataKeys using the RowIndex.
            //int id = Convert.ToInt32(lvHSNData.DataKeys[rowIndex].Values[0]);
            // HSNModalPopupExtender.Show();
            // mpeOtherNotification.Show();
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        #region ADD_LINE
        public void btnAddMore_Click(object sender, EventArgs e)
        {
            lineItem6.Visible = true;
            lineItem7.Visible = true;
            lineItem8.Visible = true;
            lineItem9.Visible = true;
            lineItem10.Visible = true;
            btnAddMore.Visible = false;
            btnRemoveMore.Visible = true;
        }
        #endregion

        #region REMOVE_LINE
        public void btnRemoveMore_Click(object sender, EventArgs e)
        {
            lineItem6.Visible = false;
            lineItem7.Visible = false;
            lineItem8.Visible = false;
            lineItem9.Visible = false;
            lineItem10.Visible = false;
            btnAddMore.Visible = true;
            btnRemoveMore.Visible = false;
        }
        #endregion



        #region HSN_NOTIFICATION
        protected void btnOK_Click(object sender, EventArgs e)
        {
            decimal Tarrif = 0;
            string selectedValue = Request.Form["MyRadioButton"];
            Session["Tarrif"] = selectedValue;
            if (Session["Tarrif"] != null)
            {
                Tarrif = Convert.ToDecimal(Session["Tarrif"].ToString());
            }


            if (Session["Seller"] != null)
                seller = (Seller)Session["seller"];


            if (Session["LineEntryCollections"] != null)
                lineCollection = (List<LineEntry>)Session["LineEntryCollections"];


            // If HSN is notified then have to change the calculations w.r.t. the notified hsn. otherwise not required.
            //if (Session["LID"] != null) // need to maintain the lineID of line entry during edit.
            //{
            //    LID = (int)Session["LID"];

            //    if (chkExport.Checked == false) // IN CASE OF EXPORT ONLY IGST IS APPLICABLE.
            //    {
            //        // Figure out destination of consumption for the HSN concerned
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).IsInter = seller.Invoice.GetConsumptionDestinationOfGS(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

            //        if (seller.Invoice.LineEntry.ElementAt(LID - 1).IsInter)
            //        {
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).TaxBenefitingState = seller.Invoice.GetTaxBenefittingState_IntraTransaction(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
            //        }


            //        if (seller.Invoice.LineEntry.ElementAt(LID - 1).IsInter)
            //        {
            //            // Checking whether HSN is notified for Inter State
            //            //if (seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.IsNotified == false)
            //            //{
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST = seller.Invoice.CalculateIGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST);
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST);

            //            //    //BR: In case of Inter State supply only IGST is applicable .therefore make CGST,SGST Rate as 0(Suggested by rama Sir)
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST = 0;
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST = 0;
            //            //}
            //            //else
            //            //{
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST = seller.Invoice.CalculateIGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, Tarrif);
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST);
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST = Tarrif;

            //            //BR: In case of Inter State supply only IGST is applicable .therefore make CGST,SGST Rate as 0(Suggested by rama Sir)
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST = 0;
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST = 0;
            //            //}
            //  }


            //else
            //        {    // Checking whether HSN is notified for CGST & SGST
            //            //if (seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.IsNotified == false)
            //            //{
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmtSGST = seller.Invoice.CalculateSGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST);
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmtCGST = seller.Invoice.CalculateCGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST);
            //            //    decimal AmtCGSTSGST = seller.Invoice.CalculateTotalCGSTSGST(seller.Invoice.LineEntry.ElementAt(LID - 1).AmtCGST, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtSGST);
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, AmtCGSTSGST);

            //            //    //BR: In case of Intra State supply only IGST is applicable .therefore make IGST Rate as 0(Suggested by rama Sir)
            //            //    seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST = 0;
            //            //}
            //            //else
            //            //{
            //            //   seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.NotificatioN = new List<Notified>();
            //            // This will be displayed in UI RadioBox with shaded page
            //            //   seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.NotificatioN = seller.Invoice.GetHSNNotificationData(seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.HSNNumber);


            //            seller.Invoice.LineEntry.ElementAt(LID - 1).AmtSGST = seller.Invoice.CalculateSGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, Tarrif);
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).AmtCGST = seller.Invoice.CalculateCGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, Tarrif);
            //            decimal AmtCGSTSGST = seller.Invoice.CalculateTotalCGSTSGST(seller.Invoice.LineEntry.ElementAt(LID - 1).AmtCGST, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtSGST);
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, AmtCGSTSGST);
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST = Tarrif;
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST = Tarrif;

            //            //BR: In case of Intra State supply only IGST is applicable .therefore make IGST Rate as 0(Suggested by rama Sir)
            //            seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST = 0;
            //            //}
            //        }
            //    }
            //    else // BR: in case of export only IGST is applicable. it is not dependent on Point of Supply
            //    {
            //        //if (seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.IsNotified == false)
            //        //{
            //        //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST = seller.Invoice.CalculateIGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST);
            //        //    seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST);

            //        //    //BR: In case of Inter State supply only IGST is applicable .therefore make CGST,SGST Rate as 0(Suggested by rama Sir)
            //        //    seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST = 0;
            //        //    seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST = 0;

            //        //}
            //        //else
            //        //{
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST = seller.Invoice.CalculateIGST(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, Tarrif);
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).AmountWithTax = seller.Invoice.CalculateAmountWithTax(seller.Invoice.LineEntry.ElementAt(LID - 1).TaxValue, seller.Invoice.LineEntry.ElementAt(LID - 1).AmtIGST);
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateIGST = Tarrif;

            //        //BR: In case of Inter State supply only IGST is applicable .therefore make CGST,SGST Rate as 0(Suggested by rama Sir)
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateCGST = 0;
            //        seller.Invoice.LineEntry.ElementAt(LID - 1).HSN.RateSGST = 0;
            //        //}
            //    }
            //}

            // to handle post back
            // at this point seller contains , seller ,reciever ,consignee and intialized the appropriate the invoice type
            Session["seller"] = seller;

            //uptil here linecollection contains line item added 
            Session["LineEntryCollections"] = lineCollection;
        }
        #endregion

        protected void btnTargetHSNO_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void txtSellerGSTIN_TextChanged(object sender, EventArgs e)
        {
            txtSellerName.Focus();
            if (!string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()) && rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice.ToString())
            {
                var profile = Common.UserManager.Users.Where(w => w.GSTNNo == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (profile == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    return;
                }
                if (GetSellerProfile.GSTNNo == profile.GSTNNo)
                {
                    this.Master.WarningMessage = "Seller and Reciever can not be same.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    txtSellerGSTIN.Text = string.Empty;
                    return;
                }
                if (txtSellerGSTIN.Text.Trim() != txtRecieverGSTIN.Text.Trim())
                {
                    try
                    {
                        txtSellerName.Text = profile.OrganizationName;
                        txtSellerAddress.Text = profile.Address;
                        //txtRecieverState.Text = profile.StateCode;
                        // txtSeStateCode.Text = profile.StateCode;
                    }
                    catch (NullReferenceException Nullex)
                    {
                        NullReferenceException nullEx = new NullReferenceException("Sellert Object Creation Issues");
                        // GSTInvoiceEx invoiceEx = new GSTInvoiceEx();                                       
                    }
                    catch (Exception ex)
                    {
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    }
                }
                //txtSellerGSTIN.Focus();
                //txtSellerName.Focus();
                //txtSellerAddress.Focus();
            }


            if (!string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()) && rblInvoicePriority.SelectedValue.ToString() == EnumConstants.InvoiceSpecialCondition.Import.ToString())
            {
                var profileImporter = Common.UserManager.Users.Where(w => w.OrganizationName == txtSellerGSTIN.Text.Trim()).FirstOrDefault();
                if (profileImporter == null)
                {
                    //TODO#2 : DISPLAY THE APPROPRIATE MESSAGE- ANkita
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelMessage", "$('#viewInvoiceModelMessage').modal();", true);
                    return;
                }
                else
                {
                    txtSellerName.Text = profileImporter.OrganizationName;
                    txtSellerAddress.Text = profileImporter.Address;

                }
            }
            //amits for-pan
            //if(!string.IsNullOrEmpty(txtSellerGSTIN.Text.Trim()))
            //{
            //    if(litSelletGSTIN.Text.Trim() == "PAN No./Aadhaar No.")
            //    {
            //        if (!Regex.IsMatch(txtSellerGSTIN.Text, @"^[A-Za-z]+$"))
            //        {
            //            txtSellerGSTIN.MaxLength = 10;
            //        }
            //        else
            //            txtSellerGSTIN.MaxLength = 16;
            //    }
            //}
        }

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        ////popup message start
        //public string WarningMessageAdd
        //{
        //    get
        //    {
        //        return lblWarningadd.Text;
        //    }
        //    set
        //    {
        //        lblWarningadd.Text = value;
        //    }
        //}

        //protected void btnAccept_Click(object sender, EventArgs e)
        //{
        //    if (uc_GSTNUsers.GetValue == 0)
        //    {
        //        BindSpecialInvoiceType();
        //        litRecieverGSTIN.Text = "GSTIN";
        //        litConsigneeGSTIN.Text = "GSTIN";
        //        txtRecieverGSTIN.Attributes.Add("placeholder", "Enter Receiver's GSTIN No.");
        //        txtConsigneeGSTIN.Attributes.Add("placeholder", "Enter Consignee's GSTIN No.");

        //        txtRecieverGSTIN.MaxLength = 16;
        //        txtConsigneeGSTIN.MaxLength = 16;

        //        lblRegularMapped.Visible = false;
        //        lboxRegularMapped.Visible = false;
        //        lblinvoiceNo.Visible = false;
        //        txtInvoiceNumber.Visible = false;
        //        lblOrderDate.Visible = false;
        //        txtOrderDate.Visible = false;
        //        lboxRegularChallanMapped.Visible = false;
        //        lblRegularChallanMapped.Visible = false;
        //        GetSellerDetails();
        //        ClearRecieverField();
        //        ClearConsigneeFieldData();
        //        BindImportInvoice();
        //        ChangeImporterUIChange();
        //        ResetFormData();
        //        HsnGridClearItem();
        //    }
        //    // Response.Redirect("GSTinvoice.aspx");
        //}

        //protected void btnReject_Click(object sender, EventArgs e)
        //{

        //}
        ////End

        //public void ConvertToDatatable(List<Notified> notify, int HSN, string Desc, int HSNID)
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("SerialNo");
        //    dt.Columns.Add("HSNNumber");
        //    dt.Columns.Add("NotificationNo");
        //    dt.Columns.Add("NotificationSNo");
        //    //dt.Columns.Add("Description");
        //    dt.Columns.Add("Tarrif");

        //    foreach (var item in notify)
        //    {
        //        var row = dt.NewRow();

        //        row["SerialNo"] = item.SerialNo;
        //        row["HSNNumber"] = HSN;
        //        row["NotificationNo"] = Convert.ToString(item.NotificationNo);
        //        row["NotificationSNo"] = Convert.ToString(item.NotificationSerialNo);
        //        //row["Description"] = Desc;
        //        row["Tarrif"] = item.Tax;

        //        dt.Rows.Add(row);
        //    }

        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //   // HSNModalPopupExtender.Show();

        //    //  return dt;
        //}


        #endregion
    }
}