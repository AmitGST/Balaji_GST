using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;



namespace BALAJI.GSP.APPLICATION.User.Trans
{
    public partial class ViewNote : System.Web.UI.Page
    {
        cls_CreditDebit_Note credit = new cls_CreditDebit_Note();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNoteType();
            }
        }
        private void BindNoteType()
        {
            try
            {
                lvNote_Received.DataSource = credit.GetCreditDebitNoteReceived(Common.LoggedInUserID());
                lvNote_Received.DataBind();
                lv_Issued.DataSource = credit.GetCreditDebitNoteIssued(Common.LoggedInUserID());
                lv_Issued.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbEdit = (LinkButton)sender;

                Int64 creditdebitID = Convert.ToInt64(lkbEdit.CommandArgument.ToString());

                var item = credit.GetCreditDebitNoteData(Common.LoggedInUserID(), creditdebitID);
                gvEditViewNote.DataSource = item.ToList();
                gvEditViewNote.DataBind();
                gvEditViewNote.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}