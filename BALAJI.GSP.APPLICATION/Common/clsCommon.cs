using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using BALAJI.GSP.APPLICATION.Model;
using BALAJI.GSP.APPLICATION;
using BALAJI.GSP.APPLICATION.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GST.Utility;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using BusinessLogic.Repositories;
using System.Configuration;



public class clsCommon
{

}

public class ReportGenerate
{
    public static void InvoicePDF(string invoiceNo,string invoiceId,string invoiceName)
    {
        try
        {

            BALAJI.GSP.APPLICATION.BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter ds = new BALAJI.GSP.APPLICATION.BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter();

            // Create Report DataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.GetData().AsEnumerable());

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report/Invoice/Invoice.rdlc";
            viewer.LocalReport.DataSources.Add(rds); // Add datasource here
            CreatePDF(invoiceName, viewer);

        }
        catch(Exception ex)
        {

        }
    }
    public static void CreatePDF(string invoiceName, ReportViewer reportViewer)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = mimeType;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + invoiceName + "." + extension);
        HttpContext.Current.Response.BinaryWrite(bytes); // create the file
        HttpContext.Current.Response.Flush(); // send it to the client to download
    }
    public static void DownloadGstr_3B(string UserId, int Month)
    {
        ReportViewer MyReportViewer = new ReportViewer();
        MyReportViewer.ProcessingMode = ProcessingMode.Remote;
        IReportServerCredentials ssrscredentials = new CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
        ServerReport serverReport = MyReportViewer.ServerReport;
        MyReportViewer.ServerReport.ReportServerCredentials = ssrscredentials;
        ReportParameter prm = new ReportParameter("SELLERUSERID", UserId);
        ReportParameter prm2 = new ReportParameter("MONTH", Month.ToString());
        serverReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ServerReport"]);
        serverReport.ReportPath = "/Invoice_Report/rpt_GSTR3B";
        MyReportViewer.ServerReport.SetParameters(prm);
        MyReportViewer.ServerReport.SetParameters(prm2);
        CreateExcelFromServer(UserId, MyReportViewer);
    }
    
   
    public static void ExcelDownloadGstr_3B(string UserId, int Month)
    {
        try
        {
            ReportViewer MyReportViewer = new ReportViewer();
            MyReportViewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials ssrscredentials = new CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
            ServerReport serverReport = MyReportViewer.ServerReport;
            MyReportViewer.ServerReport.ReportServerCredentials = ssrscredentials;
            ReportParameter prm = new ReportParameter("SELLERUSERID", UserId);
            ReportParameter prm2 = new ReportParameter("Month", Month.ToString());
            serverReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ServerReport"]);
            serverReport.ReportPath = "/Invoice_Report/rpt_GSTR3B_OFFLINE";
            MyReportViewer.ServerReport.SetParameters(prm);
            MyReportViewer.ServerReport.SetParameters(prm2);
            CreateExcelFromServer(UserId, MyReportViewer);


        }
        catch (Exception ex)
        {

        }
    }
    public static void CreateExcelFromServer(string FileName, ReportViewer reportViewer)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        byte[] bytes = reportViewer.ServerReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = mimeType;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
        HttpContext.Current.Response.BinaryWrite(bytes); // create the file
        HttpContext.Current.Response.Flush(); // send it to the client to download
    } 
  
    public static void VIEWPDFSERVER(string invoiceNo, string invoiceId)
    {
        try
        {
            ReportViewer MyReportViewer = new ReportViewer();
            // viewer.ProcessingMode = ProcessingMode.Remote;
            //// viewer.ServerReport.ReportServerUrl = new Uri("http://server/reportserver");
            // viewer.ServerReport.ReportServerUrl = new Uri("http://103.25.172.153/ReportServer");
            // //  viewer.ServerReport.ReportPath = "/Test/Report1";"/InvoiceReport/rptInvoice";
            //   viewer.ServerReport.ReportPath = "/InvoiceReport/rptInvoice";
            // viewer.ServerReport.Refresh();
            // CreatePDFFromServer(invoiceNo, viewer);

            MyReportViewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials ssrscredentials = new CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
            ServerReport serverReport = MyReportViewer.ServerReport;
            MyReportViewer.ServerReport.ReportServerCredentials = ssrscredentials;
            serverReport.ReportServerUrl = new Uri("http://103.25.172.153/reportserver");
            serverReport.ReportPath = "/Invoice_Report/rptInvoice";
            serverReport.Refresh();
            printPdf(invoiceNo, MyReportViewer);
        }
        catch (Exception ex)
        {
            //TODO

        }
    }
    public static void DownloadPDFSERVER(string invoiceId)
    {
        try
        {
            ReportViewer MyReportViewer = new ReportViewer();
           // viewer.ProcessingMode = ProcessingMode.Remote;
           //// viewer.ServerReport.ReportServerUrl = new Uri("http://server/reportserver");
           // viewer.ServerReport.ReportServerUrl = new Uri("http://103.25.172.153/ReportServer");
           // //  viewer.ServerReport.ReportPath = "/Test/Report1";"/InvoiceReport/rptInvoice";
           //   viewer.ServerReport.ReportPath = "/InvoiceReport/rptInvoice";
           // viewer.ServerReport.Refresh();
           // CreatePDFFromServer(invoiceNo, viewer);

            MyReportViewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials ssrscredentials = new CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
            ServerReport serverReport = MyReportViewer.ServerReport;
            MyReportViewer.ServerReport.ReportServerCredentials = ssrscredentials;
            ReportParameter prm = new ReportParameter("INVOICEID", invoiceId);
            serverReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ServerReport"]);
            //serverReport.ReportPath = "/Invoice_Report/rpt_GSTR3B_OFFLINE";
            //serverReport.ReportServerUrl = new Uri("http://localhost/ReportServer");
            serverReport.ReportPath = "/Invoice_Report/rptInvoice";
            MyReportViewer.ServerReport.SetParameters(prm);
            CreatePDFFromServer(invoiceId, MyReportViewer);
        }
        catch (Exception ex)
        {
            //TODO

        }
    }
    public class CustomSSRSCredentials : IReportServerCredentials
    {
        private string _SSRSUserName;
        private string _SSRSPassWord;
        private string _DomainName;

        public CustomSSRSCredentials(string UserName, string PassWord, string DomainName)
        {
            _SSRSUserName = UserName;
            _SSRSPassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_SSRSUserName, _SSRSPassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
    public static void DownloadPDFSERVERTaxInvoice(string invoiceId)
    {
        try
        {
            ReportViewer viewer = new ReportViewer();
            ReportParameter prm = new ReportParameter("INVOICEID", invoiceId);
            viewer.ProcessingMode = ProcessingMode.Remote;
            viewer.ServerReport.ReportServerUrl = new Uri("http://server/reportserver");
            viewer.ServerReport.ReportPath = "/Invoice_Report/rptInvoice";
            viewer.ServerReport.SetParameters(prm);
            viewer.ServerReport.Refresh();
        //    CreatePDFFromServer(invoiceId, viewer);
            AttachPDF(invoiceId, viewer);
        }
        catch (Exception ex)
        {
            //TODO
        }
    }
    public static void CreatePDFFromServer(string FileName, ReportViewer reportViewer)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = mimeType;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
        HttpContext.Current.Response.BinaryWrite(bytes); // create the file
        HttpContext.Current.Response.Flush(); // send it to the client to download
    }

    public static void printPdf(string FileName, ReportViewer reportViewer)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        string bytestring = Convert.ToBase64String(bytes);


        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ContentType = mimeType;
        //HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
        //HttpContext.Current.Response.BinaryWrite(bytes); // create the file
        //HttpContext.Current.Response.Flush(); // send it to the client to download
    }
    
    public static void AttachPDF(string invoiceName, ReportViewer reportViewer)
    {
       
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        List<string> AttachmentFiles = new  List<string>();
        byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        Int64 invoiceid =Convert.ToInt64(invoiceName);

     //   Int64 invoiceid = Convert.ToInt64(invoiceName);
    //    SendHTMLMails(invoiceid);
        IdentityMessage message = new IdentityMessage();
        using (MailMessage mailMessage = new MailMessage())
        {

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.Subject = "Invoice Attachment";
            mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress("mailtosatyayadav@gmail.com"));
            //mailMessage.Bcc.Add(new MailAddress("aashishshar@gmail.com"));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }

    public static void SendHTMLMails(Int64 invoiceid)
    {
        UnitOfWork unitofwork = new UnitOfWork();
        cls_Message message = new cls_Message();
        EmailService email = new EmailService();
        IdentityMessage msg = new IdentityMessage();
        var invdtls=  unitofwork.InvoiceRepository.Filter(f => f.InvoiceID == invoiceid).FirstOrDefault();
   //     msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));
        string mailBody = "";
        msg.Body = mailBody;   //"hi body.....";
        msg.Destination = invdtls.AspNetUser.Email;  //Common.UserProfile.Email;//sellerEmail;
        msg.Subject = "Invoice Attachment";
        email.Send(msg);
    }



    public void SendReportMail(clsMessageAttribute mailData, string mailString, string sellerEmail)
    {
        cls_Message message = new cls_Message();
        EmailService email = new EmailService();
        IdentityMessage msg = new IdentityMessage();
        Dictionary<string, string> replaceItem = new Dictionary<string, string>();
        replaceItem.Add("@User", mailData.UserName);
        string mailBody = message.GetMessage(EnumConstants.Message.UploadInvoice, replaceItem);
        msg.Body = mailBody;   //"hi body.....";
        msg.Destination = Common.UserProfile.Email;//sellerEmail;
        msg.Subject = "Invoices Report";
        email.Send(msg);
    }
}


