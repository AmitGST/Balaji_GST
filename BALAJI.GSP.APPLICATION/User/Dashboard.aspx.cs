using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.User
{


    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPieType();
            getInvoices();
            getSales();
            getUsers();
            TotalItems();
        }
        
        private void BindPieType()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var loggedinUserId = Common.LoggedInUserID();

                if (loggedinUserId != null)
                {

                    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.Status == true && f.CreatedBy == loggedinUserId).ToList();//f.CreatedBy==loggedinUserId &&

                    //decimal[] de = new decimal[] { 2 };
                    //AjaxControlToolkit.BarChartSeries barChart = new AjaxControlToolkit.BarChartSeries();
                    //barChart.Name = "B2B";
                    //barChart.Data = de;
                    //barChartInvoiveTypeCount.Series.Add(barChart); decimal[] de1 = new decimal[] { 5 };
                    //AjaxControlToolkit.BarChartSeries barChart1 = new AjaxControlToolkit.BarChartSeries();
                    //barChart1.Name = "B2C";
                    //barChart1.Data = de1;
                    //barChartInvoiveTypeCount.Series.Add(barChart1);
                    foreach (EnumConstants.InvoiceType inv in Enum.GetValues(typeof(EnumConstants.InvoiceType)))
                    {
                        // barChartInvoiveTypeCount.CategoriesAxis = inv.ToDescription();
                        List<decimal> itemData = new List<decimal>();
                        var item = invoices.Where(w => w.InvoiceType == (byte)inv).Count();
                        itemData.Add(Convert.ToDecimal(item));
                        AjaxControlToolkit.BarChartSeries barChart = new AjaxControlToolkit.BarChartSeries();
                        barChart.Name = inv.ToDescription();
                        barChart.Data = itemData.ToArray();

                        if (item > 0)
                            barChartInvoiveTypeCount.Series.Add(barChart);
                    }

                    foreach (EnumConstants.InvoiceSpecialCondition inv in Enum.GetValues(typeof(EnumConstants.InvoiceSpecialCondition)))
                    {

                        //List<decimal> itemData = new List<decimal>();
                        var item = invoices.Where(w => w.InvoiceSpecialCondition == (byte)inv).Count();
                        // itemData.Add(Convert.ToDecimal(item));
                        AjaxControlToolkit.PieChartValue barChart = new AjaxControlToolkit.PieChartValue();
                        barChart.Category = inv.ToDescription();
                        barChart.Data = Convert.ToDecimal(item);
                        if (item > 0)
                            pieChartSpecialInvoiceDetail.PieChartValues.Add(barChart);
                    }
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        UnitOfWork unitOfWork = new UnitOfWork();
        public void getInvoices()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedBy == loggedinUserId && f.Status == true).Count();
                    lblInvoices.Text = invoices.ToString();
                }
            }
            catch(Exception ex){
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void getSales()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    // 
                    //var invoice = unitOfWork.InvoiceRepository.Filter(f => f.InvoiceUserID == loggedinUserId).Select(c => c.InvoiceID).ToList();
                    ////var invoiceD = unitOfWork.InvoiceDataRepository.Filter(f => f.InvoiceID == invoice).ToList();
                    //var salestotal = unitOfWork.SaleRegisterDataRepositry.Filter(f => f.Id == loggedinUserId && f.Status == true).Select(c => c.GST_TRN_INVOICE.GST_TRN_INVOICE_DATA.Sum(d => d.TaxableAmount.Value));
                    //var sales = unitOfWork.SaleRegisterDataRepositry.Filter(f => f.Id == loggedinUserId && f.Status == true ).Count();
                    var sales = unitOfWork.SaleRegisterDataRepositry.Filter(f => f.Status == true).Count();
                    lblSales.Text = sales.ToString();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void getUsers()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    var users = unitOfWork.UserSignatoryRepository.Filter(f => f.CreatedBy == loggedinUserId && f.Status == true).Count();
                    lblusers.Text = users.ToString();
                }
            }
            catch (Exception ex) {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public void TotalItems()
        {
            try
            {
                var loggedinUserId = Common.LoggedInUserID();
                if (loggedinUserId != null)
                {
                    //var items = unitOfWork.PurchaseDataRepositry.Filter(f => f.GST_MST_PURCHASE_REGISTER.UserID == loggedinUserId && f.GST_MST_PURCHASE_REGISTER.Status == true).Select(c => c.Qty).Count();
                    var items = unitOfWork.ItemRepository.Filter(f => f.UserId == loggedinUserId).Select(c => c.ItemCode).Count();
                    lblItems.Text = items.ToString();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}