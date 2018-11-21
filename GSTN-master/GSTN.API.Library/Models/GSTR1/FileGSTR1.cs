using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GSTR1
{
    public class CptySum
    {
        public string ctin { get; set; }
        public string chksum { get; set; }
        public int ttl_rec { get; set; }
        public int ttl_val { get; set; }
        public double ttl_igst { get; set; }
        public int ttl_cgst { get; set; }
        public double ttl_sgst { get; set; }
        public int ttl_cess { get; set; }
        public int ttl_tax { get; set; }
    }

    public class SecSum
    {
        public string sec_nm { get; set; }
        public string chksum { get; set; }
        public int ttl_rec { get; set; }
        public int ttl_val { get; set; }
        public double ttl_igst { get; set; }
        public int ttl_cgst { get; set; }
        public double ttl_sgst { get; set; }
        public int ttl_cess { get; set; }
        public int ttl_tax { get; set; }
        public List<CptySum> cpty_sum { get; set; }
    }

    public class FileGSTR1
    {
        public string gstin { get; set; }
        public string ret_period { get; set; }
        public string summ_typ { get; set; }
        public string chksum { get; set; }
        public List<SecSum> sec_sum { get; set; }
    }
}
