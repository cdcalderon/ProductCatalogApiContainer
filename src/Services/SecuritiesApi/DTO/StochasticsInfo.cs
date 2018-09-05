using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.DTO
{
    public class StochasticsInfo
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double[] StochasticsSlowsK { get; set; }
    }
}
