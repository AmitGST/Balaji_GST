using DataAccessLayer;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Offline
{
    public class clsOffline
    {
        UnitOfWork unitOfWork;
        public clsOffline()
        {
            unitOfWork = new UnitOfWork();
        }


        public void SaveExcelOffLineData(DataSet dataSetInvoices, string SupplierGSTIN, string UserId, string offlineid)
        {
            UnitOfWork unitofwork = new UnitOfWork();
            try
            {
                decimal temp;
                foreach (EnumConstants.OfflineSheetName sheetName in Enum.GetValues(typeof(EnumConstants.OfflineSheetName)))
                {
                    if (sheetName == EnumConstants.OfflineSheetName.B2B)
                    {
                     
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2B);
                         var dt = Get5RowOnward(table,3);
                         if (dt != null)
                         {
                             var invo = from item in dt.AsEnumerable()
                                        select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                        {
                                            GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2B), UserId, offlineid),
                                            RateId = GetRateId(decimal.TryParse(item.ItemArray[8].ToString() ,out temp)? temp :(decimal?)null),
                                            TotalTaxableValue = decimal.TryParse(item.ItemArray[9].ToString() ,out temp)? temp :(decimal?)null,
                                            CessAmt = decimal.TryParse(item.ItemArray[10].ToString(), out temp) ? temp : (decimal?)null,
                                        };

                             foreach (var item in invo)
                             {
                                 unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                 unitOfWork.Save();
                             }
                         }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.B2CL)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2CL);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2CL), UserId, offlineid),
                                           RateId = GetRateId(decimal.TryParse(item.ItemArray[4].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalTaxableValue =(decimal.TryParse(item.ItemArray[5].ToString(), out temp) ? temp : (decimal?)null),
                                           CessAmt = (decimal.TryParse(item.ItemArray[6].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                    
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.B2CS)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.B2CS);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.B2CS), UserId,offlineid),
                                           RateId = GetRateId(decimal.TryParse(item.ItemArray[2].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalTaxableValue = (decimal.TryParse(item.ItemArray[3].ToString(), out temp) ? temp : (decimal?)null),
                                           CessAmt = (decimal.TryParse(item.ItemArray[4].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }

                    else if (sheetName == EnumConstants.OfflineSheetName.CDNR)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.CDNR);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.CDNR), UserId, offlineid),
                                           RateId = GetRateId(decimal.TryParse(item.ItemArray[9].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalTaxableValue = (decimal.TryParse(item.ItemArray[10].ToString(), out temp) ? temp : (decimal?)null),
                                           CessAmt = (decimal.TryParse(item.ItemArray[11].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.CDNUR)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.CDNUR);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.CDNUR), UserId, offlineid),
                                           RateId = GetRateId(decimal.TryParse(item.ItemArray[9].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalTaxableValue = (decimal.TryParse(item.ItemArray[10].ToString(), out temp) ? temp : (decimal?)null),
                                           CessAmt = (decimal.TryParse(item.ItemArray[11].ToString(), out temp) ? temp : (decimal?)null) ,
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.EXP)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.EXP);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.EXP), UserId, offlineid),
                                           RateId = GetRateId((decimal.TryParse(item.ItemArray[7].ToString(), out temp) ? temp : (decimal?)null)),
                                           TotalTaxableValue = (decimal.TryParse(item.ItemArray[8].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.AT)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.AT);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.AT), UserId, offlineid),
                                           RateId = GetRateId((decimal.TryParse(item.ItemArray[1].ToString(), out temp) ? temp : (decimal?)null)),
                                           CessAmt = (decimal.TryParse(item.ItemArray[3].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (sheetName == EnumConstants.OfflineSheetName.ATADJ)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.ATADJ);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.ATADJ), UserId, offlineid),
                                           RateId = GetRateId(decimal.TryParse(item.ItemArray[1].ToString(), out temp) ? temp : (decimal?)null),
                                           //  Gros= Convert.ToDecimal(item.ItemArray[9]),
                                           CessAmt = (decimal.TryParse(item.ItemArray[3].ToString(), out temp) ? temp : (decimal?)null)
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
                        }
                    }
                    //else if (sheetName == EnumConstants.OfflineSheetName.EXEMP)
                    //{
                    //    var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.EXEMP);
                    //    var dt = Get5RowOnward(table);
                    //    var invo = from item in dt.AsEnumerable()
                    //               select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                    //               {
                    //                   GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.EXEMP), UserId),
                    //                  // RateId = GetRateId(Convert.ToDecimal(item.ItemArray[1])),
                    //                   //  Gros= Convert.ToDecimal(item.ItemArray[9]),
                    //                   CessAmt = Convert.ToDecimal(item.ItemArray[3]),
                    //               };

                    //    foreach (var item in invo)
                    //    {
                    //        item.ValueID = item.GST_TRN_OFFLINE_INVOICE.ValueId;
                    //        //  item.Rate = item.Rate;
                    //        //  item.TotalTaxableValue = item.TotalTaxableValue;
                    //        item.CessAmt = item.CessAmt;
                    //        unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                    //        unitOfWork.Save();
                    //    }
                    //}
                    else if (sheetName == EnumConstants.OfflineSheetName.HSN)
                    {
                        var table = GetExcelDataTable(dataSetInvoices, EnumConstants.OfflineSheetName.HSN);
                        var dt = Get5RowOnward(table,2);
                        if (dt != null)
                        {
                            var invo = from item in dt.AsEnumerable()
                                       select new GST_TRN_OFFLINE_INVOICE_DATAITEM
                                       {
                                           GST_TRN_OFFLINE_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, Convert.ToByte(EnumConstants.OfflineSheetName.HSN), UserId, offlineid),
                                           TotalQuantity = (decimal.TryParse(item.ItemArray[3].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalValue = (decimal.TryParse(item.ItemArray[4].ToString(), out temp) ? temp : (decimal?)null),
                                           TotalTaxableValue = (decimal.TryParse(item.ItemArray[5].ToString(), out temp) ? temp : (decimal?)null),
                                           // RateId = GetRateId(Convert.ToDecimal(item.ItemArray[6])),
                                           IGSTAmt = (decimal.TryParse(item.ItemArray[6].ToString(), out temp) ? temp : (decimal?)null),
                                           CGSTAmt = (decimal.TryParse(item.ItemArray[7].ToString(), out temp) ? temp : (decimal?)null),
                                           SGSTAmt = (decimal.TryParse(item.ItemArray[8].ToString(), out temp) ? temp : (decimal?)null),
                                           CessAmt = (decimal.TryParse(item.ItemArray[9].ToString(), out temp) ? temp : (decimal?)null),
                                       };

                            foreach (var item in invo)
                            {
                                unitOfWork.OfflineinvoicedataitemRepository.Create(item);
                                unitOfWork.Save();
                            }
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

        private int? GetRateId(decimal? rate)
        {
            try
            {
                return unitOfWork.OfflinerateRepository.Filter(x => x.RATE == rate).SingleOrDefault().RATE_ID;
            }
            catch
            {
                return null;
            }
        }
        //private GST_TRN_CRDR_NOTE GetCRDRNote(DataRow item, string SupplierGSTIN, byte p, string UserId)
        //{
        //    GST_TRN_CRDR_NOTE note = new GST_TRN_CRDR_NOTE();
        //    if (p == 3)
        //    {
        //        note.GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, p, UserId);
        //        note.CDN_Date = item.Field<DateTime?>("Column2");
        //        note.NoteType = (byte)(item.ItemArray[5].ToString() == "C" ? 0 : 1);
        //        note.P_Gst = (byte)(item.ItemArray[12].ToString() == "Y" ? 1 : 0);
        //        note.TotalNoteValue = Convert.ToDecimal(item.ItemArray[8].ToString());
        //        note.Description = item.Field<string>("Column6");
        //        note.NoteNumber = item.ItemArray[3].ToString();
        //        note.From_UserID = note.GST_TRN_INVOICE.SellerUserID;
        //        note.To_UserID = note.GST_TRN_INVOICE.ReceiverUserID;
        //        note.CreatedBy = UserId;
        //    }
        //    else if (p == 4)
        //    {
        //        note.GST_TRN_INVOICE = GetInvoiceInformation(item, SupplierGSTIN, p, UserId);
        //        note.CDN_Date = item.Field<DateTime?>("Column2");
        //        note.NoteType = (byte)(item.ItemArray[3].ToString() == "C" ? 0 : 1);
        //        note.P_Gst = (byte)(item.ItemArray[12].ToString() == "Y" ? 1 : 0);
        //        note.TotalNoteValue = Convert.ToDecimal(item.ItemArray[8].ToString());
        //        note.Description = item.Field<string>("Column6");
        //        note.NoteNumber = item.ItemArray[1].ToString();
        //        note.From_UserID = note.GST_TRN_INVOICE.SellerUserID;
        //        note.To_UserID = note.GST_TRN_INVOICE.ReceiverUserID;
        //        note.CreatedBy = UserId;
        //    }
        //    unitOfWork.CreditDebitNoteRepository.Create(note);
        //    unitOfWork.Save();
        //    return note;
        //}

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

        private GST_TRN_OFFLINE_INVOICE GetInvoiceInformation(DataRow dataRow, string SupplierGSTIN, byte sheetCode, string UserId, string offlineid)
        {
            GST_TRN_OFFLINE_INVOICE invoice = new GST_TRN_OFFLINE_INVOICE();
            try
            {
                //B2B
                string stateCode;
                Boolean ReverseChr;
                if (sheetCode == 0)
                {
                    stateCode = dataRow.ItemArray[4].ToString().Split('-')[0];
                  
                    invoice.ReceiverGSTIN = dataRow.Field<string>("Column0");
                    invoice.InvoiceNo = dataRow.Field<string>("Column1");
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column2");
                    invoice.TotalInvoiceValue = Convert.ToString(dataRow.ItemArray[3]);
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.ReverseCharge = dataRow.ItemArray[5].ToString() == "Y" ? true : false;
                    invoice.InvoiceType = sheetCode; //Convert.ToByte(dataRow.ItemArray[6]);
                    invoice.ECommerce_GSTIN = dataRow.Field<string>("Column7");
                    invoice.SectionType =sheetCode;
                }
                //B2CL
                else if (sheetCode == 1)
                {
                    stateCode = dataRow.ItemArray[3].ToString().Split('-')[0];
                    invoice.InvoiceNo = Convert.ToString(dataRow.ItemArray[0]);
                    invoice.InvoiceDate = dataRow.Field<DateTime>("Column1");
                    invoice.TotalInvoiceValue = Convert.ToString(dataRow.ItemArray[2]);
                    invoice.ECommerce_GSTIN = dataRow.Field<string>("Column7");
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.SectionType = sheetCode;
                }
                //B2CS
                else if (sheetCode == 2)
                {
                    stateCode = dataRow.ItemArray[1].ToString().Split('-')[0];
                    invoice.Type = Convert.ToByte((EnumConstants.URType)Enum.Parse(typeof(EnumConstants.OfflineType), dataRow.ItemArray[0].ToString())); //dataRow.Field<string>("Column0");
                    invoice.ECommerce_GSTIN = dataRow.Field<string>("Column5");
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.SectionType = sheetCode;
                }
                //CDNR
                else if (sheetCode == 3)
                {

                    invoice.ReceiverGSTIN = dataRow.Field<string>("Column0");
                    invoice.InvoiceNo = Convert.ToString(dataRow.ItemArray[1]);
                    invoice.InvoiceDate = Convert.ToDateTime(dataRow.ItemArray[2]);
                    invoice.Voucher_No = Convert.ToString(dataRow.ItemArray[3]);
                    invoice.Voucher_date = Convert.ToDateTime(dataRow.ItemArray[4]);
                    invoice.Document_Type =Convert.ToString(dataRow.ItemArray[5]);
                    string NoteCode = dataRow.ItemArray[6].ToString().Split('-')[0];
                    invoice.Issuing_Note = unitOfWork.OfflineissuingnoteRepository.Find(x=>x.NoteCode== NoteCode).NoteID;
                    stateCode = dataRow.ItemArray[7].ToString().Split('-')[0];
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.Voucher_Value = Convert.ToString(dataRow.ItemArray[8]);
                    invoice.Pre_GST = dataRow.ItemArray[12].ToString() == "Y" ? true : false;
                    invoice.SectionType = sheetCode;
                    //invoice.InvoiceType = Convert.ToByte(dataRow.Field<string>("Column10"));
                    //invoice.ECommerece_GSTIN = dataRow.Field<string>("Column11");
                }
                //CDNUR
                else if (sheetCode == 4)
                {
                    stateCode = dataRow.ItemArray[7].ToString().Split('-')[0];
                    invoice.URType = Convert.ToByte((EnumConstants.URType)Enum.Parse(typeof(EnumConstants.URType), dataRow.ItemArray[0].ToString())); 
                    invoice.Voucher_No = dataRow.Field<string>("Column1");
                    invoice.Voucher_date = Convert.ToDateTime(dataRow.ItemArray[2]);
                    invoice.Document_Type = Convert.ToString(dataRow.ItemArray[3]);//Convert.ToString((EnumConstants.NoteType)Enum.Parse(typeof(EnumConstants.NoteType), dataRow.ItemArray[3].ToString())); //Convert.ToByte(dataRow.ItemArray[3]);
                    invoice.InvoiceNo = Convert.ToString(dataRow.ItemArray[4]);
                    invoice.InvoiceDate = Convert.ToDateTime(dataRow.ItemArray[5]);
                    string NoteCode = dataRow.ItemArray[6].ToString().Split('-')[0];
                    invoice.Issuing_Note = unitOfWork.OfflineissuingnoteRepository.Find(x => x.NoteCode == NoteCode).NoteID; invoice.Voucher_Value = Convert.ToString(dataRow.ItemArray[8]);
                    invoice.Pre_GST  = dataRow.ItemArray[12].ToString() == "Y" ? true : false;
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.SectionType = sheetCode;
                }
                //EXP
                else if (sheetCode == 5)
                {
                    invoice.InvoiceType = sheetCode; //invoice.InvoiceType = Convert.ToByte(dataRow.Field<string>("Column0"));
                    invoice.InvoiceNo = Convert.ToString(dataRow.ItemArray[1]);
                    invoice.InvoiceDate = Convert.ToDateTime(dataRow.ItemArray[2]);
                    invoice.TotalInvoiceValue = Convert.ToString(dataRow.ItemArray[3]);
                    invoice.PortCode = Convert.ToString(dataRow.ItemArray[4]);
                    invoice.ShippingBillNo = Convert.ToString(dataRow.ItemArray[5]);
                    invoice.ShippingBillDate = Convert.ToDateTime(dataRow.ItemArray[6]);
                    invoice.SectionType = sheetCode;
                }
                //at
                else if (sheetCode == 6)
                {
                    stateCode = dataRow.ItemArray[0].ToString().Split('-')[0];
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.SectionType = sheetCode;
                }
                //atadj
                else if (sheetCode == 7)
                {
                    stateCode = dataRow.ItemArray[0].ToString().Split('-')[0];
                    invoice.PlaceofSupply = unitOfWork.StateRepository.Filter(x => x.StateCode == stateCode).FirstOrDefault().StateID;
                    invoice.SectionType = sheetCode;
                }
                //exemp
                else if (sheetCode == 8)
                {
                    invoice.HSNDescription = dataRow.Field<string>("Column0");
                    invoice.NilRated = Convert.ToBoolean(dataRow.ItemArray[1]);
                    invoice.IsExempted = Convert.ToBoolean(dataRow.ItemArray[2]);
                    invoice.NonGSTSupplies = Convert.ToBoolean(dataRow.ItemArray[3]);
                    invoice.SectionType = sheetCode;
                }
                //hsn
                else if (sheetCode == 9)
                {
                    invoice.HSN = Convert.ToString(dataRow.ItemArray[0]);
                    invoice.HSNDescription = dataRow.Field<string>("Column1");
                    invoice.UQC = Convert.ToByte((EnumConstants.Unit)Enum.Parse(typeof(EnumConstants.Unit), dataRow.ItemArray[2].ToString().Split('-')[0]));//Convert.ToByte(dataRow.ItemArray[2]);
                    invoice.SectionType = sheetCode;
                }
                invoice.UserID = UserId;
                invoice.OfflineID = Convert.ToInt64(offlineid);
                invoice.CreatedBy = UserId;
                invoice.CreatedDate = DateTime.Now;
                unitOfWork.OfflineinvoiceRepository.Create(invoice);
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
        private DataTable Get5RowOnward(DataTable dataTable,int rowStart)
        {
            try
            { 
                //dataTable = dataTable.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                //return dataTable.Rows.Cast<System.Data.DataRow>().Skip(3).CopyToDataTable();

                dataTable = dataTable.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                return dataTable.Rows.Cast<System.Data.DataRow>().Skip(rowStart).CopyToDataTable();
            }
            catch
            {
                //himanshu did this
                return null;
            }
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
