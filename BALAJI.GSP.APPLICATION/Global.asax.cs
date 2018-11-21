using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
using BALAJI.GSP.APPLICATION;
using BALAJI.GSP.APPLICATION.Infrastructure;
using System.Web.UI.WebControls;


namespace BALAJI.GSP.APPLICATION
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            DropDownList.DisabledCssClass = "";
            TextBox.DisabledCssClass = "";
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Initialize the product database.
            //Database.SetInitializer(new d ProductDatabaseInitializer());
            // Create the custom role and user.
           // RoleActions roleActions = new RoleActions();
          //  roleActions.AddUserAndRole();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
