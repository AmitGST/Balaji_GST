using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_modelUser : System.Web.UI.UserControl
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void GetUsers(string userId)
        {
            var signUser = unitOfWork.UserSignatoryRepository.Filter(f => f.Id == userId);
            lvSignatory.DataSource = signUser.ToList();
            lvSignatory.DataBind();
        }
        public string UserStateName(object stateName)
        {
            var Name = unitOfWork.StateRepository.Find(f => f.StateCode == stateName.ToString()).StateName;
            return Name;
        }
        public string UserID
        {
            get
            {
                return ViewState["UserID"].ToString();
            }
            set
            {
                GetUsers(value);
            }
        }
    }
}