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

namespace UserInterface
{
    public partial class GSTR6 : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        #region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllList();
            }

        }
        #endregion
        public void BindAllList()
        {
            BindFile_GSTR6_3();
            BindFile_GSTR6_4_A();
            BindFile_GSTR6_4_B();
            BindFile_GSTR6_4_C();
            BindFile_GSTR6_5_A();
            BindFile_GSTR6_5_B();
            BindFile_GSTR6_6_A();
            BindFile_GSTR6_6_B();
            BindFile_GSTR6_6_C();
            BindFile_GSTR6_7_A();
            BindFile_GSTR6_7_B();
            BindFile_GSTR6_8_A();
            BindFile_GSTR6_8_B();
            BindFile_GSTR6_9_A();
            BindFile_GSTR6_9_B();
            BindFile_GSTR6_10();
            BindFile_GSTR6_11_A();
            BindFile_GSTR6_11_B();
        }
        
        private void BindFile_GSTR6_3()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_3(SellerUserID);
                    lv_GSTR6_3.DataSource = invoice.ToList();
                    lv_GSTR6_3.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_4_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_4(SellerUserID);
                    lv_GSTR6_4A.DataSource = invoice.ToList();
                    lv_GSTR6_4A.DataBind();
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_4_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_4_B(SellerUserID);
                    lv_GSTR6_4B.DataSource = invoice.ToList();
                    lv_GSTR6_4B.DataBind();
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_4_C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_4_C(SellerUserID);
                    lv_GSTR6_4C.DataSource = invoice.ToList();
                    lv_GSTR6_4C.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_5_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_5_A(SellerUserID);
                    lv_GSTR6_5A.DataSource = invoice.ToList();
                    lv_GSTR6_5A.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_5_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_5_B(SellerUserID);
                    lv_GSTR6_5B.DataSource = invoice.ToList();
                    lv_GSTR6_5B.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_6_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_6_A(SellerUserID);
                    lv_GSTR6_6A.DataSource = invoice.ToList();
                    lv_GSTR6_6A.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_6_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_6_A(SellerUserID);
                    lv_GSTR6_6B.DataSource = invoice.ToList();
                    lv_GSTR6_6B.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_6_C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_6_C(SellerUserID);
                    lv_GSTR6_6C.DataSource = invoice.ToList();
                    lv_GSTR6_6C.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_7_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_7_A(SellerUserID);
                    lv_GSTR6_7A.DataSource = invoice.ToList();
                    lv_GSTR6_7B.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_7_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_7_B(SellerUserID);
                    lv_GSTR6_7A.DataSource = invoice.ToList();
                    lv_GSTR6_7B.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_8_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_8_A(SellerUserID);
                    lv_GSTR6_8A.DataSource = invoice.ToList();
                    lv_GSTR6_8A.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_8_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_8_B(SellerUserID);
                    lv_GSTR6_8B.DataSource = invoice.ToList();
                    lv_GSTR6_8B.DataBind();
                }
            }
            catch(Exception ex){
            cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());}
        }
        private void BindFile_GSTR6_9_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_9_A(SellerUserID);
                    lv_GSTR6_9A.DataSource = invoice.ToList();
                    lv_GSTR6_9A.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());}
        }
        private void BindFile_GSTR6_9_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_9_B(SellerUserID);
                    lv_GSTR6_9B.DataSource = invoice.ToList();
                    lv_GSTR6_9B.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_10()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_10(SellerUserID);
                    lv_GSTR6_10.DataSource = invoice.ToList();
                    lv_GSTR6_10.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_11_A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_11_A(SellerUserID);
                    lv_GSTR6_11_A.DataSource = invoice.ToList();
                    lv_GSTR6_11_A.DataBind();
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR6_11_B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_6_11_B(SellerUserID);
                    lv_GSTR6_11_B.DataSource = invoice.ToList();
                    lv_GSTR6_11_B.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}