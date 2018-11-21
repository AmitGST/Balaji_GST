using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.B2B.GST.GSTInvoices
{
    public enum InvoiceType
    {
        B2BInvoice,
        AmendedB2BInvoice,
        B2CLargeInvoice,
        AmendedB2CLargeInvoice,
        B2BCreditNotesInvoice,
        B2BDebitNotesInvoice,
        AmendedB2BCreditNotesInvoice,
        AmendedB2BDebitNotesInvoice,
        B2BExportInvoice,
        AmendedB2BExportInvoice
    }

    public static  class InvoiceFactory
    {
        public static Invoice CreateInvoice(InvoiceType invoiceType)
        {
            Invoice invoice = null;
            switch(invoiceType)
            {
                    case InvoiceType.B2BInvoice:
                    invoice = new B2BInvoice();
                    break;

                    case InvoiceType.AmendedB2BInvoice:
                    invoice = new AmendedB2BInvoice();
                    break;

                    case InvoiceType.B2CLargeInvoice:
                    invoice = new B2CLargeInvoice();
                    break;

                    case InvoiceType.AmendedB2CLargeInvoice:
                    invoice = new AmendedB2CLargeInvoice();
                    break;

                    case InvoiceType.B2BCreditNotesInvoice:
                    invoice = new CreditNotesInvoice();
                    break;

                    case InvoiceType.B2BDebitNotesInvoice:
                    invoice = new B2BDebitNotesInvoice();
                    break;

                    case InvoiceType.AmendedB2BCreditNotesInvoice:
                    invoice = new AmendedCreditNotesInvoice();
                    break;

                    case InvoiceType.AmendedB2BDebitNotesInvoice:
                    invoice = new AmendedDebitNotesInvoice();
                    break;

                    case InvoiceType.B2BExportInvoice:
                    invoice = new B2BExportInvoice();
                    break;

                    case InvoiceType.AmendedB2BExportInvoice:
                    invoice = new AmendedB2BExportInvoice();
                    break;

                   default:
                    break;

            }
            return invoice;
        }

    }
}