using BALAJI.GSP.APPLICATION.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using BALAJI.GSP.APPLICATION.Model;
using GST.Utility;
using BusinessLogic.Repositories;
using DataAccessLayer;
namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_Signatory : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void GetUsers(string userId)
        {
            try
            {
                var signUser = unitOfwork.UserSignatoryRepository.Filter(f => f.Id == userId);
                //ddlSignatory.DataSource = signUser.ToList();
                //ddlSignatory.DataBind();

                ddlSignatory.DataSource = signUser.ToList();
                ddlSignatory.DataTextField = "SignatoryName";
                ddlSignatory.DataValueField = "Signatory_Id";
                ddlSignatory.DataBind();
                ddlSignatory.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

                if (ddlSignatory.Items.Count > 1)
                {
                    ddlSignatory.Visible = true;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        public string UserID
        {
            get
            {
                return ViewState["UserID"].ToString();
            }
            set
            {
                GetUsers(value);
            }
        }
    }
}