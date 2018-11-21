using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_ITC : GST_TRN_ITC
    {
        UnitOfWork unitOfWork;

        public cls_ITC()
        {
            unitOfWork = new UnitOfWork();
        }

        private bool SaveITC(GST_TRN_ITC itcData)
        {
            try
            {
                unitOfWork.ITCRepository.Create(itcData);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }

       /// <summary>
       /// Get ALL ITC
       /// </summary>
       /// <returns></returns>
        public List<GST_TRN_ITC> GetITC()
        {
            List<GST_TRN_ITC> itcItems = new List<GST_TRN_ITC>();
            try
            {
                itcItems = unitOfWork.ITCRepository.All().ToList();
               
            }
            catch (Exception ex)
            { }
            return itcItems;
        }

        public List<GST_TRN_ITC> GetITC(string userID)
        {
            List<GST_TRN_ITC> itcItems = new List<GST_TRN_ITC>();
            try
            {
                itcItems = unitOfWork.ITCRepository.Filter(f =>f.GST_TRN_INVOICE.InvoiceStatus==(byte)EnumConstants.InvoiceStatus.Fresh && f.UserID == userID ).ToList();

            }
            catch (Exception ex)
            { }
            return itcItems;
        }

        public GST_TRN_ITC GetITC(Int64 itcID)
        {
           GST_TRN_ITC itcItem = new GST_TRN_ITC();
            try
            {
                itcItem = unitOfWork.ITCRepository.Find(f => f.ITC_ID == itcID);

            }
            catch (Exception ex)
            { }
            return itcItem;
        }

        /// <summary>
        /// Receiver SIDE ITC ADD/UPDATE
        /// </summary>
        /// <param name="invoiceData"></param>
        /// <returns></returns>
        public bool SaveItcReceiver(GST_TRN_INVOICE invoiceData)
        {
            GST_TRN_ITC item = new GST_TRN_ITC();
            item.Amount = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax);
            item.IGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.IGSTAmt);
            if (item.IGST > 0)
                item.TaxType = (byte)EnumConstants.TaxType.IGST;

            item.CGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.CGSTAmt);
            if (item.CGST > 0)
                item.TaxType = (byte)EnumConstants.TaxType.CGST;

            item.SGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.SGSTAmt);
            if (item.SGST > 0)
                item.TaxType = (byte)EnumConstants.TaxType.SGST;

            if (item.SGST.Value == 0)
            {
                item.TaxType = (byte)EnumConstants.TaxType.UTGST;
                item.SGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.UGSTAmt);
            }

            item.Cess = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.CessAmt);

            item.InvoiceID = invoiceData.InvoiceID;
            item.ITCStatus = (byte)EnumConstants.ITCStatus.Active;
            if (invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.Import || invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)
            {
                //item.UserID = invoiceData.ReceiverUserID;
                // item.ITCMovement = (byte)EnumConstants.ITCMovement.Debit;
            }
            else
            {
                item.UserID = invoiceData.ReceiverUserID;
                //item.ITCMovement = (byte)EnumConstants.ITCMovement.Credit;
                //item.UserID = invoiceData.ReceiverUserID;
            }
            if (invoiceData.ParentInvoiceID.HasValue)
                item.ITCParentID = null;

            item.ITCMovement = GetITCMovementReceiver(invoiceData.InvoiceSpecialCondition);
            item.ITCVoucherType = this.ITCVoucherType;
            item.ITCDate = DateTime.Now;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = invoiceData.CreatedBy;
            bool isSave = false;
            //   if (invoiceData.InvoiceSpecialCondition != (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM || invoiceData.InvoiceSpecialCondition.Value != (byte)EnumConstants.InvoiceSpecialCondition.JobWork)
            if (invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM )
            {
                isSave = SaveITC(item);
            }
            else if(invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.Regular)
            {
                isSave = SaveITC(item);
            }
            return isSave;
        }

        /// <summary>
        /// Seller SIDE ITC ADD/UPDATE
        /// </summary>
        /// <param name="invoiceData"></param>
        /// <returns></returns>
        public bool SaveItc(GST_TRN_INVOICE invoiceData)
        {
            GST_TRN_ITC item = new GST_TRN_ITC();
            item.Amount = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.TotalAmountWithTax);
            item.IGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.IGSTAmt);
            if (item.IGST>0)
                item.TaxType = (byte)EnumConstants.TaxType.IGST;

            item.CGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.CGSTAmt);
            if (item.CGST > 0)
                item.TaxType = (byte)EnumConstants.TaxType.CGST;

            item.SGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.SGSTAmt);
            if (item.SGST > 0)
                item.TaxType = (byte)EnumConstants.TaxType.SGST;

            if (item.SGST.Value == 0)
            {
                item.TaxType = (byte)EnumConstants.TaxType.UTGST;
                item.SGST = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.UGSTAmt);
            }

        
            item.InvoiceID = invoiceData.InvoiceID;
            item.ITCStatus = (byte)EnumConstants.ITCStatus.Active;
            if (invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.Import || invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges)
            {
                item.UserID = invoiceData.ReceiverUserID;
                // item.ITCMovement = (byte)EnumConstants.ITCMovement.Debit;
            }
            else
            {
                item.UserID = invoiceData.SellerUserID;
                //item.ITCMovement = (byte)EnumConstants.ITCMovement.Credit;
                //item.UserID = invoiceData.ReceiverUserID;
            }
            if (invoiceData.ParentInvoiceID.HasValue)
                item.ITCParentID = null;

            item.Cess = invoiceData.GST_TRN_INVOICE_DATA.Sum(s => s.CessAmt);
            item.ITCMovement = GetITCMovement(invoiceData.InvoiceSpecialCondition);
            item.ITCVoucherType = this.ITCVoucherType;
            item.ITCDate = DateTime.Now;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = invoiceData.CreatedBy;
            bool isSave=false;
         //   if (invoiceData.InvoiceSpecialCondition != (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM || invoiceData.InvoiceSpecialCondition.Value != (byte)EnumConstants.InvoiceSpecialCondition.JobWork)
            if (invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM || invoiceData.InvoiceSpecialCondition.Value == (byte)EnumConstants.InvoiceSpecialCondition.JobWork)
            {
              // isSave = SaveITC(item);
            }
            else
            {
                isSave = SaveITC(item);
            }
            return isSave;
        }

        private byte GetITCMovement(byte? invoiceType)
        {
            byte ITC;
            EnumConstants.InvoiceSpecialCondition invType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType.ToString());

            switch (invType)
            {
                case EnumConstants.InvoiceSpecialCondition.ReverseCharges:
                case EnumConstants.InvoiceSpecialCondition.Import:
                    ITC = (byte)EnumConstants.ITCMovement.Credit;
                    break;
                case EnumConstants.InvoiceSpecialCondition.Export://With payment of IGST                   
                case EnumConstants.InvoiceSpecialCondition.DeemedExport:
                case EnumConstants.InvoiceSpecialCondition.SEZDeveloper:
                case EnumConstants.InvoiceSpecialCondition.SEZUnit:
                case EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice:
                case EnumConstants.InvoiceSpecialCondition.Regular:
                case EnumConstants.InvoiceSpecialCondition.Advance:
                    ITC = (byte)EnumConstants.ITCMovement.Debit;
                    break;
                default:
                    ITC = 0;
                    break;
            }
            return ITC;

        }

        private byte GetITCMovementReceiver(byte? invoiceType)
        {
            byte ITC;
            EnumConstants.InvoiceSpecialCondition invType = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), invoiceType.ToString());

            switch (invType)
            {
                case EnumConstants.InvoiceSpecialCondition.ReverseCharges:
                case EnumConstants.InvoiceSpecialCondition.Import:
                case EnumConstants.InvoiceSpecialCondition.SupplierMissingInvoice:
                case EnumConstants.InvoiceSpecialCondition.RegularRCM://2A
                    ITC = (byte)EnumConstants.ITCMovement.Credit;
                    break;
                //case EnumConstants.InvoiceSpecialCondition.Export://With payment of IGST                   
                //case EnumConstants.InvoiceSpecialCondition.DeemedExport:
                //case EnumConstants.InvoiceSpecialCondition.SEZDeveloper:
                //case EnumConstants.InvoiceSpecialCondition.SEZUnit:
                case EnumConstants.InvoiceSpecialCondition.Regular://2A
                // when the seller recieves advance and assigns the vocher to an inovice then only , ti is eligible for itc
                //case EnumConstants.InvoiceSpecialCondition.Advance:                
                    ITC = (byte)EnumConstants.ITCMovement.Debit;
                    break;
                default:
                    ITC = 0;
                    break;
            }
            return ITC;

        }

        public static string ITCColor(object itcType)
        {

            string ITCColorBox;// = Convert.ToInt32(invoicID);
            EnumConstants.ITCMovement ITCMov = (EnumConstants.ITCMovement)Enum.Parse(typeof(EnumConstants.ITCMovement), itcType != null ? itcType.ToString() : "-1");
            switch (ITCMov)
            {
                case EnumConstants.ITCMovement.Credit:
                    ITCColorBox = "<span class='label label-success'>" + ITCMov.ToString() + "</span>";

                    break;
                case EnumConstants.ITCMovement.Debit:
                   ITCColorBox = "<span class='label label-primary'>" + ITCMov.ToString() + "</span>";
                    break;
                //case EnumConstants.NoteType.Refund:
                //    NoteColorBox = "<span class='label label-danger'>" + invAudit.ToDescription() + (!string.IsNullOrEmpty(invoiceAction) ? (" - " + invAction) : "") + "</span>";
                //    break;


                default:
                    ITCColorBox = "<span class='label label-danger'>-</span>"; ;
                    break;
            }

            return ITCColorBox;

        }
    }
}
