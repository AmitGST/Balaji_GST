using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.B2B.GST.GSTIN
{
    public class SaleLedger
    {
         #region Overridding the default constructor
         public SaleLedger()
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
}
