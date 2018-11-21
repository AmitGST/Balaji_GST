using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace BALAJI.GSP.APPLICATION.Masters
{
    [Authorize("Admin")]
    public partial class Group : System.Web.UI.Page
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
                BindItems();
            }
        }
        private void BindItems()
        {
            EnumControl.GetEnumDescriptions<EnumConstants.ItemType>(ddlItemType, false);
            ddlItemType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

            var userID = Common.LoggedInUserID();
            lvItems.DataSource = unitOfwork.GroupRepository.All().OrderBy(o => o.GroupCode).ToList();
            lvItems.DataBind();
        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            GST_MST_GROUP item = new GST_MST_GROUP();
            try
            {
                EnumConstants.ItemType invType = (EnumConstants.ItemType)Enum.Parse(typeof(EnumConstants.ItemType), ddlItemType.SelectedValue);

                item.GroupType = (Byte)invType;
                item.GroupCode = txtGroupCode.Text.Trim();
                item.Description = txtDescription.Text.Trim();
                string gid = Convert.ToString(ViewState["GroupID"]);
                if (gid == "" || gid == null)
                {
                    string gCode = Convert.ToString(item.GroupCode);
                    bool getgCode = unitOfwork.GroupRepository.Contains(c => c.GroupCode == gCode);

                    if (getgCode)
                    {
                        uc_sucess.ErrorMessage = "Group code already exist.";
                        uc_sucess.VisibleError = true;
                        return;
                    }

                    item.Status = true;
                    unitOfwork.GroupRepository.Create(item);
                    unitOfwork.Save();
                    gid = string.Empty;
                    uc_sucess.SuccessMessage = "Group created successfully.";
                    uc_sucess.Visible = true;
                    BindItems();
                    ClearItem();
                }
                else
                {
                    string gCode = Convert.ToString(item.GroupCode);
                    bool getgCode = unitOfwork.GroupRepository.Contains(c => c.GroupCode == gCode);

                    //if (getgCode)
                    //{
                    //    uc_sucess.ErrorMessage = "Group code already exist.";
                    //    uc_sucess.VisibleError = true;
                    //    return;
                    //}
                    int id = Convert.ToInt32(gid);
                    var getGroup = unitOfwork.GroupRepository.Filter(f => f.GroupID == id).FirstOrDefault();
                    getGroup.GroupType = Convert.ToByte(ddlItemType.SelectedIndex.ToString());
                    //getGroup.GroupCode = txtGroupCode.Text.Trim();
                    getGroup.Description = txtDescription.Text.Trim();
                    unitOfwork.GroupRepository.Update(getGroup);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Group successfully Updated.";
                    uc_sucess.Visible = true;
                    gid = string.Empty;
                    BindItems();
                    ClearItem();

                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());

                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                uc_sucess.ErrorMessage = ex.Message;
                uc_sucess.VisibleError = true;
            }
        }

        private void ClearItem()
        {
            txtGroupCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void lvItems_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindItems();
            DataPager1.DataBind();
        }
        protected void lkbGroup_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int groupID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["GroupID"] = groupID;
                    btnGroup.Attributes.Add("GroupID", lkbItem.CommandArgument);

                    var group = unitOfwork.GroupRepository.Filter(f => f.GroupID == groupID).FirstOrDefault();
                    txtGroupCode.Text = group.GroupCode;
                    txtDescription.Text = group.Description;
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}