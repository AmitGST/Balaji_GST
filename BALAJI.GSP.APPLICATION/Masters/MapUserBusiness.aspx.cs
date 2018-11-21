using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using System.Collections.Specialized;

namespace BALAJI.GSP.APPLICATION.Masters
{
    public partial class MapUserBusiness : System.Web.UI.Page
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserLists();
                BindBusinessType();
            }
        }

        private void BindUserLists()
        {
            var UserList = unitOfwork.AspnetRepository.Filter(f => f.RegisterWithUs == true).Select(s => new { id = s.Id, UserName = s.UserName }).ToList();
            ddlUserList.DataSource = UserList;
            ddlUserList.DataValueField = "id";
            ddlUserList.DataTextField = "UserName";
            ddlUserList.DataBind();
            ddlUserList.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
        }
        private void BindBusinessType()
        {
            lbBuisnessType.DataSource = unitOfwork.BuisnessTypeRepository.All().Select(f => new { BuisnessID = f.BusinessID, BuisnessType = f.BusinessType }).ToList();
            lbBuisnessType.DataValueField = "BuisnessID";
            lbBuisnessType.DataTextField = "BuisnessType";
            lbBuisnessType.DataBind();

            // lbBuisnessType.Items.Insert(0, new ListItem(" [ Select ] ", "0"));

        }


        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
         
            try
            {
                var ddlselectedvalue = ddlUserList.SelectedItem.Value;
                StringCollection sc = new StringCollection();
                  
                    foreach (int i in lbBuisnessType.GetSelectedIndices())
                    {
                        GST_MST_USER_BUSINESSTYPE bsnstype = new GST_MST_USER_BUSINESSTYPE();
                        bsnstype.BusinessID = Convert.ToInt32(lbBuisnessType.Items[i].Value);
                        bsnstype.UserID = ddlselectedvalue;
                        //var abc = unitOfwork.AspnetRepository.Filter(s => s.Id == a).Select(s => new { Id = s.Id });
                        bsnstype.CreatedBy = Common.LoggedInUserID();
                        bsnstype.CreatedDate = DateTime.Now;
                        unitOfwork.UserBuisnessTypeRepository.Create(bsnstype);
                        unitOfwork.Save();
                        uc_sucess.SuccessMessage = "Data Save Successfully.";
                      //  uc_sucess.Visible = !String.IsNullOrEmpty(SuccessMessage);
                    }
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
    }
}