public static class DateTimeDayOfMonthExtensions
{
    public static DateTime FirstDayOfMonth(this DateTime value)
    {
        return new DateTime(value.Year, value.Month, 1);
    }

    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, NextYear.ToString().Length - 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }

    public static int DaysInMonth(this DateTime value)
    {
        return DateTime.DaysInMonth(value.Year, value.Month);
    }

    public static DateTime LastDayOfMonth(this DateTime value)
    {
        return new DateTime(value.Year, value.Month, value.DaysInMonth());
    }

    public static string GetMonthName(int month, bool abbreviate, IFormatProvider provider)
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(provider);
        if (abbreviate) return info.GetAbbreviatedMonthName(month);
        return info.GetMonthName(month);
    }

    public static string GetMonthName(int month, bool abbreviate)
    {
        return GetMonthName(month, abbreviate, null);
    }

    public static string GetMonthName(int month, IFormatProvider provider)
    {
        return GetMonthName(month, false, provider);
    }

    public static string GetMonthName(this int month)
    {
        return GetMonthName(month, false, null);
    }
}
public class DateTimeAgo
{
    static DateTimeAgo()
    {
    }

    public static string GetFormatDate(object date)
    {
        if (date != null)
        {
            if (string.IsNullOrEmpty(date.ToString()))
                return "-";
            DateTime dt = Convert.ToDateTime(date);
            return string.Format(dt.ToString("MMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US")) + ".");
        }
        else
        {
            return "-";
        }
    }

    public static string TimeAgo(string date)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(date);

            if (dt > DateTime.Now)
            {
                return "sometime from now";
            }

            TimeSpan span = DateTime.Now - dt;

            if (span.Days > 1)
            {
                return string.Format(dt.ToString("MMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US")) + ".");//,CultureInfo.CreateSpecificCulture("en-US")));//"{0:MM/dd/yyyy}", dt);
                //return string.Format(dt.ToString("ddd d MMM",CultureInfo.CreateSpecificCulture("en-US")));//"{0:MM/dd/yyyy}", dt);
            }

            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} ago", years, years == 1 ? "year" : "years");
            }

            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} ago", months, months == 1 ? "month" : "months");
            }

            if (span.Days > 0)
                return String.Format("{0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");

            if (span.Hours > 0)
                return String.Format("{0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");

            if (span.Minutes > 0)
                return String.Format("{0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");

            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);

            if (span.Seconds <= 5)
                return "just now";

            return string.Empty;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public static List<string> GetTimeIntervals()
    {
        List<string> timeIntervals = new List<string>();
        TimeSpan startTime = new TimeSpan(0, 0, 0);
        DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
        for (int i = 0; i < 48; i++)
        {
            int minutesToBeAdded = 60 * i;      // Increasing minutes by 30 minutes interval
            TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
            TimeSpan t = startTime.Add(timeToBeAdded);
            DateTime result = startDate + t;
            timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
        }
        return timeIntervals;
    }
}
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumDescriptionAttribute : Attribute
{
    private string description;

    /// <span class="code-SummaryComment"><summary></span>
    /// Gets the description stored in this attribute.
    /// <span class="code-SummaryComment"></summary></span>
    /// <span class="code-SummaryComment"><value>The description stored in the attribute.</value></span>
    public string Description
    {
        get
        {
            return this.description;
        }
    }

    /// <span class="code-SummaryComment"><summary></span>
    /// Initializes a new instance of the
    /// <span class="code-SummaryComment"><see cref="EnumDescriptionAttribute"/> class.</span>
    /// <span class="code-SummaryComment"></summary></span>
    /// <span class="code-SummaryComment"><param name="description">The description to store in this attribute.</span>
    /// <span class="code-SummaryComment"></param></span>
    public EnumDescriptionAttribute(string description)
        : base()
    {
        this.description = description;
    }
}
public static class EnumHelper
{
    /// <span class="code-SummaryComment"><summary></span>
    /// Gets the <span class="code-SummaryComment"><see cref="DescriptionAttribute" /> of an <see cref="Enum" /> </span>
    /// type value.
    /// <span class="code-SummaryComment"></summary></span>
    /// <span class="code-SummaryComment"><param name="value">The <see cref="Enum" /> type value.</param></span>
    /// <span class="code-SummaryComment"><returns>A string containing the text of the</span>
    /// <span class="code-SummaryComment"><see cref="DescriptionAttribute"/>.</returns></span>
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        string description = value.ToString();
        FieldInfo fieldInfo = value.GetType().GetField(description);
        EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
        //Type typeOfT = typeof(T);
        //T obj = Activator.CreateInstance<T>();

