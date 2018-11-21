using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.Admin
{
    [Authorize("Admin")]
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
            UnitOfWork unitOfWork = new UnitOfWork();
            var loggedinUserId = Common.LoggedInUserID();

            if (loggedinUserId != null)
            {

                var invoices = unitOfWork.InvoiceRepository.Filter(f => f.Status == true).ToList();//f.CreatedBy==loggedinUserId &&

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
                    pieChartSpecialInvoiceDetail.PieChartValues.Add(barChart);
                }
            }
        }
        UnitOfWork unitOfWork = new UnitOfWork();
        public void getInvoices()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                var invoices = unitOfWork.InvoiceRepository.Filter(f => f.Status == true).Count();
                lblInvoices.Text = invoices.ToString();
            }
        }
        public void getSales()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                var sales = unitOfWork.SaleRegisterDataRepositry.Filter(f =>  f.Status == true).Count();
                lblSales.Text = sales.ToString();
            }
        }
        public void getUsers()
        {
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                var users = unitOfWork.UserSignatoryRepository.Filter(f =>  f.Status == true).Count();
                lblusers.Text = users.ToString();
            }
        }
        public void TotalItems()
        {

            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                //var items = unitOfWork.PurchaseDataRepositry.Filter(f => f.GST_MST_PURCHASE_REGISTER.Status == true).Select(c => c.Qty).Count();
                var items = unitOfWork.ItemRepository.Filter(f => f.UserId == loggedinUserId).Select(c => c.ItemCode).Count();
                lblItems.Text = items.ToString();
            }
        }
    }
}