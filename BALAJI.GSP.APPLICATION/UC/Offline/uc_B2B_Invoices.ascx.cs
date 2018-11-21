using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using BALAJI.GSP.APPLICATION.UC.Offline.Controls;
namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_B2B_Invoices : System.Web.UI.UserControl
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
              //  Session["AuditTrailId"] = AuditTrailId;

                //if (!string.IsNullOrEmpty(Session["AuditTrailId"] as string))
                //{
                //int AuditTrailId = Convert.ToInt32(Session["AuditTrailId"]);
                var deactive = (byte)EnumConstants.Status.Deactive;
                var sheetType = (byte)EnumConstants.OfflineExcelSection.B2B;
                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.SectionType == sheetType).ToList();
                //x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId &&
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                //objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_B2b_Invoice.DataSource = objList;
                lv_B2b_Invoice.DataBind();
                //}
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        int DisplayIndex = -1;

        protected void lv_B2b_Invoice_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (lv_B2b_Invoice.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("InvoiceNo"));
                    //dt.Rows.Add("InvoiceNo");
                }
                //DropDownList ddlPos = (DropDownList)e.Item.FindControl("ddlPos");
                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                uc_SupplyType.BindItems();
                if (Convert.ToInt32(DisplayIndex) != e.Item.DisplayIndex)
                    uc_SupplyType.ddlPos_enable = false;
                else
                    uc_SupplyType.ddlPos_enable = true;


                HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
                if (hdnPos.Value != null && hdnPos.Value != "")
                    uc_SupplyType.ddlPos_SelectedValue = hdnPos.Value;
                //ddlPos.Items.FindByValue(hdnPos.Value).Selected = true;

                //supply type
                //DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
                HiddenField hdnSupplyType = (HiddenField)e.Item.FindControl("hdnSupplyType");
                if (hdnSupplyType.Value != null && hdnSupplyType.Value != "")
                    uc_SupplyType.ddlSupplyType_SelectedValue = hdnSupplyType.Value;
                // ddl_SupplyType.Items.FindByValue(hdnSupplyType.Value).Selected = true;

                //Invoice Type
                DropDownList ddl_InvoiceType = (DropDownList)e.Item.FindControl("ddl_InvoiceType");
                HiddenField hdnInvoiceType = (HiddenField)e.Item.FindControl("hdnInvoiceType");
                if (hdnInvoiceType.Value != null && hdnInvoiceType.Value != "")
                    ddl_InvoiceType.Items.FindByValue(hdnInvoiceType.Value).Selected = true;
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

                ListViewItem item = (ListViewItem)lv_B2b_Invoice.Items[Convert.ToInt32(Session["displayIndex"])];
                TextBox txtInvoice = (TextBox)item.FindControl("txtInvoiceNo");
                InvoiceNumber = txtInvoice.Text;
                AddMoreClick(sender, e);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        protected void lv_B2b_Invoice_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_B2b_Invoice.EditIndex = e.NewEditIndex;
                Session["displayIndex"] = e.NewEditIndex;
                DisplayIndex = e.NewEditIndex;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2b_Invoice_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    GST_TRN_OFFLINE_INVOICE invoice = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        uc_SupplyType uc_SupplyType = (uc_SupplyType)lv_B2b_Invoice.Items[e.ItemIndex].FindControl("uc_SupplyType");

                        TextBox txtGSTIN = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("txtGSTIN")) as TextBox;
                        if (txtGSTIN.Text != null || txtGSTIN.Text != "")
                            invoice.ReceiverGSTIN = txtGSTIN.Text.Trim();

                        TextBox txtInvoiceNo = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("txtInvoiceNo")) as TextBox;
                        if (txtInvoiceNo.Text != null || txtInvoiceNo.Text != "")
                            invoice.InvoiceNo = txtInvoiceNo.Text.Trim();

                        TextBox txt_InvoiceDate = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("txt_InvoiceDate")) as TextBox;
                        if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text.Trim(), "dd/MM/yyyy", null);
                            invoice.InvoiceDate = SOrderDate;
                        }

                        TextBox txtInvoiceValue = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("txtInvoiceValue")) as TextBox;
                        if (txtInvoiceValue.Text != null || txtInvoiceValue.Text != "")
                            invoice.TotalInvoiceValue = txtInvoiceValue.Text.Trim();

                        if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                            invoice.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                        if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                            invoice.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);

                        DropDownList ddl_InvoiceType = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("ddl_InvoiceType")) as DropDownList;
                        if (ddl_InvoiceType.SelectedIndex > 0)
                            invoice.InvoiceType = Convert.ToByte(ddl_InvoiceType.SelectedValue);

                        TextBox txtECommerce = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("txtECommerce")) as TextBox;
                        if (txtECommerce.Text != null || txtECommerce.Text != "")
                            invoice.ECommerce_GSTIN = txtECommerce.Text.Trim();

                        CheckBox chkReverse = (lv_B2b_Invoice.Items[e.ItemIndex].FindControl("chkReverse")) as CheckBox;
                        invoice.ReverseCharge = chkReverse.Checked;
                    }
                    //viveksinha-start
                    invoice.UserID = Common.LoggedInUserID();
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.UpdatedDate = DateTime.Now;
                    //end
                    unitOfwork.OfflineinvoiceRepository.Update(invoice);
                    unitOfwork.Save();
                }
                lv_B2b_Invoice.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }



        protected void lv_B2b_Invoice_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();

                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.B2B;
                    uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");

                    TextBox txtGSTIN = (e.Item.FindControl("txtGSTIN")) as TextBox;
                    if (txtGSTIN.Text != null)
                        obj.ReceiverGSTIN = txtGSTIN.Text.Trim();

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
                    if (txtInvoiceValue.Text != null)
                        obj.TotalInvoiceValue = txtInvoiceValue.Text.Trim();

                    if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                        obj.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                    if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);

                    DropDownList ddl_InvoiceType = (e.Item.FindControl("ddl_InvoiceType")) as DropDownList;
                    if (ddl_InvoiceType.SelectedIndex > 0)
                        obj.InvoiceType = Convert.ToByte(ddl_InvoiceType.SelectedValue);

                    TextBox txtECommerce = (e.Item.FindControl("txtECommerce")) as TextBox;
                    if (txtECommerce.Text != null)
                        obj.ECommerce_GSTIN = txtECommerce.Text.Trim();

                    CheckBox chkReverse = (e.Item.FindControl("chkReverse")) as CheckBox;
                    obj.ReverseCharge = chkReverse.Checked;
                }
                //viveksinha-start
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedBy = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                //end
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_B2b_Invoice.EditIndex = -1;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2b_Invoice_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");

                uc_SupplyType.BindItems();

                DropDownList ddl_InvoiceType = (DropDownList)e.Item.FindControl("ddl_InvoiceType");
                if (ddl_InvoiceType != null)
                {
                    foreach (EnumConstants.InvoiceType r in Enum.GetValues(typeof(EnumConstants.InvoiceType)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.InvoiceType), r), Convert.ToByte(r).ToString());
                        ddl_InvoiceType.Items.Add(item);
                    }
                    ddl_InvoiceType.Items.Insert(0, new ListItem(" [ SELECT ]  ", "-1"));
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
                foreach (var item in lv_B2b_Invoice.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_B2b_Invoice.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
                        var OfflineObj = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == ValueId).SingleOrDefault();
                        if (OfflineObj != null)
                        {
                            //var offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId);
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

        protected void lv_B2b_Invoice_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpB2bInvoices.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpB2bInvoices.DataBind();
                lv_B2b_Invoice.EditIndex = -1;
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