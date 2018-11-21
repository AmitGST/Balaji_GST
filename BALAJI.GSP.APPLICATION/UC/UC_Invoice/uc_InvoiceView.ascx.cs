using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System.Linq.Expressions;
using GST.Utility;
using com.B2B.GST.GSTIN;

namespace BALAJI.GSP.APPLICATION.UC.UC_Invoice
{
    public partial class uc_InvoiceView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    
        public List<GST_TRN_INVOICE_DATA> _invoiceData;
        public List<GST_TRN_INVOICE_DATA> InvoiceData
        {
            get
            {
                return _invoiceData;
            }
            set
            {
                if (value != null)
                {
                    lstInvoiceR.DataSource = value;
                    lstInvoiceR.DataBind();

                    var invoiceType = value.Select(s => s.GST_TRN_INVOICE.InvoiceSpecialCondition).Distinct().FirstOrDefault();
                  
                   
                    lblTotalAmt.Text = value.Sum(s => s.TotalAmount).ToString();
                    lbltotaltaxvalue.Text = invoiceType !=(byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges ? value.Average(s => s.Discount).ToString() : value.Sum(s => s.TotalAmount).ToString();
                    lbltotalCGST.Text ="Total CGST"+Common.RCMTextBind(invoiceType)+":"+ value.Sum(s => s.CGSTAmt).ToString();
                    lbltotalSGST.Text = "Total SGST" + Common.RCMTextBind(invoiceType) + ":" + (value.Sum(s => s.SGSTAmt) + value.Sum(s => s.UGSTAmt)).ToString();
                    lbltotalIGST.Text = "Total IGST"+Common.RCMTextBind(invoiceType)+":"+ value.Sum(s => s.IGSTAmt).ToString();
                    lbltotalCess.Text = "Total Cess" + Common.RCMTextBind(invoiceType) + ":" + value.Sum(s => s.CessAmt).ToString();

                    //lbltotalfreightcharges.Text = value.Sum(s => s.Freight + s.GST_TRN_INVOICE.Insurance + s.GST_TRN_INVOICE.PackingAndForwadingCharges).ToString();
                    lblTotalAmtwithDis.Text =invoiceType !=(byte)EnumConstants.InvoiceSpecialCondition.ReverseCharges ? (value.Sum(s => s.TotalAmountWithTax)).ToString()
                        : ((value.Sum(s => s.TotalAmountWithTax) ) - (value.Sum(s => s.CGSTAmt) + value.Sum(s => s.SGSTAmt) + value.Sum(s => s.IGSTAmt) + value.Sum(s => s.CessAmt))).ToString();// + value.Sum(s => s.CessAmt) + value.Sum(s => s.CGSTAmt) + value.Sum(s => s.SGSTAmt) + value.Sum(s => s.IGSTAmt)+value.Sum(s=>s.UGSTAmt)).ToString();
                    
                }
            }
        }

        public string TotalTransitChargeAmount
        {
            set { lbltotalfreightcharges.Text = value; }
        }

        public HashSet<GST_TRN_INVOICE_DATA> _invoiceDataHash;
        public HashSet<GST_TRN_INVOICE_DATA> InvoiceDataHasSet
        {
            get
            {
                return _invoiceDataHash;
            }
            set
            {
                if (value != null)
                {
                    lstInvoiceR.DataSource = value;
                    lstInvoiceR.DataBind();

                    var invoiceType = value.Select(s => s.GST_TRN_INVOICE.InvoiceSpecialCondition).Distinct().FirstOrDefault();
                    var invoiceUsers = value.Select(s => s.GST_TRN_INVOICE).Distinct().FirstOrDefault();
                    //seller
                    liFrom.Text = invoiceUsers.AspNetUser.GSTNNo;
                    liorg.Text = invoiceUsers.AspNetUser.OrganizationName;
                    liAdd.Text = invoiceUsers.AspNetUser.Address;
                    //reciever
                    liFrom1.Text = invoiceUsers.AspNetUser1.GSTNNo;
                    liorg1.Text = invoiceUsers.AspNetUser1.OrganizationName;
                    liAdd1.Text = invoiceUsers.AspNetUser1.Address;
                    //consigne
                    liFrom2.Text = invoiceUsers.AspNetUser2.GSTNNo;
                    liorg2.Text = invoiceUsers.AspNetUser2.OrganizationName;
                    liAdd3.Text = invoiceUsers.AspNetUser2.Address;

                    liinvoiceno.Text = invoiceUsers.InvoiceNo;
                    lidate1.Text = Convert.ToDateTime(invoiceUsers.InvoiceDate).ToString("dd-MM-yyyy");

                    liPOSSeller.Text = invoiceUsers.AspNetUser.StateCode + "-" + Common.GetStateName(invoiceUsers.AspNetUser.StateCode);// invoiceUsers.AspNetUser.StateCode;
                    liPOSReceiver.Text = invoiceUsers.AspNetUser1.StateCode + "-" + Common.GetStateName(invoiceUsers.AspNetUser1.StateCode);// invoiceUsers.AspNetUser1.StateCode;
                    liPOSConsignee.Text = invoiceUsers.AspNetUser2.StateCode + "-" + Common.GetStateName(invoiceUsers.AspNetUser2.StateCode);
                    lblTotalAmt.Text = value.Sum(s => s.TotalAmount).ToString().Replace(".00", "");
                    lbltotaltaxvalue.Text = invoiceType != (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM ? value.Sum(s => s.TaxableAmount).ToString().Replace(".00", "") : value.Sum(s => s.TaxableAmount).ToString().Replace(".00", "");
                    litTotalCGST.Text = "Total CGST" + Common.RCMTextBind(invoiceType);
                    lbltotalCGST.Text =value.Sum(s => s.CGSTAmt).ToString().Replace(".00", "");
                    litTotalSGSTUTGST.Text = "Total SGST/UTGST" + Common.RCMTextBind(invoiceType);
                    lbltotalSGST.Text = (value.Sum(s => s.SGSTAmt) + value.Sum(s => s.UGSTAmt)).ToString().Replace(".00", "");
                    litTotalIGST.Text = "Total IGST" + Common.RCMTextBind(invoiceType);
                    lbltotalIGST.Text = value.Sum(s => s.IGSTAmt).ToString().Replace(".00", "");
                    litTotalCess.Text = "Total Cess" + Common.RCMTextBind(invoiceType);
                    lbltotalCess.Text =   value.Sum(s => s.CessAmt).ToString().Replace(".00", "");
                    //+ value.Select(s => s.GST_TRN_INVOICE.Freight).Distinct().FirstOrDefault()
                     
                    var frieght = value.Select(s => s.GST_TRN_INVOICE.Freight).Distinct().FirstOrDefault();
                    var Insurance = value.Select(s => s.GST_TRN_INVOICE.Insurance).Distinct().FirstOrDefault();
                    var PackingAndForwadingCharges = value.Select(s => s.GST_TRN_INVOICE.PackingAndForwadingCharges).Distinct().FirstOrDefault();

                    lbltotalfreightcharges.Text = (frieght + Insurance + PackingAndForwadingCharges).ToString().Replace(".00", "");
                    lblTotalAmtwithDis.Text = invoiceType != (byte)EnumConstants.InvoiceSpecialCondition.RegularRCM ? (value.Sum(s => s.TotalAmountWithTax)+ (frieght + Insurance + PackingAndForwadingCharges)).ToString().Replace(".00", "")
                        : ((value.Sum(s => s.TaxableAmount) + (frieght + Insurance + PackingAndForwadingCharges))).ToString().Replace(".00", "");// + value.Sum(s => s.CessAmt) + value.Sum(s => s.CGSTAmt) + value.Sum(s => s.SGSTAmt) + value.Sum(s => s.IGSTAmt)+value.Sum(s=>s.UGSTAmt)).ToString();
                    
                    
                    var otherSum = (from p in value
                                   select new
                                   {
                                       pck = p.GST_TRN_INVOICE.PackingAndForwadingCharges,
                                       inc = p.GST_TRN_INVOICE.Insurance,
                                       frght = p.GST_TRN_INVOICE.Freight
                                   }).Distinct().Sum(s=>s.frght+s.inc+s.pck);


                    lbltotalfreightcharges.Text = otherSum.ToString();// value.Sum(s => s.GST_TRN_INVOICE.Freight + s.GST_TRN_INVOICE.Insurance + s.GST_TRN_INVOICE.PackingAndForwadingCharges).ToString();

                  //  lblTotalAmtwithDis.Text = (value.Sum(s => s.TotalAmountWithTax) + otherSum).ToString();// + value.Sum(s => s.CessAmt) + value.Sum(s => s.CGSTAmt) + value.Sum(s => s.SGSTAmt) + value.Sum(s => s.IGSTAmt)+value.Sum(s=>s.UGSTAmt)).ToString();
                    
                }
            }
        }
        public decimal _quantityTotal = 0;
        protected void lstInvoiceR_PreRender(object sender, EventArgs e)
        {
           // ListView lvItems = (ListView)fvInvoice.FindControl("lstInvoiceR");
          //  ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
          //  Label lbl = (Label)item.FindControl("Label6");
          ////Label lbl =this.lstInvoiceR.FindControl("Label6") as Label;
          // // Label lbl = (sender as ListView).FindControl("Label6") as Label;
          //  lbl.Text = _quantityTotal.ToString();

        }
        //public string Da(HashSet<DataAccessLayer.GST_TRN_INVOICE_DATA> i)
        //{
        //    return "1";
        //}
        //public decimal? TotalAmt(dynamic Items)
        //{
        //    //var parameterExpression = Expression.Parameter(Type.GetType("DataAccessLayer.GST_TRN_INVOICE_DATA"), Items);

        //    var item = parameterExpression.Sum(s => s.TotalAmount);
        //    return item;

        //}
        public decimal? TotalAmtwithTax(HashSet<DataAccessLayer.GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.TotalAmountWithTax);
            return item;

        }
        public decimal? TotalDiscount(HashSet<DataAccessLayer.GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.Discount);
            return item;

        }
        public decimal? TotalCGST(List<GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.CGSTAmt);
            return item;
 
        }
        public decimal? TotalSGST(List<GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.SGSTAmt);
            return item;

        }
        public decimal? TotalIGST(List<GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.IGSTAmt);
            return item;

        }
        public decimal? TotalUGST(List<GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.UGSTAmt);
            return item;

        }
        public decimal? TotalCESS(List<GST_TRN_INVOICE_DATA> Items)
        {
            var item = Items.Sum(s => s.CessAmt);
            return item;

        }

        //public decimal? TotalCESS(List<GST_TRN_INVOICE_DATA> Items)
        //{
        //    var item = Items.Sum(s => s.);
        //    return item;

        //}
        protected void yourLiteral_DataBinding(object sender, EventArgs e)
        {
            //Literal lt = (Literal)(sender);
            //int quantity = (int)(Eval("IGSTAmt"));
            //_quantityTotal += quantity;
            //lt.Text = quantity.ToString();
        }
        protected void lstInvoiceR_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    //ListView lvItems = (ListView)sender.FindControl("lstInvoiceR");
                    Label lbl = e.Item.FindControl("lblIGSTAmt") as Label;
                    // _quantityTotal +=Convert.ToDecimal(lbl.Text);
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
           
           
        }
    }
}