using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_ExcelReader
    {
        public static DataSet ExcelReadDataSet(string filePath)
        {

            //Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader2003 = ExcelReaderFactory.CreateBinaryReader(stream);

            //Reading from a OpenXml Excel file (2007 format; *.xlsx)
            FileStream stream = new FileStream(filePath, FileMode.Open);
            IExcelDataReader excelReader2007 = ExcelReaderFactory.CreateOpenXmlReader(stream);

            //DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader2007.AsDataSet();

            //Data Reader methods
            foreach (DataTable table in result.Tables)
            {
                //for (int i = 0; i < table.Rows.Count; i++)
                //{
                //    for (int j = 0; j < table.Columns.Count; j++)
                //        Console.Write("\"" + table.Rows[i].ItemArray[j] + "\";");
                //    Console.WriteLine();
                //}
            }
             //Free resources (IExcelDataReader is IDisposable)
            //excelReader2003.Close();
            excelReader2007.Close();
            //Console.Read();
            return result;
        }
    }
}
