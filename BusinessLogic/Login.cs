using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

using System.Threading.Tasks;

namespace com.B2B.GST.LoginModule
{
    interface ILogin
    {
        bool AuthenticatePassword(string gstinUserName, string passWord);
        bool GenerateNewPassworrd(string gstinUserName);
    }
    public class UserLogin:ILogin
    {
        string gstinNumber;

        public string GSTINNumber
        {
            get { return gstinNumber; }
            set { gstinNumber = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool AuthenticatePassword(string gstinUserName, string passWord)
        {
            GSTINNumber = gstinNumber;
            passWord = passWord;
            return true;
        }

        string GenerateHash(string passWord)
        {
             return "";
        }

        bool MatchExpression(string gstinUserName,string passWord)
        {
            // white space is not allowed
            // semi-colon is not allowed
            // alphanumeric sequence is allowed
            string userNamePattern = @"[0-9A-Za-z\-][^;\s]";

            // white space is not allowed
            // semi-colon is not allowed
            // alphanumeric sequence is allowed
            // along with _ ,@ , . , / ,# ,& , + ,-
            // _@./#&+-
            string passWordPattern = @"[0-9A-Za-z_@./#&+-][^;\s]";
            Regex userNameRegex = new Regex(userNamePattern, RegexOptions.IgnoreCase);
            Regex passWordRegex=new Regex(passWordPattern,RegexOptions.IgnoreCase);
            // Match the regular expression pattern against a text string.
            Match userNameMatch = userNameRegex.Match(gstinUserName);
            Match passWordMatch= passWordRegex.Match(passWord);

            if (userNameMatch.Success && passWordMatch.Success)
                return true;
            else
                return false;
        }

        public bool CHKLength(string gstinUserName, string password)
        {
            if (gstinUserName.Length <= 14)
                return false;
            else
            {
                bool status=MatchExpression(gstinUserName,password);
                return status;
            }
                
        }


        /// <summary>
        /// Applicable only for first time registration, associate the gstin number with 
        /// the password entered 
        /// gstin number is not case senstive --> stringComparison.OrdinalIgnoreCase 
        /// 
        /// </summary>
        /// <param name="gstinUserName"></param>
        /// <returns></returns>
        public bool GenerateNewPassworrd(string gstinUserName)
        {
            throw new NotImplementedException();
        }

        

    }

}
