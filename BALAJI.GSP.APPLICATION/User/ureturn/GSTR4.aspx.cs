using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace UserInterface
{
    public partial class GSTR4 : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindAllList();
        }
        private void BindAllList()
        {
            BindFile_GSTR4_4A();
            BindFile_GSTR4_4B();
             BindFile_GSTR4_4C();
             BindFile_GSTR4_4D();
             BindFile_GSTR4_5A();
             BindFile_GSTR4_5B();
             BindFile_GSTR4_5C();
             BindFile_GSTR4_6();
             BindFile_GSTR4_7();
             BindFile_GSTR4_9();
               
        }

        private void BindFile_GSTR4_4A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_4A(SellerUserID);
                    lv_GSTR4_4A.DataSource = invoice.ToList();
                    lv_GSTR4_4A.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR4_4B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_4B(SellerUserID);
                    lv_GSTR4_4B.DataSource = invoice.ToList();
                    lv_GSTR4_4B.DataBind();


                }
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR4_4C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_4C(SellerUserID);
                    lv_GSTR4_4C.DataSource = invoice.ToList();
                    lv_GSTR4_4C.DataBind();


                }
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR4_4D()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_4D(SellerUserID);
                    lv_GSTR4_4D.DataSource = invoice.ToList();
                    lv_GSTR4_4D.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR4_5A()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_5A(SellerUserID);
                    lv_GSTR4_5A.DataSource = invoice.ToList();
                    lv_GSTR4_5A.DataBind();
                }
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR4_5B()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_5B(SellerUserID);
                    lv_GSTR4_5B.DataSource = invoice.ToList();
                    lv_GSTR4_5B.DataBind();


                }
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindFile_GSTR4_5C()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_5C(SellerUserID);
                    lv_GSTR4_5C.DataSource = invoice.ToList();
                    lv_GSTR4_5C.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR4_6()
        {
            try
            {

                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_6(SellerUserID);
                    lv_GSTR4_6.DataSource = invoice.ToList();
                    lv_GSTR4_6.DataBind();


                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR4_7()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_7(SellerUserID);
                    lv_GSTR4_7.DataSource = invoice.ToList();
                    lv_GSTR4_7.DataBind();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindFile_GSTR4_9()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    string SellerUserID = Common.LoggedInUserID();
                    var invoice = unitOfWork.GetGSTR4_9(SellerUserID);
                    lv_GSTR4_9.DataSource = invoice.ToList();
                    lv_GSTR4_9.DataBind();


                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}