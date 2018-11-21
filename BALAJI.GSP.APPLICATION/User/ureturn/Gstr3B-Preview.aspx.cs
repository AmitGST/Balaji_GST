using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class Gstr3B_Preview : System.Web.UI.Page
    {
           UnitOfWork unitofwork=new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
        }
        
      private void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var loggedinUser = Common.LoggedInUserID();
                var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

      
        protected void lkbDownload3B_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbdownload = (LinkButton)sender;
                String UserID = Convert.ToString(lkbdownload.CommandArgument.ToString());
                var userId = Common.LoggedInUserID();
                //var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var SelectedMonth = Int32.Parse(uc_invoiceMonth.GetValue);
                /// var invoice = unitOfWork.OfflineAudittrailRepository.Filter(f => f.UserID == userId).FirstOrDefault();

                ReportGenerate.DownloadGstr_3B(userId, SelectedMonth);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbView3B_Click(object sender, EventArgs e)
        {
            
            //uc_ReportViewer.GenerateReport(userId, SelectedMonth);
            //ReportViewer MyReportViewer = new ReportViewer();
            //LinkButton lkbView3B = (LinkButton)sender;
            //String UserID = Convert.ToString(lkbView3B.CommandArgument.ToString());
            //var userId = Common.LoggedInUserID();
            ////var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
            //var SelectedMonth = Int32.Parse(uc_invoiceMonth.GetValue);
            ///// var invoice = unitOfWork.OfflineAudittrailRepository.Filter(f => f.UserID == userId).FirstOrDefault();
            ////ReportGenerate.ViewGstr_3B(userId, SelectedMonth);

        }

        protected void lkbView3B_Click1(object sender, EventArgs e)
        {
            var userId = Common.LoggedInUserID();
            var SelectedMonth = Int32.Parse(uc_invoiceMonth.GetValue);
            uc_ReportViewer.GenerateReport(userId, SelectedMonth);

            

        }
        }

    }
