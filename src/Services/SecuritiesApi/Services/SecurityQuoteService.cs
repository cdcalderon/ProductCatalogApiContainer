using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecuritiesApi.Abstractions;
using SecuritiesApi.Data;
using SecuritiesApi.Domain;
using SecuritiesApi.DTO;
using TicTacTec.TA.Library;
using YahooFinanceApi;

namespace SecuritiesApi.Services
{
    public class SecurityQuoteService: ISecurityQuoteService
    {
        private readonly FinanceSecurityContext _financeSecurityContext;

        public SecurityQuoteService(FinanceSecurityContext financeSecurityContext)
        {
            _financeSecurityContext = financeSecurityContext;
            ((DbContext)financeSecurityContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IReadOnlyList<Candle>> GetQuotes(string symbol, DateTime from, DateTime to)
        {
            return await Yahoo.GetHistoricalAsync(symbol,from , to, Period.Daily);
        }

        public async Task<IEnumerable<Stock>> GetStocks()
        {
            return await _financeSecurityContext.Stocks.ToListAsync();
        }

        public MovingAvgInfo GetMovingAveragesByPeriod(IEnumerable<Candle> historicalQuotes, int period)
        {
            int outBegIndex = 0;
            int outNbElement = 0;
            var closePrices = historicalQuotes.Select(x => Convert.ToSingle(x.Close)).ToArray();
            var outMovingAverages = new double[closePrices.Length];

            var resultState = TicTacTec.TA.Library.Core.MovingAverage(
                0, 
                closePrices.Length - 1,
                closePrices, period,
                Core.MAType.Ema,
                out outBegIndex,
                out outNbElement,
                outMovingAverages);

            return new MovingAvgInfo
            {
                StartIndex = outBegIndex,
                EndIndex = outNbElement,
                MovingAverages = outMovingAverages,
                Period = period
            };
        }
        

        public MacdInfo GetMACD(IEnumerable<Candle> historicalQuotes)
        {
            int outBegIndex = 0;
            int outNbElement = 0;
            var closePrices = historicalQuotes.Select(x => Convert.ToSingle(x.Close)).ToArray();
            var outMacds = new double[closePrices.Length];
            var outMacdSignals = new double[closePrices.Length];
            var outMacdHis = new double[closePrices.Length];

            var resultState = TicTacTec.TA.Library.Core.Macd(0, closePrices.Length - 1, closePrices, 
                8, 17 ,9, out outBegIndex, out outNbElement, outMacds, outMacdSignals, outMacdHis);

            return new MacdInfo
            {
                StartIndex = outBegIndex,
                EndIndex = outNbElement,
                Macds = outMacdHis
            };
        }
        
        public StochasticsInfo GetStochastics(IEnumerable<Candle> historicalQuotes)
        {
            int outBegIndex = 0;
            int outNbElement = 0;
            var closePrices = historicalQuotes.Select(x => Convert.ToSingle(x.Close)).ToArray();
            var highPrices = historicalQuotes.Select(x => Convert.ToSingle(x.High)).ToArray();
            var lowPrices = historicalQuotes.Select(x => Convert.ToSingle(x.Low)).ToArray();
            var slowKs = new double[closePrices.Length];
            var slowDs = new double[closePrices.Length];

            var resultState = TicTacTec.TA.Library.Core.Stoch(0, closePrices.Length - 1, highPrices,
                lowPrices, closePrices, 14, 5, 0, 5, Core.MAType.Ema, out outBegIndex, out outNbElement, slowKs, slowDs);

            return new StochasticsInfo
            {
                StartIndex = outBegIndex,
                EndIndex = outNbElement,
                StochasticsSlowsK = slowKs
            };
        }

        
        public void AddQuotesToStock(IEnumerable<Candle> historicalQuotes, Stock stock)
        {
            var mvAvgs10Info = this.GetMovingAveragesByPeriod(historicalQuotes, 10);
            var macdsInfo = this.GetMACD(historicalQuotes);
            var stochasticsInfo = this.GetStochastics(historicalQuotes);

            var mvgAvgs = mvAvgs10Info.MovingAverages.Skip(macdsInfo.StartIndex - mvAvgs10Info.StartIndex).ToArray();
            var macds = macdsInfo.Macds;
            var stochastics = stochasticsInfo.StochasticsSlowsK.Skip(macdsInfo.StartIndex - stochasticsInfo.StartIndex).ToArray();
            var quotes = historicalQuotes.Skip(macdsInfo.StartIndex).Take(macdsInfo.EndIndex);

            int idx = 0;
            foreach (var q in quotes)
            {
                var mvAvg10 = mvgAvgs[idx];
                var stoch = stochastics[idx];
                var macd = macds[idx];
                var quote = new Quote()
                {
                    DayHigh = q.High,
                    DayLow = q.Low,
                    DateTime = q.DateTime,
                    Open = q.Open,
                    Close = q.Close,
                    Volume = q.Volume,
                    MovingAverage10 = Convert.ToDecimal(mvAvg10),
                    Macd8179 = Convert.ToDecimal(macd),
                    StochasticsSlowK1450 = Convert.ToDecimal(stoch)
                };
                stock.Quotes.Add(quote);
                idx++;
            }
        }
    }
}
