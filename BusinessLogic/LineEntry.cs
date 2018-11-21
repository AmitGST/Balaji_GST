using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.B2B.GST.GSTInvoices
{

    #region LineType , it is there to help in composite invoices 
    public enum LineType
    {
        Goods,
        Service,
        Empty
    }
    #endregion

    public class LineEntry
    {
        #region datamembers
        
        // set line type whether goods or service -- attribute of line
        LineType type;

        // sequence of 1 to 10  -- attribute of line
        int lineID;
               
        // compound data type
        HSN hsn;
        
        // compound data type
        SAC sac;

        //New Item Type Introdduced
        GST_MST_ITEM item;

       

        //-- attribute of line
        decimal qty;

        //-- attribute of line
        int unit;

        //-- attribute of line
        decimal perUnitRate;

        // unit * perUnitRate
        decimal totalLineIDWise;

        // discount is applied on totalLineIDWise
        decimal discount;

        // tax value , unit * rate per unit * tax applicable
        decimal taxableValueLineIDWise;
            
        // if Inter then all the tax will be collected in this head only
        decimal amtCGSTLineIDWise;

        // if Intra then all the tax will split into half to SGST and CGST
        decimal amtSGSTLineIDWise;

        // if Intra then all the tax will split into half to SGST and CGST
        decimal amtIGSTLineIDWise;
        
        // Inter state , when origin and cosumption code varies
        bool isInter;

        // Intra state , when origin and cosumption code is same
        bool isIntra;

        // which state coffer is benefited
        string taxBenefitingState;

        // this is goind to be redundant
        decimal amountWithTax;

        // with or without discount amt+taxes
        decimal amountWithTaxLineIDWise;

        
        decimal tarrif;

        #region 4 broad categories  of HSN: as mentioned in the returns document
        bool isHSNNilRated;
        bool isHSNExempted;
        bool isHSNNonGSTGoods;
        bool isHSNZeroRatedGSTGoods;
        #endregion

        #region 4 broad categories of SAC
        bool isSACNilRated;        
        bool isSACExempted;              
        bool isSACNonGSTService;        
        bool isSACZeroRatedGSTGoods;
#endregion
             
        
        #endregion

        #region publicHandlers

        public LineType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int LineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        
        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        
        public decimal PerUnitRate
        {
            get { return perUnitRate; }
            set { perUnitRate = value; }
        }

        public decimal TotalLineIDWise
        {
            get { return totalLineIDWise; }
            set { totalLineIDWise = value; }
        }

        public decimal TaxableValueLineIDWise
        {
            get { return taxableValueLineIDWise; }
            set { taxableValueLineIDWise = value; }
        }

        public decimal AmountWithTax
        {
            get { return amountWithTax; }
            set { amountWithTax = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        
        public decimal TaxValue
        {
            get { return taxableValueLineIDWise; }
            set { taxableValueLineIDWise = value; }
        }

        public decimal AmtCGSTLineIDWise
        {
            get { return amtCGSTLineIDWise; }
            set { amtCGSTLineIDWise = value; }
        }

        public decimal AmtSGSTLineIDWise
        {
            get { return amtSGSTLineIDWise; }
            set { amtSGSTLineIDWise = value; }
        }

        public decimal AmtIGSTLineIDWise
        {
            get { return amtIGSTLineIDWise; }
            set { amtIGSTLineIDWise = value; }
        }

        public bool IsHSNNilRated
        {
            
            set { isHSNNilRated = value; }
        }

        public bool IsHSNExempted
        {
            
            set { isHSNExempted = value; }
        }

        public bool IsHSNNonGSTGoods
        {
            
            set { isHSNNonGSTGoods = value; }
        }

        public bool IsHSNZeroRatedGSTGoods
        {
            get { return isHSNZeroRatedGSTGoods; }
            set { isHSNZeroRatedGSTGoods = value; }
        }

        public bool IsSACExempted
        {
            get { return isSACExempted; }
            set { isSACExempted = value; }
        }

        public bool IsSACNilRated
        {
            get { return isSACNilRated; }
            set { isSACNilRated = value; }
        }

        public bool IsSACNonGSTService
        {
            get { return isSACNonGSTService; }
            set { isSACNonGSTService = value; }
        }

        public bool IsSACZeroRatedGSTGoods
        {
            get { return isSACZeroRatedGSTGoods; }
            set { isSACZeroRatedGSTGoods = value; }
        }

        public HSN HSN
        {
            get { return hsn; }
            set { hsn = value; }
        }

        public SAC SAC
        {
            get { return sac; }
            set { sac = value; }
        }

        public GST_MST_ITEM Item
        {
            get { return item; }
            set { item = value; }
        }

        public bool IsInter
        {
            get { return isInter; }
            set { isInter = value; }
        }

        public string TaxBenefitingState
        {
            get { return taxBenefitingState; }
            set { taxBenefitingState = value; }
        }

        public decimal Tarrif
        {
            get { return tarrif; }
            set { tarrif = value; }
        }

        public bool IsIntra
        {
            get { return isIntra; }
            set { isIntra = value; }
        }

        public decimal AmountWithTaxLineIDWise
        {
            get { return amountWithTaxLineIDWise; }
            set { amountWithTaxLineIDWise = value; }
        }

        #endregion

    }
}

