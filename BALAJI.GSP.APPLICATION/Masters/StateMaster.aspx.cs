using BusinessLogic;
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
    [Authorize("Admin")]
    public partial class StateMaster : System.Web.UI.Page
    {
        //protected override void OnPreInit(EventArgs e)
        //{
        //    if (!Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/User/User.master";
        //    }
        //    if (Common.IsAdmin())
        //    {
        //        this.MasterPageFile = "~/Admin/Admin.master";
        //    }
        //    base.OnPreInit(e);
        //}


        private UnitOfWork unitOfWork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindState();
                BindISExempted();
            }

        }
        //public string StateID
        //{
        //    get
        //    {
        //        if ((ViewState["StateID"]) != null)
        //        {
        //            int id = (int)ViewState["StateID"];
        //            return id.ToString();
        //        }
        //       // int? id = (int)ViewState["StateID"];
        //       // return (id == null) ? String.Empty : ((int)id).ToString();
        //    }
        //    set
        //    {
        //        ViewState["StateID"] = value;
        //    }
        //}
        private void BindState()
        {

            lvstate.DataSource = unitOfWork.StateRepository.All().OrderBy(o => o.StateName).ToList();
            lvstate.DataBind();

        }

        protected void btnAddState_Click(object sender, EventArgs e)
        {
            GST_MST_STATE state = new GST_MST_STATE();
            try
            {
                //Button btn=(Button)sender;

                //var i = btn.Attributes["StateID"].SingleOrDefault(); 


                state.StateName = txtStateName.Text.Trim().ToString().ToUpper();
                state.StateCode = txtStateCode.Text.Trim();

                state.CompCeilingAmount = Convert.ToDecimal(txtCompInt.Text.Trim());
                if(chkUT!= null)
                state.UT = chkUT.Checked;
                //chkUT.Checked = state.UT.HasValue ? state.UT.Value : false;
                state.Status = true;
                state.IsExempted = ddlExempted.SelectedIndex > 0 ? Convert.ToBoolean(ddlExempted.SelectedItem.Text) : false;
                state.CreatedDate = DateTime.Now;
                //state.CreatedDate = DateTime.Now;
                string sid = Convert.ToString(ViewState["StateID"]);
                if (sid == "" || sid == null)
                {
                    string sCode = Convert.ToString(state.StateCode);
                    bool getStateCode = unitOfWork.StateRepository.Contains(c => c.StateCode == sCode);

                    if (getStateCode)
                    {
                        uc_sucess.ErrorMessage = "State code already exist.";
                        uc_sucess.VisibleError = true;
                        return;
                    }

                    unitOfWork.StateRepository.Create(state);
                    unitOfWork.Save();
                    sid = string.Empty;
                    uc_sucess.SuccessMessage = "State successfully saved.";
                    uc_sucess.Visible = true;
                    ClearItem();
                    BindState();
                }
                else
                {
                    //string sCode = Convert.ToString(state.StateCode);
                    //bool getStateCode = unitOfWork.StateRepository.Contains(c => c.StateCode == sCode);

                    //if (getStateCode)
                    //{
                    //    uc_sucess.ErrorMessage = "State code already exist.";
                    //    uc_sucess.VisibleError = true;
                    //    return;
                    //}
                    int id = Convert.ToInt32(sid);
                    var getState = unitOfWork.StateRepository.Filter(f => f.StateID == id).FirstOrDefault();
                    getState.StateName = txtStateName.Text.Trim();
                    // getState.StateCode = txtStateCode.Text.Trim();
                    getState.CompCeilingAmount = Convert.ToDecimal(txtCompInt.Text.Trim());
                    getState.IsExempted = Convert.ToBoolean(ddlExempted.SelectedItem.Text);
                    getState.UT = chkUT.Checked;
                    unitOfWork.StateRepository.Update(getState);
                    unitOfWork.Save();
                    uc_sucess.SuccessMessage = "State successfully Updated.";
                    uc_sucess.Visible = true;
                    sid = string.Empty;
                    ClearItem();
                    BindState();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess.ErrorMessage = ex.Message;
                uc_sucess.VisibleError = true;
            }
        }

        private void ClearItem()
        {
            txtStateCode.Text = string.Empty;
            txtStateName.Text = string.Empty;
            txtCompInt.Text = string.Empty;
        }

        protected void lvstate_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindState();
            DataPager1.DataBind();
        }

        protected void lkbState_action_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lkbItem = (LinkButton)sender;

                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int stateID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["StateID"] = stateID;

                    btnAddState.Attributes.Add("StateID", lkbItem.CommandArgument);
                    var state = unitOfWork.StateRepository.Filter(f => f.StateID == stateID).FirstOrDefault();

                    txtStateCode.Text = state.StateCode;
                    txtStateName.Text = state.StateName;
                    txtCompInt.Text = state.CompCeilingAmount.HasValue ? state.CompCeilingAmount.ToString() : "";
                    chkUT.Checked = state.UT.HasValue ? state.UT.Value : false;

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void BindISExempted()
        {
            ddlExempted.DataSource = Enumeration.GetAll<EnumConstants.BoolStatus>();
            ddlExempted.DataTextField = "Value";
            ddlExempted.DataValueField = "Key";
            ddlExempted.DataBind();
          //  ddlExempted.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }

    }
}