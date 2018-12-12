using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GST.Utility;
using BusinessLogic.Repositories;
using System.Web.UI.HtmlControls;

namespace BALAJI.GSP.APPLICATION.UC.UC_Gstr
{
    public partial class uc_Gstr_Tileview : System.Web.UI.UserControl
    {

        UnitOfWork unitofwork = new UnitOfWork();
        Return_Status status = new Return_Status();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindRepeater()
        {
            //rptGSTR1.DataSource = // invoiceSum.ToList();
            //rptGSTR1.DataBind();
        }
        //public List<GST_TRN_RETURN_STATUS> InvoiceList
        //{

        //    set
        //    {
        //        var data = from item in value
        //                   group item by new { item.Status,item.ReturnStatus,item.Action } into g
        //                   select new
        //                   {
        //                       Status = g.Key.Status,
        //                       ReturnStatus=g.Key.ReturnStatus

        //                       //INVSPLCONDITION = g.Key.InvoiceSpecialCondition,
        //                       //TotalInvoice = g.Count(),
        //                       ////QTY = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds=>ds.Qty)),
        //                       //TAXABLEAMOUNT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.TaxableAmount)),
        //                       //IGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.IGSTAmt)),
        //                       //CGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CGSTAmt)),
        //                       //SGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.SGSTAmt)),
        //                       //CESSAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CessAmt))
        //                   };

        //        var invstatusfile =(byte) EnumConstants.ReturnFileStatus.FileGstr1;
        //        var invstatussave =(byte) EnumConstants.ReturnFileStatus.Save;
        //        var invstatussubmit =(byte) EnumConstants.ReturnFileStatus.Submit;
        //        //data.Select(s => s.Status == invstatussave).ToList();
        //       var Status=data.Select(c=> c.Status).ToList();

        //      // var ab = Convert.ToByte(Status);
        //        rptGSTR1.DataSource = data.ToList();// invoiceSum.ToList();
        //        rptGSTR1.DataBind();
        //        foreach (RepeaterItem item in rptGSTR1.Items)
        //        {
        //            HtmlGenericControl control = item.FindControl("beforeFile") as HtmlGenericControl;
        //            if (Status.Contains(invstatussave))
        //            {
        //                control.Visible = true;
        //            }
        //            else
        //            {
        //                control.Visible = false;
        //            }
        //        }

        //        //rptGsSTR3B.DataSource = EnumConstants.Return.Gstr3B.ToDescription();//.ToList();// invoiceSum.ToList();
        //        //rptGsSTR3B.DataBind();
        //    }
        //}
        //public List<GST_MST_HEADER> InvoiceList
        //{

        //    set
        //    {
        //        var data = from item in value
        //                   group item by new { item.Status, item.HeaderName } into g
        //                   select new
        //                   {

        //                       ReturnStatus = g.Key.HeaderName

        //                       //INVSPLCONDITION = g.Key.InvoiceSpecialCondition,
        //                       //TotalInvoice = g.Count(),
        //                       ////QTY = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds=>ds.Qty)),
        //                       //TAXABLEAMOUNT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.TaxableAmount)),
        //                       //IGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.IGSTAmt)),
        //                       //CGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CGSTAmt)),
        //                       //SGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.SGSTAmt)),
        //                       //CESSAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CessAmt))
        //                   };




        //        //var invoiceList = unitofwork.ReturnStatusRepository.Filter(f => f.Action == MaxAction && f.ReturnStatus == HeaderCodeStatus).ToList();



        //       //var invoice=unitofwork.ReturnStatusRepository.Filter(f=>.Where(w => w.ReturnStatus == status.HeaderCode).ToList();

        //        //data.Select(s => s.Status == invstatussave).ToList();
        //        //var Status = data.Select(c => c.MaxAction).ToList();

        //        // var ab = Convert.ToByte(Status);
        //        rptGSTR1.DataSource = data.ToList();// invoiceSum.ToList();
        //        rptGSTR1.DataBind();
        //        var invstatusfile = (byte)EnumConstants.ReturnFileStatus.FileGstr1;
        //   var invstatussave =(byte) EnumConstants.ReturnFileStatus.Save;
        //         var invstatussubmit =(byte) EnumConstants.ReturnFileStatus.Submit;
        //         //foreach (RepeaterItem item in rptGSTR1.Items)
        //         //{
        //         //    HtmlGenericControl control = item.FindControl("beforeFile") as HtmlGenericControl;
        //         //    if (MaxAction.Contains())
        //         //    {
        //         //        control.Visible = true;
        //         //    }
        //         //    else
        //         //    {
        //         //        control.Visible = false;
        //         //    }
        //         //}

