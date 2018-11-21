using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using System.Text.RegularExpressions;
using System.IO;

using System.Security.Cryptography;
using System.Web.Configuration;
using ExceptionHandling;
using com.B2B.GST.ExceptionHandling;

namespace com.B2B.GST.ExcelFunctionality
{


    public class ExcelDB
    {
        #region CheckCredentials
        /// <summary>
        /// To get the seller information , beta--> from GSTIN rest 
        /// Purpose is only for testing, till the time we have the complete data types for DB design
        /// TO DO
        /// Comment this code.
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public bool CheckCredentials(string gstinNumber, string passWord)
        {
            //DataSet data = new DataSet();
            //// hardcoding done because to direct to this excel
            //data = ReadExcelFile("Login");

            bool credentialsStatus = false;
            DataSet ds = new DataSet();
            //List<string> sellerDetails = new List<string>();
            string filePath = "Login";
            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand(" SELECT * FROM [Login$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds, "SellerCredentials");


                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Seller is not Created !!!");
                    }


                    foreach (DataTable dt in ds.Tables)
                    {
                        //Searching for the table whose name contains "login"
                        if (dt.TableName.Contains("SellerCredentials"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                // gstin number validated
                                if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    string pass = dr["PassWord"].ToString();
                                    // saved password is searched along with username supplied and 
                                    // also checking whether the user is not logged in from any other machine

                                    // this below line implements that we do not allow any login from any other window or anywhere
                                    string isLogggedStatus = dr["IsLoggedIn"].ToString();
                                    if ((passWord == pass && isLogggedStatus != WebConfigurationManager.AppSettings["LoggedInStatus"].ToString()) || isLogggedStatus == WebConfigurationManager.AppSettings["LoggedInStatus"].ToString())
                                    {
                                        credentialsStatus = true;

                                        // pass the gstin number  to update , IsLogged status to 1
                                        UpdateLogin(gstinNumber);
                                    }


                                    break;

                                }
                            }
                        }
                    }


                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Object Creation Issues :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Object Creation IssuesHB :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }



            return credentialsStatus;

           
        }

        /// <summary>
        ///  Checks whether the username and password is correct or not.
        ///  Then it checks at the login , whether the user is logged from any other machine

        /// </summary>
        /// <param name="data"> this is only for excel the complete , data in the GSTIN excel is parked</param>
        /// <param name="gstinNumber">Username</param>
        /// <param name="passWord">password</param>
        /// <returns>true iff username and password matched and IsLogged is 0</returns>
        private bool CheckCredentials(DataSet data, string gstinNumber, string passWord)
        {
            //ds is your dataset object
            bool chkCredentials = false;
            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Login"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // search for th mathching username
                        if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            string pass = dr["PassWord"].ToString();
                            // saved password is searched along with username supplied and 
                            // also checking whether the user is not logged in from any other machine

                            // this below line implements that we do not allow any login from any other window or anywhere
                            string isLogggedStatus = dr["IsLoggedIn"].ToString();
                            if ((passWord == pass && isLogggedStatus != WebConfigurationManager.AppSettings["LoggedInStatus"].ToString()) || isLogggedStatus == WebConfigurationManager.AppSettings["LoggedInStatus"].ToString())
                            {
                                chkCredentials = true;

                                // pass the gstin number  to update , IsLogged status to 1
                                UpdateLogin(gstinNumber);
                            }

                                
                            break;

                        }
                    }
                }
            }
            return chkCredentials;
        }
        #endregion

        #region ReadExcelFile
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataSet ReadExcelFile(string FilePath)
        {
            DataSet ds = new DataSet();

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();



                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    ds.Tables.Add(dt);
                    if (!sheetName.EndsWith("$"))
                        continue;
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }
        #endregion

        #region GetConnectionString
        /// <summary>
        /// List of Path for various excel files
        /// </summary>
        /// <returns></returns>
        /// 
       
        private string GetConnectionString(string filePath)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();
            //        /*
            //         Always check hard coding of path for files by
            //         * Copying the exact path to the windows and check
            //         * you get the file specified at the hard coded path or not.
            //         */


            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
            props["Extended Properties"] = "Excel 12.0 XML";

            if (filePath == "Login")
                props["Data Source"] = WebConfigurationManager.AppSettings["Login"].ToString();
            else if (filePath == "CETSH")
                props["Data Source"] = WebConfigurationManager.AppSettings["CETSH"].ToString();
            else if (filePath == "InvoiceData")
                props["Data Source"] = WebConfigurationManager.AppSettings["InvoiceData"].ToString();
            else if (filePath == "AdvancePayment")
                props["Data Source"] = WebConfigurationManager.AppSettings["AdvancePayment"].ToString();
            else if (filePath == "Export")
                props["Data Source"] = WebConfigurationManager.AppSettings["Export"].ToString();
            else if (filePath == "AdvanceUploadInvoice")
                props["Data Source"] = WebConfigurationManager.AppSettings["AdvanceUploadInvoice"].ToString();
            else if (filePath == "ExportUploadInvoice")
                props["Data Source"] = WebConfigurationManager.AppSettings["ExportUploadInvoice"].ToString();
            else if (filePath == "UploadInvoice")
                props["Data Source"] = WebConfigurationManager.AppSettings["UploadInvoice"].ToString();
            else if (filePath == "AdvanceGSTR1")
                props["Data Source"] = WebConfigurationManager.AppSettings["AdvanceGSTR1"].ToString();
            else if (filePath == "FileGSTR1")
                props["Data Source"] = WebConfigurationManager.AppSettings["FileGSTR1"].ToString();   // CHANGED BY PRITI(11-03-2017)
            else if (filePath == "ExportGSTR1")
                props["Data Source"] = WebConfigurationManager.AppSettings["ExportGSTR1"].ToString();
            else if (filePath == "FileGSTR2A")
                props["Data Source"] = WebConfigurationManager.AppSettings["FileGSTR2A"].ToString();// ADDED BY PRITI(11-03-2017)
            else if (filePath == "FileGSTR2")
                props["Data Source"] = WebConfigurationManager.AppSettings["FileGSTR2"].ToString();// ADDED BY PRITI(11-03-2017)
            else if (filePath == "FileGSTR1A")
                props["Data Source"] = WebConfigurationManager.AppSettings["FileGSTR1A"].ToString();// ADDED BY PRITI(11-03-2017)
            else if (filePath == "Import")
                props["Data Source"] = WebConfigurationManager.AppSettings["Import"].ToString();
            else if (filePath == "ReverseCharge")
                props["Data Source"] = WebConfigurationManager.AppSettings["ReverseCharge"].ToString();
            else if (filePath == "Purchase")
                props["Data Source"] = WebConfigurationManager.AppSettings["Purchase"].ToString();
            else if (filePath == "Sale")
                props["Data Source"] = WebConfigurationManager.AppSettings["Sale"].ToString();

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
        #endregion

        #region GetSellerDetails
        /// <summary>
        /// To get the seller information , beta--> from GSTIN rest 
        /// Purpose is only for testing, till the time we have the complete data types for DB design
        /// TO DO
        /// Comment this code.
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public List<string> GetSellerDetails(string gstinNumber)
        {
            //DataSet data = new DataSet();
            //data = ReadExcelFile("Login");
            //List<string> sellerDetails = new List<string>();
            //sellerDetails = GetSellerDataFrmExl(data, gstinNumber, sellerDetails);
            //return sellerDetails;


            DataSet ds = new DataSet();
            List<string> sellerDetails = new List<string>();
            string filePath = "Login";
            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [SellerCreateInvoice$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds, "SellerCreateInvoice");


                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Seller is not Created !!!");
                    }


                    foreach (DataTable dt in ds.Tables)
                    {
                        //Searching for the table whose name contains "login"
                        if (dt.TableName.Contains("SellerCreateInvoice"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    sellerDetails.Add(dr["GSTIN"].ToString());
                                    sellerDetails.Add(dr["UserName"].ToString());
                                    sellerDetails.Add(dr["RegisteredName"].ToString());
                                    sellerDetails.Add(dr["RegisteredAddress"].ToString());
                                    sellerDetails.Add(dr["RegisteredStateCode"].ToString());
                                    sellerDetails.Add(dr["RegisteredStateName"].ToString());
                                    sellerDetails.Add(dr["RegisteredStateCodeID"].ToString());
                                    sellerDetails.Add(dr["GrossTurnOver"].ToString());
                                    sellerDetails.Add(dr["FinancialPeriod"].ToString());
                                    break;

                                }
                            }
                        }
                    }
                    

                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Object Creation Issues :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Object Creation IssuesHB :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return sellerDetails;
            
        }




       
        /// <summary>
        ///  TODO Check whether the username and password is correct or not.

        /// </summary>
        /// <param name="data"></param>
        /// <param name="gstinNumber"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private List<string> GetSellerDataFrmExl(DataSet data, string gstinNumber, List<string> sellerDetails)
        {
            //ds is your dataset object

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("SellerCreateInvoice"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["UserName"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            sellerDetails.Add(dr["RegisteredName"].ToString());
                            sellerDetails.Add(dr["RegisteredAddress"].ToString());
                            sellerDetails.Add(dr["RegisteredStateCode"].ToString());
                            sellerDetails.Add(dr["RegisteredStateName"].ToString());
                            sellerDetails.Add(dr["RegisteredStateCodeID"].ToString());
                            sellerDetails.Add(dr["GrossTurnOver"].ToString());
                            sellerDetails.Add(dr["FinancialPeriod"].ToString());
                            break;

                        }
                    }
                }
            }
            return sellerDetails;
        }
        #endregion

        /// <summary>
        /// Purchase Register data 
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        #region GetSellerPurchaseRegisterData based on gstin number
        public DataSet GetSellerPurchaseRegisterData(string gstinNumber)
        {
            DataSet ds = new DataSet();
            List<string> sellerDetails = new List<string>();
            string filePath = "Purchase";
            string connectionString = GetConnectionString(filePath);
           
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd7 = new OleDbCommand("SELECT * FROM [PurchaseRegister$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);
                    da.Fill(ds,"PurchaseRegister");

                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Purchase Register Not Created !!!");
                    }

                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", Nullex);
                }

                
                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return ds;
            
        }
        #endregion

        /// <summary>
        ///  Getting entry of only that particular Product matching with HSN
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        #region GetSellerSaleRegisterData based on hsnNumber
        public SaleLedger GetSellerSaleRegisterData(string hsnNumber,string GSTINNumber)
        {
            DataSet ds = new DataSet();
            List<string> sellerDetails = new List<string>();
            string filePath = "Sale";
            string connectionString = GetConnectionString(filePath);
            SaleLedger saleLedger = new SaleLedger();
            saleLedger.StockLineEntry = new List<StockRegisterLineEntry>();
            StockRegisterLineEntry stockInHand=new StockRegisterLineEntry();

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd7 = new OleDbCommand("SELECT * FROM [SaleRegister$] WHERE HSN ='" + hsnNumber + "' AND GSTIN='" + GSTINNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);
                    da.Fill(ds, "SaleLedger");

                    // above dbcommand will create a line entry either with data or without data , so in the else foreach part 
                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        //throw new Exception("Sale Ledger Register Not Created !!!");
                    }
                    else
                    {
                        
                        foreach (DataTable dt in ds.Tables)
                        {                            
                                foreach (DataRow dr in dt.Rows)
                                {
                                        if (dr["GSTIN"].ToString()!=null)
                                        stockInHand.GstinNumber=(dr["GSTIN"].ToString());
                                        if (dr["Username"].ToString()!=null)
                                        stockInHand.UserName=dr["Username"].ToString();
                                        if (dr["Username"].ToString() != null)
                                        stockInHand.UserName=dr["Username"].ToString();
                                        if (dr["LineID"].ToString() != null)
                                        stockInHand.LineID=Int32.Parse(dr["LineID"].ToString());
                                        if (dr["HSN"].ToString() != null)
                                    	stockInHand.Hsn=(dr["HSN"].ToString());
                                        if (dr["Qty"].ToString() != null)
                                        stockInHand.Qty=Decimal.Parse(dr["Qty"].ToString());
                                        if (dr["PerUnitRate"].ToString() != null)
                                        stockInHand.PerUnitRate = Decimal.Parse(dr["PerUnitRate"].ToString());
                                        if (dr["StockInwardDate"].ToString() != null)
                                        stockInHand.StockInwardDate=Convert.ToDateTime(dr["StockInwardDate"].ToString());
                                        if (dr["StockOrderDate"].ToString() != null)
                                        stockInHand.StockOrderDate=Convert.ToDateTime(dr["StockOrderDate"].ToString());
                                        if (dr["OrderPO"].ToString() != null)
                                        stockInHand.OrderPO=(dr["OrderPO"].ToString());
                                        break;

                                    }
                                
                            }
                     
                       }
                        saleLedger.StockLineEntry.Add(stockInHand);

                    }

                
        
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return saleLedger;

        }
        #endregion

        #region UpdateSellerPurchaseRegisterData

        public bool UpdateSellerSaleRegisterData(StockRegisterLineEntry stockLineEntry)
        {

            bool stockLineEntryUpdatedStatus = false;
            string filePath = "Sale";
            string connectionString = GetConnectionString(filePath);
            int result = 0;
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                cn.Open();

                try
                {
                    OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SaleRegister$] " +
                   "([GSTIN],[Username],[LineID],[HSN],[Qty],[PerUnitRate],[StockInwardDate],[StockOrderDate],[OrderPO]) " +
                   "VALUES(@value1, @value2, @value3,@value4, @value5,@value6, @value7,@value8, @value9)", cn);
                    cmd1.Parameters.AddWithValue("@value1", stockLineEntry.GstinNumber);
                    cmd1.Parameters.AddWithValue("@value2", stockLineEntry.UserName);
                    cmd1.Parameters.AddWithValue("@value3", stockLineEntry.LineID);
                    cmd1.Parameters.AddWithValue("@value4", stockLineEntry.Hsn);
                    cmd1.Parameters.AddWithValue("@value5", stockLineEntry.Qty);
                    cmd1.Parameters.AddWithValue("@value6", stockLineEntry.PerUnitRate);
                    cmd1.Parameters.AddWithValue("@value7", stockLineEntry.StockInwardDate);
                    cmd1.Parameters.AddWithValue("@value8", stockLineEntry.StockOrderDate);
                    cmd1.Parameters.AddWithValue("@value9", stockLineEntry.OrderPO);

                    result = cmd1.ExecuteNonQuery();

                    if (result == 1)
                        stockLineEntryUpdatedStatus = true;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return stockLineEntryUpdatedStatus;
        }
