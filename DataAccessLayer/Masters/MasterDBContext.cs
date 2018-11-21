using BusinessLogic;
//using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Masters
{
    public class MasterDBContext : DbContext
    {
        public MasterDBContext()
            : base()
        {
       
        }
        public DbSet<clsState> GST_MST_States { get; set; }
    }
}
