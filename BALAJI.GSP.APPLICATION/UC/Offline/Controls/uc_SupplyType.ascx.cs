using BusinessLogic.Repositories;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC.Offline.Controls
{
    public partial class uc_SupplyType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        UnitOfWork unitofWork;
        public void BindItems()
        {
            try
            {
                unitofWork = new UnitOfWork();
                ddlPos.Items.Clear();
                ddlSupplyType.Items.Clear();
                var data = unitofWork.StateRepository.All().OrderBy(o => o.StateCode).Select(x => new { TextField = x.StateCode + "-" + x.StateName, ValueField = x.StateID.ToString() }).ToList();
                data.Insert(0, new { TextField = "[ SELECT ]", ValueField = "0" });
                ddlPos.DataSource = data;
                ddlPos.DataTextField = "TextField";
                ddlPos.DataValueField = "ValueField";
                ddlPos.DataBind();
                if (ddlSupplyType != null)
                {
                    foreach (EnumConstants.IsInter r in Enum.GetValues(typeof(EnumConstants.IsInter)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.IsInter), r), Convert.ToByte(r).ToString());
                        ddlSupplyType.Items.Add(item);
                    }

                    ddlSupplyType.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void ddlPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var UserId = Common.LoggedInUserID();
                var POS_StateId = Convert.ToInt32(ddlPos.SelectedValue);
                var POS_StateCode = unitofWork.StateRepository.Filter(x => x.StateID == POS_StateId).SingleOrDefault().StateCode;
                var StateCode = unitofWork.AspnetRepository.Filter(x => x.Id == UserId).SingleOrDefault().StateCode;
                if (StateCode == POS_StateCode)
                {
                    //IntraState Enum Value
                    ddlSupplyType.SelectedValue = "1";
                }
                else
                {
                    //InterState Enum Value
                    ddlSupplyType.SelectedValue = "0";
                }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public string ddlPos_SelectedValue
        {
            get
            {
                return ddlPos.SelectedValue;
            }
            set
            {
                ddlPos.SelectedValue = value;
            }
        }

        public string ddlSupplyType_SelectedValue
        {
            get
            {
                return ddlSupplyType.SelectedValue;
            }
            set
            {
                ddlSupplyType.SelectedValue = value;
            }
        }

        public int ddlPos_SelectedIndex
        {
            get
            {
                return ddlPos.SelectedIndex;
            }
            set
            {
                ddlPos.SelectedIndex = value;
            }
        }

        public int ddlSupplyType_SelectedIndex
        {
            get
            {
                return ddlSupplyType.SelectedIndex;
            }
            set
            {
                ddlSupplyType.SelectedIndex = value;
            }
        }


        public bool ddlPos_enable
        {
            set
            {
                ddlPos.Enabled = value;
            }
        }

    }
}