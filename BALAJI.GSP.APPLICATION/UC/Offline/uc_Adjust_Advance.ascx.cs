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
    public partial class uc_Adjust_Advance : System.Web.UI.UserControl
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
                var sheetType = (byte)EnumConstants.OfflineExcelSection.AdvanceAdjustment;
                

                List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.SectionType == sheetType).ToList();
                //var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ReturnType == ReturnType && x.SectionType == (byte)EnumConstants.OfflineExcelSection.AdvanceAdjustment).ToList();
                objList.AddRange(data);
                if (data.Count <= 0)
                {
                    lkbDelete.Visible = false;
                }
                // objList.Add(new GST_TRN_OFFLINE_INVOICE());
                lv_Adjust_Advances.DataSource = objList;
                lv_Adjust_Advances.DataBind();
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Adjust_Advances_ItemDataBound(object sender, ListViewItemEventArgs e)
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
                HiddenField HdnSupply = (HiddenField)e.Item.FindControl("HdnSupply");
                if (HdnSupply.Value != null && HdnSupply.Value != "")
                    uc_SupplyType.ddlSupplyType_SelectedValue = HdnSupply.Value;
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
                foreach (var item in lv_Adjust_Advances.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_Adjust_Advances.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
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
                 //   uc_sucess.ErrorMessage = "Please select atleast one row to delete data.";
                   // uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Adjust_Advances_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpAdjustAdvance.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpAdjustAdvance.DataBind();
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
                Session["ValueId"] = ((LinkButton)sender).CommandArgument;
                AddMoreClick(sender, e);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Adjust_Advances_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_Adjust_Advances.EditIndex = e.NewEditIndex;
                BindItems(ReturnType);
                //lv_Adjust_Advances.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_Adjust_Advances_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE obj = new GST_TRN_OFFLINE_INVOICE();
                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    uc_SupplyType uc_SupplyType = (uc_SupplyType)e.Item.FindControl("uc_SupplyType");
                    obj.ReturnType = ReturnType;
                    obj.SectionType = (byte)EnumConstants.OfflineExcelSection.AdvanceAdjustment;
                    if (uc_SupplyType.ddlPos_SelectedIndex > 0)
                       obj.PlaceofSupply = Convert.ToByte(uc_SupplyType.ddlPos_SelectedValue);

                    if (uc_SupplyType.ddlSupplyType_SelectedIndex > 0)
                        obj.SupplyType = Convert.ToByte(uc_SupplyType.ddlSupplyType_SelectedValue);
                }
                obj.UserID = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = Common.LoggedInUserID();
                unitOfwork.OfflineinvoiceRepository.Create(obj);
                unitOfwork.Save();
                lv_Adjust_Advances.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex,Common.LoggedInUserID());
            }
        }

        protected void lv_Adjust_Advances_ItemCreated(object sender, ListViewItemEventArgs e)
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