using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.User.Trans
{
    public partial class Note : System.Web.UI.Page
    {
        cls_Invoice invoice=new cls_Invoice();
        cls_CreditDebit_Note noteItem = new cls_CreditDebit_Note();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInvoice();
                BindNoteType();
            }
        }

        private void BindInvoice()
        {
            ddl_InvoiceID.DataSource = noteItem.GetModifiedInvoice(Common.LoggedInUserID());
            ddl_InvoiceID.DataValueField = "Key";
            ddl_InvoiceID.DataTextField = "Value";
            ddl_InvoiceID.DataBind();
            ddl_InvoiceID.Items.Insert(0,new ListItem("[ SELECT INVOICE ]","0"));

            //var item = unitOfWork.CreditDebitNoteRepository.Filter(f => f.From_UserID == userID).GroupBy(g => g.NoteType)
            //    .Select(g => new
            //    {
            //        NoteType = g.Key,
            //        TotalNote = g.Sum(s => s.CreditDebitID),
            //        TotalNoteValueWithTax = g.Sum(s => s.GST_TRN_CRDR_NOTE_DATA.Sum(ns => ns.TotalAmountWithTax))
            //    });
            //return item;
        }
        private void ClearField()
        {
            txtDescription.Text = string.Empty;
        }
        private void BindNoteType()
        {
            ddlNoteType.DataSource = typeof(EnumConstants.NoteType).ToList();
            ddlNoteType.DataValueField = "Key";
            ddlNoteType.DataTextField = "Value";
            ddlNoteType.DataBind();
           
        }

        protected void ddl_InvoiceID_SelectedIndexChanged(object sender, EventArgs e)
        {
             // var items = invoice.GetInvoice(Convert.ToInt64(ddl_InvoiceID.SelectedValue.ToString()));
           BindInvoiceData(noteItem.MisMatchInvoice(ddl_InvoiceID.SelectedValue.ToString()));
        }

        private void BindInvoiceData(List<GST_TRN_INVOICE_DATA> data)
        {
            gvItems.DataSource = data;
            gvItems.DataBind();
        }

        private List<GST_TRN_INVOICE_DATA> GetGVData()
        {
            List<Int64> invoiceDataID = new List<Int64>();
            foreach (GridViewRow row in gvItems.Rows)
            {
                var invoiceId =Convert.ToInt64(gvItems.DataKeys[row.DataItemIndex].Values["InvoiceDataID"].ToString());
                invoiceDataID.Add(invoiceId);
            }

            return invoice.GetInvoiceData(invoiceDataID);
        }

        protected void lkbSubmitItems_Click(object sender, EventArgs e)
        {
            try
            {
                var invoiceData = invoice.GetInvoice(Convert.ToInt64(ddl_InvoiceID.SelectedValue.ToString()));
                invoiceData.GST_TRN_INVOICE_DATA = GetGVData();
                if (invoiceData.GST_TRN_INVOICE_DATA.Count() > 0)
                {
                    EnumConstants.NoteType invAction = (EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), ddlNoteType.SelectedValue.ToString());
                  
                    noteItem.NoteType = (byte)invAction;
                    noteItem.Description = txtDescription.Text.Trim();                  
                    bool isSave = noteItem.SaveNote(invoiceData);
                    uc_sucess.SuccessMessage = "Data Submitted Successfully.";
                    uc_sucess.Visible = true;
                    ClearField();
                    //Message
                    BindInvoice();
                }
                else
                {
                    uc_sucess.ErrorMessage = "Please Enter Valid Data.";
                    uc_sucess.VisibleError = true;

                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                this.uc_sucess.ErrorMessage = ex.Message;
                uc_sucess.VisibleError = true;
            }
        }
    }
}