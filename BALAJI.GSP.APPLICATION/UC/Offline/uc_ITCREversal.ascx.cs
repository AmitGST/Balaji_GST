using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Repositories;
using DataAccessLayer;
using GST.Utility;

namespace BALAJI.GSP.APPLICATION.UC.Offline
{
    public partial class uc_ITCREversal : System.Web.UI.UserControl
    {
        UnitOfWork unitOfwork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();
            }
        }

        public void BindItems()
        {
            List<GST_TRN_OFFLINE_INVOICE_DATAITEM> objList = new List<GST_TRN_OFFLINE_INVOICE_DATAITEM>();
            var data = unitOfwork.OfflineinvoicedataitemRepository.All().ToList();
            objList.AddRange(data);
            objList.Add(new GST_TRN_OFFLINE_INVOICE_DATAITEM());
            lv_ITC_Reversal.DataSource = objList;
            lv_ITC_Reversal.DataBind();
        }
    }
}