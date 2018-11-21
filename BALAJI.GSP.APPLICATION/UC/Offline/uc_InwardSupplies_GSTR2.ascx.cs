using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_InwardSupplies_GSTR2 : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();
            }
        }

        public void BindItems()
        {
            List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
            var data = unitOfwork.OfflineinvoiceRepository.All().ToList();
            objList.AddRange(data);
            objList.Add(new GST_TRN_OFFLINE_INVOICE());
            lv_InwardSupplies.DataSource = objList;
            lv_InwardSupplies.DataBind();
        }

        protected void lv_InwardSupplies_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddlUQC = (DropDownList)e.Item.FindControl("ddlUQC");
            HiddenField hdnUQC = (HiddenField)e.Item.FindControl("hdnUQC");
            List<ListItem> ss = new List<ListItem>();
            int i = 0;
            foreach (var c in Enum.GetValues(typeof(EnumConstants.Unit)))
            {
                ss.Add(new ListItem(c.ToString(), i.ToString()));
                i++;
            }
            if (ddlUQC != null)
            {
                ddlUQC.DataSource = typeof(EnumConstants.Unit).ToList();
                ddlUQC.DataTextField = "Value";
                ddlUQC.DataValueField = "Key";
                ddlUQC.DataBind();
                ddlUQC.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
                //if (hdnUQC.Value != null && hdnUQC.Value != "")
                //    ddlUQC.Items.FindByValue(hdnUQC.Value).Selected = true;

            }
        }


    }
}