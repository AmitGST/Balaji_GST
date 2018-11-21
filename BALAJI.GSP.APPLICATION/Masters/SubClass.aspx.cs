using BusinessLogic;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Masters
{
    [Authorize("Admin")]
    public partial class SubClass : System.Web.UI.Page
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

                BindClass();
                BindSubClass();
            }
        }

        private void BindClass()
        {
            ddlClass.DataSource = unitOfwork.ClassRepository.Filter(f => f.Status == true).OrderBy(o => o.ClassCode).Select(s => new { ClassCode = s.ClassCode, ClassID = s.ClassID }).ToList();

            ddlClass.DataValueField = "ClassID";
            ddlClass.DataTextField = "ClassCode";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }


        private void BindSubClass()
        {
            lvsubclass.DataSource = unitOfwork.SubClassRepository.All().OrderBy(o => o.SubClassCode).ToList();
            lvsubclass.DataBind();
        }

        protected void btnSubClass_Click(object sender, EventArgs e)
        {

            GST_MST_SUBCLASS subclass = new GST_MST_SUBCLASS();
            try
            {
                subclass.SubClassCode = txtSubClsCode.Text.Trim();
                subclass.Description = txtDescription.Text.Trim();
                string scid = Convert.ToString(ViewState["SubClassID"]);
                if (scid == "" || scid == null)
                {
                    string scCode = Convert.ToString(subclass.SubClassCode);
                    bool getscCode = unitOfwork.SubClassRepository.Contains(c => c.SubClassCode == scCode);
                    if (ddlClass.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select Class Name";
                        uc_sucess.Visible = true;
                        return;
                    }
                    if (getscCode)
                    {
                        uc_sucess.ErrorMessage = "Sub-Class code already exist.";
                        uc_sucess.VisibleError = true;
                        return;
                    }
                    subclass.Status = true;
                    subclass.ClassID = Convert.ToInt32(ddlClass.SelectedValue.ToString());
                    unitOfwork.SubClassRepository.Create(subclass);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Sub-Class successfully saved.";
                    uc_sucess.Visible = true;
                    BindClass();
                    BindSubClass();
                    ClearItem();
                }
                else
                {
                    string scCode = Convert.ToString(subclass.SubClassCode);
                    bool getscCode = unitOfwork.SubClassRepository.Contains(c => c.SubClassCode == scCode);
                    if (ddlClass.SelectedIndex <= 0)
                    {
                        uc_sucess.ErrorMessage = "!Kindly select Class Name";
                        uc_sucess.Visible = true;
                        return;
                    }
                    int subcId = Convert.ToInt32(scid);
                    var getSubClass = unitOfwork.SubClassRepository.Filter(f => f.SubClassID == subcId).FirstOrDefault();
                    //  getSubClass.ClassID = Convert.ToInt32(ddlClass.SelectedValue.ToString());
                    getSubClass.SubClassCode = txtSubClsCode.Text.Trim();
                    getSubClass.Description = txtDescription.Text.Trim();
                    unitOfwork.SubClassRepository.Update(getSubClass);
                    unitOfwork.Save();
                    uc_sucess.SuccessMessage = "Sub-Class successfully updated.";
                    uc_sucess.Visible = true;
                    scid = string.Empty;
                    BindClass();
                    BindSubClass();
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
            ddlClass.DataTextField = string.Empty;
            txtSubClsCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void lvsubclass_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindSubClass();
            DataPager1.DataBind();
        }
        protected void lkbSubClass_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                if (!string.IsNullOrEmpty(lkbItem.CommandArgument.ToString()))
                {
                    int subclassID = Convert.ToInt32(lkbItem.CommandArgument);
                    ViewState["SubClassID"] = subclassID;
                    btnSubClass.Attributes.Add("SubClassID", lkbItem.CommandArgument);
                    var subclass = unitOfwork.SubClassRepository.Filter(f => f.SubClassID == subclassID).FirstOrDefault();

                    ddlClass.SelectedValue = Convert.ToString(subclass.ClassID);
                    txtSubClsCode.Text = subclass.SubClassCode;
                    txtDescription.Text = subclass.Description;
                    uc_sucess.Visible = false;
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