        //PropertyInfo[] propInfo = value.GetType().GetProperties();

        //foreach (PropertyInfo property in propInfo)
        //{
        //          var field = (value.GetType())Attribute.GetCustomAttribute(property, typeof(value.GetType()), false);
        //    //if(field != null) {
        //    //}
        //}

        var Type = value.GetType();
        var Name = Enum.GetName(Type, value);
        if (Name == null)
            return null;

        var Field = Type.GetField(Name);
        if (Field == null)
            return null;

        var Attr = Field.GetCustomAttribute<EnumDescriptionAttribute>();
        if (Attr == null)
            return null;

        //return Attr.Description

        if (attributes != null && attributes.Length > 0)
        {
            description = attributes[0].Description;
        }
        return description;
    }

    /// <span class="code-SummaryComment"><summary></span>
    /// Converts the <span class="code-SummaryComment"><see cref="Enum" /> type to an <see cref="IList" /> </span>
    /// compatible object.
    /// <span class="code-SummaryComment"></summary></span>
    /// <span class="code-SummaryComment"><param name="type">The <see cref="Enum"/> type.</param></span>
    /// <span class="code-SummaryComment"><returns>An <see cref="IList"/> containing the enumerated</span>
    /// type value and description.<span class="code-SummaryComment"></returns></span>
    public static IList ToList(this Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException("type");
        }

        ArrayList list = new ArrayList();
        Array enumValues = Enum.GetValues(type);

        foreach (Enum value in enumValues)
        {
            list.Add(new KeyValuePair<Enum, string>(value, value.ToDescription()));
        }

        return list;
    }
}
public static class Enumeration
{
    public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
    {
        var enumerationType = typeof(TEnum);

        if (!enumerationType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumerationType))
        {
            var name = Enum.GetName(enumerationType, value);
            dictionary.Add(value, name);
        }

