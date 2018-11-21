using GSTN.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.GSTN
{
    public class cls_GSTN_Api
    {
        public void OTPGSTN(string gstinNo,string gstnuserID)
        {

        }

        public static void RegisterDSC(string gstin, string userid, string pan)
        {
            GSTNAuthClient client = GetAuth(gstin, userid);
            GSTNDSClient client2 = new GSTNDSClient(client, gstin);

            var cert = DSCUtils.getCertificate();
            byte[] data = Encoding.UTF8.GetBytes(pan);
            var sign = Convert.ToBase64String(DSCUtils.SignCms(data, cert));
            var result = client2.RegisterDSC(pan, sign);
        }

        private static GSTNAuthClient GetAuth(string gstin, string userid)
        {

            GSTNAuthClient client = new GSTNAuthClient(gstin, userid);
            var result = client.RequestOTP();

            System.Console.Write("Enter OTP:");
            string otp = System.Console.ReadLine();

            var result2 = client.RequestToken(otp);
            return client;
        }
    }
}
