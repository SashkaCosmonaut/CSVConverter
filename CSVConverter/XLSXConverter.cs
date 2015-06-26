using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using OfficeOpenXml;
using log4net;

namespace DataConversion
{
    /// <summary>
    /// Конвертирует данные в формат Microsoft Excel (xlsx)
    /// </summary>
    public class XLSXConverter : Converter
    {
        private readonly ILog log = LogManager.GetLogger("DataConversionLogger");

        /// <summary>
        /// Экспортирует массив данных в XLSX формат с учетом выбранной локали
        /// </summary>
        /// <param name="path">Путь к файлу, в который нужно сохранить данные</param>
        /// <param name="localisation">Локализация</param>
        /// <returns>Успешное завершение операции</returns>
        public override bool Export(String path, Localisation localisation)
        {
            try
            {
                if (!path.EndsWith(".xlsx"))
                    path += ".xlsx";

                log.Info(String.Format("Export to .xlsx file to: {0}", path));
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
                log.Info(String.Format("Export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Can't export to .xlsx file!", ex);
                return false;
            }
        }
    }
}