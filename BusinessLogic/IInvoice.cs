using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.B2B.GST.GSTIN;
using System.Data;
using DataAccessLayer;


namespace com.B2B.GST.GSTInvoices
{

    #region Interface Invoice
    /// <summary>
    /// Create Invoice and CreateLine enteries are the func of Seller class
    /// All the stubs represent the funcitonality fo the Interface Invoice
    /// Methods stubs r comman to all types of Invoices
    /// </summary>
    public interface IInvoice
    {
        #region MIS related
        // get all invoices b/w these periods , based on Invoice type
        void GetInvoiceDetails(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);

        // get all HSN sumarry in all invoices b/w these periods , based on Invoice type(derieved from factory)
        void GetHSNSumamry(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);

        // get all SAC sumarry in all invoices b/w these periods , based on Invoice type(derieved from factory)
        void GetSACSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);

        // get all  state summary in all invoice b/w these periods , based on Invoice type(derieved from factory)
        void GetStateSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);
        
        // Consumption Destination of GoodsOrServices
        bool GetConsumptionDestinationOfGoodsOrServices(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID);
        
        // Get Notifed HSN data
        List<Notified> GetHSNNotificationData(string hsnNumber);

        // Get Condition on Notifed HSN data
        List<Condition> GetHSNNotifiedConditionData(string hsnNumber);

        //Get Notifed SAC data
        List<Notified> GetSACNotificationData(string sacNumber);

        //Get Condition on Notifed SAC data
        List<Condition> GetSACNotifiedConditionData(string sacNumber);

        // Get tax benefitting state , based on this get StateName and stateCodeID
        string GetTaxBenefittingState(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID);

        // view saved invoice from seller in a particular date range
        DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate);

        // save invoice data , this is when lineCollection is going to get assinged to seller.Invoice.List<LineEntry>=lineCollections
        int SaveInvoiceData(Seller seller);

        // after th SaveInvoiceData, then we upload it to GSTIN using OTP and SaveGSTRI function
        int UploadInvoice(List<Seller> SellerDaTa, String InvoiceNo);
        #endregion

        #region Financial Impact relatede
        // Get total of line entry , based on qty and perUnitRate
        decimal GetTotalLineIDWise(decimal qty, decimal perUnitRate);

        // Get taxable total of line entry , based on qty * perUnitRate* rate of tax(based on inter/intra)
        decimal GetTaxableValueLineIDWise(decimal total, decimal discount);

        
       
        // IGST calculation if inter only , line ID wise
        decimal CalculateIGSTLineIDWise(decimal taxableValue, decimal rateIGST);
        
        // CGST calculation if inter only , line ID wise
        decimal CalculateCGSTLineIDWise(decimal taxableValue, decimal rateCGST);
        
        // SGST calculation if inter only , line ID wise
        decimal CalculateSGSTLineIDWise(decimal taxableValue, decimal rateSGST);

        decimal CalculateAmountWithTax(decimal amount, decimal tax);

        decimal CalculateTotalCGSTSGST(decimal CGST, decimal SGST);
        #endregion
    }
    #endregion

    public abstract class Invoice : IInvoice
    {
        #region datamembers

        // as entered by the user , 
        decimal insurance;
        // as entered by the user , 
        decimal freight;
        // as entered by the user ,
 
        decimal packingAndForwadingCharges;

        // looping thru all line with taxableVaueLineWise
        decimal totalInvoiceValue;
        
        // convert the total amount wiht tax
        string totalInvoiceWords;

        decimal taxSub2ReverseCharges;
        
        // disclaimer string
        string declaration;
        
        // as per the excel 
        string nameOfSignatory;

        // looping thru all line with totalOfAllLine
        decimal totalOfAllLine;

        // looping thru all line with taxableVaueLineWise
        decimal totalOfAllTaxableValueLine;

        // looping thru all line with totalOfAllLine
        decimal totalOfAllIGSTLine;

        // looping thru all line with totalOfAllCGSTLine
        decimal totalOfAllCGSTLine;

        // looping thru all line with totalOfAllCGSTLine
        decimal totalOfAllSGSTLine;

        //// to mark this invoi   , this is redudundant , will go , moved to B2BInvoice 
        bool isAdvancePaymentChecked;
        bool isExportChecked;

        string designationStatus;
        string invGSTINElectronicRefNumber;
        string invoiceType;

     

        // Invoice date is the date on which invoice is issued or created
        // dtGSTElectronicRefNumGeneratedDate is the date on which the invoice number is uploaded to GSTIN 
        // may be same or different
        DateTime dtGSTElectronicRefNumGeneratedDate;
        List<LineEntry> lineEntry;
        List<GST_TRN_INVOICE_DATA> lineEntryDBType;

        string invoiceSpecialCondition;

        
        

        #endregion

        #region publicHandlers
        public Nullable<int> VendorID { get; set; }
        public Nullable<long> TransShipment_ID { get; set; }

        public decimal Freight
        {
            get { return freight; }
            set { freight = value; }
        }

        public decimal Insurance
        {
            get { return insurance; }
            set { insurance = value; }
        }

        public decimal PackingAndForwadingCharges
        {
            get { return packingAndForwadingCharges; }
            set { packingAndForwadingCharges = value; }
        }

        public decimal TotalInvoiceValue
        {
            get { return totalInvoiceValue; }
            set { totalInvoiceValue = value; }
        }

        public string TotalInvoiceWords
        {
            get { return totalInvoiceWords; }
            set { totalInvoiceWords = value; }
        }

        public decimal TaxSub2ReverseCharges
        {
            get { return taxSub2ReverseCharges; }
            set { taxSub2ReverseCharges = value; }
        }

        public string Declaration
        {
            get { return declaration; }
            set { declaration = value; }
        }

        public string NameOfSignatory
        {
            get { return nameOfSignatory; }
            set { nameOfSignatory = value; }
        }

        public string DesignationStatus
        {
            get { return designationStatus; }
            set { designationStatus = value; }
        }

        public string InvGSTINElectronicRefNumber
        {
            get { return invGSTINElectronicRefNumber; }
            set { invGSTINElectronicRefNumber = value; }
        }
        public DateTime DtGSTElectronicRefNumGeneratedDate
        {
            get { return dtGSTElectronicRefNumGeneratedDate; }
            set { dtGSTElectronicRefNumGeneratedDate = value; }
        }
        public List<LineEntry> LineEntry
        {
            get { return lineEntry; }
            set { lineEntry = value; }
        }

        public List<GST_TRN_INVOICE_DATA> LineEntryDBType
        {
            get { return lineEntryDBType; }
            set { lineEntryDBType = value; }
        }
        public decimal TotalOfAllCGSTLine
        {
            get { return totalOfAllCGSTLine; }
            set { totalOfAllCGSTLine = value; }
        }
        public decimal TotalOfAllSGSTLine
        {
            get { return totalOfAllIGSTLine; }
            set { totalOfAllIGSTLine = value; }
        }
        public decimal TotalOfAllIGSTLine
        {
            get { return totalOfAllIGSTLine; }
            set { totalOfAllIGSTLine = value; }
        }

        public decimal TotalOfAllTaxableValueLine
        {
            get { return totalOfAllTaxableValueLine; }
            set { totalOfAllTaxableValueLine = value; }
        }

        public decimal TotalofAllLine
        {
            get { return totalOfAllLine; }
            set { totalOfAllLine = value; }
        }

        // , this is redudundant , will go  moved to B2BInvoice 
        public bool IsAdvancePaymentChecked
        {
            get { return isAdvancePaymentChecked; }
            set { isAdvancePaymentChecked = value; }
        }

        // , this is redudundant , will go , moved to B2BInvoice 
        public bool IsExportChecked
        {
            get { return isExportChecked; }
            set { isExportChecked = value; }
        }
        public string InvoiceType
        {
            get { return invoiceType; }
            set { invoiceType = value; }
        }

        public string InvoiceSpecialCondition
        {
            get { return invoiceSpecialCondition; }
            set { invoiceSpecialCondition = value; }
        }
        #endregion

        #region implementingInterfaceMethods
        


        abstract public void GetInvoiceDetails(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);
        abstract public void GetHSNSumamry(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);
        abstract public void GetSACSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);
        abstract public void GetStateSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt);
        


        
        abstract public decimal GetTotalLineIDWise(decimal qty, decimal perUnitRate);
        abstract public decimal GetTaxableValueLineIDWise(decimal total, decimal discount);
        abstract public bool GetConsumptionDestinationOfGoodsOrServices(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID);
        abstract public string GetTaxBenefittingState(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID);
        abstract public decimal CalculateIGSTLineIDWise(decimal taxableValue, decimal rateIGST);
        abstract public decimal CalculateCGSTLineIDWise(decimal taxableValue, decimal rateCGST);
        abstract public decimal CalculateSGSTLineIDWise(decimal taxableValue, decimal rateSGST);
        
        abstract public List<Notified> GetHSNNotificationData(string hsnNumber);
        abstract public List<Condition> GetHSNNotifiedConditionData(string hsnNumber);
        abstract public List<Notified> GetSACNotificationData(string sacNumber);
        abstract public List<Condition> GetSACNotifiedConditionData(string sacNumber);
        
        abstract public decimal CalculateAmountWithTax(decimal amount, decimal tax);
        abstract public decimal CalculateTotalCGSTSGST(decimal CGST, decimal SGST);

        abstract public DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate);
        abstract public int SaveInvoiceData(Seller seller);
        abstract public int UploadInvoice(List<Seller> SellerDaTa, String InvoiceNo);

        #endregion

    }

}
