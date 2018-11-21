using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Admin
{
    [Authorize("Admin")]
    public partial class ReportMaster : System.Web.UI.Page
    {
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
                BindReport();

            }
        }

        //protected void ChckBStatus_CheckedChanged()
        //{

        //}



        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            GST_MST_REPORT report = new GST_MST_REPORT();
            try
            {
                report.ReportName = txtReportName.Text.Trim();
                report.ReportPath = txtReportPath.Text.Trim();
                report.ReportControlName = txtReportControl.Text.Trim();
                report.SerialNo = Convert.ToInt32(txtSerial.Text.Trim());
                report.Status = true;
                report.CreatedBy = Common.LoggedInUserID();
                report.CreatedDate = DateTime.Now;
                unitOfwork.ReportRepository.Create(report);
                unitOfwork.Save();
                uc_sucess.SuccessMessage = "Data saved successfully.";
                uc_sucess.Visible = true;
                ClearReport();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                uc_sucess.ErrorMessage = ex.Message;
                uc_sucess.VisibleError = true;
            }

        }
        private void ClearReport()
        {
            txtReportName.Text = string.Empty;
            txtReportPath.Text = string.Empty;
            txtReportControl.Text = string.Empty;
            txtSerial.Text = string.Empty;
        }
        private void BindReport()
        {
            unitOfwork = new UnitOfWork();
            var userID = Common.LoggedInUserID();
            lvReport.DataSource = unitOfwork.ReportRepository.All().OrderByDescending(o => o.CreatedDate).ToList();
            lvReport.DataBind();
        }
        protected void lvReport_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpReportPermission.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindReport();
            dpReportPermission.DataBind();
        }


        //protected void chkbox_CheckedChanged(object sender, EventArgs e)
        //{
        //int ReportId = Int32.Parse(hdnReportId.Value);
        //var ReportEntity = unitOfwork.reportPermissionRepository.Filter(x => x.ReportId == ReportId).SingleOrDefault();
        //if (ReportEntity == null)
        //{
        //    GST_MST_REPORT_PERMISSION report = new GST_MST_REPORT_PERMISSION();
        //    report.UserId = Common.LoggedInUserID();
        //    report.ReportId = ReportId;
        //    report.CreatedBy = Common.LoggedInUserID();
        //    report.CreatedDate = DateTime.Now;
        //    unitOfwork.reportPermissionRepository.Create(report);
        //    unitOfwork.Save();
        //}
        //else
        //{
        //    unitOfwork.reportPermissionRepository.Delete(ReportEntity);
        //    unitOfwork.Save();
        //}
        //}


        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    int ReportId = Int32.Parse(hdnReportId.Value.ToString());
        //    var ReportEntity = unitOfwork.reportPermissionRepository.Filter(x => x.ReportId == ReportId).SingleOrDefault();
        //    if (ReportEntity == null)
        //    {
        //        GST_MST_REPORT_PERMISSION report = new GST_MST_REPORT_PERMISSION();
        //        report.UserId = Common.LoggedInUserID();
        //        report.ReportId = ReportId;
        //        report.CreatedBy = Common.LoggedInUserID();
        //        report.CreatedDate = DateTime.Now;
        //        unitOfwork.reportPermissionRepository.Create(report);
        //        unitOfwork.Save();
        //    }
        //    else if (!chkbox.Checked)
        //    {
        //        unitOfwork.reportPermissionRepository.Delete(ReportEntity);
        //        unitOfwork.Save();
        //    }
        //    uc_sucess1.SuccessMessage = "Saved Successfully";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#modal1').modal();", true);
        //    return;
        //}

        protected void lkbView_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int ReportId = Convert.ToInt32(lkbItem.CommandArgument);
                    var reportName = unitOfwork.ReportRepository.Filter(x => x.ReportId == ReportId).FirstOrDefault().ReportName;

                    uc_ModalReport.ReportId = lkbItem.CommandArgument;
                    var entity = unitOfwork.reportPermissionRepository.Filter(f => f.ReportId == ReportId).FirstOrDefault();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$(function(){  $('#lvItems').dataTable();$('#modal1').modal();});", true);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}