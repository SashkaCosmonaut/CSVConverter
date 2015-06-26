using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using PicNetML.Arff;
using weka.core.converters;

namespace DataConversion
{
    public class ARFFConverter : Converter
    {
        public override bool Export(String path, Localisation localisation)
        {
            try
            {
                Console.WriteLine("Export to .arff...");
                var timer = new Stopwatch();
                timer.Start();

                // Sorry, ARFF Converter is not ready now!

                timer.Stop();
                Console.WriteLine("Export complete! Elapsed time: {0} ms", timer.Elapsed.Milliseconds);
                return true;
            }
            catch
            {
                Console.WriteLine("Can't write .arff file!");
                return false;
            }
        }
    }
}