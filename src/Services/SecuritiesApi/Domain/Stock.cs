using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Domain
{
    public class Stock : Security
    {
        // Primitive properties
        public decimal DayHigh { get; set; }
        public decimal DayLow { get; set; }
        public decimal Dividend { get; set; }
        public decimal Open { get; set; }
        public decimal Volume { get; set; }
        public decimal YearHigh { get; set; }
        public decimal YearLow { get; set; }
        public decimal AverageVolume { get; set; }
        public decimal AverageVolume30 { get; set; }
        public decimal MarketCap { get; set; }
        public decimal MovingAverage10 { get; set; }
        public decimal Macd8179 { get; set; }
        public decimal StochasticsSlowK1450 { get; set; }
        public int ExchangeId { get; set; }

        // Navigation properties
        public virtual Exchange Exchange { get; set; }
    }
}
