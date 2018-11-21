//using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Configuration;


//using System.ComponentModel.DescriptionAttribute;
namespace GST.Utility
{
    //for 3B
    public class ReportGenerate
    {
        public static void ExcelDownloadGstr_3B(string UserId, int Month)
        {
            try
            {
                //ReportViewer MyReportViewer = new ReportViewer();
                //MyReportViewer.ProcessingMode = ProcessingMode.Remote;
                //IReportServerCredentials ssrscredentials = new CustomSSRSCredentials(ConfigurationManager.AppSettings["SSRSUserName"], ConfigurationManager.AppSettings["SSRSPassword"], ConfigurationManager.AppSettings["SSRSDomain"]);
                //ServerReport serverReport = MyReportViewer.ServerReport;
                //MyReportViewer.ServerReport.ReportServerCredentials = ssrscredentials;
                //ReportParameter prm = new ReportParameter("SELLERUSERID", UserId);
                //ReportParameter prm2 = new ReportParameter("Month", Month.ToString());
                //serverReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ServerReport"]);
                //serverReport.ReportPath = "/Invoice_Report/rpt_GSTR3B_OFFLINE";
                //MyReportViewer.ServerReport.SetParameters(prm);
                //MyReportViewer.ServerReport.SetParameters(prm2);
                //CreateExcelFromServer(UserId, MyReportViewer);

            }
            catch (Exception ex)
            {

            }
        }
        public static void CreateExcelFromServer(string FileName, ReportViewer reportViewer)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = reportViewer.ServerReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = mimeType;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
            HttpContext.Current.Response.BinaryWrite(bytes); // create the file
            HttpContext.Current.Response.Flush(); // send it to the client to download
        }

        public class CustomSSRSCredentials : IReportServerCredentials
        {
            private string _SSRSUserName;
            private string _SSRSPassWord;
            private string _DomainName;

            public CustomSSRSCredentials(string UserName, string PassWord, string DomainName)
            {
                _SSRSUserName = UserName;
                _SSRSPassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_SSRSUserName, _SSRSPassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }


    //Enum
    public class UniqueNoGenerate
    {
        public static string RandomValue()
        {
            int maxSize = 14;
            int minSize = 5;
            char[] chars = new char[62];
            string a;

            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }

        public static string RandomValueNote()
        {
            int maxSize = 14;
            int minSize = 5;
            char[] chars = new char[62];
            string a;

            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return "NT"+result.ToString();
        }

    }




    public class EnumConstants
    {

        [Flags]
        public enum IsInter
        {
            [Description("InterState")]
            InterState,
            [Description("IntraState")]
            IntraState
        }


        //Registration page new user & old user Enum 
        [Flags]
        public enum Registration
        {
            [Description("Old User")]
            OldUser,
            [Description("New User")]
            NewUser,
        }


        //Return file 
        [Flags]
        public enum Return
        {
            [Description("Details of outward supplies of goods or services (GSTR-1)")]
            Gstr1,
            [Description("Auto Drafted Detais(For View Only) GSTR-2A")]
            Gstr2A,
            [Description("Monthly Return (GSTR-3B)")]
            Gstr3B,
            //[Description("")]
            
        }


        //Return file status

        [Flags]
        public enum ReturnFileStatus
        {
            [Description("Save")]
            Save,
            [Description("Submit")]
            Submit,
            [Description("FileGstr1")]
            FileGstr1
        }


        [Flags]
        public enum ITCVoucherType
        {
            [Description("Purchase")]
            Purchase,
            [Description("Tax Invoice/Invoice")]
            TaxInvoice,
            [Description("Journal")]
            Journal,
            [Description("Credit Note")]
            CreditNote,
            [Description("Debite Note")]
            DebiteNote
        }

        [Flags]
        public enum FinYear
        {
            January=01,
            February=02,
            March=03,
            April=04,
            May=05,
            June=06,
            July=07,
            August=08,
            September=09,
            October=10,
            November=11,
            December=12
        }

        [Flags]
        public enum ITCStatus
        {
            Active,
            Deactive
        }

        [Flags]
        public enum ITCMovement
        {
            Credit,
            Debit
        }

