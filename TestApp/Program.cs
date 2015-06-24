using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DataConversion;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new DataConversion.XLSXConverter();
            converter.Import(@"C:\Users\nsv\Documents\repo\CSVConverter\CSVConverter\test_data.csv");
            //Debug.Assert(converter.Import("test_data.csv"), "Can't import data!");
            //Debug.Assert(converter.Export("output_test_data.xlsx"), "Can't export data!");
        }
    }
}