        return dictionary;
    }
    public static string GetDescription(Enum value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        string description = value.ToString();
        FieldInfo fieldInfo = value.GetType().GetField(description);
        EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])
         fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            description = attributes[0].Description;
        }
        return description;
    }

    /// <span class="code-SummaryComment"><summary></span>
    /// Converts the <span class="code-SummaryComment"><see cref="Enum" /> type to an <see cref="IList" /> </span>
    /// compatible object.
    /// <span class="code-SummaryComment"></summary></span>
    /// <span class="code-SummaryComment"><param name="type">The <see cref="Enum"/> type.</param></span>
    /// <span class="code-SummaryComment"><returns>An <see cref="IList"/> containing the enumerated</span>
    /// type value and description.<span class="code-SummaryComment"></returns></span>
    public static IList ToList(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException("type");
        }

        ArrayList list = new ArrayList();
        Array enumValues = Enum.GetValues(type);

        foreach (Enum value in enumValues)
        {
            list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
        }

        return list;
    }
    public static string ToDescription(this Enum value)
    {
        DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : value.ToString();
    }
}
public class Common
{
    public static string IP 
    {
        get
        {
            return new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
        }
    }
    private static ApplicationUserManager _userManager;
    public static ApplicationUserManager UserManager
    {
        get
        {
            return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        set
        {
            _userManager = value;
        }
    }

    private static ApplicationUser _userProfile;
    public static ApplicationUser UserProfile
    {
        get
        {
            return _userProfile ?? LoggedInUserProfile();
        }        
    }

   
    private static ApplicationUser LoggedInUserProfile()
    {
        var userid = LoggedInUserID();
        var profile = UserManager.Users.Where(w => w.Id == userid).FirstOrDefault();
        return profile;

    }
    public static string LoggedInUserID()
    {
        var userId = HttpContext.Current.User.Identity.GetUserId();
        if (userId != null)
            return userId;
        else
            return string.Empty;
    }
    public static bool IsAuthenticate()
    {
        var status = HttpContext.Current.User.Identity.IsAuthenticated;
        return status; 
    }

    public static bool IsAdmin()
    {
        var userId = HttpContext.Current.User.Identity.GetUserId();
        if (userId != null)
            return UserManager.IsInRole(userId, EnumConstants.RoleName.Admin.ToString());
        else
            return false;
    }

    public static string GetCurrentGSTIN()
    {
        UnitOfWork unitofWork = new UnitOfWork();
        var userid =LoggedInUserID();
        var gstin = unitofWork.AspnetRepository.Filter(x => x.Id == userid).Select(x => x.GSTNNo).FirstOrDefault();
        return gstin;
    }
    public static bool IsTaxConsultant()
    {
        var userId = HttpContext.Current.User.Identity.GetUserId();
        if (userId != null)
            return UserManager.IsInRole(userId, EnumConstants.RoleName.TaxConsultant.ToString());
        else
            return false;
    }

    public static bool IsUser()
    {
        var userId = HttpContext.Current.User.Identity.GetUserId();
        if (userId != null)
            return UserManager.IsInRole(userId, EnumConstants.RoleName.User.ToString());
        else
            return false;
    }

    public static decimal CalculateTotal(decimal qty, decimal perUnitRate)
    {
        return qty * perUnitRate;
    }

    public static string GetStateName(string stateCode)
    {
        UnitOfWork unitofWork = new UnitOfWork();
        var stateName = unitofWork.StateRepository.Find(f => f.StateCode == stateCode).StateName;
        return stateName;
    }

    public static decimal TaxCalculate(bool isInterState, bool isExportedInvoice,bool isImport, bool isUTState, bool jobWork, bool isEcom, bool isUN, EnumConstants.TaxType taxType, decimal taxableValue, decimal taxRate)
    {
        decimal tax = 0;
        if (taxType == EnumConstants.TaxType.IGST)
        {
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                { tax = 0; }
                else
                {
                    if (isInterState)
                    {
                        tax = Calculate.CalculateTax(taxableValue, taxRate);
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = Calculate.CalculateTax(taxableValue, taxRate);
                        }
                        else
                        {
                            if (isImport)
                            {
                                tax = Calculate.CalculateTax(taxableValue, taxRate);
                            }
                            else
                            {
                                tax = 0;
                            }
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.CGST)
        {
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                {
                    tax = Calculate.CalculateTax(taxableValue, taxRate);
                }//(isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CGST.Value))))
                else
                {
                    if (isInterState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = Calculate.CalculateTax(taxableValue, taxRate);
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.SGST)
        {
            //isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.SGST.Value)))),
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                {
                    tax = 0;
                }
                else
                {
                    if (isInterState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = Calculate.CalculateTax(taxableValue, taxRate);
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.UTGST)
        {// isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.UGST.Value) : 0)),
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isExportedInvoice)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState)
                    {
                        tax = Calculate.CalculateTax(taxableValue, taxRate);
                    }
                    else
                    {
                        tax = 0;
                    }
                }
            }
        }
        else
        {
            tax = 0;
        }


        //if (taxType == EnumConstants.TaxType.IGST)
        //{
        //    if (isInterState || isExportedInvoice)
        //    {
        //        tax = Calculate.CalculateTax(taxableValue, taxRate);
        //    }
        //}
        //if (taxType == EnumConstants.TaxType.CGST || taxType == EnumConstants.TaxType.SGST)
        //{
        //    if (!isInterState || !isUTState)
        //    {
        //        tax = Calculate.CalculateTax(taxableValue, taxRate);
        //    }
        //}
        //if (taxType == EnumConstants.TaxType.UTGST)
        //{
        //    if (isUTState)
        //    {
        //        tax = Calculate.CalculateTax(taxableValue, taxRate);
        //    }
        //}
        
        //else if (jobWork)
        //{ tax = 0; }
        //else if (isEcom)
        //{ tax = 0; }
        //else if (isUN)
        //{ tax = 0; }
        //else
        //    tax = 0;

        return tax;
    }

    public static decimal TaxRate(bool isInterState, bool isExportedInvoice, bool isImport, bool isUTState, bool jobWork, bool isEcom, bool isUN, EnumConstants.TaxType taxType, decimal taxableValue, decimal taxRate)
    {
        decimal tax = 0;
        if (taxType == EnumConstants.TaxType.IGST)
        {
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                { tax = 0; }
                else
                {
                    if (isInterState)
                    {
                        tax = taxRate;
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = taxRate;
                        }
                        else
                        {
                            if (isImport)
                            {
                                tax = taxRate;
                            }
                            else
                            {
                                tax = 0;
                            }
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.CGST)
        {
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                {
                    tax = taxRate;
                }//(isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CGST.Value))))
                else
                {
                    if (isInterState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = taxRate;
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.SGST)
        {
            //isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.SGST.Value)))),
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isUTState)
                {
                    tax = 0;
                }
                else
                {
                    if (isInterState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isExportedInvoice)
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = taxRate;
                        }
                    }
                }
            }
        }
        else if (taxType == EnumConstants.TaxType.UTGST)
        {// isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.UGST.Value) : 0)),
            if (jobWork)
            {
                tax = 0;
            }
            else
            {
                if (isExportedInvoice)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState)
                    {
                        tax = taxRate;
                    }
                    else
                    {
                        tax = 0;
                    }
                }
            }
        }
        else
        {
            tax = 0;
        }
        return tax;
    }

    public static decimal CalculateTaxableValue(decimal total, decimal discount)
    {
        decimal taxableValue;
        if (discount > 0.0m)
        {
            taxableValue = total - (total * (discount / 100));
            taxableValue = Math.Round(taxableValue, 2);
        }
        else
            taxableValue = total;
        return taxableValue;
    }
    //public static string GetInvoiceNumber(int currentNumber)
    //{
    //    Int32 nextnumber = currentNumber + (currentNumber % 3) + 5;
    //    Random _random = new Random();
    //    //you can skip the below 2 lines if you don't want alpha numeric
    //    int num = _random.Next(0, 26); // Zero to 25
    //    char let = (char)('S' + num);
    //    return nextnumber + let.ToString();
    //}

    public static string RCMTextBind(object invoiceType)
    {
        string content = string.Empty;
        EnumConstants.InvoiceSpecialCondition InvoiceType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType != null ? invoiceType.ToString() : "-1");
        switch (InvoiceType)
        {
            case EnumConstants.InvoiceSpecialCondition.RegularRCM:
                content = "(Reverse Charges)";
                break;
            default:
                content = "";
                break;
        }
        return content;
    }
    public static string GetJobType(string JobType)
    {
        string Job;
        EnumConstants.JobType JobTYPE = (EnumConstants.JobType)Enum.Parse(typeof(EnumConstants.JobType), JobType);
        switch (JobTYPE)
        {
            case EnumConstants.JobType.Offline:
                Job = "<span class='label label-primary'>" + JobTYPE.ToDescription() + "</span>";
                break;
            case EnumConstants.JobType.Services:
                Job = "<span class='label label-warning'>" + JobTYPE.ToDescription() + "</span>";
                break;
            default:
                Job = "<span class='label label-danger'>NA</span>";
                break;
        }
        return Job;
    }

