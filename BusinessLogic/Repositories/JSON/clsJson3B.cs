using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.JSON
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
        public List<ItcInelg> itc_inelg { get; set; }
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

    public class Sgst
    {
        public int intr { get; set; }
        public int tx { get; set; }
        public int fee { get; set; }
    }

    public class Cgst
    {
        public int intr { get; set; }
        public int tx { get; set; }
        public int fee { get; set; }
    }

    public class Cess
    {
        public int intr { get; set; }
        public int tx { get; set; }
    }

    public class Igst
    {
        public int intr { get; set; }
        public int tx { get; set; }
    }

    public class TxPy
    {
        public int trans_typ { get; set; }
        public string trans_desc { get; set; }
        public int liab_ldg_id { get; set; }
        public Sgst sgst { get; set; }
        public Cgst cgst { get; set; }
        public Cess cess { get; set; }
        public Igst igst { get; set; }
    }

    public class Pdcash
    {
        public int liab_ldg_id { get; set; }
        public int trans_typ { get; set; }
        public int ipd { get; set; }
        public int cpd { get; set; }
        public int spd { get; set; }
        public int cspd { get; set; }
        public int i_intrpd { get; set; }
        public int c_intrpd { get; set; }
        public int s_intrpd { get; set; }
        public int cs_intrpd { get; set; }
        public int c_lfeepd { get; set; }
        public int s_lfeepd { get; set; }
    }

    public class Pditc
    {
        public int liab_ldg_id { get; set; }
        public int trans_typ { get; set; }
        public int i_pdi { get; set; }
        public int i_pdc { get; set; }
        public int i_pds { get; set; }
        public int c_pdi { get; set; }
        public int c_pdc { get; set; }
        public int s_pdi { get; set; }
        public int s_pds { get; set; }
        public int cs_pdcs { get; set; }
    }

    public class TxPmt
    {
        public List<TxPy> tx_py { get; set; }
        public List<Pdcash> pdcash { get; set; }
        public Pditc pditc { get; set; }
    }

    public class IntrDetails
    {
        public int iamt { get; set; }
        public int camt { get; set; }
        public int samt { get; set; }
        public int csamt { get; set; }
    }

    public class LtfeeDetails
    {
        public int camt { get; set; }
        public int samt { get; set; }
    }

    public class IntrLtfee
    {
        public IntrDetails intr_details { get; set; }
        public LtfeeDetails ltfee_details { get; set; }
    }

    public class RootObject
    {
        public string gstin { get; set; }
        public string ret_period { get; set; }
        public SupDetails sup_details { get; set; }
        public InterSup inter_sup { get; set; }
        public ItcElg itc_elg { get; set; }
        public InwardSup inward_sup { get; set; }
        public TxPmt tx_pmt { get; set; }
        public IntrLtfee intr_ltfee { get; set; }
    }
    class clsJson3B
    {
        public string gstin { get; set; }
        public string fp { get; set; }
        public List<SupDetails> supdetails { get; set; }
        public List<InterSup> inter_sup { get; set; }
        public List<ItcElg> itc_elg { get; set; }
        public List<InwardSup> inward_sup { get; set; }
        public List<TxPmt> tx_pmt { get; set; }
        public List<IntrLtfee> intr_ltfee { get; set; }
    }
}

