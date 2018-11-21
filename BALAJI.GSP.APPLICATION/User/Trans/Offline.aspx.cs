using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using GST.Utility;
using BusinessLogic.Repositories;
using BusinessLogic;
using System.Data;
using BusinessLogic.Repositories.Offline;
using System.Drawing;


namespace BALAJI.GSP.APPLICATION.User.Trans
{
    public partial class Offline : System.Web.UI.Page
    {

        // cls_OfflineData offlineObj = new cls_OfflineData();
        clsOffline ofline = new clsOffline();
        protected override void OnPreInit(EventArgs e)
        {
            if (!Common.IsAdmin())
            {
                this.MasterPageFile = "~/User/User.master";
            }
            if (Common.IsAdmin())
            {
                this.MasterPageFile = "~/Admin/Admin.master";
            }
            base.OnPreInit(e);
        }
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (string.IsNullOrEmpty(Session["AuditTrailId"] as string) && (int?)Session["MonthId"]==null)
                //if (string.IsNullOrEmpty(Session["AuditTrailId"] as string))
                //{
                //    Response.Redirect("~/Offline/Auditrail.aspx");
                //}
                //BindMonth();
                IsTaxConsultant();
                BindFinyear();
                BindGST();
                Session["datadone"] = false;
                if (!string.IsNullOrEmpty(Session["AuditTrailId"] as string))
                {
                    AsyncFileUpload1.Visible = false;
                    int AuditTrailId = Convert.ToInt32(Session["AuditTrailId"]);
                    var offlineupload = unitOfwork.OfflineRepository.Find(x => x.AuditTrailID == AuditTrailId);
                    txtGstNo.Text = offlineupload.SupplierGSTIN;
                    txtTurnover.Text = offlineupload.AggregateTurnover.ToString();
                    txtTurnoverAToJ.Text = offlineupload.AggregateTurnoverQuater.ToString();
                    ddlfinYear.SelectedValue = offlineupload.Fin_ID.ToString();
                    //ddlMonth.SelectedValue = offlineupload.Month.ToString();
                    ddlGST.SelectedValue = offlineupload.FileType.ToString();
                    lbImport.Text = "Update Invoices";
                }
            }
        }

        private void IsTaxConsultant()
        {
            if (!Common.IsTaxConsultant())
            {
                txtGstNo.Enabled = false;
                txtGstNo.Text = Common.GetCurrentGSTIN();
            }
        }
        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {


        }
       
        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {
                Page.Validate();
                bool datadone = (bool)Session["datadone"];
                if (Page.IsValid && !datadone)
                {
                    Session["datadone"] = true;
                    string filePath = "~/Offline/" + e.FileName;
                    GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL Auditrail;
                    GST_TRN_OFFLINE offlineupload;

                    string xml = string.Empty;
                    if (AsyncFileUpload1.FailedValidation)
                    {
                        uc_sucess.ErrorMessage = "Your File Have Some Errors";
                        uc_sucess.Visible = true;
                    }
                    else if (!AsyncFileUpload1.HasFile)
                    {
                        uc_sucess.ErrorMessage = "Please Select File First";
                        uc_sucess.Visible = true;
                    }
                    else
                    {
                        Auditrail = new GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL();
                        offlineupload = new GST_TRN_OFFLINE();
                        Auditrail.ExcelName = e.FileName;
                        Auditrail.JobName = e.FileName;
                        Auditrail.CreatedBy = Common.LoggedInUserID();
                        Auditrail.CreatedDate = DateTime.Now;
                        int MonthId = Convert.ToInt32(Session["MonthId"]);
                        Auditrail.Month =Convert.ToByte(MonthId);      
                        Auditrail.Status = 1;
                        Auditrail.UserID = unitOfwork.AspnetRepository.Find(x => x.GSTNNo == txtGstNo.Text).Id;
                        Auditrail.InvoiceDate = DateTime.Now;
                        var audittrailSaved = unitOfwork.OfflineAudittrailRepository.Create(Auditrail);
                        //// getting the max audit trailID based on logged in user 
                        //// placed into 
                        // Session[""]=unitOfwork.OfflineAudittrailRepository.Find(a=>a.AuditTrailID wherer 
                        unitOfwork.Save();
                        Session["AuditTrailId"] = audittrailSaved.AuditTrailID;
                        uc_B2B_Invoices.AuditTrailId = Convert.ToInt16(Session["AuditTrailId"].ToString());
                        //xml = ds.GetXml();
                        offlineupload.CreatedBy = Common.LoggedInUserID();
                        offlineupload.CreatedDate = DateTime.Now;
                        offlineupload.AggregateTurnover = Convert.ToDecimal(txtTurnover.Text.Trim());
                        offlineupload.AggregateTurnoverQuater = Convert.ToDecimal(txtTurnoverAToJ.Text.Trim());
                        offlineupload.FileType = Convert.ToByte(ddlGST.SelectedValue.ToString());
                        offlineupload.SupplierGSTIN = txtGstNo.Text.Trim();
                        offlineupload.Month = Convert.ToByte(MonthId);  
                        offlineupload.Fin_ID = Convert.ToInt32(ddlfinYear.SelectedValue.ToString());
                        offlineupload.UploadedBy = Common.LoggedInUserID();
                        offlineupload.UploadStatus = 1;
                        offlineupload.Section = 0;
                        offlineupload.AuditTrailID = Auditrail.AuditTrailID;
                        // finyear.ExcelData = xml; 
                        unitOfwork.OfflineRepository.Create(offlineupload);
                        unitOfwork.Save();
                        filePath = "~/Offline/" + AsyncFileUpload1.FileName;
                       
                        //ds = cls_ExcelReader.ExcelReadDataSet(Server.MapPath(filePath));

                        uc_sucess.SuccessMessage = "Import file return successfully.";
                        uc_sucess.Visible = true;
                        AsyncFileUpload1.SaveAs(Server.MapPath(filePath));
                        // SaveOfflineData(filePath) ;
                        //  offlineObj.SaveOfflineData(ds, txtGstNo.Text.Trim(), Common.LoggedInUserID());
                        DataSet ds = cls_ExcelReader.ExcelReadDataSet(Server.MapPath(filePath));
                        ofline.SaveExcelOffLineData(ds, txtGstNo.Text.Trim(), Common.LoggedInUserID(), offlineupload.OfflineID.ToString());
                    }

                }
                else
                {
                   
                  
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }



        //public void BindMonth()
        //{
        //    Array Months = Enum.GetValues(typeof(EnumConstants.FinYear));
        //    foreach (EnumConstants.FinYear Month in Months)
        //    {
        //        ddlMonth.Items.Add(new ListItem(Month.ToString(), ((int)Month).ToString()));
        //    }
        //}

        public void BindFinyear()
        {
            try { 
            ddlfinYear.DataSource = unitOfwork.FinYearRepository.All().OrderBy(o => o.Finyear).ToList();
            ddlfinYear.DataTextField = "Finyear_Format";
            ddlfinYear.DataValueField = "Fin_ID";
            ddlfinYear.DataBind();
            ddlfinYear.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
            string Year = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString().Substring((DateTime.Now.Year + 1).ToString().Length - 2);
            ddlfinYear.Items.FindByText(Year).Selected = true;
                }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void BindGST()
        {
            try
            {
                ddlGST.Items.Clear();
                Array FileTypes = Enum.GetValues(typeof(EnumConstants.OfflineFileType));
                foreach (EnumConstants.OfflineFileType FileType in FileTypes)
                {
                    ddlGST.Items.Add(new ListItem(FileType.ToString(), ((int)FileType).ToString()));
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        protected void lbImport_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(Session["AuditTrailId"] as string))
                {
                    int AuditTrailId = Convert.ToInt32(Session["AuditTrailId"]);

                    var Auditrail = unitOfwork.OfflineAudittrailRepository.Find(x => x.AuditTrailID == AuditTrailId);
                    var offlineupload = unitOfwork.OfflineRepository.Find(x => x.AuditTrailID == AuditTrailId);

                    offlineupload.UpdatedBy = Common.LoggedInUserID();
                    offlineupload.UpdatedDate = DateTime.Now;
                    offlineupload.AggregateTurnover = Convert.ToDecimal(txtTurnover.Text.Trim());
                    offlineupload.AggregateTurnoverQuater = Convert.ToDecimal(txtTurnoverAToJ.Text.Trim());
                    offlineupload.FileType = Convert.ToByte(ddlGST.SelectedValue.ToString());
                    offlineupload.SupplierGSTIN = txtGstNo.Text.Trim();
                    //int MonthId = Convert.ToInt32(Session["MonthId"]);
                    //offlineupload.Month = Convert.ToByte(MonthId);  
                    offlineupload.Fin_ID = Convert.ToInt32(ddlfinYear.SelectedValue.ToString());
                    offlineupload.UploadedBy = Common.LoggedInUserID();
                    offlineupload.UploadStatus = 1;
                    offlineupload.Section = 0;
                    offlineupload.AuditTrailID = Auditrail.AuditTrailID;
                    unitOfwork.OfflineRepository.Update(offlineupload);
                    unitOfwork.Save();
                    
                    //Auditrail.Month = Convert.ToByte(MonthId); 
                    Auditrail.Status = 1;
                    Auditrail.UserID = unitOfwork.AspnetRepository.Find(x => x.GSTNNo == txtGstNo.Text).Id;
                    Auditrail.InvoiceDate = DateTime.Now;
                    Auditrail.UpdatedBy = Common.LoggedInUserID();
                    Auditrail.UpdatedDate = DateTime.Now;
                    unitOfwork.OfflineAudittrailRepository.Update(Auditrail);
                    unitOfwork.Save();
                    //Session["AuditTrailId"] = null;
                    //Session["MonthId"] = null;
                    //Session["AuditTrailId"] = ((LinkButton)sender).CommandArgument;
                }
                else
                {
                    if (AsyncFileUpload1.FailedValidation)
                    {
                        uc_sucess.ErrorMessage = "Your File Have Some Errors";
                        uc_sucess.Visible = true;
                        return;
                    }
                    else if (!AsyncFileUpload1.HasFile)
                    {
                        uc_sucess.ErrorMessage = "Please Select File First";
                        uc_sucess.Visible = true;
                        return;
                    }
                   
                }
            
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
           
            Response.Redirect("~/Offline/B2Boffline");
        }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Offline/Auditrail.aspx");
        }
    }
}
