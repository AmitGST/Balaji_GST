using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GST.Utility;
using BusinessLogic.Repositories;


namespace BALAJI.GSP.APPLICATION.UC.UC_Gstr
{
    public partial class uc_Gstr3B_Tileview : System.Web.UI.UserControl
    {
        UnitOfWork unitofwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindFile_GSTR3B_Header();
            }
        }
        private void BindFile_GSTR3B_Header()
        {
            try
            {
                var month = Session["Month"];
                //Session["Month"] = month;
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    var invoice = unitofwork.GetGSTR3B_FileReturn_Header(loggedinUserId, Convert.ToInt16(month));
                    rptGSTR3B.DataSource = invoice.ToList();
                    rptGSTR3B.DataBind();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}