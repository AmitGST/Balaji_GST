using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System.Data;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_B2CL : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  BindItems(ReturnType);
            }
        }

        //using for first upload & edit offline sheet 
        public int AuditTrailId
        {
            get
            {
                string val = string.Empty;
                if (!string.IsNullOrEmpty(Session["AuditTrailId"].ToString()))
                    val = Session["AuditTrailId"].ToString();
                return Convert.ToInt16(val);
            }
            set
            {
                Session["AuditTrailId"] = value;
            }
        }



        byte ReturnType;
        public void BindItems(byte ReturnType)
        {
            try
            {
                this.ReturnType = ReturnType;
                Session["AuditTrailId"] = AuditTrailId;
                
                var userID = Common.LoggedInUserID();
                var deactive = (byte)EnumConstants.Status.Deactive;
                var sheetType = (byte)EnumConstants.OfflineExcelSection.B2CL;
                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.SectionType == (byte)EnumConstants.OfflineExcelSection.B2CL && x.UserID == userID).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                //  objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_B2CL.DataSource = objList;
                lv_B2CL.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CL_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (lv_B2CL.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("InvoiceNo"));
                    //dt.Rows.Add("InvoiceNo");
                }
                DropDownList ddlPos = (DropDownList)e.Item.FindControl("ddlPos");
                HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
                if (hdnPos.Value != null && hdnPos.Value != "")
                    ddlPos.Items.FindByValue(hdnPos.Value).Selected = true;
                //supply type
                DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
                HiddenField hdnSupplyType = (HiddenField)e.Item.FindControl("hdnSupplyType");
                if (hdnSupplyType.Value != null && hdnSupplyType.Value != "")
                    ddl_SupplyType.Items.FindByValue(hdnSupplyType.Value).Selected = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        int displayIndex;
        protected void lv_B2CL_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_B2CL.EditIndex = e.NewEditIndex;
                Session["displayIndex"] = e.NewEditIndex;
                BindItems(ReturnType);
              
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public EventHandler AddMoreClick;
        public string InvoiceNumber;
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ValueId"] = ((LinkButton)sender).CommandArgument;
                ListViewItem item = (ListViewItem)lv_B2CL.Items[Convert.ToInt32(Session["displayIndex"])];
                TextBox txtInvoice = (TextBox)item.FindControl("txtInvoiceNo");
                InvoiceNumber = txtInvoice.Text;
                AddMoreClick(sender, e);
                // this.Page.FindControl("ddlSection");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CL_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_B2CL.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    GST_TRN_OFFLINE_INVOICE invoice = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        TextBox txtInvoiceNo = (lv_B2CL.Items[e.ItemIndex].FindControl("txtInvoiceNo")) as TextBox;
                        if (txtInvoiceNo.Text != null || txtInvoiceNo.Text != "")
                            invoice.InvoiceNo = txtInvoiceNo.Text.Trim();

                        TextBox txt_InvoiceDate = (lv_B2CL.Items[e.ItemIndex].FindControl("txt_InvoiceDate")) as TextBox;
                        if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text.Trim(), "dd/MM/yyyy", null);
                            invoice.InvoiceDate = SOrderDate;
                        }

                        TextBox txtInvoiceValue = (lv_B2CL.Items[e.ItemIndex].FindControl("txtInvoiceValue")) as TextBox;
                        if (txtInvoiceValue.Text != null || txtInvoiceValue.Text != "")
                            invoice.TotalInvoiceValue = txtInvoiceValue.Text.Trim();

                        DropDownList ddlPos = (lv_B2CL.Items[e.ItemIndex].FindControl("ddlPos")) as DropDownList;
                        if (ddlPos.SelectedIndex > 0)
                            invoice.PlaceofSupply = Convert.ToByte(ddlPos.SelectedValue);

                        DropDownList ddl_SupplyType = (lv_B2CL.Items[e.ItemIndex].FindControl("ddl_SupplyType")) as DropDownList;
                        if (ddl_SupplyType.SelectedIndex > 0)
                            invoice.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedValue);

                        TextBox txtECommerce = (lv_B2CL.Items[e.ItemIndex].FindControl("txtECommerce")) as TextBox;
                        if (txtECommerce.Text != null || txtECommerce.Text != "")
                            invoice.ECommerce_GSTIN = txtECommerce.Text.Trim();
                    }
                    //viveksinha-start
                    invoice.UserID = Common.LoggedInUserID();
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.UpdatedDate = DateTime.Now;
                    //end
                    unitOfwork.OfflineinvoiceRepository.Update(invoice);
                    unitOfwork.Save();
                }
                lv_B2CL.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CL_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();

                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.B2CL;
                    TextBox txtInvoiceNo = (e.Item.FindControl("txtInvoiceNo")) as TextBox;
                    if (txtInvoiceNo.Text != null)
                        obj.InvoiceNo = txtInvoiceNo.Text.Trim();

                    TextBox txt_InvoiceDate = (e.Item.FindControl("txt_InvoiceDate")) as TextBox;
                    if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                    {
                        DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                        obj.InvoiceDate = SOrderDate;
                    }

                    TextBox txtInvoiceValue = (e.Item.FindControl("txtInvoiceValue")) as TextBox;
                    if (txtInvoiceValue.Text != "")
                        obj.TotalInvoiceValue = txtInvoiceValue.Text.Trim();

                    DropDownList ddl_Pos = (e.Item.FindControl("ddlPos")) as DropDownList;
                    if (ddl_Pos.SelectedIndex > -1)
                        obj.PlaceofSupply = Convert.ToByte(ddl_Pos.SelectedItem.Value);

                    DropDownList ddl_SupplyType = (e.Item.FindControl("ddl_SupplyType")) as DropDownList;
                    if (ddl_SupplyType.SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedItem.Value);

                    TextBox txtECommerce = (e.Item.FindControl("txtECommerce")) as TextBox;
                    if (txtECommerce.Text != null)
                        obj.ECommerce_GSTIN = txtECommerce.Text.Trim();

                }
                //viveksinha-start
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedBy = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;

                //end
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_B2CL.EditIndex = -1;
                //lv_B2CL.EditIndex++;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CL_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                DropDownList ddl_Pos = (DropDownList)e.Item.FindControl("ddlpos");
                if (ddl_Pos != null)
                {
                    var UserId = Common.LoggedInUserID();
                    var StateCode = unitOfwork.AspnetRepository.Find(x => x.Id == UserId).StateCode;
                    var data = unitOfwork.StateRepository.Filter(x => x.StateCode != StateCode).OrderBy(o => o.StateCode).Select(x => new { TextField = x.StateCode + "-" + x.StateName, ValueField = x.StateID.ToString() }).ToList();
                    data.Insert(0, new { TextField = "[ SELECT ]", ValueField = "0" });
                    ddl_Pos.DataSource = data;
                    ddl_Pos.DataTextField = "TextField";
                    ddl_Pos.DataValueField = "ValueField";
                    ddl_Pos.DataBind();
                }
                //supply type
                DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
                if (ddl_SupplyType != null)
                {
                    ListItem item = new ListItem("INTERSTATE", "0");
                    ddl_SupplyType.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                int ValueId;
                foreach (var item in lv_B2CL.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_B2CL.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
                        var OfflineObj = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == ValueId).SingleOrDefault();
                        if (OfflineObj != null)
                        {
                            List<GST_TRN_OFFLINE_INVOICE_DATAITEM> offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId).ToList();
                            foreach (var data in offlinedataobj)
                            {
                                unitOfwork.OfflineinvoicedataitemRepository.Delete(data);
                                unitOfwork.Save();
                            }
                           
                            unitOfwork.OfflineinvoiceRepository.Delete(OfflineObj);
                            unitOfwork.Save();
                            count++;
                            if (count > 0 )
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Are you sure! you want to delete data');", true);
                                uc_sucess.SuccessMessage = "Data Deleted Successfully.";
                                uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                            }
                        }
                    }
                }
                BindItems(ReturnType);
                if (count == 0)
                {
                    this.WarningMessage = "Please select atleast one row to delete data.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //uc_sucess.ErrorMessage = "Please select atleast one row to delete data.";
                    //uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CL_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpB2CL.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpB2CL.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public string WarningMessage
        {
            get
            {
                return lblWarning.Text;
            }
            set
            {
                lblWarning.Text = value;
            }
        }

    }
}