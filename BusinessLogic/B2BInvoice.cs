using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.B2B.GST.ExcelFunctionality;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using System.Data;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System.Collections;

namespace com.B2B.GST.GSTInvoices
{

    public class B2BInvoice : Invoice
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        //ExcelDB exceldb = new ExcelDB();
        //Seller seller = new Seller();
        #region members
        bool isAdvancePaymentChecked;
        bool isExportChecked;

        // Export and amended export invoices added, a stands for Ammended
        B2BExportInvoice exportInvoice;
        AmendedB2BExportInvoice aExportInvoice;

        // Advances recieved
        AdvanceRecieptInvoice advances;

        #endregion

        #region Property
        public bool IsAdvancePaymentChecked
        {
            get { return isAdvancePaymentChecked; }
            set { isAdvancePaymentChecked = value; }
        }


        public bool IsExportChecked
        {
            get { return isExportChecked; }
            set { isExportChecked = value; }
        }

        public B2BExportInvoice ExportInvoice
        {
            get { return exportInvoice; }
            set { exportInvoice = value; }
        }

        public AmendedB2BExportInvoice AExportInvoice
        {
            get { return aExportInvoice; }
            set { aExportInvoice = value; }
        }

        public AdvanceRecieptInvoice Advances
        {
            get { return advances; }
            set { advances = value; }
        }
        #endregion

        #region Methods

        public override void GetInvoiceDetails(DateTime taxPeriodStDt, DateTime taxPeriodEdDt)
        {
            throw new NotImplementedException();
        }

        public override void GetHSNSumamry(DateTime taxPeriodStDt, DateTime taxPeriodEdDt)
        {
            throw new NotImplementedException();
        }

