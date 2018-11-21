using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
   
    public partial class uc_crdr_unregister_GSTR2 : System.Web.UI.UserControl
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
            lv_Crdr_Unregister.DataSource = objList;
            lv_Crdr_Unregister.DataBind();
        }

        protected void lv_Crdr_Unregister_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddl_SupplyType = (DropDownList)e.Item.FindControl("ddl_SupplyType");
            if (ddl_SupplyType != null)
            {
                ddl_SupplyType.DataSource = typeof(EnumConstants.IsInter).ToList();
                ddl_SupplyType.DataTextField = "Value";
                ddl_SupplyType.DataValueField = "Key";
                ddl_SupplyType.DataBind();
                ddl_SupplyType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
            }
            //Reason for issuing note
            DropDownList ddlIssuingNote = (DropDownList)e.Item.FindControl("ddlIssuingNote");
            HiddenField hdnIssuingNote = (HiddenField)e.Item.FindControl("hdnIssuingNote");
            if (ddlIssuingNote != null)
            {
                ddlIssuingNote.DataSource = unitOfwork.OfflineissuingnoteRepository.All().OrderBy(o => o.NoteCode).ToList();
                ddlIssuingNote.DataTextField = "IssuingNoteName";
                ddlIssuingNote.DataValueField = "NoteID";
                ddlIssuingNote.DataBind();
                ddlIssuingNote.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
                // if (hdnIssuingNote.Value != null && hdnIssuingNote.Value != "")
                // ddl_IssuingNote.Items.FindByText(hdnIssuingNote.Value).Selected = true;
            }
            DropDownList ddl_DocumentType = (DropDownList)e.Item.FindControl("ddl_DocumentType");
            HiddenField hdnDoc = (HiddenField)e.Item.FindControl("hdnDoc");
            List<ListItem> ss = new List<ListItem>();
            int i = 0;
            foreach (var c in Enum.GetValues(typeof(EnumConstants.NoteType)))
            {
                ss.Add(new ListItem(c.ToString(), i.ToString()));
                i++;
            }
            if (ddl_DocumentType != null)
            {
                ddl_DocumentType.DataSource = ss;
                ddl_DocumentType.DataTextField = "Text";
                ddl_DocumentType.DataValueField = "Value";
                ddl_DocumentType.DataBind();
                ddl_DocumentType.Items.Insert(0, new ListItem(" [ Select ] ", "-1"));
                if (hdnDoc.Value != null && hdnDoc.Value != "")
                    ddl_DocumentType.Items.FindByValue(hdnDoc.Value).Selected = true;
            }
        }
    }
}