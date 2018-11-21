using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_hsn : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindItems();
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
                ///offlinesheetname only for hsn due to some changes
                var sheetType = (byte)EnumConstants.OfflineSheetName.HSN;

                List<GST_TRN_OFFLINE_INVOICE_DATAITEM> objList = new List<GST_TRN_OFFLINE_INVOICE_DATAITEM>();
                var data = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.GST_TRN_OFFLINE_INVOICE.ReturnType == ReturnType && x.GST_TRN_OFFLINE_INVOICE.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE_INVOICE.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.GST_TRN_OFFLINE_INVOICE.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.GST_TRN_OFFLINE_INVOICE.ReturnType == ReturnType && x.GST_TRN_OFFLINE_INVOICE.SectionType == (byte)EnumConstants.OfflineSheetName.HSN).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                lv_HSN.DataSource = objList;
                lv_HSN.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public bool CheckEntityNull(object data)
        {

            if (string.IsNullOrEmpty(data as string))
                return true;
            return false;

        }

        protected void lv_HSN_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                DropDownList ddl_UQC = (DropDownList)e.Item.FindControl("ddl_UQC");
                HiddenField hdnUQC = (HiddenField)e.Item.FindControl("hdnUQC");
                if (hdnUQC.Value != null && hdnUQC.Value != "")
                    ddl_UQC.Items.FindByValue(hdnUQC.Value).Selected = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_HSN_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_HSN.EditIndex = e.NewEditIndex;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_HSN_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_HSN.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    GST_TRN_OFFLINE_INVOICE_DATAITEM invoice = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.OfflineDataID == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        TextBox txt_HSN = (lv_HSN.Items[e.ItemIndex].FindControl("txt_HSN")) as TextBox;
                        if (txt_HSN.Text != "" || txt_HSN.Text != null)
                            invoice.GST_TRN_OFFLINE_INVOICE.HSN = txt_HSN.Text.Trim();

                        TextBox txt_Description = (lv_HSN.Items[e.ItemIndex].FindControl("txt_Description")) as TextBox;
                        if (txt_Description.Text != "" || txt_Description.Text != null)
                            invoice.GST_TRN_OFFLINE_INVOICE.HSNDescription = txt_Description.Text.Trim();

                        DropDownList ddl_UQC = (lv_HSN.Items[e.ItemIndex].FindControl("ddl_UQC")) as DropDownList;
                        if (ddl_UQC.SelectedIndex > 0)
                            invoice.GST_TRN_OFFLINE_INVOICE.UQC = Convert.ToByte(ddl_UQC.SelectedValue);

                        TextBox txt_Quantity = (lv_HSN.Items[e.ItemIndex].FindControl("txt_Quantity")) as TextBox;
                        if (txt_Quantity.Text != "" || txt_Quantity.Text != null)
                            invoice.TotalQuantity = Convert.ToDecimal(txt_Quantity.Text.Trim());

                        TextBox txt_Value = (lv_HSN.Items[e.ItemIndex].FindControl("txt_Value")) as TextBox;
                        if (txt_Value.Text != "" || txt_Value.Text != null)
                            invoice.TotalValue = Convert.ToDecimal(txt_Value.Text.Trim());

                        TextBox txt_Taxable_value = (lv_HSN.Items[e.ItemIndex].FindControl("txt_Taxable_value")) as TextBox;
                        if (txt_Taxable_value.Text != "" || txt_Taxable_value.Text != null)
                            invoice.TotalTaxableValue = Convert.ToDecimal(txt_Taxable_value.Text.Trim());

                        TextBox txt_IGST = (lv_HSN.Items[e.ItemIndex].FindControl("txt_IGST")) as TextBox;
                        if (txt_IGST.Text != "" || txt_IGST.Text != null)
                            invoice.IGSTAmt = Convert.ToDecimal(txt_IGST.Text.Trim());

                        TextBox txt_SGSTUTGST = (lv_HSN.Items[e.ItemIndex].FindControl("txt_SGSTUTGST")) as TextBox;
                        if (txt_SGSTUTGST.Text != "" || txt_SGSTUTGST.Text != null)
                            invoice.SGSTAmt = Convert.ToDecimal(txt_SGSTUTGST.Text.Trim());

                        TextBox txt_CGST = (lv_HSN.Items[e.ItemIndex].FindControl("txt_CGST")) as TextBox;
                        if (txt_CGST.Text != "" || txt_CGST.Text != null)
                            invoice.CGSTAmt = Convert.ToDecimal(txt_CGST.Text.Trim());

                        TextBox txt_Cess = (lv_HSN.Items[e.ItemIndex].FindControl("txt_Cess")) as TextBox;
                        if (txt_Cess.Text != "" || txt_Cess != null)
                            invoice.CessAmt = Convert.ToDecimal(txt_Cess.Text.Trim());

                    }
                    //viveksinha-start
                    invoice.UpdatedDate = DateTime.Now;
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.GST_TRN_OFFLINE_INVOICE.UserID = Common.LoggedInUserID();
                    //end 
                    unitOfwork.OfflineinvoicedataitemRepository.Update(invoice);
                    unitOfwork.Save();
                }
                lv_HSN.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_HSN_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE_DATAITEM obj = new GST_TRN_OFFLINE_INVOICE_DATAITEM();
                obj.GST_TRN_OFFLINE_INVOICE = new GST_TRN_OFFLINE_INVOICE();
                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.GST_TRN_OFFLINE_INVOICE.ReturnType = ReturnType;
                    obj.GST_TRN_OFFLINE_INVOICE.SectionType = (byte)EnumConstants.OfflineExcelSection.HSNWiseSummary;
                    TextBox txt_HSN = (e.Item.FindControl("txt_HSN")) as TextBox;
                    if (txt_HSN.Text != "")
                        obj.GST_TRN_OFFLINE_INVOICE.HSN = txt_HSN.Text.Trim();
                    TextBox txt_Description = (e.Item.FindControl("txt_Description")) as TextBox;
                    if (txt_Description.Text != "")
                        obj.GST_TRN_OFFLINE_INVOICE.HSNDescription = txt_Description.Text.Trim();
                    DropDownList ddl_UQC = (e.Item.FindControl("ddl_UQC")) as DropDownList;
                    if (ddl_UQC.SelectedIndex > 0)
                        obj.GST_TRN_OFFLINE_INVOICE.UQC = Convert.ToByte(ddl_UQC.SelectedValue);
                    TextBox txt_Quantity = (e.Item.FindControl("txt_Quantity")) as TextBox;
                    if (txt_Quantity.Text != "")
                        obj.TotalQuantity = Convert.ToDecimal(txt_Quantity.Text.Trim());
                    TextBox txt_Value = (e.Item.FindControl("txt_Value")) as TextBox;
                    if (txt_Value.Text != "")
                        obj.TotalValue = Convert.ToDecimal(txt_Value.Text.Trim());
                    TextBox txt_Taxable_value = (e.Item.FindControl("txt_Taxable_value")) as TextBox;
                    if (txt_Taxable_value.Text != "")
                        obj.TotalTaxableValue = Convert.ToDecimal(txt_Taxable_value.Text.Trim());
                    TextBox txt_IGST = (e.Item.FindControl("txt_IGST")) as TextBox;
                    if (txt_IGST.Text != "")
                        obj.IGSTAmt = Convert.ToDecimal(txt_IGST.Text.Trim());
                    TextBox txt_SGSTUTGST = (e.Item.FindControl("txt_SGSTUTGST")) as TextBox;
                    if (txt_SGSTUTGST.Text != "")
                        obj.SGSTAmt = Convert.ToDecimal(txt_SGSTUTGST.Text.Trim());
                    TextBox txt_CGST = (e.Item.FindControl("txt_CGST")) as TextBox;
                    if (txt_CGST.Text != "")
                        obj.CGSTAmt = Convert.ToDecimal(txt_CGST.Text.Trim());
                    TextBox txt_Cess = (e.Item.FindControl("txt_Cess")) as TextBox;
                    if (txt_Cess.Text != "")
                        obj.CessAmt = Convert.ToDecimal(txt_Cess.Text.Trim());


                }
                //viveksinha-start
                obj.GST_TRN_OFFLINE_INVOICE.UserID = Common.LoggedInUserID();
                obj.CreatedBy = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                unitOfwork.OfflineinvoicedataitemRepository.Create(obj);
                unitOfwork.Save();
                lv_HSN.EditIndex = -1;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_HSN_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                DropDownList ddl_UQC = (DropDownList)e.Item.FindControl("ddl_UQC");
                if (ddl_UQC != null)
                {
                    foreach (EnumConstants.Unit r in Enum.GetValues(typeof(EnumConstants.Unit)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.Unit), r), Convert.ToByte(r).ToString());
                        ddl_UQC.Items.Add(item);
                    }

                    ddl_UQC.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_HSN_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpHSN.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpHSN.DataBind();
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
                foreach (var item in lv_HSN.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_HSN.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
                        var OfflineObj = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == ValueId).SingleOrDefault();
                        if (OfflineObj != null)
                        {
                            List<GST_TRN_OFFLINE_INVOICE_DATAITEM> offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId).ToList();
                            foreach (var data in offlinedataobj)
                            {
                                unitOfwork.OfflineinvoicedataitemRepository.Delete(data);
                                unitOfwork.Save();
                            }
                            count++;
                            if (count > 0)
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
                    // uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
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