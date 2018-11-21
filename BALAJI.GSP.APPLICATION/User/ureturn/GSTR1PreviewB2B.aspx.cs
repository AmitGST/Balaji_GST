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
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using BusinessLogic.Repositories.GSTN;


namespace BALAJI.GSP.APPLICATION.User.ureturn
{
    public partial class GSTR1PreviewB2B : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        cls_Invoice _invoice = new cls_Invoice();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var loggedInUser = Common.LoggedInUserID();
                BindAllList(loggedInUser, DateTime.Now.Month - 1);
                //BindAllList(MonthName);
                //BindAllList((DateTime.Now.Month - 1));
                var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Enabled = false;
            }
            //amits
            uc_GSTR_Taxpayer.AddMoreClick += uc_GSTR_Taxpayer_AddMoreClick;
            uc_GSTNUsers.addInvoiceRedirect += uc_GSTNUsers_addInvoiceRedirect;
            uc_GSTNUsers.addInvoicechkRedirect += uc_GSTNUsers_addInvoicechkRedirect;
           

        }

       

        int MonthName;
        private void uc_GSTNUsers_addInvoicechkRedirect(object sender, EventArgs e)
        {
            var ddlvalue = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
            var chkvalue = uc_GSTNUsers.GetchkValue;
            // var ddlenable = uc_GSTNUsers.ddlGSTNUsers.Enabled = false;
            if (ddlvalue == 0)
            {
                //ddlenable = false;
                var loggedInUser = Common.LoggedInUserID();
                //for turnover start
                Label mpLabel = (Label)uc_GSTR_Taxpayer.FindControl("lblTurnoverAMT");
                int Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);
                List<string> userLists = new List<string>();
                if (uc_GSTNUsers.AssociatedUsersIds != null)
                {
                    userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
                }
                userLists.Add(loggedInUser);
                mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();

                //End

                BindAllList(loggedInUser, DateTime.Now.Month - 1);
                uc_invoiceMonth.BindInvoiceMonth();
            }
            else
            {
                var SellerUserId = uc_GSTNUsers.GetSellerProfile;
                BindAllList(SellerUserId, MonthName);
            }
        }
        //tax consultant by amit
        private void uc_GSTNUsers_addInvoiceRedirect(object sender, EventArgs e)
        {
            int Month = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            // for turnover start
            int Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);
            int ddlselected = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
            var loggedinUser = Common.LoggedInUserID();
            Label mpLabel = (Label)uc_GSTR_Taxpayer.FindControl("lblTurnoverAMT");
            //for turnover amits
            List<string> userLists = new List<string>();
            if (uc_GSTNUsers.AssociatedUsersIds != null)
            {
                userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
            }
            userLists.Add(loggedinUser);
            //var abc = ReturnSelf;
            //this.ReturnSelf = uc_GSTNUsers.GetItem;
            if (ddlselected > 0)
            {
                // lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
                mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == SellerUserId).Sum(s => s.TaxableAmount).ToString();
            }
            else
            {
                mpLabel.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
            }
            //End
            BindAllList(SellerUserId, Month);
        }



        public void uc_GSTR_Taxpayer_AddMoreClick(object sender, EventArgs e)
        {

            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            BindAllList(SellerUserId, mName);

        }


        public void BindListView<T>(ListView lvControl, List<T> collectionItem)
        {

            lvControl.DataSource = collectionItem;
            lvControl.DataBind();
        }
        public void BindAllList(string SellerUserId, int MonthName)
        {
            GetFile_GSTR_4A(SellerUserId, MonthName);
            GetFile_GSTR_4B(SellerUserId, MonthName);
            BindFile_GSTR_4C(SellerUserId, MonthName);
            GetFile_GSTR_5A(SellerUserId, MonthName);
            GetFile_GSTR_5A_2(SellerUserId, MonthName);
            GetFile_GSTR_1_6A(SellerUserId, MonthName);
            GetFile_GSTR_1_6B(SellerUserId, MonthName);
            GetFile_GSTR_1_6C(SellerUserId, MonthName);
            //new
            GetFile_GSTR_1_7A_1(SellerUserId, MonthName);
            GetFile_GSTR_1_7A_2(SellerUserId, MonthName);
            GetFile_GSTR_1_7B_1(SellerUserId, MonthName);
            GetFile_GSTR_1_7B_2(SellerUserId, MonthName);
            GetFile_GSTR_1_8A(SellerUserId, MonthName);
            GetFile_GSTR_1_8B(SellerUserId, MonthName);
            GetFile_GSTR_1_8C(SellerUserId, MonthName);
            GetFile_GSTR_1_8D(SellerUserId, MonthName);
            BindFILE_GSTR_1_9A();
            GetFile_GSTR_1_9B(SellerUserId, MonthName);
            GetFile_GSTR_1_9C(SellerUserId, MonthName);
            BindFILE_GSTR_1_10A();
            GetFile_GSTR_1_10AA(SellerUserId, MonthName);
            BindFILE_GSTR_1_10B();
            GetFile_GSTR_1_10BB(SellerUserId, MonthName);
            GetFile_GSTR_1_11A1(SellerUserId, MonthName);
            GetFile_GSTR_1_11A2(SellerUserId, MonthName);
            GetFile_GSTR_1_11B1(SellerUserId, MonthName);
            GetFile_GSTR_1_11B2(SellerUserId, MonthName);
            GetGSTR_1_12(SellerUserId, MonthName);
        }

        public void GetFile_GSTR_4A(string SellerUserId, int MonthName)
        {
            try
            {

                BindListView(lv_GSTR1_4A, _invoice.BindFile_GSTR_4A(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_4B(string SellerUserId, int MonthName)
        {
            try
            {

                BindListView(lv_GSTR1_4B, _invoice.GetFile_GSTR_4B(SellerUserId, MonthName));

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR_4C(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lv_GSTR1_4C, _invoice.GetFile_GSTR_4C(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_5A(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lv_GSTR1_5A1, _invoice.GetFile_GSTR_5A(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_5A_2(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lv_GSTR1_5A2, _invoice.GetFile_GSTR_5A_2(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        /// <summary>
        /// Get All Result of GSTR1 6A
        /// </summary>
        private void GetFile_GSTR_1_6A(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_6A, _invoice.GetFile_GSTR_1_6A(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        /// <summary>
        /// Get All Result of GSTR1 6B
        /// </summary>
        private void GetFile_GSTR_1_6B(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_6B, _invoice.GetFile_GSTR_1_6B(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        /// <summary>
        /// Get All Result of GSTR1 6C
        /// </summary>
        private void GetFile_GSTR_1_6C(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_6C, _invoice.GetFile_GSTR_1_6C(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_7A_1(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_7A1, _invoice.GetFile_GSTR_1_7A_1(SellerUserId, MonthName));

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_7A_2(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_7A2, _invoice.GetFile_GSTR_1_7A_2(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_7B_1(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_7B1, _invoice.GetFile_GSTR_1_7B_1(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_7B_2(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_7B2, _invoice.GetFile_GSTR_1_7B_2(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_8A(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_8A, _invoice.GetFile_GSTR_1_8A(SellerUserId, MonthName));

            }

            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_8B(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_8B, _invoice.GetFile_GSTR_1_8B(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_8C(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_8C, _invoice.GetFile_GSTR_1_8C(SellerUserId, MonthName));

            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_8D(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_8D, _invoice.GetFile_GSTR_1_8D(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //pending 
        private void BindFILE_GSTR_1_9A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    //string SellerUserID = Common.LoggedInUserID();
                    //var invoice = unitOfWork.GetGSTR_1_9A(SellerUserID);
                    //lvGSTR1_9A.DataSource = invoice.ToList();
                    //lvGSTR1_9A.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_9B(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_9B, _invoice.GetFile_GSTR_1_9B(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_9C(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_9C, _invoice.GetFile_GSTR_1_9C(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //pending 
        private void BindFILE_GSTR_1_10A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    //string SellerUserID = Common.LoggedInUserID();
                    //var invoice = unitOfWork.GetGSTR_1_10A(SellerUserID);
                    //lvGSTR1_10A.DataSource = invoice.ToList();
                    //lvGSTR1_10A.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_10AA(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_10AA, _invoice.GetFile_GSTR_1_10AA(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //pending 
        private void BindFILE_GSTR_1_10B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    //string SellerUserID = Common.LoggedInUserID();
                    //var invoice = unitOfWork.GetGSTR_1_10B(SellerUserID);
                    //lvGSTR1_10B.DataSource = invoice.ToList();
                    //lvGSTR1_10B.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_10BB(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_10BB, _invoice.GetFile_GSTR_1_10BB(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetFile_GSTR_1_11B1(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_11B1, _invoice.GetFile_GSTR_1_11B1(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //pending
        private void GetFile_GSTR_1_11B2(string SellerUserId, int MonthName)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                BindListView(lvGSTR1_11B2, _invoice.GetFile_GSTR_1_11B2(SellerUserId, MonthName));
                //var loggedinUserId = Common.LoggedInUserID();
                //if (loggedinUserId != null)
                //{
                //    string SellerUserID = Common.LoggedInUserID();
                //    var invoice = unitOfWork.GetGSTR_1_11B2(SellerUserID, MonthName);
                //    lvGSTR1_11B2.DataSource = invoice.ToList();
                //    lvGSTR1_11B2.DataBind();

                //}
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void GetGSTR_1_12(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_12, _invoice.GetGSTR_1_12(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        /// <summary>
        /// Get All Result of GSTR1 11A1
        /// </summary>
        private void GetFile_GSTR_1_11A1(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_11A1, _invoice.GetFile_GSTR_1_11A1(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        /// <summary>
        /// Get All Result of GSTR1 11A2
        /// </summary>
        private void GetFile_GSTR_1_11A2(string SellerUserId, int MonthName)
        {
            try
            {
                BindListView(lvGSTR1_11A2, _invoice.GetFile_GSTR_1_11A2(SellerUserId, MonthName));
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //end

        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();
        protected void lkbFileGSTR1_Click(object sender, EventArgs e)
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                var SellerUserId = uc_GSTNUsers.GetSellerProfile;
                int count = 0;
                if (loggedinUserId != null)
                {
                    List<string> userLists = new List<string>();
                    if (uc_GSTNUsers.AssociatedUsersIds != null)
                    {
                        userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
                    }
                    userLists.Add(loggedinUserId);
                    var ddlvalue = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
                    if (ddlvalue == 0)
                    {
                        //int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
                        DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                        DateTime lastDate = DateTime.Now.LastDayOfMonth();

                        var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID) && f.AuditTrailStatus == 1 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                        var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID) && f.AuditTrailStatus == 0 && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                        //for selectted month filing
                        //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == mName && f.AuditTrailStatus == 1 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                        //var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == loggedinUserId && f.GST_TRN_INVOICE.InvoiceMonth == mName && f.AuditTrailStatus == 0 && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                        //  var invoices = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == loggedinUserId && !filedItem.Contains(f.InvoiceID) && f.AuditTrailStatus == 0 && f.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.InvoiceDate).ToList();
                        List<string> invID = new List<string>();
                        foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in invoices)
                        {
                            string id = inv.InvoiceID.ToString();
                            audittrail.InvoiceID = inv.InvoiceID;
                            audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR1);
                            audittrail.UserIP = Common.IP;
                            audittrail.CreatedDate = DateTime.Now;
                            audittrail.InvoiceAction = inv.InvoiceAction;
                            audittrail.CreatedBy = loggedinUserId;
                            unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                            unitOfWork.Save();
                            count = count + 1;
                            invID.Add(id);
                        }
                        //var email= Common.UserProfile.Email();
                        if (count > 0)
                        {
                            string mailString = string.Empty;
                            string sellerMail = string.Empty;
                            foreach (string iId in invID)
                            {
                                Int64 id = Convert.ToInt64(iId);
                                var InvoiceDtls = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == id);
                                mailString += "<tr><td>" + InvoiceDtls.InvoiceNo.ToString() + "</td><td>" + InvoiceDtls.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax) + "</td></tr>";
                                sellerMail += ";" + InvoiceDtls.AspNetUser.Email;
                            }
                            this.Master.SuccessMessage = "Data Filed Successfully .";
                            SendHTMLMail(mailString, sellerMail.Remove(0, 1));
                            //uc_sucess.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                        }
                        else
                        {
                            this.Master.WarningMessage = "There is no Data to File.";
                            //uc_sucess.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        }
                        int mName = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);
                        var loggedInUser = uc_GSTNUsers.GetSellerProfile;
                        BindAllList(loggedInUser, mName);
                    }
                    else
                    {
                        int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
                        DateTime firstdate = DateTime.Now.FirstDayOfMonth();
                        DateTime lastDate = DateTime.Now.LastDayOfMonth();

                        var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.InvoiceUserID == SellerUserId && f.AuditTrailStatus == 1 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                        var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.InvoiceUserID == SellerUserId && f.AuditTrailStatus == 0 && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();
                        //start
                        // for current month file 
                        //var filedItem = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == SellerUserId && f.GST_TRN_INVOICE.InvoiceMonth == mName && f.AuditTrailStatus == 1 && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).Select(s => s.InvoiceID).ToList();

                        //var invoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f => f.GST_TRN_INVOICE.SellerUserID == SellerUserId && f.GST_TRN_INVOICE.InvoiceMonth == mName && f.AuditTrailStatus == 0 && !filedItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).ToList();

                        //end
                        //  var invoices = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == loggedinUserId && !filedItem.Contains(f.InvoiceID) && f.AuditTrailStatus == 0 && f.Status == true && (f.CreatedDate >= firstdate && f.CreatedDate <= lastDate)).OrderByDescending(o => o.InvoiceDate).ToList();
                        List<string> invID = new List<string>();
                        foreach (GST_TRN_INVOICE_AUDIT_TRAIL inv in invoices)
                        {
                            string id = inv.InvoiceID.ToString();
                            audittrail.InvoiceID = inv.InvoiceID;
                            audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.FileGSTR1);
                            audittrail.UserIP = Common.IP;
                            audittrail.CreatedDate = DateTime.Now;
                            audittrail.InvoiceAction = inv.InvoiceAction;
                            audittrail.CreatedBy = loggedinUserId;
                            unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                            unitOfWork.Save();
                            count = count + 1;
                            invID.Add(id);
                        }
                        //var email= Common.UserProfile.Email();
                        if (count > 0)
                        {
                            string mailString = string.Empty;
                            string sellerMail = string.Empty;
                            foreach (string iId in invID)
                            {
                                Int64 id = Convert.ToInt64(iId);
                                var InvoiceDtls = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == id);

                                mailString += "<tr><td>" + InvoiceDtls.InvoiceNo.ToString() + "</td><td>" + InvoiceDtls.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax) + "</td></tr>";
                                sellerMail += ";" + InvoiceDtls.AspNetUser.Email;
                            }

                            this.Master.SuccessMessage = "Data Filed Successfully .";
                            SendHTMLMail(mailString, sellerMail.Remove(0, 1));
                            //uc_sucess.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                        }
                        else
                        {
                            this.Master.WarningMessage = "There is no Data to File.";
                            //uc_sucess.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                        }
                        // int Month = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);
                        BindAllList(SellerUserId, mName);
                    }

                    ////var email= Common.UserProfile.Email();
                    //if (count > 0)
                    //{
                    //    string mailString = string.Empty;
                    //    string sellerMail = string.Empty;
                    //    foreach (string iId in invID)
                    //    {
                    //        Int64 id = Convert.ToInt64(iId);
                    //        var InvoiceDtls = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == id);

                    //        mailString += "<tr><td>" + InvoiceDtls.InvoiceNo.ToString() + "</td><td>" + InvoiceDtls.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax) + "</td></tr>";
                    //        sellerMail += ";" + InvoiceDtls.AspNetUser.Email;
                    //    }

                    //    this.Master.SuccessMessage = "Data Filed Successfully .";
                    //    SendHTMLMail(mailString, sellerMail.Remove(0, 1));
                    //    //uc_sucess.Visible = true;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //}
                    //else
                    //{
                    //    this.Master.WarningMessage = "There is no Data to File.";
                    //    //uc_sucess.Visible = true;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //}

                }

                //int mName = Convert.ToInt16(uc_GSTR_Taxpayer.MonthName);
                //BindAllList(loggedinUserId, MonthName);
            }
            catch (Exception ex)
            {
                this.Master.ErrorMessage = ex.Message;
                //uc_sucess.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelErrorMessage", "$('#viewInvoiceModelErrorMessage').modal();", true);
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void SendHTMLMail(string invoiceString, string sellerEmail)
        {
            try
            {
                EmailService email = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                StreamReader reader = new StreamReader(Server.MapPath("~/MailFormat/UploadInvoice.html"));
                string readFile = reader.ReadToEnd();
                msg.Body = readFile.Replace("{invoiceString}", invoiceString); ;   //"hi body.....";
                msg.Destination = Common.UserProfile.Email;//sellerEmail;
                msg.Subject = "File GSTR-1.";
                email.Send(msg);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbJson_Click(object sender, EventArgs e)
        {
            cls_Invoice_JSON jsonData = new cls_Invoice_JSON();
            List<GST_TRN_INVOICE> invoicelist = new List<GST_TRN_INVOICE>();
            var loggedInUser = Common.LoggedInUserID();
            var SellerUserId = uc_GSTNUsers.GetSellerProfile;
            int mName = Convert.ToInt16(uc_GSTR_Taxpayer.SelectedMonth);


            ////for single list view
            //foreach (ListViewDataItem item in lv_GSTR1_4B.Items)
            //{
            //    string invoiceno = (lv_GSTR1_4B.DataKeys[item.DisplayIndex].Values["InvoiceNo"].ToString());
            //    var data = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceNo == invoiceno).Where(x => x.InvoiceUserID == SellerUserId && x.InvoiceMonth == 5).ToList();
            //    var invid = unitOfWork.InvoiceRepository.Find(f => f.InvoiceNo == invoiceno).InvoiceID;
            //    clsMessageAttribute attribute = new clsMessageAttribute();
            //    var invoiceDetail = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invid);
            //    invoicelist.Add(invoiceDetail);
            //}

            ////for all data
            var InvoiceJson = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == SellerUserId).Where(x => x.InvoiceUserID == SellerUserId && x.InvoiceMonth == mName).ToList();

            string text = jsonData.GetInvoiceJSONData(InvoiceJson);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("content-disposition", "attachment;filename=\"data.json\"");
            Response.Write(text);
            Response.End();
        }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ureturn/ReturnGstr1.aspx");
        }
    }
}
