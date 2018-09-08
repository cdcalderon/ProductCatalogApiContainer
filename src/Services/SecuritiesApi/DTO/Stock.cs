using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace SecuritiesApi.DTO
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Company { get; set; }
        public int ExchangeId { get; set; }
    }
}
