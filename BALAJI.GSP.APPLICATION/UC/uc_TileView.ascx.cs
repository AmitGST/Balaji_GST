using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GST.Utility;


namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_TileView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void BindTypeInvoices()
        {
            //var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedBy == loggedinUserId && f.InvoiceStatus == 0 && f.CreatedDate.Value.Month == SelectedMonth && f.Status == true).OrderByDescending(o => o.CreatedDate).ToList();
            //rptInvoices.DataSource = invoices.ToList();
            //rptInvoices.DataBind();
        }

        public List<GST_TRN_INVOICE> InvoiceList
        {
            set
            {
                var data = from item in value                          
                           group item by new { item.InvoiceType,item.InvoiceSpecialCondition } into g
                           select new
                           {
                               InvoiceType=g.Key.InvoiceType,
                               INVSPLCONDITION = g.Key.InvoiceSpecialCondition,
                               TotalInvoice = g.Count(),
                               //QTY = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds=>ds.Qty)),
                               TAXABLEAMOUNT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.TaxableAmount)),
                               IGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.IGSTAmt)),
                               CGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CGSTAmt)),
                               SGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.SGSTAmt)),
                               CESSAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CessAmt))
                         };
                rptInvoices.DataSource = data.ToList();// invoiceSum.ToList();
                rptInvoices.DataBind();
            }
        }
        public EventHandler Info_Click;
        protected void lbinfo_Click(object sender, EventArgs e)
        {
            Info_Click(sender, e);
        }
        public string CommandNameB2B
        {
            get
            {
                return ViewState["CommandName"].ToString();
            }
            set
            {
                ViewState["CommandName"] = value;
            }
        }
    }
}