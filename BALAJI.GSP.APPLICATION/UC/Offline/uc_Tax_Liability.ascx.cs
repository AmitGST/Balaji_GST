using System;
using System.Collections.Generic;
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
    public partial class uc_Tax_Liability : System.Web.UI.UserControl
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
                var sheetType = (byte)EnumConstants.OfflineExcelSection.TaxLiability;
                
                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.SectionType == (byte)EnumConstants.OfflineExcelSection.TaxLiability).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                //objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_Tax_Liability.DataSource = objList;
                lv_Tax_Liability.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Tax_Liability_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                uc_SupplyType.BindItems();
                uc_SupplyType.ddlPos_enable = false;
                HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
                if (hdnPos.Value != null && hdnPos.Value != "")
                    uc_SupplyType.ddlPos_SelectedValue = hdnPos.Value;


                //supply type
                HiddenField Hdn_SupplyType = (HiddenField)e.Item.FindControl("Hdn_SupplyType");
                if (Hdn_SupplyType.Value != null && Hdn_SupplyType.Value != "")
                    uc_SupplyType.ddlSupplyType_SelectedValue = Hdn_SupplyType.Value;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public EventHandler AddMoreClick;
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            try
            {
                //amit 6-12-17 for rate
                Session["ValueId"] = ((LinkButton)sender).CommandArgument;
                //ListViewItem item = (ListViewItem)lv_Tax_Liability.Items[Convert.ToInt32(Session["displayIndex"])];
                //TextBox txtInvoice = (TextBox)item.FindControl("txtInvoiceNo");
                ////InvoiceNumber = txtInvoice.Text;
                AddMoreClick(sender, e);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Tax_Liability_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpTaxLiability.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpTaxLiability.DataBind();
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
                foreach (var item in lv_Tax_Liability.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_Tax_Liability.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
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
                if (count == 0)
                {
                    this.WarningMessage = "Please select atleast one row to delete data.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //uc_sucess.ErrorMessage = "Please select atleast one row to delete data.";
                    //uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Tax_Liability_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_Tax_Liability.EditIndex = e.NewEditIndex;
                //Session["displayIndex"] = e.NewEditIndex;
                BindItems(ReturnType);
                lv_Tax_Liability.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Tax_Liability_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();
                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.TaxLiability;
                    if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                        obj.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                    if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);
                    //DropDownList ddlPos = (e.Item.FindControl("ddlPos")) as DropDownList;
                    //if (ddlPos.SelectedIndex > 0)
                    //    obj.PlaceofSupply = Convert.ToByte(ddlPos.SelectedItem.Value);
                    //DropDownList ddlSupplyType = (e.Item.FindControl("ddlSupplyType")) as DropDownList;
                    //if (ddlSupplyType.SelectedIndex > 0)
                    //    obj.SupplyType = Convert.ToByte(ddlSupplyType.SelectedValue);
                }
                obj.CreatedDate = DateTime.Now;
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedBy = Common.LoggedInUserID();
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_Tax_Liability.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Tax_Liability_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                uc_SupplyType.BindItems();
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