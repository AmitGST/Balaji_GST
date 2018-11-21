using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_PurchaseRegister : GST_MST_PURCHASE_REGISTER 
    {
       UnitOfWork unitOfWork;

     public string LoggedinUserID { get; set; }
       public cls_PurchaseRegister()
       {
           unitOfWork = new UnitOfWork();
       }

        /// <summary>
        /// Get one purchase register
        /// </summary>
        /// <param name="purchaseRegisterID"></param>
        /// <returns></returns>
       public GST_MST_PURCHASE_REGISTER GetPurchaseRegister(string purchaseRegisterID)
       {
           var id = Convert.ToInt64(purchaseRegisterID);
           var purchaseRegister = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.PurchageRegisterID == id);
           return purchaseRegister;
       }
       public  decimal GetLeftItemQty(int itemID,string userID)
       {
           var purchaseItem = unitOfWork.PurchaseDataRepositry.Filter(f => f.Item_ID == itemID).Sum(s => s.Qty); //f.GST_MST_PURCHASE_REGISTER.UserID==userID &&
           decimal? saleItem = unitOfWork.SaleRegisterDataRepositry.Filter(f => f.Id == userID && f.Item_ID == itemID).Sum(s => s.Qty);
           if (saleItem == null)
           {
               saleItem = 0;
           }
           decimal LeftQty = purchaseItem.Value - saleItem.Value;
         //  return purchaseItem.Value - saleItem.Value;
           return LeftQty;
       }


       public List<GST_TRN_INVOICE> GetSaleItemsInvoices(string userID)       
       {
           var items = unitOfWork.SaleRegisterDataRepositry.Filter(f => f.Id == userID && f.GST_TRN_INVOICE.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh).Select(s => s.GST_TRN_INVOICE).Distinct().ToList();
           //var parchaseData = unitOfWork.PurchaseRegisterDataRepositry.Filter(f => items.Contains(f.SupplierInvoiceNo)).ToList();
           return items;
       }

       public void GetSaleItems(string sellerInvoiceNo)
       {
           var item = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceNo == sellerInvoiceNo && f.InvoiceStatus == (byte)EnumConstants.InvoiceStatus.Fresh).ToList();
       }

       public bool SavePurchaseRegisterData(GST_MST_PURCHASE_DATA purchaseData)
       {
           try
           {
               unitOfWork.PurchaseDataRepositry.Create(purchaseData);
               unitOfWork.Save();
               return true;
           }
           catch (Exception ex)
           { return false; }
       }



       public bool SavePurchaseRegister(GST_MST_PURCHASE_REGISTER purchaseRegister)
       {
           try
           {

               unitOfWork.PurchaseRegisterDataRepositry.Create(purchaseRegister);

               unitOfWork.Save();
               return true;
           }
           catch (Exception ex)
           { return false; }
       }
       public bool SavePurchaseRegister(GST_MST_PURCHASE_DATA purchaseData)
       {
           try
           {

               unitOfWork.PurchaseDataRepositry.Create(purchaseData);

               unitOfWork.Save();
               return true;
           }
           catch (Exception ex)
           { return false; }
       }

       public bool SaveInvoiveDataInPurchaseRegister(GST_TRN_INVOICE invoice)
       {
           var prData = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.SupplierInvoiceNo == invoice.InvoiceNo);
           if (prData == null)
           {
               SavePurchaseRegister(Purchase_Register_Data(invoice));              
           }
           return true;
       }

       /// <summary>
       /// Update Invoice Data line items from purchase regsiter items update qty and others--this will execute in seller 1A
       /// </summary>
       /// <param name="invoice"></param>
       /// <returns></returns>
       public bool UpdateInvoiveDataFromPurchaseRegister(GST_TRN_INVOICE invoice)
       {
           List<GST_MST_PURCHASE_DATA> data = new List<GST_MST_PURCHASE_DATA>();
           var prData = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.SupplierInvoiceNo == invoice.InvoiceNo);
           foreach (GST_MST_PURCHASE_DATA pData in prData.GST_MST_PURCHASE_DATA)
           {
             
               //unitOfWork.PurchaseDataRepositry.Delete(pData);
           }
           unitOfWork.Save();

           foreach (GST_TRN_INVOICE_DATA invData in invoice.GST_TRN_INVOICE_DATA)
           {
               var item = PurchaseData(invData);
               item.PurchageRegisterID = prData.PurchageRegisterID;
               //SavePurchaseRegister(item);
           }

           return true;
       }

        /// <summary>
        /// Update purchase data items from invoice--check and update qty  --done--when ACCEPT Condition by receiver--in mismatch GSTR2
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
       public bool UpdatePurchaseDataItemFromInvoice(GST_TRN_INVOICE invoice)
       {          
           List<GST_MST_PURCHASE_DATA> data = new List<GST_MST_PURCHASE_DATA>();
           var prData = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.SupplierInvoiceNo == invoice.InvoiceNo);
           foreach (GST_MST_PURCHASE_DATA pData in prData.GST_MST_PURCHASE_DATA.ToList())
           {             
               unitOfWork.PurchaseDataRepositry.Delete(pData);
              
           }
           unitOfWork.Save();

           foreach (GST_TRN_INVOICE_DATA invData in invoice.GST_TRN_INVOICE_DATA)
           {
               var item=PurchaseData(invData);
               item.PurchageRegisterID=prData.PurchageRegisterID;
               SavePurchaseRegister(item);
           }
           return true;
       }

       
       //public bool UpdateInvoiceDataItemFromPurchaseRegsiter(GST_TRN_INVOICE invoice)
       //{
       //    var prData = unitOfWork.PurchaseRegisterDataRepositry.Find(f => f.SupplierInvoiceNo == invoice.InvoiceNo);
       //    if (prData != null)
       //    {
       //        foreach (GST_MST_PURCHASE_DATA data in prData.GST_MST_PURCHASE_DATA)
       //        {
       //            unitOfWork.PurchaseDataRepositry.Delete(data);
       //        }
       //        unitOfWork.Save();
       //    }

       //    UpdatePurchaseData(invoice);
       //    return true;
       //}

     

       private GST_MST_PURCHASE_REGISTER Purchase_Register_Data(GST_TRN_INVOICE invoice)
       {
               GST_MST_PURCHASE_REGISTER PR = new GST_MST_PURCHASE_REGISTER();
               if (invoice.InvoiceSpecialCondition==(byte)EnumConstants.InvoiceSpecialCondition.Import)
               { 
                PR.SellerName = SellerName;//invoice.AspNetUser.OrganizationName;
                PR.SellerAddress = SellerAddress;//invoice.AspNetUser.Address;
                PR.SellerGSTN = SellerGSTN;//invoice.AspNetUser.GSTNNo;
                PR.ReceiverName = ReceiverName;//invoice.AspNetUser1.OrganizationName;
                PR.ReceiverAddress = ReceiverAddress;//invoice.AspNetUser1.Address;
                PR.ConsigneeName = ConsigneeName;//invoice.AspNetUser2.OrganizationName;
                PR.ConsigneeAddress = ConsigneeAddress;//invoice.AspNetUser2.Address;
                PR.StateCode = StateCode;
               }
               else
               {
                   PR.SellerName = invoice.AspNetUser.OrganizationName;
                   PR.SellerAddress = invoice.AspNetUser.Address;
                   PR.SellerGSTN = invoice.AspNetUser.GSTNNo;
                   PR.ReceiverName = invoice.AspNetUser1.OrganizationName;
                   PR.ReceiverAddress = invoice.AspNetUser1.Address;
                   PR.ConsigneeName = invoice.AspNetUser2.OrganizationName;
                   PR.ConsigneeAddress = invoice.AspNetUser2.Address;
                   PR.StateCode = Convert.ToString(invoice.AspNetUser.StateCode);
               }
               PR.UserID = LoggedinUserID;
               PR.StockInwardDate = DateTime.Now;
               PR.StockOrderDate = invoice.InvoiceDate;
               PR.OrderPo = null;//TODO
               PR.OrderPoDate = null;//TODO
               PR.SupplierInvoiceNo = invoice.InvoiceNo;
               PR.SupplierInvoiceDate = invoice.InvoiceDate;
               PR.SupplierInvoiceMonth = invoice.InvoiceMonth;
               PR.Freight = invoice.Freight;
               PR.Insurance = invoice.Insurance;
               PR.PackingAndForwadingCharges = invoice.PackingAndForwadingCharges;
               PR.ElectronicReferenceNo = invoice.ElectronicReferenceNo;
               PR.ElectronicReferenceNoDate = invoice.ElectronicReferenceNoDate;
              //PR.StateID = getinvocdtls.
               PR.PurchaseStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
               PR.Status = true;

               PR.CreatedBy = invoice.CreatedBy;
               PR.CreatedDate = DateTime.Now;

               List<GST_MST_PURCHASE_DATA> data = new List<GST_MST_PURCHASE_DATA>();
               foreach (GST_TRN_INVOICE_DATA invData in invoice.GST_TRN_INVOICE_DATA)
               {
                   data.Add(PurchaseData(invData));
               }
               PR.GST_MST_PURCHASE_DATA = data;
               return PR;
       }



       private GST_MST_PURCHASE_DATA PurchaseData(GST_TRN_INVOICE_DATA itemData)
       {

           GST_MST_PURCHASE_DATA PD = new GST_MST_PURCHASE_DATA();
           //// PD.PurchaseDataID = TODO
           PD.LineID = Convert.ToString(itemData.LineID);
           PD.Item_ID = itemData.Item_ID;
           PD.Qty = itemData.Qty;
           PD.Rate = itemData.Rate;
           PD.TotalAmount = itemData.TotalAmount;
           PD.Discount = itemData.Discount;
           PD.TaxableAmount = itemData.TaxableAmount;
           PD.TotalAmountWithTax = itemData.TotalAmountWithTax;
           PD.IGSTRate = itemData.IGSTRate;
           PD.IGSTAmt = itemData.IGSTAmt;
           PD.CGSTRate = Convert.ToString(itemData.CGSTRate);
           PD.CGSTAmt = itemData.CGSTAmt;
           PD.SGSTRate = itemData.SGSTRate;
           PD.SGSTAmt = itemData.SGSTAmt;
           PD.UGSTRate = itemData.UGSTRate;
           PD.UGSTAmt = itemData.UGSTAmt;
           PD.CessRate = itemData.CessRate;
           PD.CGSTAmt = itemData.CGSTAmt;
           //PD.InvoiceDataStatus=TODO;
           PD.Status = itemData.Status;
           PD.CreatedBy = itemData.CreatedBy;
           PD.CreatedDate = DateTime.Now;

           return PD;
       }

       public bool SaleRegister(GST_TRN_INVOICE invoice)
       {
           foreach (GST_TRN_INVOICE_DATA item in invoice.GST_TRN_INVOICE_DATA)
           {
               var itemType = unitOfWork.ItemRepository.Find(f => f.Item_ID == item.Item_ID).ItemType;
               // var purchaseItems = unitOfWork.PurchaseDataRepositry.Filter(f => f.Status == true && item.Item_ID == f.Item_ID).Select(s => s.GST_MST_PURCHASE_REGISTER.UserID).ToList();
               if (itemType == (byte)EnumConstants.ItemType.HSN)
               {
                   //string uId = seller.SellerUserID;
                   //int iTem = Convert.ToInt32(item.Item_ID);
                   //decimal LeftQty = purchaseRegister.GetLeftItemQty(iTem, uId);

                   //if (LeftQty > item.Qty)
                   //{
                   GST_MST_SALE_REGISTER salRegister = new GST_MST_SALE_REGISTER();
                   salRegister.InvoiceID = invoice.InvoiceID;
                   salRegister.PerUnitRate = item.Rate;
                   salRegister.Item_ID = item.Item_ID;
                   salRegister.Qty = item.Qty;
                   salRegister.CreatedBy = invoice.CreatedBy;
                   salRegister.CreatedDate = DateTime.Now;
                   salRegister.Status = true;
                   salRegister.Id = invoice.SellerUserID;
                   salRegister.SaleStatus = (byte)EnumConstants.SaleStatus.Fresh;
                   unitOfWork.SaleRegisterDataRepositry.Create(salRegister);

                   //q }
               }
           }
           unitOfWork.Save();
           return true;
       }
    }
}
