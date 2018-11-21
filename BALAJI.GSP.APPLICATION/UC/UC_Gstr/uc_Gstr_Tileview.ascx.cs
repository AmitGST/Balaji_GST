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
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void BindRepeater()
        {
            //rptGSTR1.DataSource = // invoiceSum.ToList();
            //rptGSTR1.DataBind();
        }
        public List<GST_TRN_RETURN_STATUS> InvoiceList
        {
            
            set
            {
                var data = from item in value
                           group item by new { item.Status,item.ReturnStatus,item.Action } into g
                           select new
                           {
                               Status = g.Key.Status,
                               ReturnStatus=g.Key.ReturnStatus
                            
                               //INVSPLCONDITION = g.Key.InvoiceSpecialCondition,
                               //TotalInvoice = g.Count(),
                               ////QTY = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds=>ds.Qty)),
                               //TAXABLEAMOUNT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.TaxableAmount)),
                               //IGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.IGSTAmt)),
                               //CGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CGSTAmt)),
                               //SGSTAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.SGSTAmt)),
                               //CESSAMT = g.Sum(s => s.GST_TRN_INVOICE_DATA.Sum(ds => ds.CessAmt))
                           };
           
                var invstatusfile =(byte) EnumConstants.ReturnFileStatus.FileGstr1;
                var invstatussave =(byte) EnumConstants.ReturnFileStatus.Save;
                var invstatussubmit =(byte) EnumConstants.ReturnFileStatus.Submit;
                //data.Select(s => s.Status == invstatussave).ToList();
               var Status=data.Select(c=> c.Status).ToList();

              // var ab = Convert.ToByte(Status);
                rptGSTR1.DataSource = data.ToList();// invoiceSum.ToList();
                rptGSTR1.DataBind();
                foreach (RepeaterItem item in rptGSTR1.Items)
                {
                    HtmlGenericControl control = item.FindControl("beforeFile") as HtmlGenericControl;
                    if (Status.Contains(invstatussave))
                    {
                        control.Visible = true;
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
              
                //rptGsSTR3B.DataSource = EnumConstants.Return.Gstr3B.ToDescription();//.ToList();// invoiceSum.ToList();
                //rptGsSTR3B.DataBind();
            }
        } 
       
        public EventHandler Info_Click;
        protected void lbinfo_Click(object sender, EventArgs e)
        {
           LinkButton lkb = (LinkButton)sender;

           ViewState["ReturnStatus"] = Convert.ToByte(lkb.CommandArgument);
           var Status = Convert.ToInt16(lkb.CommandArgument);
            if(Status==(byte)EnumConstants.Return.Gstr1)
            {
                //Info_Click(sender, e);
                Response.Redirect("~/User/ureturn/GSTR1PreviewB2B.aspx");
               // User/ureturn/GSTR1PreviewB2B
            }
            else if (Status == (byte)EnumConstants.Return.Gstr3B)
            {
                //Response.Redirect("~/User/ureturn/GSTR3BDetails.aspx");
                Response.Redirect("~/User/ureturn/GSTR3BPreview.aspx");

            }
            else
            {

            }
           
           // Server.Transfer("~/User/ureturn/GSTR1Details.aspx");
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
}