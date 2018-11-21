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
    public partial class GSTR6A : System.Web.UI.Page
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
            BindFileGSTR6A_3();
            BindFileGSTR6A_4();
        }

        private void BindFileGSTR6A_3()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_6_A_3(SellerUserID);
                lv_GSTR6A_3.DataSource = invoice.ToList();
                lv_GSTR6A_3.DataBind();
            }
        }
        private void BindFileGSTR6A_4()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                string SellerUserID = Common.LoggedInUserID();
                var invoice = unitOfWork.GetGSTR_6_A_4(SellerUserID);
                lv_GSTR6A_4.DataSource = invoice.ToList();
                lv_GSTR6A_4.DataBind();
            }
        }
    }
}