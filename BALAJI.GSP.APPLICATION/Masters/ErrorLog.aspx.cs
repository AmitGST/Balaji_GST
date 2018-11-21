using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        UnitOfWork unitofwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();

            }
        }

        //public void BindStatus()
        //{
        protected void lv_HSN_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
           
            DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");
            HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
            if (hdnStatus.Value != null && hdnStatus.Value != "")
                ddlStatus.SelectedValue = hdnStatus.Value;
        }

        public void BindItems()
        {
            List<GST_TRN_ERROR_HANDLING> objList = new List<GST_TRN_ERROR_HANDLING>();
            lv_ErrorHandling.DataSource = unitofwork.ErrorHandlingRepository.All().ToList();
            lv_ErrorHandling.DataBind();

        }

        public Dictionary<string, string> BindErrorStatus()
        {
           
           Dictionary<string,string> errorList=new Dictionary<string,string>();
                foreach (EnumConstants.ErrorHandling r in Enum.GetValues(typeof(EnumConstants.ErrorHandling)))
                {
                    errorList.Add(Enum.GetName(typeof(EnumConstants.ErrorHandling), r), Convert.ToByte(r).ToString());
                    //ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.ErrorHandling), r), Convert.ToByte(r).ToString());
                   // ddlStatus.Items.Add(item);
                }
                return errorList;
               // ddlStatus.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
           // }
        }

        protected void lv_ErrorHandling_ItemCreated(object sender, ListViewItemEventArgs e)
        {
           

        }

        protected void lv_ErrorHandling_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpError.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindItems();
            dpError.DataBind();
        }

        protected void lv_ErrorHandling_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lv_ErrorHandling.EditIndex = e.NewEditIndex;
            BindItems();
        }

        protected void lv_ErrorHandling_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            LinkButton lkbUpdate = (lv_ErrorHandling.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
                if (lkbUpdate.CommandName == "Update")
                {
                 int id = Convert.ToInt32(lkbUpdate.CommandArgument);
                  GST_TRN_ERROR_HANDLING invoice = unitofwork.ErrorHandlingRepository.Filter(x => x.ErrorLogId == id).SingleOrDefault();
                    if (invoice != null)
                    {
                        DropDownList ddlStatus = (lv_ErrorHandling.Items[e.ItemIndex].FindControl("ddlStatus")) as DropDownList;
                        if (ddlStatus.SelectedIndex > -1)
                           invoice.Status = Convert.ToByte(ddlStatus.SelectedValue);
                    }
                    unitofwork.ErrorHandlingRepository.Update(invoice);
                    unitofwork.Save();
                }
               lv_ErrorHandling.EditIndex = -1;
               BindItems();
        }


        //protected void lv_ErrorHandling_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        //{
        //    LinkButton lkbUpdate = (lv_ErrorHandling.Items[e.ItemIndex].FindControl("lkbUpdate")) as LinkButton;
        //    if (lkbUpdate.CommandName == "Update")
        //    {
        //        int id = Convert.ToInt32(lkbUpdate.CommandArgument);
        //        GST_TRN_ERROR_HANDLING invoice = unitofwork.ErrorHandlingRepository.Filter(x => x.ErrorLogId == id).SingleOrDefault();
        //        if (invoice != null)
        //        {
        //            DropDownList ddlStatus = (lv_ErrorHandling.Items[e.ItemIndex].FindControl("ddlStatus")) as DropDownList;
        //            if (ddlStatus.SelectedIndex > 0)
        //                invoice.Status = Convert.ToByte(ddlStatus.SelectedValue);
        //        }
        //        unitofwork.ErrorHandlingRepository.Create(invoice);
        //        unitofwork.Save();
        //    }
        //    lv_ErrorHandling.EditIndex = -1;
        //}

       

        



    }
}

       
       
    
