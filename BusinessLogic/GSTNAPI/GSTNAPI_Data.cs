using Integrated.API.GSTN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.GSTNAPI
{
    public class GSTNDataValue
    {
        public string Submit_RefrenceId { get; set; }
        public string Save_RefrenceId { get; set; }
        public string Submit_TransId { get; set; }
        public string Save_TransId { get; set; }
        public string Message { get; set; }
    }
    public class GSTNAPI_Data
    {
          

        public GSTNAPI_Data()
        {
            Integrated.API.GSTN.GSTNConstants.client_id = "l7xx5edefdd923ad438eb5b332a73269f812";// arr[0];
            Integrated.API.GSTN.GSTNConstants.client_secret = "383dc1f4835f43ad80f80f6cf284cd7b";
        }

        public static GSTNDataValue GSTR1Save(string dataInvoice, string gstin, string userid, string fp, string ctin, string etin, string otp)
        {
            GSTNAuthClient client = GetAuth(gstin, userid, otp);

            Integrated.API.GSTN.GSTR1.GSTR1Total model = JsonConvert.DeserializeObject<Integrated.API.GSTN.GSTR1.GSTR1Total>(dataInvoice);
            model.gstin = gstin;
            model.fp = fp;
            GSTR1ApiClient client2 = new GSTR1ApiClient(client, gstin, fp);
            var info = client2.Save(model);
            var model2 = client2.Submit(fp).Data;
            var submit_reference_id = model2.reference_id;
            var submit_trans_id = model2.trans_id;
            var save_RefrenceId = GetStatus(client2, info.Data, fp);
            GSTNDataValue items = new GSTNDataValue();
            items.Save_RefrenceId = save_RefrenceId;
            items.Submit_RefrenceId = submit_reference_id;
            //items.Save_TransId = save_RefrenceId;
            items.Submit_TransId = submit_trans_id;
            items.Message = "";
            return items;
        }

        private static string RegisterDSC(string gstin, string userid, string pan, string otp)
        {
            Integrated.API.GSTN.GSTNAuthClient client = GetAuth(gstin, userid, otp);
            Integrated.API.GSTN.GSTNDSClient client2 = new GSTNDSClient(client, gstin);

            var cert = DSCUtils.getCertificate();
            byte[] data = Encoding.UTF8.GetBytes(pan);
            var sign = Convert.ToBase64String(DSCUtils.SignCms(data, cert));
            var result = client2.RegisterDSC(pan, sign);
            return result.Message;
        }

        public static GSTNAuthClient GetAuth(string gstin, string userid,string otp)
        {

            GSTNAuthClient client = new GSTNAuthClient(gstin, userid);
           // var result = client.RequestOTP();
            var result2 = client.RequestToken(otp);
            return client;
        }

        public static void GetOTP(string gstin, string userid)
        {

            GSTNAuthClient client = new GSTNAuthClient(gstin, userid);
            var result = client.RequestOTP();
        }

        public static GSTNResult<Integrated.API.GSTN.Auth.TokenResponseModel> VerifyOTP(string gstin, string userid, string OTP)
        {
            GSTNAuthClient client = new GSTNAuthClient(gstin, userid);
            var result = client.RequestToken(OTP);
            return result;
        }
        public static GSTNAuthClient GetToken(string gstin, string userid,string otp)
        {
            GSTNAuthClient client = new GSTNAuthClient(gstin, userid);
            var result2 = client.RequestToken(otp);
            return client;
        }

        private static string GetStatus(GSTNReturnsClient client2, SaveInfo info, string fp)
        {
            //System.Console.WriteLine("Reference_ID: " + info.reference_id);
            //var status = client2.GetStatusGSTR2(fp, info.reference_id);//DB Save
            return info.reference_id;
        }
    }
}
