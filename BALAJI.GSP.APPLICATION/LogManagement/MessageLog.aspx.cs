using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.LogManagement
{
    public partial class MessageLog : System.Web.UI.Page
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindItems();
            }
        }
        public void BindItems()
        {
            List<GST_MST_MESSAGELOG> objList = new List<GST_MST_MESSAGELOG>();
            lv_MessageLog.DataSource = unitOfwork.MessageLogRepository.All().ToList();
            lv_MessageLog.DataBind();
        }

        protected void lv_MessageLog_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpMessageLog.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindItems();
            dpMessageLog.DataBind();
        }
    }
}