        [Flags]
        public enum NoteTypeStatus
        {
            Fresh,
            VerifiedIssued
        }

        [Flags]
        public enum NoteType
        {
            Credit,
            Debit
        }

        public static readonly decimal[] Rate = { 0.0m, 0.25m, 3.0m, 5.0m, 12.0m, 18.0m, 28.0m };
        
        [Flags]
        public enum Master
        {
            RoleNew,
            RoleAssign,
            GroupNew,
            UserNew,
            Users,
            BusinessType
        }
        [Flags]
        public enum RoleName
        {
            Admin,
            TaxConsultant,
            User
        }
        [Flags]
        public enum Status
        {
            Deactive,
            Active
        }
        [Flags]
        public enum Unit
        {
            CK,
            G,
            KG,
            L,
            M,
            M2,
            M3,
            MT,
            PA,
            Q,
            T,
            TU,
            U,
            //New As per gstn
            [Description("BAGS")]
            BAG,
            [Description("BALE")]
            BAL,
            [Description("BUNDLES")]
            BDL,
            [Description("BUCKLES")]
            BKL,
            [Description("BILLION OF UNITS ")]
            BOU,
            [Description("BOX")]
            BOX,
            [Description("BOTTLES")]
            BTL,
            [Description("BUNCHES")]
            BUN,
            [Description("CANS")]
            CAN,
            [Description("CUBIC METERS")]
            CBM,
            [Description("CUBIC CENTIMETERS")]
            CCM,
            [Description("CENTIMETERS")]
            CMS,
            [Description("CARTONS")]
            CTN,
            [Description("DOZENS")]
            DOZ,
            [Description("DRUMS")]
            DRM,
            [Description("GREAT GROSS")]
            GGK,
            [Description("GRAMMES")]
            GMS,
            [Description("GROSS")]
            GRS,
            [Description("GROSS YARDS")]
            GYD,
            [Description("KILOGRAMS")]
            KGS,
            [Description("KILOLITRE")]
            KLR,
            [Description("KILOMETRE")]
            KME,
            [Description("MILILITRE")]
            MLT,
            [Description("METERS")]
            MTR,
            [Description("METRIC TON")]
            MTS,
            [Description("NUMBERS")]
            NOS,
            [Description("PACKS")]
            PAC,
            [Description("PIECES")]
            PCS,
            [Description("PAIRS")]
            PRS,
            [Description("QUINTAL")]
            QTL,
            [Description("ROLLS")]
            ROL,
            [Description("SETS")]
            SET,
            [Description("SQUARE FEET")]
            SQF,
            [Description("SQUARE METERS")]
            SQM,
            [Description("SQUARE YARDS")]
            SQY,
            [Description("TABLETS")]
            TBS,
            [Description("TEN GROSS")]
            TGM,
            [Description("THOUSANDS")]
            THD,
            [Description("TONNES")]
            TON,
            [Description("TUBES")]
            TUB,
            [Description("US GALLONS")]
            UGS,
            [Description("UNITS")]
            UNT,
            [Description("YARDS")]
            YDS,
            [Description("OTHERS")]
            OTH
        }
        //amits GSTR1 Header
        [Flags]
        public enum GstrTileViewHeader
        {
            [Description("4A, 4B, 4C, 6B, 6C - B2B Invoices")]
            B2Binvoices,
            [Description("5A, 5B - B2C (Large) Invoices")]
            LargeInvoices,
            [Description("9B - Credit / Debit Notes (Registered)")]
            crdrRegister,
            [Description("9B - Credit / Debit Notes (UnRegistered)")]
            crdrUnRegister,
            [Description("6A- Exports Invoices")]
            ExportInvoices,
            [Description("9A Amended B2B Invoices")]
            AmendedB2Binvoices,
            [Description("9A Amended B2B (Large) Invoices")]
            AmendedB2BLargeinvoices,
            [Description("9A Amended Exports Invoices")]
            AmendedExportsinvoices,
            [Description("9C - Amended Credit/Debit Notes (Registered)")]
            AmendedcrdrRegisterinvoices,
            [Description("9C - Amended Credit/Debit Notes (UnRegistered)")]
            AmendedcrdrUnRegisterinvoices,
            [Description("7 - B2C (Others)")]
            B2cOther,
            [Description("8A, 8B, 8C, 8D - Nil Rated Supplies")]
            NilRatedSupplies,
            [Description("11A(1), 11A(2) - Tax Liability (Advances Received)")]
            TaxLiabilityAdvancesReceived,
            [Description("11B(1), 11B(2) - Adjustment of Advances")]
            AdjustmentAdvances,
            [Description("12 - HSN-wise summary of outward supplies")]
            HSN,
            [Description("13 - Documents Issued")]
            DocumentsIssued
        }
      


