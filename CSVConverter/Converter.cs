﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConversion
{
    abstract public class Converter
    {
        public bool Import(String path)
        {
            return false;
        }

        abstract public bool Export(String path);
    }
}
