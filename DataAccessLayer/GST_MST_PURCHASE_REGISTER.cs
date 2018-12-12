//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class GST_MST_PURCHASE_REGISTER
    {
        public GST_MST_PURCHASE_REGISTER()
        {
            this.GST_MST_PURCHASE_DATA = new HashSet<GST_MST_PURCHASE_DATA>();
        }
    
        public long PurchageRegisterID { get; set; }
        public string UserID { get; set; }
        public string StateCode { get; set; }
        public string SellerGSTN { get; set; }
        public string SellerName { get; set; }
        public string SellerAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public Nullable<System.DateTime> StockInwardDate { get; set; }
        public Nullable<System.DateTime> StockOrderDate { get; set; }
        public string OrderPo { get; set; }
        public Nullable<System.DateTime> OrderPoDate { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public Nullable<System.DateTime> SupplierInvoiceDate { get; set; }
        public Nullable<byte> SupplierInvoiceMonth { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public Nullable<decimal> Insurance { get; set; }
        public Nullable<decimal> PackingAndForwadingCharges { get; set; }
        public string ElectronicReferenceNo { get; set; }
        public Nullable<System.DateTime> ElectronicReferenceNoDate { get; set; }
        public Nullable<byte> PurchaseStatus { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> ActiveFrom { get; set; }
        public Nullable<System.DateTime> ActiveTo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<GST_MST_PURCHASE_DATA> GST_MST_PURCHASE_DATA { get; set; }
    }
}
