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
    public partial class GSTR8 : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BindAllGSTR8();
            }
        }
        public void BindAllGSTR8()
        {
            Bindlist_GSTR_8_3();
            Bindlist_GSTR_8_3A();
            Bindlist_GSTR_8_4A();
            Bindlist_GSTR_8_4B();
            Bindlist_GSTR_8_5();
            Bindlist_GSTR_8_6A();
            Bindlist_GSTR_8_6B();
            Bindlist_GSTR_8_6C();
            Bindlist_GSTR_8_7A();
            Bindlist_GSTR_8_7B();
            Bindlist_GSTR_8_7C();
            Bindlist_GSTR_8_8A();
            Bindlist_GSTR_8_8B();
            Bindlist_GSTR_8_8C();
            Bindlist_GSTR_8_9A();
            Bindlist_GSTR_8_9B();
            Bindlist_GSTR_8_9C();
        }
        private void Bindlist_GSTR_8_3()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_3(SellerUserID);
                lv_GSTR8_3A.DataSource = invoice.ToList();
                lv_GSTR8_3A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_3A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_3A(SellerUserID);
                lv_GSTR8_3B.DataSource = invoice.ToList();
                lv_GSTR8_3B.DataBind();

            }
        }
        private void Bindlist_GSTR_8_4A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_4A(SellerUserID);
                lv_GSTR8_4A.DataSource = invoice.ToList();
                lv_GSTR8_4A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_4B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_4B(SellerUserID);
                lv_GSTR8_4B.DataSource = invoice.ToList();
                lv_GSTR8_4B.DataBind();

            }
        }
        //private void Bindlist_GSTR_8_4C()
        //{

        //    var loggedinUserId = Common.LoggedInUserID();
        //    if (loggedinUserId != null)
        //    {
        //        string SellerUserID = Common.LoggedInUserID();
        //        var invoice = unitOfWork.GetGSTR_8_4C(SellerUserID);
        //        lv_GSTR8_4C.DataSource = invoice.ToList();
        //        lv_GSTR8_4C.DataBind();

        //    }
        //}
        private void Bindlist_GSTR_8_5()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_5(SellerUserID);
                lv_GSTR8_5.DataSource = invoice.ToList();
                lv_GSTR8_5.DataBind();

            }
        }
        private void Bindlist_GSTR_8_6A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_6A(SellerUserID);
                lv_GSTR8_6A.DataSource = invoice.ToList();
                lv_GSTR8_6A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_6B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_6B(SellerUserID);
                lv_GSTR8_6B.DataSource = invoice.ToList();
                lv_GSTR8_6B.DataBind();

            }
        }
        private void Bindlist_GSTR_8_6C()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_6C(SellerUserID);
                lv_GSTR8_6C.DataSource = invoice.ToList();
                lv_GSTR8_6C.DataBind();

            }
        }
        private void Bindlist_GSTR_8_7A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_7A(SellerUserID);
                lv_GSTR8_7A.DataSource = invoice.ToList();
                lv_GSTR8_7A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_7B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_7B(SellerUserID);
                lv_GSTR8_7B.DataSource = invoice.ToList();
                lv_GSTR8_7B.DataBind();

            }
        }
        private void Bindlist_GSTR_8_7C()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_7C(SellerUserID);
                lv_GSTR8_7C.DataSource = invoice.ToList();
                lv_GSTR8_7C.DataBind();

            }
        }
        private void Bindlist_GSTR_8_8A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_8A(SellerUserID);
                lv_GSTR8_8A.DataSource = invoice.ToList();
                lv_GSTR8_8A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_8B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_8B(SellerUserID);
                lv_GSTR8_8B.DataSource = invoice.ToList();
                lv_GSTR8_8B.DataBind();

            }
        }
        private void Bindlist_GSTR_8_8C()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_8C(SellerUserID);
                lv_GSTR8_8C.DataSource = invoice.ToList();
                lv_GSTR8_8C.DataBind();

            }
        }
        private void Bindlist_GSTR_8_9A()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_9A(SellerUserID);
                lv_GSTR8_9A.DataSource = invoice.ToList();
                lv_GSTR8_9A.DataBind();

            }
        }
        private void Bindlist_GSTR_8_9B()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_9B(SellerUserID);
                lv_GSTR8_9B.DataSource = invoice.ToList();
                lv_GSTR8_9B.DataBind();

            }
        }
        private void Bindlist_GSTR_8_9C()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_8_9C(SellerUserID);
                lv_GSTR8_9C.DataSource = invoice.ToList();
                lv_GSTR8_9C.DataBind();

            }
        }
    }
}