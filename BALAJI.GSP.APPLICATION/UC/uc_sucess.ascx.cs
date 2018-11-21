using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.UC
{
    public partial class uc_sucess : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string SuccessMessage
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public bool Visible { get; set; }

        public bool VisibleError { get; set; }
      
    }
}