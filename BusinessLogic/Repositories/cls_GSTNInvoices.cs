//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

namespace BusinessLogic.Repositories
{
 public  class cls_GSTNInvoices
    {
        UnitOfWork unitOfWork;

        public cls_GSTNInvoices()
        {
            unitOfWork = new UnitOfWork();
        }

        //test methd
        public class ItmDet
        {
            public int? txval { get; set; }
            public int? rt { get; set; }
            public int? iamt { get; set; }
            public int? csamt { get; set; }
        }


        public class Itm
        {
            public int? num { get; set; }
            public ItmDet itm_det { get; set; }
        }

        public class Inv
        {
            public string inum { get; set; }
            public string idt { get; set; }
            public int? val { get; set; }
            public string pos { get; set; }
            public string rchrg { get; set; }
            public string inv_typ { get; set; }
            public List<Itm> itms { get; set; }
        }

        public class B2b
        {
            public string ctin { get; set; }
            public List<Inv> inv { get; set; }
        }

        public class ItmDet2
        {
            public int? txval { get; set; }
            public int? rt { get; set; }
            public int? iamt { get; set; }
            public int? csamt { get; set; }
        }

        public class Itm2
        {
            public int? num { get; set; }
            public ItmDet2 itm_det { get; set; }
        }

        public class Inv2
        {
            public string etin { get; set; }
            public string inum { get; set; }
            public string idt { get; set; }
            public double? val { get; set; }
            public List<Itm2> itms { get; set; }
        }

        public class B2cl
        {
            public string pos { get; set; }
            public List<Inv2> inv { get; set; }
        }

        public class B2cs
        {
            public string sply_ty { get; set; }
            public string pos { get; set; }
            public string typ { get; set; }
            public double? txval { get; set; }
            public int? rt { get; set; }
            public double? iamt { get; set; }
            public int? camt { get; set; }
            public int? samt { get; set; }
            public int? csamt { get; set; }
            public string etin { get; set; }
        }

        public class Itm3
        {
            public int? txval { get; set; }
            public int? rt { get; set; }
            public double? iamt { get; set; }
            public int? csamt { get; set; }
        }

        public class Inv3
        {
            public string inum { get; set; }
            public string idt { get; set; }
            public string sbpcode { get; set; }
            public int? sbnum { get; set; }
            public string sbdt { get; set; }
            public double? val { get; set; }
            public List<Itm3> itms { get; set; }
        }

        public class Exp
        {
            public string exp_typ { get; set; }
            public List<Inv3> inv { get; set; }
        }

        public class Itm4
        {
            public int? rt { get; set; }
            public int? ad_amt { get; set; }
            public double? camt { get; set; }
            public double? samt { get; set; }
            public int? csamt { get; set; }
            public double? iamt { get; set; }
        }

        public class At
        {
            public string pos { get; set; }
            public string sply_ty { get; set; }
            public List<Itm4> itms { get; set; }
        }

        public class ItmDet3
        {
            public int? txval { get; set; }
            public int? rt { get; set; }
            public int? iamt { get; set; }
            public int? csamt { get; set; }
        }

        public class Itm5
        {
            public int? num { get; set; }
            public ItmDet3 itm_det { get; set; }
        }

        public class Nt
        {
            public string nt_num { get; set; }
            public string nt_dt { get; set; }
            public string inum { get; set; }
            public string ntty { get; set; }
            public string rsn { get; set; }
            public string idt { get; set; }
            public int? val { get; set; }
            public string p_gst { get; set; }
            public List<Itm5> itms { get; set; }
        }

        public class Cdnr
        {
            public string ctin { get; set; }
            public List<Nt> nt { get; set; }
        }

        public class ItmDet4
        {
            public int? txval { get; set; }
            public int? rt { get; set; }
            public int? iamt { get; set; }
            public int? csamt { get; set; }
        }

        public class Itm6
        {
            public int? num { get; set; }
            public ItmDet4 itm_det { get; set; }
        }

        public class Cdnur
        {
            public string nt_num { get; set; }
            public string nt_dt { get; set; }
            public string inum { get; set; }
            public string ntty { get; set; }
            public string rsn { get; set; }
            public string idt { get; set; }
            public int? val { get; set; }
            public string p_gst { get; set; }
            public string typ { get; set; }
            public List<Itm6> itms { get; set; }
        }

