using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.User.Trans
{
    public partial class ITC : System.Web.UI.Page
    {
        cls_ITC itc = new cls_ITC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindITC(); 
            }
        }
        private void BindITC()
        {
            try
            {
                var itcItems = itc.GetITC(Common.LoggedInUserID());
                lvITC.DataSource = itcItems;
                lvITC.DataBind();
              
                litTotalIGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.IGST).ToString();
                litTotalCGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.CGST).ToString();
                litTotalSGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.SGST).ToString();
                litTotalCess.Text= itcItems.Where(w =>  w.ITCMovement== (byte)EnumConstants.ITCMovement.Credit).Sum(s=> s.Cess).ToString();
                litTotalItc.Text =(itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.IGST) + itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.CGST)
                    + itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Credit).Sum(s => s.SGST)+itcItems.Where(w =>  w.ITCMovement== (byte)EnumConstants.ITCMovement.Credit).Sum(s=> s.Cess)).ToString();

                litDrTotalITC.Text = (itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.IGST) + itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.CGST)
                    + itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.SGST) + itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.Cess)).ToString();
                ;
                litDRIGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.IGST).ToString();
                litDRCGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.CGST).ToString();
                litDRSGST.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.SGST).ToString();
                litDrCESS.Text = itcItems.Where(w => w.ITCMovement == (byte)EnumConstants.ITCMovement.Debit).Sum(s => s.Cess).ToString();
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}