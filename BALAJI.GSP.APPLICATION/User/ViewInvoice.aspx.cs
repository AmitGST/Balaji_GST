using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using com.B2B.GST.LoginModule;
using System.Text.RegularExpressions;
using com.B2B.GST.GSTIN;
using com.B2B.GST.GSTInvoices;
using com.B2B.GST.ExcelFunctionality;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic.Repositories;
using GST.Utility;
using DataAccessLayer;
using System.Net;
using System.Globalization;

namespace BALAJI.GSP.APPLICATION.User.uinvoice
{
    public partial class ViewInvoice : System.Web.UI.Page
    {
        ExcelDB excelDB = new ExcelDB();
        string flag = string.Empty;
        string UniqueNo = string.Empty;
        string strSupplyType = string.Empty;
        string SellerGSTIN = string.Empty;
        DataSet ds = new DataSet();

        UnitOfWork unitOfWork = new UnitOfWork();
        GST_TRN_INVOICE_AUDIT_TRAIL audittrail = new GST_TRN_INVOICE_AUDIT_TRAIL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllInvoices();
             //   SellerGSTIN = Common.UserProfile.GSTNNo;
             //   //No need now --Ashish
             //   //if (Request.QueryString["DR"] != null)
             //   //{
             //   //    SellerGSTIN = excelDB.Decrypt(HttpUtility.UrlDecode(Request.QueryString["DR"]));
             //   //}
             //  
             ////  SellerGSTIN = "33GSPTN0231G1ZM";
             //   Session["GSTN"] = SellerGSTIN;
             //   ddlInvoiceNo.Items.Insert(0, new ListItem("- Select Invoice -", string.Empty));
             //   ddlInvoiceNo.SelectedIndex = 0;
            }
        }

        //Get All Invoice related to seller--Ashish
        private void BindAllInvoices()
        {
          
            var loggedinUserId = Common.LoggedInUserID();
            if (loggedinUserId != null)
            {
                var invoices = unitOfWork.InvoiceRepository.Filter(f => f.CreatedBy == loggedinUserId && f.Status == true).OrderByDescending(o => o.InvoiceDate).ToList();
                //foreach(gst_ )
                //audittrail.InvoiceID=
                //audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Upload);
                //audittrail.UserIP = myIP;
                //audittrail.InvoiceID = invoiceid;
                //audittrail.CreatedDate = DateTime.Now;
                //audittrail.CreatedBy = Common.LoggedInUserID(); ;
                //unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                //unitOfWork.Save();

                lvInvoices.DataSource = invoices.ToList();
                lvInvoices.DataBind();
                
            }
        }
        private void getItem(dynamic ii)
        {
 
        }
        protected void lvInvoices_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindAllInvoices();
            DataPager1.DataBind();
        }

        protected void lkb_action_Click(object sender, EventArgs e)
        {

            LinkButton lkbItem = (LinkButton)sender;

            if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
            {              
                Int64 invoiceId=Convert.ToInt64(lkbItem.CommandArgument.ToString());
                var invoice = unitOfWork.InvoiceDataRepository.Filter(f => f.InvoiceID == invoiceId).ToList();

               uc_InvoiceView.InvoiceData = invoice;

                // uc_InvoiceView.SellerData = invoice;
            }

        }


        /// <summary>
        /// Commented all code below here --ashish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgFromDate_Click(object sender, ImageClickEventArgs e)
        {
            //calFromDt.Visible = true;
        }
        protected void calFromDt_SelectionChanged(object sender, EventArgs e)
        {
            //txtFromDt.Text = calFromDt.SelectedDate.ToShortDateString();
            //calFromDt.Visible = false;
        }
        protected void imgToDt_Click(object sender, ImageClickEventArgs e)
        {
           // calToDt.Visible = true;
        }
        protected void calToDt_SelectionChanged(object sender, EventArgs e)
        {
           // txtToDt.Text = calToDt.SelectedDate.ToShortDateString();
           // calToDt.Visible = false;
          ///  SellerGSTIN = (string)Session["GSTN"];
            populateInvoiceddl(SellerGSTIN, txtFromDt.Text, txtToDt.Text);
        }

        public void populateInvoiceddl(string SellerGSTIN, string Fromdt, string Todt)
        {
            ds = excelDB.populateInvoiceddl(SellerGSTIN, Fromdt, Todt);

            if (ds.Tables.Count > 0)
            {
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlInvoiceNo.DataSource = ds.Tables[0];
                    ddlInvoiceNo.DataTextField = "InvoiceNo";
                    ddlInvoiceNo.DataValueField = "InvoiceNo";
                    ddlInvoiceNo.DataBind();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[1].Rows[0][0]);
                    lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[1].Rows[0][1]);

                   
                    ddlInvoiceNo.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
                    ddlInvoiceNo.SelectedIndex = 0;
                }
            }
        }


        protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UniqueNo = ddlInvoiceNo.SelectedValue;
            BindGrid(UniqueNo);
        }


        private void BindGrid(string UniqueNo)
        {
            ds = excelDB.FetchInvoicePreviewData(UniqueNo, "B");

            #region B2B
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables.Contains("SellerDtls"))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //  // lblInvoiceNumber.Text = "Serial No. of Invoice : " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                        //  //  lblInvoiceDate.Text = "Invoice Date : " + Convert.ToString(ds.Tables[0].Rows[0][1]);
                        //  lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[0].Rows[0][2]);
                        //  lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[0].Rows[0][3]);
                        //  //  lblSellerAddress.Text = "Address : " + Convert.ToString(ds.Tables[0].Rows[0][4]);
                        ////  hdnSellerGSTN.Value = Convert.ToString(ds.Tables[0].Rows[0][2]);

                        lblFreight.Text = "Freight : " + Convert.ToString(ds.Tables[0].Rows[0][14]);
                        lblInsurance.Text = "Insurance : " + Convert.ToString(ds.Tables[0].Rows[0][15]);
                        lblPackingAndForwadingCharges.Text = "PackingAndForwadingCharges : " + Convert.ToString(ds.Tables[0].Rows[0][16]);


                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of SellerDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("ReceiverDtls"))
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblRecieverGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[1].Rows[0][1]);
                        lblRecieverName.Text = "Name : " + Convert.ToString(ds.Tables[1].Rows[0][2]);
                        lblRecieverAddress.Text = "Address : " + Convert.ToString(ds.Tables[1].Rows[0][3]);
                        lblRecieverStateCode.Text = "State Code : " + Convert.ToString(ds.Tables[1].Rows[0][6]);
                        lblReceiverState.Text = "State : " + Convert.ToString(ds.Tables[1].Rows[0][5]);

                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of ReceiverDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("ConsigneeDtls"))
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblConsigneeGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[2].Rows[0][1]);
                        lblConsigneeName.Text = "Name : " + Convert.ToString(ds.Tables[2].Rows[0][2]);
                        lblConsigneeAddress.Text = "Address : " + Convert.ToString(ds.Tables[2].Rows[0][3]);
                        lblConsigneeStateCode.Text = "State Code : " + Convert.ToString(ds.Tables[2].Rows[0][6]);
                        lblConsigneeState.Text = "State : " + Convert.ToString(ds.Tables[2].Rows[0][5]);
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of ConsigneeDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("InvoiceDtls"))
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        int k = ds.Tables[3].Rows.Count;

                        for (int i = 0; i <= k - 1; i++)
                        {
                            HtmlTableRow tRow = new HtmlTableRow();
                            for (int j = 0; j <= 14; j++)
                            {
                                HtmlTableCell tb = new HtmlTableCell();
                                tb.InnerText = Convert.ToString(ds.Tables[3].Rows[i][j]);
                                tRow.Controls.Add(tb);
                            }
                            tblPreview.Rows.Add(tRow);
                        }

                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of InvoiceDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("AmountDtls"))
                {
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        HtmlTableRow tRow = new HtmlTableRow();
                        for (int j = 0; j <= 14; j++)
                        {
                            HtmlTableCell tb = new HtmlTableCell();
                            tb.InnerText = Convert.ToString(ds.Tables[4].Rows[0][j]);
                            tRow.Controls.Add(tb);
                        }
                        tblPreview.Rows.Add(tRow);
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of AmountDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("AmountInWords"))
                {
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        AmtInFigureVal.Text = Convert.ToString(ds.Tables[5].Rows[0][0]);
                        AmtInWordsVal.Text = Convert.ToString(ds.Tables[5].Rows[0][1]);
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of AmountInWords table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }

            }
            else
            {
                //lblMsg.Text = "System Error !!!";
                //this.InvoicePreviewModalPopupExtender.Show();
                //return;
            }
            #endregion

        }

        #region BACK_TO_PREVIOUS_PAGE
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //No need to send here gstn no--Ashish
            SellerGSTIN = (string)Session["GSTN"];
             SellerGSTIN = HttpUtility.UrlEncode(excelDB.Encrypt(SellerGSTIN));
            Response.Redirect("~/GSTInvoiceDashBoard.aspx", true);
        }
        #endregion

        protected void btnGetInvoice_Click(object sender, EventArgs e)
        {
            populateInvoiceddl(Page.User.Identity.Name, txtFromDt.Text.Trim(), txtToDt.Text.Trim());
        }

        

        private Seller BindInvoice(DataSet dsInvoice)
        {          
            Seller obj = new Seller();
           // Invoice inv = new Invoice();
            obj.CreateInvoice(obj, InvoiceType.B2BInvoice.ToString());
            #region B2B
            if (dsInvoice.Tables.Count > 0)
            {
                if (dsInvoice.Tables.Contains("SellerDtls"))
                {
                    if (dsInvoice.Tables[0].Rows.Count > 0)
                    {
                        //  // lblInvoiceNumber.Text = "Serial No. of Invoice : " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                        //  //  lblInvoiceDate.Text = "Invoice Date : " + Convert.ToString(ds.Tables[0].Rows[0][1]);
                        //  lblSellerGSTIN.Text = "GSTIN : " + Convert.ToString(ds.Tables[0].Rows[0][2]);
                        //  lblSellerName.Text = "Name : " + Convert.ToString(ds.Tables[0].Rows[0][3]);
                        //  //  lblSellerAddress.Text = "Address : " + Convert.ToString(ds.Tables[0].Rows[0][4]);
                        ////  hdnSellerGSTN.Value = Convert.ToString(ds.Tables[0].Rows[0][2]);
                        obj.SellerInvoice = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        obj.DateOfInvoice = Convert.ToString(ds.Tables[0].Rows[0][1]);

                        obj.GSTIN = Convert.ToString(ds.Tables[0].Rows[0][2]);
                        obj.NameAsOnGST = Convert.ToString(ds.Tables[0].Rows[0][3]);
                        obj.Address = Convert.ToString(ds.Tables[0].Rows[0][8]);
                       // obj.SellerStateCode = Convert.ToString(ds.Tables[0].Rows[0][6]);
                        obj.SellerStateName = Convert.ToString(ds.Tables[0].Rows[0][10]);
                    

                        obj.Invoice.Freight = Convert.ToDecimal(ds.Tables[0].Rows[0][14]);
                        obj.Invoice.PackingAndForwadingCharges = Convert.ToDecimal(ds.Tables[0].Rows[0][16]);
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of SellerDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("ReceiverDtls"))
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        Reciever rec = new Reciever();
                        rec.GSTIN = Convert.ToString(ds.Tables[1].Rows[0][1]);
                        rec.NameAsOnGST = Convert.ToString(ds.Tables[1].Rows[0][2]);
                        rec.Address = Convert.ToString(ds.Tables[1].Rows[0][3]);
                        rec.StateCode = Convert.ToString(ds.Tables[1].Rows[0][6]);
                        rec.StateName = Convert.ToString(ds.Tables[1].Rows[0][5]);
                        obj.Reciever = rec;
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of ReceiverDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("ConsigneeDtls"))
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        Consignee con = new Consignee();
                        con.GSTIN = Convert.ToString(ds.Tables[2].Rows[0][1]);
                        con.NameAsOnGST = Convert.ToString(ds.Tables[2].Rows[0][2]);
                        con.Address = Convert.ToString(ds.Tables[2].Rows[0][3]);
                        con.StateCode = Convert.ToString(ds.Tables[2].Rows[0][6]);
                        con.StateName = Convert.ToString(ds.Tables[2].Rows[0][5]);
                        obj.Consignee = con;
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of ConsigneeDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("InvoiceDtls"))
                {
                    //LineID,Description,HSN,Qty,Unit,Rate,Total,Discount,AmountWithTax,CGSTRate,CGSTAmt,SGSTRate,SGSTAmt,IGSTRate,IGSTAmt
                    DataTable dt=ds.Tables["InvoiceDtls"];
                    HSN hsn = new HSN();
                    var leItem = (from item in dt.AsEnumerable()
                                              select new LineEntry
                                              {
                                                  LineID =Convert.ToInt32( item.Field<string>("LineID")),
                                                  //HSN=item..Select(new HSN{Description})
                                                  //HSN = new HSN().Description = item.Field<string>("Description"),                                     
                                                  //HSN = new HSN().HSNNumber = item.Field<string>("HSN"),
                                                  HSN = ConvertItemToHSN(item),
                                                  Qty = Convert.ToInt32(item.Field<string>("Qty")),
                                                 //Unit = Convert.ToInt32(item.Field<string>("Unit")),
                                                   PerUnitRate = Convert.ToDecimal(item.Field<string>("Rate")),
                                                  TotalLineIDWise = Convert.ToDecimal(item.Field<string>("Total")),
                                                  Discount =Convert.ToDecimal( item.Field<string>("Discount")),
                                                 TaxValue = Convert.ToDecimal(item.Field<string>("TaxableValue")),                                                
                                                  AmountWithTax =Convert.ToDecimal( item.Field<string>("AmountWithTax")),
                                                  AmtCGSTLineIDWise =Convert.ToDecimal( item.Field<string>("CGSTAmt")),
                                                  AmtSGSTLineIDWise =Convert.ToDecimal( item.Field<string>("SGSTAmt")),
                                                  AmtIGSTLineIDWise =Convert.ToDecimal(item.Field<string>("IGSTAmt"))
                                              }).ToList();
                    

                    obj.Invoice.LineEntry = leItem;
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of InvoiceDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("AmountDtls"))
                {
                    obj.TotalAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][6]);
                    obj.TotalDiscount = Convert.ToDecimal(ds.Tables[4].Rows[0][7]);
                    obj.TotalAmountWithTax = Convert.ToDecimal(ds.Tables[4].Rows[0][8]);
                    obj.TotalCGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][10]);
                    obj.TotalSGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][12]);
                    obj.TotalIGSTAmount = Convert.ToDecimal(ds.Tables[4].Rows[0][14]);

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        HtmlTableRow tRow = new HtmlTableRow();
                        for (int j = 0; j <= 14; j++)
                        {
                            HtmlTableCell tb = new HtmlTableCell();
                            tb.InnerText = Convert.ToString(ds.Tables[4].Rows[0][j]);
                            tRow.Controls.Add(tb);
                        }
                        tblPreview.Rows.Add(tRow);
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of AmountDtls table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }
                if (ds.Tables.Contains("AmountInWords"))
                {
                    if (ds.Tables[5].Rows.Count > 0)
                    {                       
                        obj.Invoice.TotalInvoiceValue = Convert.ToDecimal(ds.Tables[5].Rows[0][0]);
                        obj.Invoice.TotalInvoiceWords = Convert.ToString(ds.Tables[5].Rows[0][1]);                       
                    }
                }
                else
                {
                    //lblMsg.Text = "System error occured during data population of AmountInWords table !!!";
                    //this.InvoicePreviewModalPopupExtender.Show();
                    //return;
                }

            }
            else
            {
                //lblMsg.Text = "System Error !!!";
                //this.InvoicePreviewModalPopupExtender.Show();
                //return;
            }
            return obj;
            #endregion

        }


        private HSN ConvertItemToHSN(DataRow item)
        {
            HSN hsn = new HSN();
            hsn.HSNNumber = item.Field<string>("HSN");
            hsn.Description = item.Field<string>("Description");
            hsn.RateCGST = Convert.ToDecimal(item.Field<string>("CGSTRate"));
            hsn.RateIGST = Convert.ToDecimal(item.Field<string>("IGSTRate"));
            hsn.RateSGST = Convert.ToDecimal(item.Field<string>("SGSTRate"));
            //hsn.PerUnitRate = Convert.ToDecimal(item.Field<string>("Rate"));
            hsn.UnitOfMeasurement = item.Field<string>("Unit");

            return hsn;
        }
       

        protected void btnUpload_Click(object sender, EventArgs e)
        {



            try
            {
                // Get the IP
                // string myIP = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();// Dns.GetHostEntry(hostName).AddressList[0].ToString();
                int count = 0;
                foreach (ListViewDataItem item in lvInvoices.Items)
                {
                    string id = lvInvoices.DataKeys[item.DataItemIndex].Values[0].ToString();
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");

                    if (chk.Checked)
                    {
                        audittrail.InvoiceID = Convert.ToInt64(id);
                        int invoiceid = Convert.ToInt32(audittrail.InvoiceID);

                        audittrail.AuditTrailStatus = Convert.ToByte(EnumConstants.InvoiceAuditTrailSatus.Upload);
                        audittrail.UserIP = Common.IP;
                        audittrail.InvoiceID = invoiceid;
                        audittrail.CreatedDate = DateTime.Now;
                        audittrail.CreatedBy = Common.LoggedInUserID(); ;
                        unitOfWork.InvoiceAuditTrailRepositry.Create(audittrail);
                        unitOfWork.Save();
                        count = count + 1;
                    }
                }
                if (count > 0)
                {
                    this.Master.SuccessMessage = "Data uploaded successfully.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                    //uc_sucess.SuccessMessage = "Data uploaded successfully.";
                   // uc_sucess.Visible = true;
                    BindAllInvoices();
                }
               else
                {
                    this.Master.WarningMessage = "Please select invoice for upload.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelWarningMessage", "$('#viewInvoiceModelWarningMessage').modal();", true);
                    //uc_sucess.ErrorMessage = ex.Message;
                   // uc_sucess.VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        GST_TRN_INVOICE GTI = new GST_TRN_INVOICE();
       // dynamic invoice;
        protected void lkb_Click(object sender, EventArgs e)
        {
              LinkButton lkbItem = (LinkButton)sender;

            if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
            {
                Int64 invoiceId = Convert.ToInt64(lkbItem.CommandArgument.ToString());
                 Invoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == invoiceId);
                 BindInvoice(Invoice);

            }


        }

        private void BindInvoice(GST_TRN_INVOICE invoice)
        {
            //List<GST_TRN_INVOICE> listInvoice = new List<GST_TRN_INVOICE>();
            //listInvoice.Add(invoice);

           // fvInvoice.DataSource = listInvoice;
            //fvInvoice.DataBind();
            //GridView gv = (GridView)fvInvoice.FindControl("gvItems");
            gvInvoice_Items.DataSource = invoice.GST_TRN_INVOICE_DATA;
            gvInvoice_Items.DataBind();
            lkbUpdateInvoice.Visible = true;
        }

        private GST_TRN_INVOICE _invoice;
        public GST_TRN_INVOICE Invoice
        {
            get
            {
                _invoice = (GST_TRN_INVOICE)Session["Invoice"];
                return _invoice!=null ? _invoice : (new GST_TRN_INVOICE());
            }
            set
            {
                Session["Invoice"] = value;
            }
        }

        protected void lkbUpdateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // GridView gv = (GridView)fvInvoice.FindControl("gvItems");
                List<GST_TRN_INVOICE_DATA> items = GetGVData();
                //var it = Invoice;
                if (Invoice != null)
                {
                    //Seller seller = new Seller();


                    GST_TRN_INVOICE inv = new GST_TRN_INVOICE();
                    inv.InvoiceDate = DateTime.Now;
                    inv.InvoiceMonth = Convert.ToByte(DateTime.Now.Month);
                    var CurrentSrlNo = unitOfWork.InvoiceRepository.Filter(f => f.SellerUserID == Invoice.AspNetUser.Id).Count() + 1;
                    inv.InvoiceNo = InvoiceOperation.InvoiceNo(Invoice.AspNetUser, Invoice.FinYear_ID.ToString(), CurrentSrlNo.ToString());
                    inv.SellerUserID = Invoice.SellerUserID;
                    inv.ReceiverUserID = Invoice.ReceiverUserID;
                    inv.ConsigneeUserID = Invoice.ConsigneeUserID;
                    inv.OrderDate = Invoice.OrderDate;
                    inv.VendorID = Invoice.VendorID;
                    inv.TransShipment_ID = Invoice.TransShipment_ID;
                    inv.Freight = Invoice.Freight;
                    inv.Insurance = Invoice.Insurance;
                    inv.PackingAndForwadingCharges = Invoice.PackingAndForwadingCharges;
                    inv.ElectronicReferenceNo = Invoice.ElectronicReferenceNo;
                    inv.ElectronicReferenceNoDate = Invoice.ElectronicReferenceNoDate;
                    inv.InvoiceType = Invoice.InvoiceType;
                    inv.FinYear_ID = Invoice.FinYear_ID;
                    inv.IsInter = Invoice.IsInter;
                    inv.ReceiverFinYear_ID = Invoice.ReceiverFinYear_ID;
                    inv.ParentInvoiceID = Invoice.InvoiceID;
                    inv.TaxBenefitingState = Invoice.AspNetUser2.StateCode;
                    inv.Status = true;
                    //  var invoicePeriod=unitOfWork.FinYearRepository.Find(f=>f.Fin_ID== Invoice.FinYear_ID).Finyear_Format;

                    //  GST_TRN_INVOICE updateInvoice = new GST_TRN_INVOICE();

                    inv.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    inv.InvoiceSpecialCondition = Invoice.InvoiceSpecialCondition;
                    inv.CreatedDate = DateTime.Now;
                    inv.UpdatedDate = DateTime.Now;
                    inv.CreatedBy = Common.LoggedInUserID();
                    inv.UpdatedBy = Common.LoggedInUserID();

                    var invoiceCreate = unitOfWork.InvoiceRepository.Create(inv);
                    unitOfWork.Save();
                    //Update old invoice status that is A or M---------------Start-------------
                    if (Invoice.InvoiceMonth == (byte)DateTime.Now.Month)
                    {
                        var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                        oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Amended);
                        // Invoice.InvoiceID = updateInvoice.InvoiceID;
                        var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                        unitOfWork.Save();
                    }
                    else
                    {
                        var oldInvoice = unitOfWork.InvoiceRepository.Find(f => f.InvoiceID == Invoice.InvoiceID);
                        oldInvoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Modified);
                        //  updateInvoice.InvoiceID = updateInvoice.InvoiceID;
                        var invoiceUpdate = unitOfWork.InvoiceRepository.Update(oldInvoice);
                        unitOfWork.Save();
                    }
                    //-------------End--------
                    // bool isInter =InvoiceOperation.GetConsumptionDestinationOfGoodsOrServices(Invoice.AspNetUser.StateCode, Invoice.AspNetUser2.StateCode, Invoice.AspNetUser1.StateCode);
                    bool isInter = Invoice.IsInter.Value;
                    bool isStateExampted = unitOfWork.StateRepository.Find(f => f.StateCode == Invoice.AspNetUser2.StateCode).IsExempted.Value;
                    bool isExport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Export || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZDeveloper || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.SEZUnit || Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.DeemedExport);
                    bool isJobwork = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.JobWork);
                    bool isImport = (Invoice.InvoiceSpecialCondition == (byte)EnumConstants.InvoiceSpecialCondition.Import);
                    var stateData = unitOfWork.StateRepository.Find(c => c.StateCode == Invoice.AspNetUser.StateCode);
                    var isUTState = stateData.UT.Value;
                    var isExempted = stateData.IsExempted.Value;

                    var invLineItem = from invo in items
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          InvoiceID = invoiceCreate.InvoiceID,
                                          LineID = invo.LineID,
                                          // GST_MST_ITEM = invo.Item,
                                          Item_ID = invo.GST_MST_ITEM.Item_ID,
                                          Qty = invo.Qty,
                                          Rate = invo.Rate,
                                          TotalAmount = invo.TotalAmount,
                                          Discount = invo.Discount,
                                          TaxableAmount = invo.TaxableAmount,
                                          IGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? invo.GST_MST_ITEM.IGST : (isImport ? invo.GST_MST_ITEM.IGST : 0)))),
                                          IGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value) : (isImport ? Calculate.CalculateIGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.IGST.Value) : 0)))),
                                          CGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : invo.GST_MST_ITEM.CGST) : 0)),
                                          CGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? (isExport ? 0 : Calculate.CalculateCGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CGST.Value)) : 0)),
                                          SGSTRate = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : invo.GST_MST_ITEM.SGST))),
                                          SGSTAmt = isJobwork ? 0 : (isUTState ? 0 : (isInter ? 0 : (isExport ? 0 : Calculate.CalculateSGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.SGST.Value)))),
                                          UGSTRate = isJobwork ? 0 : (isExport ? 0 : (isUTState ? invo.GST_MST_ITEM.UGST.Value : 0)),
                                          UGSTAmt = isJobwork ? 0 : (isExport ? 0 : (isUTState ? Calculate.CalculateUGSTLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.UGST.Value) : 0)),
                                          CessRate = isJobwork ? 0 : invo.GST_MST_ITEM.CESS,
                                          CessAmt = isJobwork ? 0 : Calculate.CalculateCESSLineIDWise(invo.TaxableAmount.HasValue ? invo.TaxableAmount.Value : 0, invo.GST_MST_ITEM.CESS.Value)
                                          //TotalAmountWithTax = invo.TaxableAmount + IGSTAmt,
                                      };


                    foreach (GST_TRN_INVOICE_DATA item in invLineItem)
                    {
                        item.TotalAmountWithTax = item.TaxableAmount + item.IGSTAmt + item.CGSTAmt + item.SGSTAmt + item.UGSTAmt + item.CessAmt;

                        unitOfWork.InvoiceDataRepository.Create(item);
                    }
                    unitOfWork.Save();
                    gvInvoice_Items.DataSource = null;
                    gvInvoice_Items.DataBind();
                    lkbUpdateInvoice.Visible = false;
                    Invoice = new GST_TRN_INVOICE();
                    this.Master.SuccessMessage = "Data updated successfully.";
                    //uc_sucess.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewInvoiceModelSucessMessage", "$('#viewInvoiceModelSucessMessage').modal();", true);
                }
            }

            catch (Exception ex) { }
        }

        public bool IsEditable(string invoicID)
        {
            int InvoicID= Convert.ToInt32(invoicID);
            return unitOfWork.InvoiceRepository.Contains(c => c.ParentInvoiceID == InvoicID);
 
        }
    
        public bool IsUpload(string invoicID)
        {
            int InvoicID = Convert.ToInt32(invoicID);
            return unitOfWork.InvoiceAuditTrailRepositry.Contains(c =>c.AuditTrailStatus==(byte)EnumConstants.InvoiceAuditTrailSatus.Upload && c.InvoiceID == InvoicID);

        }
        private List<GST_TRN_INVOICE_DATA> GetGVData()
        {
            List<GST_TRN_INVOICE_DATA> lineCollection = new List<GST_TRN_INVOICE_DATA>();
            foreach (GridViewRow row in gvInvoice_Items.Rows)
            {
                //  GridView gvSizePrice = (GridView)fvProduct.FindControl("gdvSizePrice");
                TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                TextBox txtGoodService = (TextBox)row.FindControl("txtGoodService");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                Label txtTotal = (Label)row.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                Label txtTaxableValue = (Label)row.FindControl("txtTaxableValue");

                if (!string.IsNullOrEmpty(txtGoodService.Text.Trim()))
                {
                    GST_TRN_INVOICE_DATA le = new GST_TRN_INVOICE_DATA();
                    le.LineID = row.RowIndex;
                    le.Qty = Convert.ToDecimal(txtQty.Text.Trim());
                    le.GST_MST_ITEM = unitOfWork.ItemRepository.Find(f => f.ItemCode == txtItemCode.Text.Trim());
                    le.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                    le.TotalAmount = Convert.ToDecimal(txtTotal.Text.Trim());
                    if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                    le.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                    //le.AmountWithTaxLineIDWise = Convert.ToDecimal(((Label)row.FindControl("txtTaxableValue")).Text.Trim());
                    le.TaxableAmount = Convert.ToDecimal(txtTaxableValue.Text.Trim());
                    // Grand total of all line items
                   // le.TotalAmount += le.TotalAmount;
                    // grand total of all line items with tax
                   /// le.TotalAmountWithTax += le.TotalAmountWithTax;

                    lineCollection.Add(le);
                }
            }
            return lineCollection;
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            int rowIndex = currentRow.RowIndex;
            TextBox textBox = (sender as TextBox);

            var itemData = unitOfWork.ItemRepository.Find(f => f.ItemCode == textBox.Text.Trim());//seller.GetItemInformation(textBox.Text.Trim());
            int result;

            if (itemData != null)
            {
                #region Code is working fine for HSN search , but now need to get the logic of Purchase register
                if (int.TryParse(textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) && !string.IsNullOrEmpty(textBox.Text.Trim()) && textBox.Text.Length == 8)
                {
                    try
                    {
                        string type = string.Empty;
                        // added to check whether new HSN IS NOTIFIED OR NOT
                        if (itemData.IsNotified.Value)
                        {
                            //BindNotifiedHSN(itemData.GST_MST_ITEM_NOTIFIED, lvHSNData);
                           // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalhsn", "$('#myModalhsn').modal();", true);
                            //upModal.Update();
                        }
                        if (itemData != null)
                        {
                            TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                            TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                            txtDescription.Text = itemData.Description;
                            txtUnit.Text = itemData.Unit;
                        }
                    }
                    catch (System.ArgumentNullException arguEx)
                    {
                        System.ArgumentNullException formatErr = new System.ArgumentNullException("Null value was passed.");
                      
                    }
                   

                }

            }
            else
            {
                textBox.Text = string.Empty;
                TextBox txtDescription = (TextBox)currentRow.FindControl("txtGoodService");
                TextBox txtUnit = (TextBox)currentRow.FindControl("txtUnit");
                TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");
                Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");
                TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
                Label txtTotal = (Label)currentRow.FindControl("txtTotal");
                TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");


                txtDescription.Text = string.Empty;
                txtUnit.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtTaxableValue.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtTotal.Text = string.Empty;
                //TODO:DISPLAY MESSAGE thet item does not exist.
            }

                #endregion

        }


        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            // reassigning the value of seller
            //if (Session["seller"] != null)
            //    seller = (Seller)Session["seller"];

            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");

            // getting the text box id
            string HSHtxtID = txtQty.ID;

            int rowIndex = currentRow.RowIndex;
            TextBox txtHsn = (TextBox)currentRow.FindControl("txtItemCode");
            TextBox txtRate = (TextBox)currentRow.FindControl("txtRate");
            Label txtTotal = (Label)currentRow.FindControl("txtTotal");
            TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
            Label txtTaxableValue = (Label)currentRow.FindControl("txtTaxableValue");

            if ((txtQty.Text.ToString() != ""))
            {
                // decimal? purchaseLedger = seller.GetSellerStockInventory(txtHsn.Text.Trim(), sellerProfile.Id.ToString());

                //if (purchaseLedger.HasValue)
                //{
                #region to check qty entered is there in saleRegister or not
                //if (purchaseLedger.Value >= Convert.ToInt32(txtQty.Text.Trim()))
                //{
                //    //  TO DO :: if it is null or empty , then map all UI controls like  total, discount , taxable .txt to ""
                //    if ((txtRate.Text.ToString() != "") && (!string.IsNullOrEmpty(txtRate.Text.ToString())) && (Convert.ToDecimal(txtRate.Text.ToString()) < Decimal.MaxValue))
                //    {
                // caluculate total 
                decimal totalRate = Common.CalculateTotal(Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToDecimal(txtRate.Text.Trim()));
                txtTotal.Text = totalRate.ToString();
                if (totalRate < Decimal.MaxValue)
                {
                    txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, !string.IsNullOrEmpty(txtDiscount.Text) ? Convert.ToDecimal(txtDiscount.Text) : 0).ToString();

                    if (!string.IsNullOrEmpty(txtDiscount.Text))
                    {
                        // Calculating the tax value, 
                        // for that discount given should be there , if not then else part will b called
                        if (Convert.ToDecimal(txtDiscount.Text) > 0.0m)
                        {  // tax value , unit * rate per unit * tax applicable
                            txtTaxableValue.Text = Common.CalculateTaxableValue(totalRate, Convert.ToDecimal(txtDiscount.Text)).ToString();
                        }
                        else
                        // get the focus to txt rate column with a message 
                        {
                            // TO DO ::  Use some other option to do this-- Aashis 
                            // Due to abnormal terminationn of 
                            //BALAJI.GSP.APPLICATION.User.User masterPage = this.Master as BALAJI.GSP.APPLICATION.User.User;
                            //masterPage.ErrorMessage = "Please enter Discount";
                            txtRate.Focus();
                            return;
                        }
                    }
                }
            }
            else
            {
                txtTotal.Text = string.Empty;
                txtTaxableValue.Text = string.Empty;
            }

            //}
            //else
            //{
            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //    //TODO:I NEED TO APPY MODEL POP UP HERE TO UPDATE PURCHASE REGISTER
            //}
                #endregion
            //}
            //else
            //{

            //    uc_PerchaseRegister.ItemCode = txtHsn.Text.Trim();
            //    uc_PerchaseRegister.SellerUserID = sellerProfile.Id;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "viewPurchaseRegigsterModel", "$('#viewPurchaseRegigsterModel').modal();", true);

            //    return;
            //}
            //}

        }
    }
}