using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_LeftMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Common.UserProfile.SmallImage != null)
            {
                uc_Header_Image1.ImageUrl = Common.GetImageFromByte(Common.UserProfile.SmallImage);

            }
            else
            {

                uc_Header_Image1.ImageUrl = "~/dist/img/avatar5.png";
            }
            ShowButton();
        }

        private void ShowButton()
        {
            if (Common.IsAdmin())
            {
                
            }
            else if (Common.IsTaxConsultant())
            {
                liInvoice.Visible = true;
            }
            else
            {
                liInvoice.Visible = true;  

            }
        }
    }
}