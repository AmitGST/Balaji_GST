using BALAJI.GSP.APPLICATION.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using GST.Utility;
using BusinessLogic;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_CreateBuisnessType : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindBusinessType();

        }

        protected void btnCreateBuisnessType_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdn_BusinessTypeId.Value as string))
                {
                    if (txtBuisnessTypeName.Text.Trim() != "")
                    {
                        if (unitOfwork.BuisnessTypeRepository.Contains(f => f.BusinessType == txtBuisnessTypeName.Text.Trim()))
                        {
                            uc_sucess.ErrorMessage = "Business type already exists!";
                        }
                        else
                        {
                            GST_MST_BUSINESSTYPE ob = new GST_MST_BUSINESSTYPE();
                            ob.BusinessType = txtBuisnessTypeName.Text.Trim();
                            ob.CreatedDate = DateTime.Now;
                            ob.CreatedBy = Common.LoggedInUserID();
                            unitOfwork.BuisnessTypeRepository.Create(ob);
                            unitOfwork.Save();
                            uc_sucess.SuccessMessage = "Business type created successfully";
                            hdn_BusinessTypeId.Value = null;
                            txtBuisnessTypeName.Text = "";
                            BindBusinessType();
                        }
                    }
                    else
                    {
                        uc_sucess.ErrorMessage = "Business type Cannot be null";
                    }
                }
                else
                {
                    if (txtBuisnessTypeName.Text.Trim() != "")
                    {
                        if (unitOfwork.BuisnessTypeRepository.Contains(f => f.BusinessType == txtBuisnessTypeName.Text.Trim()))
                        {
                            uc_sucess.ErrorMessage = "Business type already exists!";
                        }
                        else
                        {
                            int businessId = Convert.ToInt32(hdn_BusinessTypeId.Value);
                            var entity = unitOfwork.BuisnessTypeRepository.Filter(x => x.BusinessID == businessId).SingleOrDefault();
                            entity.BusinessType = txtBuisnessTypeName.Text.Trim();
                            entity.UpdatedDate = DateTime.Now;
                            entity.UpdatedBy = Common.LoggedInUserID();
                            unitOfwork.BuisnessTypeRepository.Update(entity);
                            unitOfwork.Save();
                            uc_sucess.SuccessMessage = "Business type updated successfully";
                            hdn_BusinessTypeId.Value = null;
                            txtBuisnessTypeName.Text = "";
                            BindBusinessType();
                        }
                    }
                    else
                    {
                        uc_sucess.ErrorMessage = "Business type Cannot be null";
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void BindBusinessType()
        {
            unitOfwork = new UnitOfWork();
            var userID = Common.LoggedInUserID();
            lvBusiness.DataSource = unitOfwork.BuisnessTypeRepository.All().OrderBy(o => o.BusinessType).ToList();
            lvBusiness.DataBind();
        }

        protected void lvBusinessType(object sender, PagePropertiesChangingEventArgs e)
        {
            dpBusinessType.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindBusinessType();
            dpBusinessType.DataBind();
        }

        protected void lkbEdit_Click(object sender, EventArgs e)
         {
            try
            {
                LinkButton lkbItem = (LinkButton)sender;
                hdn_BusinessTypeId.Value = lkbItem.CommandArgument;
                int businessId = Int32.Parse(lkbItem.CommandArgument);
                txtBuisnessTypeName.Text = unitOfwork.BuisnessTypeRepository.Filter(f => f.BusinessID == businessId).Select(x => x.BusinessType).SingleOrDefault();
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }
       
        protected void RemoveBusinessType(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnDelete = (LinkButton)sender;
                if (!string.IsNullOrEmpty(btnDelete.CommandArgument as string))
                {
                    int BusinessId = Int32.Parse(btnDelete.CommandArgument);
                    var entity = unitOfwork.BuisnessTypeRepository.Filter(f => f.BusinessID == BusinessId).SingleOrDefault();
                    if (entity != null)
                    {
                        List<GST_MST_USER_BUSINESSTYPE> Objbusinesstype = unitOfwork.UserBuisnessTypeRepository.Filter(f => f.BusinessID == BusinessId).ToList();

                        foreach (var item in Objbusinesstype)
                        {
                            unitOfwork.UserBuisnessTypeRepository.Delete(item);
                            unitOfwork.Save();
                        }
                        unitOfwork.BuisnessTypeRepository.Delete(entity);
                        unitOfwork.Save();
                        BindBusinessType();
                        uc_sucess.SuccessMessage = "Business Type deleted successfully";
                        uc_sucess.Visible = true;
                    }
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