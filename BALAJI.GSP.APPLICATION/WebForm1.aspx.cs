
using Microsoft.Reporting.WebForms;
//using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //DataTable dt = new DataTable();
            //dt = ii.Fill(rpt_Invoice_DataSet.RPT_INVOICE_GENERATEDataTable.GetDataTableSchema() GetData());
            //Generatereport(dt);
            Generatereport();
        }
        private void Generatereport()
        {
           // DataSet ds = new DataSet();
           // rpt_Invoice_DataSet.RPT_INVOICE_GENERATEDataTable dt1 = new rpt_Invoice_DataSet.RPT_INVOICE_GENERATEDataTable();
           // rpt_Invoice_DataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter ii = new rpt_Invoice_DataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter();
           ////ii.SelectCommand = this.CommandCollection[1];
           // //dt1.EnforceConstraints = false;
           // ii.Fill(dt1);
           
           // ReportViewer1.SizeToReportContent = true;
           // ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Invoice/InvoiceGenerate.rdlc");
           // ReportViewer1.LocalReport.DataSources.Clear();
           // ReportDataSource _rsource = new ReportDataSource("dsInvoiceReport", dt1.AsDataTable());
           // ReportViewer1.LocalReport.DataSources.Add(_rsource);
           // ReportViewer1.LocalReport.Refresh();
        }

        private void CreatePDF(string fileName)
        {
            // Setup DataSet

            BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter ds = new BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter();


            // Create Report DataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.GetData().AsEnumerable());


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report/Invoice/Invoice.rdlc";
            viewer.LocalReport.DataSources.Add(rds); // Add datasource here


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CreatePDF("asasdsasdasdasd");
            //ReportGenerate.CreatePDF("asasasaasaasas32", ReportViewer1);// CreatePDF.
        }
    }
}