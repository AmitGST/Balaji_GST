using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    [Authorize("Admin")]
    public partial class Hsn : System.Web.UI.Page
    {
        //protected override void OnPreInit(EventArgs e)
        //{
        //    if (!Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/User/User.master";
        //    }
        //    if (Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/Admin/Admin.master";
        //    }
        //    base.OnPreInit(e);
        //}
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ClearItem();
                BindHSN();
                BindIsNotified();
                BindSubGroup();
                BindNotifiedHSN();
                BindConditionhsn();
                BindSpclCond();
                BindBusinessType();

            }
        }
        private void BindSubGroup()
        {
            ddlSubGroup.DataSource = unitOfwork.SubGroupRepository.Filter(f => f.Status == true).OrderBy(o => o.SubGroupCode).Select(s => new { SubGroupCode = s.SubGroupCode, SubGroupID = s.SubGroupID }).ToList();
            ddlSubGroup.DataValueField = "SubGroupID";
            ddlSubGroup.DataTextField = "SubGroupCode";
            ddlSubGroup.DataBind();
            ddlSubGroup.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindSpclCond()
        {
            ddlSpclCondition.DataSource = typeof(EnumConstants.SpecialCondition).ToList();
            ddlSpclCondition.DataTextField = "Value";
            ddlSpclCondition.DataValueField = "Key";
            ddlSpclCondition.DataBind();
           // ddlSpclCondition.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
        }

        private void BindBusinessType()
        {
            ddlBusinessType.DataSource = unitOfwork.BuisnessTypeRepository.All().Select(f => new { Key = f.BusinessID, Value = f.BusinessType }).ToList();
            ddlBusinessType.DataTextField = "Value";
            ddlBusinessType.DataValueField = "Key";
            ddlBusinessType.DataBind();
            ddlBusinessType.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
        }

        private void BindNotifiedHSN()
        {
            var itemType = (byte)EnumConstants.ItemType.HSN;
            ddlNotifiedHSN.DataSource = unitOfwork.ItemRepository.Filter(f => f.Status == true && f.IsNotified == true && f.ItemType == itemType).OrderBy(o => o.Item_ID).Select(s => new { ItemCode = s.ItemCode, Item_ID = s.Item_ID }).ToList();
            ddlNotifiedHSN.DataValueField = "Item_ID";
            ddlNotifiedHSN.DataTextField = "ItemCode";
            ddlNotifiedHSN.DataBind();
            ddlNotifiedHSN.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindHSN()
        {
            byte itemType = (byte)EnumConstants.ItemType.HSN;
            lvItems.DataSource = unitOfwork.ItemRepository.Filter(f => f.ItemType == 0).ToList();
            lvItems.DataBind();
        }

        private void BindConditionhsn()
        {
            var itemType = (byte)EnumConstants.ItemType.HSN;
            ddlConditionHsn.DataSource = unitOfwork.NotifiedItemRepositry.Filter(f => f.Status == true && f.IsCondition == true).OrderBy(o => o.NotificationNo).ToList();
            ddlConditionHsn.DataValueField = "Notified_Id";
            ddlConditionHsn.DataTextField = "NotificationNo";
            ddlConditionHsn.DataBind();
            ddlConditionHsn.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        public string Item_ID
        {
            get
            {
                return ViewState["Item_ID"].ToString();
            }
            set
            {
                ViewState["Item_ID"] = value;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GST_MST_ITEM hsn = new GST_MST_ITEM();
            try
            {
                hsn.ItemCode = txtHsnNo.Text.Trim();
                hsn.SpecialConditionApplied = (byte)(EnumConstants.SpecialCondition)Enum.Parse(typeof(EnumConstants.SpecialCondition), ddlSpclCondition.SelectedItem.Value);//Enum.IsDefined(typeof(EnumConstants.SpecialCondition), strDOWFake);ddlSpclCondition.SelectedItem.Text);
                hsn.IsNotified = ddlNotified.SelectedIndex > 0 ? Convert.ToBoolean(ddlNotified.SelectedItem.Text) : false;
                hsn.IsNilRated = ddlNilRated.SelectedIndex > 0 ? Convert.ToBoolean(ddlNilRated.SelectedItem.Text) : false;
                hsn.IsExempted = ddlExempted.SelectedIndex > 0 ? Convert.ToBoolean(ddlExempted.SelectedItem.Text) : false;
                hsn.IsZeroRated = ddlZeroRated.SelectedIndex > 0 ? Convert.ToBoolean(ddlZeroRated.SelectedItem.Text) : false;
                hsn.IsNonGSTGoods = ddlNonGSTGoods.SelectedIndex > 0 ? Convert.ToBoolean(ddlNonGSTGoods.SelectedItem.Text) : false;
                hsn.Description = txtDescription.Text.Trim();
                hsn.BuisnessTypeId = Convert.ToInt32(ddlBusinessType.SelectedValue);
                string itemid = Convert.ToString(ViewState["Item_ID"]);
                if (itemid == "" || itemid == null)
                {
                    string itemcode = Convert.ToString(hsn.ItemCode);
                    bool getitemcode = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemcode);
                    if (getitemcode)
                    {
                        uc_sucess.ErrorMessage = "HSN No. already exists.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    if (ddlUnit.SelectedIndex < 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select unit name.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    hsn.Unit = ddlUnit.SelectedItem.Text;
                    hsn.IGST = Convert.ToDecimal(txtIgst.Text.Trim());
                    hsn.CGST = Convert.ToDecimal(txtCgst.Text.Trim());
                    hsn.SGST = Convert.ToDecimal(txtSgst.Text.Trim());
                    hsn.UGST = Convert.ToDecimal(txtUgst.Text.Trim());
                    hsn.CESS = Convert.ToDecimal(txtCess.Text.Trim());

                    if (ddlSubGroup.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select Sub-Group.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    hsn.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue.ToString());
                    //if (getitemcode)
                    //{
                    //    uc_sucess.ErrorMessage = "Sub-Group already exist.";
                    //    uc_sucess.VisibleError = true;
                    //    return;
                    //}
                    hsn.Status = true;
                    hsn.ItemType = 1;
                    hsn.ActiveFrom = DateTime.Now;
                    hsn.CreatedBy = Common.LoggedInUserID();
                    hsn.CreatedDate = DateTime.Now;
                    hsn.ItemType = 0;
                    hsn.UserId = Common.LoggedInUserID();

                    //string itemHsn = Convert.ToString(hsn.ItemCode);
                    //bool getitemHsn = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemHsn);

                    //if (getitemHsn)
                    //{
                    //    uc_sucess.ErrorMessage = "Sub-Group already exist.";
                    //    uc_sucess.VisibleError = true;
                    //    return;
                    //}
                    unitOfwork.ItemRepository.Create(hsn);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "HSN created successfully.";
                    uc_sucess.Visible = true;
                    ddlSubGroup.DataBind();
                    ClearItem();
                    BindSubGroup();
                    BindNotifiedHSN();
                    BindHSN();
                }
                else
                {
                    string itemcode = Convert.ToString(hsn.ItemCode);
                    bool getitemcode = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemcode);
                    if (ddlSubGroup.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select Sub-Group.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    int itemsid = Convert.ToInt32(itemid);
                    var getitemId = unitOfwork.ItemRepository.Filter(f => f.Item_ID == itemsid).FirstOrDefault();
                    getitemId.SpecialConditionApplied = Convert.ToByte(ddlSpclCondition.SelectedIndex);
                    getitemId.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue.ToString());
                    getitemId.ItemCode = txtHsnNo.Text.Trim();
                    getitemId.Unit = Convert.ToString(ddlUnit.SelectedValue.ToString());
                    getitemId.Unit = ddlUnit.SelectedItem.Text;
                    getitemId.IGST = Convert.ToDecimal(txtIgst.Text.Trim());
                    getitemId.CGST = Convert.ToDecimal(txtCgst.Text.Trim());
                    getitemId.SGST = Convert.ToDecimal(txtSgst.Text.Trim());
                    getitemId.UGST = Convert.ToDecimal(txtUgst.Text.Trim());
                    getitemId.CESS = Convert.ToDecimal(txtCess.Text.Trim());
                    getitemId.IsNotified = Convert.ToBoolean(ddlNotified.SelectedItem.Text);
                    getitemId.IsNilRated = Convert.ToBoolean(ddlNilRated.SelectedItem.Text);
                    getitemId.IsExempted = Convert.ToBoolean(ddlExempted.SelectedItem.Text);
                    getitemId.IsZeroRated = Convert.ToBoolean(ddlZeroRated.SelectedItem.Text);
                    getitemId.IsNonGSTGoods = Convert.ToBoolean(ddlNonGSTGoods.SelectedItem.Text);
                    getitemId.Description = txtDescription.Text.Trim();
                    getitemId.UpdatedBy = Common.LoggedInUserID();
                    unitOfwork.ItemRepository.Update(getitemId);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Items successfully updated.";
                    uc_sucess.Visible = true;
                    itemid = string.Empty;
                    ClearItem();
                    BindSubGroup();
                    BindNotifiedHSN();
                    BindHSN();
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
        private void ClearItem()
        {
            ddlSubGroup.DataTextField = string.Empty;
            txtHsnNo.Text = string.Empty;
            ddlUnit.SelectedIndex = -1;
            txtIgst.Text = string.Empty;
            txtCgst.Text = string.Empty;
            txtSgst.Text = string.Empty;
            txtUgst.Text = string.Empty;
            txtCess.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlBusinessType.SelectedIndex = -1;
            ddlSpclCondition.SelectedIndex = 0;
            ddlNotified.SelectedValue = "0";
            ddlNilRated.SelectedValue = "0";
            ddlExempted.SelectedValue = "0";
            ddlZeroRated.SelectedValue = "0";
            ddlNonGSTGoods.SelectedValue = "0";
        }

        private void ClearNotified()
        {
            //   ddlNotifiedHSN.DataTextField = string.Empty;
            ddlNotifiedHSN.SelectedValue = "0";
            ddlIsCondition.SelectedValue = "0";
            txtNotifNo.Text = string.Empty;
            txtNotifSerialNo.Text = string.Empty;
            txtTariff.Text = string.Empty;
            txtDesc.Text = string.Empty;
        }

        private void ClearCondition()
        {
            //ddlConditionHsn.DataTextField = string.Empty;
            ddlConditionHsn.SelectedValue = "0";
            txtCondition.Text = string.Empty;
            txtConditionSerial.Text = string.Empty;
            txtDescr.Text = string.Empty;
        }
        private void BindIsNotified()
        {
            ddlNotified.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlNotified.DataTextField = "Value";
            ddlNotified.DataValueField = "Key";
            ddlNotified.DataBind();
          //  ddlNotified.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlNilRated.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlNilRated.DataTextField = "Value";
            ddlNilRated.DataValueField = "Key";
            ddlNilRated.DataBind();
            //ddlNilRated.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlExempted.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlExempted.DataTextField = "Value";
            ddlExempted.DataValueField = "Key";
            ddlExempted.DataBind();
            //ddlExempted.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlZeroRated.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlZeroRated.DataTextField = "Value";
            ddlZeroRated.DataValueField = "Key";
            ddlZeroRated.DataBind();
           // ddlZeroRated.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlNonGSTGoods.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlNonGSTGoods.DataTextField = "Value";
            ddlNonGSTGoods.DataValueField = "Key";
            ddlNonGSTGoods.DataBind();
          //  ddlNonGSTGoods.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlIsCondition.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlIsCondition.DataTextField = "Value";
            ddlIsCondition.DataValueField = "Key";
            ddlIsCondition.DataBind();
          //  ddlIsCondition.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));

            ddlUnit.DataSource = Enumeration.GetAll<EnumConstants.Unit>();
            ddlUnit.DataTextField = "Value";
            ddlUnit.DataValueField = "Key";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
        }
        protected void lvItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindHSN();
            DataPager1.DataBind();
        }

        protected void btnNotified_Click(object sender, EventArgs e)
        {
            try
            {
                GST_MST_ITEM_NOTIFIED notifiedHsn = new GST_MST_ITEM_NOTIFIED();
                if (ddlNotifiedHSN.SelectedIndex < 0)
                {
                    uc_sucess_notified.ErrorMessage = "!Kindly select Notified.";
                    uc_sucess_notified.VisibleError = true;
                    return;
                }

                notifiedHsn.NotificationNo = txtNotifNo.Text.Trim();
                notifiedHsn.Item_ID = Convert.ToInt32(ddlNotifiedHSN.SelectedItem.Value);
                notifiedHsn.NotificationSNo = txtNotifSerialNo.Text.Trim();
                notifiedHsn.TarrifDuty = Convert.ToDecimal(txtTariff.Text.Trim());
                notifiedHsn.Description = txtDesc.Text.Trim();
                notifiedHsn.IsCondition = Convert.ToBoolean(ddlIsCondition.SelectedItem.Text);
                notifiedHsn.Status = true;
                notifiedHsn.ActiveFrom = DateTime.Now;
                notifiedHsn.CreatedBy = Common.LoggedInUserID();
                notifiedHsn.CreatedDate = DateTime.Now;

                unitOfwork.NotifiedItemRepositry.Create(notifiedHsn);
                unitOfwork.Save();
                ClearNotified();
                BindConditionhsn();
                BindHSN();
                uc_sucess_notified.SuccessMessage = "Notified Item created successfully.";
                uc_sucess_notified.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess_notified.ErrorMessage = ex.Message;
                uc_sucess_notified.VisibleError = true;
            }
        }

        protected void btnCondition_Click(object sender, EventArgs e)
        {
            try
            {
                GST_MST_ITEM_CONDITION condhsn = new GST_MST_ITEM_CONDITION();
                if (ddlConditionHsn.SelectedIndex < 0)
                {
                    uc_sucess_notified.ErrorMessage = "!Kindly select condition.";
                    uc_sucess_notified.VisibleError = true;
                    return;
                }
                condhsn.Notified_Id = Convert.ToInt32(ddlConditionHsn.SelectedItem.Value);
                condhsn.ConditionNo = txtCondition.Text.Trim();
                condhsn.ConditionSNo = txtConditionSerial.Text.Trim();
                condhsn.Description = txtDescr.Text.Trim();
                condhsn.Status = true;
                condhsn.ActiveFrom = DateTime.Now;
                condhsn.CreatedBy = Common.LoggedInUserID();
                condhsn.CreatedDate = DateTime.Now;

                unitOfwork.ConditionItemRepositry.Create(condhsn);
                unitOfwork.Save();
                uc_sucess_condition.SuccessMessage = "Saved successfully.";
                uc_sucess_condition.Visible = true;
                ClearCondition();
                BindNotifiedHSN();
                BindHSN();

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess_condition.ErrorMessage = ex.Message;
                uc_sucess_condition.VisibleError = true;
            }

        }

        protected void lkbHsn_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int itemID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["Item_ID"] = itemID;
                    btnSave.Attributes.Add("Item_ID", lkbItem.CommandArgument);
                    var itm = unitOfwork.ItemRepository.Filter(f => f.Item_ID == itemID).FirstOrDefault();
                    txtHsnNo.Text = itm.ItemCode;
                    txtDescription.Text = itm.Description;
                    ddlSubGroup.SelectedValue = Convert.ToString(itm.SubGroupID);
                    ddlUnit.SelectedItem.Text = Convert.ToString(itm.Unit);
                    txtIgst.Text = Convert.ToString(itm.IGST);
                    txtCgst.Text = Convert.ToString(itm.CGST);
                    txtSgst.Text = Convert.ToString(itm.SGST);
                    txtUgst.Text = Convert.ToString(itm.UGST);
                    txtCess.Text = Convert.ToString(itm.CESS);
                    //ddlNotified.SelectedItem.Text = Convert.ToString(itm.IsNotified);
                    // ddlNonGSTGoods.SelectedItem.Text = Convert.ToString(itm.IsNonGSTGoods);
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