using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.AddOffline
{
    public partial class uc_Export_GSTR1 : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // BindItems();
            }
        }
        public void BindItems(string Section)
        {
            try
            {
                List<GST_TRN_OFFLINE_INVOICE_RATE> objList = new List<GST_TRN_OFFLINE_INVOICE_RATE>();
                var data = unitOfwork.OfflinerateRepository.All().ToList();
                objList.AddRange(data);
                litSection.Text = Section;
                litInvoiceNo.Text = InvoiceNo;
                lvEXPORTGstr1.DataSource = objList;
                lvEXPORTGstr1.DataBind();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        public string InvoiceNo
        {
            get;
            set;
        }
        public EventHandler lkbBackEvent;
        protected void lkbBack_Click(object sender, EventArgs e)
        {
            lkbBackEvent(sender, e);
        }
        public List<GST_TRN_OFFLINE_INVOICE_DATAITEM> TotalTaxableAmount()
        {
            int valueId = Convert.ToInt32(string.IsNullOrEmpty(Session["ValueId"] as string) ? 0 : Session["ValueId"]);
            var data = unitOfwork.OfflineinvoiceRepository.Filter(x => x.ValueId == valueId).SingleOrDefault();
            if (data != null)
            {
                return data.GST_TRN_OFFLINE_INVOICE_DATAITEM.ToList();
            }
            return null;
        }
        public int RateId;

        protected void lkbSave_Click(object sender, EventArgs e)
        {
            try
            {
                GST_TRN_OFFLINE_INVOICE_DATAITEM dataitem;
                foreach (var items in lvEXPORTGstr1.Items)
                {
                    TextBox TotalTaxable_Value = (TextBox)items.FindControl("txt_TaxableValue");
                    TextBox IGST = (TextBox)items.FindControl("txtIGST");
                    //Label Rate = (Label)items.FindControl("lblRate");
                    HiddenField OfflineDataId = (HiddenField)items.FindControl("hdnOfflineDataId");
                    int RateId = Convert.ToInt32(lvEXPORTGstr1.DataKeys[items.DisplayIndex].Value);
                    int ExistingDataId = Convert.ToInt32(OfflineDataId.Value);
                    if (ExistingDataId <= 0)
                    {
                        dataitem = new GST_TRN_OFFLINE_INVOICE_DATAITEM();
                        if (Convert.ToDecimal(TotalTaxable_Value.Text) > 0)
                        {
                            dataitem.IGSTAmt = Convert.ToDecimal(IGST.Text);
                            dataitem.TotalTaxableValue = Convert.ToDecimal(TotalTaxable_Value.Text);
                            dataitem.ValueID = Convert.ToInt32(Session["ValueId"]);
                            dataitem.RateId = RateId;
                            unitOfwork.OfflineinvoicedataitemRepository.Create(dataitem);
                            unitOfwork.Save();
                        }
                    }
                    else
                    {
                        var dataitemexist = unitOfwork.OfflineinvoicedataitemRepository.Filter(x => x.OfflineDataID == ExistingDataId).SingleOrDefault();
                        if (Convert.ToDecimal(TotalTaxable_Value.Text) > 0)
                        {
                            dataitemexist.IGSTAmt = Convert.ToDecimal(IGST.Text);
                            dataitemexist.TotalTaxableValue = Convert.ToDecimal(TotalTaxable_Value.Text);
                            unitOfwork.OfflineinvoicedataitemRepository.Update(dataitemexist);
                            unitOfwork.Save();
                        }
                    }
                }
                uc_sucess.Visible = true;
                uc_sucess.SuccessMessage = "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}