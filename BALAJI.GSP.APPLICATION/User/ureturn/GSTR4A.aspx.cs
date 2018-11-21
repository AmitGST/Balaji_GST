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
    public partial class GSTR4A : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindAllList();
        }

        public void BindAllList()
        {
            BindFile_GSTR4A_3();
            BindFile_GSTR4A_3B();
            BindFile_GSTR4A_4();
            BindFile_GSTR4A_5();

        }

        private void BindFile_GSTR4A_3()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR4A_3(SellerUserID);
                lv_GSTR4A_3A.DataSource = invoice.ToList();
                lv_GSTR4A_3A.DataBind();

            }
        }

        private void BindFile_GSTR4A_3B()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR4A_3B(SellerUserID);
                lv_GSTR4A_3B.DataSource = invoice.ToList();
                lv_GSTR4A_3B.DataBind();

            }
        }

        private void BindFile_GSTR4A_4()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR4A_4(SellerUserID);
                lv_GSTR4A_4.DataSource = invoice.ToList();
                lv_GSTR4A_4.DataBind();

            }
        }

        private void BindFile_GSTR4A_5()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR4A_5(SellerUserID);
                lv_GSTR4A_5.DataSource = invoice.ToList();
                lv_GSTR4A_5.DataBind();

            }
        }
    }
}