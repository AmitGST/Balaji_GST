using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_B2B_invoice_GSTR2 : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindItems();
            }
        }
        public void BindItems()
        {
            List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
            var data = unitOfwork.OfflineinvoiceRepository.All().ToList();
            objList.AddRange(data);
            if (data.Count <= 0)
            {
                lkbDelete.Visible = false;
            }
            //objList.Add(new GST_TRN_OFFLINE_INVOICE());
            lv_B2b_Invoice_Gstr2.DataSource = objList;
            lv_B2b_Invoice_Gstr2.DataBind();
        }

        public EventHandler AddMoreClick;
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            AddMoreClick(sender, e);
        }

        protected void lv_B2b_Invoice_Gstr2_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lv_B2b_Invoice_Gstr2.EditIndex = e.NewEditIndex;
            BindItems();
        }

        protected void lv_B2b_Invoice_Gstr2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lv_B2b_Invoice_Gstr2.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
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

            //Invoice Type
            DropDownList ddl_InvoiceType = (DropDownList)e.Item.FindControl("ddl_InvoiceType");
            HiddenField hdnInvoiceType = (HiddenField)e.Item.FindControl("hdnInvoiceType");
            if (hdnInvoiceType.Value != null && hdnInvoiceType.Value != "")
                ddl_InvoiceType.Items.FindByValue(hdnInvoiceType.Value).Selected = true;
        }

        protected void lv_B2b_Invoice_Gstr2_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddlPos = (DropDownList)e.Item.FindControl("ddlPos");
            if (ddlPos != null)
            {
                var data = unitOfwork.StateRepository.All().OrderBy(o => o.StateCode).Select(x => new { TextField = x.StateCode + "-" + x.StateName, ValueField = x.StateID.ToString() }).ToList();
                data.Insert(0, new { TextField = "[ Select ]", ValueField = "0" });
                ddlPos.DataSource = data;
                ddlPos.DataTextField = "TextField";
                ddlPos.DataValueField = "ValueField";
                ddlPos.DataBind();
            }

            //supply type
            DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
            if (ddl_SupplyType != null)
            {
                foreach (EnumConstants.IsInter r in Enum.GetValues(typeof(EnumConstants.IsInter)))
                {
                    ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.IsInter), r), Convert.ToByte(r).ToString());
                    ddl_SupplyType.Items.Add(item);
                }
                ddl_SupplyType.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
            }

            //Invoice type
            DropDownList ddl_InvoiceType = (DropDownList)e.Item.FindControl("ddl_InvoiceType");
            if (ddl_InvoiceType != null)
            {
                foreach (EnumConstants.InvoiceType r in Enum.GetValues(typeof(EnumConstants.InvoiceType)))
                {
                    ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.InvoiceType), r), Convert.ToByte(r).ToString());
                    ddl_InvoiceType.Items.Add(item);
                }
                ddl_InvoiceType.Items.Insert(0, new ListItem(" [Select] ", "-1"));
            }
        }

       

        protected void lv_B2b_Invoice_Gstr2_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            LinkButton lkbUpdate = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
            if (lkbUpdate.CommandName == "Update")
            {
                int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                GST_TRN_OFFLINE_INVOICE invoice = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == id).SingleOrDefault();
                if (invoice != null)
                {
                    TextBox txtGSTIN = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("txtGSTIN")) as TextBox;
                    if (txtGSTIN != null)
                        invoice.ReceiverGSTIN = txtGSTIN.Text;
                    TextBox txtInvoiceNo = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("txtInvoiceNo")) as TextBox;
                    if (txtInvoiceNo != null)
                        invoice.InvoiceNo = txtInvoiceNo.Text;
                    TextBox txt_InvoiceDate = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("txt_InvoiceDate")) as TextBox;
                    if (txt_InvoiceDate != null)
                        invoice.InvoiceDate = Convert.ToDateTime(txt_InvoiceDate.Text);
                    TextBox txtInvoiceValue = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("txtInvoiceValue")) as TextBox;
                    if (txtInvoiceValue != null)
                        invoice.TotalInvoiceValue = txtInvoiceValue.Text;
                    DropDownList ddlPos = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("ddlPos")) as DropDownList;
                    if (ddlPos != null)
                        invoice.PlaceofSupply = Convert.ToByte(ddlPos.SelectedValue);
                    DropDownList ddl_SupplyType = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("ddl_SupplyType")) as DropDownList;
                    if (ddl_SupplyType.SelectedIndex > 0)
                        invoice.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedValue);
                    DropDownList ddl_InvoiceType = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("ddl_InvoiceType")) as DropDownList;
                    if (ddl_InvoiceType.SelectedIndex > 0)
                        invoice.InvoiceType = Convert.ToByte(ddl_InvoiceType.SelectedValue);
                    CheckBox chkReverse = (lv_B2b_Invoice_Gstr2.Items[e.ItemIndex].FindControl("chkReverse")) as CheckBox;
                    invoice.ReverseCharge = chkReverse.Checked;
                }
                unitOfwork.OfflineinvoiceRepository.Update(invoice);
                unitOfwork.Save();
            }
            lv_B2b_Invoice_Gstr2.EditIndex = -1;
            BindItems();
        }

        protected void lv_B2b_Invoice_Gstr2_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();

            LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
            if (lkbInsert.CommandName == "Insert")
            {
                TextBox txtGSTIN = (e.Item.FindControl("txtGSTIN")) as TextBox;
                if (txtGSTIN != null)
                    obj.ReceiverGSTIN = txtGSTIN.Text;
                TextBox txtInvoiceNo = (e.Item.FindControl("txtInvoiceNo")) as TextBox;
                if (txtInvoiceNo != null)
                    obj.InvoiceNo = txtInvoiceNo.Text;
                TextBox txt_InvoiceDate = (e.Item.FindControl("txt_InvoiceDate")) as TextBox;
                if (txt_InvoiceDate != null)
                    obj.InvoiceDate = DateTime.ParseExact(txt_InvoiceDate.Text, "dd/MM/yyyy", null);
                //obj.InvoiceDate = Convert.ToDateTime(txt_InvoiceDate.Text);
                TextBox txtInvoiceValue = (e.Item.FindControl("txtInvoiceValue")) as TextBox;
                if (txtInvoiceValue != null)
                    obj.TotalInvoiceValue = txtInvoiceValue.Text;
                DropDownList ddlPos = (e.Item.FindControl("ddlPos")) as DropDownList;
                if (ddlPos.SelectedIndex > 0)
                    obj.PlaceofSupply = Convert.ToByte(ddlPos.SelectedItem.Value);
                DropDownList ddl_SupplyType = (e.Item.FindControl("ddl_SupplyType")) as DropDownList;
                if (ddl_SupplyType.SelectedIndex > 0)
                    obj.SupplyType = Convert.ToByte(ddl_SupplyType.SelectedValue);
                DropDownList ddl_InvoiceType = (e.Item.FindControl("ddl_InvoiceType")) as DropDownList;
                if (ddl_InvoiceType.SelectedIndex > 0)
                    obj.InvoiceType = Convert.ToByte(ddl_InvoiceType.SelectedValue);
                CheckBox chkReverse = (e.Item.FindControl("chkReverse")) as CheckBox;
                obj.ReverseCharge = chkReverse.Checked;
            }
            unitOfwork.OfflineinvoiceRepository.Create(obj);
            unitOfwork.Save();
            lv_B2b_Invoice_Gstr2.EditIndex++;
            BindItems();
        }
        protected void lkbDelete_Click(object sender, EventArgs e)
        {
            try
            {

                int ValueId;
                foreach (var item in lv_B2b_Invoice_Gstr2.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_B2b_Invoice_Gstr2.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
                        var OfflineObj = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == ValueId).SingleOrDefault();
                        if (OfflineObj != null)
                        {
                            var offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId);
                            foreach (var data in offlinedataobj)
                            {
                                unitOfwork.OfflineinvoicedataitemRepository.Delete(data);
                                unitOfwork.Save();
                            }
                            unitOfwork.OfflineinvoiceRepository.Delete(OfflineObj);
                            unitOfwork.Save();
                            uc_sucess.SuccessMessage = "Data Deleted Successfully.";
                            uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
                        }
                    }
                }
                BindItems();
            }
            catch (Exception ex)
            {

            }
        }
    }
}