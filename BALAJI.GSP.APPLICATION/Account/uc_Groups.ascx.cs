using BALAJI.GSP.APPLICATION.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Account
{
    public partial class uc_Groups : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<ApplicationGroup> Groups()
        {
            var groupAll = this.GroupManager.Groups.OrderBy(o => o.Name);
            return groupAll;
        }

        private ApplicationGroupManager _groupManager;
        public ApplicationGroupManager GroupManager
        {
            get
            {
                return _groupManager ?? new ApplicationGroupManager();
            }
            private set
            {
                _groupManager = value;
            }
        }

        public void BindGroups(bool isBind)
        {
            if (isBind)
            {
                cbGroupList.DataSource = this.Groups().ToList();
                cbGroupList.DataBind();
            }
            else
            {
                cbGroupList.DataSource = null;
                cbGroupList.DataBind();
            }
        }

        public List<ApplicationGroup> SetGroup 
        {
            set
            {
                foreach(ApplicationGroup group in value)
                {
                    cbGroupList.Items.FindByValue(group.Id).Selected = true;
                }
            }
        }

        public List<string> SelectedGroupList()
        {
            List<string> items = new List<string>();
            foreach (ListItem li in cbGroupList.Items)
            {
                if (li.Selected)
                    items.Add(li.Value);
            }
            return items;
        }

        

    }
}