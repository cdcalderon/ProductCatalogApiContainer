﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.DTO
{
    public class MacdInfo
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double[] Macds { get; set; }
    }
}