#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GSTINNumber"></param>
        /// <returns></returns>
        public int GetTotalCountOfGoodsSellerDeals(string GSTINNumber)
        {
            string filePath = "Sale";
            string connectionString = GetConnectionString(filePath);
            int maxLineID = 0;
            DataSet ds = new DataSet();
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd7 = new OleDbCommand("SELECT * FROM [SaleRegister$] WHERE GSTIN='" + GSTINNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);
                    da.Fill(ds, "SaleRegister");

                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Sale Register Not Created !!!");
                    }
                    else
                    {
                        maxLineID = ds.Tables[0].Rows.Count;
                    }
                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller Purchace Register :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return maxLineID;
        }


        #region ReadDefaultSeed
        public int ReadDefaultSeed(string gstinNumber)
        {
            DataSet data = new DataSet();
            data = ReadExcelFile("Login");
            // -1 indicated no value 
            int seed = -1;
            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("InvoiceSeed"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // ensure seed in DB is always an int value , no alphabets strictly.
                        if (dr["UserName"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase) && dr["Seed"] != null)
                        {
                            seed = Convert.ToInt32(dr["Seed"]);
                            break;

                        }
                    }
                }
            }
            return seed;
        }

        public int ReadCurrentSrlNo(string gstinNumber)
        {
            DataSet data = new DataSet();
            data = ReadExcelFile("Login");
            // -1 indicated no value 
            int CurrentSrlNo = 0;
            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("InvoiceSeed"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // ensure seed in DB is always an int value , no alphabets strictly.
                        if (dr["UserName"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase) && dr["CurrentSrlNo"] != null)
                        {
                            CurrentSrlNo = Convert.ToInt32(dr["CurrentSrlNo"]);
                            break;

                        }
                    }
                }
            }
            return CurrentSrlNo;
        }
        #endregion

        #region GetRecieverDetails
        /// <summary>
        /// To get the seller information , beta--> from GSTIN rest 
        /// Purpose is only for testing, till the time we have the complete data types for DB design
        /// TO DO
        /// Comment this code.
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public List<string> GetRecieverDetails(string gstinNumber)
        {
            
            DataSet ds = new DataSet();
            List<string> recieverDetails = new List<string>();
            string filePath = "Login";
            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand(" SELECT * FROM [SellerCreateInvoice$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds, "RecieverCreateInvoice");


                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Reciever is not Created !!!");
                    }


                    foreach (DataTable dt in ds.Tables)
                    {
                        //Searching for the table whose name contains "login"
                        if (dt.TableName.Contains("RecieverCreateInvoice"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    recieverDetails.Add(dr["GSTIN"].ToString());
                                    recieverDetails.Add(dr["UserName"].ToString());
                                    recieverDetails.Add(dr["RegisteredName"].ToString());
                                    recieverDetails.Add(dr["RegisteredAddress"].ToString());
                                    recieverDetails.Add(dr["RegisteredStateCode"].ToString());
                                    recieverDetails.Add(dr["RegisteredStateName"].ToString());
                                    recieverDetails.Add(dr["RegisteredStateCodeID"].ToString());
                                    recieverDetails.Add(dr["GrossTurnOver"].ToString());
                                    recieverDetails.Add(dr["FinancialPeriod"].ToString());
                                    break;

                                }
                            }
                        }
                    }


                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Reciever Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Reciever Object Creation Issues :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Reciever Object Creation IssuesHB :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return recieverDetails;
        }

        // deprecated
        /// <summary>
        ///  TODO Check whether the username and password is correct or not.

        /// </summary>
        /// <param name="data"></param>
        /// <param name="gstinNumber"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private List<string> GetRecieverDataFrmExl(DataSet data, string gstinNumber, List<string> recieverDetails)
        {
            //ds is your dataset object

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("SellerCreateInvoice"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["UserName"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            recieverDetails.Add(dr["RegisteredName"].ToString());
                            recieverDetails.Add(dr["RegisteredAddress"].ToString());
                            recieverDetails.Add(dr["RegisteredStateCode"].ToString());
                            recieverDetails.Add(dr["RegisteredStateName"].ToString());
                            recieverDetails.Add(dr["RegisteredStateCodeID"].ToString());                           
                            recieverDetails.Add(dr["FinancialPeriod"].ToString());
                            break;

                        }
                    }
                }
            }
            return recieverDetails;
        }
        #endregion

        #region GetConsigneeDetails
        public List<string> GetConsigneeDetails(string gstinNumber)
        {

            DataSet ds = new DataSet();
            List<string> consigneeDetails = new List<string>();
            string filePath = "Login";
            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand(" SELECT * FROM [SellerCreateInvoice$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds, "ConsigneeCreateInvoice");


                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Consignee is not Created !!!");
                    }


                    foreach (DataTable dt in ds.Tables)
                    {
                        //Searching for the table whose name contains "login"
                        if (dt.TableName.Contains("ConsigneeCreateInvoice"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    consigneeDetails.Add(dr["GSTIN"].ToString());
                                    consigneeDetails.Add(dr["UserName"].ToString());
                                    consigneeDetails.Add(dr["RegisteredName"].ToString());
                                    consigneeDetails.Add(dr["RegisteredAddress"].ToString());
                                    consigneeDetails.Add(dr["RegisteredStateCode"].ToString());
                                    consigneeDetails.Add(dr["RegisteredStateName"].ToString());
                                    consigneeDetails.Add(dr["RegisteredStateCodeID"].ToString());
                                    consigneeDetails.Add(dr["GrossTurnOver"].ToString());
                                    consigneeDetails.Add(dr["FinancialPeriod"].ToString());
                                    break;

                                }
                            }
                        }
                    }


                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("consigneeDetails Object Creation Issues");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("consigneeDetails Object Creation Issues :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("consigneeDetails Object Creation IssuesHB :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return consigneeDetails;
        }

        // deprecated
        private List<string> GetConsigneeDataFrmExl(DataSet data, string gstinNumber, List<string> consigneeDetails)
        {
            //ds is your dataset object

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("SellerCreateInvoice"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["UserName"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            consigneeDetails.Add(dr["RegisteredName"].ToString());
                            consigneeDetails.Add(dr["RegisteredAddress"].ToString());
                            consigneeDetails.Add(dr["RegisteredStateCode"].ToString());
                            consigneeDetails.Add(dr["RegisteredStateName"].ToString());
                            consigneeDetails.Add(dr["RegisteredStateCodeID"].ToString());                          
                        }
                    }
                }

            }
            return consigneeDetails;
        }
        #endregion

        #region GetHSNInformation
        public HSN GetHSNInformation(string hsnNumber)
        {
            DataSet data = new DataSet();
            HSN hsn = new HSN();
            data = ReadExcelFile("CETSH");
            hsn = GetHSNDataFrmExl(hsnNumber, data);
            return hsn;
        }
        public SAC GetSACInformation(string sacNumber)
        {
            DataSet data = new DataSet();
            SAC sac = new SAC();
            data = ReadExcelFile("CETSH");
            sac = GetSACDataFrmExl(sacNumber, data);
            return sac;
        }
        //public List<string> GetHSNInformation(string description)
        //{
        //    DataSet data = new DataSet();
        //    List<string> HSN = new List<string>();
        //    data = ReadExcelFile("CETSH");
        //    HSN = GetHSNDataFrmExl(description, data, HSN);
        //    return HSN;
        //}
        private SAC GetSACDataFrmExl(string sacNumber, DataSet data)
        {
            //ds is your dataset object
            SAC sac = new SAC();

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "Export Worksheet"
                if (dt.TableName.Contains("ExportWorksheet"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CETSHNO"].ToString() == sacNumber)
                        {
                            sac.SACNumber = dr["CETSHNO"].ToString();
                            sac.Description = dr["Description"].ToString();
                            sac.UnitOfMeasurement = dr["Unit of Measurement"].ToString();
                            sac.RateIGST = dr["IGST"].ToString() != null ? Convert.ToDecimal(dr["IGST"].ToString()) : 0;
                            sac.RateCGST = dr["CGST"].ToString() != null ? Convert.ToDecimal(dr["CGST"].ToString()) : 0;
                            sac.RateSGST = dr["SGST"].ToString() != null ? Convert.ToDecimal(dr["SGST"].ToString()) : 0;
                            sac.Cess = dr["Cess"].ToString() != null ? Convert.ToDecimal(dr["Cess"].ToString()) : 0;
                            sac.IsNotified = Convert.ToBoolean(dr["IsNotified"].ToString());

                            break;
                        }

                    }
                }
                //break;
            }

            return sac;
        }
        private HSN GetHSNDataFrmExl(string hsnNumber,DataSet data)
        {
            //ds is your dataset object
            HSN hsn = new HSN();

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "Export Worksheet"
                if (dt.TableName.Contains("ExportWorksheet"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CETSHNO"].ToString() == hsnNumber)
                        {
                            hsn.HSNNumber = dr["CETSHNO"].ToString();
                            hsn.Description = dr["Description"].ToString();
                            hsn.UnitOfMeasurement = dr["Unit of Measurement"].ToString();
                            hsn.RateIGST = dr["IGST"].ToString() != null ? Convert.ToDecimal(dr["IGST"].ToString()) : 0;
                            hsn.RateCGST = dr["CGST"].ToString() != null ? Convert.ToDecimal(dr["CGST"].ToString()) : 0;
                            hsn.RateSGST = dr["SGST"].ToString() != null ? Convert.ToDecimal(dr["SGST"].ToString()) : 0;
                            hsn.Cess = dr["Cess"].ToString() != null ? Convert.ToDecimal(dr["Cess"].ToString()) : 0;
                            hsn.IsNotified = Convert.ToBoolean(dr["IsNotified"].ToString());

                            break;
                        }

                    }
                }
                //break;
            }

            return hsn;
        }

        /// <summary>
        ///  based hsn numnber
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <param name="data"></param>
        /// <param name="hsn"></param>
        /// <returns></returns>
        private List<string> GetHSNDataFrmExl(string hsnNumber, DataSet data, List<string> hsn)
        {
            //ds is your dataset object
            List<string> HSN = new List<string>();

            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "Export Worksheet"
                if (dt.TableName.Contains("ExportWorksheet"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CETSHNO"].ToString() == hsnNumber)
                        {
                            HSN.Add(dr["CETSHNO"].ToString());
                            HSN.Add(dr["Description"].ToString());
                            HSN.Add(dr["Unit of Measurement"].ToString());
                            HSN.Add(dr["IGST"].ToString());
                            HSN.Add(dr["CGST"].ToString());
                            HSN.Add(dr["SGST"].ToString());
                            HSN.Add(dr["Cess"].ToString());
                            HSN.Add(dr["IsNotified"].ToString());
                            
                            break;
                        }

                    }
                }
                //break;
            }

            return HSN;
        }

        /// <summary>
        ///  based on description of hsn numnber
        /// </summary>
        /// <param name="description"></param>
        /// <param name="data"></param>
        /// <param name="hsn"></param>
        /// <returns></returns>
        //private List<string> GetHSNDataFrmExl(string description, DataSet data, List<string> hsn)
        //{
        //    //ds is your dataset object
        //    List<string> HSN = new List<string>();

        //    foreach (DataTable dt in data.Tables)
        //    {
        //        //Searching for the table whose name contains "Export Worksheet"
        //        if (dt.TableName.Contains("Export Worksheet"))
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr["Description"].ToString() != null && dr["Description"].ToString() == description)
        //                {
        //                    HSN.Add(dr["Description"].ToString());
        //                    HSN.Add(dr["Unit of Measurement"].ToString());
        //                    HSN.Add(dr["IGST"].ToString());
        //                    HSN.Add(dr["CGST"].ToString());
        //                    HSN.Add(dr["SGST"].ToString());
        //                    HSN.Add(dr["Cess"].ToString());
        //                    HSN.Add(dr["IsNotified"].ToString());
        //                    HSN.Add(dr["Condition"].ToString());
        //                    break;
        //                }

        //            }
        //        }
        //        break;
        //    }

        //    return HSN;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        public List<Notified> GetHSNNotificationData(string hsnNumber)
        {
            // Code to check Notification excel , if IsNotified is true then only condition may b single/multiple
            // IsNotified is set to 1 for HSN number notified
            List<Notified> Notification = new List<Notified>();
            DataSet data = new DataSet();
            data = ReadExcelFile("CETSH");
            Notified notify = new Notified();
            foreach (DataTable dt in data.Tables)
            {

                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Notification$"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["HSN Number"]).ToString() == hsnNumber)
                        {
                            if (!string.IsNullOrEmpty(dr["SerialNo"].ToString()))
                                notify.SerialNo = Convert.ToInt32(dr["SerialNo"]);

                            notify.NotificationNo = dr["NotificationNo"].ToString();
                            notify.NotificationSerialNo = dr["NotificationSNo"].ToString();
                            notify.Description = dr["Descrioption"].ToString();
                            notify.Tax = Convert.ToDecimal(dr["Tarrif/Duty"]);
                            
                            Notification.Add(notify);
                            // y this line is added, to avoid overwriting of value
                            notify = new Notified();


                        }

                    }
                }
                //break;
            }



            return Notification;
        }

        /// <summary>
        /// TO DO 
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        public List<Condition> GetNotifiedHSNConditionsData(string hsnNumber)
        {
            List<Condition> Condition = new List<Condition>();
            DataSet data = new DataSet();
            data = ReadExcelFile("CETSH");
            Condition condition = new Condition();
            foreach (DataTable dt in data.Tables)
            {

                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Condition$"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["HSN Number"]).ToString() == hsnNumber)
                        {
                            if (!string.IsNullOrEmpty(dr["SerialNo"].ToString()))
                                condition.SerialNo = Convert.ToInt32(dr["SerialNo"]);

                            condition.ConditionNo = dr["NotificationNo"].ToString();
                            condition.ConditionSerialNo = dr["NotificationSNo"].ToString();
                            condition.Description = dr["Descrioption"].ToString();
                            condition.Tax = Convert.ToDecimal(dr["Tarrif/Duty"]);

                            Condition.Add(condition);
                            // y this line is added, to avoid overwriting of value
                            condition = new Condition();


                        }

                    }
                }
                //break;
            }



            return Condition;

        }


        #endregion

        //#region bool GetHSNInformation
        //public bool GetHSNInformation(string hsnNumber)
        //{
        //    DataSet data = new DataSet();
        //    List<string> HSN = new List<string>();
        //    data = ReadExcelFile("CETSH");
        //    HSN = GetHSNDataFrmExl(hsnNumber, data, HSN);
        //    return HSN;
        //}
        //#endregion

        #region GetSACInformation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sacNumber"></param>
        /// <returns></returns>
        public List<string> GetSACInformation(int sacNumber)
        {
            DataSet data = new DataSet();
            List<string> SAC = new List<string>();
            data = ReadExcelFile("SAC_Code");
            SAC = GetSACDataFrmExl(sacNumber, data, SAC);
            return SAC;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sacDescription"></param>
        /// <returns></returns>
        //public List<string> GetSACInformation(string sacDescription)
        //{
        //    DataSet data = new DataSet();
        //    List<string> SAC = new List<string>();
        //    data = ReadExcelFile("SAC_Code");
        //    SAC = GetSACDataFrmExl(sacDescription, data, SAC);
        //    return SAC;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="data"></param>
        /// <param name="hsn"></param>
        /// <returns></returns>
        private List<string> GetSACDataFrmExl(string description, DataSet data, List<string> sac)
        {
            //ds is your dataset object
            List<string> SAC = new List<string>();
            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Export Worksheet"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Description"].ToString() == description)
                        {
                            SAC.Add(dr["Description"].ToString());
                            SAC.Add(dr["Unit of Measurement"].ToString());
                            SAC.Add(dr["IGST"].ToString());
                            SAC.Add(dr["CGST"].ToString());
                            SAC.Add(dr["SGST"].ToString());
                            SAC.Add(dr["Cess"].ToString());
                        }
                    }
                }

            }
            return SAC;
        }

        private List<string> GetSACDataFrmExl(int sacNumber, DataSet data, List<string> sac)
        {
            //ds is your dataset object
            List<string> SAC = new List<string>();
            foreach (DataTable dt in data.Tables)
            {
                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Export Worksheet"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["Description"])== sacNumber)
                        {
                            SAC.Add(dr["Description"].ToString());
                            SAC.Add(dr["Unit of Measurement"].ToString());
                            SAC.Add(dr["IGST"].ToString());
                            SAC.Add(dr["CGST"].ToString());
                            SAC.Add(dr["SGST"].ToString());
                            SAC.Add(dr["Cess"].ToString());
                        }
                    }
                }

            }
            return SAC;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        public List<Notified> GetSACNotificationData(string sacNumber)
        {
            // Code to check Notification excel , if IsNotified is true then only condition may b single/multiple
            // IsNotified is set to 1 for HSN number notified
            List<Notified> Notification = new List<Notified>();
            DataSet data = new DataSet();
            data = ReadExcelFile("SAC_Code");
            Notified notify = new Notified();
            foreach (DataTable dt in data.Tables)
            {

                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Notification$"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["PRODUCT CODE"]).ToString() == sacNumber)
                        {
                            if (!string.IsNullOrEmpty(dr["SerialNo"].ToString()))
                                notify.SerialNo = Convert.ToInt32(dr["SerialNo"]);

                            notify.NotificationNo = dr["NotificationNo"].ToString();
                            notify.NotificationSerialNo = dr["NotificationSNo"].ToString();
                            notify.Description = dr["Descrioption"].ToString();
                            notify.Tax = Convert.ToDecimal(dr["Tarrif/Duty"]);

                            Notification.Add(notify);
                            // y this line is added, to avoid overwriting of value
                            notify = new Notified();


                        }

                    }
                }
                //break;
            }



            return Notification;
        }

        /// <summary>
        /// TO DO 
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        public List<Condition> GetNotifiedSACConditionsData(string sacNumber)
        {
            List<Condition> Condition = new List<Condition>();
            DataSet data = new DataSet();
            data = ReadExcelFile("SAC_Code");
            Condition condition = new Condition();
            foreach (DataTable dt in data.Tables)
            {

                //Searching for the table whose name contains "login"
                if (dt.TableName.Contains("Condition$"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["PRODUCT CODE"]).ToString() == sacNumber)
                        {
                            if (!string.IsNullOrEmpty(dr["SerialNo"].ToString()))
                                condition.SerialNo = Convert.ToInt32(dr["SerialNo"]);

                            condition.ConditionNo = dr["NotificationNo"].ToString();
                            condition.ConditionSerialNo = dr["NotificationSNo"].ToString();
                            condition.Description = dr["Descrioption"].ToString();
                            condition.Tax = Convert.ToDecimal(dr["Tarrif/Duty"]);

                            Condition.Add(condition);
                            // y this line is added, to avoid overwriting of value
                            condition = new Condition();


                        }

                    }
                }
                //break;
            }



            return Condition;

        }
        #endregion

        #region ConvertNumbertoWords
        public static string ConvertNumbertoWords(int number)
        {

            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static string ConvertNumbertoWords(decimal number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return "MINUS " + ConvertNumbertoWords(Math.Abs(number));

            string words = String.Empty;

            long intPortion = (long)number;
            decimal fraction = (number - intPortion);
            int decimalPrecision = GetDecimalPrecision(number);

            fraction = CalculateFraction(decimalPrecision, fraction);

            long decPortion = (long)fraction;

            words = IntToWords(intPortion);
            if (decPortion > 0)
            {
                words += " POINT ";
                words += IntToWords(decPortion);
            }

            return words.Trim();
        }

        public static string IntToWords(long number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return "MINUS " + IntToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000000000) > 0)
            {
                words += IntToWords(number / 1000000000000000) + " QUADRILLION ";
                number %= 1000000000000000;
            }

            if ((number / 1000000000000) > 0)
            {
                words += IntToWords(number / 1000000000000) + " TRILLION ";
                number %= 1000000000000;
            }

            if ((number / 1000000000) > 0)
            {
                words += IntToWords(number / 1000000000) + " BILLION ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += IntToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += IntToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += IntToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != String.Empty)
                    words += "AND ";

                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        private static int GetDecimalPrecision(decimal number)
        {
            return (Decimal.GetBits(number)[3] >> 16) & 0x000000FF;
        }

        private static decimal CalculateFraction(int decimalPrecision, decimal fraction)
        {
            switch (decimalPrecision)
            {
                case 1:
                    return fraction * 10;
                case 2:
                    return fraction * 100;
                case 3:
                    return fraction * 1000;
                case 4:
                    return fraction * 10000;
                case 5:
                    return fraction * 100000;
                case 6:
                    return fraction * 1000000;
                case 7:
                    return fraction * 10000000;
                case 8:
                    return fraction * 100000000;
                case 9:
                    return fraction * 1000000000;
                case 10:
                    return fraction * 10000000000;
                case 11:
                    return fraction * 100000000000;
                case 12:
                    return fraction * 1000000000000;
                case 13:
                    return fraction * 10000000000000;
                default:
                    return fraction * 10000000000000;
            }
        }
        #endregion

        #region SAVE_INVOICE
        private int ExistInvoiceNo(string strInvNo)
        {
            int result = 0;

            string FilePath = "InvoiceData";

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {

                    cn.Open();


                    OleDbCommand cmd7 = new OleDbCommand("SELECT InvoiceNo FROM [SellerInvoiceData$] where InvoiceNo ='" + strInvNo + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = 0;
                    }
                    else
                        result = 1;
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }

        private int IsExistExportNo(string strInvNo)
        {
            int result = 0;

            string FilePath = "Export";

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {

                    cn.Open();


                    OleDbCommand cmd7 = new OleDbCommand("SELECT ExportNo FROM [SellerInvoiceData$] where ExportNo ='" + strInvNo + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = 0;
                    }
                    else
                        result = 1;
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }
        private int IsExistVoucherNo(string strInvNo)
        {
            int result = 0;

            string FilePath = "AdvancePayment";

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {

                    cn.Open();


                    OleDbCommand cmd7 = new OleDbCommand("SELECT VoucherNo FROM [SellerInvoiceData$] where VoucherNo ='" + strInvNo + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = 0;
                    }
                    else
                        result = 1;
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }
        private int UpdateInvoiceSeed(Seller seller)
        {
            int result = 0; int CurrntSrlNo = 0;
            CurrntSrlNo = seller.SerialNoInvoice.CurrentSrlNo + 1;


            string FilePath = "Login";


            string connectionString = GetConnectionString(FilePath);
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd6 = new OleDbCommand("Update [InvoiceSeed$] set CurrentSrlNo ='" + CurrntSrlNo + "' where UserName='" + seller.GSTIN + "'", cn);

                    result = cmd6.ExecuteNonQuery();
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }
       
        public int SaveInvoiceData(Seller seller)
        {

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            bool IsInter; string isInter = string.Empty;

            foreach (LineEntry le in seller.Invoice.LineEntry)
            {
               
                seller.TotalAmount += le.TotalLineIDWise;
                seller.TotalDiscount += le.Discount;
                seller.TotalQty += le.Qty;
                seller.TotalRate += le.PerUnitRate;
                


                /* Isintra calculation*/

                IsInter = le.IsInter;
                if (IsInter == true)
                {
                    isInter = "True";
                }
                else if (IsInter == false)
                {
                    isInter = "False";
                }
                /**/
            }




            seller.GrandTotalAmount = seller.Invoice.Freight + seller.Invoice.Insurance + seller.Invoice.PackingAndForwadingCharges + seller.TotalAmountWithTax;

            seller.GrandTotalAmountInWord = ConvertNumbertoWords(Convert.ToDecimal(seller.GrandTotalAmount));

            int result = 0;
            if (seller.Invoice.IsAdvancePaymentChecked == true)
            {
                #region ADVANCE_PAYMENT
                result = IsExistVoucherNo(seller.SellerInvoice);
                if (result == 1)
                {
                    FilePath = "AdvancePayment";

                    connectionString = GetConnectionString(FilePath);


                    using (OleDbConnection cn1 = new OleDbConnection(connectionString))
                    {
                        cn1.Open();

                        try
                        {
                            
                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([VoucherNo],[Voucherdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[IsInter]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23)", cn1);
                            cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value4", seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value6", seller.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value7", seller.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value8", seller.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value9", seller.Address);
                            cmd1.Parameters.AddWithValue("@value10", seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value11", seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value12", seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value13", seller.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value14", seller.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", seller.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value17", seller.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value18", "False");
                            cmd1.Parameters.AddWithValue("@value19", "");
                            cmd1.Parameters.AddWithValue("@value20", "");
                            cmd1.Parameters.AddWithValue("@value21", "False");                        
                            cmd1.Parameters.AddWithValue("@value22", "Advance");
                            cmd1.Parameters.AddWithValue("@value23", isInter);
                          
                            result = cmd1.ExecuteNonQuery();
                            #endregion
                            if (result > 0)
                            {
                                result = 0;
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([VoucherNo],[ReceiverGSTN],[ReceiverName],[ReceiverAddress],[ReceiverStateCode],[ReceiverStateName],[ReceiverStateCodeID]) " +
                                   "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn1);
                                    cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", seller.Reciever.Address);
                                    cmd2.Parameters.AddWithValue("@value5", seller.Reciever.StateCode);
                                    cmd2.Parameters.AddWithValue("@value6", seller.Reciever.StateName);
                                    cmd2.Parameters.AddWithValue("@value7", seller.Reciever.StateCodeID);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion
                                    #region Consignee
                                    if (result == 1)
                                    {
                                        result = 0;
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([VoucherNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeAddress],[ConsigneeStateCode],[ConsigneeStateName],[ConsigneeStateCodeID]) " +
                                           "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn1);
                                            cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", seller.Consignee.Address);
                                            cmd3.Parameters.AddWithValue("@value5", seller.Consignee.StateCode);
                                            cmd3.Parameters.AddWithValue("@value6", seller.Consignee.StateName);
                                            cmd3.Parameters.AddWithValue("@value7", seller.Consignee.StateCodeID);
                                            result = cmd3.ExecuteNonQuery();
                                    #endregion
                                            if (result > 0)
                                            {
                                                result = 0;
                                                try
                                                {
                                                    #region LineEntry
                                                    foreach (LineEntry le in seller.Invoice.LineEntry)
                                                    {

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                        "([VoucherNo],[Voucherdate],[SellerGSTN],[ReceiverGSTN],[InvoiceSeed],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess],[IsInter]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45)", cn1);

                                                        cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value4", seller.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", seller.SerialNoInvoice.CurrentSrlNo);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", seller.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", seller.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", seller.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", seller.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", seller.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", seller.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", seller.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", seller.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", seller.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", seller.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", seller.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", le.HSN.IsNotified);
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", "False");
                                                        cmd4.Parameters.AddWithValue("@value44", le.HSN.Cess);
                                                        cmd4.Parameters.AddWithValue("@value45", isInter);
                                                        result = cmd4.ExecuteNonQuery();


                                                    }
                                                    #endregion
                                                 
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            else
                                            {
                                                result = 0;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                { }

                            }
                            else
                            {
                                result = 0;
                            }


                        }
                        catch
                        { }
                        finally
                        { cn1.Close(); }
                        return result;
                    }


                }
                else
                {
                    result = 3;
                }
                #endregion
            }
            else if (seller.Invoice.IsExportChecked == true)
            {
                #region Export
                result = IsExistExportNo(seller.SellerInvoice);
                if (result == 1)
                {
                    FilePath = "Export";

                    connectionString = GetConnectionString(FilePath);


                    using (OleDbConnection cn2 = new OleDbConnection(connectionString))
                    {
                        cn2.Open();

                        try
                        {
                           
                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([ExportNo],[Exportdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22)", cn2);
                            cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value4", seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value6", seller.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value7", seller.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value8", seller.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value9", seller.Address);
                            cmd1.Parameters.AddWithValue("@value10", seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value11", seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value12", seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value13", seller.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value14", seller.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", seller.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value17", seller.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value18", "False");
                            cmd1.Parameters.AddWithValue("@value19", "");
                            cmd1.Parameters.AddWithValue("@value20", "");                         
                            cmd1.Parameters.AddWithValue("@value21", "False");
                            cmd1.Parameters.AddWithValue("@value22", "EXPORT");
                          
                            result = cmd1.ExecuteNonQuery();
                            #endregion
                            if (result == 1)
                            {
                                result = 0;
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([ExportNo],[ReceiverGSTN],[ReceiverName],[ReceiverAddress],[ReceiverStateCode],[ReceiverStateName],[ReceiverStateCodeID]) " +
                                   "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                    cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", seller.Reciever.Address);
                                    cmd2.Parameters.AddWithValue("@value5", seller.Reciever.StateCode);
                                    cmd2.Parameters.AddWithValue("@value6", seller.Reciever.StateName);
                                    cmd2.Parameters.AddWithValue("@value7", seller.Reciever.StateCodeID);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion
                                    #region Consignee
                                    if (result == 1)
                                    {
                                        result = 0;
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([ExportNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeAddress],[ConsigneeStateCode],[ConsigneeStateName],[ConsigneeStateCodeID]) " +
                                           "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                            cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", seller.Consignee.Address);
                                            cmd3.Parameters.AddWithValue("@value5", seller.Consignee.StateCode);
                                            cmd3.Parameters.AddWithValue("@value6", seller.Consignee.StateName);
                                            cmd3.Parameters.AddWithValue("@value7", seller.Consignee.StateCodeID);
                                            result = cmd3.ExecuteNonQuery();
                                    #endregion
                                            if (result == 1)
                                            {
                                                result = 0;
                                                try
                                                {
                                                    foreach (LineEntry le in seller.Invoice.LineEntry)
                                                    {
                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                        "([ExportNo],[Exportdate],[SellerGSTN],[ReceiverGSTN],[InvoiceSeed],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44)", cn2);

                                                        cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value4", seller.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", seller.SerialNoInvoice.CurrentSrlNo);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", seller.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", seller.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", seller.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", seller.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", seller.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", seller.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", seller.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", seller.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", seller.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", seller.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", seller.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", le.HSN.IsNotified);
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", "False");
                                                         cmd4.Parameters.AddWithValue("@value43", le.HSN.Cess);

                                                        result = cmd4.ExecuteNonQuery();


                                                    }
                                                    
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            else
                                            {
                                                result = 0;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                { }

                            }
                            else
                            {
                                result = 0;
                            }


                        }
                        catch
                        { }
                        finally
                        { cn2.Close(); }
                        return result;
                    }


                }
                else
                {
                    result = 2;
                }
                #endregion
            }
            else
            {
                #region B2B
                result = ExistInvoiceNo(seller.SellerInvoice);
                if (result == 1)
                {
                    FilePath = "InvoiceData";

                    connectionString = GetConnectionString(FilePath);


                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {

                            #region sellerData                          

                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([InvoiceNo],[Invoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[IsInter],[ReceiverStateName],[ConsigneeStateName],[ReceiverFinancialPeriod]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28)", cn);
                            cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value4", seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value6", seller.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value7", seller.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value8", seller.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value9", seller.Address);
                            cmd1.Parameters.AddWithValue("@value10", seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value11", seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value12", seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value13", seller.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value14", seller.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", seller.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value17", seller.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value18", "False");
                            cmd1.Parameters.AddWithValue("@value19", "");
                            cmd1.Parameters.AddWithValue("@value20", "");
                            cmd1.Parameters.AddWithValue("@value21", "False");                         
                            cmd1.Parameters.AddWithValue("@value22", "B2B");              
                            cmd1.Parameters.AddWithValue("@value23", seller.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value24", seller.SellerFinancialPeriod);                          
                            cmd1.Parameters.AddWithValue("@value25", isInter);
                            cmd1.Parameters.AddWithValue("@value26", seller.Reciever.StateName);
                            cmd1.Parameters.AddWithValue("@value27", seller.Consignee.StateName);
                            cmd1.Parameters.AddWithValue("@value28", seller.Reciever.FinancialPeriod);
                            result = cmd1.ExecuteNonQuery();
                            #endregion
                            if (result == 1)
                            {
                                result = 0;
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverAddress],[ReceiverStateCode],[ReceiverStateName],[ReceiverStateCodeID],[ReceiverFinancialPeriod]) " +
                                   "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7,@value8)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", seller.Reciever.Address);
                                    cmd2.Parameters.AddWithValue("@value5", seller.Reciever.StateCode);
                                    cmd2.Parameters.AddWithValue("@value6", seller.Reciever.StateName);
                                    cmd2.Parameters.AddWithValue("@value7", seller.Reciever.StateCodeID);
                                    cmd2.Parameters.AddWithValue("@value8", seller.Reciever.FinancialPeriod);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion
                                    #region Consignee
                                    if (result == 1)
                                    {
                                        result = 0;
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeAddress],[ConsigneeStateCode],[ConsigneeStateName],[ConsigneeStateCodeID]) " +
                                           "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", seller.Consignee.Address);
                                            cmd3.Parameters.AddWithValue("@value5", seller.Consignee.StateCode);
                                            cmd3.Parameters.AddWithValue("@value6", seller.Consignee.StateName);
                                            cmd3.Parameters.AddWithValue("@value7", seller.Consignee.StateCodeID);
                                            result = cmd3.ExecuteNonQuery();
                                    #endregion
                                            if (result == 1)
                                            {
                                                result = 0;
                                                try
                                                {
                                                    foreach (LineEntry le in seller.Invoice.LineEntry)
                                                    {

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                        "([InvoiceNo],[Invoicedate],[SellerGSTN],[ReceiverGSTN],[InvoiceSeed],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[IsInter],[ConsigneeStateCode],[Cess]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value4", seller.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", seller.SerialNoInvoice.CurrentSrlNo);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", seller.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", seller.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", seller.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", seller.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", seller.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", seller.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", seller.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", seller.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", seller.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", seller.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", seller.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", le.HSN.IsNotified);
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", "False");
                                                        cmd4.Parameters.AddWithValue("@value44", isInter);                                                     
                                                        cmd4.Parameters.AddWithValue("@value45", seller.Consignee.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value46", le.HSN.Cess);

                                                        result = cmd4.ExecuteNonQuery();


                                                    }
                                                    if (result > 0)
                                                    {

                                                        result = UpdateInvoiceSeed(seller);

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            else
                                            {
                                                result = 0;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                { }

                            }
                            else
                            {
                                result = 0;
                            }


                        }
                        catch
                        { }
                        finally
                        { cn.Close(); }
                        return result;
                    }


                }
                else
                {
                    result = 0;
                }
                #endregion
            }

            return result;

        }
        #endregion

        #region MatchExpression
        /// <summary>
        /// Methods declarea a regular expression to check whether or not 15 characters contains
        /// alpha numeric characters or not.
        /// </summary>
        /// <param name="gstinUserName"></param>
        /// <returns></returns>
        bool MatchExpression(string gstinUserName)
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

            Regex userNameRegex = new Regex(userNamePattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match userNameMatch = userNameRegex.Match(gstinUserName);

            if (userNameMatch.Success)
                return true;
            else
                return false;
        }
        #endregion

        #region CheckGSTIN
        /// <summary>
        /// function checks for validity of the string at server side 
        /// will need a implement in client side via scripting
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public bool CheckGSTIN(string gstinNumber)
        {
            // below value checks whether or not gstin is registered in our DB
            bool userNameRegisterdStatus = false;
            // valid gstin number is 15 char length
            if (gstinNumber.Length <= 14)
            {
                userNameRegisterdStatus = false;
                return userNameRegisterdStatus;
            }
            else
            {
                // checks whether it is a len of 15 char all alphanumeric
                userNameRegisterdStatus = MatchExpression(gstinNumber);
                if (userNameRegisterdStatus)
                {
                    // method to check 
                    // first two character , convert to int value -- has to equal to 1 and less than 37


                     DataSet data = new DataSet();
                    // hardcoding done because to direct to this excel
                    //data = ReadExcelFile("Login");
                     userNameRegisterdStatus = CheckGSTINRegisteredStatus(gstinNumber);
                    return userNameRegisterdStatus;

                }
   
            }
            return userNameRegisterdStatus;

            
        }

        /// <summary>
        /// Check whether the username is registerd in GSTIN excel , login page
        /// Ignore the case of UserName /GSTIN number
        /// </summary>
        /// <param name="data"></param>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        private bool CheckGSTINRegisteredStatus(string gstinNumber)
        {
            DataSet ds = new DataSet();
            bool chkGSTN = false;
            List<string> sellerDetails = new List<string>();
            string filePath = "Login";
            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand(" SELECT * FROM [SellerCreateInvoice$] WHERE GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds, "SellerCreateInvoice");


                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Seller is not Created !!!");
                    }



                    foreach (DataTable dt in ds.Tables)
                    {
                        //Searching for the table whose name contains "login"
                        if (dt.TableName.Contains("SellerCreateInvoice"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["GSTIN"].ToString().Equals(gstinNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    chkGSTN = true;
                                    break;

                                }
                            }
                        }
                    }


                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Seller is not registered");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller is not registered :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Seller is not registered :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
           


            //ds is your dataset object
           
            return chkGSTN;
        }
        #endregion

        
        #region UPDATE_LOGIN
        /// <summary>
        /// This method gets called in two scenarios
        /// when authentication is succesful , then for that particular gstinNumber (username) ,IsLoggedIn is set to 1
        /// when 
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <param name="isLoggedStatus"></param>
        /// <returns></returns>
        public int UpdateLogin(string gstinNumber)
        {
            // if the update is success, then it will b 1
            int result = 0;
            
            // current status , coz 0 for not login , 1 for logged in
            int IsLoggedIn=-1;
            
            // reading the values from config file
            string LoggedIn= WebConfigurationManager.AppSettings["LoggedInStatus"].ToString();
            string LoggedOut = WebConfigurationManager.AppSettings["NotLoggedInStatus"].ToString();

            // need to pass the sheet name for the code to search in GSTIN Excel
            string fileName = "Login";
            string connectionString1 = GetConnectionString(fileName);

            using (OleDbConnection cn = new OleDbConnection(connectionString1))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT IsLoggedIn FROM [Login$] where GSTIN ='" + gstinNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    // change this : Excel is later discarded , so did not do it.
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        IsLoggedIn = Convert.ToInt32(ds.Tables[0].Rows[0]["IsLoggedIn"]);
                    }
                    if ( IsLoggedIn == 0)
                    {

                        OleDbCommand cmd1 = new OleDbCommand("Update [Login$] set IsLoggedIn ='" + LoggedIn + "' where UserName='" + gstinNumber + "'", cn);

                         return result = cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [Login$] set IsLoggedIn ='" + LoggedOut + "' where UserName='" + gstinNumber + "' and IsLoggedIn =1", cn);

                        return result = cmd1.ExecuteNonQuery();
                       
                    }
                }
                catch
                {

                }
                finally
                {

                }
                return result;
            }
        }
        #endregion

        //HARDCORE//HARDCORE
        #region UPDATE_LOGOUT
        public int UpdateLogOut(String strGSTIN)
        {
            int result = 0;
            string fileName = "Login";

            string connectionString = GetConnectionString(fileName);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [Login$] set IsLoggedIn =0 where UserName='" + strGSTIN + "' and IsLoggedIn =1", cn);

                    result = cmd.ExecuteNonQuery();

                }
                catch
                {

                }
                finally
                {

                }
                return result;
            }
        }
        #endregion
        //HARDCORE
        #region FETCH_INVOICE_PREVIEW_DATA
        public DataSet FetchInvoicePreviewData(string UniqueNo, string flag)
        {
            DataSet ds = new DataSet();
            string FilePath = string.Empty;
            string connectionString = string.Empty;

            DataTable dtSeller = new DataTable("SellerDtls");
            DataTable dtRec = new DataTable("ReceiverDtls");
            DataTable dtCon = new DataTable("ConsigneeDtls");
            DataTable dtinv = new DataTable("InvoiceDtls");
            DataTable dtTotalAmy = new DataTable("AmountDtls");
            DataTable dtTotalWord = new DataTable("AmountInWords");
            DataTable dtLogindtls = new DataTable("Designation");
            string query = string.Empty;

            if (flag == "A")
            {
                #region ADVANCE
                FilePath = "AdvancePayment";

                connectionString = GetConnectionString(FilePath);

                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        string strSellerGSTIN = string.Empty;
                        cn.Open();
                        query = "SELECT * FROM [SellerInvoiceData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbCommand cmd = new OleDbCommand(query, cn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dtSeller);
                        ds.Tables.Add(dtSeller);
                        if (dtSeller.Rows.Count > 0)
                        {
                            strSellerGSTIN = Convert.ToString(dtSeller.Rows[0][2]).Trim();

                        }

                        cmd.CommandText = "SELECT * FROM [ReceiverInvoiceData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                        da1.Fill(dtRec);
                        ds.Tables.Add(dtRec);

                        cmd.CommandText = "SELECT * FROM [ConsigneeInvoiceData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                        da2.Fill(dtCon);
                        ds.Tables.Add(dtCon);

                        cmd.CommandText = "SELECT LineID,Description,HSN,Qty,Unit,Rate,Total,Discount,AmountWithTax,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,IGSTRate,IGSTAmt FROM [HSNData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                        da3.Fill(dtinv);
                        ds.Tables.Add(dtinv);

                        cmd.CommandText = "SELECT  '','','','','','',TotalAmount,TotalDiscount,TotalAmountWithTax,'',TotalCGSTAmount,'',TotalSGSTAmount,'',TotalIGSTAmount FROM [HSNData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da4 = new OleDbDataAdapter(cmd);
                        da4.Fill(dtTotalAmy);
                        ds.Tables.Add(dtTotalAmy);

                        cmd.CommandText = "SELECT GrandTotalAmount,GrandTotalAmountInWord FROM [HSNData$] where VoucherNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da5 = new OleDbDataAdapter(cmd);
                        da5.Fill(dtTotalWord);
                        ds.Tables.Add(dtTotalWord);


                        string fileName1 = "Login";
                        string connectionString1 = GetConnectionString(fileName1);

                        cn.Close();
                        using (OleDbConnection cn1 = new OleDbConnection(connectionString1))
                        {
                            try
                            {
                                cn1.Open();
                                OleDbCommand cmd1 = new OleDbCommand("SELECT  NameOfTheSignatory,Designation FROM [Login$] where UserName ='" + strSellerGSTIN + "'", cn1);
                                OleDbDataAdapter da6 = new OleDbDataAdapter(cmd1);
                                da6.Fill(dtLogindtls);
                                ds.Tables.Add(dtLogindtls);

                            }
                            catch
                            {

                            }
                            finally
                            {
                                cn1.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                    return ds;
                }
                #endregion
            }
            else if (flag == "E")
            {
                #region EXPORT
                FilePath = "Export";

                connectionString = GetConnectionString(FilePath);

                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        string strSellerGSTIN = string.Empty;
                        cn.Open();
                        query = "SELECT * FROM [SellerInvoiceData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbCommand cmd = new OleDbCommand(query, cn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dtSeller);
                        ds.Tables.Add(dtSeller);
                        if (dtSeller.Rows.Count > 0)
                        {
                            strSellerGSTIN = Convert.ToString(dtSeller.Rows[0][2]).Trim();

                        }

                        cmd.CommandText = "SELECT * FROM [ReceiverInvoiceData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                        da1.Fill(dtRec);
                        ds.Tables.Add(dtRec);

                        cmd.CommandText = "SELECT * FROM [ConsigneeInvoiceData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                        da2.Fill(dtCon);
                        ds.Tables.Add(dtCon);

                        cmd.CommandText = "SELECT LineID,Description,HSN,Qty,Unit,Rate,Total,Discount,AmountWithTax,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,IGSTRate,IGSTAmt FROM [HSNData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                        da3.Fill(dtinv);
                        ds.Tables.Add(dtinv);

                        cmd.CommandText = "SELECT  '','','','','','',TotalAmount,TotalDiscount,TotalAmountWithTax,'',TotalCGSTAmount,'',TotalSGSTAmount,'',TotalIGSTAmount FROM [HSNData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da4 = new OleDbDataAdapter(cmd);
                        da4.Fill(dtTotalAmy);
                        ds.Tables.Add(dtTotalAmy);

                        cmd.CommandText = "SELECT GrandTotalAmount,GrandTotalAmountInWord FROM [HSNData$] where ExportNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da5 = new OleDbDataAdapter(cmd);
                        da5.Fill(dtTotalWord);
                        ds.Tables.Add(dtTotalWord);


                        string fileName1 = "Login";
                        string connectionString1 = GetConnectionString(fileName1);

                        cn.Close();
                        using (OleDbConnection cn1 = new OleDbConnection(connectionString1))
                        {
                            try
                            {
                                cn1.Open();
                                OleDbCommand cmd1 = new OleDbCommand("SELECT  NameOfTheSignatory,Designation FROM [Login$] where UserName ='" + strSellerGSTIN + "'", cn1);
                                OleDbDataAdapter da6 = new OleDbDataAdapter(cmd1);
                                da6.Fill(dtLogindtls);
                                ds.Tables.Add(dtLogindtls);

                            }
                            catch
                            {

                            }
                            finally
                            {
                                cn1.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                    return ds;
                }
                #endregion
            }
            else
            {
                #region B2B
                FilePath = "InvoiceData";

                connectionString = GetConnectionString(FilePath);

                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        string strSellerGSTIN = string.Empty;
                        cn.Open();
                        query = "SELECT * FROM [SellerInvoiceData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbCommand cmd = new OleDbCommand(query, cn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dtSeller);
                        ds.Tables.Add(dtSeller);
                        if (dtSeller.Rows.Count > 0)
                        {
                            strSellerGSTIN = Convert.ToString(dtSeller.Rows[0][2]).Trim();

                        }

                        cmd.CommandText = "SELECT * FROM [ReceiverInvoiceData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                        da1.Fill(dtRec);
                        ds.Tables.Add(dtRec);

                        cmd.CommandText = "SELECT * FROM [ConsigneeInvoiceData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                        da2.Fill(dtCon);
                        ds.Tables.Add(dtCon);

                        cmd.CommandText = "SELECT LineID,Description,HSN,Qty,Unit,Rate,Total,Discount,AmountWithTax,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,IGSTRate,IGSTAmt,TaxableValue FROM [HSNData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                        da3.Fill(dtinv);
                        ds.Tables.Add(dtinv);

                        cmd.CommandText = "SELECT  '','','','','','',TotalAmount,TotalDiscount,TotalAmountWithTax,'',TotalCGSTAmount,'',TotalSGSTAmount,'',TotalIGSTAmount FROM [HSNData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da4 = new OleDbDataAdapter(cmd);
                        da4.Fill(dtTotalAmy);
                        ds.Tables.Add(dtTotalAmy);

                        cmd.CommandText = "SELECT GrandTotalAmount,GrandTotalAmountInWord FROM [HSNData$] where InvoiceNo ='" + UniqueNo + "'";
                        OleDbDataAdapter da5 = new OleDbDataAdapter(cmd);
                        da5.Fill(dtTotalWord);
                        ds.Tables.Add(dtTotalWord);


                        string fileName1 = "Login";
                        string connectionString1 = GetConnectionString(fileName1);

                        cn.Close();
                        using (OleDbConnection cn1 = new OleDbConnection(connectionString1))
                        {
                            try
                            {
                                cn1.Open();
                                OleDbCommand cmd1 = new OleDbCommand("SELECT  NameOfTheSignatory,Designation FROM [Login$] where UserName ='" + strSellerGSTIN + "'", cn1);
                                OleDbDataAdapter da6 = new OleDbDataAdapter(cmd1);
                                da6.Fill(dtLogindtls);
                                ds.Tables.Add(dtLogindtls);

                            }
                            catch
                            {

                            }
                            finally
                            {
                                cn1.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                    return ds;
                }
                #endregion
            }


        }
        #endregion

        #region PopulateGSTR1Dtls
        public DataSet PopulateGSTR1Dtls(string SellerGSTIN)
        {
            DataSet ds = new DataSet();
            DataTable dtSeller = new DataTable();
            DataTable dtRec = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            FilePath = "InvoiceData";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd1 = new OleDbCommand("SELECT distinct ReceiverGSTN,ReceiverName FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "'", cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                    da1.Fill(dtRec);
                    if (dtRec.Rows.Count > 0)
                    {
                        ds.Tables.Add(dtRec);
                    }

                    OleDbCommand cmd2 = new OleDbCommand("SELECT distinct SellerGSTN,SellerName,SellerGrossTurnOver,SellerFinancialPeriod FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "'", cn);
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd2);
                    da2.Fill(dtSeller);

                    if (dtSeller.Rows.Count > 0)
                    {
                        ds.Tables.Add(dtSeller);
                    }
                }
                catch
                {

                }
                finally
                {

                }
                return ds;
            }
        }
        #endregion

        //HARDCORE
        #region PopulateGSTR2ADtls
        public DataSet PopulateGSTR2ADtls(string ReceiverGSTIN)
        {
            DataSet ds = new DataSet();
            DataTable dtRec = new DataTable("dtReceiverdtls");

            string fileName = "InvoiceData";

            string connectionString = GetConnectionString(fileName);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    string strSellerGSTIN = string.Empty;
                    cn.Open();

                    OleDbCommand cmd1 = new OleDbCommand("SELECT distinct ReceiverGSTN,ReceiverName,ReceiverFinancialPeriod FROM [ReceiverInvoiceData$] where ReceiverGSTN ='" + ReceiverGSTIN + "'", cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                    da1.Fill(dtRec);
                    ds.Tables.Add(dtRec);
                }
                catch
                {

                }
                finally
                {

                }
                return ds;
            }
        }
        #endregion

        #region Encrypt_Data
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        #endregion

        //amits
        #region Decrypt_Data
        //public string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    cipherText = cipherText.Replace(" ", "+");
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
        #endregion

        #region ViewInvoice
        public DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();

            ds = GetSellerInvoiceDetails(SellerGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerInvoiceDetails
        public DataSet GetSellerInvoiceDetails(string SellerGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            DataTable dtInvSummery = new DataTable("SummeryDtls");
            /*END  B2B invoice */
            FilePath = "InvoiceData";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Invoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Invoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);


                    cmd.CommandText = "SELECT distinct InvoiceNo,Invoicedate,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Invoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtInvSummery);
                    ds.Tables.Add(dtInvSummery);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region AdvancePayment
            /*START  B2B invoice */
            /*START  AdvancePayment */
            DataTable dtAdvanceSellerDtls = new DataTable("AdvanceSellerDtls");
            DataTable dtAdvanceInvoice = new DataTable("AdvanceInvoiceDtls");
            DataTable dtAdvanceInvSummery = new DataTable("AdvanceSummeryDtls");
            /*END  AdvancePayment */

            FilePath = "AdvancePayment";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Voucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                    da3.Fill(dtAdvanceSellerDtls);
                    ds.Tables.Add(dtAdvanceSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Voucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da4 = new OleDbDataAdapter(cmd);
                    da4.Fill(dtAdvanceInvoice);
                    ds.Tables.Add(dtAdvanceInvoice);


                    cmd.CommandText = "SELECT distinct VoucherNo,Voucherdate,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Voucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da5 = new OleDbDataAdapter(cmd);
                    da5.Fill(dtAdvanceInvSummery);
                    ds.Tables.Add(dtAdvanceInvSummery);

                }
                catch
                {

                }
                finally
                {

                }

            }
            #endregion

            #region EXPORT
            /*START  EXPORT */
            DataTable dtEXPORTSellerDtls = new DataTable("EXPORTSellerDtls");
            DataTable dtEXPORTInvoice = new DataTable("EXPORTInvoiceDtls");
            DataTable dtEXPORTInvSummery = new DataTable("EXPORTSummeryDtls");
            /*END  EXPORT */

            FilePath = "Export";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Exportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtEXPORTSellerDtls);
                    ds.Tables.Add(dtEXPORTSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Exportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtEXPORTInvoice);
                    ds.Tables.Add(dtEXPORTInvoice);


                    cmd.CommandText = "SELECT distinct ExportNo,Exportdate,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsElectronicReferenceNoGenerated ='False' and  Exportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtEXPORTInvSummery);
                    ds.Tables.Add(dtEXPORTInvSummery);

                }
                catch
                {

                }
                finally
                {

                }

            }
            #endregion
            return ds;
        }




        #endregion

        #endregion

        #region UploadInvoice
        public int UploadInvoice(List<Seller> SellerDaTa, string strInvoiceNo)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Seller seller = new Seller();
          
            seller.SellerDaTa = new List<Seller>();

            seller.SellerDaTa = SellerDaTa;

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            bool IsInter; string isInter = string.Empty;

            foreach (var item in seller.SellerDaTa)
            {
                //TO UPLOAD A PERTICULAR INVOICE

                if (strInvoiceNo != "")
                {
                    #region UPLOAD_SINGLE_INVOICE
                    if (item.SellerInvoice == strInvoiceNo)
                    {
                        #region ADVANCE
                        if (item.Invoice.IsAdvancePaymentChecked == true)
                        {
                            FilePath = "AdvanceUploadInvoice";
                            connectionString = GetConnectionString(FilePath);

                            foreach (LineEntry le in item.Invoice.LineEntry)
                            {
                                IsInter = le.IsInter;
                                if (IsInter == true)
                                {
                                    isInter = "True";
                                }
                                else if (IsInter == false)
                                {
                                    isInter = "False";
                                }
                            }
                            using (OleDbConnection cn = new OleDbConnection(connectionString))
                            {
                                cn.Open();

                                try
                                {
                                    #region sellerData

                                    OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                         "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[IsFileGSTR1],[IsInter]) " +
                                         "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25)", cn);
                                    cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                    cmd1.Parameters.AddWithValue("@value3", Todt);
                                    cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value10", item.Address);
                                    cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                    cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                    cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                    cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                    cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                    cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                    cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                    cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                                    cmd1.Parameters.AddWithValue("@value19", "True");
                                    cmd1.Parameters.AddWithValue("@value20", "AdvanceACK01");
                                    cmd1.Parameters.AddWithValue("@value21", Todt);
                                    cmd1.Parameters.AddWithValue("@value22", "False");
                                    cmd1.Parameters.AddWithValue("@value23", "ADVANCE");
                                    cmd1.Parameters.AddWithValue("@value24", "False");
                                    cmd1.Parameters.AddWithValue("@value25", isInter);
                                    result = cmd1.ExecuteNonQuery();

                                    #endregion

                                    if (result > 0)
                                    {
                                        #region Receiver
                                        try
                                        {
                                            OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                           "([VoucherNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                            cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                            cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                            result = cmd2.ExecuteNonQuery();
                                        #endregion

                                            if (result > 0)
                                            {

                                                #region Consignee
                                                try
                                                {
                                                    OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                                   "([VoucherNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                    cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                    cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                    cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                    cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                    result = cmd3.ExecuteNonQuery();

                                                #endregion

                                                    #region LineEntry
                                                    if (result > 0)
                                                    {
                                                        try
                                                        {

                                                            foreach (LineEntry le in item.Invoice.LineEntry)
                                                            {

                                                                OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                              "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess],[IsFileGSTR1],[IsInter]) " +
                                                                "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46)", cn);

                                                                cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                                cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                                cmd4.Parameters.AddWithValue("@value3", Todt);
                                                                cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                                cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                                cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                                cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                                cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                                cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                                cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                                cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                                cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                                cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                                cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                                cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                                cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                                cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                                cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                                cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                                cmd4.Parameters.AddWithValue("@value33", "False");
                                                                cmd4.Parameters.AddWithValue("@value34", "False");
                                                                cmd4.Parameters.AddWithValue("@value35", "False");
                                                                cmd4.Parameters.AddWithValue("@value36", "False");
                                                                cmd4.Parameters.AddWithValue("@value37", "False");
                                                                cmd4.Parameters.AddWithValue("@value38", "False");
                                                                cmd4.Parameters.AddWithValue("@value39", "False");
                                                                cmd4.Parameters.AddWithValue("@value40", "False");
                                                                cmd4.Parameters.AddWithValue("@value41", "False");
                                                                cmd4.Parameters.AddWithValue("@value42", "False");
                                                                cmd4.Parameters.AddWithValue("@value43", "True");
                                                                cmd4.Parameters.AddWithValue("@value44", le.HSN.Cess);
                                                                cmd4.Parameters.AddWithValue("@value45", "False");
                                                                cmd4.Parameters.AddWithValue("@value46", isInter);

                                                                result = cmd4.ExecuteNonQuery();
                                                                if (result > 0)
                                                                {
                                                                    UpdateAdvanceSellerInvoice(strInvoiceNo, Todt);
                                                                }

                                                            }
                                                        }
                                                        catch
                                                        {

                                                        }
                                                        finally
                                                        {

                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }



                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }

                        }
                        #endregion

                        #region Export
                        else if (item.Invoice.IsExportChecked == true)
                        {
                            FilePath = "ExportUploadInvoice";
                            connectionString = GetConnectionString(FilePath);
                            foreach (LineEntry le in item.Invoice.LineEntry)
                            {
                                IsInter = le.IsInter;
                                if (IsInter == true)
                                {
                                    isInter = "True";
                                }
                                else if (IsInter == false)
                                {
                                    isInter = "False";
                                }
                            }
                           
                            using (OleDbConnection cn = new OleDbConnection(connectionString))
                            {
                                cn.Open();

                                try
                                {
                                   
                                    #region sellerData
                                    OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                         "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[IsFileGSTR1]) " +
                                         "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24)", cn);
                                    cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                    cmd1.Parameters.AddWithValue("@value3", Todt);
                                    cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value10", item.Address);
                                    cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                    cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                    cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                    cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                    cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                    cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                    cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                    cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);                                    
                                    cmd1.Parameters.AddWithValue("@value19", "True");
                                    cmd1.Parameters.AddWithValue("@value20", "ExportACK01");
                                    cmd1.Parameters.AddWithValue("@value21", Todt); 
                                    cmd1.Parameters.AddWithValue("@value22", "False");
                                    cmd1.Parameters.AddWithValue("@value23", "EXPORT");                                   
                                    cmd1.Parameters.AddWithValue("@value24", "False");
                                    result = cmd1.ExecuteNonQuery();

                                    #endregion

                                    if (result > 0)
                                    {
                                        #region Receiver
                                        try
                                        {
                                            OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                           "([ExportNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                            cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                            cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                            result = cmd2.ExecuteNonQuery();
                                        #endregion

                                            if (result > 0)
                                            {

                                                #region Consignee
                                                try
                                                {
                                                    OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                                   "([ExportNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                    cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                    cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                    cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                    cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                    result = cmd3.ExecuteNonQuery();

                                                #endregion

                                                    #region LineEntry
                                                    if (result > 0)
                                                    {
                                                        try
                                                        {

                                                            foreach (LineEntry le in item.Invoice.LineEntry)
                                                            {

                                                                OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                              "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess],[IsFileGSTR1]) " +
                                                                "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45)", cn);

                                                                cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                                cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                                cmd4.Parameters.AddWithValue("@value3", Todt);
                                                                cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                                cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                                cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                                cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                                cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                                cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                                cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                                cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                                cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                                cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                                cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                                cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                                cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                                cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                                cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                                cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                                cmd4.Parameters.AddWithValue("@value33", "False");
                                                                cmd4.Parameters.AddWithValue("@value34", "False");
                                                                cmd4.Parameters.AddWithValue("@value35", "False");
                                                                cmd4.Parameters.AddWithValue("@value36", "False");
                                                                cmd4.Parameters.AddWithValue("@value37", "False");
                                                                cmd4.Parameters.AddWithValue("@value38", "False");
                                                                cmd4.Parameters.AddWithValue("@value39", "False");
                                                                cmd4.Parameters.AddWithValue("@value40", "False");
                                                                cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE
                                                                cmd4.Parameters.AddWithValue("@value42", "False");
                                                                cmd4.Parameters.AddWithValue("@value43", item.IsElectronicReferenceNoGenerated);
                                                                cmd4.Parameters.AddWithValue("@value44", le.HSN.Cess);
                                                                cmd4.Parameters.AddWithValue("@value45", "False");


                                                                result = cmd4.ExecuteNonQuery();
                                                                if (result > 0)
                                                                {
                                                                    UpdateExportSellerInvoice(item.SellerInvoice, Todt);
                                                                }

                                                            }
                                                        }
                                                        catch
                                                        {

                                                        }
                                                        finally
                                                        {

                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }



                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }
                        }
                        #endregion

                        #region B2B
                        else
                        {
                            FilePath = "UploadInvoice";
                            connectionString = GetConnectionString(FilePath);
                            foreach (LineEntry le in item.Invoice.LineEntry)
                            {
                                IsInter = le.IsInter;
                                if (IsInter == true)
                                {
                                    isInter = "True";
                                }
                                else if (IsInter == false)
                                {
                                    isInter = "False";
                                }
                            }
                            using (OleDbConnection cn = new OleDbConnection(connectionString))
                            {
                                cn.Open();

                                try
                                {
                                 

                                    #region sellerData
                                    OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                         "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[IsInter],[ReceiverStateName],[ConsigneeStateName],[ReceiverFinancialPeriod],[IsFileGSTR1]) " +
                                         "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30)", cn);
                                    cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                    cmd1.Parameters.AddWithValue("@value3", Todt);
                                    cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                    cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                    cmd1.Parameters.AddWithValue("@value10", item.Address);
                                    cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                    cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                    cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                    cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                    cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                    cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                    cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                    cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                                    cmd1.Parameters.AddWithValue("@value19", "True");
                                    cmd1.Parameters.AddWithValue("@value20", "B2BACK01");
                                    cmd1.Parameters.AddWithValue("@value21", Todt); 
                                    cmd1.Parameters.AddWithValue("@value22", "False");                                 
                                    cmd1.Parameters.AddWithValue("@value23", "B2B");                                  
                                    cmd1.Parameters.AddWithValue("@value24", item.SellerGrossTurnOver);
                                    cmd1.Parameters.AddWithValue("@value25", item.SellerFinancialPeriod);                                                                 
                                    cmd1.Parameters.AddWithValue("@value26", isInter);
                                    cmd1.Parameters.AddWithValue("@value27", item.Reciever.StateName);
                                    cmd1.Parameters.AddWithValue("@value28", item.Consignee.StateName);
                                    cmd1.Parameters.AddWithValue("@value29", item.Reciever.FinancialPeriod);
                                    cmd1.Parameters.AddWithValue("@value30", "False");
                                    result = cmd1.ExecuteNonQuery();

                                    #endregion

                                    if (result > 0)
                                    {
                                        #region Receiver
                                        try
                                        {
                                            OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                           "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                            cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                            cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                            result = cmd2.ExecuteNonQuery();
                                        #endregion

                                            if (result > 0)
                                            {

                                                #region Consignee
                                                try
                                                {
                                                    OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                                   "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                    cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                    cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                    cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                    cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                    result = cmd3.ExecuteNonQuery();

                                                #endregion

                                                    #region LineEntry
                                                    if (result > 0)
                                                    {
                                                        try
                                                        {
                                                            foreach (LineEntry le in item.Invoice.LineEntry)
                                                            {

                                                                OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                              "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[IsInter],[ConsigneeStateCode],[Cess],[IsFileGSTR1]) " +
                                                                "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47)", cn);

                                                                cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                                cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                                cmd4.Parameters.AddWithValue("@value3", Todt);
                                                                cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                                cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                                cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                                cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                                cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                                cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                                cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                                cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                                cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                                cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                                cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                                cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                                cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                                cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                                cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                                cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                                cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                                cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                                cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                                cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                                cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                                cmd4.Parameters.AddWithValue("@value33", "False");
                                                                cmd4.Parameters.AddWithValue("@value34", "False");
                                                                cmd4.Parameters.AddWithValue("@value35", "False");
                                                                cmd4.Parameters.AddWithValue("@value36", "False");
                                                                cmd4.Parameters.AddWithValue("@value37", "False");
                                                                cmd4.Parameters.AddWithValue("@value38", "False");
                                                                cmd4.Parameters.AddWithValue("@value39", "False");
                                                                cmd4.Parameters.AddWithValue("@value40", "False");
                                                                cmd4.Parameters.AddWithValue("@value41", "False");
                                                                cmd4.Parameters.AddWithValue("@value42", "False");
                                                                cmd4.Parameters.AddWithValue("@value43", "True");
                                                                cmd4.Parameters.AddWithValue("@value44", isInter);
                                                                cmd4.Parameters.AddWithValue("@value45", item.Consignee.StateCode);
                                                                cmd4.Parameters.AddWithValue("@value46", le.HSN.Cess);
                                                                cmd4.Parameters.AddWithValue("@value47", "False");


                                                                result = cmd4.ExecuteNonQuery();
                                                                if (result > 0)
                                                                {
                                                                    UpdateSellerInvoice(item.SellerInvoice, Todt);
                                                                }

                                                            }
                                                        }
                                                        catch
                                                        {

                                                        }
                                                        finally
                                                        {

                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }



                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }
                        }
                        #endregion
                    }
                   
                    #endregion
                }

                #region UPLOAD_ALL_INVOICE
                else
                {
                    #region ADVANCE
                    if (item.Invoice.IsAdvancePaymentChecked == true)
                    {
                        FilePath = "AdvanceUploadInvoice";
                        connectionString = GetConnectionString(FilePath);
                        foreach (LineEntry le in item.Invoice.LineEntry)
                        {
                            IsInter = le.IsInter;
                            if (IsInter == true)
                            {
                                isInter = "True";
                            }
                            else if (IsInter == false)
                            {
                                isInter = "False";
                            }
                        }
                       
                        using (OleDbConnection cn = new OleDbConnection(connectionString))
                        {
                            cn.Open();

                            try
                            {
                                #region sellerData
                               
                                OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                     "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[IsFileGSTR1],[IsInter]) " +
                                     "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25)", cn);
                                cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                cmd1.Parameters.AddWithValue("@value3", Todt);
                                cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value10", item.Address);
                                cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                                cmd1.Parameters.AddWithValue("@value19", "True");
                                cmd1.Parameters.AddWithValue("@value20", "AdvanceACK01");
                                cmd1.Parameters.AddWithValue("@value21", Todt); 
                                cmd1.Parameters.AddWithValue("@value22", "False");                              
                                cmd1.Parameters.AddWithValue("@value23", "ADVANCE");
                                cmd1.Parameters.AddWithValue("@value24", "False");
                                cmd1.Parameters.AddWithValue("@value25", isInter);
                                result = cmd1.ExecuteNonQuery();

                                #endregion

                                if (result > 0)
                                {
                                    #region Receiver
                                    try
                                    {
                                        OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                       "([VoucherNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                       "VALUES(@value1, @value2,@value3,@value4)", cn);
                                        cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                        cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                        cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                        cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                        result = cmd2.ExecuteNonQuery();
                                    #endregion

                                        if (result > 0)
                                        {

                                            #region Consignee
                                            try
                                            {
                                                OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                               "([VoucherNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                               "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                result = cmd3.ExecuteNonQuery();

                                            #endregion

                                                #region LineEntry
                                                if (result > 0)
                                                {
                                                    try
                                                    {

                                                        foreach (LineEntry le in item.Invoice.LineEntry)
                                                        {

                                                            OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                          "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess],[IsFileGSTR1],[IsInter]) " +
                                                            "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46)", cn);

                                                            cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                            cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                            cmd4.Parameters.AddWithValue("@value3", Todt);
                                                            cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                            cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                            cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                            cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                            cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                            cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                            cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                            cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                            cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                            cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                            cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                            cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                            cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                            cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                            cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                            cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                            cmd4.Parameters.AddWithValue("@value33", "False");
                                                            cmd4.Parameters.AddWithValue("@value34", "False");
                                                            cmd4.Parameters.AddWithValue("@value35", "False");
                                                            cmd4.Parameters.AddWithValue("@value36", "False");
                                                            cmd4.Parameters.AddWithValue("@value37", "False");
                                                            cmd4.Parameters.AddWithValue("@value38", "False");
                                                            cmd4.Parameters.AddWithValue("@value39", "False");
                                                            cmd4.Parameters.AddWithValue("@value40", "False");
                                                            cmd4.Parameters.AddWithValue("@value41", "False");
                                                            cmd4.Parameters.AddWithValue("@value42", "False");
                                                            cmd4.Parameters.AddWithValue("@value43", "True");
                                                            cmd4.Parameters.AddWithValue("@value44", le.HSN.Cess);
                                                            cmd4.Parameters.AddWithValue("@value45", "False");
                                                            cmd4.Parameters.AddWithValue("@value46", isInter);

                                                            result = cmd4.ExecuteNonQuery();
                                                            if (result > 0)
                                                            {
                                                                UpdateAdvanceSellerInvoice(strInvoiceNo, Todt);
                                                            }

                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    finally
                                                    {

                                                    }
                                                }
                                                #endregion
                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }



                            }
                            catch
                            {

                            }
                            finally
                            {

                            }
                        }
                    }
                    #endregion

                    #region Export
                    else if (item.Invoice.IsExportChecked == true)
                    {
                        FilePath = "ExportUploadInvoice";
                        connectionString = GetConnectionString(FilePath);

                        foreach (LineEntry le in item.Invoice.LineEntry)
                        {
                            IsInter = le.IsInter;
                            if (IsInter == true)
                            {
                                isInter = "True";
                            }
                            else if (IsInter == false)
                            {
                                isInter = "False";
                            }
                        }
                        using (OleDbConnection cn = new OleDbConnection(connectionString))
                        {
                            cn.Open();

                            try
                            {
                              
                                #region sellerData
                                OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                     "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[IsFileGSTR1]) " +
                                     "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24)", cn);
                                cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                cmd1.Parameters.AddWithValue("@value3", Todt);
                                cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value10", item.Address);
                                cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                                cmd1.Parameters.AddWithValue("@value19", "True");
                                cmd1.Parameters.AddWithValue("@value20", "ExportACK01");
                                cmd1.Parameters.AddWithValue("@value21", Todt); 
                                cmd1.Parameters.AddWithValue("@value22", "False");                             
                                cmd1.Parameters.AddWithValue("@value23", "EXPORT");
                                cmd1.Parameters.AddWithValue("@value24", "False");
                                result = cmd1.ExecuteNonQuery();

                                #endregion

                                if (result > 0)
                                {
                                    #region Receiver
                                    try
                                    {
                                        OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                       "([ExportNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                       "VALUES(@value1, @value2,@value3,@value4)", cn);
                                        cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                        cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                        cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                        cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                        result = cmd2.ExecuteNonQuery();
                                    #endregion

                                        if (result > 0)
                                        {

                                            #region Consignee
                                            try
                                            {
                                                OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                               "([ExportNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                               "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                result = cmd3.ExecuteNonQuery();

                                            #endregion

                                                #region LineEntry
                                                if (result > 0)
                                                {
                                                    try
                                                    {
                                                        
                                                        foreach (LineEntry le in item.Invoice.LineEntry)
                                                        {

                                                            OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                          "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[Cess],[IsFileGSTR1]) " +
                                                            "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45)", cn);

                                                            cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                            cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                            cmd4.Parameters.AddWithValue("@value3", Todt);
                                                            cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                            cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                            cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                            cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                            cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                            cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                            cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                            cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                            cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                            cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                            cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                            cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                            cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                            cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                            cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                            cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                            cmd4.Parameters.AddWithValue("@value33", "False");
                                                            cmd4.Parameters.AddWithValue("@value34", "False");
                                                            cmd4.Parameters.AddWithValue("@value35", "False");
                                                            cmd4.Parameters.AddWithValue("@value36", "False");
                                                            cmd4.Parameters.AddWithValue("@value37", "False");
                                                            cmd4.Parameters.AddWithValue("@value38", "False");
                                                            cmd4.Parameters.AddWithValue("@value39", "False");
                                                            cmd4.Parameters.AddWithValue("@value40", "False");
                                                            cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE
                                                            cmd4.Parameters.AddWithValue("@value42", "False");
                                                            cmd4.Parameters.AddWithValue("@value43", "True");
                                                            cmd4.Parameters.AddWithValue("@value44", le.HSN.Cess);
                                                            cmd4.Parameters.AddWithValue("@value45", "False");


                                                            result = cmd4.ExecuteNonQuery();
                                                            if (result > 0)
                                                            {
                                                                UpdateExportSellerInvoice(item.SellerInvoice, Todt);
                                                            }

                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    finally
                                                    {

                                                    }
                                                }
                                                #endregion
                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }



                            }
                            catch
                            {

                            }
                            finally
                            {

                            }
                        }
                    }
                    #endregion

                    #region B2B
                    else
                    {
                        FilePath = "UploadInvoice";
                        connectionString = GetConnectionString(FilePath);

                        foreach (LineEntry le in item.Invoice.LineEntry)
                        {
                            IsInter = le.IsInter;
                            if (IsInter == true)
                            {
                                isInter = "True";
                            }
                            else if (IsInter == false)
                            {
                                isInter = "False";
                            }
                        }
                      
                        using (OleDbConnection cn = new OleDbConnection(connectionString))
                        {
                            cn.Open();

                            try
                            {

                                #region sellerData
                                OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                     "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[IsInter],[ReceiverStateName],[ConsigneeStateName],[ReceiverFinancialPeriod],[IsFileGSTR1]) " +
                                     "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30)", cn);
                                cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                cmd1.Parameters.AddWithValue("@value3", Todt);
                                cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                                cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                                cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                                cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                                cmd1.Parameters.AddWithValue("@value10", item.Address);
                                cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                                cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                                cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                                cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                                cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                                cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                                cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                                cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                                cmd1.Parameters.AddWithValue("@value19", "True");
                                cmd1.Parameters.AddWithValue("@value20", "B2BACK01");
                                cmd1.Parameters.AddWithValue("@value21", Todt);
                                cmd1.Parameters.AddWithValue("@value22", "False");
                                cmd1.Parameters.AddWithValue("@value23", "B2B");
                                cmd1.Parameters.AddWithValue("@value24", item.SellerGrossTurnOver);
                                cmd1.Parameters.AddWithValue("@value25", item.SellerFinancialPeriod);
                                cmd1.Parameters.AddWithValue("@value26", isInter);
                                cmd1.Parameters.AddWithValue("@value27", item.Reciever.StateName);
                                cmd1.Parameters.AddWithValue("@value28", item.Consignee.StateName);
                                cmd1.Parameters.AddWithValue("@value29", item.Reciever.FinancialPeriod);
                                cmd1.Parameters.AddWithValue("@value30", "False");
                                result = cmd1.ExecuteNonQuery();

                                #endregion

                                if (result > 0)
                                {
                                    #region Receiver
                                    try
                                    {
                                        OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                       "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                       "VALUES(@value1, @value2,@value3,@value4)", cn);
                                        cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                        cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                        cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                        cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                        result = cmd2.ExecuteNonQuery();
                                    #endregion

                                        if (result > 0)
                                        {

                                            #region Consignee
                                            try
                                            {
                                                OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                               "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                               "VALUES(@value1, @value2,@value3,@value4)", cn);
                                                cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                                cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                                cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                                result = cmd3.ExecuteNonQuery();

                                            #endregion

   
                                                #region LineEntry
                                                if (result > 0)
                                                {
                                                    try
                                                    {
                                                        foreach (LineEntry le in item.Invoice.LineEntry)
                                                        {

                                                            OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                          "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[IsInter],[ConsigneeStateCode],[Cess],[IsFileGSTR1]) " +
                                                            "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47)", cn);

                                                            cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                            cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                            cmd4.Parameters.AddWithValue("@value3", Todt);
                                                            cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                            cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                            cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                            cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                            cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                            cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                            cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                            cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                            cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                            cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                            cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                            cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                            cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                            cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                            cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                            cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                            cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                            cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                            cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                            cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                            cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                            cmd4.Parameters.AddWithValue("@value33", "False");
                                                            cmd4.Parameters.AddWithValue("@value34", "False");
                                                            cmd4.Parameters.AddWithValue("@value35", "False");
                                                            cmd4.Parameters.AddWithValue("@value36", "False");
                                                            cmd4.Parameters.AddWithValue("@value37", "False");
                                                            cmd4.Parameters.AddWithValue("@value38", "False");
                                                            cmd4.Parameters.AddWithValue("@value39", "False");
                                                            cmd4.Parameters.AddWithValue("@value40", "False");
                                                            cmd4.Parameters.AddWithValue("@value41", "False"); 
                                                            cmd4.Parameters.AddWithValue("@value42", "False");
                                                            cmd4.Parameters.AddWithValue("@value43", "True");
                                                            cmd4.Parameters.AddWithValue("@value44", isInter);
                                                            cmd4.Parameters.AddWithValue("@value45", item.Consignee.StateCode);
                                                            cmd4.Parameters.AddWithValue("@value46", le.HSN.Cess);
                                                            cmd4.Parameters.AddWithValue("@value47", "False");


                                                            result = cmd4.ExecuteNonQuery();
                                                            if (result > 0)
                                                            {
                                                                UpdateSellerInvoice(item.SellerInvoice, Todt);
                                                            }

                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    finally
                                                    {

                                                    }
                                                }
                                                #endregion
                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }



                            }
                            catch
                            {

                            }
                            finally
                            {

                            }
                        }
                    }
                    #endregion

                }
                #endregion
            }
            return result;
        }
        #endregion

        #region UpdateSellerInvoice
        protected int UpdateSellerInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "InvoiceData";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsElectronicReferenceNoGenerated ='True',ElectronicReferenceNoGeneratedDate='" + Todt + "',ElectronicReferenceNoGenerated='ACK01'  where InvoiceNo='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsElectronicReferenceNoGenerated ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion

        #region UpdateAdvanceSellerInvoice
        protected int UpdateAdvanceSellerInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "AdvancePayment";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsElectronicReferenceNoGenerated ='True',ElectronicReferenceNoGeneratedDate='" + Todt + "',ElectronicReferenceNoGenerated='ACK01'  where VoucherNo='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsElectronicReferenceNoGenerated ='True' where VoucherNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion

        #region UpdateExportSellerInvoice
        protected int UpdateExportSellerInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "Export";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsElectronicReferenceNoGenerated ='True',ElectronicReferenceNoGeneratedDate='" + Todt + "',ElectronicReferenceNoGenerated='ACK01'  where ExportNo='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsElectronicReferenceNoGenerated ='True' where ExportNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion

        #region ViewGSTR1
        /* PATCH */
        public DataSet ViewGSTR1(string SellerGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();

            ds = GetSellerGSTR1Details(SellerGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerGSTR1Details
        public DataSet GetSellerGSTR1Details(string SellerGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            DataTable dtInvSummery = new DataTable("SummeryDtls");
            /*END  B2B invoice */
            FilePath = "UploadInvoice";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "'  and IsFileGSTR1='False' and  UploadedInvoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsFileGSTR1='False'  and  UploadedInvoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);


                    cmd.CommandText = "SELECT distinct InvoiceNo,Invoicedate,ReceiverGSTN,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsFileGSTR1='False'  and  UploadedInvoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtInvSummery);
                    ds.Tables.Add(dtInvSummery);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region AdvancePayment
            /*START  B2B invoice */
            /*START  AdvancePayment */
            DataTable dtAdvanceSellerDtls = new DataTable("AdvanceSellerDtls");
            DataTable dtAdvanceInvoice = new DataTable("AdvanceInvoiceDtls");
            DataTable dtAdvanceInvSummery = new DataTable("AdvanceSummeryDtls");
            /*END  AdvancePayment */

            FilePath = "AdvanceUploadInvoice";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and IsFileGSTR1='False'  and UploadedVoucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                    da3.Fill(dtAdvanceSellerDtls);
                    ds.Tables.Add(dtAdvanceSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsFileGSTR1='False' and   UploadedVoucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da4 = new OleDbDataAdapter(cmd);
                    da4.Fill(dtAdvanceInvoice);
                    ds.Tables.Add(dtAdvanceInvoice);


                    cmd.CommandText = "SELECT distinct VoucherNo,Voucherdate,ReceiverGSTN,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsFileGSTR1='False' and  UploadedVoucherdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da5 = new OleDbDataAdapter(cmd);
                    da5.Fill(dtAdvanceInvSummery);
                    ds.Tables.Add(dtAdvanceInvSummery);

                }
                catch
                {

                }
                finally
                {

                }

            }
            #endregion

            #region EXPORT
            /*START  EXPORT */
            DataTable dtEXPORTSellerDtls = new DataTable("EXPORTSellerDtls");
            DataTable dtEXPORTInvoice = new DataTable("EXPORTInvoiceDtls");
            DataTable dtEXPORTInvSummery = new DataTable("EXPORTSummeryDtls");
            /*END  EXPORT */

            FilePath = "ExportUploadInvoice";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "'  and IsFileGSTR1='False'  and  UploadedExportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtEXPORTSellerDtls);
                    ds.Tables.Add(dtEXPORTSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsFileGSTR1='False'  and  UploadedExportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtEXPORTInvoice);
                    ds.Tables.Add(dtEXPORTInvoice);


                    cmd.CommandText = "SELECT distinct ExportNo,Exportdate,ReceiverGSTN,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsFileGSTR1='False'  and  UploadedExportdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtEXPORTInvSummery);
                    ds.Tables.Add(dtEXPORTInvSummery);

                }
                catch
                {

                }
                finally
                {

                }

            }
            #endregion
            return ds;
        }




        #endregion

        #endregion

        #region FileGSTR1
        /* PATCH */
        public int FileGSTR1(List<Seller> SellerDaTa)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Seller seller = new Seller();
            bool IsInter; string isInter = string.Empty;

            seller.SellerDaTa = new List<Seller>();

            seller.SellerDaTa = SellerDaTa;

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            foreach (var item in seller.SellerDaTa)
            {

                #region ADVANCE
                if (item.Invoice.IsAdvancePaymentChecked == true)
                {
                    FilePath = "AdvanceGSTR1";
                    connectionString = GetConnectionString(FilePath);

                    foreach (LineEntry le in item.Invoice.LineEntry)
                    {                     
                        IsInter = le.IsInter;
                        if (IsInter == true)
                        {
                            isInter = "True";
                        }
                        else if (IsInter == false)
                        {
                            isInter = "False";
                        }
                        /**/
                    }

                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {
                            #region sellerData
                           
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[FileGSTR1Date],[IsInter]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22)", cn);
                            cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", Todt);
                            cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", item.Address);
                            cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);                                                
                            cmd1.Parameters.AddWithValue("@value19", "False");                         
                            cmd1.Parameters.AddWithValue("@value20", "ADVANCE");                            
                            cmd1.Parameters.AddWithValue("@value21", Todt);
                            cmd1.Parameters.AddWithValue("@value22", isInter);
                            result = cmd1.ExecuteNonQuery();

                            #endregion

                            if (result > 0)
                            {
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([VoucherNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion

                                    if (result > 0)
                                    {

                                        #region Consignee
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([VoucherNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();

                                        #endregion

                                            #region LineEntry
                                            if (result > 0)
                                            {
                                                try
                                                {
                                                    foreach (LineEntry le in item.Invoice.LineEntry)
                                                    {
                                                       
                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                      "([VoucherNo],[Voucherdate],[UploadedVoucherdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[Cess],[FileGSTR1Date],[IsInter]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", Todt);
                                                        cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False"); 
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", le.HSN.Cess);
                                                        cmd4.Parameters.AddWithValue("@value44", Todt);
                                                        cmd4.Parameters.AddWithValue("@value45", isInter);
                                                      
                                                        result = cmd4.ExecuteNonQuery();

                                                        if (result > 0)
                                                        {
                                                            UpdateAdvanceUploadInvoice(item.SellerInvoice, Todt);
                                                        }
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }



                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                #endregion

                #region Export
                else if (item.Invoice.IsExportChecked == true)
                {
                    FilePath = "ExportGSTR1";
                    connectionString = GetConnectionString(FilePath);

                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {
                          
                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[FileGSTR1Date],[ExportDescription]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22)", cn);
                            cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", Todt);
                            cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", item.Address);
                            cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);                           
                            cmd1.Parameters.AddWithValue("@value19", "False");
                            cmd1.Parameters.AddWithValue("@value20", "EXPORT");
                            cmd1.Parameters.AddWithValue("@value21", Todt);
                            cmd1.Parameters.AddWithValue("@value22", "");
                           
                            result = cmd1.ExecuteNonQuery();

                            #endregion

                            if (result > 0)
                            {
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([ExportNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion

                                    if (result > 0)
                                    {

                                        #region Consignee
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([ExportNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();

                                        #endregion

                                            #region LineEntry
                                            if (result > 0)
                                            {
                                                try
                                                {
                                                    foreach (LineEntry le in item.Invoice.LineEntry)
                                                    {
                                                       
                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                      "([ExportNo],[Exportdate],[UploadedExportdate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[Cess],[FileGSTR1Date],[ExportDescription]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", Todt);
                                                        cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", le.HSN.Cess);
                                                        cmd4.Parameters.AddWithValue("@value44", Todt);
                                                        cmd4.Parameters.AddWithValue("@value45", "");
                                                        result = cmd4.ExecuteNonQuery();

                                                        if (result > 0)
                                                        {
                                                            UpdateExportUploadInvoice(item.SellerInvoice, Todt);
                                                        }
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }



                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                #endregion

                #region B2B
                /* PATCH */
                else
                {
                    FilePath = "FileGSTR1";
                    connectionString = GetConnectionString(FilePath);
                    
                    /* CALCULATE TOTAL */
                    
                    foreach (LineEntry le in item.Invoice.LineEntry)
                    {                       
                        IsInter = le.IsInter;
                        if (IsInter == true)
                        {
                            isInter = "True";
                        }
                        else if (IsInter == false)
                        {
                            isInter = "False";
                        }                      
                    }
                  
                    /* CALCULATE TOTAL */
                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {
                           
                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[IsInter],[ReceiverStateName],[ConsigneeStateName],[ReceiverFinancialPeriod],[IsAcceptGSTR2A],[IsEditedByReceiver],[IsDeletedByReceiver],[FileGSTR1Date]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30)", cn);
                            cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", Todt);
                            cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", item.Address);
                            cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                          
                           
                            cmd1.Parameters.AddWithValue("@value19", "False");
                            cmd1.Parameters.AddWithValue("@value20", "B2B");
                            
                            cmd1.Parameters.AddWithValue("@value21", item.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value22", item.SellerFinancialPeriod);
                            cmd1.Parameters.AddWithValue("@value23", isInter);
                            cmd1.Parameters.AddWithValue("@value24", item.Reciever.StateName);
                            cmd1.Parameters.AddWithValue("@value25", item.Consignee.StateName);
                            cmd1.Parameters.AddWithValue("@value26", item.Reciever.FinancialPeriod);
                         
                          
                            cmd1.Parameters.AddWithValue("@value27", "False");
                            cmd1.Parameters.AddWithValue("@value28", "False");
                           
                            cmd1.Parameters.AddWithValue("@value29", "False");
                            cmd1.Parameters.AddWithValue("@value30", Todt);
                            result = cmd1.ExecuteNonQuery();

                            #endregion

                            if (result > 0)
                            {
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion

                                    if (result > 0)
                                    {

                                        #region Consignee
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();

                                        #endregion

                                            #region LineEntry
                                            if (result > 0)
                                            {
                                                try
                                                {
                                                    foreach (LineEntry le in item.Invoice.LineEntry)
                                                    {
                                                        //InvoiceNo	Invoicedate	UploadedInvoicedate	SellerGSTN	ReceiverGSTN	InvoiceSeed	LineID	Description	HSN	Qty	Unit	Rate	Total	Discount	TaxableValue	AmountWithTax	IGSTRate	IGSTAmt	CGSTRate	CGSTAmt	SGSTRate	SGSTAmt	TotalQty	TotalRate	TotalAmount	TotalDiscount	TotalTaxableAmount	TotalCGSTAmount	TotalSGSTAmount	TotalIGSTAmount	TotalAmountWithTax	GrandTotalAmount	GrandTotalAmountInWord	isHSNNilRated	isHSNExempted	isHSNZeroRated	isHSNNonGSTGoods	isSACNilRated	isSACEcxempted	isSACZeroRated	isSACNonGSTService	IsNotifedGoods	IsNotifiedSAC	IsInter	ConsigneeStateCode	Cess	IsAcceptGSTR2A	IsEditedByReceiver	IsDeletedByReceiver	FileGSTR1Date

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                       "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsInter],[ConsigneeStateCode],[Cess],[IsAcceptGSTR2A],[IsEditedByReceiver],[IsDeletedByReceiver],[FileGSTR1Date]) " +
                                                         "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", Todt);
                                                        cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False");
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", isInter);
                                                        cmd4.Parameters.AddWithValue("@value44", item.Consignee.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value45", le.HSN.Cess);                                                    
                                                        cmd4.Parameters.AddWithValue("@value46", "False");
                                                        cmd4.Parameters.AddWithValue("@value47", "False");
                                                        cmd4.Parameters.AddWithValue("@value48", "False");
                                                        cmd4.Parameters.AddWithValue("@value49", Todt);
                                                  
                                                      

                                                        result = cmd4.ExecuteNonQuery();

                                                        if (result > 0)
                                                        {
                                                            UpdateUploadInvoice(item.SellerInvoice, Todt);
                                                        }
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }



                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                #endregion



            }
            return result;
        }



        #region UpdateAdvanceUploadInvoice
        protected int UpdateAdvanceUploadInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "AdvanceUploadInvoice";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsFileGSTR1 ='True'  where VoucherNo ='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsFileGSTR1 ='True' where VoucherNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion

        #region UpdateExportUploadInvoice
        protected int UpdateExportUploadInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "ExportUploadInvoice";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsFileGSTR1 ='True'  where ExportNo ='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsFileGSTR1 ='True' where ExportNo ='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion

        #region UpdateUploadInvoice
        protected int UpdateUploadInvoice(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "UploadInvoice";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsFileGSTR1 ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsFileGSTR1 ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion
        #endregion

        #region ViewGSTR2A
        /* PATCH */
        public DataSet ViewGSTR2A(string strReceiverGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();
            ds = GetSellerGSTR2ADetails(strReceiverGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerGSTR2ADetails
        /* PATCH */
        public DataSet GetSellerGSTR2ADetails(string strReceiverGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /* PATCH */

            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            DataTable dtInvSummery = new DataTable("SummeryDtls");
            /*END  B2B invoice */
            FilePath = "FileGSTR1";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where ReceiverGSTN ='" + strReceiverGSTN + "' and IsAcceptGSTR2A='False' and IsEditedByReceiver ='False' and IsDeletedByReceiver ='False' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where ReceiverGSTN ='" + strReceiverGSTN + "'  and IsAcceptGSTR2A='False' and IsEditedByReceiver ='False'  and IsDeletedByReceiver ='False' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);


                    cmd.CommandText = "SELECT distinct SellerGSTN,InvoiceNo,Invoicedate,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where ReceiverGSTN ='" + strReceiverGSTN + "'  and IsAcceptGSTR2A='False' and IsEditedByReceiver ='False'  and IsDeletedByReceiver ='False' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtInvSummery);
                    ds.Tables.Add(dtInvSummery);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            return ds;
        }




        #endregion

        #endregion

        #region SaveAddedInvoicedata
        /* PATCH */
        private int IsExistReceiverInvoiceNo(string strInvoiceNo)
        {
            int result = 0;

            string FilePath = "InvoiceData";

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {

                    cn.Open();


                    OleDbCommand cmd7 = new OleDbCommand("SELECT InvoiceNo FROM [SellerInvoiceData$] where InvoiceNo ='" + strInvoiceNo + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = 2;
                    }
                    else
                        result = 1;
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }

        private int IsExistImportNo(string ImportNo)
        {
            int result = 0;

            string FilePath = "Import";

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {

                    cn.Open();


                    OleDbCommand cmd7 = new OleDbCommand("SELECT ImportNo FROM [SellerInvoiceData$] where ImportNo ='" + ImportNo + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = 3;
                    }
                    else
                        result = 1;
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }

        public int SaveAddedInvoicedata(Seller seller, LineEntry line, List<LineEntry> LineEntry, string Mode)
        {
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            string FilePath = string.Empty;
            string connectionString = string.Empty;
            bool IsInter; string isInter = string.Empty;
            string IsEditedByReceiver = string.Empty;
            string IsAddedByReceiver = string.Empty;
            string IsAcceptedByReceiver = string.Empty;

            /* START  AMOUNT IN WORDS*/
            decimal TotalTaxableAmount = 0; decimal TotalAmount = 0; decimal TotalDiscount = 0;
            decimal TotalCGSTAmount = 0; decimal TotalSGSTAmount = 0; decimal TotalIGSTAmount = 0;
            decimal TotalQty = 0; decimal TotalRate = 0; decimal TotalAmountWithTax = 0;

            foreach (LineEntry le in seller.Invoice.LineEntry)
            {
                TotalTaxableAmount += le.TaxValue;
                TotalAmount += le.TotalLineIDWise;
                TotalDiscount += le.Discount;
                TotalQty += le.Qty;
                TotalRate += le.PerUnitRate;
                if (line.IsInter)
                {
                    TotalIGSTAmount += le.AmtIGSTLineIDWise;
                    TotalAmountWithTax += le.AmtIGSTLineIDWise + le.TaxValue;
                }
                else
                {
                    TotalCGSTAmount += le.AmtCGSTLineIDWise;
                    TotalSGSTAmount += le.AmtSGSTLineIDWise;
                    TotalAmountWithTax += le.AmtCGSTLineIDWise + le.AmtCGSTLineIDWise + le.TaxValue;
                }

                IsInter = le.IsInter;
                        if (IsInter == true)
                        {
                            isInter = "True";
                        }
                        else if (IsInter == false)
                        {
                            isInter = "False";
                        }
            }


            decimal GrandTotalAmount = seller.Invoice.Freight + seller.Invoice.Insurance + seller.Invoice.PackingAndForwadingCharges + TotalAmountWithTax;

            string GrandTotalAmountInWord = ConvertNumbertoWords(Convert.ToDecimal(GrandTotalAmount));

            /* END AMOUNT IN WORDS */


            int result = 0;
            if (Mode == "Import")
            {
                #region Import

                FilePath = "Import";

                connectionString = GetConnectionString(FilePath);


                using (OleDbConnection cn2 = new OleDbConnection(connectionString))
                {
                    cn2.Open();

                    try
                    {
                        result = IsExistImportNo(seller.SellerInvoice);
                        if (result == 1)
                        {

                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([ImportNo],[Importdate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[IsAdvance],[InvoiceType],[IsExport],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverGrossTurnOver],[ReceiverFinancialPeriod],[ConsigneeGrossTurnOver],[ConsigneeFinancialPeriod],[ImportDescription]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31)", cn2);
                            cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value4", seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value6", seller.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value7", seller.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value8", seller.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value9", seller.Address);
                            cmd1.Parameters.AddWithValue("@value10", seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value11", seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value12", seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value13", seller.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value14", seller.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", seller.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value17", seller.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value18", "False");
                            cmd1.Parameters.AddWithValue("@value19", "");
                            cmd1.Parameters.AddWithValue("@value20", "");
                            cmd1.Parameters.AddWithValue("@value21", "False");
                            cmd1.Parameters.AddWithValue("@value22", "False");
                            cmd1.Parameters.AddWithValue("@value23", "B2B");
                            cmd1.Parameters.AddWithValue("@value24", "False");
                            cmd1.Parameters.AddWithValue("@value25", seller.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value26", seller.SellerFinancialPeriod);
                          
                            cmd1.Parameters.AddWithValue("@value28", seller.Reciever.FinancialPeriod);
                           
                            cmd1.Parameters.AddWithValue("@value31", "");
                            result = cmd1.ExecuteNonQuery();
                            #endregion
                            if (result == 1)
                            {
                                result = 0;
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([ImportNo],[ReceiverGSTN],[ReceiverName],[ReceiverAddress],[ReceiverStateCode],[ReceiverStateName],[ReceiverStateCodeID]) " +
                                   "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                    cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", seller.Reciever.Address);
                                    cmd2.Parameters.AddWithValue("@value5", seller.Reciever.StateCode);
                                    cmd2.Parameters.AddWithValue("@value6", seller.Reciever.StateName);
                                    cmd2.Parameters.AddWithValue("@value7", seller.Reciever.StateCodeID);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion
                                    #region Consignee
                                    if (result == 1)
                                    {
                                        result = 0;
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([ImportNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeAddress],[ConsigneeStateCode],[ConsigneeStateName],[ConsigneeStateCodeID]) " +
                                           "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                            cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", seller.Consignee.Address);
                                            cmd3.Parameters.AddWithValue("@value5", seller.Consignee.StateCode);
                                            cmd3.Parameters.AddWithValue("@value6", seller.Consignee.StateName);
                                            cmd3.Parameters.AddWithValue("@value7", seller.Consignee.StateCodeID);
                                            result = cmd3.ExecuteNonQuery();
                                    #endregion
                                            if (result == 1)
                                            {
                                                result = 0;
                                                try
                                                {
                                                    #region LineEntry
                                                    foreach (LineEntry le in seller.Invoice.LineEntry)
                                                    {

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                        "([ImportNo],[Importdate],[SellerGSTN],[ReceiverGSTN],[InvoiceSeed],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated],[ImportDescription]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44)", cn2);

                                                        cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value4", seller.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", seller.SerialNoInvoice.CurrentSrlNo);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", line.HSN.IsNotified);
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", "False");
                                                        cmd4.Parameters.AddWithValue("@value44", "");

                                                        result = cmd4.ExecuteNonQuery();


                                                    }
                                                    #endregion

                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            else
                                            {
                                                result = 0;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                { }

                            }
                            else
                            {
                                result = 0;
                            }

                        }
                    }
                    catch
                    { }
                    finally
                    { cn2.Close(); }
                    return result;
                }


                //}
                //else
                //{
                //    result = 0;
                //}
                #endregion
            }
            else if (Mode == "ReverseCharge")
            {
                #region ReverseCharge

                FilePath = "ReverseCharge";

                connectionString = GetConnectionString(FilePath);


                using (OleDbConnection cn2 = new OleDbConnection(connectionString))
                {
                    cn2.Open();

                    try
                    {

                        #region sellerData
                        OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                             "([InvoiceNo],[Invoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[IsAdvance],[InvoiceType],[IsExport],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverGrossTurnOver],[ReceiverFinancialPeriod],[ConsigneeGrossTurnOver],[ConsigneeFinancialPeriod]) " +
                             "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30)", cn2);
                        cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                        cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                        cmd1.Parameters.AddWithValue("@value3", seller.GSTIN);
                        cmd1.Parameters.AddWithValue("@value4", seller.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                        cmd1.Parameters.AddWithValue("@value6", seller.Reciever.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value7", seller.Consignee.GSTIN);
                        cmd1.Parameters.AddWithValue("@value8", seller.Consignee.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value9", seller.Address);
                        cmd1.Parameters.AddWithValue("@value10", seller.SellerStateCode);
                        cmd1.Parameters.AddWithValue("@value11", seller.SellerStateName);
                        cmd1.Parameters.AddWithValue("@value12", seller.SellerStateCodeID);
                        cmd1.Parameters.AddWithValue("@value13", seller.Reciever.StateCode);
                        cmd1.Parameters.AddWithValue("@value14", seller.Consignee.StateCode);
                        cmd1.Parameters.AddWithValue("@value15", seller.Invoice.Freight);
                        cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Insurance);
                        cmd1.Parameters.AddWithValue("@value17", seller.Invoice.PackingAndForwadingCharges);
                        cmd1.Parameters.AddWithValue("@value18", "False");
                        cmd1.Parameters.AddWithValue("@value19", "");
                        cmd1.Parameters.AddWithValue("@value20", "");
                        cmd1.Parameters.AddWithValue("@value21", "False");
                        cmd1.Parameters.AddWithValue("@value22", "False");
                        cmd1.Parameters.AddWithValue("@value23", "B2B");
                        cmd1.Parameters.AddWithValue("@value24", "False");
                        cmd1.Parameters.AddWithValue("@value25", seller.SellerGrossTurnOver);
                        cmd1.Parameters.AddWithValue("@value26", seller.SellerFinancialPeriod);
                       
                        cmd1.Parameters.AddWithValue("@value28", seller.Reciever.FinancialPeriod);
                      
                        result = cmd1.ExecuteNonQuery();
                        #endregion
                        if (result == 1)
                        {
                            result = 0;
                            #region Receiver
                            try
                            {
                                OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                               "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverAddress],[ReceiverStateCode],[ReceiverStateName],[ReceiverStateCodeID]) " +
                               "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                cmd2.Parameters.AddWithValue("@value4", seller.Reciever.Address);
                                cmd2.Parameters.AddWithValue("@value5", seller.Reciever.StateCode);
                                cmd2.Parameters.AddWithValue("@value6", seller.Reciever.StateName);
                                cmd2.Parameters.AddWithValue("@value7", seller.Reciever.StateCodeID);

                                result = cmd2.ExecuteNonQuery();
                            #endregion
                                #region Consignee
                                if (result == 1)
                                {
                                    result = 0;
                                    try
                                    {
                                        OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                       "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeAddress],[ConsigneeStateCode],[ConsigneeStateName],[ConsigneeStateCodeID]) " +
                                       "VALUES(@value1, @value2,@value3, @value4,@value5,@value6,@value7)", cn2);
                                        cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                        cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                        cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                        cmd3.Parameters.AddWithValue("@value4", seller.Consignee.Address);
                                        cmd3.Parameters.AddWithValue("@value5", seller.Consignee.StateCode);
                                        cmd3.Parameters.AddWithValue("@value6", seller.Consignee.StateName);
                                        cmd3.Parameters.AddWithValue("@value7", seller.Consignee.StateCodeID);
                                        result = cmd3.ExecuteNonQuery();
                                #endregion
                                        if (result == 1)
                                        {
                                            #region LineEntry
                                            result = 0;
                                            try
                                            {
                                                foreach (LineEntry le in seller.Invoice.LineEntry)
                                                {

                                                    OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                    "([InvoiceNo],[Invoicedate],[SellerGSTN],[ReceiverGSTN],[InvoiceSeed],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[IsElectronicReferenceNoGenerated]) " +
                                                    "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43)", cn2);

                                                    cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                    cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                    cmd4.Parameters.AddWithValue("@value3", seller.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value4", seller.Reciever.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value5", 0);
                                                    cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                    cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                    cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                    cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                    cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                    cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                    cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                    cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                    cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                    cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                    cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                    cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value22", TotalQty);
                                                    cmd4.Parameters.AddWithValue("@value23", TotalRate);
                                                    cmd4.Parameters.AddWithValue("@value24", TotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value25", TotalDiscount);
                                                    cmd4.Parameters.AddWithValue("@value26", TotalTaxableAmount);
                                                    cmd4.Parameters.AddWithValue("@value27", TotalCGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value28", TotalSGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value29", TotalIGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value30", TotalAmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value31", GrandTotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value32", GrandTotalAmountInWord);
                                                    cmd4.Parameters.AddWithValue("@value33", "False");
                                                    cmd4.Parameters.AddWithValue("@value34", "False");
                                                    cmd4.Parameters.AddWithValue("@value35", "False");
                                                    cmd4.Parameters.AddWithValue("@value36", "False");
                                                    cmd4.Parameters.AddWithValue("@value37", "False");
                                                    cmd4.Parameters.AddWithValue("@value38", "False");
                                                    cmd4.Parameters.AddWithValue("@value39", "False");
                                                    cmd4.Parameters.AddWithValue("@value40", "False");
                                                    cmd4.Parameters.AddWithValue("@value41", line.HSN.IsNotified);
                                                    cmd4.Parameters.AddWithValue("@value42", "False");
                                                    cmd4.Parameters.AddWithValue("@value43", "False");

                                                    result = cmd4.ExecuteNonQuery();


                                                }
                                                //if (result > 0)
                                                //{

                                                //    result = UpdateInvoiceSeed(seller);

                                                //}

                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            result = 0;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                                else
                                {
                                    result = 0;
                                }
                            }
                            catch
                            {

                            }
                            finally
                            { }

                        }
                        else
                        {
                            result = 0;
                        }


                    }
                    catch
                    { }
                    finally
                    { cn2.Close(); }
                    return result;
                }


                //}
                //else
                //{
                //    result = 0;
                //}
                #endregion

            }
            else
            {
                #region B2B
                if (Mode == "AddGSTR2A")
                {
                    IsEditedByReceiver = "False";
                    IsAddedByReceiver = "True";
                    IsAcceptedByReceiver = "False";
                    result = IsExistReceiverInvoiceNo(seller.SellerInvoice);
                }
                else if (Mode == "EditGSTR2A")
                {
                    IsEditedByReceiver = "True";
                    IsAddedByReceiver = "False";
                    IsAcceptedByReceiver = "False";

                    result = 1;
                }

                if (result == 1)
                {
                    FilePath = "FileGSTR2A";

                    connectionString = GetConnectionString(FilePath);


                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {

                            #region sellerData


                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverFinancialPeriod],[FileGSTR2ADate],[IsUploadGSTR2],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[ReceiverStateName],[ConsigneeStateName],[IsDeletedByReceiver],[IsRejectedByReceiver]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31,@value32,@value33)", cn);
                            cmd1.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", "");
                            cmd1.Parameters.AddWithValue("@value4", seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", seller.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", seller.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", seller.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", seller.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", seller.Address);
                            cmd1.Parameters.AddWithValue("@value11", seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", seller.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", seller.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", seller.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", seller.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", seller.Invoice.PackingAndForwadingCharges);                         
                            cmd1.Parameters.AddWithValue("@value19", "False");                          
                            cmd1.Parameters.AddWithValue("@value20", "B2B");                         
                            cmd1.Parameters.AddWithValue("@value21", seller.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value22", seller.SellerFinancialPeriod);                       
                            cmd1.Parameters.AddWithValue("@value23", seller.Reciever.FinancialPeriod);                          
                            cmd1.Parameters.AddWithValue("@value24", Todt);
                            cmd1.Parameters.AddWithValue("@value25", "False");
                            cmd1.Parameters.AddWithValue("@value26", IsEditedByReceiver);
                            cmd1.Parameters.AddWithValue("@value27", IsAddedByReceiver);
                            cmd1.Parameters.AddWithValue("@value28", IsAcceptedByReceiver);
                            cmd1.Parameters.AddWithValue("@value29", isInter);
                            cmd1.Parameters.AddWithValue("@value30", seller.Reciever.StateName);
                            cmd1.Parameters.AddWithValue("@value31", seller.Consignee.StateName);
                            cmd1.Parameters.AddWithValue("@value32", "False");
                            cmd1.Parameters.AddWithValue("@value33", "False");

                            result = cmd1.ExecuteNonQuery();
                            #endregion
                            if (result == 1)
                            {
                                result = 0;
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3, @value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", seller.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", seller.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", seller.Reciever.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion
                                    #region Consignee
                                    if (result == 1)
                                    {
                                        result = 0;
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3, @value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", seller.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", seller.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", seller.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();
                                    #endregion
                                            if (result == 1)
                                            {
                                                #region LineEntry
                                                result = 0;
                                                try
                                                {
                                                    foreach (LineEntry le in seller.Invoice.LineEntry)
                                                    {


                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                        "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[FileGSTR2ADate],[IsUploadGSTR2],[IsMatched],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[SellerStateCode],[SellerStateName],[ReceiverStateCode],[ReceiverStateName],[ConsigneeStateCode],[ConsigneeStateName],[IsDeletedByReceiver],[IsRejectedByReceiver],[Cess]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49,@value50,@value51,@value52,@value53,@value54,@value55,@value56,@value57,@value58)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", "");
                                                        cmd4.Parameters.AddWithValue("@value4", seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", seller.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False");
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", Todt);
                                                        cmd4.Parameters.AddWithValue("@value44", "False");
                                                        cmd4.Parameters.AddWithValue("@value45", "False");                                                     
                                                        cmd4.Parameters.AddWithValue("@value46", IsEditedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value47", IsAddedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value48", IsAcceptedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value49", isInter);
                                                        cmd4.Parameters.AddWithValue("@value50", seller.SellerStateCode);
                                                        cmd4.Parameters.AddWithValue("@value51", seller.SellerStateName);
                                                        cmd4.Parameters.AddWithValue("@value52", seller.Reciever.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value53", seller.Reciever.StateName);
                                                        cmd4.Parameters.AddWithValue("@value54", seller.Consignee.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value55", seller.Consignee.StateName);
                                                        cmd4.Parameters.AddWithValue("@value56", "False");
                                                        cmd4.Parameters.AddWithValue("@value57", "False");
                                                        cmd4.Parameters.AddWithValue("@value58", le.HSN.Cess);
                                                        result = cmd4.ExecuteNonQuery();

                                                        if (result > 0)
                                                        {
                                                            if (Mode == "EditGSTR2A")
                                                            {
                                                                UpdateGSTR1(seller.SellerInvoice, Todt, "EditGSTR2A");
                                                            }
                                                        }

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                result = 0;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                { }

                            }
                            else
                            {
                                result = 0;
                            }


                        }
                        catch
                        { }
                        finally
                        { cn.Close(); }
                        return result;
                    }


                }
                else
                {
                    result = 0;
                }
                #endregion
            }


            return result;

        }
        #endregion

        #region SaveGSTR2A
        /* PATCH */
        public int SaveGSTR2A(List<Reciever> RecieverData, string strInvoiceNo)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Reciever reciever = new Reciever();

            reciever.RecieverData = new List<Reciever>();

            reciever.RecieverData = RecieverData;

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            bool IsInter; string isInter = string.Empty;


            foreach (var item in reciever.RecieverData)
            {
                //TO UPLOAD A PERTICULAR INVOICE


                #region B2B
                FilePath = "FileGSTR2A";
                connectionString = GetConnectionString(FilePath);

               
                foreach (LineEntry le in item.Invoice.LineEntry)
                {                    
                    IsInter = le.IsInter;
                    if (IsInter == true)
                    {
                        isInter = "True";
                    }
                    else if (IsInter == false)
                    {
                        isInter = "False";
                    }                 
                }

              
                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    cn.Open();

                    try
                    {                       
                        #region sellerData
                        OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                             "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverFinancialPeriod],[FileGSTR2ADate],[IsUploadGSTR2],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[ReceiverStateName],[ConsigneeStateName],[IsDeletedByReceiver],[IsRejectedByReceiver]) " +
                             "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31,@value32,@value33)", cn);
                        cmd1.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                        cmd1.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                        cmd1.Parameters.AddWithValue("@value3", Todt);
                        cmd1.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                        cmd1.Parameters.AddWithValue("@value5", item.Seller.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value6", item.GSTIN);
                        cmd1.Parameters.AddWithValue("@value7", item.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                        cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value10", item.Seller.Address);
                        cmd1.Parameters.AddWithValue("@value11", item.Seller.SellerStateCode);
                        cmd1.Parameters.AddWithValue("@value12", item.Seller.SellerStateName);
                        cmd1.Parameters.AddWithValue("@value13", item.Seller.SellerStateCodeID);
                        cmd1.Parameters.AddWithValue("@value14", item.StateCode);                       
                        cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                        cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                        cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                        cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                        cmd1.Parameters.AddWithValue("@value19", "True");
                        cmd1.Parameters.AddWithValue("@value20", "B2B");
                        cmd1.Parameters.AddWithValue("@value21", item.Seller.SellerGrossTurnOver);
                        cmd1.Parameters.AddWithValue("@value22", item.Seller.SellerFinancialPeriod);
                        cmd1.Parameters.AddWithValue("@value23", item.FinancialPeriod);
                        cmd1.Parameters.AddWithValue("@value24", Todt);
                        cmd1.Parameters.AddWithValue("@value25", "False");
                        cmd1.Parameters.AddWithValue("@value26", "False");                    
                        cmd1.Parameters.AddWithValue("@value27", "False");                      
                        cmd1.Parameters.AddWithValue("@value28", "True");
                        cmd1.Parameters.AddWithValue("@value29", isInter);
                        cmd1.Parameters.AddWithValue("@value30", item.StateName);
                        cmd1.Parameters.AddWithValue("@value31", item.Consignee.StateName);
                        cmd1.Parameters.AddWithValue("@value32", "False");
                        cmd1.Parameters.AddWithValue("@value33", "False");


                        result = cmd1.ExecuteNonQuery();

                        #endregion

                        if (result > 0)
                        {
                            #region Receiver
                            try
                            {
                                OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                               "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                               "VALUES(@value1, @value2,@value3,@value4)", cn);
                                cmd2.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                cmd2.Parameters.AddWithValue("@value2", item.GSTIN);
                                cmd2.Parameters.AddWithValue("@value3", item.NameAsOnGST);
                                cmd2.Parameters.AddWithValue("@value4", item.StateCode);

                                result = cmd2.ExecuteNonQuery();
                            #endregion

                                if (result > 0)
                                {

                                    #region Consignee
                                    try
                                    {
                                        OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                       "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                       "VALUES(@value1, @value2,@value3,@value4)", cn);
                                        cmd3.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                        cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                        cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                        cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                        result = cmd3.ExecuteNonQuery();

                                    #endregion

                                        #region LineEntry
                                        if (result > 0)
                                        {
                                            try
                                            {
                                                foreach (LineEntry le in item.Invoice.LineEntry)
                                                {
                                                    // InvoiceNo	Invoicedate	UploadedInvoicedate	SellerGSTN	ReceiverGSTN	LineID	Description	HSN	Qty	Unit	Rate	Total	Discount	TaxableValue	AmountWithTax	IGSTRate	IGSTAmt	CGSTRate	CGSTAmt	SGSTRate	SGSTAmt	TotalQty	TotalRate	TotalAmount	TotalDiscount	TotalTaxableAmount	TotalCGSTAmount	TotalSGSTAmount	TotalIGSTAmount	TotalAmountWithTax	GrandTotalAmount	GrandTotalAmountInWord	isHSNNilRated	isHSNExempted	isHSNZeroRated	isHSNNonGSTGoods	isSACNilRated	isSACEcxempted	isSACZeroRated	isSACNonGSTService	IsNotifedGoods	IsNotifiedSAC	FileGSTR2ADate	IsUploadGSTR2	IsMatched	IsEditedByReceiver	IsAddedByReceiver	IsAcceptedByReceiver	IsInter	SellerStateCode	SellerStateName	ReceiverStateCode	ReceiverStateName	ConsigneeStateCode	ConsigneeStateName	IsDeletedByReceiver	IsRejectedByReceiver

                                                    OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                  "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[FileGSTR2ADate],[IsUploadGSTR2],[IsMatched],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[SellerStateCode],[SellerStateName],[ReceiverStateCode],[ReceiverStateName],[ConsigneeStateCode],[ConsigneeStateName],[IsDeletedByReceiver],[IsRejectedByReceiver],[Cess]) " +
                                                    "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49,@value50,@value51,@value52,@value53,@value54,@value55,@value56,@value57,@value58)", cn);

                                                    cmd4.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                                    cmd4.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                                                    cmd4.Parameters.AddWithValue("@value3", Todt);
                                                    cmd4.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value5", item.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                    cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                    cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                    cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                    cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                    cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                    cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                    cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                    cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                    cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                    cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                    cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value22", item.Seller.TotalQty);
                                                    cmd4.Parameters.AddWithValue("@value23", item.Seller.TotalRate);
                                                    cmd4.Parameters.AddWithValue("@value24", item.Seller.TotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value25", item.Seller.TotalDiscount);
                                                    cmd4.Parameters.AddWithValue("@value26", item.Seller.TotalTaxableAmount);
                                                    cmd4.Parameters.AddWithValue("@value27", item.Seller.TotalCGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value28", item.Seller.TotalSGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value29", item.Seller.TotalIGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value30", item.Seller.TotalAmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value31", item.Seller.GrandTotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value32", item.Seller.GrandTotalAmountInWord);
                                                    cmd4.Parameters.AddWithValue("@value33", "False");
                                                    cmd4.Parameters.AddWithValue("@value34", "False");
                                                    cmd4.Parameters.AddWithValue("@value35", "False");
                                                    cmd4.Parameters.AddWithValue("@value36", "False");
                                                    cmd4.Parameters.AddWithValue("@value37", "False");
                                                    cmd4.Parameters.AddWithValue("@value38", "False");
                                                    cmd4.Parameters.AddWithValue("@value39", "False");
                                                    cmd4.Parameters.AddWithValue("@value40", "False");
                                                    cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE LATER
                                                    cmd4.Parameters.AddWithValue("@value42", "False");
                                                    cmd4.Parameters.AddWithValue("@value43", Todt);
                                                    cmd4.Parameters.AddWithValue("@value44", "False");
                                                    cmd4.Parameters.AddWithValue("@value44", "True");
                                                    cmd4.Parameters.AddWithValue("@value46", "False");
                                                    cmd4.Parameters.AddWithValue("@value47", "False");
                                                    cmd4.Parameters.AddWithValue("@value48", "True");
                                                    cmd4.Parameters.AddWithValue("@value49", isInter);
                                                    cmd4.Parameters.AddWithValue("@value50", item.Seller.SellerStateCode);
                                                    cmd4.Parameters.AddWithValue("@value51", item.Seller.SellerStateName);
                                                    cmd4.Parameters.AddWithValue("@value52", item.StateCode);
                                                    cmd4.Parameters.AddWithValue("@value53", item.StateName);
                                                    cmd4.Parameters.AddWithValue("@value54", item.Consignee.StateCode);
                                                    cmd4.Parameters.AddWithValue("@value55", item.Consignee.StateName);
                                                    cmd4.Parameters.AddWithValue("@value56", "False");
                                                    cmd4.Parameters.AddWithValue("@value57", "False");
                                                    cmd4.Parameters.AddWithValue("@value58", le.HSN.Cess);

                                                    result = cmd4.ExecuteNonQuery();
                                                    if (result > 0)
                                                    {
                                                        UpdateGSTR1(item.Seller.SellerInvoice, Todt, "N");
                                                    }

                                                }
                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                        }
                                        #endregion
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                            catch
                            {

                            }
                            finally
                            {

                            }
                        }



                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }

                #endregion

            }
            return result;
        }

        #region UpdateGSTR1 //hello
        protected int UpdateGSTR1(string SellerInvoice, string Todt, string flag)
        {
            int result = 0;

            string FilePath = "FileGSTR1";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    if (flag == "N")
                    {
                        OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsAcceptGSTR2A ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                        result = cmd.ExecuteNonQuery();


                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsAcceptGSTR2A ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();

                    }
                    else if (flag == "EditGSTR2A")
                    {
                        OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsEditedByReceiver  ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                        result = cmd.ExecuteNonQuery();


                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsEditedByReceiver  ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();

                    }
                    else if (flag == "D")
                    {
                        OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsDeletedByReceiver  ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                        result = cmd.ExecuteNonQuery();


                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsDeletedByReceiver  ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();

                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion
        #endregion

        #region ViewGSTR2
        /* PATCH */
        public DataSet ViewGSTR2(string strReceiverGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();
            ds = GetSellerGSTR2Details(strReceiverGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerGSTR2ADetails
        /* PATCH */
        public DataSet GetSellerGSTR2Details(string strReceiverGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /* PATCH */

            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            DataTable dtInvSummery = new DataTable("SummeryDtls");
            /*END  B2B invoice */
            FilePath = "FileGSTR2A";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where ReceiverGSTN ='" + strReceiverGSTN + "' and IsUploadGSTR2='False' and  FileGSTR2ADate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where ReceiverGSTN ='" + strReceiverGSTN + "'  and IsUploadGSTR2='False' and  FileGSTR2ADate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);


                    cmd.CommandText = "SELECT distinct SellerGSTN,InvoiceNo,Invoicedate,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where ReceiverGSTN ='" + strReceiverGSTN + "'  and IsUploadGSTR2='False' and  FileGSTR2ADate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtInvSummery);
                    ds.Tables.Add(dtInvSummery);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            return ds;
        }




        #endregion

        #endregion

        #region SaveGSTR2
        /* PATCH */
        public int SaveGSTR2(List<Reciever> RecieverData, string strInvoiceNo)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Reciever reciever = new Reciever();

            reciever.RecieverData = new List<Reciever>();

            reciever.RecieverData = RecieverData;

            string FilePath = string.Empty;
            string connectionString = string.Empty;

            string isEditedByReceiver = string.Empty;
            string isAddedByReceiver = string.Empty;
            string isAcceptedByReceiver = string.Empty;
            string isDeletedByReceiver = string.Empty;
            string isRejectedByReceiver = string.Empty;


            foreach (var item in reciever.RecieverData)
            {
                //TO UPLOAD A PERTICULAR INVOICE


                #region B2B
                FilePath = "FileGSTR2";
                connectionString = GetConnectionString(FilePath);
                bool IsInter; string isInter = string.Empty;

                foreach (LineEntry le in item.Invoice.LineEntry)
                {
                    /* Isintra calculation*/

                    IsInter = le.IsInter;
                    if (IsInter == true)
                    {
                        isInter = "True";
                    }
                    else if (IsInter == false)
                    {
                        isInter = "False";
                    }                   
                }

               
                if (item.Seller.IsEditedByReceiver == true)
                {
                    isEditedByReceiver = "True";
                }
                else
                {
                    isEditedByReceiver = "False";
                }
                if (item.Seller.IsAddedByReceiver == true)
                {
                    isAddedByReceiver = "True";
                }
                else
                {
                    isAddedByReceiver = "False";
                }
                if (item.Seller.IsAcceptedByReceiver == true)
                {
                    isAcceptedByReceiver = "True";
                }
                else
                {
                    isAcceptedByReceiver = "False";
                }
                if (item.Seller.IsDeletedByReceiver == true)
                {
                    isDeletedByReceiver = "True";
                }
                else
                {
                    isDeletedByReceiver = "False";
                }
                if (item.Seller.IsRejectedByReceiver == true)
                {
                    isRejectedByReceiver = "True";
                }
                else
                {
                    isRejectedByReceiver = "False";
                }
                /**/

                
                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    cn.Open();

                    try
                    {

                        #region sellerData
                        OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                             "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverFinancialPeriod],[FileGSTR2Date],[IsAcceptGSTR1A],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[ReceiverStateName],[ConsigneeStateName],[IsAcceptedBySeller],[IsRejectedBySeller],[IsDeletedByReceiver],[IsRejectedByReceiver]) " +
                             "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31,@value32,@value33,@value34,@value35)", cn);
                        cmd1.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                        cmd1.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                        cmd1.Parameters.AddWithValue("@value3", Todt);
                        cmd1.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                        cmd1.Parameters.AddWithValue("@value5", item.Seller.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value6", item.GSTIN);
                        cmd1.Parameters.AddWithValue("@value7", item.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                        cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                        cmd1.Parameters.AddWithValue("@value10", item.Seller.Address);
                        cmd1.Parameters.AddWithValue("@value11", item.Seller.SellerStateCode);
                        cmd1.Parameters.AddWithValue("@value12", item.Seller.SellerStateName);
                        cmd1.Parameters.AddWithValue("@value13", item.Seller.SellerStateCodeID);
                        cmd1.Parameters.AddWithValue("@value14", item.StateCode);
                        cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                        cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                        cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                        cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                        cmd1.Parameters.AddWithValue("@value19", "True");                      
                        cmd1.Parameters.AddWithValue("@value20", "B2B");                       
                        cmd1.Parameters.AddWithValue("@value21", item.Seller.SellerGrossTurnOver);
                        cmd1.Parameters.AddWithValue("@value22", item.Seller.SellerFinancialPeriod);                      
                        cmd1.Parameters.AddWithValue("@value23", item.FinancialPeriod);                      
                        cmd1.Parameters.AddWithValue("@value24", Todt);
                        cmd1.Parameters.AddWithValue("@value25", "False");
                        cmd1.Parameters.AddWithValue("@value26", isEditedByReceiver);
                        cmd1.Parameters.AddWithValue("@value27", isAddedByReceiver);
                        cmd1.Parameters.AddWithValue("@value28", isAcceptedByReceiver);
                        cmd1.Parameters.AddWithValue("@value29", isInter);
                        cmd1.Parameters.AddWithValue("@value30", item.StateName);
                        cmd1.Parameters.AddWithValue("@value31", item.Consignee.StateName);                       
                        cmd1.Parameters.AddWithValue("@value32", "False");
                        cmd1.Parameters.AddWithValue("@value33", "False");
                        cmd1.Parameters.AddWithValue("@value34", isDeletedByReceiver);
                        cmd1.Parameters.AddWithValue("@value35", isRejectedByReceiver);

                        result = cmd1.ExecuteNonQuery();
                        #endregion

                        if (result > 0)
                        {
                            #region Receiver
                            try
                            {
                                OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                               "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                               "VALUES(@value1, @value2,@value3,@value4)", cn);
                                cmd2.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                cmd2.Parameters.AddWithValue("@value2", item.GSTIN);
                                cmd2.Parameters.AddWithValue("@value3", item.NameAsOnGST);
                                cmd2.Parameters.AddWithValue("@value4", item.StateCode);

                                result = cmd2.ExecuteNonQuery();
                            #endregion

                                if (result > 0)
                                {

                                    #region Consignee
                                    try
                                    {
                                        OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                       "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                       "VALUES(@value1, @value2,@value3,@value4)", cn);
                                        cmd3.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                        cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                        cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                        cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                        result = cmd3.ExecuteNonQuery();

                                    #endregion

                                        #region LineEntry
                                        if (result > 0)
                                        {
                                            try
                                            {
                                                
                                                foreach (LineEntry le in item.Invoice.LineEntry)
                                                {

                                                    OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                  "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[FileGSTR2Date],[IsAcceptGSTR1A],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[SellerStateCode],[SellerStateName],[ReceiverStateCode],[ReceiverStateName],[ConsigneeStateCode],[ConsigneeStateName],[IsAcceptedBySeller],[IsRejectedBySeller],[IsDeletedByReceiver],[Cess],[IsRejectedByReceiver]) " +
                                                    "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11,@value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49,@value50,@value51,@value52,@value53,@value54,@value55,@value56,@value57,@value58,@value59)", cn);

                                                    cmd4.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                                    cmd4.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                                                    cmd4.Parameters.AddWithValue("@value3", Todt);
                                                    cmd4.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value5", item.GSTIN);
                                                    cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                    cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                    cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                    cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                    cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                    cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                    cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                    cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                    cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                    cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                    cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                    cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                    cmd4.Parameters.AddWithValue("@value22", item.Seller.TotalQty);
                                                    cmd4.Parameters.AddWithValue("@value23", item.Seller.TotalRate);
                                                    cmd4.Parameters.AddWithValue("@value24", item.Seller.TotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value25", item.Seller.TotalDiscount);
                                                    cmd4.Parameters.AddWithValue("@value26", item.Seller.TotalTaxableAmount);
                                                    cmd4.Parameters.AddWithValue("@value27", item.Seller.TotalCGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value28", item.Seller.TotalSGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value29", item.Seller.TotalIGSTAmount);
                                                    cmd4.Parameters.AddWithValue("@value30", item.Seller.TotalAmountWithTax);
                                                    cmd4.Parameters.AddWithValue("@value31", item.Seller.GrandTotalAmount);
                                                    cmd4.Parameters.AddWithValue("@value32", item.Seller.GrandTotalAmountInWord);
                                                    cmd4.Parameters.AddWithValue("@value33", "False");
                                                    cmd4.Parameters.AddWithValue("@value34", "False");
                                                    cmd4.Parameters.AddWithValue("@value35", "False");
                                                    cmd4.Parameters.AddWithValue("@value36", "False");
                                                    cmd4.Parameters.AddWithValue("@value37", "False");
                                                    cmd4.Parameters.AddWithValue("@value38", "False");
                                                    cmd4.Parameters.AddWithValue("@value39", "False");
                                                    cmd4.Parameters.AddWithValue("@value40", "False");
                                                    cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE LATER
                                                    cmd4.Parameters.AddWithValue("@value42", "False");                                         
                                                    cmd4.Parameters.AddWithValue("@value43", Todt);
                                                    cmd4.Parameters.AddWithValue("@value44", "False");
                                                    cmd4.Parameters.AddWithValue("@value45", isEditedByReceiver);
                                                    cmd4.Parameters.AddWithValue("@value46", isAddedByReceiver);
                                                    cmd4.Parameters.AddWithValue("@value47", isAcceptedByReceiver);
                                                    cmd4.Parameters.AddWithValue("@value48", isInter);
                                                    cmd4.Parameters.AddWithValue("@value49", item.Seller.SellerStateCode);
                                                    cmd4.Parameters.AddWithValue("@value50", item.Seller.SellerStateName);
                                                    cmd4.Parameters.AddWithValue("@value51", item.StateCode);
                                                    cmd4.Parameters.AddWithValue("@value52", item.StateName);
                                                    cmd4.Parameters.AddWithValue("@value53", item.Consignee.StateCode);
                                                    cmd4.Parameters.AddWithValue("@value54", item.Consignee.StateName);  
                                                    cmd4.Parameters.AddWithValue("@value55", "False");
                                                    cmd4.Parameters.AddWithValue("@value56", "False");
                                                    cmd4.Parameters.AddWithValue("@value57", isDeletedByReceiver);
                                                    cmd4.Parameters.AddWithValue("@value58", le.HSN.Cess);
                                                    cmd4.Parameters.AddWithValue("@value59", isRejectedByReceiver);

                                                    result = cmd4.ExecuteNonQuery();
                                                    if (result > 0)
                                                    {
                                                        UpdateGSTR2A(item.Seller.SellerInvoice, Todt);
                                                    }

                                                }
                                            }
                                            catch
                                            {

                                            }
                                            finally
                                            {

                                            }
                                        }
                                        #endregion
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                            catch
                            {

                            }
                            finally
                            {

                            }
                        }



                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }

                #endregion

            }
            return result;
        }

        #region UpdateGSTR2A
        protected int UpdateGSTR2A(string SellerInvoice, string Todt)
        {
            int result = 0;

            string FilePath = "FileGSTR2A";

            string connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsUploadGSTR2 ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsUploadGSTR2 ='True' where InvoiceNo='" + SellerInvoice + "'", cn);

                        result = cmd1.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            return result;

        }
        #endregion
        #endregion

        #region ViewGSTR1A
        /* PATCH */
        public DataSet ViewGSTR1A(string SellerGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();
            ds = GetSellerGSTR1ADetails(SellerGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerGSTR1ADetails
        public DataSet GetSellerGSTR1ADetails(string SellerGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            DataTable dtInvSummery = new DataTable("SummeryDtls");
            /*END  B2B invoice */
            FilePath = "FileGSTR2";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "'  and IsAcceptGSTR1A='False' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);


                    cmd.CommandText = "SELECT * FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and IsAcceptGSTR1A='False'  and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);


                    cmd.CommandText = "SELECT distinct InvoiceNo,Invoicedate,ReceiverGSTN,TotalQty,TotalRate,TotalDiscount,TotalAmount,TotalCGSTAmount,TotalSGSTAmount,TotalIGSTAmount,TotalTaxableAmount,TotalAmountWithTax FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsAcceptGSTR1A='False'  and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    da2.Fill(dtInvSummery);
                    ds.Tables.Add(dtInvSummery);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion


            return ds;
        }




        #endregion

        #endregion

        #region SaveGSTR1A
        /* PATCH */
        public int SaveGSTR1A(List<Seller> SellerDaTa, string strInvoiceNo, string flag)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Seller seller = new Seller();

            seller.SellerDaTa = new List<Seller>();

            seller.SellerDaTa = SellerDaTa;

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            string IsAcceptedBySeller = string.Empty;
            string IsRejectedBySeller = string.Empty;

            string isEditedByReceiver = string.Empty;
            string isAddedByReceiver = string.Empty;
            string isAcceptedByReceiver = string.Empty;
            string isDeletedByReceiver = string.Empty;
            string isRejectedByReceiver = string.Empty;

            if (flag == "R")
            {
                IsAcceptedBySeller = "False";
                IsRejectedBySeller = "True";
            }
            else if (flag == "A")
            {
                IsAcceptedBySeller = "True";
                IsRejectedBySeller = "False";
            }

            foreach (var item in seller.SellerDaTa)
            {
                if (item.SellerInvoice == strInvoiceNo)
                {
                    #region B2B
                    /* PATCH */
                    FilePath = "FileGSTR1A";
                    connectionString = GetConnectionString(FilePath);
                    bool IsInter; string isInter = string.Empty;
                   
                    foreach (LineEntry le in item.Invoice.LineEntry)
                    {                      
                        /* Isintra calculation*/

                        IsInter = le.IsInter;
                        if (IsInter == true)
                        {
                            isInter = "True";
                        }
                        else if (IsInter == false)
                        {
                            isInter = "False";
                        }
                       
                    }

                  
                    if (item.IsEditedByReceiver == true)
                    {
                        isEditedByReceiver = "True";
                    }
                    else
                    {
                        isEditedByReceiver = "False";
                    }
                    if (item.IsAddedByReceiver == true)
                    {
                        isAddedByReceiver = "True";
                    }
                    else
                    {
                        isAddedByReceiver = "False";
                    }
                    if (item.IsAcceptedByReceiver == true)
                    {
                        isAcceptedByReceiver = "True";
                    }
                    else
                    {
                        isAcceptedByReceiver = "False";
                    }
                    if (item.IsDeletedByReceiver == true)
                    {
                        isDeletedByReceiver = "True";
                    }
                    else
                    {
                        isDeletedByReceiver = "False";
                    }
                    if (item.IsRejectedByReceiver == true)
                    {
                        isRejectedByReceiver = "True";
                    }
                    else
                    {
                        isRejectedByReceiver = "False";
                    }

                    /**/

                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {
                            #region sellerData
                          
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsMatched],[InvoiceType],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverFinancialPeriod],[FileGSTR1ADate],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsDeletedByReceiver],[IsRejectedByReceiver],[IsAcceptedBySeller],[IsRejectedBySeller],[IsInter],[ReceiverStateName],[ConsigneeStateName]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31,@value32,@value33,@value34)", cn);
                            cmd1.Parameters.AddWithValue("@value1", item.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", "");
                            cmd1.Parameters.AddWithValue("@value4", item.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", item.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", item.Reciever.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", item.Reciever.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", item.Address);
                            cmd1.Parameters.AddWithValue("@value11", item.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", item.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", item.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", item.Reciever.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value19", "True");                         
                            cmd1.Parameters.AddWithValue("@value20", "B2B");                         
                            cmd1.Parameters.AddWithValue("@value21", item.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value22", item.SellerFinancialPeriod);                           
                            cmd1.Parameters.AddWithValue("@value23", item.Reciever.FinancialPeriod);                           
                            cmd1.Parameters.AddWithValue("@value24", Todt);
                            cmd1.Parameters.AddWithValue("@value25", isEditedByReceiver);
                            cmd1.Parameters.AddWithValue("@value26", isAddedByReceiver);
                            cmd1.Parameters.AddWithValue("@value27", isAcceptedByReceiver);
                            cmd1.Parameters.AddWithValue("@value28", isDeletedByReceiver);
                            cmd1.Parameters.AddWithValue("@value29", isRejectedByReceiver);
                            cmd1.Parameters.AddWithValue("@value30", IsAcceptedBySeller);
                            cmd1.Parameters.AddWithValue("@value31", IsRejectedBySeller);
                            cmd1.Parameters.AddWithValue("@value32", isInter);
                            cmd1.Parameters.AddWithValue("@value33", item.Reciever.StateName);
                            cmd1.Parameters.AddWithValue("@value34", item.Consignee.StateName);
                            result = cmd1.ExecuteNonQuery();

                            #endregion

                            if (result > 0)
                            {
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", item.Reciever.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", item.Reciever.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", item.Reciever.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion

                                    if (result > 0)
                                    {

                                        #region Consignee
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();

                                        #endregion

                                            #region LineEntry
                                            if (result > 0)
                                            {
                                                try
                                                {
                                                   
                                                    foreach (LineEntry le in item.Invoice.LineEntry)
                                                    {

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                       "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[FileGSTR1ADate],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsDeletedByReceiver],[IsRejectedByReceiver],[IsAcceptedBySeller],[IsRejectedBySeller],[IsInter],[SellerStateCode],[SellerStateName],[ReceiverStateCode],[ReceiverStateName],[ConsigneeStateCode],[ConsigneeStateName],[Cess]) " +
                                                         "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49,@value50,@value51,@value52,@value53,@value54,@value55,@value56,@value57,@value58)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", item.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", item.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", "");
                                                        cmd4.Parameters.AddWithValue("@value4", item.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", item.Reciever.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", item.TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", item.TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", item.TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", item.TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", item.TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", item.TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", item.TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", item.TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", item.TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", item.GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", item.GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False"); 
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", Todt);
                                                        cmd4.Parameters.AddWithValue("@value44", isEditedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value45", isAddedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value46", isAcceptedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value47", isDeletedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value48", isRejectedByReceiver);
                                                        cmd4.Parameters.AddWithValue("@value49", IsAcceptedBySeller);
                                                        cmd4.Parameters.AddWithValue("@value50", IsRejectedBySeller);
                                                        cmd4.Parameters.AddWithValue("@value51", isInter);
                                                        cmd4.Parameters.AddWithValue("@value52", item.SellerStateCode);
                                                        cmd4.Parameters.AddWithValue("@value53", item.SellerStateName);
                                                        cmd4.Parameters.AddWithValue("@value54", item.Reciever.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value55", item.Reciever.StateName);
                                                        cmd4.Parameters.AddWithValue("@value56", item.Consignee.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value57", item.Consignee.StateName);
                                                        cmd4.Parameters.AddWithValue("@value58", le.HSN.Cess);
                                                        result = cmd4.ExecuteNonQuery();

                                                        if (result > 0)
                                                        {
                                                            UpdateGSTR2(item.SellerInvoice, Todt, flag);
                                                        }

                                                    }
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }



                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }

                    #endregion
                }


            }
            return result;
        }

        #region UpdateGSTR2
        protected int UpdateGSTR2(string SellerInvoice, string Todt, string flag)
        {
            int result = 0;

            string FilePath = "FileGSTR2";

            string connectionString = GetConnectionString(FilePath);

            #region ACCEPTED_BY_SELLER
            if (flag == "A")
            {
                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        cn.Open();

                        OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsAcceptedBySeller ='True',IsRejectedBySeller ='False'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                        result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsAcceptedBySeller ='True',IsRejectedBySeller ='False'  where InvoiceNo='" + SellerInvoice + "'", cn);

                            result = cmd1.ExecuteNonQuery();
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            #endregion

            #region REJECTED_BY_SELLER
            else if (flag == "R")
            {
                using (OleDbConnection cn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        cn.Open();

                        OleDbCommand cmd = new OleDbCommand("Update [SellerInvoiceData$] set IsAcceptedBySeller ='False',IsRejectedBySeller ='True'  where InvoiceNo ='" + SellerInvoice + "'", cn);

                        result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            OleDbCommand cmd1 = new OleDbCommand("Update [HSNData$] set IsAcceptedBySeller ='False',IsRejectedBySeller ='True'  where InvoiceNo='" + SellerInvoice + "'", cn);

                            result = cmd1.ExecuteNonQuery();
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            #endregion
            return result;

        }
        #endregion


        #endregion

        #region FetchInvoiceDataTOEditInvoice
        /* PATCH */
        public DataSet FetchInvoiceDataTOEditInvoice(string strInvoiceNo)
        {
            DataSet ds = new DataSet();
            ds = GetInvoiceDataTOEditInvoice(strInvoiceNo);
            return ds;
        }

        #region GetInvoiceDataTOEditInvoice
        /* PATCH */
        public DataSet GetInvoiceDataTOEditInvoice(string strInvoiceNo)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string FilePath = string.Empty;
            string connectionString = string.Empty;


            #region B2B
            /* PATCH */

            /*START  B2B invoice */
            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtReceiverDtls = new DataTable("ReceiverDtls");
            DataTable dtConsigneeDtls = new DataTable("ConsigneeDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");
            /*END  B2B invoice */
            FilePath = "InvoiceData";

            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT * FROM [SellerInvoiceData$] where InvoiceNo ='" + strInvoiceNo + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtSellerDtls);
                    ds.Tables.Add(dtSellerDtls);

                    //cmd.CommandText = "SELECT * FROM [ReceiverInvoiceData$] where InvoiceNo ='" + strInvoiceNo + "' ";
                    //OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    //da1.Fill(dtReceiverDtls);
                    //ds.Tables.Add(dtReceiverDtls);

                    //cmd.CommandText = "SELECT * FROM [ConsigneeInvoiceData$] where InvoiceNo ='" + strInvoiceNo + "' ";
                    //OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                    //da2.Fill(dtConsigneeDtls);
                    //ds.Tables.Add(dtConsigneeDtls);

                    cmd.CommandText = "SELECT * FROM [HSNData$] where InvoiceNo ='" + strInvoiceNo + "' ";
                    OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                    da3.Fill(dtInvoice);
                    ds.Tables.Add(dtInvoice);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            return ds;
        }

        #endregion
        #endregion

        #region DeleteGSTR2A
        /* PATCH */
        public int DeleteGSTR2A(List<Reciever> RecieverData, string strInvoiceNo)
        {
            int result = 0;
            string Todt = DateTime.Now.ToString("dd/MM/yyyy");
            Reciever reciever = new Reciever();

            reciever.RecieverData = new List<Reciever>();

            reciever.RecieverData = RecieverData;

            string FilePath = string.Empty;
            string connectionString = string.Empty;
            bool IsInter; string isInter = string.Empty;


            foreach (var item in reciever.RecieverData)
            {
                if (item.Seller.SellerInvoice == strInvoiceNo)
                {

                    #region B2B
                    FilePath = "FileGSTR2A";
                    connectionString = GetConnectionString(FilePath);

                    /* CALCULATE TOTAL */
                    decimal TotalTaxableAmount = 0; decimal TotalAmount = 0; decimal TotalDiscount = 0;
                    decimal TotalCGSTAmount = 0; decimal TotalSGSTAmount = 0; decimal TotalIGSTAmount = 0;
                    decimal TotalQty = 0; decimal TotalRate = 0; decimal TotalAmountWithTax = 0;

                    foreach (LineEntry le in item.Invoice.LineEntry)
                    {
                        /* START  AMOUNT IN WORDS*/


                        TotalTaxableAmount += le.TaxValue;
                        TotalAmount += le.TotalLineIDWise;
                        TotalDiscount += le.Discount;
                        TotalQty += le.Qty;
                        TotalRate += le.PerUnitRate;
                        if (le.HSN.RateIGST != 0)
                        {
                            TotalIGSTAmount += le.AmtIGSTLineIDWise;
                            TotalAmountWithTax += le.AmtIGSTLineIDWise + le.TaxValue;
                        }
                        else
                        {
                            TotalCGSTAmount += le.AmtCGSTLineIDWise;
                            TotalSGSTAmount += le.AmtSGSTLineIDWise;
                            TotalAmountWithTax += le.AmtCGSTLineIDWise + le.AmtCGSTLineIDWise + le.TaxValue;
                        }
                        /* Isintra calculation*/

                        IsInter = le.IsInter;
                        if (IsInter == true)
                        {
                            isInter = "True";
                        }
                        else if (IsInter == false)
                        {
                            isInter = "False";
                        }
                        /**/
                    }

                    decimal GrandTotalAmount = item.Invoice.Freight + item.Invoice.Insurance + item.Invoice.PackingAndForwadingCharges + TotalAmountWithTax;

                    string GrandTotalAmountInWord = ConvertNumbertoWords(Convert.ToDecimal(GrandTotalAmount));

                    /* END AMOUNT IN WORDS */

                    /* CALCULATE TOTAL */
                    using (OleDbConnection cn = new OleDbConnection(connectionString))
                    {
                        cn.Open();

                        try
                        {

                            #region sellerData
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [SellerInvoiceData$] " +
                                 "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[SellerName],[ReceiverGSTN],[ReceiverName],[ConsigneeGSTN],[ConsigneeName],[SellerAddress],[SellerStateCode],[SellerStateName],[SellerStateCodeID],[ReceiverStateCode],[ConsigneeStateCode],[Freight],[Insurance],[PackingAndForwadingCharges],[IsElectronicReferenceNoGenerated],[ElectronicReferenceNoGenerated],[ElectronicReferenceNoGeneratedDate],[IsMatched],[IsAdvance],[InvoiceType],[IsExport],[SellerGrossTurnOver],[SellerFinancialPeriod],[ReceiverGrossTurnOver],[ReceiverFinancialPeriod],[ConsigneeGrossTurnOver],[ConsigneeFinancialPeriod],[FileGSTR2ADate],[IsUploadGSTR2],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[ReceiverStateName],[ConsigneeStateName],[IsDeletedByReceiver]) " +
                                 "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9,@value10,@value11,@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22,@value23,@value24,@value25,@value26,@value27,@value28,@value29,@value30,@value31,@value32,@value33,@value34,@value35,@value36,@value37,@value38,@value39,@value40)", cn);
                            cmd1.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                            cmd1.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                            cmd1.Parameters.AddWithValue("@value3", Todt);
                            cmd1.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                            cmd1.Parameters.AddWithValue("@value5", item.Seller.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value6", item.GSTIN);
                            cmd1.Parameters.AddWithValue("@value7", item.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value8", item.Consignee.GSTIN);
                            cmd1.Parameters.AddWithValue("@value9", item.Consignee.NameAsOnGST);
                            cmd1.Parameters.AddWithValue("@value10", item.Seller.Address);
                            cmd1.Parameters.AddWithValue("@value11", item.Seller.SellerStateCode);
                            cmd1.Parameters.AddWithValue("@value12", item.Seller.SellerStateName);
                            cmd1.Parameters.AddWithValue("@value13", item.Seller.SellerStateCodeID);
                            cmd1.Parameters.AddWithValue("@value14", item.StateCode);
                            cmd1.Parameters.AddWithValue("@value15", item.Consignee.StateCode);
                            cmd1.Parameters.AddWithValue("@value16", item.Invoice.Freight);
                            cmd1.Parameters.AddWithValue("@value17", item.Invoice.Insurance);
                            cmd1.Parameters.AddWithValue("@value18", item.Invoice.PackingAndForwadingCharges);
                            cmd1.Parameters.AddWithValue("@value19", "True");
                            cmd1.Parameters.AddWithValue("@value20", "B2BACK01");
                            cmd1.Parameters.AddWithValue("@value21", Todt);
                            cmd1.Parameters.AddWithValue("@value22", "True");
                            cmd1.Parameters.AddWithValue("@value23", "False");
                            cmd1.Parameters.AddWithValue("@value24", "B2B");
                            cmd1.Parameters.AddWithValue("@value25", "False");
                            cmd1.Parameters.AddWithValue("@value26", item.Seller.SellerGrossTurnOver);
                            cmd1.Parameters.AddWithValue("@value27", item.Seller.SellerFinancialPeriod);
                          
                            cmd1.Parameters.AddWithValue("@value29", item.FinancialPeriod);
                           
                            cmd1.Parameters.AddWithValue("@value32", Todt);
                            cmd1.Parameters.AddWithValue("@value33", "False");
                            cmd1.Parameters.AddWithValue("@value34", "False");
                            cmd1.Parameters.AddWithValue("@value35", "False");
                            cmd1.Parameters.AddWithValue("@value36", "True");
                            cmd1.Parameters.AddWithValue("@value37", isInter);
                            cmd1.Parameters.AddWithValue("@value38", item.StateName);
                            cmd1.Parameters.AddWithValue("@value39", item.Consignee.StateName);
                            cmd1.Parameters.AddWithValue("@value40", "True");

                            result = cmd1.ExecuteNonQuery();

                            #endregion

                            if (result > 0)
                            {
                                #region Receiver
                                try
                                {
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO [ReceiverInvoiceData$] " +
                                   "([InvoiceNo],[ReceiverGSTN],[ReceiverName],[ReceiverStateCode]) " +
                                   "VALUES(@value1, @value2,@value3,@value4)", cn);
                                    cmd2.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                    cmd2.Parameters.AddWithValue("@value2", item.GSTIN);
                                    cmd2.Parameters.AddWithValue("@value3", item.NameAsOnGST);
                                    cmd2.Parameters.AddWithValue("@value4", item.StateCode);

                                    result = cmd2.ExecuteNonQuery();
                                #endregion

                                    if (result > 0)
                                    {

                                        #region Consignee
                                        try
                                        {
                                            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO [ConsigneeInvoiceData$] " +
                                           "([InvoiceNo],[ConsigneeGSTN],[ConsigneeName],[ConsigneeStateCode]) " +
                                           "VALUES(@value1, @value2,@value3,@value4)", cn);
                                            cmd3.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                            cmd3.Parameters.AddWithValue("@value2", item.Consignee.GSTIN);
                                            cmd3.Parameters.AddWithValue("@value3", item.Consignee.NameAsOnGST);
                                            cmd3.Parameters.AddWithValue("@value4", item.Consignee.StateCode);

                                            result = cmd3.ExecuteNonQuery();

                                        #endregion

                                            #region LineEntry
                                            if (result > 0)
                                            {
                                                try
                                                {
                                                    foreach (LineEntry le in item.Invoice.LineEntry)
                                                    {

                                                        OleDbCommand cmd4 = new OleDbCommand("INSERT INTO [HSNData$] " +
                                                      "([InvoiceNo],[Invoicedate],[UploadedInvoicedate],[SellerGSTN],[ReceiverGSTN],[LineID],[Description],[HSN],[Qty],[Unit],[Rate],[Total],[Discount],[TaxableValue],[AmountWithTax],[IGSTRate],[IGSTAmt],[CGSTRate],[CGSTAmt],[SGSTRate],[SGSTAmt],[TotalQty],[TotalRate],[TotalAmount],[TotalDiscount],[TotalTaxableAmount],[TotalCGSTAmount],[TotalSGSTAmount],[TotalIGSTAmount],[TotalAmountWithTax],[GrandTotalAmount],[GrandTotalAmountInWord],[isHSNNilRated],[isHSNExempted],[isHSNZeroRated],[isHSNNonGSTGoods],[isSACNilRated],[isSACEcxempted],[isSACZeroRated],[isSACNonGSTService],[IsNotifedGoods],[IsNotifiedSAC],[FileGSTR2ADate],[IsUploadGSTR2],[IsMatched],[IsEditedByReceiver],[IsAddedByReceiver],[IsAcceptedByReceiver],[IsInter],[SellerStateCode],[SellerStateName],[ReceiverStateCode],[ReceiverStateName],[ConsigneeStateCode],[ConsigneeStateName],[IsDeletedByReceiver]) " +
                                                        "VALUES(@value1, @value2,@value3, @value4,@value5, @value6,@value7, @value8,@value9, @value10,@value11, @value12,@value13, @value14,@value15, @value16, @value17, @value18, @value19, @value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30, @value31, @value32, @value33, @value34, @value35, @value36,@value37,@value38,@value39,@value40,@value41,@value42,@value43,@value44,@value45,@value46,@value47,@value48,@value49,@value50,@value51,@value52,@value53,@value54,@value55,@value56)", cn);

                                                        cmd4.Parameters.AddWithValue("@value1", item.Seller.SellerInvoice);
                                                        cmd4.Parameters.AddWithValue("@value2", item.Seller.DateOfInvoice);
                                                        cmd4.Parameters.AddWithValue("@value3", Todt);
                                                        cmd4.Parameters.AddWithValue("@value4", item.Seller.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value5", item.GSTIN);
                                                        cmd4.Parameters.AddWithValue("@value6", le.LineID);
                                                        cmd4.Parameters.AddWithValue("@value7", le.HSN.Description);
                                                        cmd4.Parameters.AddWithValue("@value8", le.HSN.HSNNumber);
                                                        cmd4.Parameters.AddWithValue("@value9", le.Qty);
                                                        cmd4.Parameters.AddWithValue("@value10", le.HSN.UnitOfMeasurement);
                                                        cmd4.Parameters.AddWithValue("@value11", le.PerUnitRate);
                                                        cmd4.Parameters.AddWithValue("@value12", le.TotalLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value13", le.Discount);
                                                        cmd4.Parameters.AddWithValue("@value14", le.TaxValue);
                                                        cmd4.Parameters.AddWithValue("@value15", le.AmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value16", le.HSN.RateIGST);
                                                        cmd4.Parameters.AddWithValue("@value17", le.AmtIGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value18", le.HSN.RateCGST);
                                                        cmd4.Parameters.AddWithValue("@value19", le.AmtCGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value20", le.HSN.RateSGST);
                                                        cmd4.Parameters.AddWithValue("@value21", le.AmtSGSTLineIDWise);
                                                        cmd4.Parameters.AddWithValue("@value22", TotalQty);
                                                        cmd4.Parameters.AddWithValue("@value23", TotalRate);
                                                        cmd4.Parameters.AddWithValue("@value24", TotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value25", TotalDiscount);
                                                        cmd4.Parameters.AddWithValue("@value26", TotalTaxableAmount);
                                                        cmd4.Parameters.AddWithValue("@value27", TotalCGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value28", TotalSGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value29", TotalIGSTAmount);
                                                        cmd4.Parameters.AddWithValue("@value30", TotalAmountWithTax);
                                                        cmd4.Parameters.AddWithValue("@value31", GrandTotalAmount);
                                                        cmd4.Parameters.AddWithValue("@value32", GrandTotalAmountInWord);
                                                        cmd4.Parameters.AddWithValue("@value33", "False");
                                                        cmd4.Parameters.AddWithValue("@value34", "False");
                                                        cmd4.Parameters.AddWithValue("@value35", "False");
                                                        cmd4.Parameters.AddWithValue("@value36", "False");
                                                        cmd4.Parameters.AddWithValue("@value37", "False");
                                                        cmd4.Parameters.AddWithValue("@value38", "False");
                                                        cmd4.Parameters.AddWithValue("@value39", "False");
                                                        cmd4.Parameters.AddWithValue("@value40", "False");
                                                        cmd4.Parameters.AddWithValue("@value41", "False"); //NEED TO CHNAGE LATER
                                                        cmd4.Parameters.AddWithValue("@value42", "False");
                                                        cmd4.Parameters.AddWithValue("@value43", Todt);
                                                        cmd4.Parameters.AddWithValue("@value44", "False");
                                                        cmd4.Parameters.AddWithValue("@value44", "True");
                                                        cmd4.Parameters.AddWithValue("@value46", "False");
                                                        cmd4.Parameters.AddWithValue("@value47", "False");
                                                        cmd4.Parameters.AddWithValue("@value48", "True");
                                                        cmd4.Parameters.AddWithValue("@value49", isInter);
                                                        cmd4.Parameters.AddWithValue("@value50", item.Seller.SellerStateCode);
                                                        cmd4.Parameters.AddWithValue("@value51", item.Seller.SellerStateName);
                                                        cmd4.Parameters.AddWithValue("@value52", item.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value53", item.StateName);
                                                        cmd4.Parameters.AddWithValue("@value54", item.Consignee.StateCode);
                                                        cmd4.Parameters.AddWithValue("@value55", item.Consignee.StateName);
                                                        cmd4.Parameters.AddWithValue("@value56", "True");

                                                        result = cmd4.ExecuteNonQuery();
                                                        if (result > 0)
                                                        {
                                                            UpdateGSTR1(item.Seller.SellerInvoice, Todt, "D");
                                                        }

                                                    }
                                                }
                                                catch
                                                {

                                                }
                                                finally
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                finally
                                {

                                }
                            }



                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }

                    #endregion
                }
            }
            return result;
        }


        #endregion


        //PATCH
        #region ViewGSTR3
        /* PATCH */
        public DataSet ViewGSTR3(string SellerGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();

            ds = GetSellerGSTR3Details(SellerGSTN, FromDt, Todate);
            return ds;
        }

        #region GetSellerGSTR3Details
        public DataSet GetSellerGSTR3Details(string SellerGSTN, string FromDt, String Todate)
        {
            string query = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string FilePath = string.Empty;
            string connectionString = string.Empty;

            string strConsigneeStateCode = string.Empty;
            decimal strIGSTRate = 0;
            decimal strIGSTAmt = 0;
            decimal strAmountWithTax = 0;

            decimal strCGSTRate = 0;
            decimal strCGSTAmt = 0;
            decimal strSGSTRate = 0;
            decimal strSGSTAmt = 0;

            List<string> ConsigneeStateCode = new List<string>();



            #region OUTWARD_Supply



            /*start-- ALL VARIABLES AND TABLES FOR OUTWARDE SUPPLY */


            /*START  B2B invoice */
            DataTable dtOutWardInterStateDtls = new DataTable("OutWardInterStateDtls");
            DataTable dtOutWardIntraStateDtls = new DataTable("OutWardIntraStateDtls");
            DataTable dtOutWardConsigneeStateCode = new DataTable("OutWardConsigneeStateCode");
            /*END  B2B invoice */


            /* start--DATATABLE FOR IS INTER CALCULATION */
            DataTable dtOutWardInter = new DataTable("OutWardInter");
            dtOutWardInter.Columns.Add("ConsigneeStateCode");
            dtOutWardInter.Columns.Add("IGSTRate");
            dtOutWardInter.Columns.Add("IGSTAmt");
            dtOutWardInter.Columns.Add("TaxableValue");
            /* end--DATATABLE FOR IS INTER CALCULATION */

            /* start--DATATABLE FOR ISTRA CALCULATION*/
            DataTable dtOutWardIntra = new DataTable("OutWardIntra");
            dtOutWardIntra.Columns.Add("ConsigneeStateCode");
            dtOutWardIntra.Columns.Add("CGSTRate");
            dtOutWardIntra.Columns.Add("CGSTAmt");
            dtOutWardIntra.Columns.Add("SGSTRate");
            dtOutWardIntra.Columns.Add("SGSTAmt");
            dtOutWardIntra.Columns.Add("TaxableValue");
            /* end--DATATABLE FOR ISTRA CALCULATION*/

            /*end-- ALL VARIABLES AND TABLES FOR OUTWARDE SUPPLY */

            #region B2B

            FilePath = "FileGSTR1";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct ConsigneeStateCode FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtOutWardConsigneeStateCode);


                    ConsigneeStateCode = GetConsigneeStateCodeFrmExl(dtOutWardConsigneeStateCode, SellerGSTN, ConsigneeStateCode);

                    int cnt = ConsigneeStateCode.Count;

                    #region isInter
                    for (int i = 0; i <= cnt - 1; i++)
                    {
                        strConsigneeStateCode = ConsigneeStateCode[i].ToString();

                        cmd.CommandText = "SELECT ConsigneeStateCode,IGSTRate,IGSTAmt,TaxableValue FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and ConsigneeStateCode ='" + strConsigneeStateCode + "' and IsInter='True' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                        da1.Fill(dtOutWardInterStateDtls);

                        if (dtOutWardInterStateDtls.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtOutWardInterStateDtls.Rows)
                            {
                                strConsigneeStateCode = dr["ConsigneeStateCode"].ToString();
                                strIGSTRate = Convert.ToDecimal(dr["IGSTRate"]);
                                strIGSTAmt += Convert.ToDecimal(dr["IGSTAmt"]);
                                strAmountWithTax += Convert.ToDecimal(dr["TaxableValue"]);
                            }


                            var row = dtOutWardInter.NewRow();
                            row["ConsigneeStateCode"] = strConsigneeStateCode;
                            row["IGSTRate"] = strIGSTRate;
                            row["IGSTAmt"] = Convert.ToString(strIGSTAmt);
                            row["TaxableValue"] = Convert.ToString(strAmountWithTax);

                            dtOutWardInter.Rows.Add(row);

                            dtOutWardInterStateDtls = new DataTable();
                            strConsigneeStateCode = string.Empty;
                            strIGSTRate = 0;
                            strIGSTAmt = 0;
                            strAmountWithTax = 0;
                        }
                    }
                    #endregion

                    strConsigneeStateCode = string.Empty;

                    #region isIntra
                    for (int i = 0; i <= cnt - 1; i++)
                    {

                        strConsigneeStateCode = ConsigneeStateCode[i].ToString();


                        cmd.CommandText = "SELECT ConsigneeStateCode,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,TaxableValue FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and ConsigneeStateCode ='" + strConsigneeStateCode + "' and IsInter='False' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                        OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                        da2.Fill(dtOutWardIntraStateDtls);

                        if (dtOutWardIntraStateDtls.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtOutWardIntraStateDtls.Rows)
                            {
                                strConsigneeStateCode = dr["ConsigneeStateCode"].ToString();
                                strCGSTRate = Convert.ToDecimal(dr["CGSTRate"]);
                                strCGSTAmt += Convert.ToDecimal(dr["CGSTAmt"]);
                                strSGSTRate = Convert.ToDecimal(dr["SGSTRate"]);
                                strSGSTAmt += Convert.ToDecimal(dr["SGSTAmt"]);
                                strAmountWithTax += Convert.ToDecimal(dr["TaxableValue"]);
                            }


                            var rw = dtOutWardIntra.NewRow();
                            rw["ConsigneeStateCode"] = strConsigneeStateCode;
                            rw["CGSTRate"] = strCGSTRate;
                            rw["CGSTAmt"] = Convert.ToString(strCGSTAmt);
                            rw["SGSTRate"] = strSGSTRate;
                            rw["SGSTAmt"] = Convert.ToString(strSGSTAmt);
                            rw["TaxableValue"] = Convert.ToString(strAmountWithTax);

                            dtOutWardIntra.Rows.Add(rw);

                            dtOutWardIntraStateDtls = new DataTable();
                            strConsigneeStateCode = string.Empty;
                            strIGSTRate = 0;
                            strIGSTAmt = 0;
                            strAmountWithTax = 0;
                        }
                    }
                    #endregion

                    //ds.Tables.Add(dtOutWardInter);
                    //ds.Tables.Add(dtOutWardIntra);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region Export
            DataTable dtExport = new DataTable("Export");
            FilePath = "ExportGSTR1";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct ExportNo,ExportDescription,TotalTaxableAmount,IGSTRate,TotalIGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtExport);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region REVISION_OF_SUPPLY_INVOICES

            DataTable dtInvoiceNo = new DataTable("InvoiceNo");
            DataTable dtInvoiceDtls = new DataTable("InvoiceDtls");
            DataTable dtInvoiceDtlsGSTR2 = new DataTable("InvoiceDtlsGSTR2");

            string strInvNo = string.Empty;
            string strInvDate = string.Empty;
            decimal strTaxableValue = 0;
            decimal strIGST = 0;
            decimal strCGST = 0;
            decimal strSGST = 0;
            decimal strAdditionalTax = 0;


            /* start--DATATABLE FOR ISTRA CALCULATION*/
            DataTable dtAmend = new DataTable("Amend");
            dtAmend.Columns.Add("InvoiceNo");
            dtAmend.Columns.Add("Invoicedate");
            dtAmend.Columns.Add("TotalTaxableAmount");
            dtAmend.Columns.Add("TotalCGSTAmount");
            dtAmend.Columns.Add("TotalIGSTAmount");
            dtAmend.Columns.Add("TotalSGSTAmount");
            /* end--DATATABLE FOR ISTRA CALCULATION*/

            FilePath = "FileGSTR1";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct InvoiceNo,Invoicedate FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTN + "' and IsEditedByReceiver='True' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoiceNo);

                    if (dtInvoiceNo.Rows.Count > 0)
                    {
                        int rwCnt = dtInvoiceNo.Rows.Count;

                        for (int i = 0; i <= rwCnt - 1; i++)
                        {
                            strInvNo = (ds.Tables[0].Rows[i][0]).ToString();
                            strInvDate = (ds.Tables[0].Rows[i][1]).ToString();


                            cmd.CommandText = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and InvoiceNo ='" + strInvNo + "' and IsEditedByReceiver='True' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                            OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                            da2.Fill(dtInvoiceDtls);

                            if (dtInvoiceDtls.Rows.Count == 1)
                            {
                                strTaxableValue = Convert.ToDecimal(dtInvoiceDtls.Rows[0][0]);
                                strCGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][1]);
                                strIGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][2]);
                                strSGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][3]);
                                //    strAdditionalTax = Convert.ToDecimal(ds.Tables[0].Rows[i][0]);


                                FilePath = "FileGSTR2";
                                connectionString = GetConnectionString(FilePath);

                                cmd.CommandText = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "' and InvoiceNo ='" + strInvNo + "' and IsEditedByReceiver='True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                                OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                                da3.Fill(dtInvoiceDtlsGSTR2);

                                if (dtInvoiceDtlsGSTR2.Rows.Count == 1)
                                {
                                    var row = dtAmend.NewRow();
                                    row["InvoiceNo"] = strInvNo;
                                    row["Invoicedate"] = strInvDate;
                                    row["TotalTaxableAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][0]) - strTaxableValue;
                                    row["TotalCGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][1]) - strCGST;
                                    row["TotalIGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][2]) - strIGST;
                                    row["TotalSGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][3]) - strSGST;

                                    dtAmend.Rows.Add(row);

                                    dtInvoiceDtlsGSTR2 = new DataTable();
                                    dtInvoiceDtls = new DataTable();
                                    strTaxableValue = 0;
                                    strCGST = 0;
                                    strIGST = 0;
                                    strSGST = 0;
                                    strInvNo = string.Empty;
                                    strInvDate = string.Empty;

                                }



                            }
                        }
                    }

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region TOTAL_TAX_LIABILITY

            DataTable dtTotalTaxLiability = new DataTable("TotalTaxLiability");

            FilePath = "FileGSTR1";
            connectionString = GetConnectionString(FilePath);


            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsEditedByReceiver='False' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            FilePath = "FileGSTR2";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsEditedByReceiver='True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            #endregion

            #endregion


            #region INWARD_Supply

            FilePath = "FileGSTR2";
            connectionString = GetConnectionString(FilePath);

            /*start-- ALL VARIABLES AND TABLES FOR OUTWARDE SUPPLY */


            /*START  B2B invoice */
            DataTable dtInWardInterStateDtls = new DataTable("InWardInterStateDtls");
            DataTable dtInWardIntraStateDtls = new DataTable("InWardIntraStateDtls");
            DataTable dtInWardConsigneeStateCode = new DataTable("InWardConsigneeStateCode");
            /*END  B2B invoice */


            /* start--DATATABLE FOR IS INTER CALCULATION */
            DataTable dtInWardInter = new DataTable("InWardInter");
            dtInWardInter.Columns.Add("ConsigneeStateCode");
            dtInWardInter.Columns.Add("IGSTRate");
            dtInWardInter.Columns.Add("IGSTAmt");
            dtInWardInter.Columns.Add("AmountWithTax");
            /* end--DATATABLE FOR IS INTER CALCULATION */

            /* start--DATATABLE FOR ISTRA CALCULATION*/
            DataTable dtInWardIntra = new DataTable("InWardIntra");
            dtInWardIntra.Columns.Add("ConsigneeStateCode");
            dtInWardIntra.Columns.Add("CGSTRate");
            dtInWardIntra.Columns.Add("CGSTAmt");
            dtInWardIntra.Columns.Add("SGSTRate");
            dtInWardIntra.Columns.Add("SGSTAmt");
            dtInWardIntra.Columns.Add("AmountWithTax");
            /* end--DATATABLE FOR ISTRA CALCULATION*/

            /*end-- ALL VARIABLES AND TABLES FOR OUTWARDE SUPPLY */

            #region B2B
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct ConsigneeStateCode FROM [SellerInvoiceData$] where ReceiverGSTN ='" + SellerGSTN + "' and IsReverseCharged <> 'True' and IsImported <> 'True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtInWardConsigneeStateCode);

                    ConsigneeStateCode = new List<string>();

                    ConsigneeStateCode = GetConsigneeStateCodeFrmExl(dtInWardConsigneeStateCode, SellerGSTN, ConsigneeStateCode);

                    int cnt = ConsigneeStateCode.Count;

                    #region isInter
                    for (int i = 0; i <= cnt - 1; i++)
                    {
                        strConsigneeStateCode = ConsigneeStateCode[i].ToString();

                        cmd.CommandText = "SELECT ConsigneeStateCode,IGSTRate,IGSTAmt,TaxableValue FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and ConsigneeStateCode ='" + strConsigneeStateCode + "' and IsInter='True' and IsAcceptedBySeller='True' and IsReverseCharged !='True' and IsImported <> 'True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                        OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                        da1.Fill(dtInWardInterStateDtls);

                        if (dtInWardInterStateDtls.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtInWardInterStateDtls.Rows)
                            {
                                strConsigneeStateCode = dr["ConsigneeStateCode"].ToString();
                                strIGSTRate = Convert.ToDecimal(dr["IGSTRate"]);
                                strIGSTAmt += Convert.ToDecimal(dr["IGSTAmt"]);
                                strAmountWithTax += Convert.ToDecimal(dr["AmountWithTax"]);
                            }


                            var row = dtInWardInter.NewRow();
                            row["ConsigneeStateCode"] = strConsigneeStateCode;
                            row["IGSTRate"] = strIGSTRate;
                            row["IGSTAmt"] = Convert.ToString(strIGSTAmt);
                            row["AmountWithTax"] = Convert.ToString(strAmountWithTax);

                            dtInWardInter.Rows.Add(row);

                            dtInWardInterStateDtls = new DataTable();
                            strConsigneeStateCode = string.Empty;
                            strIGSTRate = 0;
                            strIGSTAmt = 0;
                            strAmountWithTax = 0;
                        }
                    }
                    #endregion

                    strConsigneeStateCode = string.Empty;

                    #region isIntra
                    for (int i = 0; i <= cnt - 1; i++)
                    {

                        strConsigneeStateCode = ConsigneeStateCode[i].ToString();


                        cmd.CommandText = "SELECT ConsigneeStateCode,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,TaxableValue FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and ConsigneeStateCode ='" + strConsigneeStateCode + "' and IsInter='False' and IsAcceptedBySeller='True' and IsReverseCharged <> 'True' and IsImported <> 'True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                        OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                        da2.Fill(dtInWardIntraStateDtls);

                        if (dtInWardIntraStateDtls.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtInWardIntraStateDtls.Rows)
                            {
                                strConsigneeStateCode = dr["ConsigneeStateCode"].ToString();
                                strCGSTRate = Convert.ToDecimal(dr["CGSTRate"]);
                                strCGSTAmt += Convert.ToDecimal(dr["CGSTAmt"]);
                                strSGSTRate = Convert.ToDecimal(dr["SGSTRate"]);
                                strSGSTAmt += Convert.ToDecimal(dr["SGSTAmt"]);
                                strAmountWithTax += Convert.ToDecimal(dr["AmountWithTax"]);
                            }


                            var rw = dtInWardIntra.NewRow();
                            rw["ConsigneeStateCode"] = strConsigneeStateCode;
                            rw["CGSTRate"] = strIGSTRate;
                            rw["CGSTAmt"] = Convert.ToString(strIGSTAmt);
                            rw["SGSTRate"] = strIGSTRate;
                            rw["SGSTAmt"] = Convert.ToString(strIGSTAmt);
                            rw["AmountWithTax"] = Convert.ToString(strAmountWithTax);

                            dtInWardIntra.Rows.Add(rw);

                            dtInWardIntraStateDtls = new DataTable();
                            strConsigneeStateCode = string.Empty;
                            strIGSTRate = 0;
                            strIGSTAmt = 0;
                            strAmountWithTax = 0;
                        }
                    }
                    #endregion

                    //ds.Tables.Add(dtInWardInter);
                    //ds.Tables.Add(dtInWardIntra);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region Import
            DataTable dtImport = new DataTable("Import");
            FilePath = "Import";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct ImportNo,Importdate,ImportDescription,TotalTaxableAmount,IGSTRate,TotalIGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and  Importdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtImport);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region REVISION_OF_PURCHASE_INVOICES

            dtInvoiceNo = new DataTable("InvoiceNo");
            dtInvoiceDtls = new DataTable("InvoiceDtls");
            dtInvoiceDtlsGSTR2 = new DataTable("InvoiceDtlsGSTR2");

            strInvNo = string.Empty;
            strInvDate = string.Empty;
            strTaxableValue = 0;
            strIGST = 0;
            strCGST = 0;
            strSGST = 0;
            strAdditionalTax = 0;


            /* start--DATATABLE FOR ISTRA CALCULATION*/
            DataTable dtPurchaseInvoice = new DataTable("PurchaseInvoice");
            dtPurchaseInvoice.Columns.Add("InvoiceNo");
            dtPurchaseInvoice.Columns.Add("Invoicedate");
            dtPurchaseInvoice.Columns.Add("TotalTaxableAmount");
            dtPurchaseInvoice.Columns.Add("TotalCGSTAmount");
            dtPurchaseInvoice.Columns.Add("TotalIGSTAmount");
            dtPurchaseInvoice.Columns.Add("TotalSGSTAmount");
            /* end--DATATABLE FOR ISTRA CALCULATION*/

            FilePath = "FileGSTR2";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct InvoiceNo,Invoicedate FROM [SellerInvoiceData$] where ReceiverGSTN ='" + SellerGSTN + "' and IsEditedByReceiver='True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dtInvoiceNo);

                    if (dtInvoiceNo.Rows.Count > 0)
                    {
                        int rwCnt = dtInvoiceNo.Rows.Count;

                        for (int i = 0; i <= rwCnt - 1; i++)
                        {
                            strInvNo = (ds.Tables[0].Rows[i][0]).ToString();
                            strInvDate = (ds.Tables[0].Rows[i][1]).ToString();


                            cmd.CommandText = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and InvoiceNo ='" + strInvNo + "' and IsEditedByReceiver='True' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                            OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                            da2.Fill(dtInvoiceDtls);

                            if (dtInvoiceDtls.Rows.Count == 1)
                            {
                                strTaxableValue = Convert.ToDecimal(dtInvoiceDtls.Rows[0][0]);
                                strCGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][1]);
                                strIGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][2]);
                                strSGST = Convert.ToDecimal(dtInvoiceDtls.Rows[0][3]);
                                //    strAdditionalTax = Convert.ToDecimal(ds.Tables[0].Rows[i][0]);


                                FilePath = "FileGSTR1";
                                connectionString = GetConnectionString(FilePath);

                                cmd.CommandText = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and InvoiceNo ='" + strInvNo + "' and IsEditedByReceiver='True' and  FileGSTR1Date between  '" + FromDt + "' and '" + Todate + "' ";
                                OleDbDataAdapter da3 = new OleDbDataAdapter(cmd);
                                da3.Fill(dtInvoiceDtlsGSTR2);

                                if (dtInvoiceDtlsGSTR2.Rows.Count == 1)
                                {
                                    var row = dtPurchaseInvoice.NewRow();
                                    row["InvoiceNo"] = strInvNo;
                                    row["Invoicedate"] = strInvDate;
                                    row["TotalTaxableAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][0]) - strTaxableValue;
                                    row["TotalCGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][1]) - strCGST;
                                    row["TotalIGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][2]) - strIGST;
                                    row["TotalSGSTAmount"] = Convert.ToDecimal(dtInvoiceDtlsGSTR2.Rows[0][3]) - strSGST;

                                    dtPurchaseInvoice.Rows.Add(row);

                                    dtInvoiceDtlsGSTR2 = new DataTable();
                                    dtInvoiceDtls = new DataTable();
                                    strTaxableValue = 0;
                                    strCGST = 0;
                                    strIGST = 0;
                                    strSGST = 0;
                                    strInvNo = string.Empty;
                                    strInvDate = string.Empty;

                                }



                            }
                        }
                    }

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            #endregion

            #region REVERSE_CHARGE

            DataTable dtReverseCharge = new DataTable("ReverseCharge");

            FilePath = "ReverseCharge";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    // need to add date filter. will do it after saving the data
                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "'  and  Invoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtReverseCharge);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            FilePath = "FileGSTR2A";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    // need to add date filter. will do it after saving the data
                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where SellerGSTN ='" + SellerGSTN + "'  and IsEditedByReceiver='True'";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            #endregion

            #region INWARD_TOTAL_TAX_LIABILITY

            DataTable dtInwardTotalTaxLiability = new DataTable("InwardTotalTaxLiability");

            FilePath = "FileGSTR2";
            connectionString = GetConnectionString(FilePath);


            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();                    
                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and  FileGSTR2Date between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtInwardTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            FilePath = "Import";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and  Importdate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtInwardTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }

            FilePath = "ReverseCharge";
            connectionString = GetConnectionString(FilePath);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    query = "SELECT distinct TotalTaxableAmount,TotalCGSTAmount,TotalIGSTAmount,TotalSGSTAmount FROM [HSNData$] where ReceiverGSTN ='" + SellerGSTN + "' and  Invoicedate between  '" + FromDt + "' and '" + Todate + "' ";
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtInwardTotalTaxLiability);
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            #endregion

            #endregion


            /* OUTWARD Supplies */
            ds.Tables.Add(dtOutWardInter);
            ds.Tables.Add(dtOutWardIntra);
            ds.Tables.Add(dtExport);
            ds.Tables.Add(dtAmend);
            ds.Tables.Add(dtTotalTaxLiability);

            /* INWARD Supplies */

            ds.Tables.Add(dtInWardInter);
            ds.Tables.Add(dtInWardIntra);
            ds.Tables.Add(dtImport);
            ds.Tables.Add(dtPurchaseInvoice);
            ds.Tables.Add(dtReverseCharge);
            ds.Tables.Add(dtInwardTotalTaxLiability);

            return ds;
        }

        private List<string> GetConsigneeStateCodeFrmExl(DataTable dtConsigneeStateCode, string ConsigneeStateCode, List<string> ConsigneeState)
        {

            foreach (DataRow dr in dtConsigneeStateCode.Rows)
            {
                ConsigneeState.Add(dr["ConsigneeStateCode"].ToString());
            }


            return ConsigneeState;
        }


        #endregion

        #endregion

        #region populateInvoiceddl
        public DataSet populateInvoiceddl(string SellerGSTIN, string Fromdt, string Todt)
        {
            DataSet ds = new DataSet();

            ds = GetAllInvoice(SellerGSTIN, Fromdt, Todt);
            return ds;
        }

        private DataSet GetAllInvoice(string SellerGSTIN, string Fromdt, string Todt)
        {
           
            DataSet ds = new DataSet();
            string FilePath = "InvoiceData";

            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT InvoiceNo,Invoicedate FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "' and  Invoicedate between  '" + Fromdt + "' and '" + Todt + "' ", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dtInvoice);

                    OleDbCommand cmd1 = new OleDbCommand("SELECT distinct SellerGSTN,SellerName,SellerAddress FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "' and  Invoicedate between  '" + Fromdt + "' and '" + Todt + "' ", cn);
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd1);
                    da2.Fill(dtSellerDtls);

                    ds.Tables.Add(dtInvoice);
                    ds.Tables.Add(dtSellerDtls);
                    
                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return ds;
        }
        #endregion
        
        //For all Invoice of Seller
        #region populateInvoiceddl
        /// <summary>
        /// For all Invoice of Seller
        /// </summary>
        /// <param name="SellerGSTIN"></param>
        /// <returns></returns>
        public DataSet populateInvoiceddl(string SellerGSTIN)
        {
            DataSet ds = new DataSet();

            ds = GetAllInvoice(SellerGSTIN);
            return ds;
        }
        private DataSet GetAllInvoice(string SellerGSTIN)
        {

            DataSet ds = new DataSet();
            string FilePath = "InvoiceData";

            DataTable dtSellerDtls = new DataTable("SellerDtls");
            DataTable dtInvoice = new DataTable("InvoiceDtls");

            string connectionString = GetConnectionString(FilePath);



            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    //OleDbCommand cmd = new OleDbCommand("SELECT InvoiceNo,Invoicedate FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "'", cn);
                    //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    //da.Fill(dtInvoice);

                    OleDbCommand cmd1 = new OleDbCommand("SELECT distinct ReceiverName,ConsigneeName,InvoiceNo,Invoicedate FROM [SellerInvoiceData$] where SellerGSTN ='" + SellerGSTIN + "'", cn);
                    OleDbDataAdapter da2 = new OleDbDataAdapter(cmd1);
                    da2.Fill(dtSellerDtls);

                    //ds.Tables.Add(dtInvoice);
                    ds.Tables.Add(dtSellerDtls);

                }
                catch
                {

                }
                finally
                {
                    cn.Close();
                }
            }
            return ds;
        }
      
        #endregion


        /// <summary>
        ///  Private Method:Extracting data from GSTIN string as provided by the seller
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns>Object of SerialNoOfInvoice </returns>
        #region ValidateStrutureGSTIN
        private static bool ValidateStructureGSTIN(string gstinNumber)
        {
            bool verifyGSTIN = false;
            StringBuilder createSerialNo = new StringBuilder();
            // extract stateCode from GSTIN string
            for (int i = 0; i <= 2; i++)
            {
                if (i < 2)
                {
                    createSerialNo.Append(gstinNumber[i]);
                    
                }
                else
                    createSerialNo.Clear();
            }

            // extract PanID from GSTIN string
            for (int i = 0; i <= 11; i++)
            {
                if ((i >= 2 && i <= 12))
                {
                    createSerialNo.Append(gstinNumber[i]);
                   
                }
                else
                    createSerialNo.Clear();
            }
            // extract Entity Code
            //for (int i = 0; i <= 11; i++)
            for (int i = 0; i <= 12; i++)
            {
                //if (i == 12)
                if (i == 13)
                {
                    createSerialNo.Append(gstinNumber[i]);
                    
                }
                else
                    createSerialNo.Clear();
            }
            //
            for (int i = 0; i <= 14; i++)
            {
                if (i == 14)
                {
                    createSerialNo.Append(gstinNumber[i]);
                   

                }
                else
                    createSerialNo.Clear();
            }

            return verifyGSTIN;
        }
        #endregion

        /// <summary>
        /// Checks whether the HSN is present or not in the master
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <returns></returns>
        #region HSN present
        public bool IsHSNPresentInTheMaster(string hsnNumber)
        {
            DataSet ds = new DataSet();
            List<string> sellerDetails = new List<string>();
            string filePath = "CETSH";
            string connectionString = GetConnectionString(filePath);
            bool isHSNPresent = false;

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmd7 = new OleDbCommand("SELECT * FROM [ExportWorksheet$] WHERE CETSHNO ='" + hsnNumber + "'", cn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd7);
                    da.Fill(ds, "HSNRegister");

                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        throw new Exception("Purchase Register Updation with New HSN , HSN not found !!!");
                    }
                    else
                        isHSNPresent = true;

                }
                catch (NullReferenceException Nullex)
                {
                    NullReferenceException nullEx = new NullReferenceException("Purchase Register Updation with New HSN , HSN not found");
                    GSTExcelDBEx invoiceEx = new GSTExcelDBEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Purchase Register Updation with New HSN , HSN not found :-ExcelDB", Nullex);
                }


                catch (Exception ex)
                {
                    GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                    // TO DO- Pramod , make this code work
                    //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                    // check in event viewer -TO DO
                    //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                   //// logger.LogError("Purchase Register Updation with New HSN , HSN not found :-ExcelDB", ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return isHSNPresent;
        }
        #endregion

       
    }


}



