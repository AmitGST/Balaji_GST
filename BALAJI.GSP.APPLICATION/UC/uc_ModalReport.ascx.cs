using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_ModalReport : System.Web.UI.UserControl
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUsers();
            }
        }
        private void GetUsers()
        {
            var users = unitOfWork.AspnetRepository.All();
            lvUsers.DataSource = users.ToList();
            lvUsers.DataBind();
        }
        public string ReportId
        {
            set
            {
                hdnReportId.Value = value;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                GST_MST_REPORT_PERMISSION report = new GST_MST_REPORT_PERMISSION();
                int ReportId = Int32.Parse(hdnReportId.Value.ToString());

                foreach (ListViewDataItem item in lvUsers.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkReport");


                    if (chk.Checked)
                    {

                        report.UserId = Common.LoggedInUserID();
                        report.ReportId = ReportId;
                        report.CreatedBy = Common.LoggedInUserID();
                        report.CreatedDate = DateTime.Now;
                        unitOfWork.reportPermissionRepository.Create(report);
                        unitOfWork.Save();
                        uc_sucess.SuccessMessage = "Saved Successfully";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#modal1').modal();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModaldataTable", "$('.dataTable').dataTable();", true);

                    }

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}