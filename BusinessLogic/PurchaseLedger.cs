using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.B2B.GST.GSTInvoices;

namespace com.B2B.GST.GSTIN
{
     /// <summary>
     /// This class basically maintains stock of HSN or SAC available with the seller
     /// at the time of Invoice Creation
     /// </summary>
     public class PurchaseLedger
     {
         #region Overridding the default constructor
         public PurchaseLedger()
         {
             stockLineEntry = null;
         }
         #endregion

         // instance Member
         List<StockRegisterLineEntry> stockLineEntry;

         // Public handler
         public List<StockRegisterLineEntry> StockLineEntry
         {
             get { return stockLineEntry; }
             set { stockLineEntry = value; }
         }


     }

    /// <summary>
    /// This calls maintains the line entry 
    /// </summary>
     public class StockRegisterLineEntry
     {

         #region Overriding Default Constructor
         public StockRegisterLineEntry()
         {
             lineID = -1;
             hsn = null;
             qty = 0.0m;
             perUnitRate = 0.0m;

             /*
              * DateTime myDate = new DateTime();
              * DateTime myDate = default(DateTime);
              * Both of them are equal 1/1/0001 12:00:00 AM
              * Above value is also equal to DateTime.MinValue
              */
             stockInwardDate = default(DateTime);
             stockOrderDate = default(DateTime);
             orderPO = string.Empty;

         }
         #endregion

         #region instance members

         // gstinNumber is the gstin of the seller
         string gstinNumber;
               
         // userName stands for the Tax Consultant
         string userName;
                 
         // sequence of 1 to 10  -- attribute of line
         int lineID;

         // compound data type
         string hsn;

         //-- attribute of line
         decimal qty;

         //-- attribute of line
         decimal perUnitRate;

         // Date on which delevery of stock is taken in the premises
         DateTime stockInwardDate;

         

         // Date on which order for the stock is placed with the seller
         DateTime stockOrderDate;

         

         //Order PO
         string orderPO;

        


         #endregion

         #region PublicHandlers

         public string GstinNumber
         {
             get { return gstinNumber; }
             set { gstinNumber = value; }
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

         public string Hsn
         {
             get { return hsn; }
             set { hsn = value; }
         }

         public decimal PerUnitRate
         {
             get { return perUnitRate; }
             set { perUnitRate = value; }
         }

         public DateTime StockInwardDate
         {
             get { return stockInwardDate; }
             set { stockInwardDate = value; }
         }

         public DateTime StockOrderDate
         {
             get { return stockOrderDate; }
             set { stockOrderDate = value; }
         }

         public string OrderPO
         {
             get { return orderPO; }
             set { orderPO = value; }
         }

         public string UserName
         {
             get { return userName; }
             set { userName = value; }
         }
         #endregion
     }
}