        public class Datum
        {
            public int? num { get; set; }
            public string hsn_sc { get; set; }
            public string desc { get; set; }
            public string uqc { get; set; }
            public double? qty { get; set; }
            public double? val { get; set; }
            public double? txval { get; set; }
            public double? iamt { get; set; }
            public decimal? samt { get; set; }
            public decimal? camt { get; set; }
            public decimal? csamt { get; set; }
        }

        public class Hsn
        {
            public List<Datum> data { get; set; }
        }

        public class Itm7
        {
            public int? rt { get; set; }
            public int? ad_amt { get; set; }
            public int? iamt { get; set; }
            public int? csamt { get; set; }
        }

        public class Txpd
        {
            public string pos { get; set; }
            public string sply_ty { get; set; }
            public List<Itm7> itms { get; set; }
        }

        public class clsonlineinvoice
        {
            public string gstin { get; set; }
            public string fp { get; set; }
            public int? gt { get; set; }
            public int? cur_gt { get; set; }
            public string version { get; set; }
            public string hash { get; set; }
            public List<B2b> b2b { get; set; }
            public List<B2cl> b2cl { get; set; }
            public List<B2cs> b2cs { get; set; }
            public List<Exp> exp { get; set; }
            public List<At> at { get; set; }
            public List<Cdnr> cdnr { get; set; }
            public List<Cdnur> cdnur { get; set; }
            public Hsn hsn { get; set; }
            public List<Txpd> txpd { get; set; }
        }


        //to do 
        //public static string GetCurrentGSTIN()
        //{
        //    UnitOfWork unitofWork = new UnitOfWork();
        //    var userid = LoggedInUserID();
        //    var gstin = unitofWork.AspnetRepository.Filter(x => x.Id == userid).Select(x => x.GSTNNo).FirstOrDefault();
        //    return gstin;
        //}
        //public static string LoggedInUserID()
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();
        //    if (userId != null)
        //        return userId;
        //    else
        //        return string.Empty;
        //}

        // final method 
        public string generateJson(Int16 getMonth, string gstin, string userid, string isAmendment)
        {

            if (isAmendment == Convert.ToString(GST.Utility.EnumConstants.InvoiceStatus.Amended))
            {
                var data = unitOfWork.InvoiceRepository.Filter(x => x.SellerUserID == userid && x.InvoiceStatus == 1 && x.InvoiceMonth == getMonth);
                clsonlineinvoice OnlineInvoices = new clsonlineinvoice();
                OnlineInvoices.gstin = gstin;
                OnlineInvoices.hash = "hash";
                OnlineInvoices.gt = 0;
                OnlineInvoices.cur_gt = 0;
                OnlineInvoices.fp = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                OnlineInvoices.version = "GST2.0";
                OnlineInvoices.b2b = new List<B2b>();
                OnlineInvoices.b2cl = new List<B2cl>();
                OnlineInvoices.b2cs = new List<B2cs>();
                OnlineInvoices.cdnr = new List<Cdnr>();
                OnlineInvoices.cdnur = new List<Cdnur>();
                OnlineInvoices.exp = new List<Exp>();
                OnlineInvoices.at = new List<At>();

                OnlineInvoices.txpd = new List<Txpd>();
                var B2Bs = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Regular);
                foreach (var B2B in B2Bs)
                {
                    OnlineInvoices.b2b.Add(GetB2b(B2B));
                }

                var b2cldatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CL);
                foreach (var b2cldata in b2cldatas)
                {
                    OnlineInvoices.b2cl.Add(GetB2cl(b2cldata));
                }

                var b2csdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CS);
                foreach (var b2csdata in b2csdatas)
                {
                    OnlineInvoices.b2cs.Add(GetB2cs(b2csdata));
                }

