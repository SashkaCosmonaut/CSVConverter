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

namespace DataConversion
{
    abstract public class Converter
    {
        protected DataTable dataTable;

        public bool Import(String path)
        {
            try
            {
                Console.WriteLine("Import...");
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
                Console.WriteLine("Import complete! Csv file contains {0} rows. Elapsed time: {1} ms", dataTable.Rows.Count, timer.Elapsed.Milliseconds);
                return true;
            }
            catch
            {
                Console.WriteLine("Can't read file!");
                return false;
            }
        }

        abstract public bool Export(String path);
    }
}