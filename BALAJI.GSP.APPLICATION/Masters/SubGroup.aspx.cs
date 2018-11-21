using BusinessLogic;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    [Authorize("Admin")]
    public partial class SubGroup : System.Web.UI.Page
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

        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EnumControl.GetEnumDescriptions<EnumConstants.ItemType>(ddlItemType, false);
                ddlItemType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
                BindGroup();
                BindSubgrp();

            }
        }

        private void BindGroup()
        {


            EnumConstants.ItemType invType = (EnumConstants.ItemType)Enum.Parse(typeof(EnumConstants.ItemType), ddlItemType.SelectedValue);

            ddlGroup.DataSource = unitOfwork.GroupRepository.Filter(f => f.GroupType == (byte)invType && f.Status == true).OrderBy(o => o.GroupCode).Select(s => new { GroupCode = s.GroupCode, GroupID = s.GroupID }).ToList();
            ddlGroup.DataValueField = "GroupID";
            ddlGroup.DataTextField = "GroupCode";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        private void BindSubgrp()
        {
            lvSubgrp.DataSource = unitOfwork.SubGroupRepository.All().OrderBy(o => o.SubGroupCode).ToList();
            lvSubgrp.DataBind();

        }

        //old

        protected void btnSubGroup_Click(object sender, EventArgs e)
        {
            GST_MST_SUBGROUP subgroup = new GST_MST_SUBGROUP();

            try
            {
                //subgroup.GST_MST_GROUP.GroupType = (Byte)(EnumConstants.ItemType)Enum.Parse(typeof(EnumConstants.ItemType), ddlItemType.SelectedValue);
                subgroup.SubGroupCode = txtSubGrpCode.Text.Trim();
                subgroup.Description = txtDescription.Text.Trim();
                string sgid = Convert.ToString(ViewState["SubGroupID"]);
                if (sgid == "" || sgid == null)
                {
                    string sgCode = Convert.ToString(subgroup.SubGroupCode);
                    bool getsgCode = unitOfwork.SubGroupRepository.Contains(c => c.SubGroupCode == sgCode);
                    if (ddlGroup.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select Group Code";
                        uc_sucess.Visible = true;
                        return;
                    }
                    subgroup.GroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());
                    if (getsgCode)
                    {
                        uc_sucess.ErrorMessage = "Sub-Group code already exist.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    subgroup.Status = true;
                    unitOfwork.SubGroupRepository.Create(subgroup);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Subgroup successfully saved.";
                    uc_sucess.Visible = true;
                    BindSubgrp();
                    BindGroup();
                    ClearItem();
                }
                else
                {
                    string sgCode = Convert.ToString(subgroup.SubGroupCode);
                    bool getsgCode = unitOfwork.SubGroupRepository.Contains(c => c.SubGroupCode == sgCode);
                    if (ddlGroup.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "Kindly select Group Code";
                        uc_sucess.Visible = true;
                        return;
                    }
                    //if (getsgCode)
                    //{
                    //    uc_sucess.ErrorMessage = " Group code already exist.";
                    //    uc_sucess.VisibleError = true;
                    //    return;
                    //}
                    int id = Convert.ToInt32(sgid);
                    var getSubGroup = unitOfwork.SubGroupRepository.Filter(f => f.SubGroupID == id).FirstOrDefault();
                    // getSubGroup.GroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());
                    getSubGroup.SubGroupCode = txtSubGrpCode.Text.Trim();
                    getSubGroup.Description = txtDescription.Text.Trim();
                    unitOfwork.SubGroupRepository.Update(getSubGroup);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Sub-Group successfully Updated.";
                    uc_sucess.Visible = true;
                    sgid = string.Empty;
                    BindSubgrp();
                    BindGroup();
                    ClearItem();
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
            ddlItemType.SelectedIndex = 0;
            txtSubGrpCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void lvSubgrp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindSubgrp();
            DataPager1.DataBind();
        }

        protected void lkbSubGroup_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int subgroupID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["SubGroupID"] = subgroupID;

                    btnSubGroup.Attributes.Add("SubGroupID", lkbItem.CommandArgument);
                    var subgroup = unitOfwork.SubGroupRepository.Filter(f => f.SubGroupID == subgroupID).FirstOrDefault();

                    //ddlSubGroup.SelectedValue = Convert.ToString(classHSN.SubGroupID);
                    ddlGroup.SelectedValue = Convert.ToString(subgroup.GroupID);
                    txtSubGrpCode.Text = subgroup.SubGroupCode;
                    txtDescription.Text = subgroup.Description;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGroup();
           // ddlItemType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
    }
}
