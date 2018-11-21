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
    public partial class uc_ImportServices_GSTR2 : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindItems();
            }
        }

        public void BindItems()
        {
            List<GST_TRN_OFFLINE_INVOICE> objList = new List<GST_TRN_OFFLINE_INVOICE>();
            var data = unitOfwork.OfflineinvoiceRepository.All().ToList();
            objList.AddRange(data);
            objList.Add(new GST_TRN_OFFLINE_INVOICE());
            lv_ImportServices.DataSource = objList;
            lv_ImportServices.DataBind();
        }

        protected void lv_ImportServices_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddlPos = (DropDownList)e.Item.FindControl("ddlPos");
            HiddenField hdnPos = (HiddenField)e.Item.FindControl("hdnPos");
            if (ddlPos != null)
            {
                ddlPos.DataSource = unitOfwork.StateRepository.All().OrderBy(o => o.StateName).Select(x => new { TextField = x.StateCode + "-" + x.StateName, ValueField = x.StateID }).ToList();
                ddlPos.DataTextField = "TextField";
                ddlPos.DataValueField = "ValueField";
                ddlPos.DataBind();
                ddlPos.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
                if (hdnPos.Value != null && hdnPos.Value != "")
                    ddlPos.Items.FindByValue(hdnPos.Value).Selected = true;
            }
        }
    }
}
