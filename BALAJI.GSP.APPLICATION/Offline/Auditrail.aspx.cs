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
using BusinessLogic.Repositories.Offline;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

namespace BALAJI.GSP.APPLICATION.Offline
{
    public partial class Auditrail : System.Web.UI.Page
    {
        UnitOfWork unitOfWork;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uc_invoiceMonth.BindInvoiceMonth();
                BindItems();
            }
            uc_invoiceMonth.SelectedIndexChange += uc_InvoiceMonth_SelectedIndexChanged;
        }

        public Auditrail()
        {
            unitOfWork = new UnitOfWork();
        }

        private void uc_InvoiceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindItems();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void BindItems()
        {
            try
            {
                var loggedinUser = Common.LoggedInUserID();
                var SelectedMonth = Convert.ToByte(uc_invoiceMonth.GetValue);
                var invoices = unitOfWork.OfflineAudittrailRepository.Filter(f => f.Month == SelectedMonth && f.CreatedBy == loggedinUser && f.Status == 1).OrderByDescending(o => o.InvoiceDate).ToList();
                lv_AuditTrail.DataSource = invoices.ToList();
                lv_AuditTrail.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbDownloadJSON_Click(object sender, EventArgs e)
        {
            //    MainContent.Visible = false;
            //    SecondaryContent.Visible = true;
            //     Response.Redirect("~/Offline/B2Boffline");
            try
            {
                Int16 getMonth = Convert.ToInt16(uc_invoiceMonth.GetValue);
                generateJson(getMonth);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbUploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MonthId"] = Convert.ToInt16(uc_invoiceMonth.GetValue);
                //Session["AuditTrailId"] =((LinkButton)sender).CommandArgument;
                //Session["AuditTrailId"] = null;
                Response.Redirect("~/User/Trans/Offline.aspx");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void lkbDownload3B_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbdownload = (LinkButton)sender;
                String UserID = Convert.ToString(lkbdownload.CommandArgument.ToString());
                var userId = Common.LoggedInUserID();
                //var Month = Convert.ToByte(uc_invoiceMonth.GetValue);
                var SelectedMonth = Int32.Parse(uc_invoiceMonth.GetValue);
                /// var invoice = unitOfWork.OfflineAudittrailRepository.Filter(f => f.UserID == userId).FirstOrDefault();

                ReportGenerate.ExcelDownloadGstr_3B(userId, SelectedMonth);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void generateJson(Int16 getMonth)
        {
            try
            {
                var gstin = Common.GetCurrentGSTIN();
                var userid = Common.LoggedInUserID();
                var data = unitOfWork.OfflineinvoiceRepository.Filter(x => x.UserID == userid && x.GST_TRN_OFFLINE.Month == getMonth);
                clsJSONOffline JSONOffline = new clsJSONOffline();
                JSONOffline.gstin = gstin;
                JSONOffline.hash = "hash";
                JSONOffline.gt = 0;
                JSONOffline.cur_gt = 0;
                JSONOffline.fp = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                JSONOffline.version = "GST2.0";
                JSONOffline.b2b = new List<B2b>();
                JSONOffline.b2cl = new List<B2cl>();
                JSONOffline.b2cs = new List<B2cs>();
                JSONOffline.cdnr = new List<Cdnr>();
                JSONOffline.cdnur = new List<Cdnur>();
                JSONOffline.exp = new List<Exp>();
                JSONOffline.at = new List<At>();

                JSONOffline.txpd = new List<Txpd>();
                var B2Bs = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.B2B);
                foreach (var B2B in B2Bs)
                {
                    JSONOffline.b2b.Add(GetB2b(B2B));
                }

                var b2cldatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.B2CL);
                foreach (var b2cldata in b2cldatas)
                {
                    JSONOffline.b2cl.Add(GetB2cl(b2cldata));
                }

                var b2csdatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.B2CS);
                foreach (var b2csdata in b2csdatas)
                {
                    JSONOffline.b2cs.Add(GetB2cs(b2csdata));
                }

                var expdatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.EXP);
                foreach (var expdata in expdatas)
                {
                    JSONOffline.exp.Add(GetExpData(expdata));
                }


                var ats = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.AT);
                foreach (var at in ats)
                {
                    JSONOffline.at.Add(Getatsdata(at));
                }

                var cdnrdatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.CDNR);
                foreach (var cdnrdata in cdnrdatas)
                {
                    JSONOffline.cdnr.Add(Getcdnrdata(cdnrdata));
                }

                var cdnurdatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.CDNUR);
                foreach (var cdnur in cdnurdatas)
                {
                    JSONOffline.cdnur.Add(Getcdnurdata(cdnur));
                }
                var hsndatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.HSN).ToList();
                JSONOffline.hsn = Gethsndata(hsndatas);

                var txpddatas = data.Where(x => x.SectionType == (byte)EnumConstants.OfflineSection.MASTER);
                foreach (var txpddata in txpddatas)
                {
                    JSONOffline.txpd.Add(Gettxpdata(txpddata));
                }


                DownloadJson(JSONOffline);
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private Txpd Gettxpdata(GST_TRN_OFFLINE_INVOICE txpddata)
        {
            Txpd txpd = new Txpd();
            txpd.pos = Convert.ToString(txpddata.PlaceofSupply);
            txpd.sply_ty = txpddata.SupplyType == 0 ? "INTER" : "INTRA";
            foreach (var data in txpddata.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                txpd.itms.Add(GetItm7(data));
            }

            return txpd;
        }

        private Itm7 GetItm7(GST_TRN_OFFLINE_INVOICE_DATAITEM data)
        {
            Itm7 itm7 = new Itm7();
            itm7.csamt = (int?)data.CessAmt;
            itm7.iamt = (int?)data.IGSTAmt;
            itm7.rt = data.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)data.GST_TRN_OFFLINE_INVOICE_RATE.RATE;
            return itm7;
        }

        private Hsn Gethsndata(List<GST_TRN_OFFLINE_INVOICE> hsndatas)
        {
            Hsn hsnsource = new Hsn();
            hsnsource.data = new List<Datum>();
            foreach (var data in hsndatas)
            {
                hsnsource.data.Add(GetDatum(data));
            }
            return hsnsource;
        }

        private Datum GetDatum(GST_TRN_OFFLINE_INVOICE data)
        {

            Datum datumSource = new Datum();
            datumSource.desc = data.HSNDescription;
            datumSource.uqc = "ABC";
            datumSource.hsn_sc = data.HSN;
            datumSource.num = 12;
            if (data.GST_TRN_OFFLINE_INVOICE_DATAITEM.Count > 0)
            {
                datumSource.samt = (int?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().SGSTAmt;
                datumSource.camt = (int?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().CGSTAmt;
                datumSource.csamt = (int?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().CessAmt;
                datumSource.qty = (double?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().TotalQuantity;
                datumSource.txval = (double?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().TotalTaxableValue;
                datumSource.iamt = (double?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().IGSTAmt;
                datumSource.val = (double?)data.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault().TotalValue;
            }
            return datumSource;

        }

        public Cdnur Getcdnurdata(GST_TRN_OFFLINE_INVOICE cdnurdata)
        {
            Cdnur cdnur = new Cdnur();
            cdnur.idt = Convert.ToDateTime(cdnurdata.InvoiceDate).ToString("dd-mm-yyyy");
            cdnur.inum = cdnurdata.InvoiceNo;
            cdnur.nt_dt = Convert.ToString(cdnurdata.Voucher_date);
            cdnur.nt_num = cdnurdata.Voucher_No;
            cdnur.ntty = Convert.ToString(cdnurdata.NoteType);
            cdnur.p_gst = Convert.ToString(cdnurdata.Pre_GST);
            cdnur.rsn = Convert.ToString(cdnurdata.Issuing_Note);
            cdnur.val = Convert.ToInt32(cdnurdata.Voucher_Value);
            cdnur.itms = new List<Itm6>();
            foreach (var data in cdnurdata.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                cdnur.itms.Add(GetItem6(data));
            }
            return cdnur;
        }

        private Itm6 GetItem6(GST_TRN_OFFLINE_INVOICE_DATAITEM data)
        {
            Itm6 itm6data = new Itm6();
            itm6data.num = 12;
            itm6data.itm_det = new ItmDet4 { csamt = (int?)data.CessAmt, iamt = (int?)data.IGSTAmt, txval = (int?)data.TotalTaxableValue, rt = data.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)data.GST_TRN_OFFLINE_INVOICE_RATE.RATE };
            return itm6data;
        }

        private At Getatsdata(GST_TRN_OFFLINE_INVOICE atdata)
        {
            At at = new At();
            at.itms = new List<Itm4>();
            // at.pos = atdata.GST_MST_STATE.StateName;
            at.itms.AddRange(GetItm4(atdata));
            at.sply_ty = atdata.SupplyType == 0 ? "INTER" : "INTRA";
            return at;
        }

        private List<Itm4> GetItm4(GST_TRN_OFFLINE_INVOICE atdata)
        {
            List<Itm4> itm4s = new List<Itm4>();
            Itm4 itm4 = new Itm4();
            foreach (var data in atdata.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                itm4.camt = (double?)data.CGSTAmt;
                itm4.csamt = (int?)data.CessAmt;
                itm4.iamt = (double?)data.IGSTAmt;
                itm4.rt = data.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)data.GST_TRN_OFFLINE_INVOICE_RATE.RATE;
                itm4.samt = (int?)data.SGSTAmt;
                itm4s.Add(itm4);
            }
            return itm4s;
        }

        public Itm5 GetItem5(GST_TRN_OFFLINE_INVOICE_DATAITEM item5Invoice)
        {
            Itm5 itm5data = new Itm5();
            itm5data.itm_det = new ItmDet3();
            itm5data.num = 12;
            itm5data.itm_det = new ItmDet3 { csamt = (int?)item5Invoice.CessAmt, iamt = (int?)item5Invoice.IGSTAmt, txval = (int?)item5Invoice.TotalTaxableValue, rt = item5Invoice.GST_TRN_OFFLINE_INVOICE_RATE != null ? (int?)item5Invoice.GST_TRN_OFFLINE_INVOICE_RATE.RATE : null };
            return itm5data;


        }

        public Nt GetNt(GST_TRN_OFFLINE_INVOICE_DATAITEM ntInvoice)
        {
            Nt ntdata = new Nt();
            ntdata.itms = new List<Itm5>();
            ntdata.idt = Convert.ToString(ntInvoice.GST_TRN_OFFLINE_INVOICE.InvoiceDate);
            ntdata.inum = ntInvoice.GST_TRN_OFFLINE_INVOICE.InvoiceNo;
            ntdata.nt_dt = Convert.ToString(ntInvoice.GST_TRN_OFFLINE_INVOICE.Voucher_date);
            ntdata.nt_num = ntInvoice.GST_TRN_OFFLINE_INVOICE.Voucher_No;
            ntdata.ntty = Convert.ToString(ntInvoice.GST_TRN_OFFLINE_INVOICE.NoteType);
            ntdata.p_gst = Convert.ToString(ntInvoice.GST_TRN_OFFLINE_INVOICE.Pre_GST);
            ntdata.rsn = Convert.ToString(ntInvoice.GST_TRN_OFFLINE_INVOICE.Issuing_Note);
            ntdata.val = Convert.ToInt32(ntInvoice.GST_TRN_OFFLINE_INVOICE.Voucher_Value);
            ntdata.itms.Add(GetItem5(ntInvoice));
            return ntdata;
        }

        public Cdnr Getcdnrdata(GST_TRN_OFFLINE_INVOICE cdnrInvoice)
        {
            Cdnr cdnrdatas = new Cdnr();
            cdnrdatas.ctin = "Tin";//cdnrInvoice.TIN;
            cdnrdatas.nt = new List<Nt>();
            foreach (var data in cdnrInvoice.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                cdnrdatas.nt.Add(GetNt(data));
            }
            return cdnrdatas;
        }

        public B2cl GetB2cl(GST_TRN_OFFLINE_INVOICE b2clInvoice)
        {
            B2cl b2cldata = new B2cl();
            b2cldata.inv = new List<Inv2>();
            b2cldata.pos = b2clInvoice.PlaceofSupply.ToString();
            foreach (var data in b2clInvoice.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                b2cldata.inv.Add(GetInv2(data));
            }
            return b2cldata;
        }

        public Inv2 GetInv2(GST_TRN_OFFLINE_INVOICE_DATAITEM Inv2invoice)
        {

            Inv2 inv2data = new Inv2();
            inv2data.itms = new List<Itm2>();
            inv2data.etin = "tinno";
            inv2data.idt = "idt";
            inv2data.inum = Inv2invoice.GST_TRN_OFFLINE_INVOICE.InvoiceNo;
            inv2data.val = Convert.ToDouble(Inv2invoice.TotalValue);
            inv2data.itms.Add(GetItm2(Inv2invoice));
            return inv2data;

        }

        private Itm2 GetItm2(GST_TRN_OFFLINE_INVOICE_DATAITEM Inv2invoice)
        {
            return new Itm2 { num = 12, itm_det = new ItmDet2 { csamt = (int?)Inv2invoice.CessAmt, iamt = (int?)Inv2invoice.IGSTAmt, rt = Inv2invoice.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)Inv2invoice.GST_TRN_OFFLINE_INVOICE_RATE.RATE, txval = (int?)Inv2invoice.TotalTaxableValue } };//Convert.ToInt32(Inv2invoice.GST_TRN_OFFLINE_INVOICE_RATE.RATE)
        }

        public ItmDet2 GetItmDet2(GST_TRN_OFFLINE_INVOICE_DATAITEM ItmDet2Invoice)
        {
            ItmDet2 invDet2 = new ItmDet2();
            invDet2.csamt = (int?)ItmDet2Invoice.CessAmt;
            invDet2.iamt = (int?)ItmDet2Invoice.IGSTAmt;
            invDet2.rt = ItmDet2Invoice.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)ItmDet2Invoice.GST_TRN_OFFLINE_INVOICE_RATE.RATE;
            invDet2.txval = (int?)ItmDet2Invoice.TotalTaxableValue;
            return invDet2;
        }

        public B2cs GetB2cs(GST_TRN_OFFLINE_INVOICE ItmB2csInvoice)
        {

            B2cs invb2cs = new B2cs();
            invb2cs.pos = Convert.ToString(ItmB2csInvoice.PlaceofSupply);
            invb2cs.typ = Convert.ToString(ItmB2csInvoice.Type);
            var b2bdata = ItmB2csInvoice.GST_TRN_OFFLINE_INVOICE_DATAITEM.FirstOrDefault();
            if (b2bdata != null)
            {
                invb2cs.camt = (int?)b2bdata.CGSTAmt;
                invb2cs.csamt = (int?)b2bdata.CessAmt;
                //invb2cs.etin = ItmB2csInvoice.TinNo;
                invb2cs.iamt = (int?)b2bdata.IGSTAmt;
                invb2cs.samt = (int?)b2bdata.SGSTAmt;
                //invb2cs.sply_ty = ItmB2csInvoice.
                invb2cs.txval = (double?)b2bdata.TaxableAmount;
                invb2cs.rt = b2bdata.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)b2bdata.GST_TRN_OFFLINE_INVOICE_RATE.RATE;
            }
            return invb2cs;


        }

        private Exp GetExpData(GST_TRN_OFFLINE_INVOICE expdata)
        {
            return new Exp { exp_typ = "Wpay", inv = GetInv3(expdata) };
        }

        private List<Inv3> GetInv3(GST_TRN_OFFLINE_INVOICE expdata)
        {
            List<Inv3> invs3 = new List<Inv3>();
            Inv3 inv3;
            foreach (var dataitem in expdata.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                inv3 = new Inv3();
                inv3.itms = new List<Itm3>();
                inv3.idt = Convert.ToDateTime(dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
                inv3.inum = dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceNo;
                inv3.itms.Add(GetItm3(dataitem));
                inv3.sbdt = Convert.ToDateTime(dataitem.GST_TRN_OFFLINE_INVOICE.ShippingBillDate).ToString("dd-mm-yyyy");
                inv3.sbnum = Convert.ToInt32(dataitem.GST_TRN_OFFLINE_INVOICE.ShippingBillNo);
                inv3.sbpcode = dataitem.GST_TRN_OFFLINE_INVOICE.PortCode;
                inv3.val = (int?)dataitem.TotalTaxableValue;
                invs3.Add(inv3);
            }
            return invs3;
        }

        private Itm3 GetItm3(GST_TRN_OFFLINE_INVOICE_DATAITEM dataitem)
        {
            return new Itm3 { csamt = (int?)dataitem.CessAmt, iamt = (int?)dataitem.IGSTAmt, rt = dataitem.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)dataitem.GST_TRN_OFFLINE_INVOICE_RATE.RATE, txval = (int?)dataitem.TotalTaxableValue };
        }

        private B2b GetB2b(GST_TRN_OFFLINE_INVOICE B2B)
        {
            return new B2b { ctin = "tinno", inv = GetInv(B2B) };
        }

        private List<Inv> GetInv(GST_TRN_OFFLINE_INVOICE B2B)
        {
            List<Inv> invs = new List<Inv>();
            Inv inv;
            foreach (var dataitem in B2B.GST_TRN_OFFLINE_INVOICE_DATAITEM)
            {
                inv = new Inv();
                inv.itms = new List<Itm>();
                inv.idt = Convert.ToDateTime(dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceDate).ToString("dd-mm-yyyy");
                inv.inum = dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceNo;
                inv.inv_typ = dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceType == null ? "" : ((EnumConstants.InvoiceType)dataitem.GST_TRN_OFFLINE_INVOICE.InvoiceType).ToString()[0].ToString();
                inv.itms.Add(GetItm(dataitem));
                inv.pos = dataitem.GST_TRN_OFFLINE_INVOICE.PlaceofSupply == null ? "" : dataitem.GST_TRN_OFFLINE_INVOICE.GST_MST_STATE.StateCode;
                inv.rchrg = Convert.ToBoolean(dataitem.GST_TRN_OFFLINE_INVOICE.ReverseCharge) ? "Y" : "N";
                inv.val = (int?)dataitem.TotalTaxableValue;
                invs.Add(inv);
            }
            return invs;
        }

        private Itm GetItm(GST_TRN_OFFLINE_INVOICE_DATAITEM dataitem)
        {
            Itm Item = new Itm();
            Item.num = 12;
            ItmDet ItemDetail = new ItmDet();
            ItemDetail.csamt = (int?)dataitem.CessAmt;
            ItemDetail.iamt = (int?)dataitem.IGSTAmt;
            ItemDetail.rt = dataitem.GST_TRN_OFFLINE_INVOICE_RATE == null ? null : (int?)dataitem.GST_TRN_OFFLINE_INVOICE_RATE.RATE;
            ItemDetail.txval = (int?)dataitem.TotalTaxableValue;
            Item.itm_det = ItemDetail;

            return Item;
        }

        public void DownloadJson(clsJSONOffline JSONOffline)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(JSONOffline);
                StringWriter oStringWriter = new StringWriter();
                Response.ContentType = "text/plain";

                Response.AddHeader("content-disposition", "attachment;filename=data.json");
                Response.Clear();

                using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
                {
                    writer.Write(json);
                }
                Response.End();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        //private string _AuditTrailId;

        //public string CommandName
        //{
        //    get
        //    {
        //        return _AuditTrailId;
        //    }

        //    set 
        //    {
        //        _AuditTrailId = value; 
        //    }
        //}


        protected void lkbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                
                Session["AuditTrailId"] = ((LinkButton)sender).CommandArgument;
                Session["MonthId"] = null;
                Response.Redirect("~/User/Trans/Offline.aspx");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        //protected void lkbDelete_Click(object sender, EventArgs e)
        //{



        // }


        //Eti
        protected void lv_AuditTrail_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            LinkButton lkbDelete = (lv_AuditTrail.Items[e.ItemIndex].FindControl("lkbDelete")) as LinkButton;
            if (lkbDelete.CommandName == "Update")
            {
                int auditTrailIda = Convert.ToInt32(lkbDelete.CommandArgument);
                GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL auditTrail = unitOfWork.OfflineAudittrailRepository.Filter(f => f.AuditTrailID == auditTrailIda ).SingleOrDefault();
                auditTrail.Status = Convert.ToByte(EnumConstants.Status.Deactive);
                unitOfWork.OfflineAudittrailRepository.Update(auditTrail);
                unitOfWork.Save();
                BindItems();
            }
        }

        protected void lkbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ureturn/ReturnGstr1.aspx");
        }


        //protected void lkbDelete_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    LinkButton btnDelete = (LinkButton)sender;
        //    //    int auditTrailId = Convert.ToInt32(btnDelete.CommandArgument);
        //    //    using (unitOfWork = new UnitOfWork())
        //    //    {
        //    //        List<GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL> auditTrail = unitOfWork.OfflineAudittrailRepository.Filter(f => f.AuditTrailID == auditTrailId).ToList();
        //    //        if (auditTrail != null)
        //    //        {
        //    //            List<GST_TRN_OFFLINE> a = unitOfWork.OfflineRepository.Filter(f => f.AuditTrailID == auditTrailId).ToList();
        //    //            if (a != null)
        //    //                foreach (var data in a)
        //    //                {
        //    //                    foreach (GST_TRN_OFFLINE_INVOICE inv in data.GST_TRN_OFFLINE_INVOICE)
        //    //                    {
        //    //                        foreach (GST_TRN_OFFLINE_INVOICE_DATAITEM invD in inv.GST_TRN_OFFLINE_INVOICE_DATAITEM)
        //    //                        {
        //    //                            unitOfWork.OfflineinvoicedataitemRepository.Delete(invD);
        //    //                            unitOfWork.Save();
        //    //                        }
        //    //                        unitOfWork.OfflineinvoiceRepository.Delete(inv);
        //    //                        unitOfWork.Save();
        //    //                    }
        //    //                    unitOfWork.OfflineRepository.Delete(data);
        //    //                    unitOfWork.Save();
        //    //                }
        //    //        }
        //    //    }
        //    //}catch(Exception ex)
        //    //{}



        //old
        //    //LinkButton btnDelete = (LinkButton)sender;
        //    //int auditTrailId = Convert.ToInt32(btnDelete.CommandArgument);


        //    //List<GST_TRN_OFFLINE_INVOICE_AUDIT_TRAIL> auditTrail = unitOfWork.OfflineAudittrailRepository.Filter(f => f.AuditTrailID == auditTrailId).ToList();
        //    //if (auditTrail != null)
        //    //{
        //    //    var a = unitOfWork.OfflineRepository.Filter(f => f.AuditTrailID == auditTrailId).FirstOrDefault();

        //    //    foreach (var data in a)
        //    //    {
        //    //        unitOfWork.OfflineRepository.Delete(data);
        //    //        unitOfWork.Save();
        //    //    }
        //    //List<GST_TRN_OFFLINE_INVOICE> b = (unitOfWork.OfflineRepository.Filter(f => f.OfflineID == a).ToList());


        //    //foreach (var data in a)
        //    //{
        //    //    unitOfWork.OfflineRepository.Delete(data);
        //    //    unitOfWork.Save();
        //    //}
        //    // List<GST_TRN_OFFLINE_INVOICE_DATAITEM> c = unitOfWork.OfflineinvoicedataitemRepository.Filter(f => f.ValueID == Convert.ToInt64(b)).ToList();






        //    //List<GST_TRN_OFFLINE_INVOICE_DATAITEM> offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId).ToList();

        //    //int count = 0;
        //    //int ValueId;
        //    //foreach (var item in lv_B2CL.Items)
        //    //{
        //    //    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
        //    //    if (chk.Checked)
        //    //    {
        //    //        ValueId = Convert.ToInt32(lv_B2CL.DataKeys[item.DisplayIndex].Values["ValueId"].ToString());
        //    //        var OfflineObj = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == ValueId).SingleOrDefault();
        //    //        if (OfflineObj != null)
        //    //        {
        //    //            List<GST_TRN_OFFLINE_INVOICE_DATAITEM> offlinedataobj = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.ValueID == ValueId).ToList();
        //    //            foreach (var data in offlinedataobj)
        //    //            {
        //    //                unitOfwork.OfflineinvoicedataitemRepository.Delete(data);
        //    //                unitOfwork.Save();
        //    //            }

        //    //            unitOfwork.OfflineinvoiceRepository.Delete(OfflineObj);
        //    //            unitOfwork.Save();
        //    //            count++;
        //    //            if (count > 0)
        //    //            {
        //    //                uc_sucess.SuccessMessage = "Data Deleted Successfully.";
        //    //                uc_sucess.Visible = !String.IsNullOrEmpty(uc_sucess.SuccessMessage);
        //    //            }
        //    //        }
        //    //    }
        //    //}



        //    //old------------------------
        //    //foreach (GST_TRN_OFFLINE offLineInvoices in auditTrail.GST_TRN_OFFLINE)
        //    //{
        //    //    foreach (GST_TRN_OFFLINE_INVOICE inv in offLineInvoices.GST_TRN_OFFLINE_INVOICE)
        //    //    {
        //    //        foreach (GST_TRN_OFFLINE_INVOICE_DATAITEM item in inv.GST_TRN_OFFLINE_INVOICE_DATAITEM)
        //    //        {

        //    //            unitOfWork.OfflineinvoicedataitemRepository.Delete(item);
        //    //            unitOfWork.Save();
        //    //        }
        //    //        unitOfWork.OfflineinvoiceRepository.Delete(inv);
        //    //        unitOfWork.Save();
        //    //    }
        //    //    unitOfWork.OfflineRepository.Delete(offLineInvoices);
        //    //    unitOfWork.Save();
        //    //}
        //    //unitOfWork.OfflineAudittrailRepository.Delete(auditTrail);
        //    //unitOfWork.Save();

        //}

    }
}



