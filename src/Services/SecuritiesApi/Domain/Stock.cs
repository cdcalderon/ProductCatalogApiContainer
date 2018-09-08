using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Domain
{
    public class Stock : Security
    {
        // Primitive properties
        
        public int ExchangeId { get; set; }

        // Navigation properties
        public virtual Exchange Exchange { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