        [Flags]
        public enum OfflineType
        {
            [Description("Other Than ECommerce")]
            OE,
            [Description("ECommerce")]
            E
        }
        [Flags]
        public enum ErrorHandling
        {
            Pending,
            Resolve
        }
        [Flags]
        public enum BoolStatus
        {

            False,
            True
        }
        [Flags]
        public enum RegisteredWithUs
        {
            False,
            True
        }
        [Flags]
        public enum UserType
        {
            [Description("Regular Dealer (RD)")]
            RegularDealerRD,
            [Description("ISD")]
            InputServiceDistributorISD,
            [Description("Comp. Dealer (CD)")]
            CompoundingDealerCD,
            [Description("PSU Unit")]
            PSU,
            [Description("United Nations (UN)")]
            UN,
            [Description("E-Comm Operator")]
            EComOperator,
            [Description("Importer")]
            Importer,
            [Description("Exporter")]
            Exporter,
            [Description("Non-GST User")]
            NonGSTRegisteredUser,
            [Description("Reciever")]
            Reciever,
            [Description("Consignee")]
            Consignee
        }
        [Flags]
        public enum ItemType
        {
            HSN,
            SAC
        }
        [Flags]
        public enum SpecialCondition
        {
            [Description("None")]
            None,
            [Description("Is ITC Denied")]
            ISITCDenied,
            [Description("Is Reversecharge Applicable")]
            IsReverseChargeApplicable
        }
        [Flags]
        public enum InvoiceStatus
        {
            Fresh,
            Amended,
            Modified
        }

        [Flags]
        public enum TaxType
        {
            IGST,
            CGST,
            SGST,
            UTGST,
            CESS

        }

        [Flags]
        public enum InvoiceActionAuditTrail
        {
            NA,
            Accept,
            Reject,
            Delete,
            Pending,
            Modify,
            AutoMatch,
            Add
        }

        [Flags]
        public enum InvoiceAuditTrailSatus
        {
            [Description("Upload")]
            Upload,
            [Description("File GSTR1")]
            FileGSTR1,
            [Description("Import-2A")]
            Import2A,
            [Description("File GSTR2")]
            FileGSTR2,
            [Description("Import-1A")]
            Import1A,
            [Description("File GSTR1A")]
            FileGSTR1A,
            [Description("Import GSTR3")]
            ImportGSTR3,
            [Description("File GSTR3")]
            FileGSTR3,
            [Description("Data Freez")]
            DataFreez,
            [Description("Pending")]
            Pending,
            [Description("Save")]
            Save,
            [Description("Submit")]
            Submit
        }
        //tyo
        [Flags]
        public enum URType
        {
            B2CL,
            EXPWP,
            EXPWOP
        }

        public enum Period
        {
            Monthly,
            Quterly,
            Yearly
        }
        //Offline
        [Flags]
        public enum GSTPay
        {
            WPAY,
            WOPAY
        }

        [Flags]
        public enum InvoiceSpecialCondition
        {
            [Description("Regular")]
            Regular,
            [Description("Export")]
            Export,
            [Description("Advance")]
            Advance,
            [Description("Job-Work")]
            JobWork,
            [Description("SEZ Unit")]
            SEZUnit,
            [Description("SEZ Developer")]
            SEZDeveloper,
            [Description("Deemed Export")]
            DeemedExport,
            [Description("B2C Small")]
            B2CS,
            [Description("B2C Large")]
            B2CL,
            [Description("Reverse Charges")]
            ReverseCharges,
            [Description("E-Commerce")]
            ECommerce,
            [Description("Import")]
            Import,
            [Description("Supplier Missing Invoice")]
            SupplierMissingInvoice,
            [Description("Regular-RCM")]
            RegularRCM

        }
        //Offline
        public enum InvoiceType
        {
            [Description("B2B")]
            B2B,
            [Description("B2C")]
            B2C,
            [Description("Regular")]
            Regular,
            [Description("SEZ supplies with payment")]
            SEZsupplies_WithPayment,
            [Description("SEZ supplies without payment")]
            SEZsupplies_WithoutPayment,
            [Description("Deemed Exp")]
            Deemed_Exp


