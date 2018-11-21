using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_CreditDebit_Note : GST_TRN_CRDR_NOTE
    {
        UnitOfWork unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public cls_CreditDebit_Note()
        {
            unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemData"></param>
        /// <returns></returns>
        public bool SaveNote(GST_TRN_CRDR_NOTE itemData)
        {
            try
            {
                unitOfWork.CreditDebitNoteRepository.Create(itemData);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemData"></param>
        /// <returns></returns>
        public bool DeleteNote(GST_TRN_CRDR_NOTE itemData)
        {
            try
            {
                unitOfWork.CreditDebitNoteRepository.Delete(itemData);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemData"></param>
        /// <returns></returns>
        public bool UpdateNote(GST_TRN_CRDR_NOTE itemData)
        {
            try
            {
                unitOfWork.CreditDebitNoteRepository.Update(itemData);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public bool SaveNote(GST_TRN_INVOICE invoice)
        {
            SaveNote(RefectorNote(invoice));
            return true;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        private GST_TRN_CRDR_NOTE RefectorNote(GST_TRN_INVOICE invoice)
        {
            GST_TRN_CRDR_NOTE item = new GST_TRN_CRDR_NOTE();
            item.From_UserID = invoice.SellerUserID;
            item.To_UserID = invoice.ReceiverUserID;
            item.InvoiceID = invoice.InvoiceID;
            item.NoteTypeStatus =(byte)EnumConstants.NoteTypeStatus.Fresh;
            item.CDN_Date = DateTime.Now;
            item.NoteType = this.NoteType;
            item.Description = this.Description;
            string noteNo="";
            if((EnumConstants.NoteType)this.NoteType==EnumConstants.NoteType.Credit)
            {
                noteNo = "CR" + UniqueNoGenerate.RandomValueNote();
            }
            else if ((EnumConstants.NoteType)this.NoteType == EnumConstants.NoteType.Debit)
            {
                noteNo = "DR" + UniqueNoGenerate.RandomValueNote();
            }
            else            
            {
                noteNo = UniqueNoGenerate.RandomValueNote();
            }
            item.NoteNumber = noteNo;
            item.CreatedBy = invoice.SellerUserID;
            item.CreatedDate = DateTime.Now;

            List<GST_TRN_CRDR_NOTE_DATA> data = new List<GST_TRN_CRDR_NOTE_DATA>();
            foreach (GST_TRN_INVOICE_DATA invData in invoice.GST_TRN_INVOICE_DATA)
            {
                data.Add(ItemData(invData));
            }
            item.GST_TRN_CRDR_NOTE_DATA = data;

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemData"></param>
        /// <returns></returns>
        private GST_TRN_CRDR_NOTE_DATA ItemData(GST_TRN_INVOICE_DATA itemData)
        {
            GST_TRN_CRDR_NOTE_DATA item = new GST_TRN_CRDR_NOTE_DATA();
            //// PD.PurchaseDataID = TODO
            item.LineID = itemData.LineID;
            item.Item_ID = itemData.Item_ID;
            item.Qty = itemData.Qty;
            item.Rate = itemData.Rate;
            item.TotalAmount = itemData.TotalAmount;
            item.Discount = itemData.Discount;
            item.TaxableAmount = itemData.TaxableAmount;
            item.TotalAmountWithTax = itemData.TotalAmountWithTax;
            item.IGSTRate = itemData.IGSTRate;
            item.IGSTAmt = itemData.IGSTAmt;
            item.CGSTRate =itemData.CGSTRate;
            item.CGSTAmt = itemData.CGSTAmt;
            item.SGSTRate = itemData.SGSTRate;
            item.SGSTAmt = itemData.SGSTAmt;
            item.UGSTRate = itemData.UGSTRate;
            item.UGSTAmt = itemData.UGSTAmt;
            item.CessRate = itemData.CessRate;
            item.CGSTAmt = itemData.CGSTAmt;
            //PD.InvoiceDataStatus=TODO;
            item.Status = itemData.Status;
             //PD.CreatedBy = Common.LoggedInUserID();
            item.CreatedDate = DateTime.Now;

            return item;
        }

        public List<GST_TRN_CRDR_NOTE> GetCreditDebitNote()
        {
            List<GST_TRN_CRDR_NOTE> itcItems = new List<GST_TRN_CRDR_NOTE>();
            try
            {
                itcItems = unitOfWork.CreditDebitNoteRepository.All().ToList();

            }
            catch (Exception ex)
            { }
            return itcItems;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_CRDR_NOTE> GetCreditDebitNoteIssued(string userID)
        {
            List<GST_TRN_CRDR_NOTE> itcItems = new List<GST_TRN_CRDR_NOTE>();
            try
            {
               itcItems = unitOfWork.CreditDebitNoteRepository.Filter(f => f.From_UserID == userID).ToList();
            }
            catch (Exception ex)
            { }
            return itcItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_CRDR_NOTE> GetCreditDebitNoteReceived(string userID)
        {
            List<GST_TRN_CRDR_NOTE> itcItems = new List<GST_TRN_CRDR_NOTE>();
            try
            {
                itcItems = unitOfWork.CreditDebitNoteRepository.Filter(f => f.To_UserID == userID).ToList();
            }
            catch (Exception ex)
            { }
            return itcItems;
        }

        //public List<GST_TRN_CRDR_NOTE> GetCDNGroup(string userID)
        //{

        //    var item = unitOfWork.CreditDebitNoteRepository.Filter(f => f.From_UserID == userID).GroupBy(g => g.NoteType).ToList();
        //    return item;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></paramt>
        /// <returns></returns>
        public GST_TRN_CRDR_NOTE GetCreditDebitNote(Int64 itemID)
        {
            GST_TRN_CRDR_NOTE itcItem =new GST_TRN_CRDR_NOTE();
            try
            {
                itcItem = unitOfWork.CreditDebitNoteRepository.Find(f => f.CreditDebitID == itemID);
            }
            catch (Exception ex)
            { }
            return itcItem;
        }

        /// <summary>
        /// All invoices which is modified or Amendment in GSTR2A and immported in 1A  
        /// </summary>
        /// <returns></returns>
        public IDictionary GetModifiedInvoice(string userID)
        {
            var importInvoice = (byte)EnumConstants.InvoiceAuditTrailSatus.Import1A;
            IDictionary  listItem = new Dictionary<long, string>();
            //var importStatus = (byte)EnumConstants.InvoiceStatus.Fresh;
            var noteItem = unitOfWork.CreditDebitNoteRepository.Filter(f => f.From_UserID == userID).Select(s => s.InvoiceID).ToList();
            var modifiedInvoices = unitOfWork.InvoiceAuditTrailRepositry.Filter(f =>!noteItem.Contains(f.InvoiceID) && f.GST_TRN_INVOICE.SellerUserID==userID && f.AuditTrailStatus == importInvoice && f.GST_TRN_INVOICE.ParentInvoiceID != null).ToList().Distinct();
            if (modifiedInvoices != null)
            {
                KeyValuePair<long,string> kvp=new KeyValuePair<long,string>();
               // var item=modifiedInvoices.Select(d=>new {InvoiceID=d.GST_TRN_INVOICE.InvoiceID,InvoiceNo= d.GST_TRN_INVOICE.InvoiceNo}).ToList();
                foreach (GST_TRN_INVOICE_AUDIT_TRAIL trail in modifiedInvoices)
                {
                    if(!listItem.Contains(trail.GST_TRN_INVOICE.InvoiceID))
                    {
                        listItem.Add(trail.GST_TRN_INVOICE.InvoiceID, trail.GST_TRN_INVOICE.InvoiceNo);
                    }
                        // kvp.Key = trail.GST_TRN_INVOICE.InvoiceID;
                   // kvp.Value = trail.GST_TRN_INVOICE.InvoiceNo;
                }
              
                //if(listItem.Contains()
                //modifiedInvoices.ToDictionary(d => d.GST_TRN_INVOICE.InvoiceID, d => d.GST_TRN_INVOICE.InvoiceNo);
            }
           
            return listItem;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        public List<GST_TRN_INVOICE_DATA> MisMatchInvoice(string invoiceID)
        {
            Int64 invoiceId=Convert.ToInt64(invoiceID);
            var modifiedInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceId);
            List<GST_TRN_INVOICE_DATA> itemData = new List<GST_TRN_INVOICE_DATA>();
            if (modifiedInvoice.ParentInvoiceID.HasValue)
            {
                var compareInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == modifiedInvoice.ParentInvoiceID);
                
                var result = compareInvoice.GST_TRN_INVOICE_DATA.Select(s => new { s.Qty, s.Rate }).ToList().Except(modifiedInvoice.GST_TRN_INVOICE_DATA.Select(s => new { s.Qty, s.Rate }).ToList()).ToList();
                //Mismatch item whic r not match with item
                itemData = modifiedInvoice.GST_TRN_INVOICE_DATA.Where(b => compareInvoice.GST_TRN_INVOICE_DATA.Any(a => a.Qty != b.Qty && a.Item_ID==b.Item_ID)).ToList();
                
            }
            return itemData;
        }

        public static string CreditColor(object noteType)
        {
            
            string NoteColorBox;// = Convert.ToInt32(invoicID);
            EnumConstants.NoteType NoteType = (EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), noteType != null ? noteType.ToString() : "-1");
            switch (NoteType)
            {
                case EnumConstants.NoteType.Credit:
                    NoteColorBox = "<span class='label label-success'>" + NoteType.ToString() +"</span>";
                    
                    break;
                case EnumConstants.NoteType.Debit:
                    NoteColorBox = "<span class='label label-primary'>" + NoteType.ToString() +"</span>"; 
                    break;
                //case EnumConstants.NoteType.Refund:
                //    NoteColorBox = "<span class='label label-danger'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                //    break;
               
                
                default:
                    NoteColorBox  = "<span class='label label-danger'>-</span>"; ;
                    break;
            }

            return NoteColorBox;

        }

        /// <summary>
        /// For Get Data from GST_TRN_CRDR_NOTE_DATA table when click on edit button after v1.0
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GST_TRN_CRDR_NOTE_DATA> GetCreditDebitNoteData(string userID, decimal creditdebitID)
        {
            List<GST_TRN_CRDR_NOTE_DATA> CRDRDataItems = new List<GST_TRN_CRDR_NOTE_DATA>();
            try
            {
                CRDRDataItems = unitOfWork.CreditDebitNoteDataRepository.Filter(f => f.GST_TRN_CRDR_NOTE.From_UserID == userID && f.CreditDebitID == creditdebitID).ToList();
            }
            catch (Exception ex)
            { }
            return CRDRDataItems;
        }

    }
}
