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
    
    public partial class GST_MST_VENDOR
    {
        public GST_MST_VENDOR()
        {
            this.GST_MST_VENDOR_SERVICE = new HashSet<GST_MST_VENDOR_SERVICE>();
            this.GST_MST_VENDOR_TRANS_SHIPMENT = new HashSet<GST_MST_VENDOR_TRANS_SHIPMENT>();
            this.GST_TRN_INVOICE = new HashSet<GST_TRN_INVOICE>();
            this.GST_TRN_VENDOR_SERVICE = new HashSet<GST_TRN_VENDOR_SERVICE>();
        }
    
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string EmailID { get; set; }
        public string NameOfSignatory { get; set; }
        public string Designation { get; set; }
        public string GSTNNo { get; set; }
        public Nullable<byte> OwnerShip { get; set; }
        public Nullable<byte> StateID { get; set; }
        public string UserID { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual GST_MST_STATE GST_MST_STATE { get; set; }
        public virtual ICollection<GST_MST_VENDOR_SERVICE> GST_MST_VENDOR_SERVICE { get; set; }
        public virtual ICollection<GST_MST_VENDOR_TRANS_SHIPMENT> GST_MST_VENDOR_TRANS_SHIPMENT { get; set; }
        public virtual ICollection<GST_TRN_INVOICE> GST_TRN_INVOICE { get; set; }
        public virtual ICollection<GST_TRN_VENDOR_SERVICE> GST_TRN_VENDOR_SERVICE { get; set; }
    }
}
