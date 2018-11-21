using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using eSignASPLib;
//using eSignASPLib.DTO;
//using GSTN.API;
//using GSTN.API.GSTR1;
using Microsoft.VisualBasic;



namespace UserInterface.UserControl.OTP
{
    public partial class uc_OTP : System.Web.UI.UserControl
    {
        public uc_OTP()
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This is GSTN No for OTP users  
        /// </summary>
        public string Gstin { get; set; }

        ///// <summary>
        ///// This is Finacial Period as per GSTN
        ///// </summary>
        //public string fp { get; set; }

        /// <summary>
        /// This is Finacial Period as per GSTN
        /// </summary>
        public string GetOTP
        {
            get
            {
                return txtOTP.Text.Trim();
            }
        }

        /// <summary>
        /// This is GSTIN OTP
        /// </summary>
        public string OTP { get; set; }

        //public GSTNAuthClient SendOTPRequest
        //{
        //    get 
        //    {
        //        GSTNAuthClient client = GetAuth(this.Gstin);
        //        return client;
        //    }
        //}

       // public string 
        //private GSTNAuthClient GetAuth(string gstin)
        //{
        //    txtOTP.Text = string.Empty;
        //    GSTNAuthClient client = new GSTNAuthClient(gstin);
        //    var result = client.RequestOTP(GSTNConstants.userid);

        //    //System.Console.Write("Enter OTP:");
        //    string otp =this.OTP;// System.Console.ReadLine();
        //    litOtpMessage.Text = "OTP Send.";
        //    divOTP.Visible = true;
        //   // this.OTP = result.Message;
        //   // var result2 = client.RequestToken(GSTNConstants.userid, otp);
        //    return client;
        //}

        protected void btnSubmitOTP_Click(object sender, EventArgs e)
        {
           // GSTNAuthClient client = GetAuth(this.Gstin);
           // return client;
        }

        
    }
}