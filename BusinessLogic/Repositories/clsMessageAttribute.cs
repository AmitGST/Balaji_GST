using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class clsMessageAttribute
    {
        public string UserName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceAction { get; set; }
        public string InvoiceTotalAmount { get; set; }
        public List<string> MailsTo { get; set; }
        public List<string> MailsCc { get; set; }
        public string MailString { get; set; }
        public string Message { get; set; }
        public string CustomMessage { get; set; }
    }
}
