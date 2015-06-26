using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Diagnostics;
using Kent.Boogaart.KBCsv;
using Kent.Boogaart.KBCsv.Extensions;
using Kent.Boogaart.KBCsv.Extensions.Data;
using log4net;

namespace DataConversion
{
    /// <summary>
    /// Поддерживаемая локализация
    /// </summary>
    /// <remarks>
    /// National Language Support (NLS) API Reference:
    /// https://msdn.microsoft.com/ru-ru/goglobal/bb896001.aspx
    /// </remarks>
    public enum Localisation : int
    {
        Russian = 0x0419,
        English = 0x0009,
        Dutch = 0x0413
    }

    /// <summary>
    /// Конвертирует массив данных в нужный формат с учетом текущей локали
    /// </summary>
    abstract public class Converter
    {
        private readonly ILog log = LogManager.GetLogger("DataConversionLogger");

        /// <summary>
        /// Массив с данными
        /// </summary>
        protected DataTable dataTable;

        /// <summary>
        /// Инициализирует массив данных (чтобы не импортировать из файла)
        /// </summary>
        /// <param name="table">Таблица с данными</param>
        /// <returns>Успех выполнения операции</returns>
        public bool Import(DataTable table)
        {
            dataTable = table;
            return true;
        }

        /// <summary>
        /// Импортирует массив данныех из CSV файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Успех выполнения операции</returns>
        public bool Import(String path)
        {
            try
            {
                log.Info("Import CSV file...");
                var timer = new Stopwatch();
                timer.Start();

                dataTable = new DataTable();

                using (var streamReader = new StreamReader(path))
                using (var reader = new CsvReader(streamReader))
                {
                    reader.ValueSeparator = ';';
                    reader.ReadHeaderRecord();
                    dataTable.Fill(reader);
                }

                timer.Stop();
                log.Info(String.Format("Import complete! Csv file contains {0} rows. Elapsed time: {1} ms",
                    dataTable.Rows.Count, timer.Elapsed.Milliseconds));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Can't read file!", ex);
                return false;
            }
        }

        abstract public bool Export(String path, Localisation localisation);
    }
}