            //[Description("B2CL")]
            //B2CLarge
            //AmendedB2CLargeInvoice,
            //B2BCreditNotesInvoice,
            // B2BDebitNotesInvoice,
            // AmendedB2BCreditNotesInvoice,
            // AmendedB2BDebitNotesInvoice,
            // B2BExportInvoice,
            //AmendedB2BExportInvoice
            //B2BInvoice,
            //AmendedB2BInvoice,
            //B2CLargeInvoice,
            //AmendedB2CLargeInvoice,
            //B2BCreditNotesInvoice,
            //B2BDebitNotesInvoice,
            //AmendedB2BCreditNotesInvoice,
            //AmendedB2BDebitNotesInvoice,
            //B2BExportInvoice,
            //AmendedB2BExportInvoice
        }
        public enum OwnerShipType
        {
            None,
            Hired,
            Owned,
        }
        [Flags]
        public enum SaleStatus
        {
            Fresh,
            Modify,
            Damage
        }
        [Flags]
        public enum Message
        {
            UploadInvoice,
            FileGSTR1,
            ImportGSTR2A,
            FileGSTR2,
            ImportGSTR1A,
            FileGSTR1A,
            Accept,
            Reject,
            Pending
        }
        [Flags]
        public enum InvoiceDataStatus
        {
            Accept,
            Modify,
            Reject,
            Delete,
            Pending
        }
      
        [Flags]
        public enum Designation
        {
            [Description("Finance Executive")]
            FINANCEEXECUTIVE=0,
            [Description("Account Executive")]
            ACCOUNTEXECUTIVE=1,
            [Description("Finance Manager")]
            FINANCEMANAGER=2,
            [Description("Account Manager")]
            ACCOUNTMANAGER=3,
            [Description("CA/ Tax Consultant")]
            CA_TAXCONSULTANT=4,
            [Description("Lawyer")]
            LAWYER
        }

        //Return Type 
        [Flags]
        public enum OfflineFileType
        {
           GSTR1=0,
           GSTR2A=1,
           GSTR2=2,
           GSTR1A=3,
           GSTR3=4
        }

        //section
        [Flags]
        public enum OfflineSection
        {
            //NA,
            //Uploaded,
            //Datatransfer
            B2B=0,
            B2CL=1,
            B2CS=2,
            CDNR=3,
            CDNUR=4,
            EXP=5,
            AT=6,
            ATADJ=7,
            EXEMP=8,
            HSN=9,
            DOCS=10,
            MASTER=11
        }
        
          [Flags]
        public enum OfflineSheetName
        {
            //NA,
            //Uploaded,
            //Datatransfer
             B2B=0,
            B2CL=1,
            B2CS=2,
            CDNR=3,
            CDNUR=4,
            EXP=5,
            AT=6,
            ATADJ=7,
            EXEMP=8,
            HSN=9,
            DOCS=10,
            MASTER=11
        }
        [Flags]
        public enum OfflineUploadStatus
        {
            NA,
            Uploaded,
            Datatransfer
        }

        [Flags]
        public enum OfflineExcelSection
        {
            [Description("B2B Invoices - 4A, 4B, 4C, 6B, 6C")]
            B2B =0,
            [Description("B2C(Large) Invoices - 5A, 5B")]
            B2CL= 1,
            [Description("B2C(Small) Details - 7")]
            B2CS = 2,
            [Description("Credit/Debit Notes(Registered) - 9B")]
            CreditDebitReg = 3,
            [Description("Credit/Debit Notes(Unregistered) -9B")]
            CreditDebitNonReg = 4,
            [Description("Export Invoices - 6A")]
            ExportInvoice = 5,
            [Description("Tax Liability(Advance Received) - 11A(1), 11(2)")]
            TaxLiability = 6,
            [Description("Adjustment of Advances - 11B(1), 11B(2)")]
            AdvanceAdjustment = 7,
            [Description("HSN-wise Summary of Outward Supplies - 12")]
            HSNWiseSummary = 8
        }
        //Satyawan--Job Type
        public enum JobType
        {
            [Description("Offline")]
            Offline,
            [Description("Services")]
            Services
        }

    }

