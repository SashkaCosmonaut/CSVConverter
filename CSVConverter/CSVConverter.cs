using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using Kent.Boogaart.KBCsv;
using Kent.Boogaart.KBCsv.Extensions;
using Kent.Boogaart.KBCsv.Extensions.Data;

namespace DataConversion
{
    public class CSVConverter : Converter
    {
        public override bool Export(String path)
        {
            try
            {
                Console.WriteLine("Export to .csv...");
                var timer = new Stopwatch();
                timer.Start();

                using (var streamWriter = new StreamWriter(path))
                using (var writer = new CsvWriter(streamWriter))
                {
                    //foreach (DataRow row in dataTable.Rows)
                    //{
                    //    Console.WriteLine("--- Row ---");
                    //    foreach (var item in row.ItemArray)
                    //    {
                    //        Console.Write("Item: ");
                    //        Console.WriteLine(item);
                    //    }
                    //}

                    writer.ValueSeparator = ';';
                    dataTable.WriteCsv(writer);
                }

                timer.Stop();
                Console.WriteLine("Export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds);
                return true;
            }
            catch
            {
                Console.WriteLine("Can't write .csv file!");
                return false;
            }
        }
    }
}