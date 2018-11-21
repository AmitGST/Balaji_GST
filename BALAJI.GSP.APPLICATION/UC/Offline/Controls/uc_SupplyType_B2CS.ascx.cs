using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline.Controls
{
    public partial class uc_SupplyType_B2CS : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        UnitOfWork unitofWork;
        public void BindItems()
        {
            try
            {
                unitofWork = new UnitOfWork();
                ddlPos.Items.Clear();
                ddl_SupplyType.Items.Clear();
                ddlRate.Items.Clear();
                var data = unitofWork.StateRepository.All().OrderBy(o => o.StateCode).Select(x => new { TextField = x.StateCode + "-" + x.StateName, ValueField = x.StateID.ToString() }).ToList();
                data.Insert(0, new { TextField = "[ SELECT ]", ValueField = "0" });
                ddlPos.DataSource = data;
                ddlPos.DataTextField = "TextField";
                ddlPos.DataValueField = "ValueField";
                ddlPos.DataBind();
                if (ddl_SupplyType != null)
                {
                    foreach (EnumConstants.IsInter r in Enum.GetValues(typeof(EnumConstants.IsInter)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(EnumConstants.IsInter), r), Convert.ToByte(r).ToString());
                        ddl_SupplyType.Items.Add(item);
                    }

                    ddl_SupplyType.Items.Insert(0, new ListItem(" [ SELECT ] ", "-1"));
                }

                var data2 = unitofWork.OfflinerateRepository.All().Select(x => new { TextField = x.RATE.ToString(), ValueField = x.RATE_ID.ToString() }).ToList();
                if (ddlRate != null)
                {
                    ddlRate.DataSource = data2;
                    ddlRate.DataValueField = "ValueField";
                    ddlRate.DataTextField = "TextField";
                    ddlRate.DataBind();
                    ddlRate.Items.Insert(0, new ListItem(" [ SELECT ]  ", "-1"));
                }
            }
            catch (Exception ex)
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
                return ddl_SupplyType.SelectedValue;
            }
            set
            {
                ddl_SupplyType.SelectedValue = value;
            }
        }

        public string ddlRate_SelectedValue
        {
            get
            {
                return ddlRate.SelectedValue;
            }
            set
            {
                ddlRate.SelectedValue = value;
            }
        }

        public int ddlRate_SelectedIndex
        {
            get
            {
                return ddlRate.SelectedIndex;
            }
            set
            {
                ddlRate.SelectedIndex = value;
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
                return ddl_SupplyType.SelectedIndex;
            }
            set
            {
                ddl_SupplyType.SelectedIndex = value;
            }
        }

        public object TotalTaxable_Value
        {
            get
            {
                return Convert.ToDecimal(txt_Taxable_value.Text);
            }
            set
            {
                txt_Taxable_value.Text = Convert.ToString(value);
            }
        }

        public object IntegratedTax
        {
            get
            {
                return Convert.ToDecimal(txtIntegratedTax.Text);
            }
            set
            {
                txtIntegratedTax.Text = Convert.ToString(value);
            }
        }
        public object CentralTax
        {
            get
            {
                return Convert.ToDecimal(txtCentralTax.Text);
            }
            set
            {
                txtCentralTax.Text = Convert.ToString(value);
            }
        }

        public object StateTax
        {
            get
            {
                return Convert.ToDecimal(txt_SGSTUTGST.Text);
            }
            set
            {
                txt_SGSTUTGST.Text = Convert.ToString(value);
            }
        }

        //disable Feilds
        public bool TotalTaxable_enable
        {
            set
            {
                txt_Taxable_value.Enabled = value;
            }
        }
        public bool IntegratedTax_enable
        {
            set
            {
                txtIntegratedTax.Enabled = value;

            }
        }
        public bool CentralTax_enable
        {
            set
            {
                txtCentralTax.Enabled = value;
            }
        }

        public bool StateTax_enable
        {
            set
            {
                txt_SGSTUTGST.Enabled = value;
            }
        }

        public bool Cess_enable
        {
            set
            {
                txtCess.Enabled = value;
            }
        }
        //
        public object Cess
        {
            get
            {
                return Convert.ToDecimal(txtCess.Text);
            }
            set
            {
                txtCess.Text = Convert.ToString(value);
            }
        }
        public bool ddlPos_enable
        {
            set
            {
                ddlPos.Enabled = value;
            }
        }
        public bool ddlRate_enable
        {
            set
            {
                ddlRate.Enabled = value;
            }
        }

        protected void ddlPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Taxable_value.Text.Trim() == "")
                {
                    txt_Taxable_value.Text = "0.0";
                }
                var UserId = Common.LoggedInUserID();
                var POS_StateId = Convert.ToInt32(ddlPos.SelectedValue);
                var POS_StateCode = unitofWork.StateRepository.Filter(x => x.StateID == POS_StateId).SingleOrDefault().StateCode;
                var StateCode = unitofWork.AspnetRepository.Filter(x => x.Id == UserId).SingleOrDefault().StateCode;
                var RateAmount = (Convert.ToDecimal(txt_Taxable_value.Text) * Convert.ToDecimal(ddlRate.Text)) / 100;
                if (StateCode == POS_StateCode)
                {
                    //IntraState Enum Value
                    ddl_SupplyType.SelectedValue = "1";
                    txtIntegratedTax.Text = "0.0";
                    txt_SGSTUTGST.Enabled = true;
                    txtCentralTax.Enabled = true;
                    txt_SGSTUTGST.Text = RateAmount <= 0 ? txt_Taxable_value.Text : (RateAmount / 2).ToString();
                    txtCentralTax.Text = RateAmount <= 0 ? txt_Taxable_value.Text : (RateAmount / 2).ToString();
                    txtIntegratedTax.Enabled = false;

                }
                else
                {
                    //InterState Enum Value
                    ddl_SupplyType.SelectedValue = "0";
                    txtCentralTax.Text = "0.0";
                    txt_SGSTUTGST.Text = "0.0";
                    txtCentralTax.Enabled = false;
                    txt_SGSTUTGST.Enabled = false;
                    txtIntegratedTax.Text = RateAmount <= 0 ? txt_Taxable_value.Text : RateAmount.ToString();
                    txtIntegratedTax.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        public void RateCalculate()
        {
            try
            {
                var RateAmount = ((Convert.ToDecimal(txt_Taxable_value.Text) * Convert.ToDecimal(ddlRate.SelectedItem.Text)) / 100);
                if (ddlSupplyType_SelectedValue == "1")
                {
                    //IntraState Enum Value
                    txtIntegratedTax.Text = "0.0";
                    txt_SGSTUTGST.Enabled = true;
                    txtCentralTax.Enabled = true;
                    txt_SGSTUTGST.Text = RateAmount <= 0 ? txt_Taxable_value.Text : (RateAmount / 2).ToString();
                    txtCentralTax.Text = RateAmount <= 0 ? txt_Taxable_value.Text : (RateAmount / 2).ToString();
                    txtIntegratedTax.Enabled = false;

                }
                else if (ddlSupplyType_SelectedValue == "0")
                {
                    txtCentralTax.Text = "0.0";
                    txt_SGSTUTGST.Text = "0.0";
                    txtCentralTax.Enabled = false;
                    txt_SGSTUTGST.Enabled = false;
                    txtIntegratedTax.Text = RateAmount <= 0 ? txt_Taxable_value.Text : RateAmount.ToString();
                    txtIntegratedTax.Enabled = true;
                }
            }
               
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }


        protected void ddlRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RateCalculate();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void txt_Taxable_value_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RateCalculate();
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }



    }

}