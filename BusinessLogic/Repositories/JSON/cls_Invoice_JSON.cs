using Newtonsoft.Json;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using eSignASPLib;
//using eSignASPLib.DTO;

using GST.Utility;
using GSTN.API.GSTR1;
//using GSTN.API.GSTR1;

namespace BusinessLogic.Repositories.GSTN
{
    public class cls_Invoice_JSON
    {

        //public List<B2bOutward> b2b { get; set; }
        ////public List<B2bAOutward> b2ba { get; set; }
        ////public List<B2clOutward> b2cl { get; set; }
        ////public List<B2clAOutward> b2cla { get; set; }
        ////public List<B2csOutward> b2cs { get; set; }
        ////public List<B2CSAOutward> b2csa { get; set; }
        ////public NilRatedOutward nil { get; set; }
        ////public List<Exp> exp { get; set; }
        ////public List<ExpA> expa { get; set; }
        ////public List<CDNAOutward> cdna { get; set; }
        ////public List<AtOutward> at { get; set; }
        ////public List<AtAOutward> ata { get; set; }
        ////public List<CdnOutward> cdn { get; set; }
        ////public List<TxpOutward> txpd { get; set; }
        ////public List<EComOutward> ecom_invocies { get; set; }
        UnitOfWork unitOfWork = new UnitOfWork();

        private string GetNameFromGSTIN(string GSTIN)
        {
            return unitOfWork.AspnetRepository.Filter(x => x.GSTNNo == GSTIN).Select(x => x.FirstName + " " + x.LastName).SingleOrDefault();
        }

