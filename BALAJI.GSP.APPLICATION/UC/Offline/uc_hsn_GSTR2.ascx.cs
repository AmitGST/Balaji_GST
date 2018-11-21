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
    public partial class uc_hsn_GSTR2 : System.Web.UI.UserControl
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
            List<GST_TRN_OFFLINE_INVOICE_DATAITEM> objList = new List<GST_TRN_OFFLINE_INVOICE_DATAITEM>();
            var datas = unitOfwork.OfflineinvoicedataitemRepository.All().ToList();
            objList.AddRange(datas);
            objList.Add(new GST_TRN_OFFLINE_INVOICE_DATAITEM());
            lv_HSNInwardSupplies.DataSource = objList;
            lv_HSNInwardSupplies.DataBind();
        }

        public bool CheckEntityNull(object data)
        {
            if(string.IsNullOrEmpty(data as string))
                return true;
            return false;
        }

        protected void lv_HSNInwardSupplies_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddl_UQC = (DropDownList)e.Item.FindControl("ddl_UQC");
            if (ddl_UQC != null)
            {
                ddl_UQC.DataSource = typeof(EnumConstants.IsInter).ToList();
                ddl_UQC.DataTextField = "Value";
                ddl_UQC.DataValueField = "Key";
                ddl_UQC.DataBind();
                ddl_UQC.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            }

        }

    }
}