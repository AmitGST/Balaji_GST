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
    
    public partial class GST_MST_USER_CURRENT_TURNOVER
    {
        public int CurrentTurnover_ID { get; set; }
        public string User_ID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<long> InvoiceID { get; set; }
        public Nullable<decimal> InvoiceAmountWithTax { get; set; }
        public Nullable<decimal> InvoiceAmountWithoutTax { get; set; }
        public Nullable<byte> TurnOverStatus { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual GST_TRN_INVOICE GST_TRN_INVOICE { get; set; }
    }
}
