using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecuritiesApi.Domain;
using SecuritiesApi.DTO;
using YahooFinanceApi;

namespace SecuritiesApi.Abstractions
{
    public interface ISecurityQuoteService
    {
        Task<IReadOnlyList<Candle>> GetQuotes(string symbol, DateTime from, DateTime to);
        MovingAvgInfo GetMovingAveragesByPeriod(IEnumerable<Candle> historicalQuotes, int period);
        IEnumerable<Stock> GetStocksWithMovingAvg10(IEnumerable<Candle> historicalQuotes, MovingAvgInfo movingAvgInfo);

        MacdInfo GetMACD(IEnumerable<Candle> historicalQuotes);
        IEnumerable<Stock> GetStocksWithMacds(IEnumerable<Candle> historicalQuotes, MacdInfo macdInfo);
    }
}
