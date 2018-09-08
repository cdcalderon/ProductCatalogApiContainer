using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecuritiesApi.Domain;

namespace SecuritiesApi.Data
{
    public class SecuritiesSeed
    {
        public static async Task SeedAsync(FinanceSecurityContext context)
        {
            context.Database.Migrate();
            if (!context.Exchanges.Any())
            {
                context.Exchanges.AddRange(GetSeededExchanges());
                await context.SaveChangesAsync();
            }
            if (!context.Securities.Any())
            {
                context.Securities.AddRange(GetSeededSecurities());
                await context.SaveChangesAsync();
            }

        }

        static IEnumerable<Exchange> GetSeededExchanges()
        {
            return new List<Exchange>()
            {
                new Exchange(){ Title = "Nasdaq" },
                new Exchange(){ Title = "NYSE" },
                new Exchange(){ Title = "S&P500" }
            };
        }
        static IEnumerable<Security> GetSeededSecurities()
        {
            return new List<Security>()
            {
                new Stock(){ Symbol = "AAPL", Company = "Apple Inc" ,ExchangeId = 1 },
                new MutualFund(){ Symbol = "MGF",MorningStarRating = 2}
            };
        }
    }
}
