using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.DataSetExtensions;
namespace BusinessLogic.Repositories
{
    public class cls_OfflineData : GST_TRN_OFFLINE
    {
        UnitOfWork unitOfWork;

        public cls_OfflineData()
        {
            unitOfWork = new UnitOfWork();
        }

        public void SaveOfflineData(DataSet dataSetInvoices, string SupplierGSTIN, string UserId)
        {
            try
            {
                foreach (EnumConstants.OfflineSheetName sheetName in Enum.GetValues(typeof(EnumConstants.OfflineSheetName)))
                {
                    if (sheetName == EnumConstants.OfflineSheetName.B2B)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2B);
                        var dt = Get5RowOnward(table);
                        var invoice = from item in dt.AsEnumerable()
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          Item_ID = FindItemId(item.ItemArray[12].ToString()),
                                          GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2B), UserId),
                                          Qty = decimal.Parse(item.ItemArray[11].ToString()),
                                          Rate = decimal.Parse(item.ItemArray[8].ToString()),
                                          TaxableAmount = decimal.Parse(item.ItemArray[9].ToString()),
                                          CessAmt = decimal.Parse(item.ItemArray[10].ToString()),
                                          Discount = decimal.Parse(item.ItemArray[13].ToString()),
                                          InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                          //Used CreatedBy as a Temporary variable To Store Reciever GSTIN 
                                          CreatedBy = item.ItemArray[4].ToString(),
                                      };
                        foreach (var item in invoice)
                        {
                            item.CGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CGST).SingleOrDefault() : 0);
                            item.SGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.SGST).SingleOrDefault() : 0);
                            item.IGSTRate = (!FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault() : 0);
                            item.CessRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CESS).SingleOrDefault();
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.CGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.CGSTRate));
                            item.SGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.IGSTRate));
                            item.IGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.SGSTRate));
                            item.InvoiceID = item.GST_TRN_INVOICE.InvoiceID;
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.InvoiceDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.B2CL)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2CL);
                        var dt = Get5RowOnward(table);
                        var invoice = from item in dt.AsEnumerable()
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          Item_ID = FindItemId(item.ItemArray[9].ToString()),
                                          GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2CL), UserId),
                                          Qty = decimal.Parse(item.ItemArray[8].ToString()),
                                          Rate = decimal.Parse(item.ItemArray[4].ToString()),
                                          TaxableAmount = decimal.Parse(item.ItemArray[5].ToString()),
                                          CessAmt = decimal.Parse(item.ItemArray[6].ToString()),
                                          Discount = decimal.Parse(item.ItemArray[13].ToString()),
                                          InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                          //Used CreatedBy as a Temporary variable To Store Reciever GSTIN 
                                          CreatedBy = item.ItemArray[3].ToString(),
                                      };
                        foreach (var item in invoice)
                        {
                            item.CGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CGST).SingleOrDefault() : 0);
                            item.SGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.SGST).SingleOrDefault() : 0);
                            item.IGSTRate = (!FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault() : 0);
                            item.CessRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CESS).SingleOrDefault();
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.CGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.CGSTRate));
                            item.SGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.IGSTRate));
                            item.IGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.SGSTRate));
                            item.InvoiceID = item.GST_TRN_INVOICE.InvoiceID;
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.InvoiceDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.B2CS)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2CS);
                        var dt = Get5RowOnward(table);
                        var invoice = from item in dt.AsEnumerable()
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          Item_ID = FindItemId(item.ItemArray[7].ToString()),
                                          GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2CS), UserId),
                                          Qty = decimal.Parse(item.ItemArray[6].ToString()),
                                          Rate = decimal.Parse(item.ItemArray[2].ToString()),
                                          TaxableAmount = decimal.Parse(item.ItemArray[3].ToString()),
                                          CessAmt = item.Field<decimal?>("Column4"),
                                          Discount = decimal.Parse(item.ItemArray[11].ToString()),
                                          InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                          //Used CreatedBy as a Temporary variable To Store Reciever GSTIN 
                                          CreatedBy = item.ItemArray[1].ToString(),
                                      };
                        foreach (var item in invoice)
                        {
                            item.CGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CGST).SingleOrDefault() : 0);
                            item.SGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.SGST).SingleOrDefault() : 0);
                            item.IGSTRate = (!FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault() : 0);
                            item.CessRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CESS).SingleOrDefault();
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.CGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.CGSTRate));
                            item.SGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.IGSTRate));
                            item.IGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.SGSTRate));
                            item.InvoiceID = item.GST_TRN_INVOICE.InvoiceID;
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.InvoiceDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }

                    else if (sheetName == EnumConstants.OfflineSheetName.CDNR)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.CDNR);
                        var dt = Get5RowOnward(table);
                        var CRDR_Note_Data = from item in dt.AsEnumerable()
                                             select new GST_TRN_CRDR_NOTE_DATA
                                             {
                                                 GST_TRN_CRDR_NOTE = GetCRDRNote(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.CDNR), UserId),
                                                 Item_ID = FindItemId(item.ItemArray[13].ToString()),
                                                 Qty = decimal.Parse(item.ItemArray[17].ToString()),
                                                 Rate = decimal.Parse(item.ItemArray[8].ToString()),
                                                 TaxableAmount = decimal.Parse(item.ItemArray[9].ToString()),
                                                 CessAmt = decimal.Parse(item.ItemArray[10].ToString()),
                                                 Discount = decimal.Parse(item.ItemArray[16].ToString()),
                                                 InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                                 //Used CreatedBy as a Temporary variable
                                                 CreatedBy = item.ItemArray[7].ToString(),
                                             };
                        foreach (var item in CRDR_Note_Data)
                        {

                            item.CreatedDate = DateTime.Now;
                            item.CGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CGST).SingleOrDefault() : 0);
                            item.SGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.SGST).SingleOrDefault() : 0);
                            item.IGSTRate = (!FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault() : 0);
                            item.CessRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CESS).SingleOrDefault();
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.CGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.CGSTRate));
                            item.SGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.IGSTRate));
                            item.IGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.SGSTRate));
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.GST_TRN_CRDR_NOTE.GST_TRN_INVOICE.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.CreditDebitNoteDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.CDNUR)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.CDNUR);
                        var dt = Get5RowOnward(table);
                        var CRDR_Note_Data = from item in dt.AsEnumerable()
                                             select new GST_TRN_CRDR_NOTE_DATA
                                             {
                                                 GST_TRN_CRDR_NOTE = GetCRDRNote(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.CDNUR), UserId),
                                                 Item_ID = FindItemId(item.ItemArray[13].ToString()),
                                                 Qty = decimal.Parse(item.ItemArray[18].ToString()),
                                                 Rate = decimal.Parse(item.ItemArray[9].ToString()),
                                                 TaxableAmount = decimal.Parse(item.ItemArray[10].ToString()),
                                                 CessAmt = item.Field<decimal?>("Column11"),
                                                 Discount = decimal.Parse(item.ItemArray[17].ToString()),
                                                 InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                                 //Used CreatedBy as a Temporary variable
                                                 CreatedBy = item.Field<string>("Column7"),
                                             };
                        foreach (var item in CRDR_Note_Data)
                        {

                            item.CreatedDate = DateTime.Now;
                            if (item.CreatedBy != null)
                            {
                                item.CGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CGST).SingleOrDefault() : 0);
                                item.SGSTRate = (FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.SGST).SingleOrDefault() : 0);
                                item.IGSTRate = (!FindSameState(SupplierGSTIN, item.CreatedBy) ? unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault() : 0);
                            }
                            else
                            {
                                item.IGSTRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.IGST).SingleOrDefault();
                                item.CGSTRate = 0;
                                item.SGSTRate = 0;
                            }
                            item.CessRate = unitOfWork.ItemRepository.Filter(x => x.Item_ID == item.Item_ID).Select(xx => xx.CESS).SingleOrDefault();
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.CGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.CGSTRate));
                            item.SGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.IGSTRate));
                            item.IGSTAmt = Calculate.CalculateTax(Convert.ToDecimal(item.TaxableAmount), Convert.ToDecimal(item.SGSTRate));
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.GST_TRN_CRDR_NOTE.GST_TRN_INVOICE.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.CreditDebitNoteDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.EXP)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.EXP);
                        var dt = Get5RowOnward(table);
                        var invoice = from item in dt.AsEnumerable()
                                      select new GST_TRN_INVOICE_DATA
                                      {
                                          Item_ID = FindItemId(item.ItemArray[9].ToString()),
                                          GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.EXP), UserId),
                                          Qty = decimal.Parse(item.ItemArray[14].ToString()),
                                          Rate = decimal.Parse(item.ItemArray[6].ToString()),
                                          TaxableAmount = decimal.Parse(item.ItemArray[7].ToString()),
                                          InvoiceDataStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh),
                                          //Used CreatedBy as a Temporary variable To Store Reciever GSTIN 
                                      };
                        foreach (var item in invoice)
                        {
                            item.TotalAmount = item.Rate * item.Qty;
                            item.TotalAmountWithTax = item.TaxableAmount + item.TotalAmount;
                            item.InvoiceID = item.GST_TRN_INVOICE.InvoiceID;
                            item.LineID = unitOfWork.InvoiceDataRepository.Filter(x => x.InvoiceID == item.InvoiceID).Select(xx => xx.LineID).Max() + 1;
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.InvoiceDataRepository.Create(item);
                            unitOfWork.Save();
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.HSN)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.HSN);
                        var dt = Get5RowOnward(table);
                        var HSN = from item in dt.AsEnumerable()
                                  select new GST_MST_ITEM
                                  {
                                      CESS = Convert.ToDecimal(item.ItemArray[9].ToString()),
                                      CGST = Convert.ToDecimal(item.ItemArray[7].ToString()),
                                      Description = item.ItemArray[1].ToString(),
                                      ItemCode = item.ItemArray[0].ToString(),
                                      SGST = Convert.ToDecimal(item.ItemArray[8].ToString()),
                                      IGST = Convert.ToDecimal(item.ItemArray[6].ToString()),
                                      UGST = Convert.ToDecimal(item.ItemArray[8].ToString()),
                                      Unit = item.ItemArray[2].ToString().Split('-')[0],
                                  };
                        foreach (var item in HSN)
                        {
                            item.CreatedBy = UserId;
                            item.CreatedDate = DateTime.Now;
                            unitOfWork.ItemRepository.Create(item);
                            unitOfWork.Save();
                        }

                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        private GST_TRN_CRDR_NOTE GetCRDRNote(DataRow item, string SupplierGSTIN, byte p, string UserId)
        {
            GST_TRN_CRDR_NOTE note = new GST_TRN_CRDR_NOTE();
            if (p == 3)
            {
                note.GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, p, UserId);
                note.CDN_Date = item.Field<DateTime?>("Column2");
                note.NoteType = (byte)(item.ItemArray[5].ToString() == "C" ? 0 : 1);
                note.P_Gst = (byte)(item.ItemArray[12].ToString() == "Y" ? 1 : 0);
                note.TotalNoteValue = Convert.ToDecimal(item.ItemArray[8].ToString());
                note.Description = item.Field<string>("Column6");
                note.NoteNumber = item.ItemArray[3].ToString();
                note.From_UserID = note.GST_TRN_INVOICE.SellerUserID;
                note.To_UserID = note.GST_TRN_INVOICE.ReceiverUserID;
                note.CreatedBy = UserId;
            }
            else if (p == 4)
            {
                note.GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, p, UserId);
                note.CDN_Date = item.Field<DateTime?>("Column2");
                note.NoteType = (byte)(item.ItemArray[3].ToString() == "C" ? 0 : 1);
                note.P_Gst = (byte)(item.ItemArray[12].ToString() == "Y" ? 1 : 0);
                note.TotalNoteValue = Convert.ToDecimal(item.ItemArray[8].ToString());
                note.Description = item.Field<string>("Column6");
                note.NoteNumber = item.ItemArray[1].ToString();
                note.From_UserID = note.GST_TRN_INVOICE.SellerUserID;
                note.To_UserID = note.GST_TRN_INVOICE.ReceiverUserID;
                note.CreatedBy = UserId;
            }
            unitOfWork.CreditDebitNoteRepository.Create(note);
            unitOfWork.Save();
            return note;
        }

        private bool FindSameState(string SupplierGSTIN, string State)
        {
            var SupplierStateCode = unitOfWork.AspnetRepository.Filter(x => x.GSTNNo == SupplierGSTIN).Select(xx => xx.StateCode).SingleOrDefault();
            if (SupplierStateCode == State.Split('-')[0])
                return true;
            return false;
        }

        private int FindItemId(string ItemNameCode)
        {
            var _ItemCode = ItemNameCode.ToString().Split('.')[0];
            return (unitOfWork.ItemRepository.Filter(x => x.ItemCode == _ItemCode).Select(xx => xx.Item_ID).SingleOrDefault());
        }

        private GST_TRN_INVOICE GetInvoiceInformation(DataRow dataRow, string SupplierGSTIN, byte sheetCode, string UserId)
        {
            GST_TRN_INVOICE invoice = new GST_TRN_INVOICE();
            try
            {
                cls_Invoice invoiceObject = new cls_Invoice();
                //B2B
                if (sheetCode == 0)
                {
                    var RecieverGSTIN = dataRow.Field<string>("Column0");
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[1].ToString().Contains('.') ? dataRow.ItemArray[1].ToString().Split('.')[0] : dataRow.ItemArray[1].ToString();
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column2");
                    invoice.InvoiceMonth = (byte)invoice.InvoiceDate.Value.Month;
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(dataRow.Field<string>("Column0"));
                    var ConsigneeUserId = FindUserId(dataRow.Field<string>("Column7"));
                    invoice.ConsigneeUserID = ConsigneeUserId == null ? invoice.ReceiverUserID : ConsigneeUserId;
                    invoice.OrderDate = dataRow.Field<DateTime>("Column2").Date;
                    //invoice.VendorID = 
                    invoice.Freight = Convert.ToInt32(dataRow.ItemArray[13].ToString().Split('.')[0]);
                    invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[14].ToString().Split('.')[0]);
                    invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[15].ToString().Split('.')[0]);
                    //invoice.   = 
                    //invoice.ElectronicReferenceNoDate = 
                    invoice.InvoiceType = sheetCode;
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    // invoice.TaxBenefitingState = 
                    invoice.Status = true;
                    EnumConstants.InvoiceSpecialCondition value = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), dataRow.ItemArray[6].ToString());
                    invoice.InvoiceSpecialCondition = (byte)value;
                }
                //B2CL
                else if (sheetCode == 1)
                {
                    var RecieverGSTIN = dataRow.ItemArray[14].ToString().Contains('.') ? dataRow.ItemArray[14].ToString().Split('.')[0] : dataRow.ItemArray[14].ToString();
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[0].ToString().Contains('.') ? dataRow.ItemArray[0].ToString().Split('.')[0] : dataRow.ItemArray[0].ToString();
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column1");
                    invoice.InvoiceMonth = (byte)invoice.InvoiceDate.Value.Month;
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(RecieverGSTIN);
                    var ConsigneeUserId = FindUserId(dataRow.Field<string>("Column7"));
                    invoice.ConsigneeUserID = ConsigneeUserId == null ? invoice.ReceiverUserID : ConsigneeUserId;
                    invoice.Freight = Convert.ToInt32(dataRow.ItemArray[10].ToString().Split('.')[0]);
                    invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[11].ToString().Split('.')[0]);
                    invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[12].ToString().Split('.')[0]);
                    invoice.InvoiceType = sheetCode;
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    invoice.Status = true;
                    EnumConstants.InvoiceSpecialCondition value = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), dataRow.ItemArray[6].ToString());
                    invoice.InvoiceSpecialCondition = (byte)value;
                }
                //B2CS
                else if (sheetCode == 2)
                {
                    var RecieverGSTIN = dataRow.ItemArray[12].ToString().Contains('.') ? dataRow.ItemArray[12].ToString().Split('.')[0] : dataRow.ItemArray[12].ToString();
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[0].ToString().Contains('.') ? dataRow.ItemArray[0].ToString().Split('.')[0] : dataRow.ItemArray[0].ToString();
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(RecieverGSTIN);
                    var ConsigneeUserId = FindUserId(dataRow.Field<string>("Column5"));
                    invoice.ConsigneeUserID = ConsigneeUserId == null ? invoice.ReceiverUserID : ConsigneeUserId;
                    invoice.Freight = Convert.ToInt32(dataRow.ItemArray[8].ToString().Split('.')[0]);
                    invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[9].ToString().Split('.')[0]);
                    invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[10].ToString().Split('.')[0]);
                    invoice.InvoiceType = 1;
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    invoice.Status = true;
                    EnumConstants.InvoiceSpecialCondition value = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), dataRow.ItemArray[6].ToString());
                    invoice.InvoiceSpecialCondition = (byte)value;
                }
                //CDNR
                else if (sheetCode == 3)
                {
                    var RecieverGSTIN = dataRow.Field<string>("Column0");
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[1].ToString().Contains('.') ? dataRow.ItemArray[1].ToString().Split('.')[0] : dataRow.ItemArray[1].ToString();
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column4");
                    invoice.InvoiceMonth = (byte)invoice.InvoiceDate.Value.Month;
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(dataRow.Field<string>("Column0"));
                    invoice.OrderDate = dataRow.Field<DateTime>("Column2").Date;
                    invoice.Freight = Convert.ToInt32(dataRow.ItemArray[14].ToString().Split('.')[0]);
                    invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[15].ToString().Split('.')[0]);
                    invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[16].ToString().Split('.')[0]);
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    invoice.Status = true;
                }
                //CDNUR
                else if (sheetCode == 4)
                {
                    var RecieverGSTIN = dataRow.ItemArray[19].ToString();
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[4].ToString().Contains('.') ? dataRow.ItemArray[4].ToString().Split('.')[0] : dataRow.ItemArray[4].ToString();
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column5");
                    invoice.InvoiceMonth = (byte)invoice.InvoiceDate.Value.Month;
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(dataRow.ItemArray[19].ToString());
                    invoice.OrderDate = dataRow.Field<DateTime>("Column2").Date;
                    invoice.Freight = Convert.ToInt32(dataRow.ItemArray[14].ToString().Split('.')[0]);
                    invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[15].ToString().Split('.')[0]);
                    invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[16].ToString().Split('.')[0]);
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    invoice.Status = true;
                    EnumConstants.InvoiceSpecialCondition value = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), dataRow.ItemArray[1].ToString());
                    invoice.InvoiceSpecialCondition = (byte)value;
                }
                //EXP
                else if (sheetCode == 5)
                {
                    var RecieverGSTIN = dataRow.ItemArray[8].ToString().Contains('.') ? dataRow.ItemArray[8].ToString().Split('.')[0] : dataRow.ItemArray[8].ToString();
                    var GstnExit = unitOfWork.AspnetRepository.Filter(w => w.GSTNNo == RecieverGSTIN).FirstOrDefault();
                    invoice.InvoiceNo = dataRow.ItemArray[1].ToString().Contains('.') ? dataRow.ItemArray[1].ToString().Split('.')[0] : dataRow.ItemArray[1].ToString();
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column2");
                    invoice.InvoiceMonth = (byte)invoice.InvoiceDate.Value.Month;
                    invoice.SellerUserID = FindUserId(SupplierGSTIN);
                    invoice.ReceiverUserID = FindUserId(RecieverGSTIN);
                    invoice.OrderDate = dataRow.Field<DateTime>("Column2").Date;
                    //invoice.VendorID = 
                    //invoice.Freight = Convert.ToInt32(dataRow.ItemArray[14].ToString().Split('.')[0]);
                    //invoice.Insurance = Convert.ToInt32(dataRow.ItemArray[15].ToString().Split('.')[0]);
                    //invoice.PackingAndForwadingCharges = Convert.ToInt32(dataRow.ItemArray[16].ToString().Split('.')[0]);
                    ////invoice.   = 
                    //invoice.ElectronicReferenceNoDate = 
                    invoice.InvoiceType = 0;
                    invoice.FinYear_ID = invoiceObject.GetCurrentFinYear();
                    string getIsinter = GstnExit == null ? "" : GstnExit.GSTNNo;
                    string strStatecode = getIsinter.Substring(0, 2);
                    string Statecod = GstnExit == null ? "" : GstnExit.StateCode;
                    if (strStatecode == Statecod)
                    {
                        invoice.IsInter = false;
                    }
                    else { invoice.IsInter = true; }
                    invoice.InvoiceStatus = Convert.ToByte(EnumConstants.InvoiceStatus.Fresh);
                    // invoice.TaxBenefitingState = 
                    invoice.Status = true;
                    EnumConstants.InvoiceSpecialCondition value = (EnumConstants.InvoiceSpecialCondition)Enum.Parse(typeof(EnumConstants.InvoiceSpecialCondition), dataRow.ItemArray[1].ToString());
                    invoice.InvoiceSpecialCondition = (byte)value;
                }
                invoice.CreatedBy = UserId;
                invoice.CreatedDate = DateTime.Now;
                unitOfWork.InvoiceRepository.Create(invoice);
                unitOfWork.Save();
            }


            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return invoice;
        }

        private string FindUserId(string GSTNNo)
        {
            return unitOfWork.AspnetRepository.Filter(x => x.GSTNNo == GSTNNo).Select(xx => xx.Id).SingleOrDefault();
        }
        private DataTable Get5RowOnward(DataTable dataTable)
        {
            dataTable = dataTable.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
            return dataTable.Rows.Cast<System.Data.DataRow>().Skip(3).CopyToDataTable();
        }

        private DataTable GetExcelDataTable(DataSet dataSetInvoice, EnumConstants.OfflineSheetName sheetName)
        {
            var data = dataSetInvoice.Tables[sheetName.ToString()];
            return data;
        }

        public void SaveOfflineData(GST_TRN_OFFLINE itemData)
        {
            try
            {

                //GST_TRN_OFFLINE item = new GST_TRN_OFFLINE();
                //item.AggregateTurnover = itemData.AggregateTurnover;
                //item.AggregateTurnoverQuater = itemData.AggregateTurnoverQuater; 
                //item.FileType = itemData.FileType.Value;
                //item.SupplierGSTIN = itemData.SupplierGSTIN;
                //item.Month = itemData.Month.Value;
                //item.Fin_ID = itemData.Fin_ID.Value;
                //item.UpdatedBy = item.UpdatedBy;
                //item.UploadedBy = item.UploadedBy;
                //item.UploadStatus = 1;
                //item.Section = 0;
                //item.ExcelData = itemData.ExcelData;

                unitOfWork.OfflineRepository.Create(itemData);
                unitOfWork.Save();

            }
            catch (Exception ex)
            {

            }
        }


    }
}
