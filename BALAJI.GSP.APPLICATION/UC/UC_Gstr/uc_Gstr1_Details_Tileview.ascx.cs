using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using GST.Utility;
using System.Web;

namespace BALAJI.GSP.APPLICATION.UC.UC_Gstr
{
    public partial class uc_Gstr1_Details_Tileview : System.Web.UI.UserControl
    {
        cls_Invoice _invoice = new cls_Invoice();
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetGSTR_1_5_B2CL
            if (!IsPostBack)
            {
              
                BindFile_GSTR1_Header();
            }
           // uc_invoiceMonth.SelectedIndexChange += uc_invoiceMonth_SelectedIndexChange;
            BindFile_GSTR1_Header();
      
        }

        private void uc_invoiceMonth_SelectedIndexChange(object sender, EventArgs e)
        {
            int MonthNme = Convert.ToInt16(uc_invoiceMonth.GetValue);
        }
        public EventHandler Info_Click;
        protected void lbinfo_Click(object sender, EventArgs e)
        {
            Info_Click(sender, e);
        }
      
        private void BindFile_GSTR1_Header()
        {
            try
            {
                var month = Session["Month"];
                //Session["Month"] = month;
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    var invoice = unitOfWork.GetGSTR_1_HeaderDetails(loggedinUserId, Convert.ToInt16(month));
                    rptGSTR1.DataSource = invoice.ToList();
                    rptGSTR1.DataBind();
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