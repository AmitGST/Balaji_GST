using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BALAJI.GSP.APPLICATION.Service
{
    /// <summary>
    /// Summary description for AutoPopulate
    /// </summary>
  
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoPopulate : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

           [WebMethod]
        public string[] GetItems(string prefixText,int count)
        {
               //Changes by amits
            UnitOfWork unitOfWork = new UnitOfWork();
            var loggedinUserId = Common.LoggedInUserID();
            var domaintypes = unitOfWork.UserBuisnessTypeRepository.Filter(f => f.UserID == loggedinUserId).Select(x => x.BusinessID);
            var HsnSac = unitOfWork.ItemRepository.Filter(f => f.ItemCode.Contains(prefixText) && f.Status == true && f.UserId == loggedinUserId && domaintypes.Contains(f.BuisnessTypeId)).Take(10).Select(s => s.ItemCode).ToList();
          
            return HsnSac.ToArray();
        }
    }
}
