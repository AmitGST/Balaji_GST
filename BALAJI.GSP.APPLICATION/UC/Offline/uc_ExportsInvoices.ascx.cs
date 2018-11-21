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
    public partial class uc_ExportsInvoices : System.Web.UI.UserControl
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

                var deactive = (byte)EnumConstants.Status.Deactive;
                var sheetType = (byte)EnumConstants.OfflineExcelSection.ExportInvoice;
                
                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.SectionType == (byte)(EnumConstants.OfflineExcelSection.ExportInvoice)).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                //objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_Export.DataSource = objList;
                lv_Export.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Export_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (lv_Export.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("InvoiceNo"));
                    //dt.Rows.Add("InvoiceNo");
                }
                // GST Payment
                DropDownList ddl_GSTPayment = (DropDownList)e.Item.FindControl("ddl_GSTPayment");
                HiddenField hdnGSTPayment = (HiddenField)e.Item.FindControl("hdnGSTPayment");
                if (hdnGSTPayment.Value != null && hdnGSTPayment.Value != "")
                    ddl_GSTPayment.Items.FindByValue(hdnGSTPayment.Value).Selected = true;

                // Supply Type
                DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
                HiddenField Hdn_SupplyType = (HiddenField)e.Item.FindControl("Hdn_SupplyType");
                if (Hdn_SupplyType.Value != null && Hdn_SupplyType.Value != "")
                    ddl_SupplyType.Items.FindByValue(Hdn_SupplyType.Value).Selected = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        int displayIndex;
        public EventHandler AddMoreClick;
        public string InvoiceNumber;
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ValueId"] = ((LinkButton)sender).CommandArgument;
                ListViewItem item = (ListViewItem)lv_Export.Items[Convert.ToInt32(Session["displayIndex"])];
                //TextBox txtInvoice = (TextBox)item.FindControl("txtInvoiceNo");
                //InvoiceNumber = txtInvoice.Text;
                AddMoreClick(sender, e);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Export_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_Export.EditIndex = e.NewEditIndex;
                Session["displayIndex"] = e.NewEditIndex;
                BindItems(ReturnType);
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Export_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_Export.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    GST_TRN_OFFLINE_INVOICE invoice = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        DropDownList ddl_GSTPayment = (lv_Export.Items[e.ItemIndex].FindControl("ddl_GSTPayment")) as DropDownList;
                        if (ddl_GSTPayment.SelectedIndex > 0)
                            invoice.GST_Payment = Convert.ToByte(ddl_GSTPayment.SelectedValue);

                        TextBox txt_invoiceno = (lv_Export.Items[e.ItemIndex].FindControl("txt_invoiceno")) as TextBox;
                        if (txt_invoiceno.Text != null || txt_invoiceno.Text != "")
                            invoice.InvoiceNo = txt_invoiceno.Text.Trim();

                        TextBox txt_InvoiceDate = (lv_Export.Items[e.ItemIndex].FindControl("txt_InvoiceDate")) as TextBox;
                        if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                            invoice.InvoiceDate = SOrderDate;
                        }

                        TextBox txt_InvoiceValue = (lv_Export.Items[e.ItemIndex].FindControl("txt_InvoiceValue")) as TextBox;
                        if (txt_InvoiceValue.Text != null || txt_InvoiceValue.Text != "")
                            invoice.TotalInvoiceValue = txt_InvoiceValue.Text.Trim();

                        TextBox txt_PortCode = (lv_Export.Items[e.ItemIndex].FindControl("txt_PortCode")) as TextBox;
                        if (txt_PortCode.Text != null || txt_PortCode.Text != "")
                            invoice.PortCode = txt_PortCode.Text.Trim();

                        TextBox txt_ShippingBillNo = (lv_Export.Items[e.ItemIndex].FindControl("txt_ShippingBillNo")) as TextBox;
                        if (txt_ShippingBillNo.Text != null || txt_ShippingBillNo.Text != "")
                            invoice.ShippingBillNo = txt_ShippingBillNo.Text;

                        TextBox txt_ShippingBillDate = (lv_Export.Items[e.ItemIndex].FindControl("txt_ShippingBillDate")) as TextBox;
                        if (txt_ShippingBillDate.Text != null || txt_ShippingBillDate.Text != "-")
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_ShippingBillDate.Text, "dd/MM/yyyy", null);
                            invoice.ShippingBillDate = SOrderDate;
                        }

                        DropDownList ddl_SupplyType = (lv_Export.Items[e.ItemIndex].FindControl("ddl_SupplyType")) as DropDownList;
                        if (ddl_SupplyType.SelectedIndex > 0)
                            invoice.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedValue);
                    }
                    //viveksinha-start
                    invoice.UpdatedDate = DateTime.Now;
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.UserID = Common.LoggedInUserID();
                    //end 
                    unitOfwork.OfflineinvoiceRepository.Update(invoice);
                    unitOfwork.Save();
                    lv_Export.EditIndex = -1;
                    BindItems(ReturnType);
                    //lkbDelete.Visible = true;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Export_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();
                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.ExportInvoice;
                    DropDownList ddl_GSTPayment = (e.Item.FindControl("ddl_GSTPayment")) as DropDownList;
                    if (ddl_GSTPayment.SelectedIndex > 0)
                        obj.GST_Payment = Convert.ToByte(ddl_GSTPayment.SelectedValue);

                    TextBox txt_invoiceno = (e.Item.FindControl("txt_invoiceno")) as TextBox;
                    if (txt_invoiceno.Text != null)
                        obj.InvoiceNo = txt_invoiceno.Text.Trim();

                    TextBox txt_InvoiceDate = (e.Item.FindControl("txt_InvoiceDate")) as TextBox;
                    if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                    {
                        DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                        obj.InvoiceDate = SOrderDate;
                    }
                    //obj.InvoiceDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                    TextBox txt_InvoiceValue = (e.Item.FindControl("txt_InvoiceValue")) as TextBox;
                    if (txt_InvoiceValue.Text != null)
                        obj.TotalInvoiceValue = txt_InvoiceValue.Text.Trim();
                    TextBox txt_PortCode = (e.Item.FindControl("txt_PortCode")) as TextBox;
                    if (txt_PortCode.Text != null)
                        obj.PortCode = txt_PortCode.Text.Trim();
                    TextBox txt_ShippingBillNo = (e.Item.FindControl("txt_ShippingBillNo")) as TextBox;
                    if (txt_ShippingBillNo.Text != null)
                        obj.ShippingBillNo = txt_ShippingBillNo.Text.Trim();

                    TextBox txt_ShippingBillDate = (e.Item.FindControl("txt_ShippingBillDate")) as TextBox;
                    if (txt_ShippingBillDate.Text != null || txt_ShippingBillDate.Text != "-")
                    {
                        DateTime SOrderDate = DateTime.ParseExact(txt_ShippingBillDate.Text, "dd/MM/yyyy", null);
                        obj.ShippingBillDate = SOrderDate;
                    }
                    DropDownList ddl_SupplyType = (e.Item.FindControl("ddl_SupplyType")) as DropDownList;
                    if (ddl_SupplyType.SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedValue);
                }

                //viveksinha-start
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedBy = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_Export.EditIndex = -1;
                //lv_Export.EditIndex++;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Export_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                DropDownList ddl_GSTPayment = (DropDownList)e.Item.FindControl("ddl_GSTPayment");
                if (ddl_GSTPayment != null)
                {
                    foreach (EnumConstants.GSTPay r in Enum.GetValues(typeof(EnumConstants.GSTPay)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.GSTPay), r), Convert.ToByte(r).ToString());
                        ddl_GSTPayment.Items.Add(item);

                    }
                    ddl_GSTPayment.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
                }

                //supply type
                DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");

                if (ddl_SupplyType != null)
                {
                    if (ddl_SupplyType != null)
                    {
                        ListItem item = new ListItem("INTERSTATE", "0");
                        ddl_SupplyType.Items.Add(item);
                    }
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
            {
                try
                {
                    int count = 0;
                    int ValueId;
                    foreach (var item in lv_Export.Items)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chk");
                        if (chk.Checked)
                        {
                            ValueId = Convert.ToInt32(lv_Export.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
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
                       // uc_sucess.ErrorMessage = "Please select atleast one row to delete data.";
                       // uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    cls_ErrorLog ob = new cls_ErrorLog();
                    cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                }
            }
        }

        protected void lv_Export_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpExport.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpExport.DataBind();
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