    public static string InvoiceTypeColor(string invoiceType,string invoice)
    {
        string InvoiceType;// = Convert.ToInt32(invoicID);
        EnumConstants.InvoiceSpecialCondition invType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType);
        EnumConstants.InvoiceType Inv = (EnumConstants.InvoiceType)Enum.Parse(typeof(EnumConstants.InvoiceType), invoice);
        switch (invType)
        {
            case EnumConstants.InvoiceSpecialCondition.Import:
                InvoiceType = "<span class='label bg-blue'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.Export:
                InvoiceType = "<span class='label bg-green'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.JobWork:
                InvoiceType = "<span class='label label-primary'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.SEZDeveloper:
                InvoiceType = "<span class='label bg-yellow'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.SEZUnit:
                InvoiceType = "<span class='label bg-teal'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.ECommerce:
                InvoiceType = "<span class='label bg-red'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.DeemedExport:
                InvoiceType = "<span class='label bg-orange'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.Regular:
                InvoiceType = "<span class='label bg-purple'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.ReverseCharges:
                InvoiceType = "<span class='label bg-black'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.Advance:
                InvoiceType = "<span class='label bg-blue'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.B2CL:
                InvoiceType = "<span class='label label-danger'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.B2CS:
                InvoiceType = "<span class='label bg-red'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.RegularRCM:
                InvoiceType = "<span class='label bg-green'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice:
                InvoiceType = "<span class='label bg-gray'>" + Inv + " - " + invType.ToDescription() + "</span>";
                break;
            default:
                InvoiceType ="<span class='label label-danger'>NA</span>";;
                break;
        }

