using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.Data;


namespace BALAJI.GSP.APPLICATION.User.Trans
{
    public partial class ItextSharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            string pdfFileName = Request.PhysicalApplicationPath + "User\\Trans\\GenerateHTMLTOPDF.pdf";
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        //protected void lkbDownloadPDF_Click(object sender, EventArgs e)
        //{
        //    DownloadAsPDF();
        //}

        //public void DownloadAsPDF()
        //{
        //    try
        //    {
        //        string strHtml = string.Empty;
        //        string pdfFileName = Request.PhysicalApplicationPath + "..\\Trans \\" + "GenerateHTMLTOPDF.pdf";
 
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        dvHtml.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        strHtml = sr.ReadToEnd();
        //        sr.Close();
 
        //        CreatePDFFromHTMLFile(strHtml, pdfFileName);
 
        //        Response.ContentType = "application/x-download";
        //        Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "GenerateHTMLTOPDF.pdf"));
        //        Response.WriteFile(pdfFileName);
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        

        //public void CreatePDFFromHTMLFile(string HtmlStream, string FileName)
        //{
        //    try
        //    {
        //        object TargetFile = FileName;
        //        string ModifiedFileName = string.Empty;
        //        string FinalFileName = string.Empty;


        //        GeneratePDF.HtmlToPdfBuilder builder = new GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);
        //        GeneratePDF.HtmlPdfPage first = builder.AddPage();
        //        first.AppendHtml(HtmlStream);
        //        byte[] file = builder.RenderPdf();
        //        File.WriteAllBytes(TargetFile.ToString(), file);

        //        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
        //        ModifiedFileName = TargetFile.ToString();
        //        ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

        //        iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
        //        reader.Close();
        //        if (File.Exists(TargetFile.ToString()))
        //            File.Delete(TargetFile.ToString());
        //        FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
        //        File.Copy(ModifiedFileName, FinalFileName);
        //        if (File.Exists(ModifiedFileName))
        //            File.Delete(ModifiedFileName);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}