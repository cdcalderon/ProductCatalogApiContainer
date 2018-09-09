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
        Task<IEnumerable<Domain.Stock>> GetStocks();
        Task AddStock(DTO.Stock stockDto);
        Task<IReadOnlyList<Candle>> GetQuotes(string symbol, DateTime from, DateTime to);
        MovingAvgInfo GetMovingAveragesByPeriod(IEnumerable<Candle> historicalQuotes, int period);
        MacdInfo GetMACD(IEnumerable<Candle> historicalQuotes);
        StochasticsInfo GetStochastics(IEnumerable<Candle> historicalQuotes);
        void AddQuotesToStock(IEnumerable<Candle> historicalQuotes, Domain.Stock stock);
        Task<IEnumerable<Domain.Exchange>> GetExchanges();
    }
}
