using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Kent.Boogaart.KBCsv;
using Kent.Boogaart.KBCsv.Extensions;
using Kent.Boogaart.KBCsv.Extensions.Data;
using log4net;

namespace DataConversion
{
    public class CSVConverter : Converter
    {
        private readonly ILog log = LogManager.GetLogger("DataConversionLogger");

        /// <summary>
        /// Экспортирует массив данных в CSV формат с учетом выбранной локали
        /// </summary>
        /// <param name="path">Путь к файлу, в который нужно сохранить данные</param>
        /// <param name="localisation">Локализация</param>
        /// <returns>Успешное завершение операции</returns>
        public override bool Export(String path, Localisation localisation)
        {
            try
            {
                if (!path.EndsWith(".csv"))
                    path += ".csv";

                Thread.CurrentThread.CurrentCulture = new CultureInfo((int)localisation);

                log.Info(String.Format("Export to .csv file to: {0}", path));
                var timer = new Stopwatch();
                timer.Start();

                using (var streamWriter = new StreamWriter(path))
                using (var writer = new CsvWriter(streamWriter))
                {
                    writer.ValueSeparator = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator[0];
                    dataTable.WriteCsv(writer);
                }

                timer.Stop();
                log.Info(String.Format("CSV export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Can't export to .csv file!", ex);
                return false;
            }
        }
    }
}