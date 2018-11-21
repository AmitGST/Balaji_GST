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

namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class frm_UserManage : System.Web.UI.Page
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        protected void Page_PreInit(object sender, EventArgs e)
        {
           // this.MasterPageFile = "~/User/User.master";
        }
        protected string SuccessMessage
        {
            get;
            private set;
        }
        private ApplicationGroupManager _groupManager;
        public ApplicationGroupManager GroupManager
        {
            get
            {
                return _groupManager ?? new ApplicationGroupManager();
            }
            private set
            {
                _groupManager = value;
            }
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       
       

       
       
     
       

       
    }

}