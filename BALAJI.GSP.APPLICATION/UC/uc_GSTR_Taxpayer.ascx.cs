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
using System.Globalization;
using BusinessLogic.Repositories;



namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_GSTR_Taxpayer : System.Web.UI.UserControl
    {
        #region VARIABLE_DECLARETION
        string PID = string.Empty;
        string SellerGSTIN = string.Empty;
        ExcelDB excelDB = new ExcelDB();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string strReceiverGSTN = string.Empty;
        string period = string.Empty;
        string month = string.Empty;
        string year = string.Empty;
        #endregion
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if()
            try
            {
                int Month = Convert.ToInt32(uc_invoiceMonth.GetIndexValue);
                //int ddlIndex = uc_GSTNUsers.GetselValue;
                var userProfile = uc_GSTNUsers.GetUserUserProfile;
                var SellerUserId = uc_GSTNUsers.GetSellerProfile;
                var loggedinUser = Common.LoggedInUserID();
                DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                lblFinYear.Text = DateTime.Now.Year.ToString();
                lblGSTIN.Text = userProfile.GSTNNo;
                lblTaxpayerName.Text = userProfile.OrganizationName;
                int Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);

                //for turnover amits
               

                //userLists.Add(loggedinUser);
               //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                

                //turn over
                //var Month = Convert.ToByte(DateTime.Now.Month - 1);
                //int Month = Convert.ToInt16(SelectedMonth);
                //var loggedinUser = Common.LoggedInUserID();
                //List<string> userLists = new List<string>();
                //if (uc_GSTNUsers.AssociatedUsersIds != null)
                //{
                //    userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
                //}
                //userLists.Add(loggedinUser);
                ////var abc = ReturnSelf;
                ////this.ReturnSelf = uc_GSTNUsers.GetItem;
                //if (uc_GSTNUsers.ddlGSTNUsers.SelectedIndex > 0)
                //{
                //    // lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
                //    lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                //}
                //else
                //{
                //    lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
                //    //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
                //}

                ////old 
                //if (ddlUservalue == 0)
                //{
                //var loggedInUser = Common.LoggedInUserID();
                //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == invoiceMonth && f.GST_TRN_INVOICE.InvoiceUserID == loggedInUser).Sum(s => s.TaxableAmount).ToString();
                //}
                //else
                //{
                //var SellerUserId = uc_GSTNUsers.GetSellerProfile;
                //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == invoiceMonth && f.GST_TRN_INVOICE.InvoiceUserID == SellerUserId).Sum(s => s.TaxableAmount).ToString();
                // }
                uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
                uc_GSTNUsers.addInvoiceRedirect += uc_GSTNUsers_addInvoiceRedirect;
               
                Turnover();
                //uc_GSTNUsers.addInvoiceUnchkRedirectaa += uc_GSTNUsers_addInvoiceUnchkRedirectaa;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //private void uc_GSTNUsers_addInvoiceUnchkRedirectaa(object sender, EventArgs e)
        //{
        //    DropDownList ddl = (DropDownList)sender;
        //    uc_GSTNUsers.ddlGSTNUsers.SelectedIndex = ddl.SelectedIndex;
        //}


        public void Turnover()
        {
            //var Monthpageload = Convert.ToByte(DateTime.Now.Month - 1);
            //int index = (uc_GSTNUsers.FindControl("ddlGSTNUsers") as DropDownList).SelectedIndex;
            //int ddlindx = testa;
            //int Month = Convert.ToInt16(SelectedMonth);
            //var loggedinUser = Common.LoggedInUserID();
            //List<string> userLists = new List<string>();
            //if (uc_GSTNUsers.AssociatedUsersIds != null)
            //{
            //    userLists = uc_GSTNUsers.AssociatedUsersIds;//TODO:Repetation remove need to work here again asap by ankita
            //}
            //userLists.Add(loggedinUser);
            ////var abc = ReturnSelf;
            ////this.ReturnSelf = uc_GSTNUsers.GetItem;
            //if (Convert.ToInt32(uc_GSTNUsers.ddlGSTNUsers.SelectedItem.Value) > 0)
            //{
            //    // lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
            //    lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
            //}
            //else
            //{
            //    lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Monthpageload && f.GST_TRN_INVOICE.InvoiceUserID == loggedinUser).Sum(s => s.TaxableAmount).ToString();
            //    //lblTurnoverAMT.Text = unitOfWork.InvoiceDataRepository.Filter(f => f.GST_TRN_INVOICE.InvoiceMonth == Month && userLists.Contains(f.GST_TRN_INVOICE.InvoiceUserID)).Sum(s => s.TaxableAmount).ToString();
            //}
        }

        public void uc_GSTNUsers_addInvoiceRedirect(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            uc_GSTNUsers.ddlGSTNUsers.SelectedValue = ddl.SelectedValue;
        }


        public EventHandler AddMoreClick;
        //public string SelectedMonth;
        public void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var loggedinUser = Common.LoggedInUserID();
            this.MonthName = Convert.ToInt32(uc_invoiceMonth.GetValue);
            AddMoreClick(sender, e);

        }

        public int SelectedMonth
        {
            get
            {
                int MonthIndex = uc_invoiceMonth.GetIndexValue;
                return MonthIndex;
            }

        }

        public int testa
        {
            get
            {
                int ddlindx = uc_GSTNUsers.ddlGSTNUsers.SelectedIndex;
                return ddlindx;
            }

        }

        //public int SelectedReturn
        //{
        //    get
        //    {
        //        int ReturnIndex = uc_GSTNUsers.GetValue;
        //        return ReturnIndex;
        //    }
        //}

        public int MonthName
        {
            get
            {
                var mName = string.IsNullOrEmpty(ViewState["MonthName"].ToString()) ? "" : ViewState["MonthName"].ToString();
                return Convert.ToInt32(mName);
            }
            set
            {
                ViewState["MonthName"] = value;
            }
        }

        //public int ReturnSelf
        //{
        //    get
        //    {
        //        var ReturnSelf = string.IsNullOrEmpty(ViewState["SelectedReturn"].ToString()) ? "" : ViewState["SelectedReturn"].ToString();
        //        return Convert.ToInt32(ReturnSelf);
        //    }
        //    set
        //    {
        //        ViewState["SelectedReturn"] = value;
        //    }
        //}
    }
}