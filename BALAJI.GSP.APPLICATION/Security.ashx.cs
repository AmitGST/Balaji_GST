using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace BALAJI.GSP.APPLICATION
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    public class Security : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {           

         
           
        }
        //public Boolean isAdminUser()
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var user = System.Web.HttpContext.Current.User.Identity;
        //        ApplicationDbContext context = new ApplicationDbContext();
        //        var UserManager = new UserManager<ApplicationUser>(context);
        //        var s = UserManager.GetRoles(user.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}