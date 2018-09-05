using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.DTO
{
    public class MovingAvgInfo
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Period { get; set; }
        public double[] MovingAverages { get; set; }
    }
}
