using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Services
{
    public static class ExcelService
    {
        public static bool DataTableToExcel(DataTable datatable, out string error, string fileName)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                var workbook = new Workbook();
                var sheet = workbook.Worksheets[0];
                var cells = sheet.Cells;

                var nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        if (nRow == 0)
                        {
                            cells[nRow, i].PutValue(datatable.Columns[i].ColumnName);
                        }
                    }

                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {
                            cells[nRow, i].PutValue(row[i]);
                        }
                    }
                    catch (Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                //string filepath = @"C:\1.txt";
                //workbook.Save(filepath);
                workbook.Save(HttpContext.Current.Response, fileName + ".xlsx", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Xlsx));
                error = "导出成功！";
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }
    }
}
