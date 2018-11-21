using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using businessAccessLayer;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Text;
using System.Security.Cryptography;
using DataAccessLayer;
using BusinessLogic.Repositories;
using GST.Utility;
using Microsoft.AspNet.Identity;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR1APreviewB2B : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();

        //#region VARIABLE_DECLARETION
        //string PID = string.Empty;
        string SellerGSTIN = string.Empty;
        //ExcelDB excelDB = new ExcelDB();
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        string strReceiverGSTN = string.Empty;
        string strInvoiceNo = string.Empty;
        //string flag = string.Empty;
        string period = string.Empty;
        string month = string.Empty;
        string year = string.Empty;
        //#endregion

        //#region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var stratdt = new DateTime(now.Year, now.Month, 1);
            SellerGSTIN = Page.User.Identity.Name;
            if (!IsPostBack)
            {
                uc_Signatory.UserID = Common.LoggedInUserID();

               // BindAllGSTN1AList(MonthName);
                BindAllGSTN1AList((DateTime.Now.Month - 1));
            }
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
           
        }

        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {

            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            BindAllGSTN1AList(mName);
        }
       
        private void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }
        int MonthName;
        public void BindAllGSTN1AList(int MonthName)
        {
            //var month = uc_GSTR_Taxpayer.MonthName;
            GetGSTR1A_3A(MonthName);
            GetGSTR1A_3B(MonthName);
            GetGSTR1A_4A(MonthName);
            GetGSTR1A_4B(MonthName);
            GetGSTR1A_5(MonthName);
        }
        private void GetGSTR1A_3A(int MonthName)
        {
            try
            {
                var LoggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR1A_3A, _invoice.GetGSTR1A_3A(LoggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR1A_3B(int MonthName)
        {
            try
            {
                var LoggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR1A_3B, _invoice.GetGSTR1A_3B(LoggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
            cls_ErrorLog ob = new cls_ErrorLog();
            cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetGSTR1A_4A(int MonthName)
        {
            try
            {
                var LoggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR_1A_4A, _invoice.GetGSTR1A_4A(LoggedinUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void GetGSTR1A_4B(int MonthName)
        {
            try
            {
                var LoggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR_1A_4B, _invoice.GetGSTR1A_4B(LoggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetGSTR1A_5(int MonthName)
        {
            try
            {
                var LoggedinUserId = Common.LoggedInUserID();
                BindListView(lv_GSTR_1A_5, _invoice.GetGSTR1A_5(LoggedinUserId, MonthName));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


         GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        
         protected void lkvGSTR1A_Click(object sender, EventArgs e)
         {
             try
             {
                 UnitOfWork unitOfWork = new UnitOfWork();
                 int count = 0;
                 var loggedinUserId = Common.LoggedInUserID();
                 if (loggedinUserId != null)
                 {
                     DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                     DateTime lastDate = DateTime.Now.LastDayOfMonth();
                     byte FileGSTR2 = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR2);
                     byte Import1A = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Import1A);

                     var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && (f.SellerInvoiceAction != (byte)EnumConstants.InvoiceActionAuditTrail.NA || f.SellerInvoiceAction != null) && f.AuditTrailStatus == (byte)EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                     var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.AuditTrailStatus == Import1A && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                     List<clsMessageAttribute> invAttributes = new List<clsMessageAttribute>();
                     List<string> mailsToList = new List<string>();

                     List<string> invID = new List<string>();
                     foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in invoices)
                     {
                         clsMessageAttribute attribute = new clsMessageAttribute();
                         //var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceID);
                         string invoiceNo = inv.GST_TRN_INVOICE.InvoiceNo;

                         string id = inv.InvoiceID.ToString();
                         audittrail.InvoiceID = inv.InvoiceID;
                         audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR1A);
                         audittrail.UserIP = Common.IP;
                         audittrail.InvoiceAction = inv.InvoiceAction;
                         audittrail.SellerInvoiceAction = inv.ReceiverInvoiceAction == (byte)EnumConstants.InvoiceActionAuditTrail.Accept ? inv.ReceiverInvoiceAction : inv.SellerInvoiceAction;
                         audittrail.ReceiverInvoiceAction = inv.ReceiverInvoiceAction;
                         audittrail.SellerInvoiceActionDate = inv.SellerInvoiceActionDate;
                         audittrail.ReceiverInvoiceActionDate = inv.ReceiverInvoiceActionDate;
                         audittrail.CreatedDate = DateTime.Now;
                         audittrail.CreatedBy = loggedinUserId;
                         unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                         unitOfWork.Save();
                         count = count + 1;
                         if (!mailsToList.Contains(inv.GST_TRN_INVOICE.AspNetUser.Email))
                         {
                             mailsToList.Add(inv.GST_TRN_INVOICE.AspNetUser.Email);
                         }
                         attribute.UserName = inv.GST_TRN_INVOICE.AspNetUser.OrganizationName;
                         //attribute.MailsTo.Add();
                         attribute.InvoiceNo = invoiceNo;
                         attribute.InvoiceDate = DateTimeAgo.GetFormatDate(inv.GST_TRN_INVOICE.InvoiceDate);
                         attribute.InvoiceTotalAmount = inv.GST_TRN_INVOICE.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax).ToString();
                         invAttributes.Add(attribute);
                         invID.Add(id);
                     }

                     if (count > 0)
                     {

                         string mailString = string.Empty;
                         string sellerMail = string.Empty;
                         clsMessageAttribute mailData = new clsMessageAttribute();

                         foreach (clsMessageAttribute iId in invAttributes)
                         {
                             mailString += "<tr><td align='left' style='table-layout:auto'>" + iId.InvoiceNo.ToString() + "</td>";
                             mailString += "<td align='middle' style='table-layout:auto'>" + iId.InvoiceDate.ToString() + "</td>";
                             mailString += "<td align='right' style='table-layout:auto'>" + iId.InvoiceTotalAmount.ToString() + "</td></tr>";
                             mailData.UserName = iId.UserName;
                         }


                         this.Master.SuccessMessage = "Data Filed Successfully.";
                         SendHTMLMail(mailData, mailString, String.Join(";", mailsToList.ToArray()));
                         //  uc_sucess.Visible = true;
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                     }
                     else
                     {
                         this.Master.WarningMessage = "There is no data to File!.";
                         //uc_sucess.Visible = true;
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                     }
                 }
                 var month = uc_GSTR_Taxpayer.MonthName;
                 BindAllGSTN1AList(month);
             //   BindAllGSTN1AList((DateTime.Now.Month - 1));
             }
             catch (Exception ex)
             {
                 cls_ErrorLog ob = new cls_ErrorLog();
                 cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                 this.Master.ErrorMessage = ex.Message;
                 //uc_sucess.Visible = true;
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);

             }
         }
     

         public void SendHTMLMail(clsMessageAttribute mailData, string mailString, string sellerEmail)
        {
            try
            {
                cls_Message message = new cls_Message();
                EmailService email = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                Dictionary<string, string> replaceItem = new Dictionary<string, string>();
                replaceItem.Add("@User", mailData.UserName);
                replaceItem.Add("@InvoiceData", mailString);
                string mailBody = message.GetMessage(EnumConstants.Message.FileGSTR1A, replaceItem);
                msg.Body = mailBody;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "GSTR-1A filed";
                email.Send(msg);
            }
             catch(Exception ex)
            {
                 cls_ErrorLog ob = new cls_ErrorLog();
                 cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
             }
        }

    }
}