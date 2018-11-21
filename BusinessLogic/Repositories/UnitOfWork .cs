using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private GST_DBEntities context = new GST_DBEntities();
        private Repository<GST_MST_STATE> stateRepository;
        private Repository<GST_MST_GROUP> groupRepositry;
        private Repository<GST_MST_SUBGROUP> subGroupRepositry;
        private Repository<GST_MST_CLASS> classRepositry;
        private Repository<GST_MST_SUBCLASS> subClassRepositry;
        private Repository<GST_MST_ITEM> itemRepositry;
        // private Repository<GST_MST_SAC> sacRepositry;
        private Repository<GST_MST_VENDOR> vendorRepositry;
        private Repository<GST_MST_FINYEAR> finYearRepositry;
        private Repository<GST_TRN_INVOICE> invoiceRepositry;
        private Repository<GST_TRN_INVOICE_DATA> invoiceDataRepositry;
        private Repository<GST_MST_SALE_REGISTER> saleRegisterDataRepositry;
        private Repository<GST_MST_PURCHASE_REGISTER> purchaseRegisterDataRepositry;
        private Repository<GST_MST_VENDOR_TRANS_SHIPMENT> transShipmentRepositry;
        private Repository<GST_TRN_VENDOR_SERVICE> vendorServiceRepositry;
        private Repository<GST_MST_USER_CURRENT_TURNOVER> currentTurnoverRepositry;
        private Repository<GST_MST_ITEM_NOTIFIED> notifiedItemRepositry;
        private Repository<GST_MST_ITEM_CONDITION> conditionItemRepositry;
        //private Repository<GST_MST_VENDOR_SERVICE> vendorService1Repositry;
        private Repository<GST_TRN_INVOICE_AUDIT_TRAIL> invoiceAuditTrailRepositry;
        private Repository<GST_MST_USER_SIGNATORY> userSignatoryRepository;
        private Repository<GST_MST_PURCHASE_DATA> purchaseDataRepositry;
        private Repository<GST_MST_MESSAGE> messageRepository;
        private Repository<GST_TRN_ITC> itcRepository;
        private Repository<GST_TRN_CRDR_NOTE> noteRepository;
        private Repository<GST_TRN_CRDR_NOTE_DATA> notedataRepository;
        private Repository<GST_TRN_INVOICE_MAP> invoiceMapRepository;
        private Repository<GST_TRN_CONSOLIDATED_INVOICE> consolidatedInvoiceRepository;
        private Repository<GST_TRN_OFFLINE> offlineRepository;
        private Repository<AspNetUser> aspnetRepository;
        private Repository<GST_MST_REPORT> reportRepository;
        private Repository<GST_MST_CUSTOM_INVOICE> invoicenoRepository;
        private Repository<GST_MST_BUSINESSTYPE> buisnessTypeRepository;
        private Repository<GST_MST_USER_BUSINESSTYPE> userBuisnessTypeRepository;
        private Repository<GST_MST_REPORT_PERMISSION> reportpermissionRepository;
        private Repository<GST_MST_PRESENT_USER> presentUserRepository;
        private Repository<GST_MST_OFFLINE_SECTION> offlinesectionRepository;
        private Repository<GST_TRN_OFFLINE_INVOICE> offlineinvoiceRepository;
        private Repository<GST_TRN_OFFLINE_INVOICE_SECTION_RULE> offlinesectionruleRepository;
        private Repository<GST_TRN_OFFLINE_INVOICE_DATAITEM> offlinedataitemRepository;
        private Repository<GST_TRN_OFFLINE_ISSUINGNOTE_REASON> offlineissuingnoteRepository;
        private Repository<GST_TRN_OFFLINE_INVOICE_RATE> offlinerateRepository;
        private Repository<GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL> offlineAudittrailRepository;
        private Repository<GST_TRN_ERROR_HANDLING> errorHandlingRepository;
        private Repository<GST_MST_MESSAGELOG> messagelogrepository;
        private Repository<GST_MST_EXCEPTIONLOG> exceptionlogrepository;
        private Repository<GST_TRN_RETURN_STATUS> returnstatusrepository;





        public Repository<GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL> OfflineAudittrailRepository
        {
            get
            {
                if (this.offlineAudittrailRepository == null)
                {
                    this.offlineAudittrailRepository = new Repository<GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL>(context);
                }
                return offlineAudittrailRepository;
            }
        }

        public Repository<GST_TRN_RETURN_STATUS> ReturnStatusRepository
        {
            get
            {
                if (this.returnstatusrepository == null)
                {
                    this.returnstatusrepository = new Repository<GST_TRN_RETURN_STATUS>(context);
                }
                return returnstatusrepository;
            }
        }

        public Repository<GST_TRN_OFFLINE_INVOICE_RATE> OfflinerateRepository
        {
            get
            {
                if (this.offlinerateRepository == null)
                {
                    this.offlinerateRepository = new Repository<GST_TRN_OFFLINE_INVOICE_RATE>(context);
                }
                return offlinerateRepository;
            }
        }
        public Repository<GST_TRN_OFFLINE_ISSUINGNOTE_REASON> OfflineissuingnoteRepository
        {
            get
            {
                if (this.offlineissuingnoteRepository == null)
                {
                    this.offlineissuingnoteRepository = new Repository<GST_TRN_OFFLINE_ISSUINGNOTE_REASON>(context);
                }
                return offlineissuingnoteRepository;
            }
        }
        public Repository<GST_TRN_OFFLINE_INVOICE_DATAITEM> OfflineinvoicedataitemRepository
        {
            get
            {
                if (this.offlinedataitemRepository == null)
                {
                    this.offlinedataitemRepository = new Repository<GST_TRN_OFFLINE_INVOICE_DATAITEM>(context);
                }
                return offlinedataitemRepository;
            }
        }




        public Repository<GST_MST_EXCEPTIONLOG> ExceptionLogRepository
        {
            get
            {
                if (this.exceptionlogrepository == null)
                {
                    this.exceptionlogrepository = new Repository<GST_MST_EXCEPTIONLOG>(context);
                }
                return exceptionlogrepository;
            }
        }

        public Repository<GST_MST_MESSAGELOG> MessageLogRepository
        {
            get
            {
                if (this.messagelogrepository == null)
                {
                    this.messagelogrepository = new Repository<GST_MST_MESSAGELOG>(context);
                }
                return messagelogrepository;
            }
        }
        public Repository<GST_MST_OFFLINE_SECTION> OfflinesectionRepository
        {
            get
            {
                if (this.offlinesectionRepository == null)
                {
                    this.offlinesectionRepository = new Repository<GST_MST_OFFLINE_SECTION>(context);
                }
                return offlinesectionRepository;
            }
        }
        public Repository<GST_TRN_OFFLINE_INVOICE> OfflineinvoiceRepository
        {
            get
            {
                if (this.offlineinvoiceRepository == null)
                {
                    this.offlineinvoiceRepository = new Repository<GST_TRN_OFFLINE_INVOICE>(context);
                }
                return offlineinvoiceRepository;
            }
        }
        public Repository<GST_TRN_OFFLINE_INVOICE_SECTION_RULE> OfflinesectionruleRepository
        {
            get
            {
                if (this.offlinesectionruleRepository == null)
                {
                    this.offlinesectionruleRepository = new Repository<GST_TRN_OFFLINE_INVOICE_SECTION_RULE>(context);
                }
                return offlinesectionruleRepository;
            }
        }

        public Repository<GST_MST_REPORT_PERMISSION> reportPermissionRepository
        {
            get
            {
                if (this.reportpermissionRepository == null)
                {
                    this.reportpermissionRepository = new Repository<GST_MST_REPORT_PERMISSION>(context);
                }
                return reportpermissionRepository;
            }
        }
        public Repository<GST_MST_USER_BUSINESSTYPE> UserBuisnessTypeRepository
        {
            get
            {
                if (this.userBuisnessTypeRepository == null)
                {
                    this.userBuisnessTypeRepository = new Repository<GST_MST_USER_BUSINESSTYPE>(context);
                }
                return userBuisnessTypeRepository;
            }
        }

        public Repository<GST_MST_BUSINESSTYPE> BuisnessTypeRepository
        {
            get
            {
                if (this.buisnessTypeRepository == null)
                {
                    this.buisnessTypeRepository = new Repository<GST_MST_BUSINESSTYPE>(context);
                }
                return buisnessTypeRepository;
            }
        }

        public Repository<GST_MST_CUSTOM_INVOICE> InvoiceNoRepository
        {
            get
            {
                if (this.invoicenoRepository == null)
                {
                    this.invoicenoRepository = new Repository<GST_MST_CUSTOM_INVOICE>(context);
                }
                return invoicenoRepository;
            }
        }


        public Repository<GST_MST_REPORT> ReportRepository
        {
            get
            {
                if (this.reportRepository == null)
                {
                    this.reportRepository = new Repository<GST_MST_REPORT>(context);
                }
                return reportRepository;
            }
        }
        public Repository<AspNetUser> AspnetRepository
        {
            get
            {
                if (this.aspnetRepository == null)
                {
                    this.aspnetRepository = new Repository<AspNetUser>(context);
                }
                return aspnetRepository;
            }
        }

        public Repository<GST_MST_PRESENT_USER> PresentUserRepository
        {
            get
            {
                if (this.presentUserRepository == null)
                {
                    this.presentUserRepository = new Repository<GST_MST_PRESENT_USER>(context);
                }
                return presentUserRepository;
            }
        }

        public Repository<GST_TRN_OFFLINE> OfflineRepository
        {
            get
            {
                if (this.offlineRepository == null)
                {
                    this.offlineRepository = new Repository<GST_TRN_OFFLINE>(context);
                }
                return offlineRepository;
            }
        }


        public Repository<GST_TRN_CONSOLIDATED_INVOICE> ConsolidatedInvoiceRepository
        {
            get
            {
                if (this.consolidatedInvoiceRepository == null)
                {
                    this.consolidatedInvoiceRepository = new Repository<GST_TRN_CONSOLIDATED_INVOICE>(context);
                }
                return consolidatedInvoiceRepository;
            }
        }

        public Repository<GST_TRN_INVOICE_MAP> InvoiceMapRepository
        {
            get
            {
                if (this.invoiceMapRepository == null)
                {
                    this.invoiceMapRepository = new Repository<GST_TRN_INVOICE_MAP>(context);
                }
                return invoiceMapRepository;
            }
        }

        public Repository<GST_TRN_ITC> ITCRepository
        {
            get
            {
                if (this.itcRepository == null)
                {
                    this.itcRepository = new Repository<GST_TRN_ITC>(context);
                }
                return itcRepository;
            }
        }

        public Repository<GST_TRN_CRDR_NOTE> CreditDebitNoteRepository
        {
            get
            {
                if (this.noteRepository == null)
                {
                    this.noteRepository = new Repository<GST_TRN_CRDR_NOTE>(context);
                }
                return noteRepository;
            }
        }

        public Repository<GST_TRN_CRDR_NOTE_DATA> CreditDebitNoteDataRepository
        {
            get
            {
                if (this.notedataRepository == null)
                {
                    this.notedataRepository = new Repository<GST_TRN_CRDR_NOTE_DATA>(context);
                }
                return notedataRepository;
            }
        }

        public Repository<GST_MST_MESSAGE> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new Repository<GST_MST_MESSAGE>(context);
                }
                return messageRepository;
            }
        }
        public Repository<GST_MST_USER_SIGNATORY> UserSignatoryRepository
        {
            get
            {
                if (this.userSignatoryRepository == null)
                {
                    this.userSignatoryRepository = new Repository<GST_MST_USER_SIGNATORY>(context);
                }
                return userSignatoryRepository;
            }
        }
        public Repository<GST_TRN_INVOICE_AUDIT_TRAIL> InvoiceAuditTrailRepositry
        {
            get
            {
                if (this.invoiceAuditTrailRepositry == null)
                {
                    this.invoiceAuditTrailRepositry = new Repository<GST_TRN_INVOICE_AUDIT_TRAIL>(context);
                }
                return invoiceAuditTrailRepositry;
            }
        }

        public Repository<GST_MST_ITEM_NOTIFIED> NotifiedItemRepositry
        {
            get
            {
                if (this.notifiedItemRepositry == null)
                {
                    this.notifiedItemRepositry = new Repository<GST_MST_ITEM_NOTIFIED>(context);
                }
                return notifiedItemRepositry;
            }
        }


        public Repository<GST_MST_ITEM_CONDITION> ConditionItemRepositry
        {
            get
            {
                if (this.conditionItemRepositry == null)
                {
                    this.conditionItemRepositry = new Repository<GST_MST_ITEM_CONDITION>(context);
                }
                return conditionItemRepositry;
            }
        }

        public Repository<GST_MST_VENDOR_TRANS_SHIPMENT> TransShipmentRepositry
        {
            get
            {
                if (this.transShipmentRepositry == null)
                {
                    this.transShipmentRepositry = new Repository<GST_MST_VENDOR_TRANS_SHIPMENT>(context);
                }
                return transShipmentRepositry;
            }
        }


        public Repository<GST_TRN_VENDOR_SERVICE> VendorServiceRepositry
        {
            get
            {
                if (this.vendorServiceRepositry == null)
                {
                    this.vendorServiceRepositry = new Repository<GST_TRN_VENDOR_SERVICE>(context);
                }
                return vendorServiceRepositry;
            }
        }

        public Repository<GST_MST_USER_CURRENT_TURNOVER> CurrentTurnoverRepositry
        {
            get
            {
                if (this.currentTurnoverRepositry == null)
                {
                    this.currentTurnoverRepositry = new Repository<GST_MST_USER_CURRENT_TURNOVER>(context);
                }
                return currentTurnoverRepositry;
            }
        }

        public Repository<GST_MST_PURCHASE_REGISTER> PurchaseRegisterDataRepositry
        {
            get
            {
                if (this.purchaseRegisterDataRepositry == null)
                {
                    this.purchaseRegisterDataRepositry = new Repository<GST_MST_PURCHASE_REGISTER>(context);
                }
                return purchaseRegisterDataRepositry;
            }
        }

        public Repository<GST_MST_SALE_REGISTER> SaleRegisterDataRepositry
        {
            get
            {
                if (this.saleRegisterDataRepositry == null)
                {
                    this.saleRegisterDataRepositry = new Repository<GST_MST_SALE_REGISTER>(context);
                }
                return saleRegisterDataRepositry;
            }
        }

        public Repository<GST_MST_FINYEAR> FinYearRepository
        {
            get
            {

                if (this.finYearRepositry == null)
                {
                    this.finYearRepositry = new Repository<GST_MST_FINYEAR>(context);
                }
                return finYearRepositry;
            }
        }
        public Repository<GST_TRN_INVOICE> InvoiceRepository
        {
            get
            {

                if (this.invoiceRepositry == null)
                {
                    this.invoiceRepositry = new Repository<GST_TRN_INVOICE>(context);
                }
                return invoiceRepositry;
            }
        }
        public Repository<GST_TRN_INVOICE_DATA> InvoiceDataRepository
        {
            get
            {

                if (this.invoiceDataRepositry == null)
                {
                    this.invoiceDataRepositry = new Repository<GST_TRN_INVOICE_DATA>(context);
                }
                return invoiceDataRepositry;
            }
        }
        public Repository<GST_MST_STATE> StateRepository
        {
            get
            {

                if (this.stateRepository == null)
                {
                    this.stateRepository = new Repository<GST_MST_STATE>(context);
                }
                return stateRepository;
            }
        }

        public Repository<GST_MST_GROUP> GroupRepository
        {
            get
            {

                if (this.groupRepositry == null)
                {
                    this.groupRepositry = new Repository<GST_MST_GROUP>(context);
                }
                return groupRepositry;
            }
        }
        public Repository<GST_MST_SUBGROUP> SubGroupRepository
        {
            get
            {

                if (this.subGroupRepositry == null)
                {
                    this.subGroupRepositry = new Repository<GST_MST_SUBGROUP>(context);
                }
                return subGroupRepositry;
            }
        }
        public Repository<GST_MST_CLASS> ClassRepository
        {
            get
            {

                if (this.classRepositry == null)
                {
                    this.classRepositry = new Repository<GST_MST_CLASS>(context);
                }
                return classRepositry;
            }
        }
        public Repository<GST_MST_SUBCLASS> SubClassRepository
        {
            get
            {

                if (this.subClassRepositry == null)
                {
                    this.subClassRepositry = new Repository<GST_MST_SUBCLASS>(context);
                }
                return subClassRepositry;
            }
        }
        public Repository<GST_MST_ITEM> ItemRepository
        {
            get
            {

                if (this.itemRepositry == null)
                {
                    this.itemRepositry = new Repository<GST_MST_ITEM>(context);
                }
                return itemRepositry;
            }
        }

        public Repository<GST_MST_VENDOR> VendorRepository
        {
            get
            {

                if (this.vendorRepositry == null)
                {
                    this.vendorRepositry = new Repository<GST_MST_VENDOR>(context);
                }
                return vendorRepositry;
            }
        }

        public Repository<GST_TRN_ERROR_HANDLING> ErrorHandlingRepository
        {
            get
            {

                if (this.errorHandlingRepository == null)
                {
                    this.errorHandlingRepository = new Repository<GST_TRN_ERROR_HANDLING>(context);
                }
                return errorHandlingRepository;
            }
        }
        public Repository<GST_MST_PURCHASE_DATA> PurchaseDataRepositry
        {
            get
            {

                if (this.purchaseDataRepositry == null)
                {
                    this.purchaseDataRepositry = new Repository<GST_MST_PURCHASE_DATA>(context);
                }
                return purchaseDataRepositry;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        public List<PROC_GSTR1_SUMMARY_Result> GetGSTR1_Summary(string GSTNNO, int MONTH)
        {
            var result = context.PROC_GSTR1_SUMMARY(GSTNNO, MONTH).ToList();
            return result;
        }
        public List<PROC_GSTR1_HSN_SUMMARY_Result> GetGSTR1hsnSummary(string GSTNNO, int MONTH)
        {
            var result = context.PROC_GSTR1_HSN_SUMMARY(GSTNNO, MONTH).ToList();
            return result;
        }

        //amittest changes return starting
        // 3B

        public List<PROC_FILERETURN_GSTR_3B_HEADER_Result> GetGSTR3B_FileReturn_Header(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR_3B_HEADER(SellerUserID, month).ToList();
            return result;
        }

        //GSTR1_4--1
        public List<PROC_FILERETURN_GSTR1_4A4B4C6B6C_B2B_Result> GetGSTR1_FileReturn_4(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_4A4B4C6B6C_B2B(SellerUserID, month).ToList();
            return result;
        }
        //GSTR1_5--2
        public List<PROC_FILERETURN_GSTR1_5A5B_B2C_Result> GetGSTR1_FileReturn_5(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_5A5B_B2C(SellerUserID, month).ToList();
            return result;
        }
        //GSTR1_6--3
        public List<PROC_FILERETURN_GSTR1_6A_EXPORT_Result> GetGSTR1_FileReturn_6(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_6A_EXPORT(SellerUserID, month).ToList();
            return result;
        }
        //4
        public List<PROC_FILERETURN_GSTR1_7C_B2C_OTHER_Result> GetGSTR1_FileReturn_7(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_7C_B2C_OTHER(SellerUserID, month).ToList();
            return result;
        }
        //5
        public List<PROC_FILERETURN_GSTR1_8_NILRATE_EXEMPTED_NONGST_Result> GetGSTR1_FileReturn_8(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_8_NILRATE_EXEMPTED_NONGST(SellerUserID, month).ToList();
            return result;
        }
        
        //6
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_B2C_LARGE_Result> GetGSTR1_FileReturn_9A_LARGE_B2C(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9A_AMENDED_B2C_LARGE(SellerUserID, month).ToList();
            return result;
        }
        //7
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_EXPORT_Result> GetGSTR1_FileReturn_9A_AMD_EXP(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9A_AMENDED_EXPORT(SellerUserID, month).ToList();
            return result;
        }
        //8
        public List<PROC_FILERETURN_GSTR1_9B_CRDR_REGISTER_Result> GetGSTR1_FileReturn_9B_CRDR_REG(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9B_CRDR_REGISTER(SellerUserID, month).ToList();
            return result;
        }
        //9
        public List<PROC_FILERETURN_GSTR1_9B_CRDR_UNREGISTER_Result> GetGSTR1_FileReturn_9B_CRDR_UNREG(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9B_CRDR_UNREGISTER(SellerUserID, month).ToList();
            return result;
        }
        //10
        public List<PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_REGISTER_Result> GetGSTR1_FileReturn_9C_AME_CRDR_REG(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_REGISTER(SellerUserID, month).ToList();
            return result;
        }
        //11
        public List<PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_UNREGISTER_Result> GetGSTR1_FileReturn_9C_AMd_CRDR_UNREG(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9C_AMENDED_CRDR_UNREGISTER(SellerUserID, month).ToList();
            return result;
        }
        //12
        public List<PROC_FILERETURN_GSTR1_10_AMENDED_B2C_OTHER_Result> GetGSTR1_FileReturn_10_AMD_B2C(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_10_AMENDED_B2C_OTHER(SellerUserID, month).ToList();
            return result;
        }
        //13
        public List<PROC_FILERETURN_GSTR1_11A_AME_TAXLIABILITY_ADV_Result> GetGSTR1_FileReturn_11A_AMD(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_11A_AME_TAXLIABILITY_ADV(SellerUserID, month).ToList();
            return result;
        }
        //14
        public List<PROC_FILERETURN_GSTR1_11A111A2_TAXLIABILITY_ADV_Result> GetGSTR1_FileReturn_11A1_11A2(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_11A111A2_TAXLIABILITY_ADV(SellerUserID, month).ToList();
            return result;
        }
        //15
        public List<PROC_FILERETURN_GSTR1_11B_AMENDED_ADJ_ADV_Result> GetGSTR1_FileReturn_11B(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_11B_AMENDED_ADJ_ADV(SellerUserID, month).ToList();
            return result;
        }
        //16
        public List<PROC_FILERETURN_GSTR1_11B1_11B2_ADJUSTMENT_ADV_Result> GetGSTR1_FileReturn_11B1_11B2(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_11B1_11B2_ADJUSTMENT_ADV(SellerUserID, month).ToList();
            return result;
        }
        //17
        public List<PROC_FILERETURN_GSTR1_9A_AMENDED_B2B_INVOICES_Result> GetGSTR1_FileReturn_9A_AMD_B2B(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR1_9A_AMENDED_B2B_INVOICES(SellerUserID, month).ToList();
            return result;
        }

      
        //OLD
        public List<PROC_FILERETURN_GSTR_1_HEADER_Result1> GetGSTR_1_HeaderDetails(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR_1_HEADER(SellerUserID, month).ToList();

            return result;
        }
        //TEST
        public List<PROC_FILE_GSTR_1_Test4A_details_Result> GetGSTR_1_Test4A_Details(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_Test4A_details(SellerUserID, month).ToList();
            return result;
        }
        //Ending

        //FileProc
        public List<PROC_FILE_GSTR_1_4A_Result> GetGSTR_1_4A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_4A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_4B_Result> GetGSTR_1_4B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_4B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_4C_Result> GetGSTR_1_4C(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_4C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_5A_Result> GetGSTR_1_5A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_5A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_5A_2_Result> GetGSTR_1_5A1(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_5A_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR1_6A_Result> GetGSTR_1_6A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR1_6A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR1_6B_Result> GetGSTR_1_6B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR1_6B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR1_6C_Result> GetGSTR_1_6C(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR1_6C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_11A1_Result> GetGSTR_1_11A1(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_11A1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_11A2_Result> GetGSTR_1_11A2(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_11A2(SellerUserID, month).ToList();
            return result;
        }

        //start Unit of Work
        public int GENERATE_MISMATCH_INVOICE(string UserID)
        {
            var result = context.PROC_PURCHASE_REGISTER_MISMATCH_INVOICE(UserID);
            return result;
        }

        public List<PROC_FILE_GSTR_1_7A_1_Result> GetGSTR_1_7A1(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_7A_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_7A_2_Result> GetGSTR_1_7A2(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_7A_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_7B_1_Result> GetGSTR_1_7B1(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_7B_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_1_7B_2_Result> GetGSTR_1_7B2(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_7B_2(SellerUserID, month).ToList();
            return result;
        }

        //public List<PROC_FILE_GSTR_1_10A_Result> GetGSTR_1_10A(string SellerUserID, int month)
        //{
        //    var result = context.PROC_FILE_GSTR_1_10A(SellerUserID, month).ToList();
        //    return result;
        //}

        public List<PROC_FILE_GSTR_1_10AA_Result> GetGSTR_1_10AA(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_10AA(SellerUserID, month).ToList();
            return result;
        }

        //public List<PROC_FILE_GSTR_1_10B_Result> GetGSTR_1_10B(string SellerUserID, int month)
        //{
        //    var result = context.PROC_FILE_GSTR_1_10B(SellerUserID, month).ToList();
        //    return result;
        //}

        public List<PROC_FILE_GSTR_1_10BB_Result> GetGSTR_1_10BB(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_10BB(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_11B1_Result> GetGSTR_1_11B1(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_11B1(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_11B2_Result> GetGSTR_1_11B2(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_11B2(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_12_Result> GetGSTR_1_12(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_12(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_8A_Result> GetGSTR_1_8A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_8A(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_8B_Result> GetGSTR_1_8B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_8B(SellerUserID, month).ToList();
            return result;
        }
        /*Ankita 3B*/
        public List<PROC_FILERETURN_GSTR3B_3_1_Result> GetGSTR_3B_3_1(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR3B_3_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_3_2_Result> GetGSTR_3B_3_2(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR3B_3_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_5_Result> GetGSTR_3B_3_5(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR3B_5(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_5_1_Result> GetGSTR_3B_5_1(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR3B_5_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILERETURN_GSTR3B_ITC_Result> GetGSTR_3B_ITC(string SellerUserID, int month)
        {
            var result = context.PROC_FILERETURN_GSTR3B_ITC(SellerUserID, month).ToList();
            return result;
        }
        //public List<PROC_RETURN_GSTR3B_3_5_Result> GetGSTR_3B_3_5(string SellerUserID, int month)
        //{
        //    var result = context.PROC_RETURN_GSTR3B_3_5(SellerUserID, month).ToList();
        //    return result;
        //}

        public List<PROC_FILE_GSTR_1_8C_Result> GetGSTR_1_8C(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_8C(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_8D_Result> GetGSTR_1_8D(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_8D(SellerUserID, month).ToList();
            return result;
        }

        //public List<PROC_FILE_GSTR_1_9A_Result> GetGSTR_1_9A(string SellerUserID, int month)
        //{
        //    var result = context.PROC_FILE_GSTR_1_9A(SellerUserID, month).ToList();
        //    return result;
        //}

        public List<PROC_FILE_GSTR_1_9B_Result> GetGSTR_1_9B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_9B(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1_9C_Result> GetGSTR_1_9C(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1_9C(SellerUserID, month).ToList();
            return result;
        }
        //end
        //Import-2
        //public List<PROC_FILE_GSTR2_4B> GetGSTR2_4B(string RECIVERUSERID)
        //{
        //    var result = context.PROC_FILE_GSTR2_4B(RECIVERUSERID).ToList();
        //    return result;
        //}

        public List<PROC_IMPORT_GSTR_2_8A_Result> GetGSTR_2_8A(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_8A(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR2_6A_Result> GetGSTR2_6A(string RECIVERUSERID, int month)
        {
            var result = context.PROC_FILE_GSTR2_6A(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR2_6B_Result> GetGSTR2_6B(string RECIVERUSERID, int month)
        {
            var result = context.PROC_FILE_GSTR2_6B(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_8B_Result> GetGSTR_2_8B(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_8B(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR_2_9A_Result> GetGSTR_2_9A(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_9A(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_9B_Result> GetGSTR_2_9B(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_9B(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR_2_10A_1_Result> GetGSTR_2_10A_1(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_10A_1(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_10A_2_Result> GetGSTR_2_10A_2(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_10A_2(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_10B_1_Result> GetGSTR_2_10B_1(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_10B_1(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_10B_2_Result> GetGSTR_2_10B_2(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_10B_2(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR_2_11A_Result> GetGSTR_2_11A(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_11A(RECIVERUSERID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2_11B_Result> GetGSTR_2_11B(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_11B(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR_2_12_Result> GetGSTR_2_12(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_12(RECIVERUSERID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR_2_13_Result> GetGSTR_2_13(string RECIVERUSERID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2_13(RECIVERUSERID, month).ToList();
            return result;
        }
        //End

        //start 2A
        public List<PROC_FILE_GSTR2A_3_Result> GetGSTR_2A_3(string ReceiverUserID, int month)
        {
            var result = context.PROC_FILE_GSTR2A_3(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR2A_4_Result> GetGSTR_2A_4(string ReceiverUserID, int month)
        {
            var result = context.PROC_FILE_GSTR2A_4(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2A_5_Result> GetGSTR_2A_5(string ReceiverUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2A_5(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2A_6_CREDIT_Result> GetGSTR_2A_6C(string ReceiverUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2A_6_CREDIT(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2A_6_INVOICE_Result> GetGSTR_2A_6I(string ReceiverUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2A_6_INVOICE(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2A_7A_Result> GetGSTR_2A_7A(string ReceiverUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2A_7A(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR_2A_7B_Result> GetGSTR_2A_7B(string ReceiverUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR_2A_7B(ReceiverUserID, month).ToList();
            return result;
        }


        public List<PROC_FILE_GSTR2_3_Result> GetGSTR2_3(string ReceiverUserID, int month)
        {
            var result = context.PROC_FILE_GSTR2_3(ReceiverUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR2_4A_Result> GetGSTR2_4A(string ReceiverUserID, int month)
        {
            var result = context.PROC_FILE_GSTR2_4A(ReceiverUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR2_4B_Result> GetGSTR2_4B(string ReceiverUserID, int month)
        {
            var result = context.PROC_FILE_GSTR2_4B(ReceiverUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR1A_3A_Result> GetGSTR1A_3A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR1A_3A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR1A_3B_Result> GetGSTR1A_3B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR1A_3B(SellerUserID, month).ToList();
            return result;
        }

        //new
        public List<PROC_FILE_GSTR_1A_4A_Result> GetGSTR1A_4A(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1A_4A(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1A_4B_Result> GetGSTR1A_4B(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1A_4B(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_1A_5_Result> GetGSTR1A_5(string SellerUserID, int month)
        {
            var result = context.PROC_FILE_GSTR_1A_5(SellerUserID, month).ToList();
            return result;
        }

        public List<PROC_IMPORT_GSTR3_4_1_A_Result> GetGSTR3_4_1_A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_1_A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_1_B_Result> GetGSTR3_4_1_B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_1_B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_1_C_Result> GetGSTR3_4_1_C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_1_C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_1_D_Result> GetGSTR3_4_1_D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_1_D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_2_A_Result> GetGSTR3_4_2_A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_2_A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_2_B_Result> GetGSTR3_4_2_B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_2_B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_4_2_C_Result> GetGSTR3_4_2_C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_4_2_C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_5A_1_Result> GetGSTR3_5A_1(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_5A_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_5A_2_Result> GetGSTR3_5A_2(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_5A_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_5B_1_Result> GetGSTR3_5B_1(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_5B_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_5B_2_Result> GetGSTR3_5B_2(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_5B_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_6_1_Result> GetGSTR3_6_1(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_6_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_6_2_Result> GetGSTR3_6_2(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_6_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_7_Result> GetGSTR3_7(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_7(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_8A_Result> GetGSTR3_8A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_8A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_8B_Result> GetGSTR3_8B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_8B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_8C_Result> GetGSTR3_8C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_8C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_8D_Result> GetGSTR3_8D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_8D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_9A_Result> GetGSTR3_9A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_9A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_9B_Result> GetGSTR3_9B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_9B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_10A_Result> GetGSTR3_10A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_10A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_10B_Result> GetGSTR3_10B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_10B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_10C_Result> GetGSTR3_10C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_10C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_10D_Result> GetGSTR3_10D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_10D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_11_Result> GetGSTR3_11(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_11(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_12A_Result> GetGSTR3_12A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_12A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_12B_Result> GetGSTR3_12B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_12B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_12C_Result> GetGSTR3_12C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_12C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_12D_Result> GetGSTR3_12D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_12D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_13_1_Result> GetGSTR3_13_1(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_13_1(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_13_2_Result> GetGSTR3_13_2(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_13_2(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_14_A_Result> GetGSTR3_14A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_14_A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_14_B_Result> GetGSTR3_14B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_14_B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_14_C_Result> GetGSTR3_14C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_14_C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_14_D_Result> GetGSTR3_14D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_14_D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_15_A_Result> GetGSTR3_15A(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_15_A(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_15_B_Result> GetGSTR3_15B(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_15_B(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_15_C_Result> GetGSTR3_15C(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_15_C(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_IMPORT_GSTR3_15_D_Result> GetGSTR3_15D(string SellerUserID, int month)
        {
            var result = context.PROC_IMPORT_GSTR3_15_D(SellerUserID, month).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_4A_Result> GetGSTR4_4A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_4A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_4B_Result> GetGSTR4_4B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_4B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_4C_Result> GetGSTR4_4C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_4C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_4D_Result> GetGSTR4_4D(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_4D(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_5A_Result> GetGSTR4_5A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_5A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_5B_Result> GetGSTR4_5B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_5B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_5C_Result> GetGSTR4_5C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_5C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_6_Result> GetGSTR4_6(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_6(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_7_Result> GetGSTR4_7(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_7(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_4_9_Result> GetGSTR4_9(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4_9(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_4A_3_Result> GetGSTR4A_3(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4A_3(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_4A_3B_Result> GetGSTR4A_3B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4A_3B(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_4A_4_Result> GetGSTR4A_4(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4A_4(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_4A_5_Result> GetGSTR4A_5(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_4A_5(SellerUserID).ToList();
            return result;
        }

        // amits GSTR_7
        public List<PROC_FILE_GSTR_7_3_Result> GetGSTR_7_3(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_3(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_4_Result> GetGSTR_7_4(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_4(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_5A_Result> GetGSTR_7_5A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_5A(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_5B_Result> GetGSTR_7_5B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_5B(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_5C_Result> GetGSTR_7_5C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_5C(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_6_Result> GetGSTR_7_6(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_6(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_7A_Result> GetGSTR_7_7A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_7A(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_7B_Result> GetGSTR_7_7B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_7B(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_7C_Result> GetGSTR_7_7C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_7C(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_8A_Result> GetGSTR_7_8A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_8A(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_8B_Result> GetGSTR_7_8B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_8B(SellerUserID).ToList();
            return result;
        }

        public List<PROC_FILE_GSTR_7_8C_Result> GetGSTR_7_8C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_7_8C(SellerUserID).ToList();
            return result;
        }

        //amits GSTR_8
        public List<PROC_FILE_GSTR_8_3_Result> GetGSTR_8_3(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_3(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_3A_Result> GetGSTR_8_3A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_3A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_4A_Result> GetGSTR_8_4A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_4A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_4B_Result> GetGSTR_8_4B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_4B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_5_Result> GetGSTR_8_5(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_5(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_6A_Result> GetGSTR_8_6A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_6A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_6B_Result> GetGSTR_8_6B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_6B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_6C_Result> GetGSTR_8_6C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_6C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_7A_Result> GetGSTR_8_7A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_7A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_7B_Result> GetGSTR_8_7B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_7B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_7C_Result> GetGSTR_8_7C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_7C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_8A_Result> GetGSTR_8_8A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_8A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_8B_Result> GetGSTR_8_8B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_8B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_8C_Result> GetGSTR_8_8C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_8C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_9A_Result> GetGSTR_8_9A(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_9A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_9B_Result> GetGSTR_8_9B(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_9B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_8_9C_Result> GetGSTR_8_9C(string SellerUserID)
        {
            var result = context.PROC_FILE_GSTR_8_9C(SellerUserID).ToList();
            return result;
        }
        //amitS END
        //viveksinha
        public List<PROC_FILE_GSTR_6_3_Result> GetGSTR_6_3(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_3(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_4_Result> GetGSTR_6_4(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_4(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_4B_Result> GetGSTR_6_4_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_4B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_4C_Result> GetGSTR_6_4_C(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_4C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_5A_Result> GetGSTR_6_5_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_5A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_5B_Result> GetGSTR_6_5_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_5B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR6_6A_Result> GetGSTR_6_6_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR6_6A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR6_6B_Result> GetGSTR_6_6_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR6_6B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR6_6C_Result> GetGSTR_6_6_C(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR6_6C(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_7A_Result> GetGSTR_6_7_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_7A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_7B_Result> GetGSTR_6_7_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_7B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_8A_Result> GetGSTR_6_8_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_8A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_8B_Result> GetGSTR_6_8_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_8B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_9A_Result> GetGSTR_6_9_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_9A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_9B_Result> GetGSTR_6_9_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_9B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_10_Result> GetGSTR_6_10(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_10(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_11A_Result> GetGSTR_6_11_A(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_11A(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6_11B_Result> GetGSTR_6_11_B(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6_11B(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6A_3_Result> GetGSTR_6_A_3(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6A_3(SellerUserID).ToList();
            return result;
        }
        public List<PROC_FILE_GSTR_6A_4_Result> GetGSTR_6_A_4(string SellerUserID)
        {

            var result = context.PROC_FILE_GSTR_6A_4(SellerUserID).ToList();
            return result;
        }


        //gstr3b test amit
        public List<PROC_FILE_GSTR_3B_3_1_Result> GetGSTR3_3_1(String SellerUserID, int Month)
        {
            var result = context.PROC_FILE_GSTR_3B_3_1(SellerUserID, Month).ToList();
            return result;
        }




        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