    public class HelperUtility
    {
        public static string IP
        {
            get
            {
                return new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
            }
        }
    }

    //public class MapItem
    //{
    //    UnitOfWork unitOfWork = new UnitOfWork();
    //    public List<GST_TRN_INVOICE_DATA> ParseInvoiceData(string stateCode,EnumConstants.InvoiceSpecialCondition sellerSpecialInvoice,List<GST_TRN_INVOICE_DATA> invoiceData)
    //    {
    //        var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == stateCode);
    //        bool isStateExampted = stateData.IsExempted.Value;
    //        bool isExport = (sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.Export || sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.SEZDeveloper || sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.SEZUnit || sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.DeemedExport);
    //        bool isJobwork = (sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.JobWork);
    //        bool isImport = (sellerSpecialInvoice == EnumConstants.InvoiceSpecialCondition.Import);

    //        var isUTState = stateData.UT.Value;
    //        var isExempted = stateData.IsExempted.Value;

    //        List<GST_TRN_INVOICE_DATA> items = new List<GST_TRN_INVOICE_DATA>();
    //        var invLineItem = from invo in invoiceData
    //                          select new GST_TRN_INVOICE_DATA
    //                          {
    //                              //InvoiceID = invo.InvoiceID,
    //                              LineID = invo.LineID,
    //                              // GST_MST_ITEM = invo.Item,
    //                              Item_ID = invo.Item.Item_ID,
    //                              Qty = invo.Qty,
    //                              Rate = invo.PerUnitRate,
    //                              TotalAmount = invo.TotalLineIDWise,
    //                              Discount = invo.Discount,
    //                              TaxableAmount = invo.TaxableValueLineIDWise,
    //                              TotalAmountWithTax = invo.TaxValue,
    //                              IGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? invo.Item.IGST : (isImport ? invo.Item.IGST : 0)))),
    //                              IGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : 0)))),
    //                              CGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : invo.Item.CGST) : 0)),
    //                              CGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CGST.Value)) : 0)),
    //                              SGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : invo.Item.SGST))),
    //                              SGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.SGST.Value)))),
    //                              UGSTRate = isJobwork ? 0 : (isExport ? 0 : (isUTState ? invo.Item.UGST.Value : 0)),
    //                              UGSTAmt = isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.UGST.Value) : 0)),
    //                              CessRate = isJobwork ? 0 : invo.Item.CESS,
    //                              CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CESS.Value)
    //                          };
    //        return invLineItem.ToList();
    //    }

    //}

    public static class DateTimeDayOfMonthExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static string GenerateFinancialPeriod()
        {
            string currentYear = DateTime.Now.Year.ToString();
            int repeatingYear = Convert.ToInt32(currentYear) + 1;
            string NextYear = repeatingYear.ToString();
            NextYear = NextYear.Substring(NextYear.Length - 2);

            return currentYear + "-" + NextYear;

        }

        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString().Substring(2, NextYear.ToString().Length - 2);
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static string GetMonthName(int month, bool abbreviate, IFormatProvider provider)
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(provider);
            if (abbreviate) return info.GetAbbreviatedMonthName(month);
            return info.GetMonthName(month);
        }

        public static string GetMonthName(int month, bool abbreviate)
        {
            return GetMonthName(month, abbreviate, null);
        }

        public static string GetMonthName(int month, IFormatProvider provider)
        {
            return GetMonthName(month, false, provider);
        }