        return InvoiceType;

    }
    public static string InvoiceAuditTrailStatus(string invoiceAction, string invoiceAudit)
    {
        string InvoiceType;// = Convert.ToInt32(invoicID);
        EnumConstants.InvoiceActionAuditTrail invAction = (EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), invoiceAction);
        EnumConstants.InvoiceAuditTrailSatus invAudit = (EnumConstants.InvoiceAuditTrailSatus)Enum.Parse(typeof(EnumConstants.InvoiceAuditTrailSatus), invoiceAudit);
        switch (invAction)
        {
            case EnumConstants.InvoiceActionAuditTrail.NA:
                InvoiceType = "<span class='label label-default'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Accept:
                InvoiceType = "<span class='label bg-green'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Delete:
                InvoiceType = "<span class='label label-danger'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Reject:
                InvoiceType = "<span class='label label-warning'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Pending:
                InvoiceType = "<span class='label label-info'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Modify:
                InvoiceType = "<span class='label label-danger'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                break;
            //case EnumConstants.InvoiceActionAuditTrail.FileGSTR3:
            //    InvoiceType = "<span class='label bg-red'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
            //    break;
            default:
                InvoiceType = "<span class='label label-danger'>-</span>"; ;
                break;
        }

        return InvoiceType;

    }
    public static string InvoiceAuditTrailSatus(string invoiceAudit)
    {
        string InvoiceType;// = Convert.ToInt32(invoicID);
        //EnumConstants.InvoiceActionAuditTrail invAction = (EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), invoiceAction);
        EnumConstants.InvoiceAuditTrailSatus invAudit = (EnumConstants.InvoiceAuditTrailSatus)Enum.Parse(typeof(EnumConstants.InvoiceAuditTrailSatus), invoiceAudit);
        switch (invAudit)
        {
            case EnumConstants.InvoiceAuditTrailSatus.Upload:
                InvoiceType = "<span class='label label-default'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.FileGSTR1:
                InvoiceType = "<span class='label bg-green'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A:
                InvoiceType = "<span class='label bg-green'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.FileGSTR2:
                InvoiceType = "<span class='label bg-green'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.Import2A:
                InvoiceType = "<span class='label label-primary'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.Import1A:
                InvoiceType = "<span class='label label-primary'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.Pending:
                InvoiceType = "<span class='label label-danger'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.ImportGSTR3:
                InvoiceType = "<span class='label label-primary'>" + invAudit.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceAuditTrailSatus.FileGSTR3:
                InvoiceType = "<span class='labelbg-green'>" + invAudit.ToDescription() + "</span>";
                break;
            default:
                InvoiceType = "<span class='label label-danger'>NA</span>"; ;
                break;
        }

        return InvoiceType;

    }

    public static string InvoiceAuditTrailAction(object invoiceAction)
    {
        if (invoiceAction == null)
        {
            invoiceAction = "0";
        }
        string InvoiceType;// = Convert.ToInt32(invoicID);
        EnumConstants.InvoiceActionAuditTrail invAction = (EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), invoiceAction.ToString());
        // EnumConstants.InvoiceAuditTrailSatus invAudit = (EnumConstants.InvoiceAuditTrailSatus)Enum.Parse(typeof(EnumConstants.InvoiceAuditTrailSatus), invoiceAudit);
        switch (invAction)
        {
            case EnumConstants.InvoiceActionAuditTrail.NA:
                InvoiceType = "<span class='label label-default'>" + invAction.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Accept:
                InvoiceType = "<span class='label bg-green'>" + invAction.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Delete:
                InvoiceType = "<span class='label label-danger'>" + invAction.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Reject:
                InvoiceType = "<span class='label label-warning'>" + invAction.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Pending:
                InvoiceType = "<span class='label label-info'>" + invAction.ToDescription() + "</span>";
                break;
            case EnumConstants.InvoiceActionAuditTrail.Modify:
                InvoiceType = "<span class='label label-danger'>" + invAction.ToDescription() + "</span>";
                break;
            default:
                InvoiceType = "<span class='label label-danger'>NA</span>"; ;
                break;
        }
        return InvoiceType;
    }

    public static string UserType(string userType)
    {
        string UserType;// = Convert.ToInt32(invoicID);
        //EnumConstants.InvoiceActionAuditTrail invAction = (EnumConstants.InvoiceActionAuditTrail)Enum.Parse(typeof(EnumConstants.InvoiceActionAuditTrail), invoiceAction);
        EnumConstants.UserType user = (EnumConstants.UserType)Enum.Parse(typeof(EnumConstants.UserType), userType);
        switch (user)
        {
            case EnumConstants.UserType.RegularDealerRD:
                UserType = "<span class='label label-info'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.CompoundingDealerCD:
                UserType = "<span class='label bg-green'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.EComOperator:
                UserType = "<span class='label bg-blue'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.Exporter:
                UserType = "<span class='label bg-purple'>" +  user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.Importer:
                UserType = "<span class='label label-primary'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.InputServiceDistributorISD:
                UserType = "<span class='label label-danger'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.NonGSTRegisteredUser:
                UserType = "<span class='label bg-blue'>" + user.ToDescription() + "</span>";
                break;
                 case EnumConstants.UserType.PSU:
                UserType = "<span class='label bg-yellow'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.UN:
                UserType = "<span class='label bg-black'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.Reciever:
                UserType = "<span class='label label-success'>" + user.ToDescription() + "</span>";
                break;
            case EnumConstants.UserType.Consignee:
                UserType = "<span class='label label-danger'>" + user.ToDescription() + "</span>";
                break;
          
            default:
                UserType  = "<span class='label label-danger'>NA</span>"; ;
                break;
        }

        return UserType; 

    }

    // 24 = 192 bits
    private const int SaltByteSize = 24;
    private const int HashByteSize = 24;
    private const int HasingIterationsCount = 10101;


    public static string HashPassword(string password)
    {
        // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing

        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(HashByteSize);
        }
        byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
        Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
        Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        byte[] _passwordHashBytes;

        int _arrayLen = (SaltByteSize + HashByteSize) + 1;

        if (hashedPassword == null)
        {
            return false;
        }

        if (password == null)
        {
            throw new ArgumentNullException("password");
        }

        byte[] src = Convert.FromBase64String(hashedPassword);

        if ((src.Length != _arrayLen) || (src[0] != 0))
        {
            return false;
        }

        byte[] _currentSaltBytes = new byte[SaltByteSize];
        Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

        byte[] _currentHashBytes = new byte[HashByteSize];
        Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCount))
        {
            _passwordHashBytes = bytes.GetBytes(SaltByteSize);
        }

        return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

    }

    private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
    {
        int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
        var xor = firstHash.Length ^ secondHash.Length;
        for (int i = 0; i < _minHashLength; i++)
            xor |= firstHash[i] ^ secondHash[i];
        return 0 == xor;
    }

    public static string GetImageFromByte(byte[] imageByte)
    {
        string base64String = Convert.ToBase64String(imageByte, 0, imageByte.Length);
        string url = "data:image/png;base64," + base64String;

        return url;
    }

    public static byte[] GetImageByte(FileUpload fileUploadControlId)
    {
        Byte[] ImgByte = null;
        string filePath = fileUploadControlId.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;
        //Set the contenttype based on File Extension
        switch (ext)
        {
            case ".jpg":
            case ".JPG":
                contenttype = "image/jpg";
                break;
            case ".png":
            case ".PNG":
                contenttype = "image/png";
                break;
            case ".jpeg":
            case ".JPEG":
                contenttype = "image/jpeg";
                break;
        }
        if (contenttype != String.Empty)
        {
            Stream fs = fileUploadControlId.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fileUploadControlId.PostedFile.InputStream);
            decimal size = Math.Round(((decimal)fileUploadControlId.PostedFile.ContentLength / (decimal)1024), 2);
            //if (size > 2048)
            //{
            //    uc_sucess.ErrorMessage = "Size of the image to be uploaded cannot exceed two mb.";
            //    return;
            //}
            ImgByte = bytes;
        }
        return ImgByte;
    }

    ///// <summary>
    ///// Has Password
    ///// </summary>
    ///// <param name="password"></param>
    ///// <returns></returns>
    //public static string HashPassword(string password)
    //{
    //    byte[] salt;
    //    byte[] buffer2;
    //    if (password == null)
    //    {
    //        throw new ArgumentNullException("password");
    //    }
    //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
    //    {
    //        salt = bytes.Salt;
    //        buffer2 = bytes.GetBytes(0x20);
    //    }
    //    byte[] dst = new byte[0x31];
    //    Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
    //    Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
    //    return Convert.ToBase64String(dst);
    //}

    //public static bool VerifyHashedPassword(string hashedPassword, string password)
    //{
    //    byte[] buffer4;
    //    if (hashedPassword == null)
    //    {
    //        return false;
    //    }
    //    if (password == null)
    //    {
    //        throw new ArgumentNullException("password");
    //    }
    //    byte[] src = Convert.FromBase64String(hashedPassword);
    //    if ((src.Length != 0x31) || (src[0] != 0))
    //    {
    //        return false;
    //    }
    //    byte[] dst = new byte[0x10];
    //    Buffer.BlockCopy(src, 1, dst, 0, 0x10);
    //    byte[] buffer3 = new byte[0x20];
    //    Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
    //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
    //    {
    //        buffer4 = bytes.GetBytes(0x20);
    //    }
    //    return ByteArraysEqual(buffer3, buffer4);
    //}

    public static bool IsLoggedInUserRole(string Role)
    {
        return UserManager.IsInRole(LoggedInUserID(), Role);
    }
}

