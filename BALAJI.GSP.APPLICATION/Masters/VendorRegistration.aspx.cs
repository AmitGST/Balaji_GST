using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using System.Data;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class VendorRegistration : System.Web.UI.Page
    {
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

        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindVendor();
                BindVendorName();
                BindTransShipment();
                BindWareHouseFacility();
                BindDesignation();
                BindOwnerShipType();
                BindStateCode();
                BindItem();
            }
        }

        private void BindVendor()
        {
            unitOfwork = new UnitOfWork();
            var userID = Common.LoggedInUserID();
            lvVendor.DataSource = unitOfwork.VendorRepository.Filter(v => v.UserID == userID).OrderByDescending(o => o.CreatedDate).ToList();
            lvVendor.DataBind();
        }

        private void BindTransShipment()
        {

            //long TransS = Convert.ToInt32(ddlVendorName.SelectedValue.Trim());

            var userID = Common.LoggedInUserID();
            lvTransShipment.DataSource = unitOfwork.TransShipmentRepositry.Filter(f => f.GST_MST_VENDOR.UserID == userID && f.Status == true && f.GST_MST_VENDOR.Status == true).OrderByDescending(o => o.CreatedDate).ToList();
            lvTransShipment.DataBind();

        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            GST_MST_VENDOR vendor = new GST_MST_VENDOR();
            try
            {
                var ITEM = unitOfwork.VendorRepository.Find(F => F.GSTNNo == txtGSTNnum.Text.Trim());
                if (ITEM == null)
                {
                    vendor.VendorName = txtEntityName.Text.Trim();
                    vendor.NameOfSignatory = txtSignatory.Text.Trim();
                    vendor.Address = txtAddress.Text.Trim();
                    vendor.EmailID = txtEmail.Text.Trim();
                    vendor.Designation = Convert.ToString(ddlDesignation.SelectedIndex);
                    vendor.CreatedDate = DateTime.Now;
                    vendor.GSTNNo = txtGSTNnum.Text.Trim();
                    vendor.OwnerShip = Convert.ToByte(ddl_OwnerType.SelectedItem.Value);
                    vendor.StateID = Convert.ToByte(ddl_State.SelectedItem.Value);
                    vendor.CreatedBy = Common.LoggedInUserID();
                    vendor.Status = true;
                    vendor.UserID = Common.LoggedInUserID();
                    //
                    //GST_MST_VENDOR_SERVICE
                    //    vendor.GST_MST_VENDOR_SERVICE=

                    List<GST_TRN_VENDOR_SERVICE> serviceitems = new List<DataAccessLayer.GST_TRN_VENDOR_SERVICE>();
                    foreach (ListItem li in lvAdd.Items)
                    {
                        GST_TRN_VENDOR_SERVICE serviceitem = new GST_TRN_VENDOR_SERVICE();
                        serviceitem.Item_ID = Convert.ToInt32(li.Value);
                        serviceitems.Add(serviceitem);

                    }
                    vendor.GST_TRN_VENDOR_SERVICE = serviceitems;
                    unitOfwork.VendorRepository.Create(vendor);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Vendor successfully saved.";
                    uc_sucess.Visible = true;
                    ClearItems();

                    BindVendor();
                    BindVendorName();
                }
                else
                {
                    uc_sucess.ErrorMessage = "GSTIN No. Already Exist!";
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess.ErrorMessage = ex.Message;
                uc_sucess.VisibleError = true;
            }

        }

        private void ClearItems()
        {
            txtGSTNnum.Text = string.Empty;
            txtEntityName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSignatory.Text = string.Empty;
            ddlDesignation.SelectedValue = "0";
            //txtDesignation.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtItem.Text = string.Empty;
            lvAdd.Items.Clear();
            ddl_OwnerType.SelectedIndex = 0;
            ddl_State.SelectedIndex = -1;
        }
        protected void btnTransShipment_Click(object sender, EventArgs e)
        {
            GST_MST_VENDOR_TRANS_SHIPMENT TransShipment = new GST_MST_VENDOR_TRANS_SHIPMENT();
            try
            {
                TransShipment.VendorID = Convert.ToInt32(ddlVendorName.SelectedValue.Trim());
                TransShipment.Item_ID = Convert.ToInt32(ddlItem.SelectedValue);
                TransShipment.VehicleRegistrationNumber = txtRegNo.Text.Trim();
                TransShipment.TransShipmentNo = txtTransShipmentNo.Text.Trim();
                TransShipment.DriverLicenceNumber = txtDriverNo.Text.Trim();
                //TransShipment.IsInterNationalWarehouseAvailed = Convert.ToBoolean(ddlfacility.SelectedItem.Value);
                TransShipment.IsInterNationalWarehouseAvailed = ddlfacility.SelectedIndex > 0 ? Convert.ToBoolean(ddlfacility.SelectedItem.Text) : false;
                TransShipment.FromLocation = txtlocation.Text.Trim();
                TransShipment.ToLocation = TxtToLocation.Text.Trim();
                DateTime TDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
                TransShipment.TransShipmentDate = TDate;
                TransShipment.BillAmount = Convert.ToDecimal(txtBillAmt.Text.Trim());
                //DateTime TransShipmentDate= DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
                TransShipment.Distance_ID = Convert.ToInt32(txtdistance.Text.Trim());
                TransShipment.CreatedBy = Common.LoggedInUserID();
                TransShipment.CreatedDate = DateTime.Now;
                TransShipment.Status = true;
                TransShipment.UpdatedBy = Common.LoggedInUserID();

                unitOfwork.TransShipmentRepositry.Create(TransShipment);
                unitOfwork.Save();
                uc_sucess_trans.SuccessMessage = "Trans Shipment (E-bill) successfully saved.";
                uc_sucess_trans.Visible = true;
                ClearTran();
                BindVendor();
                BindTransShipment();

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess_trans.ErrorMessage = ex.Message;
                uc_sucess_trans.VisibleError = true;
            }
        }

        private void ClearTran()
        {
            //txtEntityName.Text = string.Empty;
            ddlVendorName.SelectedValue = "0";
            ddlItem.SelectedValue = "0";
            txtRegNo.Text = string.Empty;
            txtDriverNo.Text = string.Empty;
            //ddlfacility.Items.Clear();
            ddlfacility.SelectedValue = "0";
            txtlocation.Text = string.Empty;
            TxtToLocation.Text = string.Empty;
            txtBillAmt.Text = string.Empty;
            txtTransShipmentNo.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtdistance.Text = string.Empty;
        }

        private void BindWareHouseFacility()
        {
            ddlfacility.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlfacility.DataTextField = "Value";
            ddlfacility.DataValueField = "Key";
            ddlfacility.DataBind();
           // ddlfacility.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindOwnerShipType()
        {
            ddl_OwnerType.DataSource = Enumeration.GetAll<EnumConstants.OwnerShipType>();
            ddl_OwnerType.DataTextField = "Value";
            ddl_OwnerType.DataValueField = "Key";
            ddl_OwnerType.DataBind();
          //  ddl_OwnerType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindDesignation()
        {
            ddlDesignation.DataSource = typeof(EnumConstants.Designation).ToList();
            ddlDesignation.DataTextField = "Value";
            ddlDesignation.DataValueField = "Key";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindVendorName()
        {
            var userId = Common.LoggedInUserID();
            ddlVendorName.DataSource = unitOfwork.VendorRepository.Filter(f => f.Status == true && f.UserID == userId).OrderBy(o => o.VendorName).Select(s => new { VendorName = s.VendorName, VendorID = s.VendorID }).ToList();
            ddlVendorName.DataTextField = "VendorName";
            ddlVendorName.DataValueField = "VendorID";
            ddlVendorName.DataBind();
            ddlVendorName.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindItem()
        {
            try
            {
                var i = -1;
                if (ddlVendorName.SelectedIndex > 0)
                    i = Convert.ToInt32(ddlVendorName.SelectedItem.Value);
                //ddlitem.DataSource = unitOfwork.ItemRepository.Filter(f => f.Status == true).OrderBy(o => o.ItemCode).Select(s => new { ItemCode = s.ItemCode, Item_ID = s.Item_ID }).ToList();
                ddlItem.DataSource = unitOfwork.VendorServiceRepositry.Filter(f => f.GST_MST_VENDOR.VendorID == i).Select(s => new { ItemCode = s.GST_MST_ITEM.ItemCode, Item_ID = s.GST_MST_ITEM.Item_ID }).ToList();
                // ddlitem.DataSource = unitOfwork.VendorServiceRepositry.Select(s => new { ItemCode = s.GST_MST_ITEM.ItemCode, Item_ID = s.GST_MST_ITEM.Item_ID });
                ddlItem.DataTextField = "ItemCode";
                ddlItem.DataValueField = "Item_ID";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindItem();
        }

        private void BindStateCode()
        {
            var statedata = unitOfwork.StateRepository.Filter(f => f.Status == true).OrderBy(o => o.StateName).Select(s => new { StateName = s.StateName, StateID = s.StateID }).ToList();
            ddl_State.DataSource = statedata;
            ddl_State.DataValueField = "StateID";
            ddl_State.DataTextField = "StateName";
            ddl_State.DataBind();
            ddl_State.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            ddl_State.DataSource = statedata;
        }
        protected void btnAd_Click(object sender, EventArgs e)
        {
            //DataTable dtitem = (DataTable)ViewState["Item"];
            //dtitem.Rows.Add();
            //dtitem.Rows[dtitem.Rows.Count - 1]["ItemName"] = txtItem.Text.Trim();
            //txtItem.Text = string.Empty;
            //ViewState["Item"] = dtitem;
            try
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    ListItem liItem = new ListItem();
                    var item = unitOfwork.ItemRepository.Find(f => f.ItemCode == txtItem.Text.Trim());
                    if (item != null)
                    {
                        liItem.Text = item.ItemCode;
                        liItem.Value = item.Item_ID.ToString();
                        if (!lvAdd.Items.Contains(liItem))
                        {
                            lvAdd.Items.Add(liItem);
                            uc_sucess1.SuccessMessage = "Added Successfully..";
                            uc_sucess1.Visible = true;
                        }
                        else
                        {
                            uc_sucess1.SuccessMessage = "Already Exist!";
                            uc_sucess1.Visible = true;
                        }
                    }
                    else
                    {
                        uc_sucess1.ErrorMessage = "Please enter valid HSN/SAC!";
                        uc_sucess1.VisibleError = true;
                    }
                }
                else
                {
                    uc_sucess1.ErrorMessage = "Please enter valid HSN/SAC!";
                    uc_sucess1.VisibleError = true;
                }
                txtItem.Text = string.Empty;
                //lvAdd.Items.Add()

                //lvAdd.DataSource = dtitem;
                //lvAdd.DataTextField = "ItemName";
                //lvAdd.DataValueField = "ItemId";
                //lvAdd.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lvVendor_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpvendorservice.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindVendor();
            dpvendorservice.DataBind();
        }

        protected void lvTransShipment_PagePropertiesChanging1(object sender, PagePropertiesChangingEventArgs e)
        {
            dpTranshipment.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindTransShipment();
            dpTranshipment.DataBind();
        }

        protected void lkbShip_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbTrans = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbTrans.CommandArgument.ToString()))
                {
                    int TranshipmentID = Convert.ToInt32(lkbTrans.CommandArgument);
                    ViewState["TransShipment_ID"] = TranshipmentID;
                    btntransShipment.Attributes.Add("TransShipment_ID", lkbTrans.CommandArgument);
                    var itm = unitOfwork.TransShipmentRepositry.Filter(f => f.TransShipment_ID == TranshipmentID).FirstOrDefault();
                    ddlVendorName.SelectedValue = Convert.ToString(itm.VendorID);
                    BindItem();
                    if (ddlItem.SelectedIndex > -1)
                        ddlItem.SelectedValue = Convert.ToString(itm.Item_ID);
                    txtRegNo.Text = itm.VehicleRegistrationNumber;
                    txtDriverNo.Text = itm.DriverLicenceNumber;
                    // ddlfacility.SelectedValue = Convert.ToString(itm.IsInterNationalWarehouseAvailed);
                    txtlocation.Text = itm.FromLocation;
                    TxtToLocation.Text = itm.ToLocation;
                    txtBillAmt.Text = Convert.ToString(itm.BillAmount);
                    txtTransShipmentNo.Text = itm.TransShipmentNo;
                    txtdistance.Text = Convert.ToString(itm.Distance_ID);
                    txtDate.Text = Convert.ToString(itm.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

       
    }
}