using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecuritiesApi.Abstractions;
using SecuritiesApi.Domain;
using SecuritiesApi.DTO;
using TicTacTec.TA.Library;
using YahooFinanceApi;

namespace SecuritiesApi.Services
{
    public class SecurityQuoteService: ISecurityQuoteService
    {
        public async Task<IReadOnlyList<Candle>> GetQuotes(string symbol, DateTime from, DateTime to)
        {
            return await Yahoo.GetHistoricalAsync(symbol,from , to, Period.Daily);
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

        public IEnumerable<Stock> GetStocksWithMovingAvg10(IEnumerable<Candle> historicalQuotes, MovingAvgInfo movingAvgInfo)
        {
            var stocks = new List<Stock>();
            var quotes = historicalQuotes.Skip(movingAvgInfo.StartIndex).Take(movingAvgInfo.EndIndex);
            var movingAvgs = movingAvgInfo.MovingAverages.ToArray();
            int idx = 0;
            foreach(var quote in quotes)
            {
                var movingAvg = movingAvgs[idx];
                var stock = new Stock()
                {
                    DayHigh = quote.High,
                    DayLow = quote.Low,
                    RetrievalDateTime = quote.DateTime,
                    Open = quote.Open,
                    Last = quote.Close,
                    Volume = quote.Volume,
                    MovingAverage10 = Convert.ToDecimal(movingAvg)
                };
                stocks.Add(stock);
                idx++;
            }

            return stocks;
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

        public IEnumerable<Stock> GetStocksWithMacds(IEnumerable<Candle> historicalQuotes, MacdInfo macdInfo)
        {
            var stocks = new List<Stock>();
            var quotes = historicalQuotes.Skip(macdInfo.StartIndex).Take(macdInfo.EndIndex);
            var macds = macdInfo.Macds.ToArray();
            int idx = 0;
            foreach (var quote in quotes)
            {
                var macd = macds[idx];
                var stock = new Stock()
                {
                    DayHigh = quote.High,
                    DayLow = quote.Low,
                    RetrievalDateTime = quote.DateTime,
                    Open = quote.Open,
                    Last = quote.Close,
                    Volume = quote.Volume,
                    Macd8179 = Convert.ToDecimal(macd)
                };
                stocks.Add(stock);
                idx++;
            }

            return stocks;
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

        public IEnumerable<Stock> GetStocksWithStochastics(IEnumerable<Candle> historicalQuotes, StochasticsInfo stochasticsInfoInfo)
        {
            var stocks = new List<Stock>();
            var quotes = historicalQuotes.Skip(stochasticsInfoInfo.StartIndex).Take(stochasticsInfoInfo.EndIndex);
            var stochastics = stochasticsInfoInfo.StochasticsSlowsK.ToArray();
            int idx = 0;
            foreach (var quote in quotes)
            {
                var stoch = stochastics[idx];
                var stock = new Stock()
                {
                    DayHigh = quote.High,
                    DayLow = quote.Low,
                    RetrievalDateTime = quote.DateTime,
                    Open = quote.Open,
                    Last = quote.Close,
                    Volume = quote.Volume,
                    StochasticsSlowK1450 = Convert.ToDecimal(stoch)
                };
                stocks.Add(stock);
                idx++;
            }

            return stocks;
        }

        public IEnumerable<Stock> SetStockIndicatorsForSignals(IEnumerable<Candle> historicalQuotes)
        {
            var stocks = new List<Stock>();
            var mvAvgs10Info = this.GetMovingAveragesByPeriod(historicalQuotes, 10);
            var macdsInfo = this.GetMACD(historicalQuotes);
            var stochasticsInfo = this.GetStochastics(historicalQuotes);

            var mvgAvgs = mvAvgs10Info.MovingAverages.Skip(macdsInfo.StartIndex - mvAvgs10Info.StartIndex).ToArray();
            var macds = macdsInfo.Macds;
            var stochastics = stochasticsInfo.StochasticsSlowsK.Skip(macdsInfo.StartIndex - stochasticsInfo.StartIndex).ToArray();
            

            var quotes = historicalQuotes.Skip(macdsInfo.StartIndex).Take(macdsInfo.EndIndex);

            int idx = 0;
            foreach (var quote in quotes)
            {
                var mvAvg10 = mvgAvgs[idx];
                var stoch = stochastics[idx];
                var macd = macds[idx];
                var stock = new Stock()
                {
                    DayHigh = quote.High,
                    DayLow = quote.Low,
                    RetrievalDateTime = quote.DateTime,
                    Open = quote.Open,
                    Last = quote.Close,
                    Volume = quote.Volume,
                    MovingAverage10 = Convert.ToDecimal(mvAvg10),
                    Macd8179 = Convert.ToDecimal(macd),
                    StochasticsSlowK1450 = Convert.ToDecimal(stoch)
                };
                stocks.Add(stock);
                idx++;
            }

            return stocks;
        }
    }
}