public static class Crypto
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256/8; // 256 bits
        private const int SaltSize = 128/8; // 128 bits

        /* =======================
         * HASHED PASSWORD FORMATS
         * =======================
         * 
         * Version 0:
         * PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
         * (See also: SDL crypto guidelines v5.1, Part III)
         * Format: { 0x00, salt, subkey }
         */

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        // hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input)
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) text hash.

            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
public static class ExtensionMethod
{
    //public static EntityCollection<T> ToEntityCollection<T>(this List<T> list) where T : class
    //{
    //    EntityCollection<T> entityCollection = new EntityCollection<T>();
    //    list.ForEach(entityCollection.Add);
    //    return entityCollection;
    //}
    public enum DateTimeFormat
    {
        Date, // 25 Apr 2014
        DateWithDayPrefix, // Wed, 16 Apr 2014 
        DateWithFullDayPrefix, // Saturday, 12 Apr 2014
        DateWithTime, // 12 Apr 2014 01:34 PM
        DateTimeStamp, // 12 Apr 2014 13:34
        DateJson, // 2014-04-12
        DateTimeJson, // 2014-04-12 13:34:20
        DateLocal // 2014/04/12
    }

    /// <summary>
    /// Returns formatted date string for display purpose
    /// </summary>
    public static string DisplayDate(this string date)
    {
        return DisplayDate(date, DateTimeFormat.Date);
    }

    /// <summary>
    /// Returns formatted date string for display purpose
    /// </summary>
    public static string DisplayDate(this string date, DateTimeFormat format)
    {
        switch (format)
        {
            case DateTimeFormat.DateWithDayPrefix:
                return string.Format("{0:ddd, dd MMM yyyy}", date);

            case DateTimeFormat.DateWithFullDayPrefix:
                return string.Format("{0:dddd, dd MMM yyyy}", date);

            case DateTimeFormat.DateWithTime:
                return string.Format("{0:dd MMM yyyy h:mm tt}", date);

            case DateTimeFormat.DateTimeStamp:
                return string.Format("{0:dd MMM yyyy HH:mm}", date);

            case DateTimeFormat.DateJson:
                return string.Format("{0:yyyy-MM-dd}", date);

            case DateTimeFormat.DateTimeJson:
                return string.Format("{0:yyyy-MM-dd HH:mm:ss}", date);

            case DateTimeFormat.DateLocal:
                return string.Format("{0:yyyy/MM/dd}", date);

            case DateTimeFormat.Date:
            default:
                return string.Format("{0:dd MMM yyyy}", date);
        }
    }

    /// <summary>
    /// Returns formatted date string in Database timestamp format.
    /// </summary>
    public static string DBTimestamp(this DateTime date)
    {
        return string.Format("{0:yyyy-MM-dd hh:mm:ss.fff}", date);
    }

    public static DataTable AsDataTable<T>(this IEnumerable<T> list)
    where T : class
    {
        DataTable dtOutput = new DataTable("tblOutput");

        //if the list is empty, return empty data table
        if (list.Count() == 0)
            return dtOutput;

        //get the list of  public properties and add them as columns to the
        //output table           
        PropertyInfo[] properties = list.FirstOrDefault().GetType().
            GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo propertyInfo in properties)
            dtOutput.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);

        //populate rows
        DataRow dr;
        //iterate through all the objects in the list and add them
        //as rows to the table
        foreach (T t in list)
        {
            dr = dtOutput.NewRow();
            //iterate through all the properties of the current object
            //and set their values to data row
            foreach (PropertyInfo propertyInfo in properties)
            {
                dr[propertyInfo.Name] = propertyInfo.GetValue(t, null);
            }
            dtOutput.Rows.Add(dr);
        }
        return dtOutput;
    }


}
//For Authorized url
public class Authorize : Attribute
{
    string UserId = Common.LoggedInUserID();
    UnitOfWork unitOfWork = new UnitOfWork();

    public Authorize(string Roles)
    {
        try
        {
            string[] RoleArray = Roles.Split(',');
            bool NotAuthorized = true;
            string Role;
            foreach (string temp in RoleArray)
            {
                Role = temp.Trim();
                if (Common.IsLoggedInUserRole(Role))
                {
                    NotAuthorized = false;
                }
            }
            if (NotAuthorized)
                HttpContext.Current.Response.Redirect("~/NotAuthorized.aspx");
        }
        catch
        {
            HttpContext.Current.Response.Redirect("~/NotAuthorized.aspx");
        }
    }
}


