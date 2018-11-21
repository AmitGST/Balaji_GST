using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace BALAJI.GSP.APPLICATION.UC.Report
{
    public partial class uc_ReportViewer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void GenerateReport(string SELLERUSERID, int MONTH)
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                IReportServerCredentials ssrscredentials = new ReportGenerate.CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
                ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://server/ReportServer");
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Invoice_Report/rpt_GSTR3B");
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Values.Add(SELLERUSERID.ToString());
                reportParameterCollection[1].Values.Add(MONTH.ToString());
                ReportDataSource rds = new ReportDataSource("SELLERUSERID", "MONTH");
                ReportViewer1.LocalReport.DataSources.Clear();
                //Add ReportDataSource  
                ReportViewer1.LocalReport.DataSources.Add(rds);
                //ReportViewer1.ServerReport.SetParameters(reportParameterCollection);
              
                ReportViewer1.ServerReport.Refresh();
                
            }
            catch (Exception ex)
            {

            }

        }
    }
}
