using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using log4net;
//using PicNetML.Arff;
//using PicNetML.RuntimeHelpers.Xrff;
//using weka.core.converters;
//using DM.Ceka;

namespace DataConversion
{
    public class ARFFConverter : Converter
    {
        private readonly ILog log = LogManager.GetLogger("DataConversionLogger");

        /// <summary>
        /// Экспортирует массив данных в ARFF формат с учетом выбранной локали
        /// </summary>
        /// <param name="path">Путь к файлу, в который нужно сохранить данные</param>
        /// <param name="localisation">Локализация</param>
        /// <returns>Успешное завершение операции</returns>
        public override bool Export(String path, Localisation localisation)
        {
            try
            {
                if (!path.EndsWith(".arff"))
                    path += ".arff";

                Thread.CurrentThread.CurrentCulture = new CultureInfo((int)localisation);

                log.Info(String.Format("Export to .arff file to: {0}", path));
                var timer = new Stopwatch();
                timer.Start();

                using (var writer = new StreamWriter(path))
                {
                    writer.WriteLine("@RELATION iris");

                    foreach (var column in dataTable.Columns.Cast<DataColumn>())
                    {
                        if (column.DataType == typeof(double))
                        {
                            writer.WriteLine(String.Format("@ATTRIBUTE \"{0}\" NUMERIC", column.ColumnName));
                        }
                        else if (column.DataType == typeof(DateTime))
                        {
                            string dateFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern;
                            writer.WriteLine(String.Format("@ATTRIBUTE \"{0}\" DATE [{1}]", column.ColumnName, dateFormat));
                        }
                        else
                        {
                            writer.WriteLine(String.Format("@ATTRIBUTE \"{0}\" STRING", column.ColumnName));
                        }
                    }

                    string listSeparator = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;

                    writer.WriteLine("@DATA");
                    foreach (DataRow row in dataTable.Rows)
                        writer.WriteLine(String.Join(listSeparator, row.ItemArray));
                }

                timer.Stop();
                log.Info(String.Format("ARFF export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Can't export to .arff file!", ex);
                return false;
            }
        }
    }
}