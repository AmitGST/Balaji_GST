using BusinessLogic.Repositories;
using GST.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           // obj = new cls_ExcelReader(); 


            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //StringWriter stringWriter = new StringWriter();
            //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            //divc.RenderControl(htmlTextWriter);

            //StringReader stringReader = new StringReader(stringWriter.ToString());
            //iTextSharp.text.Document Doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 100f, 0f);
            //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(Doc);
            //iTextSharp.text.pdf.PdfWriter.GetInstance(Doc, Response.OutputStream);

            //Doc.Open();
            ////.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //htmlparser.Parse(stringReader);
            //Doc.Close();
            //Response.Write(Doc);
            //Response.End();
          
        }

        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
           

        }

        public override void VerifyRenderingInServerForm(Control txt_salutaion)
        {
            /* Verifies that the control is rendered */
        }

        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            string filePath = "~/Offline/" + e.FileName;
            AsyncFileUpload1.SaveAs(Server.MapPath(filePath));
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (AsyncFileUpload1.HasFile)
            {
                string filePath = "~/Offline/" + AsyncFileUpload1.FileName;
               DataSet ds= cls_ExcelReader.ExcelReadDataSet(Server.MapPath(filePath));

            }
        }
        //public static string PopulateForgotPasswordBody(string userName, string title, string url, string pwd)
        //{
        //    string body = string.Empty;
        //    using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/MailFormat/ForgotPassword.htm")))
        //    {
        //        body = reader.ReadToEnd();
        //    }
        //    body = body.Replace("{UserName}", userName);
        //    body = body.Replace("{Title}", title);
        //    body = body.Replace("{Url}", url);
        //    body = body.Replace("{Password}", pwd);
        //    return body;
        //}
    }
}