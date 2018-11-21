using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.GSTN_API
{
   public class dto_GSTR1:SaveGSTRData
    {
        private SaveGSTRData Savegstrdata(GST_TRN_INVOICE invoice)
        {
            SaveGSTRData gstrdata = new SaveGSTRData();
                gstrdata.gstin = invoice.AspNetUser.GSTNNo;
                gstrdata.fp = Convert.ToString(invoice.FinYear_ID);
                gstrdata.gt = Convert.ToInt64(invoice.AspNetUser.GrossTurnOver);
                gstrdata.cur_gt = Convert.ToInt64(invoice.AspNetUser.GrossTurnOver);

                List<B2b> b2b = new List<B2b>();
                foreach (var crData in invoice.GST_TRN_CRDR_NOTE)
               {
                   b2b.Add(b2bData(crData));
               }
             //   gstrdata.B2b = b2b;

           return gstrdata;
        }
       private B2b b2bData(GST_TRN_CRDR_NOTE crdrNote) 
       {
           B2b clsb2b = new B2b();
          /// clsb2b.ctin = crdrNote.
           return clsb2b;
       }

    }
}
