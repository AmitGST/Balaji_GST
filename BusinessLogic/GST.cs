using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using com.B2B.GST.GSTInvoices;
using System.Data.OleDb;
using System.Data;
using com.B2B.GST.ExcelFunctionality;
using businessAccessLayer;
using com.B2B.GST.ExceptionHandling;
using System.Globalization;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;


namespace com.B2B.GST.GSTIN
{
    /// <summary>
    /// Seller stand for outward supply
    /// When authentication is success , seller object get populated either thru JSON/Local search
    /// </summary>

    #region ClassSeller
    public class Seller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        #region DataMembers

        string sellerUserID;

        
        // seller assigned GSTIN
        string GSTin;

        // username , in the case of TC or organization is required
        string userName;

        // seller name as with GSTIN network
        string nameAsOnGst;

        // seller name with GSTIN network
        string address;

        // string (DD-MM-YYYY)
        string dateOfInvoice;

        //additional info is string , that we keep for cases where v need to pass string info across pages
        string addintionalInformation;

        // as in from 01- 37
        string sellerStateCode;

        // as in from AP- Andhra pradesh or AR for Arunachal Pradesh
        string sellerStateCodeID;

        //This for ecom operater order date
        DateTime orderDate;
        
        // as in  name of the state, from where the seller is registered 
        string sellerStateName;

        string sellerInvoice;

        // Compound class that gets the invoice number in a specific pattern
        SerialNoOfInvoice serialNoInvoice;

        // Compund class that of Reciever, 
        Reciever reciever;

        // Compund class that of consignee
        Consignee consignee;

        // this is a abstract class that implements the Interface Invoice. On this Factory pattern is called to issue 
        // appropirate invoice on runtime
        Invoice invoice;

        // keep stock of goods in the seller purchase register/ledger
        PurchaseLedger purchaseLedger;

        // keep stock of goods in the seller sale register/leger
        SaleLedger saleLedger;
           
        //
        List<Seller> SellerData;

        // this class is going to b used in scenario where a entity is coming from non gst era to gst era
        TransitionalProvisonal transitionalProvision;

        // 
        TransShipment transShipment;



        // this is for provisoinal transitional factor
        decimal sellerGrossTurnOver;

        // this is autopopulated field , fixed for one year 
        string sellerFinancialPeriod;

        // on successfull submission fo savegstr1 methods, in resposne payload if electronic reference no is generated
        // then this value will be true in our DB
        bool isElectronicReferenceNoGenerated;

        // on successfull submission fo savegstr1 methods, in resposne payload we get electronicReferenceNoGenerated
        // transaction id(trans_id) Alphanumeric 15 characters, something like LAPN24235325555 , this GSTIN network parlance
        string electronicReferenceNoGenerated;

        // on electronicReferenceNoGenerated
        // transaction id(trans_id) Alphanumeric 15 characters , we will store the the Datetime in DB
        string electronicReferenceNoGeneratedDate; // now this variable taking as string ,need to change later as datetime.

        #region ForGSTR-1A import :: These field will be used by the seller
        bool isEditedByReceiver;
        bool isAddedByReceiver;
        bool isAcceptedByReceiver;
        bool isDeletedByReceiver;
        bool isRejectedByReceiver;
        #endregion

        decimal totalTaxableAmount;
        decimal totalAmount;
        decimal totalDiscount;
        decimal totalQty;
        decimal totalRate;
        decimal totalIGSTAmount;
        decimal totalCGSTAmount;
        decimal totalSGSTAmount;
        decimal totalAmountWithTax;
        decimal grandTotalAmount;
        string grandTotalAmountInWord;
        string createdBy;

        #endregion