        public string GetInvoiceJSONData(List<GST_TRN_INVOICE> invoicelist)
        {
            string finalJson = string.Empty;
            List<B2bOutward> b2b = new List<B2bOutward>();
            List<B2bAOutward> b2ba = new List<B2bAOutward>();
            List<B2clOutward> b2cl = new List<B2clOutward>();
            List<B2clAOutward> b2cla= new List<B2clAOutward>();
            List<B2csOutward> b2cs = new List<B2csOutward>();
            List<B2CSAOutward> b2csa = new List<B2CSAOutward>();
            GSTR1Total model = new GSTR1Total();
            foreach (var invoice in invoicelist)
            {
                model.gstin = invoice.AspNetUser.GSTNNo;
                model.gt += Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmount));
                model.cur_gt += Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
                model.fp = invoice.InvoiceMonth.ToString() + invoice.GST_MST_FINYEAR.Finyear;
                if (invoice.InvoiceType == (byte)EnumConstants.InvoiceType.B2B)
                {
                    if (invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh)
                    {
                        model.b2b = GetB2BJson(b2b, invoice);
                    }
                    else if(invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Amended)
                    {
                        model.b2ba = GetB2BAJson(b2ba, invoice);
                     }
                }
                else if(invoice.InvoiceType == (byte)EnumConstants.InvoiceType.B2C)
                {
                    if (invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CL)
                    {
                        if (invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh)
                        {
                            model.b2cl = GetB2CLJson(b2cl, invoice);
                     }
                        else if (invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Amended)
                        {
                            model.b2cla = GetB2CLAJson(b2cla, invoice);
                     }
                    }
                    //else if(invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.B2CS)
                    //{
                    //    if (invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh)
                    //    {
                    //        finalJson += GetB2CSJson(b2cs, model, invoice);
                    //    }
                    //    else if (invoice.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Amended)
                    //    {
                    //        finalJson += GetB2CSAJson(b2csa, model, invoice);
                    //    }
                    //}
                }   
            }
            finalJson += JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented,
                         new JsonSerializerSettings
                         {
                             NullValueHandling = NullValueHandling.Ignore
                         });
                        
            return finalJson;
        }

        private List<B2bOutward> GetB2BJson(List<B2bOutward> b2b, GST_TRN_INVOICE invoice)
        {
            string b2bJson = string.Empty;
           
            B2bOutward b2bItem = new B2bOutward();
            b2bItem.ctin = invoice.AspNetUser2.GSTNNo;//.ConsigneeUserID;
            B2BInv inv = new B2BInv();
            inv.inum = invoice.InvoiceNo;
            inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
            inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
            inv.pos = invoice.TaxBenefitingState;
            inv.rchrg = "N";//TODO:need to change
            inv.inv_typ = "R";//
            //TODO:need to change and update according to count according to in invoice item. 
            List<B2Bitem> lineItems = new List<B2Bitem>();
            foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
            {
                B2Bitem invItem = new B2Bitem();
                invItem.num = itm.LineID.Value;
                ItmDet itemDetail = new ItmDet();
                itemDetail.rt = Convert.ToDouble(itm.Rate);
                itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
                itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
                itemDetail.camt = Convert.ToDouble(itm.CGSTAmt);
                itemDetail.samt = Convert.ToDouble(itm.SGSTAmt);
                invItem.itm_det = itemDetail;
                lineItems.Add(invItem);
            }
            inv.itms = lineItems;
            List<B2BInv> invList = new List<B2BInv>();
            invList.Add(inv);

            b2bItem.inv = invList;
            b2b.Add(b2bItem);
            return b2b;
         
        }

        private List<B2bAOutward> GetB2BAJson(List<B2bAOutward> b2ba, GST_TRN_INVOICE invoice)
        {
            string b2bJson = string.Empty;

            B2bAOutward b2baItem = new B2bAOutward();
            b2baItem.ctin = invoice.AspNetUser2.GSTNNo;//.ConsigneeUserID;
            B2BAInv inv = new B2BAInv();
            inv.inum = invoice.InvoiceNo;
            inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
            inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
            inv.pos = invoice.TaxBenefitingState;
            inv.rchrg = "N";//TODO:need to change
            inv.inv_typ = "R";//
            //TODO:need to change and update according to count according to in invoice item. 
            List<B2Bitem> lineItems = new List<B2Bitem>();
            foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
            {
                B2Bitem invItem = new B2Bitem();
                invItem.num = itm.LineID.Value;
                ItmDet itemDetail = new ItmDet();
                itemDetail.rt = Convert.ToDouble(itm.Rate);
                itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
                itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
                itemDetail.camt = Convert.ToDouble(itm.CGSTAmt);
                itemDetail.samt = Convert.ToDouble(itm.SGSTAmt);
                invItem.itm_det = itemDetail;
                lineItems.Add(invItem);
            }
            inv.itms = lineItems;
            List<B2BAInv> invList = new List<B2BAInv>();
            invList.Add(inv);

            b2baItem.inv = invList;
            b2ba.Add(b2baItem);
            return b2ba;
        }

        private List<B2clOutward> GetB2CLJson(List<B2clOutward> b2cl, GST_TRN_INVOICE invoice)
        {
            string b2cJson = string.Empty;
            

            B2clOutward b2clItem = new B2clOutward();
            B2CLInv inv = new B2CLInv();
            inv.inum = invoice.InvoiceNo;
            inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
            inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
            inv.pos = invoice.TaxBenefitingState;
            //inv.chksum = invoice.
            //inv.cname = GetNameFromGSTIN(invoice.ReceiverUserID);
            //inv.etin =
            //inv.itms = 
            //inv.prs =
            //TODO:need to change and update according to count according to in invoice item. 
            List<B2CLitem> lineItems = new List<B2CLitem>();
            foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
            {
                B2CLitem invItem = new B2CLitem();
                invItem.num = itm.LineID.Value;
                B2CLItmDet itemDetail = new B2CLItmDet();
                itemDetail.irt = Convert.ToDouble(itm.Rate);
                itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
                itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
                itemDetail.csamt = Convert.ToDouble(itm.CGSTAmt);
                itemDetail.cssrt = Convert.ToDouble(itm.CessRate);
                itemDetail.irt = Convert.ToDouble(itm.IGSTRate);
                itemDetail.hsn_sc = Convert.ToString(itm.Item_ID);
                //itemDetail.ty = itm.
                invItem.itm_det = itemDetail;
                lineItems.Add(invItem);
            }
            inv.itms = lineItems;
            List<B2CLInv> invList = new List<B2CLInv>();
            invList.Add(inv);
            b2clItem.inv = invList;
            b2cl.Add(b2clItem);
            return b2cl;
        }

        private List<B2clAOutward> GetB2CLAJson(List<B2clAOutward> b2cl, GST_TRN_INVOICE invoice)
        {
            string b2cJson = string.Empty;
           

            B2clAOutward b2clItem = new B2clAOutward();
            B2CLAInv inv = new B2CLAInv();
            inv.inum = invoice.InvoiceNo;
            inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
            inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
            inv.pos = invoice.TaxBenefitingState;
            //inv.chksum = invoice.
            //inv.cname = GetNameFromGSTIN(invoice.ReceiverUserID);
            //inv.etin =
            //inv.itms = 
            //inv.prs =

            //TODO:need to change and update according to count according to in invoice item. 
            List<B2CLitem> lineItems = new List<B2CLitem>();
            foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
            {
                B2CLitem invItem = new B2CLitem();
                invItem.num = itm.LineID.Value;
                B2CLItmDet itemDetail = new B2CLItmDet();
                itemDetail.irt = Convert.ToDouble(itm.Rate);
                itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
                itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
                itemDetail.csamt = Convert.ToDouble(itm.CGSTAmt);
                itemDetail.cssrt = Convert.ToDouble(itm.CessRate);
                itemDetail.irt = Convert.ToDouble(itm.IGSTRate);
                itemDetail.hsn_sc = Convert.ToString(itm.Item_ID);
                //itemDetail.ty
                invItem.itm_det = itemDetail;
                lineItems.Add(invItem);
            }
            inv.itms = lineItems;
            List<B2CLAInv> invList = new List<B2CLAInv>();
            invList.Add(inv);
            b2clItem.inv = invList;
            b2cl.Add(b2clItem);
            return b2cl;
        }
             
        //private string GetB2CSJson(List<B2csOutward> b2cs, GSTR1Total model, GST_TRN_INVOICE invoice)
        //{
        //    string b2cJson = string.Empty;
        //    model.gstin = invoice.AspNetUser.GSTNNo;
        //    model.gt = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmount));
        //    model.cur_gt = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
        //    model.fp = invoice.InvoiceMonth.ToString() + invoice.GST_MST_FINYEAR.Finyear;

        //    B2csOutward b2csitem = new B2csOutward();
        //    B2CLAInv inv = new B2CLAInv();
        //    inv.inum = invoice.InvoiceNo;
        //    inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
        //    inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
        //    inv.pos = invoice.TaxBenefitingState;
        //    //inv.chksum = invoice.
        //    //inv.cname = GetNameFromGSTIN(invoice.ReceiverUserID);
        //    //inv.etin =
        //    //inv.itms = 
        //    //inv.prs =

        //    //TODO:need to change and update according to count according to in invoice item. 
        //    List<B2CLitem> lineItems = new List<B2CLitem>();
        //    foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
        //    {
        //        B2CLitem invItem = new B2CLitem();
        //        invItem.num = itm.LineID.Value;
        //        B2CLItmDet itemDetail = new B2CLItmDet();
        //        itemDetail.irt = Convert.ToDouble(itm.Rate);
        //        itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
        //        itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
        //        itemDetail.csamt = Convert.ToDouble(itm.CGSTAmt);
        //        itemDetail.cssrt = Convert.ToDouble(itm.CessRate);
        //        itemDetail.irt = Convert.ToDouble(itm.IGSTRate);
        //        itemDetail.hsn_sc = Convert.ToString(itm.Item_ID);
        //        invItem.itm_det = itemDetail;
        //        lineItems.Add(invItem);
        //    }
        //    inv.itms = lineItems;
        //    List<B2CLAInv> invList = new List<B2CLAInv>();
        //    invList.Add(inv);
        //    b2csitem.inv = invList;
        //    b2cs.Add(b2clItem);
        //    model.b2cla = b2cl;
        //    b2cJson = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented,
        //              new JsonSerializerSettings
        //              {
        //                  NullValueHandling = NullValueHandling.Ignore
        //              });
        //    return b2cJson;
        //}

        //private string GetB2CSAJson(List<B2CSAOutward> b2csa, GSTR1Total model, GST_TRN_INVOICE invoice)
        //{
        //    string b2cJson = string.Empty;
        //    model.gstin = invoice.AspNetUser.GSTNNo;
        //    model.gt = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmount));
        //    model.cur_gt = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
        //    model.fp = invoice.InvoiceMonth.ToString() + invoice.GST_MST_FINYEAR.Finyear;

        //    B2CSAOutward b2clItem = new B2CSAOutward();
        //    B2CSInv inv = new B2CSInv();
        //    inv.inum = invoice.InvoiceNo;
        //    inv.idt = invoice.InvoiceDate.Value.ToString("dd-MM-yyyy");
        //    inv.val = Convert.ToDouble(invoice.GST_TRN_INVOICE_DATA.Sum(s => s.TaxableAmount));
        //    inv.pos = invoice.TaxBenefitingState;
        //    //inv.chksum = invoice.
        //    //inv.cname = GetNameFromGSTIN(invoice.ReceiverUserID);
        //    //inv.etin =
        //    //inv.itms = 
        //    //inv.prs =

        //    //TODO:need to change and update according to count according to in invoice item. 
        //    List<B2CSitem> lineItems = new List<B2CSitem>();
        //    foreach (GST_TRN_INVOICE_DATA itm in invoice.GST_TRN_INVOICE_DATA)
        //    {
        //        B2CSitem invItem = new B2CSitem();
        //        invItem.num = itm.LineID.Value;
        //        B2CSItmDet itemDetail = new B2CSItmDet();
        //        itemDetail.irt = Convert.ToDouble(itm.Rate);
        //        itemDetail.txval = Convert.ToDouble(itm.TaxableAmount);
        //        itemDetail.iamt = Convert.ToDouble(itm.IGSTAmt);
        //        itemDetail.csamt = Convert.ToDouble(itm.CGSTAmt);
        //        itemDetail.cssrt = Convert.ToDouble(itm.CessRate);
        //        itemDetail.irt = Convert.ToDouble(itm.IGSTRate);
        //        itemDetail.hsn_sc = Convert.ToString(itm.Item_ID);
        //        invItem.itm_det = itemDetail;
        //        lineItems.Add(invItem);
        //    }
        //    inv.itms = lineItems;
        //    List<B2CSInv> invList = new List<B2CSInv>();
        //    invList.Add(inv);
        //    b2csItem.inv = invList;
        //    b2cs.Add(b2csItem);
        //    model.b2cl = b2cs;
        //    b2cJson = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented,
        //              new JsonSerializerSettings
        //              {
        //                  NullValueHandling = NullValueHandling.Ignore
        //              });
        //    return b2cJson;
        //}

   }
}