        //        //rptGsSTR3B.DataSource = EnumConstants.Return.Gstr3B.ToDescription();//.ToList();// invoiceSum.ToList();
        //        //rptGsSTR3B.DataBind();
        //    }
        //} 
        public EventHandler Info_Click;
        protected void lbinfo_Click(object sender, EventArgs e)
        {
            LinkButton lkb = (LinkButton)sender;

            //ViewState["ReturnStatus"] = Convert.ToByte(lkb.CommandArgument);
            //Session["ReturnSessionStatus"] = Convert.ToByte(lkb.CommandArgument);
            //var a = Session["ReturnSessionStatus"];
            string commandName = lkb.CommandName;
            var Status = Convert.ToInt16(lkb.CommandArgument);

            if (commandName == "ViewDetail" && Status == (byte)EnumConstants.Return.Gstr1)
            {
                Response.Redirect("~/User/ureturn/GSTR1PreviewB2B.aspx");
            }
            else if (commandName == "ViewDetail" && Status == (byte)EnumConstants.Return.Gstr3B)
            {
                Response.Redirect("~/User/ureturn/GSTR3BPreview.aspx");

            }
            else if (commandName == "Offline" && Status == (byte)EnumConstants.Return.Gstr1)
            {
                Response.Redirect("~/User/Trans/Offline.aspx");
            }
            else if (commandName == "Offline" && Status == (byte)EnumConstants.Return.Gstr3B)
            {
                Response.Redirect("~/User/Trans/Offline.aspx");
            }
            else if (commandName == "Online" && Status == (byte)EnumConstants.Return.Gstr1)
            {
                Response.Redirect("~/User/ureturn/GSTR1Details.aspx");
            }
            else if (commandName == "Online" && Status == (byte)EnumConstants.Return.Gstr3B)
            {
                Response.Redirect("~/User/ureturn/GSTR3BDetails.aspx");
            }
            else
            {

            }
        }


        public List<ReturnHeaderCodeStatus> ReturenHeaderStatus(int Year, int MonthName,string UserID)
        {

            var invGSTR1 = (byte)EnumConstants.Return.Gstr1;
            var invGSTR2 = (byte)EnumConstants.Return.Gstr2A;
            var invGSTR3B = (byte)EnumConstants.Return.Gstr3B;
            var HeaderCodeStatus = unitofwork.headerrepository.Filter(f => (f.HeaderCodeID == invGSTR1 || f.HeaderCodeID == invGSTR2 || f.HeaderCodeID == invGSTR3B)).Select(s => s.HeaderCodeID).ToList();//.ToList();
            var MaxAction = unitofwork.ReturnStatusRepository.Filter(f => f.ReturnStatus == invGSTR1 || f.ReturnStatus == invGSTR2 || f.ReturnStatus == invGSTR3B).Max(a => a.Action);
            var count = unitofwork.InvoiceRepository.Filter(f => f.InvoiceUserID == UserID && f.InvoiceMonth == MonthName && f.FinYear_ID == Year).Count(c => c.InvoiceID != null);
            var rstatus = (from R in unitofwork.ReturnStatusRepository.Filter(f => f.ReturnStatus == invGSTR1 || f.ReturnStatus == invGSTR2 || f.ReturnStatus == invGSTR3B)// && f.Action == MaxAction)
                           join h in unitofwork.headerrepository.Filter(f => f.HeaderCodeID == invGSTR1 || f.HeaderCodeID == invGSTR2 || f.HeaderCodeID == invGSTR3B) on R.ReturnStatus equals h.HeaderCodeID
                           //where R.Action == MaxAction && R.FinYear_ID == Year && R.Period == MonthName && R.User_id == UserID
                           where R.FinYear_ID == Year && R.Period == MonthName && R.User_id == UserID
                           select new ReturnHeaderCodeStatus
                              {
                                  HeaderCodeID = h.HeaderCodeID,
                                  HeaderName = h.HeaderName,
                                  ReturnStatus = R.ReturnStatus.Value,
                                  Action = R.Action.Value,
                                  Status = R.Status.Value
                              }).ToList();


            rptGSTR1.DataSource = rstatus;
            rptGSTR1.DataBind();
            return rstatus;
        }
        public int InvoiceCount(int month, string userId,int year)
        {
            var count = unitofwork.InvoiceRepository.Filter(f=>f.InvoiceUserID==userId && f.InvoiceMonth==month && f.FinYear_ID==year).Count(c=>c.InvoiceID!=null);
            return count;
        }
        protected void btnPrepareOffline_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Offline/Auditrail");
        }

        protected void btnPrepareOnline_Click(object sender, EventArgs e)
        {
            //condition amits
            Info_Click(sender, e);

        }


    }

    public class ReturnHeaderCodeStatus
    {
        public byte HeaderCodeID { get; set; }
        public string HeaderName { get; set; }
        public byte ReturnStatus { get; set; }
        public byte Action { get; set; }
        public byte Status { get; set; }

    }
}