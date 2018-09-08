using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Domain
{
    public abstract class Security
    {
        // Primitive properties
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Company { get; set; }
    }
}
