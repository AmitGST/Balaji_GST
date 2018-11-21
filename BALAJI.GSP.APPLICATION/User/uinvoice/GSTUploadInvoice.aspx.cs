using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using businessAccessLayer;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic.Repositories;

namespace BALAJI.GSP.APPLICATION.User.uinvoice
{
    public partial class GSTUploadInvoice : System.Web.UI.Page
    {
        #region VARIABLE_DECLARETION
        string ENGSTIN = string.Empty;
        string strInvoiceType = "B2BInvoice";
        ExcelDB excelDB = new ExcelDB();
        DataTable dt = new DataTable();
        string strInvoiceNo = string.Empty;
        string strFlag = string.Empty;
        #endregion

        #region PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            DateTime now = DateTime.Now;
            var stratdt = new DateTime(now.Year, now.Month, 1);
            txtFromDate.Text = stratdt.ToString("dd-MM-yyyy");

            if (Request.QueryString["BI"] != null)
            {
                //strInvoiceType = excelDB.Decrypt(HttpUtility.UrlDecode(Request.QueryString["BI"]));
            }
            //Use this for GSTN no--Ashish remove GSTIN other query string
            ENGSTIN = Page.User.Identity.Name;
            if (!IsPostBack)
            {

                //lblSellerGSTINVal.Text = "Seller GSTIN : " + ENGSTIN;
                //lblGSTN.Text = ENGSTIN;
                BindGrid();
            }

        }
        #endregion

        protected void BindGrid()
        {
            //DataSet ds = new DataSet();
            //ds = ViewInvoice(lblGSTN.Text.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            //GVUploadInvoice.DataSource = ds.Tables[2];
            //GVUploadInvoice.DataBind();

            //GVAdvancePayment.DataSource = ds.Tables[5];
            //GVAdvancePayment.DataBind();

            //GVExport.DataSource = ds.Tables[8];
            //GVExport.DataBind();
        }

