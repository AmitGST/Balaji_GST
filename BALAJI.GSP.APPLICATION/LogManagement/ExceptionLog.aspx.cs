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
    public partial class EXCEPTIONLOG : System.Web.UI.Page
    {
        UnitOfWork unitofWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();

            }
        }
        public void BindItems()
        {
            List<GST_MST_EXCEPTIONLOG> objlist = new List<GST_MST_EXCEPTIONLOG>();
            lv_Exception.DataSource = unitofWork.ExceptionLogRepository.All().ToList();
            lv_Exception.DataBind();

        }

        protected void lv_Exception_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

            dpExceptionLog.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindItems();
            dpExceptionLog.DataBind();
        }
    }
}