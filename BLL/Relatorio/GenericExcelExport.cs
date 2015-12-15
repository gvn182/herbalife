using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI;

namespace BLL.Relatorio
{
    public class GenericExcelExport
    {
        public void ExportToExcel(String FileName, Page page, DataTable DataTable)
        {
            
            // Create a new workbook and worksheet.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            SpreadsheetGear.IWorksheet worksheet = workbook.Worksheets["Sheet1"];

            // Get the top left cell for the DataTable.
            SpreadsheetGear.IRange range = worksheet.Cells["A1"];

            // Copy the DataTable to the worksheet range.
            range.CopyFromDataTable(DataTable, SpreadsheetGear.Data.SetDataFlags.None);

            // Auto size all worksheet columns which contain data
            worksheet.UsedRange.Columns.AutoFit();

            // Stream the Excel spreadsheet to the client in a format
            // compatible with Excel 97/2000/XP/2003/2007/2010.
            page.Response.Clear();
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            workbook.SaveToStream(page.Response.OutputStream, SpreadsheetGear.FileFormat.Excel8);
            page.Response.End();
        }
    }
}
