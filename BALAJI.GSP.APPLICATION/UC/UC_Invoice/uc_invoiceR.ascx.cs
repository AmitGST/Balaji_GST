using com.B2B.GST.GSTIN;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_invoiceR : System.Web.UI.UserControl
    {
        //Seller seller; 
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (SellerData != null)
            //{
            //    List<Seller> sell = new List<Seller>();
            //    sell.Add(SellerData);
            //    rptInvoice.DataSource = sell;
            //    rptInvoice.DataBind();
            //}
        }

        public Seller SellerData
        {
            get
            {
                return SellerData;
            }
            set
            {
                if (value != null)
                {
                    List<Seller> sell = new List<Seller>();
                    sell.Add(value);
                    rptInvoice.DataSource = sell;
                    rptInvoice.DataBind();
                    //Seller s = new Seller();
                    //s.Invoice.InvoiceSpecialCondition
                    //var item = (from s in sell
                    //            select new
                    //            {
                    //                fre = s.Invoice.Freight,
                    //                pck = s.Invoice.PackingAndForwadingCharges,
                    //                inc = s.Invoice.Insurance
                    //            }).Sum(s => s.fre + s.pck + s.inc);


                }
            }
        }

       
    }
}