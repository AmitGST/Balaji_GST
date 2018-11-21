
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.B2B.GST.GSTIN
{
    public class TransitionalProvisonal
    {

        #region instance member
        decimal stockInHand;
        decimal itcCarriedForward;
        #endregion

        #region PublicHandler

        public decimal ItcCarriedForward
        {
            get { return itcCarriedForward; }
            set { itcCarriedForward = value; }
        }

        public decimal StockInHand
        {
            get { return stockInHand; }
            set { stockInHand = value; }
        }
        #endregion
    }
}
