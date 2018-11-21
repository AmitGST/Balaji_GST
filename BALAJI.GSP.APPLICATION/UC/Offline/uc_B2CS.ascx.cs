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
    public partial class uc_B2CS : System.Web.UI.UserControl
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
                var sheetType = (byte)EnumConstants.OfflineExcelSection.B2CS;
               // List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
                
                
                List<GST_TRN_OFFLINE_INVOICE_DATAITEM> objList = new List<GST_TRN_OFFLINE_INVOICE_DATAITEM>();
                var data = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.GST_TRN_OFFLINE_INVOICE.ReturnType == ReturnType && x.GST_TRN_OFFLINE_INVOICE.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.Status != deactive && x.GST_TRN_OFFLINE_INVOICE.GST_TRN_OFFLINE.GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL.AuditTrailID == AuditTrailId && x.GST_TRN_OFFLINE_INVOICE.SectionType == sheetType).ToList();

                // var data = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.GST_TRN_OFFLINE_INVOICE.ReturnType == ReturnType && x.GST_TRN_OFFLINE_INVOICE.SectionType == (byte)EnumConstants.OfflineExcelSection.B2CS);
                objList.AddRange(data);
                lv_B2CS.DataSource = objList;
                lv_B2CS.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        int? DisplayIndex;
        protected void lv_B2CS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {  //Type
                DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlType");
                HiddenField hdnType = (HiddenField)e.Item.FindControl("hdnType");
                HiddenField hdnCess = (HiddenField)e.Item.FindControl("hdnCess");
                HiddenField hdnIGST = (HiddenField)e.Item.FindControl("hdnIGST");
                HiddenField hdnSGST = (HiddenField)e.Item.FindControl("hdnSGST");
                HiddenField hdnCGST = (HiddenField)e.Item.FindControl("hdnCGST");
                HiddenField hdnTotalTaxableValue = (HiddenField)e.Item.FindControl("hdnTotalTaxableValue");
                TextBox txtEcommerce = (TextBox)e.Item.FindControl("txtECommerce");
                if (hdnType.Value != null && hdnType.Value != "")
                    ddlType.Items.FindByValue(hdnType.Value).Selected = true;

                uc_SupplyType_B2CS uc_SupplyTypeB2Cs = (uc_SupplyType_B2CS)e.Item.FindControl("uc_SupplyType_B2CS");
                uc_SupplyTypeB2Cs.BindItems();
                uc_SupplyTypeB2Cs.TotalTaxable_Value = hdnTotalTaxableValue.Value;
                uc_SupplyTypeB2Cs.IntegratedTax = hdnIGST.Value;
                uc_SupplyTypeB2Cs.Cess = string.IsNullOrEmpty(hdnCess.Value as string) ? "0.0" : hdnCess.Value;
                uc_SupplyTypeB2Cs.StateTax = hdnSGST.Value;
                uc_SupplyTypeB2Cs.CentralTax = hdnCGST.Value;


                if (DisplayIndex == e.Item.DisplayIndex)
                {
                    uc_SupplyTypeB2Cs.IntegratedTax_enable = true;
                    uc_SupplyTypeB2Cs.CentralTax_enable = true;
                    uc_SupplyTypeB2Cs.Cess_enable = true;
                    uc_SupplyTypeB2Cs.StateTax_enable = true;
                    uc_SupplyTypeB2Cs.TotalTaxable_enable = true;
                    uc_SupplyTypeB2Cs.ddlPos_enable = true;
                    uc_SupplyTypeB2Cs.ddlRate_enable = true;
                    ddlType.Enabled = true;
                    txtEcommerce.Enabled = true;

                }
                else
                {
                    uc_SupplyTypeB2Cs.IntegratedTax_enable = false;
                    uc_SupplyTypeB2Cs.CentralTax_enable = false;
                    uc_SupplyTypeB2Cs.Cess_enable = false;
                    uc_SupplyTypeB2Cs.StateTax_enable = false;
                    uc_SupplyTypeB2Cs.TotalTaxable_enable = false;
                    uc_SupplyTypeB2Cs.ddlPos_enable = false;
                    uc_SupplyTypeB2Cs.ddlRate_enable = false;
                    ddlType.Enabled = false;
                    txtEcommerce.Enabled = false;

                }
                HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
                if (hdnPos.Value != null && hdnPos.Value != "")
                    uc_SupplyTypeB2Cs.ddlPos_SelectedValue = hdnPos.Value;

                HiddenField Hdn_SupplyType = (HiddenField)e.Item.FindControl("Hdn_SupplyType");
                if (Hdn_SupplyType.Value != null && Hdn_SupplyType.Value != "")
                    uc_SupplyTypeB2Cs.ddlSupplyType_SelectedValue = Hdn_SupplyType.Value;
                var UserId = Common.LoggedInUserID();
                var StateCode = unitOfwork.AspnetRepository.Filter(x => x.Id == UserId).SingleOrDefault().StateCode;
                var POS_StateId = Convert.ToInt32(uc_SupplyTypeB2Cs.ddlPos_SelectedValue);
                var POS_StateCode = unitOfwork.StateRepository.Filter(x => x.StateID == POS_StateId).SingleOrDefault().StateCode;
                if (StateCode == POS_StateCode)
                    uc_SupplyTypeB2Cs.ddlSupplyType_SelectedValue = "1";
                else
                    uc_SupplyTypeB2Cs.ddlSupplyType_SelectedValue = "0";
                //Rate


                HiddenField hdnRate = (HiddenField)e.Item.FindControl("hdnRate");
                if (hdnRate.Value != null && hdnRate.Value != "")
                    uc_SupplyTypeB2Cs.ddlRate_SelectedValue = hdnRate.Value;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CS_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                lv_B2CS.EditIndex = e.NewEditIndex;
                DisplayIndex = lv_B2CS.EditIndex;

                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CS_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                LinkButton lkbUpdate = (lv_B2CS.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                    int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                    byte Rate;
                    GST_TRN_OFFLINE_INVOICE_DATAITEM invoice = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.OfflineDataID == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        DropDownList ddlType = (lv_B2CS.Items[e.ItemIndex].FindControl("ddlType")) as DropDownList;
                        if (ddlType != null)
                            invoice.GST_TRN_OFFLINE_INVOICE.Type = Convert.ToByte(ddlType.SelectedValue);
                        uc_SupplyType_B2CS uc_SupplyTypeB2Cs = (uc_SupplyType_B2CS)lv_B2CS.Items[e.ItemIndex].FindControl("uc_SupplyType_B2CS");

                        invoice.GST_TRN_OFFLINE_INVOICE.PlaceofSupply = Convert.ToByte(uc_SupplyTypeB2Cs.ddlPos_SelectedValue);

                        invoice.TotalTaxableValue = Convert.ToDecimal(uc_SupplyTypeB2Cs.TotalTaxable_Value);
                        if (uc_SupplyTypeB2Cs.ddlSupplyType_SelectedIndex > 0)
                            invoice.GST_TRN_OFFLINE_INVOICE.SupplyType = Convert.ToByte(uc_SupplyTypeB2Cs.ddlSupplyType_SelectedValue);
                        invoice.RateId = Convert.ToInt32(uc_SupplyTypeB2Cs.ddlRate_SelectedValue);
                        invoice.IGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.IntegratedTax);

                        invoice.CGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.CentralTax);

                        invoice.SGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.StateTax);

                        invoice.CessAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.Cess);

                        TextBox txtECommerce = (lv_B2CS.Items[e.ItemIndex].FindControl("txtECommerce")) as TextBox;
                        if (txtECommerce.Text != null || txtECommerce.Text != "")
                            invoice.GST_TRN_OFFLINE_INVOICE.ECommerce_GSTIN = txtECommerce.Text;
                    }
                    //viveksinha-start
                    invoice.GST_TRN_OFFLINE_INVOICE.UserID = Common.LoggedInUserID();
                    invoice.UpdatedBy = Common.LoggedInUserID();
                    invoice.UpdatedDate = DateTime.Now;
                    //end
                    unitOfwork.OfflineinvoicedataitemRepository.Update(invoice);
                    unitOfwork.Save();
                }
                lv_B2CS.EditIndex = -1;
                BindItems(ReturnType);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CS_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                byte Rate;
                GST_TRN_OFFLINE_INVOICE_DATAITEM obj = new GST_TRN_OFFLINE_INVOICE_DATAITEM();
                obj.GST_TRN_OFFLINE_INVOICE = new GST_TRN_OFFLINE_INVOICE();
                LinkButton lkbInsert = (e.Item.FindControl("lkbInsert")) as LinkButton;
                if (lkbInsert.CommandName == "Insert")
                {
                    obj.GST_TRN_OFFLINE_INVOICE.ReturnType = ReturnType;
                    obj.GST_TRN_OFFLINE_INVOICE.SectionType = (byte)EnumConstants.OfflineExcelSection.B2CS;

                    DropDownList ddlType = (e.Item.FindControl("ddlType")) as DropDownList;
                    if (ddlType.SelectedIndex > 0)
                        obj.GST_TRN_OFFLINE_INVOICE.Type = Convert.ToByte(ddlType.SelectedValue);

                    uc_SupplyType_B2CS uc_SupplyTypeB2Cs = (uc_SupplyType_B2CS)e.Item.FindControl("uc_SupplyType_B2CS");


                    obj.GST_TRN_OFFLINE_INVOICE.PlaceofSupply = Convert.ToByte(uc_SupplyTypeB2Cs.ddlPos_SelectedValue);

                    obj.TotalTaxableValue = Convert.ToDecimal(uc_SupplyTypeB2Cs.TotalTaxable_Value);
                    if (uc_SupplyTypeB2Cs.ddlSupplyType_SelectedIndex > 0)
                        obj.GST_TRN_OFFLINE_INVOICE.SupplyType = Convert.ToByte(uc_SupplyTypeB2Cs.ddlSupplyType_SelectedValue);


                    obj.RateId = Convert.ToInt32(uc_SupplyTypeB2Cs.ddlRate_SelectedValue);


                    obj.IGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.IntegratedTax);

                    obj.CGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.CentralTax);

                    obj.SGSTAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.StateTax);

                    obj.CessAmt = Convert.ToDecimal(uc_SupplyTypeB2Cs.Cess);

                    TextBox txtECommerce = (e.Item.FindControl("txtECommerce")) as TextBox;
                    if (txtECommerce != null)
                        obj.GST_TRN_OFFLINE_INVOICE.ECommerce_GSTIN = txtECommerce.Text.Trim();

                }
                //viveksinha-start
                obj.GST_TRN_OFFLINE_INVOICE.UserID = Common.LoggedInUserID();
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = Common.LoggedInUserID();
                //end
                unitOfwork.OfflineinvoicedataitemRepository.Create(obj);
                unitOfwork.Save();
                lv_B2CS.EditIndex = -1;
                //lv_B2CS.EditIndex++;
                BindItems(ReturnType);
                lkbDelete.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CS_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                uc_SupplyType_B2CS uc_SupplyType_B2CS = (uc_SupplyType_B2CS)e.Item.FindControl("uc_SupplyType_B2CS");

                uc_SupplyType_B2CS.BindItems();

                DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlType");
                if (ddlType != null)
                {
                    foreach (EnumConstants.UserType r in Enum.GetValues(typeof(EnumConstants.UserType)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.UserType), r), Convert.ToByte(r).ToString());
                        ddlType.Items.Add(item);
                    }

                    ddlType.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
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
                foreach (var item in lv_B2CS.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        ValueId = Convert.ToInt32(lv_B2CS.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
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
                                //this.SuccessMessage = "Data Deleted Successfully.";
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                                //Response.Write("<script>alert('Are you sure Want to delete data');</script>");
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
                 
                    //uc_sucess.ErrorMessage = "";
                    //uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lv_B2CS_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpB2CS.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindItems(ReturnType);
                dpB2CS.DataBind();
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