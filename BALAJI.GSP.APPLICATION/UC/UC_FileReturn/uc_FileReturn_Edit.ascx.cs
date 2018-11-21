using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic.Repositories;
using GST.Utility;
using DataAccessLayer;
using System.Net;
using System.Globalization;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using BusinessLogic.Repositories.GSTN;



namespace BALAJI.GSP.APPLICATION.UC.UC_FileReturn
{
    public partial class uc_FileReturn_Edit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        private void BindInvoice(GST_TRN_INVOICE invoice)
        {
            gvInvoice_Edit.DataSource = invoice.GST_TRN_INVOICE_DATA;
            gvInvoice_Edit.DataBind();
        }
    }
}