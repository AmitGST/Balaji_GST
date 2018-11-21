using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using com.B2B.GST.GSTIN;
using com.B2B.GST.ExcelFunctionality;

namespace com.B2B.GST.GSTInvoices
{

    /// <summary>
    ///  CDN Invoice data , applied on B2b or b2c
    /// </summary>
    public class CreditNotesInvoice : Invoice
    {


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
            return 10.05m;
        }
        public override decimal GetTaxableValueLineIDWise(decimal total, decimal discount)
        {
            decimal taxableValue;
            if (discount != 0 || discount != 0.00m)
                taxableValue = total - (total * (discount / 100));
            else
                taxableValue = total;
            return taxableValue;
        }
        public override bool GetConsumptionDestinationOfGoodsOrServices(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID)
        {
            bool isIntra = false;
            // Case 1: When Reciever(BilledTo) and Consignee(ShippedTo) is same AND Seller is also in the same state
            //  state code id is same for seller , reciever and consignee then return false to IsIntra. This is then Inter state transaction--> No state wise summary
            if ((sellerStateCodeID.Equals(recieverStateCodeID, StringComparison.OrdinalIgnoreCase)) && (recieverStateCodeID.Equals(consigneeStateCodeID, StringComparison.OrdinalIgnoreCase)))
            {
                isIntra = false;

            }
            // Case 2: seller and (reciever + consignee(same state)) in different state --> state wise summary is there , IGST applicable
            //  Case 3: nested if seller , reciever and consignee all  r in differnet state --> state wise summary is there , IGST applicable
            if (sellerStateCodeID != recieverStateCodeID)
            {
                // this is not required here.
                if (recieverStateCodeID != consigneeStateCodeID)
                {
                    isIntra = true;

                }
                isIntra = true;

            }
            return isIntra;
        }

        // if intra , IGST is put also capturing the destination state
        public override string GetTaxBenefittingState(string sellerStateCodeID, string recieverStateCodeID, string consigneeStateCodeID)
        {
            string taxBenefitingState = "NA";
            // Case 1: When Reciever(BilledTo) and Consignee(ShippedTo) is same AND Seller is also in the same state
            //  state code id is same for seller , reciever and consignee then return false to IsIntra. This is then Inter state transaction--> No state wise summary
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
                taxBenefitingState = recieverStateCodeID;

            }
            return taxBenefitingState;

        }

        // if intra , calculate IGST
        public override decimal CalculateIGSTLineIDWise(decimal taxableValue, decimal rateIGST)
        {
            decimal IGST = (taxableValue * (19 / 100));
            return IGST;
        }
        // if inter, calculate CGST
        public override decimal CalculateCGSTLineIDWise(decimal taxableValue, decimal rateIGST)
        {
            decimal CGST = (taxableValue * (9 / 100));
            return CGST;
        }

        // if inter, calculate SGST
        public override decimal CalculateSGSTLineIDWise(decimal taxableValue, decimal rateIGST)
        {
            decimal SGST = (taxableValue * (9 / 100));
            return SGST;
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
            decimal AmountWithTax = Math.Round((amount + tax));

            return AmountWithTax;
        }
        public override decimal CalculateTotalCGSTSGST(decimal CGST, decimal SGST)
        {
            decimal TotalCGSTSGST = Math.Round((CGST + SGST));

            return TotalCGSTSGST;
        }
        public override DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate)
        {
            throw new NotImplementedException();
        }
        public override int SaveInvoiceData(Seller seller)
        {
            throw new NotImplementedException();
        }
        public override int UploadInvoice(List<Seller> SellerDaTa, String InvoiceNo)
        {
            throw new NotImplementedException();
        }
    }
}