        public override void GetSACSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt)
        {
            throw new NotImplementedException();
        }

        public override void GetStateSummary(DateTime taxPeriodStDt, DateTime taxPeriodEdDt)
        {
            throw new NotImplementedException();
        }




        public override decimal GetTotalLineIDWise(decimal qty, decimal perUnitRate)
        {
            return qty * perUnitRate;
        }

        public override decimal GetTaxableValueLineIDWise(decimal total, decimal discount)
        {
            decimal taxableValue;
            if (discount > 0.0m)
            {
                taxableValue = total - (total * (discount / 100));
                taxableValue = Math.Round(taxableValue, 2);
            }
            else
                taxableValue = total;
            return taxableValue;
        }

        // Checking wheter it is an inter /Intra state transactions
        public override bool GetConsumptionDestinationOfGoodsOrServices(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID)
        {
            bool isInter = false;
            // Case 1: When Reciever(BilledTo) and Consignee(ShippedTo) is same AND Seller is also in the same state
            //  state code id is same for seller , reciever and consignee then return false to isInter. This is then Inter state transaction--> No state wise summary
            if ((sellerStateCodeID.Equals(recieverStateCodeID, StringComparison.OrdinalIgnoreCase)) && (recieverStateCodeID.Equals(consigneeStateCodeID, StringComparison.OrdinalIgnoreCase)))
            {
                isInter = false;

            }
            // Case 2: seller and (reciever + consignee(same state)) in different state --> state wise summary is there , IGST applicable
            //  Case 3: nested if seller , reciever and consignee all  r in differnet state --> state wise summary is there , IGST applicable

            // TEMPORARILY OFF NEED TO CHECK AGAIN AFTER GETING COMPLETE SCENARIO
            //if(sellerStateCodeID !=recieverStateCodeID)
            //{
            //    // this is not required here.
            //    if(recieverStateCodeID!=consigneeStateCodeID)
            //    {
            //        isInter = true;

            //    }
            //    isInter = true;

            //}
            else if (sellerStateCodeID == consigneeStateCodeID)
            {
                isInter = false;
            }
            else if (sellerStateCodeID != consigneeStateCodeID)
            {
                isInter = true;
            }
            else
            {
                isInter = true;
            }

            return isInter;
        }

        // if intra , IGST is put also capturing the destination state
        public override string GetTaxBenefittingState(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID)
        {
            string taxBenefitingState = "NA";
            // Case 1: When Reciever(BilledTo) and Consignee(ShippedTo) is same AND Seller is also in the same state
            //  state code id is same for seller , reciever and consignee then return false to isInter. This is then Inter state transaction--> No state wise summary
            if ((sellerStateCodeID.Equals(recieverStateCodeID, StringComparison.OrdinalIgnoreCase)) && (recieverStateCodeID.Equals(consigneeStateCodeID, StringComparison.OrdinalIgnoreCase)))
            {
                taxBenefitingState = sellerStateCodeID;

            }
            // Case 2: seller and (reciever + consignee(same state)) in different state --> state wise summary is there , IGST applicable
            //  Case 3: nested if seller , reciever and consignee all  r in differnet state --> state wise summary is there , IGST applicable
            if (sellerStateCodeID != recieverStateCodeID)
            {
                // this is not required here.
                if (recieverStateCodeID != consigneeStateCodeID)
                {
                    taxBenefitingState = consigneeStateCodeID;

                }
                else
                    taxBenefitingState = recieverStateCodeID;


            }

            return taxBenefitingState;

        }

        // if intra , calculate IGST
        public override decimal CalculateIGSTLineIDWise(decimal taxableValue, decimal rateIGST)
        {
            decimal IGST = (taxableValue * (rateIGST / 100));
            IGST = Math.Round(IGST, 2);
            return IGST;
        }

        // if inter, calculate CGST
        public override decimal CalculateCGSTLineIDWise(decimal taxableValue, decimal rateCGST)
        {
            decimal CGST = (taxableValue * (rateCGST / 100));
            CGST = Math.Round(CGST, 2);
            return CGST;
        }

        // if inter, calculate SGST
        public override decimal CalculateSGSTLineIDWise(decimal taxableValue, decimal rateSGST)
        {
            decimal SGST = (taxableValue * (rateSGST / 100));
            SGST = Math.Round(SGST, 2);
            return SGST;
        }
        // if inter, calculate UGST
        public decimal CalculateUGSTLineIDWise(decimal taxableValue, decimal rateUGST)
        {
            decimal UGST = (taxableValue * (rateUGST / 100));
            UGST = Math.Round(UGST, 2);
            return UGST;
        }
        // if inter, calculate CESS
        public decimal CalculateCESSLineIDWise(decimal taxableValue, decimal rateCESS)
        {
            decimal CESS = (taxableValue * (rateCESS / 100));
            CESS = Math.Round(CESS, 2);
            return CESS;
        }

        public override List<Notified> GetHSNNotificationData(string hsnNumber)
        {
            List<Notified> notify = new List<Notified>();
            ExcelDB exceldb = new ExcelDB();
            notify = exceldb.GetHSNNotificationData(hsnNumber);
            return notify;
        }

        public override List<Condition> GetHSNNotifiedConditionData(string hsnNumber)
        {
            List<Condition> condition = new List<Condition>();
            ExcelDB exceldb = new ExcelDB();
            condition = exceldb.GetNotifiedHSNConditionsData(hsnNumber);
            return condition;
        }


        public override List<Notified> GetSACNotificationData(string sacNumber)
        {
            List<Notified> notify = new List<Notified>();
            ExcelDB exceldb = new ExcelDB();
            notify = exceldb.GetSACNotificationData(sacNumber);
            return notify;
        }

        public override List<Condition> GetSACNotifiedConditionData(string sacNumber)
        {
            List<Condition> condition = new List<Condition>();
            ExcelDB exceldb = new ExcelDB();
            condition = exceldb.GetNotifiedSACConditionsData(sacNumber);
            return condition;
        }

        public override decimal CalculateAmountWithTax(decimal amount, decimal tax)
        {
            decimal AmountWithTax = (amount + tax);
            AmountWithTax = Math.Round(AmountWithTax, 2);

            return AmountWithTax;
        }
        public override decimal CalculateTotalCGSTSGST(decimal CGST, decimal SGST)
        {
            decimal TotalCGSTSGST = (CGST + SGST);
            return TotalCGSTSGST;
        }

        public override DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate)
        {
            DataSet ds = new DataSet();
            ExcelDB exceldb = new ExcelDB();
            ds = exceldb.ViewInvoice(SellerGSTN, FromDt, Todate);
            return ds;
        }

        public override int SaveInvoiceData(Seller seller)
        {
            cls_Invoice invoiceObject = new cls_Invoice();
            int result = 0;

            GST_TRN_INVOICE inv = new GST_TRN_INVOICE();
            inv.InvoiceNo = seller.SellerInvoice;
            inv.InvoiceDate = DateTime.Now;//Convert.ToDateTime(seller.DateOfInvoice);
            inv.InvoiceMonth = Convert.ToByte(DateTime.Now.Month);
            //  inv.InvoiceMonth = Convert.ToByte(DateTime.Now.Month);
            inv.SellerUserID = seller.SellerUserID;
            inv.ReceiverUserID = seller.Reciever.RecieveruserID;
            inv.ConsigneeUserID = seller.Consignee.ConsigneeUserID;
            inv.VendorID = seller.Invoice.VendorID;
            inv.TransShipment_ID = seller.Invoice.TransShipment_ID;
            inv.Freight = seller.Invoice.Freight;
            inv.Insurance = seller.Invoice.Insurance;
            inv.PackingAndForwadingCharges = seller.Invoice.PackingAndForwadingCharges;
            inv.FinYear_ID = invoiceObject.GetCurrentFinYear();// Convert.ToInt32(DateTime.UtcNow.Year + "-" + DateTime.UtcNow.AddYears(1));//Convert.ToInt32(seller.SerialNoInvoice.FinancialYear);
            // inv.IsInter = seller.Reciever.StateCode == seller.Consignee.StateCode ? true : false;
            inv.ReceiverFinYear_ID = invoiceObject.GetCurrentFinYear(); // Convert.ToInt32(DateTime.UtcNow.Year + "-" + DateTime.UtcNow.AddYears(1));// Convert.ToInt32(seller.SerialNoInvoice.FinancialYear);
            inv.Status = true;
            inv.CreatedDate = DateTime.Now;
            inv.InvoiceStatus = (byte)EnumConstants.InvoiceStatus.Fresh;
            // inv.InvoiceStatus = (byte)EnumConstants.InvoiceStatus.Fresh;
            bool isInter = GetConsumptionDestinationOfGoodsOrServices(seller.SellerStateCode, seller.Reciever.StateCode, seller.Consignee.StateCode);
            inv.IsInter = isInter;
            inv.TaxBenefitingState = seller.Consignee.StateCode;
            inv.CreatedBy = seller.CreatedBy;
            //condition for tax con.
            //if ((EnumConstants.InvoiceSpecialCondition.Regular).ToString || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.Export) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.Advance) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.JobWork) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.SEZUnit) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.SEZDeveloper) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.DeemedExport) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.B2CS) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.B2CL) || Convert.ToBoolean(EnumConstants.InvoiceSpecialCondition.ECommerce))
            if (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Regular.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Export.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Advance.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.JobWork.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.B2CS.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.B2CL.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.ECommerce.ToString())
           // if (seller.Invoice.InvoiceSpecialCondition == (EnumConstants.InvoiceSpecialCondition.Regular) || (EnumConstants.InvoiceSpecialCondition.Export) || (EnumConstants.InvoiceSpecialCondition.Advance).ToString() || (EnumConstants.InvoiceSpecialCondition.JobWork).ToString() || (EnumConstants.InvoiceSpecialCondition.SEZUnit).ToString() || (EnumConstants.InvoiceSpecialCondition.SEZDeveloper).ToString() || (EnumConstants.InvoiceSpecialCondition.DeemedExport).ToString() || (EnumConstants.InvoiceSpecialCondition.B2CS).ToString() || (EnumConstants.InvoiceSpecialCondition.B2CL).ToString() || (EnumConstants.InvoiceSpecialCondition.ECommerce).ToString())
            {
                //inv.InvoiceUserID = seller.Reciever.RecieveruserID;
                inv.InvoiceUserID = seller.SellerUserID;
            }
            else
            {
                inv.InvoiceUserID = seller.Reciever.RecieveruserID;
            }
            inv.InvoiceType = (byte)(EnumConstants.InvoiceType)Enum.Parse(typeof(EnumConstants.InvoiceType), seller.Invoice.InvoiceType);

            if (inv.InvoiceType == (byte)EnumConstants.InvoiceType.B2C)
            {
                var totalBillamount = seller.Invoice.LineEntry.Sum(amt => amt.AmountWithTax);
                if (totalBillamount > 250000)
                {
                    inv.InvoiceSpecialCondition = (byte)(EnumConstants.InvoiceSpecialCondition.B2CL);
                }
                else
                {
                    inv.InvoiceSpecialCondition = (byte)(EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), seller.Invoice.InvoiceSpecialCondition);

                }
            }
            else
            {
                inv.InvoiceSpecialCondition = (byte)(EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), seller.Invoice.InvoiceSpecialCondition);
            }

            if (inv.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.ECommerce)
                inv.OrderDate = seller.OrderDate;

            var invoiceCreate = unitOfWork.InvoiceRepository.Create(inv);

            unitOfWork.Save();

            if (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Export.ToString())
            {
                bool isStateExampted = false;
            }
            else
            {
                bool isStateExampted = unitOfWork.StateRepository.Find(f => f.StateCode == seller.Consignee.StateCode).IsExempted.Value;
            }
            bool isExported = false;
            if (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Export.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZDeveloper.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.SEZUnit.ToString() || seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.DeemedExport.ToString())
            {
                isExported = true;
            }

            bool isJobwork = (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.JobWork.ToString());
            bool isImport = (seller.Invoice.InvoiceSpecialCondition == EnumConstants.InvoiceSpecialCondition.Import.ToString());
            var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == seller.SellerStateCode);
            var isUTState = stateData.UT.Value;
            var isExempted = stateData.IsExempted.Value;
            var isEcom = false;
            var isUn = false;
            var invLineItem = from invo in seller.Invoice.LineEntry
                              select new GST_TRN_INVOICE_DATA
                              {
                                  InvoiceID = invoiceCreate.InvoiceID,
                                  LineID = invo.LineID,
                                  // GST_MST_ITEM = invo.Item,
                                  Item_ID = invo.Item.Item_ID,
                                  Qty = invo.Qty,
                                  Rate = invo.PerUnitRate,
                                  TotalAmount = invo.TotalLineIDWise,
                                  Discount = invo.Discount,
                                  TaxableAmount = invo.TaxableValueLineIDWise,
                                  // TotalAmountWithTax = invo.TaxValue,
                                  IGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableValueLineIDWise, invo.Item.IGST.Value), //isJobwork ? 0 : (isUTState ? 0 : (isInter ? invo.Item.IGST : (isExport ? invo.Item.IGST : (isImport ? invo.Item.IGST : 0)))),
                                  IGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.IGST, invo.TaxableValueLineIDWise, invo.Item.IGST.Value),// isJobwork ? 0 : (isUTState ? 0 : (isInter ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableValueLineIDWise, invo.Item.IGST.Value) : 0)))),
                                  CGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableValueLineIDWise, invo.Item.CGST.Value),
                                  CGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.CGST, invo.TaxableValueLineIDWise, invo.Item.CGST.Value),
                                  SGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableValueLineIDWise, invo.Item.SGST.Value),
                                  SGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.SGST, invo.TaxableValueLineIDWise, invo.Item.SGST.Value),
                                  UGSTRate = Calculate.TaxRate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableValueLineIDWise, invo.Item.UGST.Value),
                                  UGSTAmt = Calculate.TaxCalculate(invo.Item, isInter, isExported, isImport, isUTState, isJobwork, isEcom, isUn, EnumConstants.TaxType.UTGST, invo.TaxableValueLineIDWise, invo.Item.UGST.Value),
                                  CessRate = isJobwork ? 0 : invo.Item.CESS,
                                  CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableValueLineIDWise, invo.Item.CESS.Value)
                              };


            foreach (GST_TRN_INVOICE_DATA item in invLineItem)
            {
                item.TotalAmountWithTax = item.TaxableAmount + item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;
                item.CreatedDate = DateTime.Now;
                unitOfWork.InvoiceDataRepository.Create(item);
            }

            unitOfWork.Save();

            //start- Mapp Regular and Jobwork Challan no.
            foreach (Int64 itm in seller.ChallanInvoiceIds)
            {
                GST_TRN_INVOICE_MAP map = new GST_TRN_INVOICE_MAP();
                map.MapfromInvoiceID = itm;
                map.MaptoInvoiceID = invoiceCreate.InvoiceID;
                map.Status = true;
                map.CreatedBy = seller.CreatedBy;
                map.CreatedDate = DateTime.Now;
                unitOfWork.InvoiceMapRepository.Create(map);
            }

            unitOfWork.Save();
            //end
            foreach (Int64 itm in seller.AdvanceInvoiceIds)
            {
                GST_TRN_INVOICE_MAP map = new GST_TRN_INVOICE_MAP();
                map.MapfromInvoiceID = itm;
                map.MaptoInvoiceID = invoiceCreate.InvoiceID;
                map.Status = true;
                map.CreatedBy = seller.CreatedBy;
                map.CreatedDate = DateTime.Now;
                unitOfWork.InvoiceMapRepository.Create(map);

            }
            unitOfWork.Save();

            cls_ITC itc = new cls_ITC();
            itc.ITCVoucherType = (byte)EnumConstants.ITCVoucherType.TaxInvoice;
            itc.SaveItc(invoiceCreate);

            cls_PurchaseRegister purchaseRegister = new cls_PurchaseRegister();
            //var allitems = invLineItem.Select(s => s.Item_ID).Distinct();
            if (inv.InvoiceSpecialCondition == (byte)(EnumConstants.InvoiceSpecialCondition.Advance))
            {
                //No data will be save in sale and purchase.
            }
            else if (inv.InvoiceSpecialCondition != (byte)(EnumConstants.InvoiceSpecialCondition.Import))  //enumm
            {
                foreach (GST_TRN_INVOICE_DATA item in invLineItem)
                {
                    var itemType = unitOfWork.ItemRepository.Find(f => f.Item_ID == item.Item_ID).ItemType;
                    // var purchaseItems = unitOfWork.PurchaseDataRepositry.Filter(f => f.Status == true && item.Item_ID == f.Item_ID).Select(s => s.GST_MST_PURCHASE_REGISTER.UserID).ToList();
                    if (itemType == (byte)EnumConstants.ItemType.HSN)
                    {
                        string uId = seller.SellerUserID;
                        int iTem = Convert.ToInt32(item.Item_ID);
                        decimal LeftQty = purchaseRegister.GetLeftItemQty(iTem, uId);

                        if (LeftQty > item.Qty)
                        {
                            GST_MST_SALE_REGISTER salRegister = new GST_MST_SALE_REGISTER();
                            salRegister.InvoiceID = invoiceCreate.InvoiceID;
                            salRegister.PerUnitRate = item.Rate;
                            salRegister.Item_ID = item.Item_ID;
                            salRegister.Qty = item.Qty;
                            salRegister.CreatedBy = seller.CreatedBy;
                            salRegister.CreatedDate = DateTime.Now;
                            salRegister.Status = true;
                            salRegister.Id = uId;
                            salRegister.SaleStatus = (byte)EnumConstants.SaleStatus.Fresh;
                            unitOfWork.SaleRegisterDataRepositry.Create(salRegister);
                            unitOfWork.Save();
                        }
                    }
                }
            }

            else
            {
                //Add stock in PURCHSE Reagister When item is import from seller..
                purchaseRegister.LoggedinUserID = invoiceCreate.SellerUserID;
                purchaseRegister.SellerName = seller.NameAsOnGST;
                purchaseRegister.SellerAddress = seller.Address;
                purchaseRegister.SellerGSTN = seller.GSTIN;
                purchaseRegister.ReceiverName = seller.Reciever.NameAsOnGST;
                purchaseRegister.ReceiverAddress = seller.Reciever.Address;
                purchaseRegister.ConsigneeName = seller.Consignee.NameAsOnGST;
                purchaseRegister.ConsigneeAddress = seller.Consignee.Address;
                purchaseRegister.StateCode = seller.SellerStateCode;

                purchaseRegister.SaveInvoiveDataInPurchaseRegister(inv);
            }

            if (inv.InvoiceSpecialCondition == (byte)(EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice))
            {
                GST_TRN_INVOICE_AUDIT_TRAIL IAT = new GST_TRN_INVOICE_AUDIT_TRAIL();

                IAT.InvoiceID = invoiceCreate.InvoiceID;
                IAT.AuditTrailStatus = (byte)EnumConstants.InvoiceAuditTrailSatus.Import2A;
                IAT.InvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Add;
                IAT.UserIP = HelperUtility.IP;
                //IAT.SellerInvoiceAction = "" ;
                IAT.ReceiverInvoiceAction = (byte)EnumConstants.InvoiceActionAuditTrail.Add;
                // IAT.SellerInvoiceActionDate = DateTime.Now;
                IAT.ReceiverInvoiceActionDate = DateTime.Now;
                IAT.CreatedBy = invoiceCreate.CreatedBy;
                IAT.CreatedDate = DateTime.Now;
                unitOfWork.InvoiceAuditTrailRepositry.Create(IAT);
                unitOfWork.Save();
            }
            //foreach (GST_TRN_INVOICE_DATA item in invLineItem)
            //{
            // itc.UserID = seller.Reciever.RecieveruserID;
            // itc.Amount = item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;
            //itc.InvoiceID = invoiceCreate.InvoiceID;
            // itc.ITCStatus = (byte)(EnumConstants.ITCStatus.Active);
            // itc.ITCVoucherType = (byte)EnumConstants.ITCMovement.Credit;
            // itc.ITCDate = DateTime.Now;
            // itc.ITCMovement = (byte)(EnumConstants.InvoiceSpecialCondition.Import);
            //// itc.TaxType = item.UGSTAmt;
            // itc.IGST = item.IGSTAmt;
            // itc.CGST = item.CGSTAmt;
            // if (item.SGSTAmt != 0)
            // { 
            // itc.SGST = item.SGSTAmt;
            // }
            // else
            // {
            //     itc.SGST = item.UGSTAmt;
            // }
            // itc.CreatedBy = seller.CreatedBy;
            // itc.CreatedDate = DateTime.Now;
            // unitOfWork.ITCRepository.Create(itc);
            // unitOfWork.Save();
            //}


            //User Current TurnOver
            GST_MST_USER_CURRENT_TURNOVER turnOver = new GST_MST_USER_CURRENT_TURNOVER();
            turnOver.User_ID = inv.SellerUserID;
            turnOver.Month = inv.InvoiceMonth;
            turnOver.InvoiceID = invoiceCreate.InvoiceID;
            turnOver.InvoiceAmountWithTax = invLineItem.Sum(s => s.TotalAmount);
            turnOver.InvoiceAmountWithoutTax = invLineItem.Sum(s => s.TaxableAmount);
            turnOver.TurnOverStatus = 0;//TODO:Need to change
            turnOver.CreatedBy = inv.CreatedBy;
            turnOver.CreatedDate = DateTime.Now;
            turnOver.Status = true;
            unitOfWork.CurrentTurnoverRepositry.Create(turnOver);
            unitOfWork.Save();
            //// ExcelDB exceldb = new ExcelDB();
            ////result = exceldb.SaveInvoiceData(seller);

            //  } 
            return result;
        }
        //public override int SaveInvoiceData(Seller seller)
        //{
        //    int result = 0;
        //    ExcelDB exceldb = new ExcelDB();
        //    result = exceldb.SaveInvoiceData(seller);
        //    return result;
        //}

        public override int UploadInvoice(List<Seller> SellerDaTa, String InvoiceNo)
        {
            int result = 0;
            Seller seller = new Seller();
            seller.SellerDaTa = new List<Seller>();
            seller.SellerDaTa = SellerDaTa;
            ExcelDB exceldb = new ExcelDB();

            result = exceldb.UploadInvoice(seller.SellerDaTa, InvoiceNo);
            return result;
        }
        #endregion
    }
}