        public static string GetMonthName(this int month)
        {
            return GetMonthName(month, false, null);
        }
    }

    public class InvoiceOperation
    {
        // UnitOfWork unitOfWork = new UnitOfWork();

        public static string InvoiceNo(AspNetUser userProfile, string Fin_ID, string currentSrlNo)
        {
            // var CurrentSrlNo = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == userProfile.Id).Count() + 1;
            var ddMMyyyyFormat = DateTime.Now.ToString("ddMMyyyy");
            string finYearfomrat = GetCurrentFinancialYear();

            return userProfile.StateCode + "_" + ExtractPanNo(userProfile.GSTNNo) + "_" + ddMMyyyyFormat + "_" + Fin_ID + "_" + currentSrlNo;
        }

        public static string ExtractPanNo(string gstinNo)
        {
            return gstinNo.Substring(2, gstinNo.Length - 5);
        }

        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString().Substring(2, NextYear.ToString().Length - 2);
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
        }

        // Checking wheter it is an inter /Intra state transactions
        public static bool GetConsumptionDestinationOfGoodsOrServices(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID)
        {
            bool isInter = false;
            // Case 1: When Reciever(BilledTo) and Consignee(ShippedTo) is same AND Seller is also in the same state
            //  state code id is same for seller , reciever and consignee then return false to isInter. This is then Inter state transaction--> No state wise summary
            if ((sellerStateCodeID.Equals(recieverStateCodeID, StringComparison.OrdinalIgnoreCase)) && (recieverStateCodeID.Equals(consigneeStateCodeID, StringComparison.OrdinalIgnoreCase)))
            {
                isInter = true;
            }
            else if (sellerStateCodeID == consigneeStateCodeID)
            {
                isInter = true;
            }
            else if (sellerStateCodeID != consigneeStateCodeID)
            {
                isInter = false;
            }
            else
            {
                isInter = true;
            }

            return isInter;
        }

    }

    public class Calculate
    {


        //public static decimal CalculateTax(bool isJobWork,bool isUTState,bool isInter,bool isExport,bool isJobWork,bool isJobWork)
        //{
        //    decimal tax = 0.00;
        //    if()


        //    return tax;
        //}

        // if intra , calculate IGST

        public static decimal TaxCalculate(GST_MST_ITEM item, bool isInterState, bool isExportedInvoice, bool isImport, bool isUTState, bool jobWork, bool isEcom, bool isUN, EnumConstants.TaxType taxType, decimal taxableValue, decimal taxRate)
        {
            decimal tax = 0;
            if (taxType == EnumConstants.TaxType.IGST)
            {
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState && isInterState)
                    {
                        tax = CalculateTax(taxableValue, taxRate);
                    }
                    else if (isUTState)
                    { tax = 0; }
                    else
                    {
                        if (isInterState)
                        {
                            tax = CalculateTax(taxableValue, taxRate);
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = CalculateTax(taxableValue, taxRate);
                            }
                            else
                            {
                                if (isImport)
                                {
                                    tax = CalculateTax(taxableValue, taxRate);
                                }
                                else
                                {
                                    tax = 0;
                                }
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.CGST)
            {
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState && isInterState)
                    {
                        tax = 0;
                    }
                    else if (isUTState)
                    { tax = CalculateTax(taxableValue, taxRate); }

                    //if (isUTState)
                    //{
                    //    tax = CalculateTax(taxableValue, taxRate);//0;
                    //}//(isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CGST.Value))))
                    else
                    {
                        if (isInterState)
                        {
                            tax = 0;
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = 0;
                            }
                            else
                            {
                                tax = CalculateTax(taxableValue, taxRate);
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.SGST)
            {
                //isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.SGST.Value)))),
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState && isInterState)
                    {
                        tax = 0;
                    }
                    //else if (isUTState)
                    //{ tax = CalculateTax(taxableValue, taxRate); }
                    else   if (isUTState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isInterState)
                        {
                            tax = 0;
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = 0;
                            }
                            else
                            {
                                tax = CalculateTax(taxableValue, taxRate);
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.UTGST)
            {// isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.UGST.Value) : 0)),
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isExportedInvoice)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isUTState && isInterState)
                        {
                            tax = 0;
                        }
                        else  if (isUTState)
                        {
                            tax = CalculateTax(taxableValue, taxRate);
                        }
                        else
                        {
                            tax = 0;
                        }
                    }
                }
            }
            else
            {
                tax = 0;
            }

            return tax;
        }

        public static decimal TaxRate(GST_MST_ITEM item, bool isInterState, bool isExportedInvoice, bool isImport, bool isUTState, bool jobWork, bool isEcom, bool isUN, EnumConstants.TaxType taxType, decimal taxableValue, decimal taxRate)
        {
            decimal tax = 0;
            if (taxType == EnumConstants.TaxType.IGST)
            {
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    //if (isUTState)
                    //{ tax = 0; }
                    if (isUTState && isInterState)
                    {
                        tax = taxRate;
                    }
                    else if (isUTState)
                    { tax = 0; }
                    else
                    {
                        if (isInterState)
                        {
                            tax = taxRate;
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = taxRate;
                            }
                            else
                            {
                                if (isImport)
                                {
                                    tax = taxRate;
                                }
                                else
                                {
                                    tax = 0;
                                }
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.CGST)
            {
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState && isInterState)
                    {
                        tax = 0;
                    }
                    else if (isUTState)
                    { tax = taxRate; }
                    //if (isUTState)
                    //{
                    //    tax = taxRate;//0;
                    //}//(isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CGST.Value))))
                    else
                    {
                        if (isInterState)
                        {
                            tax = 0;
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = 0;
                            }
                            else
                            {
                                tax = taxRate;
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.SGST)
            {
                //isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.SGST.Value)))),
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isUTState && isInterState)
                    {
                        tax = 0;
                    }
                    //else if (isUTState)
                    //{ tax = taxRate; }
                    else    if (isUTState)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isInterState)
                        {
                            tax = 0;
                        }
                        else
                        {
                            if (isExportedInvoice)
                            {
                                tax = 0;
                            }
                            else
                            {
                                tax = taxRate;
                            }
                        }
                    }
                }
            }
            else if (taxType == EnumConstants.TaxType.UTGST)
            {// isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.UGST.Value) : 0)),
                if (jobWork)
                {
                    tax = 0;
                }
                else if (item.IsNilRated.Value)
                {
                    tax = 0;
                }
                else
                {
                    if (isExportedInvoice)
                    {
                        tax = 0;
                    }
                    else
                    {
                        if (isUTState && isInterState)
                        {
                            tax = 0;
                        }
                        else if (isUTState)
                        {
                            tax = taxRate;
                        }
                        //if (isUTState)
                        //{
                        //    tax = taxRate;
                        //}
                        else
                        {
                            tax = 0;
                        }
                    }
                }
            }
            else
            {
                tax = 0;
            }
            return tax;
        }

        public static decimal CalculateTax(decimal taxableValue, decimal rateIGST)
        {
            decimal IGST = (taxableValue * (rateIGST / 100));
            IGST = Math.Round(IGST, 2);
            return IGST;
        }

        public static decimal CalculateIGSTLineIDWise(decimal taxableValue, decimal rateIGST)
        {
            decimal IGST = (taxableValue * (rateIGST / 100));
            IGST = Math.Round(IGST, 2);
            return IGST;
        }

        // if inter, calculate CGST
        public static decimal CalculateCGSTLineIDWise(decimal taxableValue, decimal rateCGST)
        {
            decimal CGST = (taxableValue * (rateCGST / 100));
            CGST = Math.Round(CGST, 2);
            return CGST;
        }

        // if inter, calculate SGST
        public static decimal CalculateSGSTLineIDWise(decimal taxableValue, decimal rateSGST)
        {
            decimal SGST = (taxableValue * (rateSGST / 100));
            SGST = Math.Round(SGST, 2);
            return SGST;
        }
        // if inter, calculate UGST
        public static decimal CalculateUGSTLineIDWise(decimal taxableValue, decimal rateUGST)
        {
            decimal UGST = (taxableValue * (rateUGST / 100));
            UGST = Math.Round(UGST, 2);
            return UGST;
        }
        // if inter, calculate CESS
        public static decimal CalculateCESSLineIDWise(decimal taxableValue, decimal rateCESS)
        {
            decimal CESS = (taxableValue * (rateCESS / 100));
            CESS = Math.Round(CESS, 2);
            return CESS;
        }
    }
}