        public string SellerUserID
        {
            get { return sellerUserID; }
            set { sellerUserID = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public decimal TotalCessAmount { get; set; }
        #region Accessors

        public string GSTIN
        {
            get { return GSTin; }
            set { GSTin = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string NameAsOnGST
        {
            get { return nameAsOnGst; }
            set { nameAsOnGst = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string DateOfInvoice
        {
            get { return dateOfInvoice; }
            set { dateOfInvoice = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public string SellerInvoice
        {
            get { return sellerInvoice; }
            set { sellerInvoice = value; }
        }

        public SerialNoOfInvoice SerialNoInvoice
        {
            get { return serialNoInvoice; }
            set { serialNoInvoice = value; }
        }

        public Reciever Reciever
        {
            get { return reciever; }
            set { reciever = value; }
        }

        public Consignee Consignee
        {
            get { return consignee; }
            set { consignee = value; }
        }

        public string SellerStateCode
        {
            get { return sellerStateCode; }
            set { sellerStateCode = value; }
        }

        public string SellerStateCodeID
        {
            get { return sellerStateCodeID; }
            set { sellerStateCodeID = value; }
        }

        public string SellerStateName
        {
            get { return sellerStateName; }
            set { sellerStateName = value; }
        }

        public Invoice Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }

        public PurchaseLedger PurchaseLedger
        {
            get { return purchaseLedger; }
            set { purchaseLedger = value; }
        }

        public SaleLedger SaleLedger
        {
            get { return saleLedger; }
            set { saleLedger = value; }
        }

        public List<Seller> SellerDaTa
        {
            get { return SellerData; }
            set { SellerData = value; }
        }

        public List<Int64> AdvanceInvoiceIds { get; set; }

        public List<Int64> ChallanInvoiceIds { get; set; }

        public decimal SellerGrossTurnOver
        {
            get { return sellerGrossTurnOver; }
            set { sellerGrossTurnOver = value; }
        }

        public string SellerFinancialPeriod
        {
            get { return sellerFinancialPeriod; }
            set { sellerFinancialPeriod = value; }
        }

        public bool IsElectronicReferenceNoGenerated
        {
            get { return isElectronicReferenceNoGenerated; }
            set { isElectronicReferenceNoGenerated = value; }
        }

        public bool IsEditedByReceiver
        {
            get { return isEditedByReceiver; }
            set { isEditedByReceiver = value; }
        }

        public bool IsAddedByReceiver
        {
            get { return isAddedByReceiver; }
            set { isAddedByReceiver = value; }
        }

        public bool IsAcceptedByReceiver
        {
            get { return isAcceptedByReceiver; }
            set { isAcceptedByReceiver = value; }
        }

        public bool IsDeletedByReceiver
        {
            get { return isDeletedByReceiver; }
            set { isDeletedByReceiver = value; }
        }

        public bool IsRejectedByReceiver
        {
            get { return isRejectedByReceiver; }
            set { isRejectedByReceiver = value; }
        }

        public decimal TotalTaxableAmount
        {
            get { return totalTaxableAmount; }
            set { totalTaxableAmount = value; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public decimal TotalDiscount
        {
            get { return totalDiscount; }
            set { totalDiscount = value; }
        }

        public decimal TotalQty
        {
            get { return totalQty; }
            set { totalQty = value; }
        }

        public decimal TotalRate
        {
            get { return totalRate; }
            set { totalRate = value; }
        }

        public decimal TotalIGSTAmount
        {
            get { return totalIGSTAmount; }
            set { totalIGSTAmount = value; }
        }

        public decimal TotalCGSTAmount
        {
            get { return totalCGSTAmount; }
            set { totalCGSTAmount = value; }
        }

        public decimal TotalSGSTAmount
        {
            get { return totalSGSTAmount; }
            set { totalSGSTAmount = value; }
        }

        public decimal TotalAmountWithTax
        {
            get { return totalAmountWithTax; }
            set { totalAmountWithTax = value; }
        }

        public decimal GrandTotalAmount
        {
            get { return grandTotalAmount; }
            set { grandTotalAmount = value; }
        }

        public string GrandTotalAmountInWord
        {
            get { return grandTotalAmountInWord; }
            set { grandTotalAmountInWord = value; }
        }

        public string ElectronicReferenceNoGenerated
        {
            get { return electronicReferenceNoGenerated; }
            set { electronicReferenceNoGenerated = value; }
        }

        public string ElectronicReferenceNoGeneratedDate
        {
            get { return electronicReferenceNoGeneratedDate; }
            set { electronicReferenceNoGeneratedDate = value; }
        }

        public string AddintionalInformation
        {
            get { return addintionalInformation; }
            set { addintionalInformation = value; }
        }

        public TransitionalProvisonal TransitionalProvision
        {
            get { return transitionalProvision; }
            set { transitionalProvision = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        ///  Exposed Method:Extracting data from GSTIN string as provided by the seller
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns>Object of SerialNoOfInvoice </returns>

        public SerialNoOfInvoice GenerateSerailNoOfInvoice(string gstinNumber)
        {
            SerialNoOfInvoice newSerialNo = ExtractInfoFrmGSTINStr(gstinNumber);

            return newSerialNo;
        }

        /// <summary>
        ///  Private Method:Extracting data from GSTIN string as provided by the seller
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns>Object of SerialNoOfInvoice </returns>
        private static SerialNoOfInvoice ExtractInfoFrmGSTINStr(string gstinNumber)
        {
            SerialNoOfInvoice newSerialNo = new SerialNoOfInvoice();
            StringBuilder createSerialNo = new StringBuilder();
            // extract stateCode from GSTIN string
            for (int i = 0; i <= 2; i++)
            {
                if (i < 2)
                {
                    createSerialNo.Append(gstinNumber[i]);
                    newSerialNo.StateCodeID = Convert.ToString(createSerialNo);
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
                    newSerialNo.PanID = Convert.ToString(createSerialNo);
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
                    newSerialNo.EntityCode = Convert.ToString(createSerialNo);
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
                    newSerialNo.ChkDigit = Convert.ToString(createSerialNo);

                }
                else
                    createSerialNo.Clear();
            }

            newSerialNo.Date = DateTime.UtcNow.Day + "/" + DateTime.UtcNow.Month + "/" + DateTime.UtcNow.Year;
            newSerialNo.Month = DateTime.UtcNow.Month;
            newSerialNo.FinancialYear = DateTime.UtcNow.Year + "-" + DateTime.UtcNow.AddYears(1);

            return newSerialNo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public Seller GenerateSellerInvoiceData(string gstinNumber,string userID)
        {
            // TODO
            // Hit your Local DB first for the GSTIN string and its associated data.
            // If response is not positive
            // Wire it to Rest API: GET of GST.
            // If response is positive, then populate the seller attributes accordingly.
            // If response is negative , tell the user to registration Page.

            // As of now , we are reading by Excel file
           Seller seller = new Seller();
            //ExcelDB excelDB = new ExcelDB();
            //List<string> sellerDetails = excelDB.GetSellerDetails(gstinNumber);
            //seller.GSTIN = gstinNumber;
            
            //if (sellerDetails != null && sellerDetails.Count == 9)
            //{

            //    #region sequence in which the sellerDetails r populated in the excelDB
            //    //sellerDetails.Add(dr["GSTIN"].ToString());
            //    //sellerDetails.Add(dr["UserName"].ToString());
            //    //sellerDetails.Add(dr["RegisteredName"].ToString());
            //    //sellerDetails.Add(dr["RegisteredAddress"].ToString());
            //    //sellerDetails.Add(dr["RegisteredStateCode"].ToString());
            //    //sellerDetails.Add(dr["RegisteredStateName"].ToString());
            //    //sellerDetails.Add(dr["RegisteredStateCodeID"].ToString());
            //    //sellerDetails.Add(dr["GrossTurnOver"].ToString());
            //    //sellerDetails.Add(dr["FinancialPeriod"].ToString());
            //    #endregion

            //    seller.GSTIN = sellerDetails[0].ToString();
            //    seller.UserName = sellerDetails[1].ToString();
            //    seller.NameAsOnGST = sellerDetails[2].ToString();
            //    seller.Address = sellerDetails[3].ToString();
            //    seller.SellerStateCode = sellerDetails[4].ToString();
            //    seller.SellerStateName = sellerDetails[5].ToString();
            //    seller.SellerStateCodeID = sellerDetails[6].ToString();
            //    seller.SellerGrossTurnOver = Convert.ToDecimal(sellerDetails[7]);
            //    seller.SellerFinancialPeriod = sellerDetails[8].ToString();
                            
            //}
            
            seller.DateOfInvoice = DateTime.Now.ToString("dd-MM-yyyy");

            seller.SerialNoInvoice = seller.GenerateSerailNoOfInvoice(gstinNumber);

            seller.serialNoInvoice.CurrentSrlNo = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == userID).Count() + 1;// excelDB.ReadDefaultSeed(gstinNumber);
            //seller.serialNoInvoice.CurrentSrlNo = excelDB.ReadCurrentSrlNo(gstinNumber);

            //
            var ddMMyyyyFormat = DateTime.Now.ToString("ddMMyyyy");

            //
            string finYearfomrat=seller.serialNoInvoice.GenerateFinancialPeriod();          
            seller.serialNoInvoice.FinancialYear = unitOfWork.FinYearRepository.Filter(f => f.Finyear_Format == finYearfomrat).FirstOrDefault().Fin_ID.ToString();

            // on the save or save and upload of invoice will go to 
            // db and update the InvoiceSeed in the GSTIN excel file
            // 
            seller.sellerInvoice = seller.SerialNoInvoice.StateCodeID + "_" + seller.SerialNoInvoice.PanID + "_" + ddMMyyyyFormat + "_" + seller.serialNoInvoice.FinancialYear + "_" + seller.serialNoInvoice.CurrentSrlNo; 

            return seller;

        }


        /// <summary>
        /// On page load , when seller is authenticated the details related to his purchase register is captured 
        /// and populated in the Purchase Register obj of seller.This is usefull when GetGoodsOrServiceInfo txt change event is called
        /// on hsn , input parameter is seller obj , return back the seller obj populated with purchase register of the seller 
        /// </summary>
        /// <param name="seller"></param>
        /// <returns> Seller populated wiht purchase register</returns>
        public Seller GetSellerPurchaseRegisterData(Seller seller)
        {

            //Seller seller = new Seller();
            ExcelDB excelDB = new ExcelDB();
            DataSet ds = new DataSet();
            ds = excelDB.GetSellerPurchaseRegisterData(seller.GSTIN);
            StockRegisterLineEntry stockListing;
            // v only have one table , that is how the sql is formed to get all stock entries as per the GSTIN
            if (ds.Tables[0].Rows.Count > 0)
            {
                // in here it is sure that the seller has entry in the Purchase register 
                // so we will initialize the purchase register
                seller.PurchaseLedger = new PurchaseLedger();

                // then we will initialize the StockLineEntry 
                seller.PurchaseLedger.StockLineEntry = new List<StockRegisterLineEntry>();


                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    // for each row , new stock listing is created
                    stockListing = new StockRegisterLineEntry();

                    if (!string.IsNullOrEmpty(row["GSTIN"].ToString()))
                        stockListing.GstinNumber = row["GSTIN"].ToString();

                    if (!string.IsNullOrEmpty(row["Username"].ToString()))
                        stockListing.UserName = row["Username"].ToString();

                    if (!string.IsNullOrEmpty(row["LineID"].ToString()))
                        stockListing.LineID = Int32.Parse(row["LineID"].ToString());

                    if (!string.IsNullOrEmpty(row["HSN"].ToString()))
                        stockListing.Hsn = row["HSN"].ToString();

                    if (!string.IsNullOrEmpty(row["PerUnitRate"].ToString()))
                        stockListing.PerUnitRate = decimal.Parse(row["PerUnitRate"].ToString());

                    if (!string.IsNullOrEmpty(row["StockInwardDate"].ToString()))
                        stockListing.StockInwardDate = DateTime.Parse(row["StockInwardDate"].ToString());

                    if (!string.IsNullOrEmpty(row["StockOrderDate"].ToString()))
                        stockListing.StockOrderDate = DateTime.Parse(row["StockOrderDate"].ToString());

                    if (!string.IsNullOrEmpty(row["OrderPO"].ToString()))
                        stockListing.OrderPO = row["OrderPO"].ToString();

                    if (!string.IsNullOrEmpty(row["Qty"].ToString()))
                        stockListing.Qty = decimal.Parse(row["Qty"].ToString());

                    // stock listing is added to stock line entry 
                    seller.PurchaseLedger.StockLineEntry.Add(stockListing);

                }

            }

            return seller;

        }

        /// <summary>
        /// purpose of GetSellerStockInventoryStatus , checks whether 
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public bool GetSellerStockInventoryStatus(string hsnNumber, Seller seller)
        {
            bool stockStatus = false;
            foreach (StockRegisterLineEntry stock in seller.PurchaseLedger.StockLineEntry)
            {
                if (stock.Hsn == hsnNumber && stock.Qty > 0)
                    stockStatus = true;
            }
            return stockStatus;
        }

        /// <summary>
        /// getting particular information of hsn present in the purchase register as per the gstin number of the seller
        /// </summary>
        /// <param name="hsnNumber"></param>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public SaleLedger GetSellerStockInventoryStatus(string hsnNumber, string gstinNumber)
        {
            DataSet StockInPurchaseRegister = new DataSet();
            ExcelDB excelDB = new ExcelDB();
            SaleLedger saleLedger = new SaleLedger();
            saleLedger = excelDB.GetSellerSaleRegisterData(hsnNumber, gstinNumber);
            return saleLedger;
        }

        public decimal? GetSellerStockInventory(string itemcode, string sellerUserID)
        {
            //var itemID = unitOfWork.ItemRepository.Find(f => f.ItemCode == itemcode.Trim()).Item_ID;
           // var purchageItem = unitOfWork.PurchaseRegisterDataRepositry.Filter(f => f.Item_ID == itemID && f.UserID == sellerUserID).Sum(s => s.Qty);
            return null;//purchageItem;
        }

        public List<GST_MST_PURCHASE_REGISTER> GetSellerStockInventoryStatus(int itemID, string sellerUserID)
        {
          //  var purchageItems =//TODO : unitOfWork.PurchaseRegisterDataRepositry.Filter(f => f.Item_ID == itemID && f.UserID == sellerUserID).ToList();
            return null;
        }

        /// <summary>
        /// After HSN is found in stock register and quantity is greater than 0, then the seller will create invoice 
        /// in the line entry we we add quantity at that point 
        /// </summary>
        /// <returns></returns>
        //public bool GetSellerStockInventoryAsPerQuantityInHand(string hsnNumber, int lineID, Seller seller)
        //{
        //    bool stockStatusAsPerQuantityInHand = false;
        //    //Seller seller = new Seller();
        //    ExcelDB excelDB = new ExcelDB();
        //    DataSet ds = new DataSet();

        //    StockRegisterLineEntry stockListing;

        //    // then we will initialize the StockLineEntry 
        //    seller.PurchaseLedger.StockLineEntry = new List<StockRegisterLineEntry>();

        //    ds = excelDB.UpdateSellerPurchaseRegisterData(hsnNumber, lineID, seller);

        //    // v only have one table , that is how the sql is formed to get all stock entries as per the GSTIN
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        // in here it is sure that the seller has entry in the Purchase register 
        //        // so we will initialize the purchase register
        //        seller.PurchaseLedger = new PurchaseLedger();




        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {

        //            // for each row , new stock listing is created
        //            stockListing = new StockRegisterLineEntry();

        //            if (!string.IsNullOrEmpty(row["LineID"].ToString()))
        //                stockListing.LineID = Int32.Parse(row["LineID"].ToString());

        //            if (!string.IsNullOrEmpty(row["HSN"].ToString()))
        //                stockListing.Hsn = row["HSN"].ToString();

        //            if (!string.IsNullOrEmpty(row["PerUnitRate"].ToString()))
        //                stockListing.PerUnitRate = decimal.Parse(row["PerUnitRate"].ToString());

        //            if (!string.IsNullOrEmpty(row["StockInwardDate"].ToString()))
        //                stockListing.StockInwardDate = DateTime.Parse(row["StockInwardDate"].ToString());

        //            if (!string.IsNullOrEmpty(row["StockOrderDate"].ToString()))
        //                stockListing.StockOrderDate = DateTime.Parse(row["StockOrderDate"].ToString());

        //            if (!string.IsNullOrEmpty(row["OrderPO"].ToString()))
        //                stockListing.OrderPO = row["OrderPO"].ToString();

        //            if (!string.IsNullOrEmpty(row["Qty"].ToString()))
        //                stockListing.Qty = decimal.Parse(row["Qty"].ToString());

        //            // stock listing is added to stock line entry 
        //            seller.PurchaseLedger.StockLineEntry.Add(stockListing);

        //        }

        //    }

        //    // return seller;
        //    return true;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockLineEntry"></param>
        /// <returns></returns>
        public bool UpdateSellerSaleRegisterData(StockRegisterLineEntry stockLineEntry)
        {
            ExcelDB excelDB = new ExcelDB();
            bool stockLineEntryUpdatedStatus = false;
            stockLineEntryUpdatedStatus = excelDB.UpdateSellerSaleRegisterData(stockLineEntry);
            return stockLineEntryUpdatedStatus;
        }
        /// <summary>
        /// this shows as to how many count of HSN , a particular seller deals
        /// </summary>
        /// <param name="gstinNumber"></param>
        /// <returns></returns>
        public int GetTotalCountOfGoodsSellerDeals(string gstinNumber)
        {
            DataSet TotalCountOfGoodsSellerDeals = new DataSet();
            ExcelDB excelDB = new ExcelDB();

            int maxLineID = excelDB.GetTotalCountOfGoodsSellerDeals(gstinNumber);
            return maxLineID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="recieverGSTIN"></param>
        /// <returns></returns>
        public Seller GenerateRecieverInvoiceData(Seller seller, string recieverGSTIN)
        {
            // TODO
            // Hit your Local DB first fo9r the GSTIN string and its associated data.
            // If response is not positive
            // Wire it to Rest API: GET of GST.
            // If response is positive, then populate the seller attributes accordingly.
            // If response is negative , tell the user to registration Page.

            // As of now , we are reading by Excel file

            ExcelDB excelDB = new ExcelDB();
            seller.Reciever = new Reciever();
            List<string> recieverDetails = excelDB.GetRecieverDetails(recieverGSTIN);

            // always chk the lenght of on the go collection entity , to remove index out of range exception

            #region sequence in which the sellerDetails r populated in the excelDB
            //sellerDetails.Add(dr["GSTIN"].ToString());
            //sellerDetails.Add(dr["UserName"].ToString());
            //sellerDetails.Add(dr["RegisteredName"].ToString());
            //sellerDetails.Add(dr["RegisteredAddress"].ToString());
            //sellerDetails.Add(dr["RegisteredStateCode"].ToString());
            //sellerDetails.Add(dr["RegisteredStateName"].ToString());
            //sellerDetails.Add(dr["RegisteredStateCodeID"].ToString());
            //sellerDetails.Add(dr["GrossTurnOver"].ToString());
            //sellerDetails.Add(dr["FinancialPeriod"].ToString());
            #endregion
            if (recieverDetails.Count ==9)
            {
                // TO DO : check whether this id belongs to any exempted entity like Govt/PSU/UN

                seller.Reciever.GSTIN = recieverDetails.ElementAt(0);
                seller.Reciever.UserName = recieverDetails.ElementAt(1);
                seller.Reciever.NameAsOnGST = recieverDetails.ElementAt(2);
                seller.Reciever.Address = recieverDetails.ElementAt(3);
                seller.Reciever.StateCode = recieverDetails.ElementAt(4);
                seller.Reciever.StateName = recieverDetails.ElementAt(5);
                seller.Reciever.StateCodeID = recieverDetails.ElementAt(6);
                seller.Reciever.RecieverGrossTurnOver =Decimal.Parse(recieverDetails.ElementAt(7).ToString());
                seller.Reciever.FinancialPeriod = recieverDetails.ElementAt(8);
            }
            
            
            return seller;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="consigneeGSTIN"></param>
        /// <returns></returns>
        public Seller GenerateConsigneeInvoiceData(Seller seller, string consigneeGSTIN)
        {
            // TODO
            // Hit your Local DB first for the GSTIN string and its associated data.
            // If response is not positive
            // Wire it to Rest API: GET of GST.
            // If response is positive, then populate the seller attributes accordingly.
            // If response is negative , tell the user to registration Page.

            // As of now , we are reading by Excel file

            ExcelDB excelDB = new ExcelDB();
            seller.Consignee = new Consignee();
            List<string> ConsigneeDetails = excelDB.GetConsigneeDetails(consigneeGSTIN);

            // always chk the lenght of on the go collection entity , to remove index out of range exception
            if (ConsigneeDetails.Count == 9)
            {

                #region sequence in which the consignee r populated in the excelDB
                //sellerDetails.Add(dr["GSTIN"].ToString());
                //sellerDetails.Add(dr["UserName"].ToString());
                //sellerDetails.Add(dr["RegisteredName"].ToString());
                //sellerDetails.Add(dr["RegisteredAddress"].ToString());
                //sellerDetails.Add(dr["RegisteredStateCode"].ToString());
                //sellerDetails.Add(dr["RegisteredStateName"].ToString());
                //sellerDetails.Add(dr["RegisteredStateCodeID"].ToString());
                //sellerDetails.Add(dr["GrossTurnOver"].ToString());
                //sellerDetails.Add(dr["FinancialPeriod"].ToString());
                #endregion

                // TO DO : check whether this id belongs to any exempted entity like Govt/PSU/UN
                seller.Consignee.GSTIN = ConsigneeDetails.ElementAt(0);
                seller.Consignee.UserName = ConsigneeDetails.ElementAt(1);
                seller.Consignee.NameAsOnGST = ConsigneeDetails.ElementAt(2);
                seller.Consignee.Address = ConsigneeDetails.ElementAt(3);
                seller.Consignee.StateCode = ConsigneeDetails.ElementAt(4);
                seller.Consignee.StateName = ConsigneeDetails.ElementAt(5);
                seller.Consignee.StateCodeID = ConsigneeDetails.ElementAt(6);
                seller.Consignee.ConsigneeGrossTurnOver= Decimal.Parse(ConsigneeDetails.ElementAt(7).ToString());
                seller.Consignee.ConsigneeFinancialPeriod = ConsigneeDetails.ElementAt(8);
            }
            
            
            return seller;

        }

        public Seller CreateInvoice(Seller seller, string invoiceType)
        {
            #region INVOICE_TYPE
            // to invoke the correct invoice type creation process
            switch (invoiceType)
            {
                // creates the instance of b2b invoice from the factory 
                case "B2B":
                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                    seller.Invoice = b2bInvoice;
                    seller.Invoice.InvoiceType = EnumConstants.InvoiceType.B2B.ToString();//TODO
                    break;
                case "AmendedB2BInvoice":
                    Invoice AmendedB2BInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BInvoice);
                    seller.Invoice = AmendedB2BInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "B2C":
                    Invoice B2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2CLargeInvoice);
                    seller.Invoice = B2CLargeInvoice;
                    seller.Invoice.InvoiceType =EnumConstants.InvoiceType.B2C.ToString();// invoiceType;
                    break;
                case "AmendedB2CLargeInvoice":
                    Invoice AmendedB2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2CLargeInvoice);
                    seller.Invoice = AmendedB2CLargeInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "B2BCreditNotesInvoice":
                    Invoice B2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BCreditNotesInvoice);
                    seller.Invoice = B2BCreditNotesInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "B2BDebitNotesInvoice":
                    Invoice B2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BDebitNotesInvoice);
                    seller.Invoice = B2BDebitNotesInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "AmendedB2BCreditNotesInvoice":
                    Invoice AmendedB2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BCreditNotesInvoice);
                    seller.Invoice = AmendedB2BCreditNotesInvoice;
                    seller.Invoice.InvoiceType = "invoiceType";
                    break;
                case "AmendedB2BDebitNotesInvoice":
                    Invoice AmendedB2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BDebitNotesInvoice);
                    seller.Invoice = AmendedB2BDebitNotesInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "B2BExportInvoice":
                    Invoice B2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BExportInvoice);
                    seller.Invoice = B2BExportInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
                case "AmendedB2BExportInvoice":
                    Invoice AmendedB2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BExportInvoice);
                    seller.Invoice = AmendedB2BExportInvoice;
                    seller.Invoice.InvoiceType = invoiceType;
                    break;
            }
            #endregion

            return seller;

        }

        // prima facie seller responsibility to check whether the value entered is correct or not
        // rather use int.tryparse
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        //public  List<string> GetHSNInformation(string hsnDescription)
        //{
        //    // this will go
        //    List<string> hsn;
        //    ExcelDB exceldb = new ExcelDB();
        //    hsn = exceldb.GetHSNInformation(hsnDescription);
        //    return hsn;
        //}


        public GST_MST_ITEM GetItemInformation(string itemCode)
        {
            // this will go

            //HSN hsn=new HSN();
            //unitOfWork = new UnitOfWork();
            var item = unitOfWork.ItemRepository.Filter(f => f.ItemCode.Trim() == itemCode && f.Status==true).SingleOrDefault();
            //hsn.HSNID = item.Item_ID;
            //hsn.HSNNumber = item.ItemCode;
            //hsn.HSNID = item.ItemType;
            //hsn.HSNID = item.Description;
            //hsn.HSNID = item.Unit;
            //hsn.HSNID = item.IGST;
            //hsn.HSNID = item.CGST;
            //hsn.HSNID = item.SGST;
            //hsn.HSNID = item.CESS;
            //hsn.HSNID = item.UGST;
            //hsn.HSNID = item.IsNotified;
            //hsn.HSNID = item.IsNilRated;
            //hsn.HSNID = item.IsCondition;

            //hsn.HSNID = item.IsExempted;
            //hsn.HSNID = item.IsZeroRated;
            //hsn.HSNID = item.IsNonGSTGoods;
            //hsn.HSNID = item.SpecialConditionApplied;


            return item;
        }

        public bool IsHSNPresentInTheMaster(string hsnNumber)
        {
            // this will go
            bool hsnPresentInMaster = false;

            ExcelDB exceldb = new ExcelDB();
            hsnPresentInMaster = exceldb.IsHSNPresentInTheMaster(hsnNumber);

            return hsnPresentInMaster;
        }

        public bool IsItemPresentInTheMaster(string itemCode)
        {           
            var item = unitOfWork.ItemRepository.Contains(f => f.ItemCode == itemCode && f.Status == true);
            //ExcelDB exceldb = new ExcelDB();
            //hsnPresentInMaster = exceldb.IsHSNPresentInTheMaster(hsnNumber);

            return item;
        }

        public SAC GetSACInformation(string SACNumber)
        {
            // this will go

            SAC sac=new SAC();
            ExcelDB exceldb = new ExcelDB();
            sac = exceldb.GetSACInformation(SACNumber);
            return sac;
        }

        //public List<string> GetSACInformation(string hsnDescription)
        //{
        //    // this will go
        //    List<string> sac;
        //    ExcelDB exceldb = new ExcelDB();
        //    sac = exceldb.GetHSNInformation(hsnDescription);
        //    return sac;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hsn"></param>
        /// <param name="lineCollection"></param>
        /// <returns></returns>
        /// 
        public List<LineEntry> CreateLineCollections(List<string> data, List<LineEntry> lineCollection, string type, int actionCode)
        {
            LineEntry lineEntry = new LineEntry();

            // Getting the count of line entry
            // if the lineCollection is null , that means no line entry is created , id is intialized to 0 , so pre increment to 1
            // lineCollection.Count==null
            if (lineCollection.Count >= 0)
            {
                // controls the count in line collections , for proper numbering of the  line id
                int jockey = lineCollection.Count;
                #region Set LineID
                // pre increment is imp, this will be 1 , if first line is created 
                lineEntry.LineID = ++jockey;
                #endregion

                #region SetTheLineType
                // set the type of line 
                if (GSTINBLStaticValues.ServiceType == type)
                    lineEntry.Type = LineType.Service;
                else
                    lineEntry.Type = LineType.Goods;
                #endregion

                #region Service or Goods in Line
                // Intialize the class of HSN /SAC accordingly 
                //List<string> hsn,List<LineEntry> lineCollection,string type
                if (GSTINBLStaticValues.ServiceType == type)
                {
                    lineEntry.SAC = new SAC();

                    // actioncode is your HSN Number,, this should be done by the method of seller to get sac ccode
                    //lineEntry.SAC.SACNumber = actionCode;
                    int result;
                    bool val = Int32.TryParse(data[0].ToString(), out result);

                    try
                    {
                        if ((data.Count > 0) && (data.Count == 8))
                        {
                            lineEntry.SAC.SACNumber = result.ToString();
                            lineEntry.SAC.Description = data[1].ToString();
                            lineEntry.SAC.UnitOfMeasurement = data[2].ToString();
                            lineEntry.SAC.RateIGST = Decimal.Parse(data[3].ToString());
                            lineEntry.SAC.RateCGST = Decimal.Parse(data[4].ToString());
                            lineEntry.SAC.RateSGST = Decimal.Parse(data[5].ToString());
                            lineEntry.SAC.Cess = Decimal.Parse(data[6].ToString());
                            lineEntry.SAC.IsNotified = Boolean.Parse(data[7].ToString());
                            if (lineEntry.SAC.IsNotified)
                            {
                                lineEntry.SAC.Notification1 = new List<Notified>();

                            }

                        }
                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", arguEx);
                    }
                    catch (System.FormatException formatEx)
                    {
                        System.FormatException formatErr = new System.FormatException("Converting HSN into Int, if alphanum val is entered.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", formatEx);
                    }
                    catch (System.OverflowException overFlw)
                    {
                        System.OverflowException overFlow = new System.OverflowException("Value supplied exceed the range of datatype.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", overFlw);
                    }
                    catch (Exception ex)
                    {
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", ex);
                    }


                }
                // hsn data to b assigned if ,it is first line
                else
                {
                    lineEntry.HSN = new HSN();
                    // actioncode is your HSN Number , this should be done by the method of seller to get sac ccode
                    //lineEntry.HSN.HSNNumber = actionCode;
                    //int result;
                    // bool val = Int32.TryParse(data[0].ToString(), out result);
                    try
                    {
                        if ((data.Count > 0) && (data.Count == 8))
                        {
                            lineEntry.HSN.HSNNumber = data[0].ToString();
                            lineEntry.HSN.Description = data[1].ToString();
                            lineEntry.HSN.UnitOfMeasurement = data[2].ToString();
                            lineEntry.HSN.RateIGST = Decimal.Parse(data[3].ToString());
                            lineEntry.HSN.RateCGST = Decimal.Parse(data[4].ToString());
                            lineEntry.HSN.RateSGST = Decimal.Parse(data[5].ToString());
                            lineEntry.HSN.Cess = Decimal.Parse(data[6].ToString());
                            lineEntry.HSN.IsNotified = Boolean.Parse(data[7].ToString());

                        }
                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", arguEx);
                    }
                    catch (System.FormatException formatEx)
                    {
                        System.FormatException formatErr = new System.FormatException("Converting HSN into Int, if alphanum val is entered.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", formatEx);
                    }
                    catch (System.OverflowException overFlw)
                    {
                        System.OverflowException overFlow = new System.OverflowException("Value supplied exceed the range of datatype.");
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(nullEx.InnerException.ToString(), Nullex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", overFlw);
                    }
                    catch (Exception ex)
                    {
                        GSTInvoiceEx invoiceEx = new GSTInvoiceEx();
                        // TO DO- Pramod , make this code work
                        //invoiceEx.GSTInvoiceEx(ex.InnerException.ToString(), ex);

                        // check in event viewer -TO DO
                        //\\192.168.1.100\Sharing\Ashish Changes\PramodCode\BalajiGSP\BusinessLogic\bin\Debug
                       //// logger.LogError("Seller Fetching action items based on actioncode: Conversion issue", ex);
                    }

                }



                #endregion

            }
            // this part when 1 line entry is already created
            else
            {
                #region Set LineID
                if (lineCollection.Count == 0)
                {
                    // 
                    int id = 0;
                    // getting the number of lines in lineCollection
                    id = lineCollection.Count;

                    // pre incrementing it
                    ++id;

                    //assigning it line id
                    lineEntry.LineID = id;
                }
                #endregion

                #region SetTheLineType
                // set the type of line 
                if (GSTINBLStaticValues.ServiceType == type)
                    lineEntry.Type = LineType.Service;
                else
                    lineEntry.Type = LineType.Goods;
                #endregion

                #region Service or Goods in Line
                // Intialize the class of HSN /SAC accordingly 
                //List<string> hsn,List<LineEntry> lineCollection,string type
                if (GSTINBLStaticValues.ServiceType == type)
                {
                    lineEntry.SAC = new SAC();
                    lineEntry.SAC.SACNumber = actionCode.ToString();

                    if ((data.Count > 0) && (data.Count == 7))
                    {
                        lineEntry.SAC.Description = data[0].ToString();
                        lineEntry.SAC.UnitOfMeasurement = data[1].ToString();
                        lineEntry.SAC.RateIGST = Convert.ToDecimal(data[2]);
                        lineEntry.SAC.RateCGST = Convert.ToDecimal(data[3]);
                        lineEntry.SAC.RateSGST = Convert.ToDecimal(data[4]);
                        lineEntry.SAC.Cess = Convert.ToDecimal(data[5]);
                        lineEntry.SAC.IsNotified = Convert.ToBoolean(data[6]);
                    }

                }
                else
                {
                    lineEntry.HSN = new HSN();
                    lineEntry.HSN.HSNNumber = actionCode.ToString();
                    if ((data.Count > 0) && (data.Count == 7))
                    {
                        lineEntry.HSN.Description = data[0].ToString();
                        lineEntry.HSN.UnitOfMeasurement = data[1].ToString();
                        // BVA: for decimal values when data is getting entered in the backend
                        lineEntry.HSN.RateIGST = Convert.ToDecimal(data[2]);
                        lineEntry.HSN.RateCGST = Convert.ToDecimal(data[3]);
                        lineEntry.HSN.RateSGST = Convert.ToDecimal(data[4]);
                        lineEntry.HSN.Cess = Convert.ToDecimal(data[5]);
                        lineEntry.HSN.IsNotified = Convert.ToBoolean(data[6]);
                    }
                }



                #endregion
            }

            // add funcitons retrun void of Linked list
            // so just add it 
            lineCollection.Add(lineEntry);
            // return it
            return lineCollection;

        }

        /// <summary>
        /// // scenario handled user entered a valid hsn and populated the HSN class in the first go, called Create Line Collections
        // then he makes the 
        // A) Field blank
        //B ) Enters a valid HSN
        //C) Enter a invalid HSN

        /// <summary>
        /// This method is overloaded wiht 3 and 5 parameters
        /// This method takes into account when empty string is entered in the second attempt on the same line ID 
        /// after a valid HSN/SAC is entered in the first attempt 
        /// </summary>
        /// <param name="lineCollection">Complete lineCollection with various lineID</param>
        /// <param name="lineID">LineID wheere we need to updatee</param>
        /// <param name="goodServiceCode"> string contains a HSN/SAC value or empty string</param>
        /// <returns>Update LineCollection , so as data sancity is maintained</returns>
        public List<LineEntry> UpdateLineCollections(List<LineEntry> lineCollection, int lineID, string actionCodeVal, int ControlType)
        {
            foreach (var l in lineCollection)
            {
                // line id matches , assuming multiple lines r there
                // and the HSN or sac entered is empty string )
                // along with this 
                // 
                switch (ControlType)
                {
                    case 1:
                        {
                            if (l.LineID == lineID && string.IsNullOrEmpty(actionCodeVal) && string.IsNullOrWhiteSpace(actionCodeVal))
                            {
                                // zero means no value
                                l.HSN.HSNNumber = String.Empty;
                                l.HSN.Description = string.Empty;
                                l.HSN.RateCGST = 0.0m;
                                l.HSN.RateIGST = 0.0m;
                                l.HSN.RateSGST = 0.0m;
                                l.HSN.Cess = 0.0m;
                                l.HSN.IsNotified = false;
                                l.HSN.UnitOfMeasurement = string.Empty;
                                l.Qty = 0.0m;
                                l.PerUnitRate = 0.0m;
                                // empty is there , when user does fiddling with the system
                                l.Type = LineType.Empty;
                                // based on CGST or IGST/SGST combined only taxable value can be retained, hence those r set to 0.0m , tax value is also set 
                                l.TaxableValueLineIDWise = 0.0m;
                                l.TotalLineIDWise = 0.0m;
                                l.Discount = 0.0m;
                                if (l.IsInter)
                                {
                                    l.AmtCGSTLineIDWise = 0.0m;
                                    l.AmtSGSTLineIDWise = 0.0m;
                                }
                                else
                                {
                                    l.AmtCGSTLineIDWise = 0.0m;
                                }

                            }
                            break;
                        }

                    // qty changed after the first pass of OK data
                    // second time he inputs blank or valid data 
                    case 2:
                        {
                            if (l.LineID == lineID && l.Qty.ToString() != actionCodeVal.ToString())
                            {
                                if (actionCodeVal.ToString() != "")
                                {
                                    // v imp , we have not touched Unit of measurement , HSN Numnber , Description and LineType,
                                    // user has not fiddled with the those control , the premise for HSN number related attributes 
                                    // even in this context
                                    l.Qty = Decimal.Parse(actionCodeVal.ToString());


                                }
                                else
                                {
                                    l.Qty = 0.0m;
                                    l.PerUnitRate = 0.0m;
                                    l.TotalLineIDWise = 0.0m;
                                    l.Discount = 0.0m;
                                    l.AmountWithTax = 0.0m;
                                    if (l.IsInter)
                                    {
                                        l.AmtCGSTLineIDWise = 0.0m;
                                        l.AmtSGSTLineIDWise = 0.0m;
                                    }
                                    else
                                    {
                                        l.AmtIGSTLineIDWise = 0.0m;
                                    }
                                    l.TaxBenefitingState = "";
                                    l.Type = LineType.Empty;
                                    l.TaxableValueLineIDWise = 0.0m;
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            if (l.LineID == lineID && l.PerUnitRate.ToString() != actionCodeVal.ToString())
                            {
                                if (actionCodeVal.ToString() != "")
                                {
                                    // v imp , we have not touched Unit of measurement , HSN Numnber , Description and LineType,
                                    // user has not fiddled with the those control , the premise for HSN number related attributes 
                                    // even in this context
                                    l.PerUnitRate = Decimal.Parse(actionCodeVal.ToString());

                                }
                                else
                                    l.PerUnitRate = 0.0m;
                                l.TotalLineIDWise = 0.0m;
                                l.TaxableValueLineIDWise = 0.0m;

                            }
                            break;
                        }

                    case 4:
                        {
                            if (l.LineID == lineID && l.Discount.ToString() != actionCodeVal.ToString())
                            {
                                if (actionCodeVal.ToString() != "")
                                {
                                    // v imp , we have not touched Unit of measurement , HSN Numnber , Description and LineType,
                                    // user has not fiddled with the those control , the premise for HSN number related attributes 
                                    // even in this context
                                    l.Discount = Decimal.Parse(actionCodeVal.ToString());

                                }
                                else
                                {
                                    l.Discount = 0.0m;
                                    l.TaxableValueLineIDWise = l.TotalLineIDWise;
                                }
                            }
                            break;
                        }
                }
            }
            return lineCollection;
        }

        /// <summary>
        ///  This method is overloaded and takes 5 parameter 
        /// </summary>
        /// <param name="lineCollection">Complete lineCollection with various lineID</param>
        /// <param name="lineID">LineID wheere we need to updatee</param>
        /// <param name="goodServiceCode"> string contains a HSN/SAC value or empty str
        /// <param name="goodServiceData">New set of coordinates for updated HSN/SAC code in the second attempt</param>
        /// <param name="actionCode"> to determine , the new coordinates corresponds to a service , goods or empty</param>
        /// <returns>Update LineCollection , so as data sancity is maintained</returns>
        public List<LineEntry> UpdateLineCollections(List<LineEntry> lineCollection, int lineID, string goodServiceCode, List<string> goodServiceData, string actionCode)
        {
            int result;
            bool val = Int32.TryParse(goodServiceData[0].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            foreach (var l in lineCollection)
            {
                if (l.LineID == lineID && (goodServiceCode != (l.HSN.HSNNumber).ToString()))
                // line id matches but the goods or service code does not match to  blank or white space , but a valid HSN
                {
                    if (actionCode == GSTINBLStaticValues.GoodsType)
                        l.Type = LineType.Goods;
                    else if (actionCode == GSTINBLStaticValues.ServiceType)
                        l.Type = LineType.Service;
                    else
                        l.Type = LineType.Empty;

                    // done here to avoid 01011090
                    l.HSN.HSNNumber = goodServiceData[0].ToString();
                    l.HSN.Description = goodServiceData[1].ToString();
                    l.HSN.UnitOfMeasurement = goodServiceData[2].ToString();
                    l.HSN.RateIGST = Decimal.Parse(goodServiceData[3].ToString());
                    l.HSN.RateCGST = Decimal.Parse(goodServiceData[4].ToString());
                    l.HSN.RateSGST = Decimal.Parse(goodServiceData[5].ToString());
                    l.HSN.Cess = Decimal.Parse(goodServiceData[6].ToString());
                    l.HSN.IsNotified = Boolean.Parse(goodServiceData[7].ToString());
                    l.TotalLineIDWise = 0.0m;
                    l.Qty = 0.0m;
                    l.PerUnitRate = 0.0m;
                    l.AmountWithTax = 0.0m;
                    l.TaxableValueLineIDWise = 0.0m;
                    l.TaxValue = 0.0m;
                    l.AmtCGSTLineIDWise = 0.0m;
                    l.AmtSGSTLineIDWise = 0.0m;
                    l.AmtIGSTLineIDWise = 0.0m;
                    l.Discount = 0.0m;

                }
            }

            return lineCollection;

        }

        /// <summary>
        /// Naming convention of each control , contains the numeric digit at the end , shows the row position only
        /// Based  on this row position , we can do manupulation in the UI
        /// 
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public int GetLineIDOfInvoice(string lineID)
        {
            int lastResult;
            int secondLastResutl;
            int thirdLastResult;
            int result = 0;
            int length = 0;
            //
            bool lastStatus;
            bool secondLastStatus;
            bool thirdLastStatus;
            if(lineID!=null)
                { 
                   length = lineID.Length - 1;
                }
            // this way we can have functionality for 999 line entries in one invoice
            lastStatus = Int32.TryParse(lineID[length].ToString(), out lastResult);
            secondLastStatus = Int32.TryParse(lineID[length - 1].ToString(), out secondLastResutl);
            thirdLastStatus = Int32.TryParse(lineID[length - 2].ToString(), out thirdLastResult);

            if (lastStatus && secondLastStatus && thirdLastStatus)
                result = Convert.ToInt32(Convert.ToString(secondLastResutl) + Convert.ToString(secondLastResutl) + Convert.ToString(lastStatus));
            else if (lastStatus && secondLastStatus)
                result = Convert.ToInt32(Convert.ToString(secondLastResutl) + Convert.ToString(lastResult));
            else if (lastStatus)
                result = lastResult;
            else
                result = -1;


            return result;
        }

        /// <summary>
        /// from the above dfunciton, we recieve LineID of the line collection,
        /// we will check whether the line ID exist in the line collection or not 
        /// if it does not exist , then code flow goes normally for CreateLineCollecitons
        /// otherwise it goes for UpdateLineCollection.
        /// </summary>
        /// <param name="lineCollections"></param>
        /// <param name="chkLineIDVal"></param>
        /// <returns></returns>
        public bool CheckLineIDExist(List<LineEntry> lineCollections, int chkLineIDVal)
        {
            bool status = false;
            foreach (var l in lineCollections)
            {
                if (l.LineID == chkLineIDVal)
                {// if true , lineID exist and call the UpdateeLineCollection
                    status = true;
                    break;
                }
            }
            // if false , lineID does not exist and call the CreateLineCollection
            return status;
        }

        public List<LineEntry> GetUpdatedTax(List<LineEntry> lineCollection, Seller seller)
        {
            // Figure out destination of consumption for the HSN concerned
            for (int i = 0; i < lineCollection.Count; i++)
            {
                lineCollection[i].IsIntra = seller.Invoice.GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);

                if (lineCollection[i].IsIntra)
                {
                    lineCollection[i].TaxBenefitingState = seller.Invoice.GetTaxBenefittingState(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
                }

                if (lineCollection[i].IsIntra)
                {
                    lineCollection[i].AmtIGSTLineIDWise = seller.Invoice.CalculateIGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateIGST);
                }
                else
                {
                    lineCollection[i].AmtSGSTLineIDWise = seller.Invoice.CalculateSGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateCGST);
                    lineCollection[i].AmtCGSTLineIDWise = seller.Invoice.CalculateCGSTLineIDWise(lineCollection[i].TaxValue, lineCollection[i].HSN.RateSGST);
                }
            }
            return lineCollection;

        }
        #endregion

    }
    #endregion

    #region ClassSerialNo
    public class SerialNoOfInvoice
    {
        string stateCodeID;

        public string StateCodeID
        {
            get { return stateCodeID; }
            set { stateCodeID = value; }
        }
        string panID;

        public string PanID
        {
            get { return panID; }
            set { panID = value; }
        }
        string entityCode;

        public string EntityCode
        {
            get { return entityCode; }
            set { entityCode = value; }
        }
        string chkDigit;

        public string ChkDigit
        {
            get { return chkDigit; }
            set { chkDigit = value; }
        }

        // this will remain constant for a period of 1 year;
        string financialYear;

        public string FinancialYear
        {
            get { return financialYear; }
            set { financialYear = value; }
        }

        int month;

        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        int seedSequence;

        public int SeedSequence
        {
            get { return seedSequence; }
            set { seedSequence = value; }
        }

        int currentSrlNo;

        public int CurrentSrlNo
        {
            get { return currentSrlNo; }
            set { currentSrlNo = value; }
        }

        public string GenerateFinancialPeriod()
        {
            string currentYear = DateTime.Now.Year.ToString();
            int repeatingYear = Convert.ToInt32(currentYear) + 1;
            string NextYear = repeatingYear.ToString();
            NextYear = NextYear.Substring(NextYear.Length - 2);

            return currentYear + "-" + NextYear;

        }

    }
    #endregion

    #region Reciever
    /// <summary>
    /// Reciever means billed to entity
    /// </summary>
    public class Reciever
    {
        #region datamembers
        string GSTin;
        string nameAsOnGst;
        string address;
        string stateName;
        string stateCode;
        string stateCodeID;
        string financialPeriod;
        string recieveruserID;

        
        // username , in the case of TC or organization is required
        string userName;

        // this is for provisoinal transitional factor
        decimal recieverGrossTurnOver;	

        /* START-- added by priti(06-03-2017) to edit and add reciever details from GSTR2 */
        Seller seller;
        Consignee consignee;
        Invoice invoice;
        List<Reciever> recieverData;
        /* END-- added by priti(06-03-2017) to edit and add reciever details from GSTR2 */

        #endregion

        #region publicHandlers
        /// <summary>
        /// In the case of reciever , GSTIN/Unique ID , 
        /// </summary>
        public string GSTIN
        {
            get { return GSTin; }
            set { GSTin = value; }
        }
        public string NameAsOnGST
        {
            get { return nameAsOnGst; }
            set { nameAsOnGst = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
        public string StateCode
        {
            get { return stateCode; }
            set { stateCode = value; }
        }
        public string StateCodeID
        {
            get { return stateCodeID; }
            set { stateCodeID = value; }
        }
        public string FinancialPeriod
        {
            get { return financialPeriod; }
            set { financialPeriod = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string RecieveruserID
        {
            get { return recieveruserID; }
            set { recieveruserID = value; }
        }

        public decimal RecieverGrossTurnOver
        {
            get { return recieverGrossTurnOver; }
            set { recieverGrossTurnOver = value; }
        }

        /* START-- added by priti(06-03-2017) to edit and add reciever details from GSTR2 */
        public Seller Seller
        {
            get { return seller; }
            set { seller = value; }
        }
        public Consignee Consignee
        {
            get { return consignee; }
            set { consignee = value; }
        }
        public Invoice Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }
        public List<Reciever> RecieverData
        {
            get { return recieverData; }
            set { recieverData = value; }
        }
       
        /* END-- added by priti(06-03-2017) to edit and add reciever details from GSTR2 */

    }
        #endregion
    #endregion

    #region BusinessEntityType
    public enum BusinessEntityType
    {
        Propritership = 1,
        Partnership, // add more
    }
    #endregion

    #region Consignee
    /// <summary>
    /// StateCode in here determines Intrastate or Interstate.
    /// </summary>
    public class Consignee
    {
        #region datamembers
        string GSTin;
        string nameAsOnGst;
        string address;
        string stateName;
        string stateCode;
        string stateCodeID;
        string consigneeUserID;
        public string ConsigneeUserID
        {
            get { return consigneeUserID; }
            set { consigneeUserID = value; }
        }
        // username , in the case of TC or organization is required
        string userName;

        // this is autopopulated field , fixed for one year 
        string consigneesellerFinancialPeriod;

        // this is for provisoinal transitional factor
        decimal consigneeGrossTurnOver;
        #endregion

        #region publicHandlers
        /// <summary>
        /// In the case of reciever , GSTIN/Unique ID , for Govt/PSU/UN.
        /// </summary>
        public string GSTIN
        {
            get { return GSTin; }
            set { GSTin = value; }
        }

        public string NameAsOnGST
        {
            get { return nameAsOnGst; }
            set { nameAsOnGst = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }


        public string StateCode
        {
            get { return stateCode; }
            set { stateCode = value; }
        }

        public string StateCodeID
        {
            get { return stateCodeID; }
            set { stateCodeID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }




        public decimal ConsigneeGrossTurnOver
        {
            get { return consigneeGrossTurnOver; }
            set { consigneeGrossTurnOver = value; }
        }

        public string ConsigneeFinancialPeriod
        {
            get { return consigneesellerFinancialPeriod; }
            set { consigneesellerFinancialPeriod = value; }
        }



        #endregion
    }

    #endregion


}




