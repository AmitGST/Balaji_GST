using BusinessLogic;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Entity.Validation;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    [Authorize("Admin")]
    public partial class Class : System.Web.UI.Page
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
                BindSubGroup();
                BindClass();
            }
        }

        private void BindSubGroup()
        {
            ddlSubGroup.DataSource = unitOfwork.SubGroupRepository.Filter(f => f.GST_MST_GROUP.GroupType == 1 && f.Status == true).OrderBy(o => o.SubGroupCode).Select(s => new { SubGroupCode = s.SubGroupCode, SubGroupID = s.SubGroupID }).ToList();

            ddlSubGroup.DataValueField = "SubGroupID";
            ddlSubGroup.DataTextField = "SubGroupCode";
            ddlSubGroup.DataBind();
            ddlSubGroup.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }

        private void BindClass()
        {
            unitOfwork = new UnitOfWork();
            lvclass.DataSource = unitOfwork.ClassRepository.All().OrderBy(o => o.ClassCode).ToList();
            lvclass.DataBind();

        }

        protected void btnClass_Click(object sender, EventArgs e)
        {
            GST_MST_CLASS classs = new GST_MST_CLASS();

            try
            {
                //classs.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue.ToString());
                classs.ClassCode = txtClassCode.Text.Trim();
                classs.ClassDescription = txtDescription.Text.Trim();
                string cid = Convert.ToString(ViewState["ClassID"]);
                if (cid == "" || cid == null)
                {
                    string cCode = Convert.ToString(classs.ClassCode);
                    bool getcCode = unitOfwork.ClassRepository.Contains(c => c.ClassCode == cCode);
                    if (ddlSubGroup.SelectedIndex <= 0)
                    {
                        uc_sucess1.ErrorMessage = "!Kindly select group Name";
                        uc_sucess1.Visible = true;
                        return;
                    }
                    classs.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue.ToString());
                    if (getcCode)
                    {
                        uc_sucess1.ErrorMessage = "Class code already exist.";
                        uc_sucess1.VisibleError = true;
                        return;
                    }
                    classs.Status = true;
                    unitOfwork.ClassRepository.Create(classs);
                    unitOfwork.Save();
                    uc_sucess1.SuccessMessage = "Class successfully saved.";
                    uc_sucess1.Visible = true;
                    BindSubGroup();
                    BindClass();
                    ClearItem();
                }
                else
                {
                    string cCode = Convert.ToString(classs.ClassCode);
                    bool getcCode = unitOfwork.ClassRepository.Contains(c => c.ClassCode == cCode);
                    if (ddlSubGroup.SelectedIndex <= 0)
                    {
                        uc_sucess1.ErrorMessage = "!Kindly select Sub-Group Name";
                        uc_sucess1.Visible = true;
                        return;
                    }
                    int id = Convert.ToInt32(cid);
                    var getClass = unitOfwork.ClassRepository.Filter(f => f.ClassID == id).FirstOrDefault();
                    //getClass.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue.ToString());
                    getClass.ClassCode = txtClassCode.Text.Trim();
                    getClass.ClassDescription = txtDescription.Text.Trim();
                    unitOfwork.ClassRepository.Update(getClass);
                    unitOfwork.Save();
                    uc_sucess1.SuccessMessage = "Class successfully updated.";
                    uc_sucess1.Visible = true;
                    cid = string.Empty;
                    BindClass();
                    BindSubGroup();
                    ClearItem();
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
                uc_sucess1.ErrorMessage = ex.Message;
                uc_sucess1.VisibleError = true;
            }
        }

        private void ClearItem()
        {
            txtClassCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void lvclass_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindClass();
            DataPager1.DataBind();
        }
        protected void lkbClass_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int classID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["ClassID"] = classID;
                    btnClass.Attributes.Add("ClassID", lkbItem.CommandArgument);

                    var classHSN = unitOfwork.ClassRepository.Filter(f => f.ClassID == classID).FirstOrDefault();
                    ddlSubGroup.SelectedValue = Convert.ToString(classHSN.SubGroupID);
                    txtClassCode.Text = classHSN.ClassCode;
                    txtDescription.Text = classHSN.ClassDescription;
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
            }
        }
    }
}