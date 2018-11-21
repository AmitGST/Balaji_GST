using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Admin
{
    public partial class ReportPermission : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (!Common.IsAdmin())
            {
                this.MasterPageFile = "~/User/User.master";
            }
            if (Common.IsAdmin())
            {
                this.MasterPageFile = "~/Admin/Admin.master";
            }
            base.OnPreInit(e);
        }

        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindReport();
            BindUsers();
        }
        private void BindReport()
        {
            unitOfwork = new UnitOfWork();
            var userID = Common.LoggedInUserID();
        //    lvReport.DataSource = unitOfwork.ReportRepository.All().OrderByDescending(o => o.CreatedDate).ToList();
            lvReport.DataBind();
        }
        private void BindUsers()
        {
            var users = unitOfwork.AspnetRepository.All().OrderBy(o=>o.GSTNNo).ToList();
            ddl_Users.DataSource = users;

            ddl_Users.DataValueField = "ID";
            ddl_Users.DataTextField = "GSTNNo";
            ddl_Users.DataBind();
            ddl_Users.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            ddl_Users.DataSource = users;
        }

        protected void lvReport_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpReportPermission.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindReport();
            dpReportPermission.DataBind();
        }
    }
}