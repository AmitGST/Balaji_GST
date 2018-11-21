using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API
{
    public class GSTNConstants
    {
        public static byte[] appKey = null;
        public static string base_url = "http://devapi.gstsystem.co.in";
        public static string base_path = ".";
        public static string client_id = "l7xx5edefdd923ad438eb5b332a73269f812";
        public static string client_secret = "383dc1f4835f43ad80f80f6cf284cd7b";
        public static string publicip = "";

        public static byte[] GetAppKeyBytes()
        {
            if (appKey == null)
            {
                appKey = EncryptionUtils.CreateAesKeyBC();
            }
            return appKey;

        }
        public static string GetAppKeyEncoded()
        {
            if (appKey == null)
            {
                appKey = EncryptionUtils.CreateAesKeyBC();
            }
            return Convert.ToBase64String(appKey);

        }

    }
}