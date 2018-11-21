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
    public partial class uc_crdr_registered : System.Web.UI.UserControl
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
                var sheetType = (byte)EnumConstants.OfflineExcelSection.CreditDebitReg;
                
                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.SectionType == (byte)EnumConstants.OfflineExcelSection.CreditDebitReg).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                //objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_crdrRegister.DataSource = objList;
                lv_crdrRegister.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_crdrRegister_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (lv_crdrRegister.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("InvoiceNo"));
                    //dt.Rows.Add("InvoiceNo");
                }

                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                uc_SupplyType.BindItems();
                if (Convert.ToInt32(Session["DisplayIndex"]) != e.Item.DisplayIndex)
                    uc_SupplyType.ddlPos_enable = false;
                else
                {
                    uc_SupplyType.ddlPos_enable = true;
                    Session["DisplayIndex"] = -1;
                }
                //Place of Supply
                HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
                if (hdnPos.Value != null && hdnPos.Value != "")
                    uc_SupplyType.ddlPos_SelectedValue = hdnPos.Value;

                //Supply Type
                HiddenField hdnSupplyType = (HiddenField)e.Item.FindControl("hdnSupplyType");
                if (hdnSupplyType.Value != null && hdnSupplyType.Value != "")
                    uc_SupplyType.ddlSupplyType_SelectedValue = hdnSupplyType.Value;

                // Document type
                DropDownList ddl_DocumentType = (DropDownList)e.Item.FindControl("ddl_DocumentType");
                HiddenField hdnDoc = (HiddenField)e.Item.FindControl("hdnDoc");
                if (hdnDoc.Value != null && hdnDoc.Value != "")
                    ddl_DocumentType.Items.FindByValue(hdnDoc.Value).Selected = true;

                // Reason for issuing note
                DropDownList ddl_IssuingNote = (DropDownList)e.Item.FindControl("ddl_IssuingNote");
                HiddenField hdnIssuingNote = (HiddenField)e.Item.FindControl("hdnIssuingNote");
                if (hdnIssuingNote.Value != null && hdnIssuingNote.Value != "")
                    ddl_IssuingNote.Items.FindByValue(hdnIssuingNote.Value).Selected = true;
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
               //ListViewItem item = (ListViewItem)lv_crdrRegister.Items[Convert.ToInt32(Session["displayIndex"])];
                //TextBox txt_invoiceno = (TextBox)item.FindControl("txt_invoiceno");
                //InvoiceNumber = txt_invoiceno.Text;
                AddMoreClick(sender, e);
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_crdrRegister_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
               
                //ListViewItem item = lv_crdrRegister.Items[e.NewEditIndex];
                //uc_SupplyType uc_SupplyType = (uc_SupplyType)item.FindControl("uc_SupplyType");
                lv_crdrRegister.EditIndex = e.NewEditIndex;
                //uc_SupplyType.BindItems();
                Session["displayIndex"] = e.NewEditIndex;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_crdrRegister_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_crdrRegister.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    GST_TRN_OFFLINE_INVOICE invoice = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        uc_SupplyType uc_SupplyType = (uc_SupplyType)lv_crdrRegister.Items[e.ItemIndex].FindControl("uc_SupplyType");

                        TextBox txtRcvrGSTIN = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txtRcvrGSTIN")) as TextBox;
                        if (txtRcvrGSTIN.Text != null || txtRcvrGSTIN.Text != "")
                            invoice.ReceiverGSTIN = txtRcvrGSTIN.Text.Trim();

                        TextBox txtVoucherNo = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txtVoucherNo")) as TextBox;
                        if (txtVoucherNo.Text != null)
                            invoice.Voucher_No = txtVoucherNo.Text.Trim();

                        TextBox txt_VoucherDate = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txt_VoucherDate")) as TextBox;
                        if (txt_VoucherDate.Text != null || txt_VoucherDate.Text != "-")
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_VoucherDate.Text.Trim(), "dd/MM/yyyy", null);
                            invoice.Voucher_date = SOrderDate;
                        }

                        TextBox txt_invoiceno = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txt_invoiceno")) as TextBox;
                        if (txt_invoiceno.Text != null)
                            invoice.InvoiceNo = txt_invoiceno.Text.Trim();

                        TextBox txt_InvoiceDate = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txt_InvoiceDate")) as TextBox;
                        if (txt_InvoiceDate.Text != "-" || txt_InvoiceDate.Text != null)
                        {
                            DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                            invoice.InvoiceDate = SOrderDate;
                        }

                        DropDownList ddl_DocumentType = (lv_crdrRegister.Items[e.ItemIndex].FindControl("ddl_DocumentType")) as DropDownList;
                        if (ddl_DocumentType.SelectedIndex > 0)
                            invoice.Document_Type = ddl_DocumentType.SelectedValue;

                        DropDownList ddl_IssuingNote = (lv_crdrRegister.Items[e.ItemIndex].FindControl("ddl_IssuingNote")) as DropDownList;
                        if (ddl_IssuingNote.SelectedIndex > 0)
                            invoice.Issuing_Note = Convert.ToInt32(ddl_IssuingNote.SelectedItem.Value);

                        TextBox txt_VoucherValue = (lv_crdrRegister.Items[e.ItemIndex].FindControl("txt_VoucherValue")) as TextBox;
                        if (txt_VoucherValue.Text != null)
                            invoice.Voucher_Value = txt_VoucherValue.Text.Trim();

                        if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                            invoice.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                        if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                            invoice.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);

                        CheckBox chk_PreGST = (lv_crdrRegister.Items[e.ItemIndex].FindControl("chk_PreGST")) as CheckBox;
                        invoice.Pre_GST = chk_PreGST.Checked;

                    }
                    //viveksinha-start
                    invoice.UserID = Common.LoggedInUserID();
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.UpdatedDate = DateTime.Now;
                    unitOfwork.OfflineinvoiceRepository.Update(invoice);
                    unitOfwork.Save();
                    lv_crdrRegister.EditIndex = -1;
                    BindItems(ReturnType);
                    //end
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_crdrRegister_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();

                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.CreditDebitReg;
                    uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");

                    TextBox txtRcvrGSTIN = (e.Item.FindControl("txtRcvrGSTIN")) as TextBox;
                    if (txtRcvrGSTIN.Text != null)
                        obj.ReceiverGSTIN = txtRcvrGSTIN.Text.Trim();

                    TextBox txtVoucherNo = (e.Item.FindControl("txtVoucherNo")) as TextBox;
                    if (txtVoucherNo.Text != null)
                        obj.Voucher_No = txtVoucherNo.Text.Trim();

                    TextBox txt_VoucherDate = (e.Item.FindControl("txt_VoucherDate")) as TextBox;
                    if (txt_VoucherDate.Text != null || txt_VoucherDate.Text != "-")
                        obj.Voucher_date = DateTime.ParseExact(txt_VoucherDate.Text, "dd/MM/yyyy", null);

                    TextBox txt_invoiceno = (e.Item.FindControl("txt_invoiceno")) as TextBox;
                    if (txt_invoiceno.Text != null)
                        obj.InvoiceNo = txt_invoiceno.Text.Trim();

                    TextBox txt_InvoiceDate = (e.Item.FindControl("txt_InvoiceDate")) as TextBox;
                    if (txt_InvoiceDate.Text != null || txt_InvoiceDate.Text != "-")
                    {
                        DateTime SOrderDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                        obj.InvoiceDate = SOrderDate;
                    }

                    DropDownList ddl_DocumentType = (e.Item.FindControl("ddl_DocumentType")) as DropDownList;
                    if (ddl_DocumentType.SelectedIndex > 0)
                        obj.Document_Type = ddl_DocumentType.SelectedValue;

                    DropDownList ddl_IssuingNote = (e.Item.FindControl("ddl_IssuingNote")) as DropDownList;
                    if (ddl_IssuingNote.SelectedIndex > 0)
                        obj.Issuing_Note = Convert.ToInt32(ddl_IssuingNote.SelectedItem.Value);

                    TextBox txt_VoucherValue = (e.Item.FindControl("txt_VoucherValue")) as TextBox;
                    if (txt_VoucherValue != null)
                        obj.Voucher_Value = txt_VoucherValue.Text.Trim();

                    if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                        obj.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                    if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);


                    CheckBox chk_PreGST = (e.Item.FindControl("chk_PreGST")) as CheckBox;
                    obj.Pre_GST = chk_PreGST.Checked;

                }
                //viveksinha-start
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = Common.LoggedInUserID();
                //end
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_crdrRegister.EditIndex = -1;
                //lv_crdrRegister.EditIndex++;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        protected void lv_crdrRegister_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");

                uc_SupplyType.BindItems();

                // Document Type
                DropDownList ddl_DocumentType = (DropDownList)e.Item.FindControl("ddl_DocumentType");
                if (ddl_DocumentType != null)
                {
                    foreach (EnumConstants.NoteType r in Enum.GetValues(typeof(EnumConstants.NoteType)))
                    {
                        ddl_DocumentType.Items.Add(new ListItem(Enum.GetName(typeof(EnumConstants.NoteType), r), Convert.ToString(r).ToString()));
                    }
                    ddl_DocumentType.Items.Insert(0, new ListItem(" [SELECT] ", "-1"));
                }

                // Reason for issuing note
                DropDownList ddl_IssuingNote = (DropDownList)e.Item.FindControl("ddl_IssuingNote");
                if (ddl_IssuingNote != null)
                {
                    var data = unitOfwork.OfflineissuingnoteRepository.All().ToList();
                    data.Insert(0, new GST_TRN_OFFLINE_ISSUINGNOTE_REASON { IssuingNoteName = "[ SELECT ]", NoteID = -1 });
                    ddl_IssuingNote.DataSource = data;
                    ddl_IssuingNote.DataTextField = "IssuingNoteName";
                    ddl_IssuingNote.DataValueField = "NoteID";
                    ddl_IssuingNote.DataBind();
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
                foreach (var item in lv_crdrRegister.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_crdrRegister.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
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

        protected void lv_crdrRegister_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpcrdr_registered.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpcrdr_registered.DataBind();
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