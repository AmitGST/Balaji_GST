using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.Entity.Validation;
using GST.Utility;
using System.IO;
using System.Web;
using BusinessLogic.Repositories;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BusinessLogic.Repositories
{
    public interface IErrorLog
    {
        //public int Id { get; set; }
        //public string ErrorPage { get; set; }
        //public Nullable<byte> ErrorStatus { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
    }
    
    public class cls_ErrorLog
    {
        UnitOfWork unitOfWork;
        public cls_ErrorLog()
        {
            unitOfWork = new UnitOfWork();

        }
        
        public static void LogError(Exception ex,string loggedInUserId)
        {
            HttpContext ctxObject = HttpContext.Current;
            GST_TRN_ERROR_HANDLING error = new GST_TRN_ERROR_HANDLING();
            error.CreatedDate = DateTime.Now;
            error.Status = (byte)EnumConstants.ErrorHandling.Pending;
            error.ErrorPage = Path.GetFileName(ctxObject.Request.Path);
            error.CreatedBy = loggedInUserId;
            error.Message = ex.Message;
            error.RequestUrl = (ctxObject.Request.Url != null) ? ctxObject.Request.Url.ToString() : String.Empty;
            if (ctxObject.Request.ServerVariables["HTTP_REFERER"] != null)
            {
                error.ServerName = ctxObject.Request.ServerVariables["HTTP_REFERER"].ToString();
            }
            string ipaddress;
            ipaddress = ctxObject.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
              error.UserIP = ctxObject.Request.ServerVariables["REMOTE_ADDR"];
            //error. = (ctxObject.Request.UserHostAddress != null) ? ctxObject.Request.UserHostAddress : String.Empty;
            error.ErrorSource = ex.Source;
            error.TargetSite = ex.TargetSite.ToString();
            error.StackTrace = ex.StackTrace;
            cls_ErrorLog errorLog = new cls_ErrorLog();
            errorLog.SaveError(error);
        }

        public static void LogError(DbEntityValidationException ex, string loggedInUserId)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                string ErrorMessage;
                foreach (var ve in eve.ValidationErrors)
                {
                    ErrorMessage = ve.PropertyName + "  " + ve.ErrorMessage;
                }
            }
            HttpContext ctxObject = HttpContext.Current;
            GST_TRN_ERROR_HANDLING error = new GST_TRN_ERROR_HANDLING();
            error.CreatedDate = DateTime.Now;
            error.Status = (byte)EnumConstants.ErrorHandling.Pending;
            error.ErrorPage = Path.GetFileName(ctxObject.Request.Path);
            error.CreatedBy = loggedInUserId;
            error.Message = ex.Message;
            error.RequestUrl = (ctxObject.Request.Url != null) ? ctxObject.Request.Url.ToString() : String.Empty;
            if (ctxObject.Request.ServerVariables["HTTP_REFERER"] != null)
            {
                error.ServerName = ctxObject.Request.ServerVariables["HTTP_REFERER"].ToString();
            }
            error.UserIP = (ctxObject.Request.UserHostAddress != null) ? ctxObject.Request.UserHostAddress : String.Empty;
            error.ErrorSource = ex.Source;
            error.TargetSite = ex.TargetSite.ToString();
            error.StackTrace = ex.StackTrace;
            cls_ErrorLog errorLog = new cls_ErrorLog();
            errorLog.SaveError(error);

        }

        public void SaveError(GST_TRN_ERROR_HANDLING error)
        {
            try
            {
                unitOfWork.ErrorHandlingRepository.Create(error);
                unitOfWork.Save();
               
            }
            catch (Exception ex)
            {  }
        }
    }
}
