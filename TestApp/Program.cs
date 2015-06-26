using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using DataConversion;

namespace TestApp
{
    class Program
    {
        /// <summary>
        /// Пример массива данных
        /// </summary>
        /// <returns>Массив данных</returns>
        static DataTable GetTestDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(double));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            table.Rows.Add(0.213235, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50.2342345, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10.4, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21.3, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100.44, "Dilantin", "Melanie", DateTime.Now);

            return table;
        }

        /// <summary>
        /// Пример использования библиотеки
        /// </summary>
        static void Main(string[] args)
        {
            string path = @"C:\Users\nsv\Documents\repo\CSVConverter\CSVConverter\";

            ConverterAdapter.convert(GetTestDataTable(), path + "result_en", ConversionFormat.ARFF, Localisation.Russian);
        }
    }
}