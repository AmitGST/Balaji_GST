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
    public partial class Sac : System.Web.UI.Page
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
                BindSac();
                BindIsNotified();
                BindSubClass();
                BindNotifiedItem();
                BindConditioned();
                // ddlSubClass.DataBind();
            }
        }

        private void BindSubClass()
        {
            ddlSubClass.DataSource = unitOfwork.SubClassRepository.Filter(f => f.Status == true).OrderBy(o => o.SubClassCode).Select(s => new { SubClassCode = s.SubClassCode, SubClassID = s.SubClassID }).ToList();
            ddlSubClass.DataValueField = "SubClassID";
            ddlSubClass.DataTextField = "SubClassCode";
            ddlSubClass.DataBind();
            ddlSubClass.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        private void BindNotifiedItem()
        {
            var itemType = (byte)EnumConstants.ItemType.SAC;
            ddlNotifiedItem.DataSource = unitOfwork.ItemRepository.Filter(f => f.Status == true && f.IsNotified == true && f.ItemType == itemType).OrderBy(o => o.Item_ID).Select(s => new { ItemCode = s.ItemCode, Item_ID = s.Item_ID }).ToList();
            ddlNotifiedItem.DataValueField = "Item_ID";
            ddlNotifiedItem.DataTextField = "ItemCode";
            ddlNotifiedItem.DataBind();
            ddlNotifiedItem.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindConditioned()
        {
            var itemType = (byte)EnumConstants.ItemType.SAC;
            ddlConditionItems.DataSource = unitOfwork.NotifiedItemRepositry.Filter(f => f.Status == true && f.IsCondition == true).OrderBy(o => o.NotificationNo).ToList();
            ddlConditionItems.DataValueField = "Notified_Id";
            ddlConditionItems.DataTextField = "NotificationNo";
            ddlConditionItems.DataBind();
            ddlConditionItems.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

        private void BindSac()
        {
            byte itemType = (byte)EnumConstants.ItemType.SAC;
            lvSac.DataSource = unitOfwork.ItemRepository.Filter(f => f.ItemType == 1).ToList();
            lvSac.DataBind();
        }

        private void ClearItem()
        {
            ddlSubClass.DataTextField = string.Empty;
            txtSacCode.Text = string.Empty;
            ddlNotified.SelectedIndex = 0;
            ddlSpclCondition.SelectedIndex = 0;
            ddlNonGSTGoods.SelectedIndex = 0;
            ddlIsNilRated.SelectedIndex = 0;
            ddlExempted.SelectedIndex = 0;
            ddlZeroRated.SelectedIndex = 0;
            txtDescription.Text = string.Empty;
            txtUgst.Text = string.Empty;
            txtSgst.Text = string.Empty;
            txtIgst.Text = string.Empty;
            txtCess.Text = string.Empty;
            txtCgst.Text = string.Empty;
        }

        private void ClearNotified()
        {
            //ddlNotifiedItem.DataTextField = string.Empty;
            ddlNotifiedItem.SelectedValue = "0";
            txtNotifNo.Text = string.Empty;
            txtNotifSerialNo.Text = string.Empty;
            txtTariff.Text = string.Empty;
            ddlIsCondition.SelectedValue = "0";
            txtDescr.Text = string.Empty;
        }

        private void ClearCondition()
        {
            // ddlConditionItems.DataTextField = string.Empty;
            ddlConditionItems.SelectedValue = "0";
            txtCondition.Text = string.Empty;
            txtConditionSerial.Text = string.Empty;
            txtDesc.Text = string.Empty;
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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            GST_MST_ITEM sac = new GST_MST_ITEM();
            try
            {

                sac.ItemCode = txtSacCode.Text.Trim();
                sac.CESS = Convert.ToDecimal(txtCess.Text.Trim());
                sac.CGST = Convert.ToDecimal(txtCgst.Text.Trim());
                sac.IGST = Convert.ToDecimal(txtIgst.Text.Trim());
                sac.SGST = Convert.ToDecimal(txtSgst.Text.Trim());
                sac.UGST = Convert.ToDecimal(txtUgst.Text.Trim());
                sac.SpecialConditionApplied = (byte)(EnumConstants.SpecialCondition)Enum.Parse(typeof(EnumConstants.SpecialCondition), ddlSpclCondition.SelectedItem.Value);//Enum.IsDefined(typeof(EnumConstants.SpecialCondition), strDOWFake);ddlSpclCondition.SelectedItem.Text);
                sac.IsNotified = ddlNotified.SelectedIndex > 0 ? Convert.ToBoolean(ddlNotified.SelectedItem.Text) : false;
                sac.IsNilRated = ddlIsNilRated.SelectedIndex > 0 ? Convert.ToBoolean(ddlIsNilRated.SelectedItem.Text) : false;
                sac.IsExempted = ddlExempted.SelectedIndex > 0 ? Convert.ToBoolean(ddlExempted.SelectedItem.Text) : false;
                sac.IsZeroRated = ddlZeroRated.SelectedIndex > 0 ? Convert.ToBoolean(ddlZeroRated.SelectedItem.Text) : false;
                sac.IsNonGSTGoods = ddlNonGSTGoods.SelectedIndex > 0 ? Convert.ToBoolean(ddlNonGSTGoods.SelectedItem.Text) : false;
                sac.Description = txtDescription.Text.Trim();
                string itemid = Convert.ToString(ViewState["Item_ID"]);
                if (itemid == "" || itemid == null)
                {
                    string itemcode = Convert.ToString(sac.ItemCode);
                    bool getitemcode = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemcode);
                    if (getitemcode)
                    {
                        uc_sucess_sac.ErrorMessage = "SAC Code already exists.";
                        uc_sucess_sac.VisibleError = true;
                        return;
                    }
                    if (ddlSubClass.SelectedIndex <= 0)
                    {
                        uc_sucess_sac.ErrorMessage = "!Kindly select Sub-Class name.";
                        uc_sucess_sac.VisibleError = true;
                        return;
                    }
                    sac.SubClassID = Convert.ToInt32(ddlSubClass.SelectedValue.ToString());
                    sac.Status = true;
                    sac.ItemType = 1;
                    sac.ItemType = (byte)EnumConstants.ItemType.SAC;
                    sac.ActiveFrom = DateTime.Now;
                    sac.CreatedBy = Common.LoggedInUserID();
                    sac.CreatedDate = DateTime.Now;
                    sac.UserId = Common.LoggedInUserID();

                    //string itemSac = Convert.ToString(sac.ItemCode);
                    //bool getitemSac = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemSac);

                    //if (getitemSac)
                    //{
                    //    uc_sucess_sac.ErrorMessage = "Item already exist.";
                    //    uc_sucess_sac.VisibleError = true;
                    //    return;
                    //}
                    unitOfwork.ItemRepository.Create(sac);
                    unitOfwork.Save();
                    uc_sucess_sac.SuccessMessage = "SAC created successfully.";
                    uc_sucess_sac.Visible = true;
                    ddlSubClass.DataBind();
                    ClearItem();
                    BindSubClass();
                    BindSac();
                    BindNotifiedItem();
                }
                else
                {
                    string itemcode = Convert.ToString(sac.ItemCode);
                    bool getitemcode = unitOfwork.ItemRepository.Contains(c => c.ItemCode == itemcode);
                    if (ddlSubClass.SelectedIndex <= 0)
                    {
                        uc_sucess_sac.ErrorMessage = "!Kindly select Sub-Class name.";
                        uc_sucess_sac.VisibleError = true;
                        return;
                    }
                    int itemsid = Convert.ToInt32(itemid);

                    var getitemId = unitOfwork.ItemRepository.Filter(f => f.Item_ID == itemsid).FirstOrDefault();
                    getitemId.SpecialConditionApplied = Convert.ToByte(ddlSpclCondition.SelectedIndex);
                    getitemId.SubClassID = Convert.ToInt32(ddlSubClass.SelectedValue.ToString());
                    getitemId.ItemCode = txtSacCode.Text.Trim();
                    getitemId.CESS = Convert.ToDecimal(txtCess.Text.Trim());
                    getitemId.CGST = Convert.ToDecimal(txtCgst.Text.Trim());
                    getitemId.IGST = Convert.ToDecimal(txtIgst.Text.Trim());
                    getitemId.SGST = Convert.ToDecimal(txtSgst.Text.Trim());
                    getitemId.UGST = Convert.ToDecimal(txtUgst.Text.Trim());
                    getitemId.IsNotified = Convert.ToBoolean(ddlNotified.SelectedItem.Text);
                    getitemId.IsNilRated = Convert.ToBoolean(ddlIsNilRated.SelectedItem.Text);
                    getitemId.IsExempted = Convert.ToBoolean(ddlExempted.SelectedItem.Text);
                    getitemId.IsZeroRated = Convert.ToBoolean(ddlZeroRated.SelectedItem.Text);
                    getitemId.IsNonGSTGoods = Convert.ToBoolean(ddlNonGSTGoods.SelectedItem.Text);
                    getitemId.Description = txtDescription.Text.Trim();
                    getitemId.UpdatedBy = Common.LoggedInUserID();
                    unitOfwork.ItemRepository.Update(getitemId);
                    unitOfwork.Save();
                    uc_sucess_sac.SuccessMessage = "Items successfully updated.";
                    uc_sucess_sac.Visible = true;
                    itemid = string.Empty;
                    ClearItem();
                    BindSubClass();
                    BindSac();
                    BindNotifiedItem();
                }
            }

            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess_sac.ErrorMessage = ex.Message;
                uc_sucess_sac.VisibleError = true;
            }
            //catch (Exception ex)
            //{
            //    uc_sucess_sac.ErrorMessage = ex.Message;
            //    uc_sucess_sac.VisibleError = true;
            //}
        }

        protected void btnNotified_Click(object sender, EventArgs e)
        {
            try
            {
                GST_MST_ITEM_NOTIFIED notified = new GST_MST_ITEM_NOTIFIED();
                if (ddlNotifiedItem.SelectedIndex < 0)
                {
                    uc_sucess_notified.ErrorMessage = "!Kindly select Notified.";
                    uc_sucess_notified.VisibleError = true;
                    return;
                }
                notified.NotificationNo = txtNotifNo.Text.Trim();
                notified.Item_ID = Convert.ToInt32(ddlNotifiedItem.SelectedItem.Value);
                notified.NotificationSNo = txtNotifSerialNo.Text.Trim();
                notified.TarrifDuty = Convert.ToDecimal(txtTariff.Text.Trim());
                notified.Description = txtDescr.Text.Trim();
                notified.IsCondition = Convert.ToBoolean(ddlIsCondition.SelectedItem.Text);
                notified.Status = true;
                notified.ActiveFrom = DateTime.Now;
                notified.CreatedBy = Common.LoggedInUserID();
                notified.CreatedDate = DateTime.Now;

                unitOfwork.NotifiedItemRepositry.Create(notified);
                unitOfwork.Save();
                ClearNotified();
                BindConditioned();
                BindSac();

                uc_sucess_notified.SuccessMessage = "Notified Item created successfully.";
                uc_sucess_notified.Visible = true;
            }
            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
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
                GST_MST_ITEM_CONDITION cond = new GST_MST_ITEM_CONDITION();

                cond.Notified_Id = Convert.ToInt32(ddlConditionItems.SelectedItem.Value);
                cond.ConditionNo = txtCondition.Text.Trim();
                cond.ConditionSNo = txtConditionSerial.Text.Trim();
                cond.Description = txtDesc.Text.Trim();
                cond.Status = true;
                cond.ActiveFrom = DateTime.Now;
                cond.CreatedBy = Common.LoggedInUserID();
                cond.CreatedDate = DateTime.Now;

                unitOfwork.ConditionItemRepositry.Create(cond);
                unitOfwork.Save();
                ClearCondition();
                BindSac();
                BindNotifiedItem();

                uc_sucess_condition.SuccessMessage = "Saved successfully.";
                uc_sucess_condition.Visible = true;
            }

            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess_condition.ErrorMessage = ex.Message;
                uc_sucess_condition.VisibleError = true;
            }
            //catch (Exception ex)
            //{
            //    uc_sucess_condition.ErrorMessage = ex.Message;
            //    uc_sucess_condition.VisibleError = true;
            //}
        }
        private void BindIsNotified()
        {
            ddlNotified.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlNotified.DataTextField = "Value";
            ddlNotified.DataValueField = "Key";
            ddlNotified.DataBind();
            //ddlNotified.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlIsNilRated.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlIsNilRated.DataTextField = "Value";
            ddlIsNilRated.DataValueField = "Key";
            ddlIsNilRated.DataBind();
            //ddlIsNilRated.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlExempted.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlExempted.DataTextField = "Value";
            ddlExempted.DataValueField = "Key";
            ddlExempted.DataBind();
            //ddlExempted.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlZeroRated.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlZeroRated.DataTextField = "Value";
            ddlZeroRated.DataValueField = "Key";
            ddlZeroRated.DataBind();
            //ddlZeroRated.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlNonGSTGoods.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlNonGSTGoods.DataTextField = "Value";
            ddlNonGSTGoods.DataValueField = "Key";
            ddlNonGSTGoods.DataBind();
            //ddlNonGSTGoods.Items.Insert(0, new ListItem(" [ Select ] ", "0"));


            ddlSpclCondition.DataSource = typeof(EnumConstants.SpecialCondition).ToList();
            ddlSpclCondition.DataTextField = "Value";
            ddlSpclCondition.DataValueField = "Key";
            ddlSpclCondition.DataBind();
            //ddlSpclCondition.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            ddlIsCondition.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlIsCondition.DataTextField = "Value";
            ddlIsCondition.DataValueField = "Key";
            ddlIsCondition.DataBind();
           // ddlIsCondition.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        protected void lvSac_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindSac();
            DataPager1.DataBind();
        }

        protected void lkbSac_Click(object sender, EventArgs e)
        {
            try
            {
                GST_MST_ITEM sac = new GST_MST_ITEM();
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int itemID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["Item_ID"] = itemID;

                    btnsave.Attributes.Add("Item_ID", lkbItem.CommandArgument);
                    var item = unitOfwork.ItemRepository.Filter(f => f.Item_ID == itemID).FirstOrDefault();
                    ddlSubClass.SelectedValue = Convert.ToString(item.SubClassID);
                    txtSacCode.Text = item.ItemCode;
                    txtCgst.Text = Convert.ToString(item.CGST);
                    txtCess.Text = Convert.ToString(item.CESS);
                    txtIgst.Text = Convert.ToString(item.IGST);
                    txtSgst.Text = Convert.ToString(item.SGST);
                    txtUgst.Text = Convert.ToString(item.UGST);
                    txtDescription.Text = item.Description;
                    //ddlSpclCondition.SelectedItem.Text = Convert.ToString(item.SpecialConditionApplied);
                    //ddlNonGSTGoods.SelectedItem.Text = Convert.ToString(item.IsNonGSTGoods);

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