public static class StringExtensions
{
    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with an elipses (...)
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>
    /// <returns></returns>
    public static string Truncate(this string input, int length)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return input.Length <= length ? input : input.Substring(0, length);
        //return Truncate(input, length, "...");
    }

    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with the given value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>
    /// <param name="suffix">The value to suffix the return value with (if truncation is performed)</param>
    /// <returns></returns>
    public static string Truncate(this string input, int length, string suffix)
    {
        if (input == null) return "";
        if (input.Length <= length) return input;

        if (suffix == null) suffix = "...";

        return input.Substring(0, length - suffix.Length) + suffix;
    }
    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with an elipses (...)
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>
    /// <returns></returns>
    //public static string Truncate(this string input, int length)
    //{
    //    return Truncate(input, length);
    //}

    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with an elipses (...)
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>
    /// <returns></returns>
    //public static string Truncate(string input, Int16 maxLength)
    //{
    //    return TruncateString(input, maxLength);
    //}

    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with the given value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>
    /// <param name="suffix">The value to suffix the return value with (if truncation is performed)</param>
    /// <returns></returns>
    //public static string TruncateSuffix(this string input, int length, string suffix)
    //{
    //    if (input == null) return "";
    //    if (input.Length <= length) return input;

    //    if (suffix == null) suffix = "...";

    //    return input.Substring(0, length - suffix.Length) + suffix;
    //}


    /// <summary>
    /// Returns the given string truncated to the specified length, suffixed with the given value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length">Maximum length of return string</param>    
    /// <returns></returns>  
    //public static string Truncate(this string value, Int16 maxLength)
    //{
    //    if (string.IsNullOrEmpty(value)) return value;
    //    return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    //}

    /// <summary>
    /// Splits a given string into an array based on character line breaks
    /// </summary>
    /// <param name="input"></param>
    /// <returns>String array, each containing one line</returns>
    public static string[] ToLineArray(this string input)
    {
        if (input == null) return new string[] { };
        return System.Text.RegularExpressions.Regex.Split(input, "\r\n");
    }

    /// <summary>
    /// Splits a given string into a strongly-typed list based on character line breaks
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Strongly-typed string list, each containing one line</returns>
    public static List<string> ToLineList(this string input)
    {
        List<string> output = new List<string>();
        output.AddRange(input.ToLineArray());
        return output;
    }

    /// <summary>
    /// Replaces line breaks with self-closing HTML 'br' tags
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ReplaceBreaksWithBR(this string input)
    {
        return string.Join("<br/>", input.ToLineArray());
    }

    /// <summary>
    /// Replaces any single apostrophes with two of the same
    /// </summary>
    /// <param name="input"></param>
    /// <returns>String</returns>
    public static string DoubleApostrophes(this string input)
    {
        return Regex.Replace(input, "'", "''");
    }

    /// <summary>
    /// Encodes the input string as HTML (converts special characters to entities)
    /// </summary>
    /// <param name="input"></param>
    /// <returns>HTML-encoded string</returns>
    public static string ToHTMLEncoded(this string input)
    {
        return HttpContext.Current.Server.HtmlEncode(input);
    }

    /// <summary>
    /// Encodes the input string as a URL (converts special characters to % codes)
    /// </summary>
    /// <param name="input"></param>
    /// <returns>URL-encoded string</returns>
    public static string ToURLEncoded(this string input)
    {
        return HttpContext.Current.Server.UrlEncode(input);
    }

    /// <summary>
    /// Decodes any HTML entities in the input string
    /// </summary>
    /// <param name="input"></param>
    /// <returns>String</returns>
    public static string FromHTMLEncoded(this string input)
    {
        return HttpContext.Current.Server.HtmlDecode(input);
    }

    /// <summary>
    /// Decodes any URL codes (% codes) in the input string
    /// </summary>
    /// <param name="input"></param>
    /// <returns>String</returns>
    public static string FromURLEncoded(this string input)
    {
        return HttpContext.Current.Server.UrlDecode(input);
    }

    /// <summary>
    /// Removes any HTML tags from the input string
    /// </summary>
    /// <param name="input"></param>
    /// <returns>String</returns>
    public static string StripHTML(this string input)
    {
        return Regex.Replace(input, @"<(style|script)[^<>]*>.*?</\1>|</?[a-z][a-z0-9]*[^<>]*>|<!--.*?-->", "");
    }
}

public static class DateTimeExtensions { }
public class EnumControl
{

    public EnumControl()
    {

        //
        // TODO: Add constructor logic here
        //
    }




    /// <summary>
    /// Dynamic Enum Bind in control
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ddl"></param>
    public static void BindEnumDropDown<T>(DropDownList ddl)
    {
        string[] enumNames = Enum.GetNames(typeof(T));
        foreach (string item in enumNames)
        {
            //get the enum item value
            int value = (int)Enum.Parse(typeof(T), item);
            ListItem listItem = new ListItem(item, value.ToString());
            ddl.Items.Add(listItem);
        }
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("[ Select ]", "0"));
    }

    public static void GetEnumDescriptions<T>(Control ctrl,bool isSelect)
    {
        var list = new List<KeyValuePair<Enum, string>>();
        
        foreach (Enum value in Enum.GetValues(typeof(T)))
        {
            string description = value.ToDescription();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            //var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).First();
            //if (attribute != null)
            //{
            //    description = (attribute as DescriptionAttribute).Description;
            //}
            list.Add(new KeyValuePair<Enum, string>(value, description));
        }
        if (ctrl is DropDownList)
        {
            DropDownList control = (DropDownList)ctrl;
            control.DataSource = list;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
            if (isSelect)
                control.Items.Insert(0, new ListItem("[ Select ]", "0"));
        }
        else if (ctrl is RadioButtonList)
        {

            RadioButtonList control = (RadioButtonList)ctrl;
            control.DataSource = list;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
        }
        else if (ctrl is Repeater)
        {
            Repeater control = (Repeater)ctrl;
            control.DataSource = list;
            control.DataBind();
        }
        else if (ctrl is DataList)
        {
            DataList control = (DataList)ctrl;
            control.DataSource = list;
            control.DataBind();
        }
        else if (ctrl is CheckBoxList)
        {
            CheckBoxList control = (CheckBoxList)ctrl;
            control.DataSource = list;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
        }
        else if (ctrl is BulletedList)
        {
            BulletedList control = (BulletedList)ctrl;
            control.DataSource = list;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
        }
    }
}


