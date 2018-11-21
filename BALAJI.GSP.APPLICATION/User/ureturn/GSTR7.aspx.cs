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
    public partial class GSTR7 : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BindAllGSTR7();
            }

        }
        public void BindAllGSTR7()
        {
            Bindlist_GSTR_7_3();
            Bindlist_GSTR_7_4();
            Bindlist_GSTR_7_5A();
            Bindlist_GSTR_7_5B();
            Bindlist_GSTR_7_5C();
            Bindlist_GSTR_7_6();
            Bindlist_GSTR_7_7A();
            Bindlist_GSTR_7_7B();
            Bindlist_GSTR_7_7C();
            Bindlist_GSTR_7_8A();
            Bindlist_GSTR_7_8B();
            Bindlist_GSTR_7_8C();
        }

        private void Bindlist_GSTR_7_3()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_3(SellerUserID);
                    lv_GSTR7_3.DataSource = invoice.ToList();
                    lv_GSTR7_3.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_4()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_4(SellerUserID);
                    lv_GSTR7_4.DataSource = invoice.ToList();
                    lv_GSTR7_4.DataBind();

                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_5A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_5A(SellerUserID);
                    lv_GSTR7_5A.DataSource = invoice.ToList();
                    lv_GSTR7_5A.DataBind();

                }
            }
            catch(Exception ex)
                 {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        
        }
        private void Bindlist_GSTR_7_5B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_5B(SellerUserID);
                    lv_GSTR7_5B.DataSource = invoice.ToList();
                    lv_GSTR7_5B.DataBind();

                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_5C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_5C(SellerUserID);
                    lv_GSTR7_5C.DataSource = invoice.ToList();
                    lv_GSTR7_5C.DataBind();

                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_6()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_6(SellerUserID);
                    lv_GSTR7_6.DataSource = invoice.ToList();
                    lv_GSTR7_6.DataBind();

                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_7A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_7_7A(SellerUserID);
                lv_GSTR7_7A.DataSource = invoice.ToList();
                lv_GSTR7_7A.DataBind();

            }
        }
        private void Bindlist_GSTR_7_7B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_7_7B(SellerUserID);
                lv_GSTR7_7B.DataSource = invoice.ToList();
                lv_GSTR7_7B.DataBind();

            }
        }
        private void Bindlist_GSTR_7_7C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_7C(SellerUserID);
                    lv_GSTR7_7C.DataSource = invoice.ToList();
                    lv_GSTR7_7C.DataBind();

                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_8A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_8A(SellerUserID);
                    lv_GSTR7_8A.DataSource = invoice.ToList();
                    lv_GSTR7_8A.DataBind();

                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_8B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_8B(SellerUserID);
                    lv_GSTR7_8B.DataSource = invoice.ToList();
                    lv_GSTR7_8B.DataBind();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Bindlist_GSTR_7_8C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR_7_8C(SellerUserID);
                    lv_GSTR7_8C.DataSource = invoice.ToList();
                    lv_GSTR7_8C.DataBind();

                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}