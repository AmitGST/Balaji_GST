
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.API.GSTN.GSTR3B
{

    public class OsupDet
    {
        public int txval { get; set; }
        public int iamt { get; set; }
        public int camt { get; set; }
        public int samt { get; set; }
        public int csamt { get; set; }
    }

    public class OsupZero
    {
        public int txval { get; set; }
        public int iamt { get; set; }
        public int csamt { get; set; }
    }

    public class OsupNilExmp
    {
        public int txval { get; set; }
    }

    public class IsupRev
    {
        public int txval { get; set; }
        public int iamt { get; set; }
        public int camt { get; set; }
        public int samt { get; set; }
        public int csamt { get; set; }
    }

    public class OsupNongst
    {
        public int txval { get; set; }
    }

    public class SupDetails
    {
        public OsupDet osup_det { get; set; }
        public OsupZero osup_zero { get; set; }
        public OsupNilExmp osup_nil_exmp { get; set; }
        public IsupRev isup_rev { get; set; }
        public OsupNongst osup_nongst { get; set; }
    }

    public class UnregDetail
    {
        public string pos { get; set; }
        public int txval { get; set; }
        public int iamt { get; set; }
    }

    public class CompDetail
    {
        public string pos { get; set; }
        public int txval { get; set; }
        public int iamt { get; set; }
    }

    public class UinDetail
    {
        public string pos { get; set; }
        public int txval { get; set; }
        public int iamt { get; set; }
    }

    public class InterSup
    {
        public List<UnregDetail> unreg_details { get; set; }
        public List<CompDetail> comp_details { get; set; }
        public List<UinDetail> uin_details { get; set; }
    }

    public class ItcAvl
    {
        public string ty { get; set; }
        public double iamt { get; set; }
        public int camt { get; set; }
        public double samt { get; set; }
        public int csamt { get; set; }
    }

    public class ItcRev
    {
        public string ty { get; set; }
        public double iamt { get; set; }
        public int camt { get; set; }
        public double samt { get; set; }
        public int csamt { get; set; }
    }

    public class ItcNet
    {
        public double iamt { get; set; }
        public int camt { get; set; }
        public double samt { get; set; }
        public int csamt { get; set; }
    }

    public class ItcInelg
    {
        public string ty { get; set; }
        public double iamt { get; set; }
        public int camt { get; set; }
        public double samt { get; set; }
        public int csamt { get; set; }
    }

    public class ItcElg
    {
        public List<ItcAvl> itc_avl { get; set; }
        public List<ItcRev> itc_rev { get; set; }
        public ItcNet itc_net { get; set; }
        public List<ItcInelg> itcinelg { get; set; }
    }

    public class IsupDetail
    {
        public string ty { get; set; }
        public int inter { get; set; }
        public int intra { get; set; }
    }

    public class InwardSup
    {
        public List<IsupDetail> isup_details { get; set; }
    }

    public class IntrDetails
    {
        public int iamt { get; set; }
        public int camt { get; set; }
        public int samt { get; set; }
        public int csamt { get; set; }
    }

    public class IntrLtfee
    {
        public IntrDetails intr_details { get; set; }
    }

    public class GSTR3BTotal
    {
        public string gstin { get; set; }
        public string ret_period { get; set; }
        public SupDetails sup_details { get; set; }
        public InterSup inter_sup { get; set; }
        public ItcElg itc_elg { get; set; }
        public InwardSup inward_sup { get; set; }
        public IntrLtfee intr_ltfee { get; set; }
    }
}
