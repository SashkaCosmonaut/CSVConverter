using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using OfficeOpenXml;

namespace DataConversion
{
    public class XLSXConverter : Converter
    {
        public override bool Export(String path)
        {
            try
            {
                Console.WriteLine("Export of .xlsx file...");
                var timer = new Stopwatch();
                timer.Start();
                var file = new FileInfo(path);
                using (var pck = new ExcelPackage(file))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                    ws.Cells.AutoFitColumns();
                    pck.Save();
                }

                timer.Stop();
                Console.WriteLine("Export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds);
                return true;
            }
            catch
            {
                Console.WriteLine("Export failed! Please check, may be file with such name is already exist...");
                return false;
            }
        }
    }
}