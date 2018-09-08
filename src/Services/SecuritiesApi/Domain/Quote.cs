using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public decimal DayHigh { get; set; }
        public decimal DayLow { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public DateTime DateTime { get; set; }
        public decimal MovingAverage10 { get; set; }
        public decimal Macd8179 { get; set; }
        public decimal StochasticsSlowK1450 { get; set; }
    }
}
