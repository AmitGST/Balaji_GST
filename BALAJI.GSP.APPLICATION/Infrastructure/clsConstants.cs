using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BALAJI.GSP.APPLICATION.Infrastructure
{
    public class clsConstants
    {
        public static readonly string hostMembershipUrl = "http://localhost:63074/";


        //public static List<string> Roles(this ClaimsIdentity identity)
        //{
        //    return identity.Claims
        //                   .Where(c => c.Type == ClaimTypes.Role)
        //                   .Select(c => c.Value)
        //                   .ToList();
        //}
    }

   
}