                var expdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export);
                foreach (var expdata in expdatas)
                {
                    OnlineInvoices.exp.Add(GetExpData(expdata));
                }


                var ats = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Advance);
                foreach (var at in ats)
                {
                    OnlineInvoices.at.Add(Getatsdata(at));
                }

                //var cdnrdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.CDNR);
                //foreach (var cdnrdata in cdnrdatas)
                //{
                //    OnlineInvoices.cdnr.Add(Getcdnrdata(cdnrdata));
                //}

                //var cdnurdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.CDNUR);
                //foreach (var cdnur in cdnurdatas)
                //{
                //    OnlineInvoices.cdnur.Add(Getcdnurdata(cdnur));
                //}
                //var hsndatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.HSN).ToList();
                //OnlineInvoices.hsn = Gethsndata(hsndatas);

                //var txpddatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.MASTER);
                //foreach (var txpddata in txpddatas)
                //{
                //    OnlineInvoices.txpd.Add(Gettxpdata(txpddata));
                //}


                return DownloadJsonApi(OnlineInvoices);

            }
            else
            {
                //var gstin = GetCurrentGSTIN();
                //var userid =LoggedInUserID();
                var data = unitOfWork.InvoiceRepository.Filter(x => x.SellerUserID == userid && x.InvoiceStatus == 0 && x.InvoiceMonth == getMonth);
                clsonlineinvoice OnlineInvoices = new clsonlineinvoice();
                OnlineInvoices.gstin = gstin;
                OnlineInvoices.hash = "hash";
                OnlineInvoices.gt = 0;
                OnlineInvoices.cur_gt = 0;
                OnlineInvoices.fp = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                OnlineInvoices.version = "GST2.0";
                OnlineInvoices.b2b = new List<B2b>();
                OnlineInvoices.b2cl = new List<B2cl>();
                OnlineInvoices.b2cs = new List<B2cs>();
                OnlineInvoices.cdnr = new List<Cdnr>();
                OnlineInvoices.cdnur = new List<Cdnur>();
                OnlineInvoices.exp = new List<Exp>();
                OnlineInvoices.at = new List<At>();

                OnlineInvoices.txpd = new List<Txpd>();
                var B2Bs = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Regular);
                foreach (var B2B in B2Bs)
                {
                    OnlineInvoices.b2b.Add(GetB2b(B2B));
                }

                var b2cldatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CL);
                foreach (var b2cldata in b2cldatas)
                {
                    OnlineInvoices.b2cl.Add(GetB2cl(b2cldata));
                }

                var b2csdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CS);
                foreach (var b2csdata in b2csdatas)
                {
                    OnlineInvoices.b2cs.Add(GetB2cs(b2csdata));
                }

                var expdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export);
                foreach (var expdata in expdatas)
                {
                    OnlineInvoices.exp.Add(GetExpData(expdata));
                }


                var ats = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Advance);
                foreach (var at in ats)
                {
                    OnlineInvoices.at.Add(Getatsdata(at));
                }

                //var cdnrdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.CDNR);
                //foreach (var cdnrdata in cdnrdatas)
                //{
                //    OnlineInvoices.cdnr.Add(Getcdnrdata(cdnrdata));
                //}

                //var cdnurdatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.CDNUR);
                //foreach (var cdnur in cdnurdatas)
                //{
                //    OnlineInvoices.cdnur.Add(Getcdnurdata(cdnur));
                //}
                //var hsndatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.HSN).ToList();
                //OnlineInvoices.hsn = Gethsndata(hsndatas);

                //var txpddatas = data.Where(x => x.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.MASTER);
                //foreach (var txpddata in txpddatas)
                //{
                //    OnlineInvoices.txpd.Add(Gettxpdata(txpddata));
                //}


                return DownloadJsonApi(OnlineInvoices);

            }

        }

        //Test Methods
        private Txpd Gettxpdata(GST_TRN_INVOICE txpddata)
        {
            Txpd txpd = new Txpd();
            txpd.pos = Convert.ToString(txpddata.TaxBenefitingState);
            txpd.sply_ty = txpddata.IsInter == true ? "INTER" : "INTRA";
            foreach (var data in txpddata.GST_TRN_INVOICE_DATA)
            {
                txpd.itms.Add(GetItm7(data));
            }

            return txpd;
        }

        private Itm7 GetItm7(GST_TRN_INVOICE_DATA data)
        {
            Itm7 itm7 = new Itm7();
            itm7.csamt = (int?)data.CessAmt;
            itm7.iamt = (int?)data.IGSTAmt;
            itm7.rt = (int?)data.Rate;
            return itm7;
        }

        private Hsn Gethsndata(List<GST_TRN_INVOICE_DATA> hsndatas)
        {
            Hsn hsnsource = new Hsn();
            hsnsource.data = new List<Datum>();
            foreach (var data in hsndatas)
            {
                hsnsource.data.Add(GetDatum(data));
            }
            return hsnsource;
        }

        private Datum GetDatum(GST_TRN_INVOICE_DATA data)
        {

            Datum datumSource = new Datum();
            datumSource.desc = (string)data.GST_MST_ITEM.Description;
            datumSource.uqc = "ABC";
            datumSource.hsn_sc = (string)data.GST_MST_ITEM.ItemCode;
            datumSource.num = 12;
            //if (data.Count > 0)
            //{
            datumSource.samt = (int)data.SGSTAmt;
            datumSource.camt = (int)data.CGSTAmt;
            datumSource.csamt = (int)data.CessAmt;
            datumSource.qty = (double?)data.Qty;
            datumSource.txval = (double?)data.TotalAmountWithTax;
            datumSource.iamt = (double?)data.IGSTAmt;
            datumSource.val = (double?)data.TotalAmount;
            //}
            return datumSource;

        }

        public Cdnur Getcdnurdata(GST_TRN_CRDR_NOTE cdnurdata)
        {
            Cdnur cdnur = new Cdnur();
            cdnur.idt = Convert.ToDateTime(cdnurdata.GST_TRN_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
            cdnur.inum = cdnurdata.GST_TRN_INVOICE.InvoiceNo;
            cdnur.nt_dt = Convert.ToDateTime(cdnurdata.GST_TRN_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
            cdnur.nt_num = cdnurdata.GST_TRN_INVOICE.InvoiceNo;
            cdnur.ntty = Convert.ToString(cdnurdata.NoteType);
            cdnur.p_gst = Convert.ToString(cdnurdata.P_Gst);
            //cdnur.rsn = Convert.ToString(cdnurdata.Issuing_Note);
            //cdnur.val = Convert.ToInt32(cdnurdata.Voucher_Value);
            cdnur.itms = new List<Itm6>();
            foreach (var data in cdnurdata.GST_TRN_CRDR_NOTE_DATA)
            {
                cdnur.itms.Add(GetItem6(data));
            }
            return cdnur;
        }

        private Itm6 GetItem6(GST_TRN_CRDR_NOTE_DATA data)
        {
            Itm6 itm6data = new Itm6();
            itm6data.num = 12;
            itm6data.itm_det = new ItmDet4
            {
                csamt = (int?)data.CessAmt,
                iamt = (int?)data.IGSTAmt,
                txval = (int?)data.TotalAmountWithTax,
                rt = (int)data.Rate
            };
            return itm6data;
        }

        private At Getatsdata(GST_TRN_INVOICE atdata)
        {
            At at = new At();
            at.itms = new List<Itm4>();
            // at.pos = atdata.GST_MST_STATE.StateName;
            at.itms.AddRange(GetItm4(atdata));
            at.sply_ty = atdata.IsInter == true ? "INTER" : "INTRA";
            return at;
        }

        private List<Itm4> GetItm4(GST_TRN_INVOICE atdata)
        {
            List<Itm4> itm4s = new List<Itm4>();
            Itm4 itm4 = new Itm4();
            foreach (var data in atdata.GST_TRN_INVOICE_DATA)
            {
                itm4.camt = (double?)data.CGSTAmt;
                itm4.csamt = (int?)data.CessAmt;
                itm4.iamt = (double?)data.IGSTAmt;
                itm4.rt = (int?)data.Rate;
                itm4.samt = (int?)data.SGSTAmt;
                itm4s.Add(itm4);
            }
            return itm4s;
        }

        public Itm5 GetItem5(GST_TRN_CRDR_NOTE_DATA item5Invoice)
        {
            Itm5 itm5data = new Itm5();
            itm5data.itm_det = new ItmDet3();
            itm5data.num = 12;
            itm5data.itm_det = new ItmDet3
            {
                csamt = (int?)item5Invoice.CessAmt,
                iamt = (int?)item5Invoice.IGSTAmt,
                txval = (int?)item5Invoice.TotalAmountWithTax,
                rt = (int?)item5Invoice.Rate
            };
            return itm5data;


        }

        public Nt GetNt(GST_TRN_CRDR_NOTE_DATA ntInvoice)
        {
            Nt ntdata = new Nt();
            ntdata.itms = new List<Itm5>();
            ntdata.nt_dt = Convert.ToString(ntInvoice.GST_TRN_CRDR_NOTE.GST_TRN_INVOICE.InvoiceDate);
            ntdata.nt_num = ntInvoice.GST_TRN_CRDR_NOTE.GST_TRN_INVOICE.InvoiceNo;
            ntdata.ntty = Convert.ToString(ntInvoice.GST_TRN_CRDR_NOTE.NoteType);
            ntdata.p_gst = Convert.ToString(ntInvoice.GST_TRN_CRDR_NOTE.P_Gst);
            //ntdata.rsn = Convert.ToString(ntInvoice.Issuing_Note);
            //ntdata.val = Convert.ToInt32(ntInvoice.GST_TRN_OFFLINE_INVOICE.Voucher_Value);
            ntdata.itms.Add(GetItem5(ntInvoice));
            return ntdata;
        }

        public Cdnr Getcdnrdata(GST_TRN_CRDR_NOTE cdnrInvoice)
        {
            Cdnr cdnrdatas = new Cdnr();
            cdnrdatas.ctin = "Tin";//cdnrInvoice.TIN;
            cdnrdatas.nt = new List<Nt>();
            foreach (var data in cdnrInvoice.GST_TRN_CRDR_NOTE_DATA)
            {
                cdnrdatas.nt.Add(GetNt(data));
            }
            return cdnrdatas;
        }

        public B2cl GetB2cl(GST_TRN_INVOICE b2clInvoice)
        {
            B2cl b2cldata = new B2cl();
            b2cldata.inv = new List<Inv2>();
            b2cldata.pos = b2clInvoice.AspNetUser.StateCode.ToString();
            foreach (var data in b2clInvoice.GST_TRN_INVOICE_DATA)
            {
                b2cldata.inv.Add(GetInv2(data));
            }
            return b2cldata;
        }

        public Inv2 GetInv2(GST_TRN_INVOICE_DATA Inv2invoice)
        {

            Inv2 inv2data = new Inv2();
            inv2data.itms = new List<Itm2>();
            inv2data.etin = "tinno";
            inv2data.idt = "idt";
            inv2data.inum = Inv2invoice.GST_TRN_INVOICE.InvoiceNo;
            inv2data.val = Convert.ToDouble(Inv2invoice.TotalAmountWithTax);
            inv2data.itms.Add(GetItm2(Inv2invoice));
            return inv2data;

        }

        private Itm2 GetItm2(GST_TRN_INVOICE_DATA Inv2invoice)
        {
            return new Itm2
            {
                num = 12,
                itm_det = new ItmDet2
                    {
                        csamt = (int?)Inv2invoice.CessAmt,
                        iamt = (int?)Inv2invoice.IGSTAmt,
                        rt = Inv2invoice.Rate == null ? null : (int?)Inv2invoice.Rate,
                        txval = (int?)Inv2invoice.TotalAmountWithTax
                    }
            };//Convert.ToInt32(Inv2invoice.GST_TRN_OFFLINE_INVOICE_RATE.RATE)
        }

        public ItmDet2 GetItmDet2(GST_TRN_INVOICE_DATA ItmDet2Invoice)
        {
            ItmDet2 invDet2 = new ItmDet2();
            invDet2.csamt = (int?)ItmDet2Invoice.CessAmt;
            invDet2.iamt = (int?)ItmDet2Invoice.IGSTAmt;
            invDet2.rt = ItmDet2Invoice.Rate == null ? null : (int?)ItmDet2Invoice.Rate;
            invDet2.txval = (int?)ItmDet2Invoice.TotalAmountWithTax;
            return invDet2;
        }

        public B2cs GetB2cs(GST_TRN_INVOICE ItmB2csInvoice)
        {

            B2cs invb2cs = new B2cs();
            invb2cs.pos = Convert.ToString(ItmB2csInvoice.AspNetUser.StateCode);
            // invb2cs.typ = Convert.ToString(ItmB2csInvoice.Type);
            var b2bdata = ItmB2csInvoice.GST_TRN_INVOICE_DATA.FirstOrDefault();
            if (b2bdata != null)
            {
                invb2cs.camt = (int?)b2bdata.CGSTAmt;
                invb2cs.csamt = (int?)b2bdata.CessAmt;
                //invb2cs.etin = ItmB2csInvoice.TinNo;
                invb2cs.iamt = (int?)b2bdata.IGSTAmt;
                invb2cs.samt = (int?)b2bdata.SGSTAmt;
                //invb2cs.sply_ty = ItmB2csInvoice.
                invb2cs.txval = (double?)b2bdata.TaxableAmount;
                invb2cs.rt = b2bdata.Rate == null ? null : (int?)b2bdata.Rate;
            }
            return invb2cs;


        }

        private Exp GetExpData(GST_TRN_INVOICE expdata)
        {
            return new Exp { exp_typ = "Wpay", inv = GetInv3(expdata) };
        }

        private List<Inv3> GetInv3(GST_TRN_INVOICE expdata)
        {
            List<Inv3> invs3 = new List<Inv3>();
            Inv3 inv3;
            foreach (var dataitem in expdata.GST_TRN_INVOICE_DATA)
            {
                inv3 = new Inv3();
                inv3.itms = new List<Itm3>();
                inv3.idt = Convert.ToDateTime(dataitem.GST_TRN_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
                inv3.inum = dataitem.GST_TRN_INVOICE.InvoiceNo;
                inv3.itms.Add(GetItm3(dataitem));
                //  inv3.sbdt = Convert.ToDateTime(dataitem.GST_TRN_OFFLINE_INVOICE.ShippingBillDate).ToString("dd-mm-yyyy");
                // inv3.sbnum = Convert.ToInt32(dataitem.GST_TRN_OFFLINE_INVOICE.ShippingBillNo);
                // inv3.sbpcode = dataitem.GST_TRN_OFFLINE_INVOICE.PortCode;
                inv3.val = (int?)dataitem.TotalAmountWithTax;
                invs3.Add(inv3);
            }
            return invs3;
        }

        private Itm3 GetItm3(GST_TRN_INVOICE_DATA dataitem)
        {
            return new Itm3
            {
                csamt = (int?)dataitem.CessAmt,
                iamt = (int?)dataitem.IGSTAmt,
                rt = dataitem.Rate == null ? null : (int?)dataitem.Rate,
                txval = (int?)dataitem.TotalAmountWithTax
            };
        }

        public B2b GetB2b(GST_TRN_INVOICE B2B)
        {
            return new B2b { ctin = "tinno", inv = GetInv(B2B) };
        }

        private List<Inv> GetInv(GST_TRN_INVOICE B2B)
        {
            List<Inv> invs = new List<Inv>();
            Inv inv;
            foreach (var dataitem in B2B.GST_TRN_INVOICE_DATA)
            {
                inv = new Inv();
                inv.itms = new List<Itm>();
                inv.idt = Convert.ToDateTime(dataitem.GST_TRN_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
                inv.inum = dataitem.GST_TRN_INVOICE.InvoiceNo;
                inv.inv_typ = dataitem.GST_TRN_INVOICE.InvoiceType == null ? "" : ((EnumConstants.InvoiceType)dataitem.GST_TRN_INVOICE.InvoiceType).ToString()[0].ToString();
                inv.itms.Add(GetItm(dataitem));
                //inv.pos = dataitem..PlaceofSupply == null ? "" : dataitem.GST_TRN_OFFLINE_INVOICE.GST_MST_STATE.StateCode;
                //inv.rchrg = Convert.ToBoolean(dataitem.GST_TRN_OFFLINE_INVOICE.ReverseCharge) ? "Y" : "N";
                inv.val = (int?)dataitem.TotalAmountWithTax;
                invs.Add(inv);
            }
            return invs;
        }

        private Itm GetItm(GST_TRN_INVOICE_DATA dataitem)
        {
            Itm Item = new Itm();
            Item.num = 12;
            ItmDet ItemDetail = new ItmDet();
            ItemDetail.csamt = (int?)dataitem.CessAmt;
            ItemDetail.iamt = (int?)dataitem.IGSTAmt;
            ItemDetail.rt = dataitem.Rate == null ? null : (int?)dataitem.Rate;
            ItemDetail.txval = (int?)dataitem.TotalAmountWithTax;
            Item.itm_det = ItemDetail;

            return Item;
        }
        //pending
        public string DownloadJsonApi(clsonlineinvoice JSONOffline)
        {
               var json = new JavaScriptSerializer().Serialize(JSONOffline);
                
                return json;
        }
    }
}