        #region ViewInvoice
        private DataSet ViewInvoice(string SellerGSTN, string FromDt, String Todate)
        {
            Seller seller = new Seller();
            seller.Reciever = new Reciever();
            seller.Consignee = new Consignee();
            int p = 0; int m = 0;
            DataTable dt = new DataTable();
            string strInvoiceNo = string.Empty;


            #region INVOICE_TYPE
            switch (strInvoiceType)
            {
                case "B2BInvoice":
                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                    seller.Invoice = b2bInvoice;
                    break;
                case "AmendedB2BInvoice":
                    Invoice AmendedB2BInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BInvoice);
                    seller.Invoice = AmendedB2BInvoice;
                    break;
                case "B2CLargeInvoice":
                    Invoice B2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2CLargeInvoice);
                    seller.Invoice = B2CLargeInvoice;
                    break;
                case "AmendedB2CLargeInvoice":
                    Invoice AmendedB2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2CLargeInvoice);
                    seller.Invoice = AmendedB2CLargeInvoice;
                    break;
                case "B2BCreditNotesInvoice":
                    Invoice B2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BCreditNotesInvoice);
                    seller.Invoice = B2BCreditNotesInvoice;
                    break;
                case "B2BDebitNotesInvoice":
                    Invoice B2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BDebitNotesInvoice);
                    seller.Invoice = B2BDebitNotesInvoice;
                    break;
                case "AmendedB2BCreditNotesInvoice":
                    Invoice AmendedB2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BCreditNotesInvoice);
                    seller.Invoice = AmendedB2BCreditNotesInvoice;
                    break;
                case "AmendedB2BDebitNotesInvoice":
                    Invoice AmendedB2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BDebitNotesInvoice);
                    seller.Invoice = AmendedB2BDebitNotesInvoice;
                    break;
                case "B2BExportInvoice":
                    Invoice B2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BExportInvoice);
                    seller.Invoice = B2BExportInvoice;
                    break;
                case "AmendedB2BExportInvoice":
                    Invoice AmendedB2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BExportInvoice);
                    seller.Invoice = AmendedB2BExportInvoice;
                    break;
            }
            #endregion

            seller.Invoice.LineEntry = new List<LineEntry>();
            LineEntry line;

            DataSet ds = new DataSet();


            ds = seller.Invoice.ViewInvoice(SellerGSTN, FromDt, Todate);

            seller.SellerDaTa = new List<Seller>();

            if (ds.Tables.Count > 0)
            {
                #region B2B
                if (ds.Tables.Contains("SellerDtls"))
                {
                    if (ds.Tables["SellerDtls"].Rows.Count > 0)
                    {
                        int j = ds.Tables[0].Rows.Count;

                        for (int i = 0; i <= j - 1; i++)
                        {
                            seller.SellerInvoice = (ds.Tables[0].Rows[i]["InvoiceNo"]).ToString();
                            seller.DateOfInvoice = (ds.Tables[0].Rows[i]["Invoicedate"]).ToString();
                            seller.GSTIN = (ds.Tables[0].Rows[i]["SellerGSTN"]).ToString();
                            seller.NameAsOnGST = (ds.Tables[0].Rows[i]["SellerName"]).ToString();
                            seller.Reciever.GSTIN = (ds.Tables[0].Rows[i]["ReceiverGSTN"]).ToString();
                            seller.Reciever.NameAsOnGST = (ds.Tables[0].Rows[i]["ReceiverName"]).ToString();
                            seller.Consignee.GSTIN = (ds.Tables[0].Rows[i]["ConsigneeGSTN"]).ToString();
                            seller.Consignee.NameAsOnGST = (ds.Tables[0].Rows[i]["ConsigneeName"]).ToString();
                            seller.Address = (ds.Tables[0].Rows[i]["SellerAddress"]).ToString();
                            seller.SellerStateCode = (ds.Tables[0].Rows[i]["SellerStateCode"]).ToString();
                            seller.SellerStateName = (ds.Tables[0].Rows[i]["SellerStateName"]).ToString();
                            seller.SellerStateCodeID = (ds.Tables[0].Rows[i]["SellerStateCodeID"]).ToString();
                            seller.Reciever.StateCode = (ds.Tables[0].Rows[i]["ReceiverStateCode"]).ToString();
                            seller.Consignee.StateCode = (ds.Tables[0].Rows[i]["ConsigneeStateCode"]).ToString();
                            seller.Invoice.Freight = Convert.ToInt32(ds.Tables[0].Rows[i]["Freight"]);
                            seller.Invoice.Insurance = Convert.ToInt32(ds.Tables[0].Rows[i]["Insurance"]);
                            seller.Invoice.PackingAndForwadingCharges = Convert.ToInt32(ds.Tables[0].Rows[i]["PackingAndForwadingCharges"]);

                            seller.SellerGrossTurnOver = Convert.ToDecimal(ds.Tables[0].Rows[i]["SellerGrossTurnOver"]);
                            seller.SellerFinancialPeriod = (ds.Tables[0].Rows[i]["SellerFinancialPeriod"]).ToString();
                            seller.Reciever.FinancialPeriod = (ds.Tables[0].Rows[i]["ReceiverFinancialPeriod"]).ToString();

                            seller.Reciever.StateName = (ds.Tables[0].Rows[i]["ReceiverStateName"]).ToString();
                            seller.Consignee.StateName = (ds.Tables[0].Rows[i]["ConsigneeStateName"]).ToString();


                            //if (((ds.Tables[0].Rows[i]["IsElectronicReferenceNoGenerated"]).ToString()) == "True")
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = true;
                            //}
                            //else
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = false;
                            //}


                            //seller.ElectronicReferenceNoGenerated = (ds.Tables[0].Rows[i]["ElectronicReferenceNoGenerated"]).ToString();
                            //seller.ElectronicReferenceNoGeneratedDate = (ds.Tables[0].Rows[i]["ElectronicReferenceNoGeneratedDate"]).ToString();

                            seller.Invoice.IsAdvancePaymentChecked = false;
                            seller.Invoice.IsExportChecked = false;


                            int n = ds.Tables[1].Rows.Count;
                            for (m = p; m <= n - 1; m++)
                            {
                                if ((ds.Tables[0].Rows[i]["InvoiceNo"]).ToString() == (ds.Tables[1].Rows[m]["InvoiceNo"]).ToString())
                                {
                                    line = new LineEntry();

                                    line.HSN = new com.B2B.GST.GSTInvoices.HSN();

                                    //InvoiceNo	Invoicedate	SellerGSTN	ReceiverGSTN	InvoiceSeed	LineID	Description	HSN	Qty	Unit	Rate	Total	Discount	
                                    //TaxableValue	AmountWithTax	IGSTRate	IGSTAmt	CGSTRate	CGSTAmt	SGSTRate	SGSTAmt	TotalQty	TotalRate	TotalAmount	TotalDiscount	
                                    //TotalTaxableAmount	TotalCGSTAmount	TotalSGSTAmount	TotalIGSTAmount	TotalAmountWithTax	GrandTotalAmount	GrandTotalAmountInWord	isHSNNilRated	
                                    //isHSNExempted	isHSNZeroRated	isHSNNonGSTGoods	isSACNilRated	isSACEcxempted	isSACZeroRated	isSACNonGSTService	IsNotifedGoods	IsNotifiedSAC	
                                    //IsElectronicReferenceNoGenerated	IsInter	SellerStateCode	SellerStateName	ReceiverStateCode	ReceiverStateName	ConsigneeStateCode	ConsigneeStateName
                                    line.LineID = Convert.ToInt32(ds.Tables[1].Rows[m]["LineID"]);
                                    line.HSN.Description = (ds.Tables[1].Rows[m]["Description"]).ToString();
                                    line.HSN.HSNNumber = (ds.Tables[1].Rows[m]["HSN"]).ToString();
                                    line.Qty = Convert.ToDecimal(ds.Tables[1].Rows[m]["Qty"]);
                                    line.HSN.UnitOfMeasurement = (ds.Tables[1].Rows[m]["Unit"]).ToString();
                                    line.PerUnitRate = Convert.ToDecimal(ds.Tables[1].Rows[m]["Rate"]);
                                    line.TotalLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["Total"]);
                                    line.Discount = Convert.ToDecimal(ds.Tables[1].Rows[m]["Discount"]);
                                    line.TaxValue = Convert.ToDecimal(ds.Tables[1].Rows[m]["TaxableValue"]);
                                    line.AmountWithTax = Convert.ToDecimal(ds.Tables[1].Rows[m]["AmountWithTax"]);
                                    line.HSN.RateIGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["IGSTRate"]);
                                    line.AmtIGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["IGSTAmt"]);
                                    line.HSN.RateCGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["CGSTRate"]);
                                    line.AmtCGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["CGSTAmt"]);
                                    line.HSN.RateSGST = Convert.ToDecimal(ds.Tables[1].Rows[m]["SGSTRate"]);
                                    line.AmtSGSTLineIDWise = Convert.ToDecimal(ds.Tables[1].Rows[m]["SGSTAmt"]);
                                    line.HSN.Cess = Convert.ToDecimal(ds.Tables[1].Rows[m]["Cess"]);

                                    seller.TotalQty = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalQty"]);
                                    seller.TotalRate = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalRate"]);
                                    seller.TotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalAmount"]);
                                    seller.TotalDiscount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalDiscount"]);
                                    seller.TotalTaxableAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalTaxableAmount"]);
                                    seller.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalCGSTAmount"]);
                                    seller.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalSGSTAmount"]);
                                    seller.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalIGSTAmount"]);
                                    seller.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[1].Rows[m]["TotalAmountWithTax"]);
                                    seller.GrandTotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[m]["GrandTotalAmount"]);
                                    seller.GrandTotalAmountInWord = Convert.ToString(ds.Tables[1].Rows[m]["GrandTotalAmountInWord"]);


                                    if (((ds.Tables[1].Rows[m]["IsInter"]).ToString()) == "True")
                                    {
                                        line.IsInter = true;
                                    }
                                    else
                                    {
                                        line.IsInter = false;
                                    }

                                    seller.Invoice.LineEntry.Add(line);
                                    p = 1 + p;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            seller.SellerDaTa.Add(seller);
                            Session["SellerDaTa"] = seller.SellerDaTa;

                            seller = new Seller();
                            seller.Reciever = new Reciever();
                            seller.Consignee = new Consignee();
                            seller.SellerDaTa = new List<Seller>();


                            #region INVOICE_TYPE
                            switch (strInvoiceType)
                            {
                                case "B2BInvoice":
                                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                                    seller.Invoice = b2bInvoice;
                                    break;
                                case "AmendedB2BInvoice":
                                    Invoice AmendedB2BInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BInvoice);
                                    seller.Invoice = AmendedB2BInvoice;
                                    break;
                                case "B2CLargeInvoice":
                                    Invoice B2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2CLargeInvoice);
                                    seller.Invoice = B2CLargeInvoice;
                                    break;
                                case "AmendedB2CLargeInvoice":
                                    Invoice AmendedB2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2CLargeInvoice);
                                    seller.Invoice = AmendedB2CLargeInvoice;
                                    break;
                                case "B2BCreditNotesInvoice":
                                    Invoice B2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BCreditNotesInvoice);
                                    seller.Invoice = B2BCreditNotesInvoice;
                                    break;
                                case "B2BDebitNotesInvoice":
                                    Invoice B2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BDebitNotesInvoice);
                                    seller.Invoice = B2BDebitNotesInvoice;
                                    break;
                                case "AmendedB2BCreditNotesInvoice":
                                    Invoice AmendedB2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BCreditNotesInvoice);
                                    seller.Invoice = AmendedB2BCreditNotesInvoice;
                                    break;
                                case "AmendedB2BDebitNotesInvoice":
                                    Invoice AmendedB2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BDebitNotesInvoice);
                                    seller.Invoice = AmendedB2BDebitNotesInvoice;
                                    break;
                                case "B2BExportInvoice":
                                    Invoice B2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BExportInvoice);
                                    seller.Invoice = B2BExportInvoice;
                                    break;
                                case "AmendedB2BExportInvoice":
                                    Invoice AmendedB2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BExportInvoice);
                                    seller.Invoice = AmendedB2BExportInvoice;
                                    break;
                            }
                            #endregion

                            seller.Invoice.LineEntry = new List<LineEntry>();

                            if ((List<Seller>)Session["SellerDaTa"] != null)
                                seller.SellerDaTa = (List<Seller>)Session["SellerDaTa"];


                        }

                    }
                }
                else
                {
                    //change master page here--Ashish
                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                    ////masterPage.ShowModalPopup();
                    ////masterPage.ErrorMessage = "System error occured during data population of SellerDtls table !!!";
                }
                #endregion

                #region ADVANCE
                if (ds.Tables.Contains("AdvanceSellerDtls"))
                {
                    p = 0; m = 0;
                    if (ds.Tables["AdvanceSellerDtls"].Rows.Count > 0)
                    {
                        int j = ds.Tables[3].Rows.Count;

                        for (int i = 0; i <= j - 1; i++)
                        {

                            seller.SellerInvoice = (ds.Tables[3].Rows[i]["VoucherNo"]).ToString();
                            seller.DateOfInvoice = (ds.Tables[3].Rows[i]["Voucherdate"]).ToString();
                            seller.GSTIN = (ds.Tables[3].Rows[i]["SellerGSTN"]).ToString();
                            seller.NameAsOnGST = (ds.Tables[3].Rows[i]["SellerName"]).ToString();
                            seller.Reciever.GSTIN = (ds.Tables[3].Rows[i]["ReceiverGSTN"]).ToString();
                            seller.Reciever.NameAsOnGST = (ds.Tables[3].Rows[i]["ReceiverName"]).ToString();
                            seller.Consignee.GSTIN = (ds.Tables[3].Rows[i]["ConsigneeGSTN"]).ToString();
                            seller.Consignee.NameAsOnGST = (ds.Tables[3].Rows[i]["ConsigneeName"]).ToString();
                            seller.Address = (ds.Tables[3].Rows[i]["SellerAddress"]).ToString();
                            seller.SellerStateCode = (ds.Tables[3].Rows[i]["SellerStateCode"]).ToString();
                            seller.SellerStateName = (ds.Tables[3].Rows[i]["SellerStateName"]).ToString();
                            seller.SellerStateCodeID = (ds.Tables[3].Rows[i]["SellerStateCodeID"]).ToString();
                            seller.Reciever.StateCode = (ds.Tables[3].Rows[i]["ReceiverStateCode"]).ToString();
                            seller.Consignee.StateCode = (ds.Tables[3].Rows[i]["ConsigneeStateCode"]).ToString();
                            seller.Invoice.Freight = Convert.ToInt32(ds.Tables[3].Rows[i]["Freight"]);
                            seller.Invoice.Insurance = Convert.ToInt32(ds.Tables[3].Rows[i]["Insurance"]);
                            seller.Invoice.PackingAndForwadingCharges = Convert.ToInt32(ds.Tables[3].Rows[i]["PackingAndForwadingCharges"]);

                            seller.Invoice.IsAdvancePaymentChecked = true;
                            seller.Invoice.IsExportChecked = false;

                            //if (((ds.Tables[3].Rows[i]["IsElectronicReferenceNoGenerated"]).ToString()) == "True")
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = true;
                            //}
                            //else
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = false;
                            //}


                            //seller.ElectronicReferenceNoGenerated = (ds.Tables[3].Rows[i]["ElectronicReferenceNoGenerated"]).ToString();
                            //seller.ElectronicReferenceNoGeneratedDate = (ds.Tables[3].Rows[i]["ElectronicReferenceNoGeneratedDate"]).ToString();

                            int n = ds.Tables[4].Rows.Count;
                            for (m = p; m <= n - 1; m++)
                            {
                                if ((ds.Tables[3].Rows[i]["VoucherNo"]).ToString() == (ds.Tables[4].Rows[m]["VoucherNo"]).ToString())
                                {
                                    line = new LineEntry();

                                    line.HSN = new com.B2B.GST.GSTInvoices.HSN();

                                    line.LineID = Convert.ToInt32(ds.Tables[4].Rows[m]["LineID"]);
                                    line.HSN.Description = (ds.Tables[4].Rows[m]["Description"]).ToString();
                                    line.HSN.HSNNumber = (ds.Tables[4].Rows[m]["HSN"]).ToString();
                                    line.Qty = Convert.ToDecimal(ds.Tables[4].Rows[m]["Qty"]);
                                    line.HSN.UnitOfMeasurement = (ds.Tables[4].Rows[m]["Unit"]).ToString();
                                    line.PerUnitRate = Convert.ToDecimal(ds.Tables[4].Rows[m]["Rate"]);
                                    line.TotalLineIDWise = Convert.ToDecimal(ds.Tables[4].Rows[m]["Total"]);
                                    line.Discount = Convert.ToDecimal(ds.Tables[4].Rows[m]["Discount"]);
                                    line.TaxValue = Convert.ToDecimal(ds.Tables[4].Rows[m]["TaxableValue"]);
                                    line.AmountWithTax = Convert.ToDecimal(ds.Tables[4].Rows[m]["AmountWithTax"]);
                                    line.HSN.RateIGST = Convert.ToDecimal(ds.Tables[4].Rows[m]["IGSTRate"]);
                                    line.AmtIGSTLineIDWise = Convert.ToDecimal(ds.Tables[4].Rows[m]["IGSTAmt"]);
                                    line.HSN.RateCGST = Convert.ToDecimal(ds.Tables[4].Rows[m]["CGSTRate"]);
                                    line.AmtCGSTLineIDWise = Convert.ToDecimal(ds.Tables[4].Rows[m]["CGSTAmt"]);
                                    line.HSN.RateSGST = Convert.ToDecimal(ds.Tables[4].Rows[m]["SGSTRate"]);
                                    line.AmtSGSTLineIDWise = Convert.ToDecimal(ds.Tables[4].Rows[m]["SGSTAmt"]);
                                    line.HSN.Cess = Convert.ToDecimal(ds.Tables[4].Rows[m]["Cess"]);



                                    seller.TotalQty = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalQty"]);
                                    seller.TotalRate = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalRate"]);
                                    seller.TotalAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalAmount"]);
                                    seller.TotalDiscount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalDiscount"]);
                                    seller.TotalTaxableAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalTaxableAmount"]);
                                    seller.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalCGSTAmount"]);
                                    seller.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalSGSTAmount"]);
                                    seller.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalIGSTAmount"]);
                                    seller.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[4].Rows[m]["TotalAmountWithTax"]);
                                    seller.GrandTotalAmount = Convert.ToDecimal(ds.Tables[4].Rows[m]["GrandTotalAmount"]);
                                    seller.GrandTotalAmountInWord = Convert.ToString(ds.Tables[4].Rows[m]["GrandTotalAmountInWord"]);


                                    seller.Invoice.LineEntry.Add(line);
                                    p = 1 + p;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            seller.SellerDaTa.Add(seller);
                            Session["SellerDaTa"] = seller.SellerDaTa;

                            seller = new Seller();
                            seller.Reciever = new Reciever();
                            seller.Consignee = new Consignee();
                            seller.SellerDaTa = new List<Seller>();


                            #region INVOICE_TYPE
                            switch (strInvoiceType)
                            {
                                case "B2BInvoice":
                                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                                    seller.Invoice = b2bInvoice;
                                    break;
                                case "AmendedB2BInvoice":
                                    Invoice AmendedB2BInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BInvoice);
                                    seller.Invoice = AmendedB2BInvoice;
                                    break;
                                case "B2CLargeInvoice":
                                    Invoice B2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2CLargeInvoice);
                                    seller.Invoice = B2CLargeInvoice;
                                    break;
                                case "AmendedB2CLargeInvoice":
                                    Invoice AmendedB2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2CLargeInvoice);
                                    seller.Invoice = AmendedB2CLargeInvoice;
                                    break;
                                case "B2BCreditNotesInvoice":
                                    Invoice B2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BCreditNotesInvoice);
                                    seller.Invoice = B2BCreditNotesInvoice;
                                    break;
                                case "B2BDebitNotesInvoice":
                                    Invoice B2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BDebitNotesInvoice);
                                    seller.Invoice = B2BDebitNotesInvoice;
                                    break;
                                case "AmendedB2BCreditNotesInvoice":
                                    Invoice AmendedB2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BCreditNotesInvoice);
                                    seller.Invoice = AmendedB2BCreditNotesInvoice;
                                    break;
                                case "AmendedB2BDebitNotesInvoice":
                                    Invoice AmendedB2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BDebitNotesInvoice);
                                    seller.Invoice = AmendedB2BDebitNotesInvoice;
                                    break;
                                case "B2BExportInvoice":
                                    Invoice B2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BExportInvoice);
                                    seller.Invoice = B2BExportInvoice;
                                    break;
                                case "AmendedB2BExportInvoice":
                                    Invoice AmendedB2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BExportInvoice);
                                    seller.Invoice = AmendedB2BExportInvoice;
                                    break;
                            }
                            #endregion

                            seller.Invoice.LineEntry = new List<LineEntry>();

                            if ((List<Seller>)Session["SellerDaTa"] != null)
                                seller.SellerDaTa = (List<Seller>)Session["SellerDaTa"];


                        }

                    }

                }
                else
                {
                    //UserInterface.loggedIn_applicationMasterPage masterPage = this.Master as UserInterface.loggedIn_applicationMasterPage;
                    ////masterPage.ShowModalPopup();
                    ////masterPage.ErrorMessage = "System error occured during data population of SellerDtls table !!!";
                }
                #endregion

                #region EXPORT
                if (ds.Tables.Contains("EXPORTSellerDtls"))
                {
                    p = 0; m = 0;
                    if (ds.Tables["EXPORTSellerDtls"].Rows.Count > 0)
                    {
                        int j = ds.Tables[6].Rows.Count;

                        for (int i = 0; i <= j - 1; i++)
                        {

                            seller.SellerInvoice = (ds.Tables[6].Rows[i]["ExportNo"]).ToString();
                            seller.DateOfInvoice = (ds.Tables[6].Rows[i]["Exportdate"]).ToString();
                            seller.GSTIN = (ds.Tables[6].Rows[i]["SellerGSTN"]).ToString();
                            seller.NameAsOnGST = (ds.Tables[6].Rows[i]["SellerName"]).ToString();
                            seller.Reciever.GSTIN = (ds.Tables[6].Rows[i]["ReceiverGSTN"]).ToString();
                            seller.Reciever.NameAsOnGST = (ds.Tables[6].Rows[i]["ReceiverName"]).ToString();
                            seller.Consignee.GSTIN = (ds.Tables[6].Rows[i]["ConsigneeGSTN"]).ToString();
                            seller.Consignee.NameAsOnGST = (ds.Tables[6].Rows[i]["ConsigneeName"]).ToString();
                            seller.Address = (ds.Tables[6].Rows[i]["SellerAddress"]).ToString();
                            seller.SellerStateCode = (ds.Tables[6].Rows[i]["SellerStateCode"]).ToString();
                            seller.SellerStateName = (ds.Tables[6].Rows[i]["SellerStateName"]).ToString();
                            seller.SellerStateCodeID = (ds.Tables[6].Rows[i]["SellerStateCodeID"]).ToString();
                            seller.Reciever.StateCode = (ds.Tables[6].Rows[i]["ReceiverStateCode"]).ToString();
                            seller.Consignee.StateCode = (ds.Tables[6].Rows[i]["ConsigneeStateCode"]).ToString();
                            seller.Invoice.Freight = Convert.ToInt32(ds.Tables[6].Rows[i]["Freight"]);
                            seller.Invoice.Insurance = Convert.ToInt32(ds.Tables[6].Rows[i]["Insurance"]);
                            seller.Invoice.PackingAndForwadingCharges = Convert.ToInt32(ds.Tables[6].Rows[i]["PackingAndForwadingCharges"]);


                            seller.Invoice.IsAdvancePaymentChecked = false;
                            seller.Invoice.IsExportChecked = true;

                            //if (((ds.Tables[6].Rows[i]["IsElectronicReferenceNoGenerated"]).ToString()) == "True")
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = true;
                            //}
                            //else
                            //{
                            //    seller.IsElectronicReferenceNoGenerated = false;
                            //}


                            //seller.ElectronicReferenceNoGenerated = (ds.Tables[6].Rows[i]["ElectronicReferenceNoGenerated"]).ToString();
                            //seller.ElectronicReferenceNoGeneratedDate = (ds.Tables[6].Rows[i]["ElectronicReferenceNoGeneratedDate"]).ToString();

                            int n = ds.Tables[7].Rows.Count;
                            for (m = p; m <= n - 1; m++)
                            {
                                if ((ds.Tables[6].Rows[i]["ExportNo"]).ToString() == (ds.Tables[7].Rows[m]["ExportNo"]).ToString())
                                {
                                    line = new LineEntry();

                                    line.HSN = new com.B2B.GST.GSTInvoices.HSN();
                                    line.LineID = Convert.ToInt32(ds.Tables[7].Rows[m]["LineID"]);
                                    line.HSN.Description = (ds.Tables[7].Rows[m]["Description"]).ToString();
                                    line.HSN.HSNNumber = (ds.Tables[7].Rows[m]["HSN"]).ToString();
                                    line.Qty = Convert.ToDecimal(ds.Tables[7].Rows[m]["Qty"]);
                                    line.HSN.UnitOfMeasurement = (ds.Tables[7].Rows[m]["Unit"]).ToString();
                                    line.PerUnitRate = Convert.ToDecimal(ds.Tables[7].Rows[m]["Rate"]);
                                    line.TotalLineIDWise = Convert.ToDecimal(ds.Tables[7].Rows[m]["Total"]);
                                    line.Discount = Convert.ToDecimal(ds.Tables[7].Rows[m]["Discount"]);
                                    line.TaxValue = Convert.ToDecimal(ds.Tables[7].Rows[m]["TaxableValue"]);
                                    line.AmountWithTax = Convert.ToDecimal(ds.Tables[7].Rows[m]["AmountWithTax"]);
                                    line.HSN.RateIGST = Convert.ToDecimal(ds.Tables[7].Rows[m]["IGSTRate"]);
                                    line.AmtIGSTLineIDWise = Convert.ToDecimal(ds.Tables[7].Rows[m]["IGSTAmt"]);
                                    line.HSN.RateCGST = Convert.ToDecimal(ds.Tables[7].Rows[m]["CGSTRate"]);
                                    line.AmtCGSTLineIDWise = Convert.ToDecimal(ds.Tables[7].Rows[m]["CGSTAmt"]);
                                    line.HSN.RateSGST = Convert.ToDecimal(ds.Tables[7].Rows[m]["SGSTRate"]);
                                    line.AmtSGSTLineIDWise = Convert.ToDecimal(ds.Tables[7].Rows[m]["SGSTAmt"]);
                                    line.HSN.Cess = Convert.ToDecimal(ds.Tables[7].Rows[m]["Cess"]);


                                    seller.TotalQty = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalQty"]);
                                    seller.TotalRate = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalRate"]);
                                    seller.TotalAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalAmount"]);
                                    seller.TotalDiscount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalDiscount"]);
                                    seller.TotalTaxableAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalTaxableAmount"]);
                                    seller.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalCGSTAmount"]);
                                    seller.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalSGSTAmount"]);
                                    seller.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalIGSTAmount"]);
                                    seller.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[7].Rows[m]["TotalAmountWithTax"]);
                                    seller.GrandTotalAmount = Convert.ToDecimal(ds.Tables[7].Rows[m]["GrandTotalAmount"]);
                                    seller.GrandTotalAmountInWord = Convert.ToString(ds.Tables[7].Rows[m]["GrandTotalAmountInWord"]);


                                    seller.Invoice.LineEntry.Add(line);
                                    p = 1 + p;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            seller.SellerDaTa.Add(seller);
                            Session["SellerDaTa"] = seller.SellerDaTa;

                            seller = new Seller();
                            seller.Reciever = new Reciever();
                            seller.Consignee = new Consignee();
                            seller.SellerDaTa = new List<Seller>();



                            #region INVOICE_TYPE
                            switch (strInvoiceType)
                            {
                                case "B2BInvoice":
                                    Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                                    seller.Invoice = b2bInvoice;
                                    break;
                                case "AmendedB2BInvoice":
                                    Invoice AmendedB2BInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BInvoice);
                                    seller.Invoice = AmendedB2BInvoice;
                                    break;
                                case "B2CLargeInvoice":
                                    Invoice B2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2CLargeInvoice);
                                    seller.Invoice = B2CLargeInvoice;
                                    break;
                                case "AmendedB2CLargeInvoice":
                                    Invoice AmendedB2CLargeInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2CLargeInvoice);
                                    seller.Invoice = AmendedB2CLargeInvoice;
                                    break;
                                case "B2BCreditNotesInvoice":
                                    Invoice B2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BCreditNotesInvoice);
                                    seller.Invoice = B2BCreditNotesInvoice;
                                    break;
                                case "B2BDebitNotesInvoice":
                                    Invoice B2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BDebitNotesInvoice);
                                    seller.Invoice = B2BDebitNotesInvoice;
                                    break;
                                case "AmendedB2BCreditNotesInvoice":
                                    Invoice AmendedB2BCreditNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BCreditNotesInvoice);
                                    seller.Invoice = AmendedB2BCreditNotesInvoice;
                                    break;
                                case "AmendedB2BDebitNotesInvoice":
                                    Invoice AmendedB2BDebitNotesInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BDebitNotesInvoice);
                                    seller.Invoice = AmendedB2BDebitNotesInvoice;
                                    break;
                                case "B2BExportInvoice":
                                    Invoice B2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BExportInvoice);
                                    seller.Invoice = B2BExportInvoice;
                                    break;
                                case "AmendedB2BExportInvoice":
                                    Invoice AmendedB2BExportInvoice = InvoiceFactory.CreateInvoice(InvoiceType.AmendedB2BExportInvoice);
                                    seller.Invoice = AmendedB2BExportInvoice;
                                    break;
                            }
                            #endregion

                            seller.Invoice.LineEntry = new List<LineEntry>();

                            if ((List<Seller>)Session["SellerDaTa"] != null)
                                seller.SellerDaTa = (List<Seller>)Session["SellerDaTa"];


                        }

                    }

                }
                else
                {
                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                    ////masterPage.ShowModalPopup();
                    ////masterPage.ErrorMessage = "System error occured during data population of SellerDtls table !!!";
                }
                #endregion


            }
            else
            {
                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                ////masterPage.ShowModalPopup();
                ////masterPage.ErrorMessage = "System Error !!!";
            }
            if (ds.Tables["SellerDtls"].Rows.Count == 0 && ds.Tables["AdvanceSellerDtls"].Rows.Count == 0 && ds.Tables["EXPORTSellerDtls"].Rows.Count == 0)
            {
                BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                ////masterPage.ShowModalPopup();
                ////masterPage.ErrorMessage = "No data to upload !!!";

                BtnUpload.Attributes.Add("style", "display:none");
            }

            return ds;
        }
        #endregion

        #region UPLOAD_ALL_INVOICES
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            strFlag = "all";
            UploadInvoice(strFlag, 0);

        }
        #endregion

        #region TO_UPLOAD_EACH_ROW
        protected void GVUploadInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UploadRow")
                {
                    // selIndex is the row ID of GridView.need to handle button visibility
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int selIndex = row.RowIndex;

                    strInvoiceNo = Convert.ToString(e.CommandArgument.ToString());
                    strFlag = "single";
                    UploadInvoice(strFlag, selIndex);
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        #endregion

        #region UploadInvoice
        protected void UploadInvoice(string strFlag, int selIndex)
        {
            try
            {
                Seller seller = new Seller();
                seller.SellerDaTa = new List<Seller>();

                //TEMPORARY SOLUTION
                Invoice b2bInvoice = InvoiceFactory.CreateInvoice(InvoiceType.B2BInvoice);
                seller.Invoice = b2bInvoice;

                seller.SellerDaTa = (List<Seller>)Session["SellerDaTa"];
                int result = seller.Invoice.UploadInvoice(seller.SellerDaTa, strInvoiceNo);

                if (result > 0)
                {
                    if (Session["SellerDaTa"] != null)
                        Session["SellerDaTa"] = null;

                    BindGrid();

                    if (strFlag == "all")
                    {
                        BtnUpload.Attributes.Add("style", "display:none");
                    }
                    else if (strFlag == "single")
                    {
                        // GVUploadInvoice.Rows[selIndex].FindControl("lnkUpload").Visible = false;
                    }
                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                    //  masterPage.ShowModalPopupSuccess();
                    ////masterPage.ErrorMessage = "Data Uploaded Successfully !!!";

                }
                else
                {
                    if (Session["SellerDaTa"] != null)
                        Session["SellerDaTa"] = null;
                    BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                    ////masterPage.ShowModalPopup();
                    ////masterPage.ErrorMessage = "Error !!! Unable to Upload Data!";

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
        #endregion

        #region BACK_TO_PREVIOUS_PAGE
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string GSTIN = HttpUtility.UrlEncode(excelDB.Encrypt(ENGSTIN));
            Response.Redirect("~/GSTInvoiceDashBoard.aspx", true);
        }
        #endregion
    }
}