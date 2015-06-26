using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConversion
{
    /// <summary>
    /// Формат файла
    /// </summary>
    public enum ConversionFormat
    {
        CSV,
        XLSX,
        ARFF
    }

    /// <summary>
    /// Адаптер конвертеров
    /// </summary>
    public class ConverterAdapter
    {
        /// <summary>
        /// Конвертирует массив данных в нужнный формат с учетом нужной локализации
        /// </summary>
        /// <param name="inputData">Входные данные</param>
        /// <param name="outputFilePath">Файл, в который нужно сохранить данные</param>
        /// <param name="format">Формат файла, в который нужно сохранить</param>
        /// <param name="localisation">Выбранная локализация</param>
        /// <returns>Успех конвертации</returns>
        public static bool convert(System.Data.DataTable inputData, string outputFilePath, ConversionFormat format, Localisation localisation)
        {
            if (format == ConversionFormat.CSV)
            {
                var conv = new CSVConverter();
                return conv.Import(inputData) && conv.Export(outputFilePath, localisation);
            }
            else if (format == ConversionFormat.XLSX)
            {
                var conv = new XLSXConverter();
                return conv.Import(inputData) && conv.Export(outputFilePath, localisation);
            }
            else if (format == ConversionFormat.ARFF)
            {
                var conv = new ARFFConverter();
                return conv.Import(inputData) && conv.Export(outputFilePath, localisation);
            }
            return false;
        }
    }
}
