using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecuritiesApi.Abstractions;
using SecuritiesApi.Data;


namespace SecuritiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public readonly ISecurityQuoteService _securityQuoteService;
        private readonly FinanceSecurityContext _securityContext;
        private readonly IOptionsSnapshot<FinanceSecuritiesSettings> _settings;
        public SecurityController(FinanceSecurityContext catalogContext, 
            IOptionsSnapshot<FinanceSecuritiesSettings> settings, ISecurityQuoteService securityQuoteService)
        {
            _securityContext = catalogContext;
            _settings = settings;
            _securityQuoteService = securityQuoteService;
            ((DbContext)catalogContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Stocks()
        {
            var items = await _securityContext.Stocks.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> StocksWithAvgs()
        {
            var quotes = await _securityQuoteService.GetQuotes("AAPL", new DateTime(2017, 1, 1), DateTime.Now);
            var movingAvgs10 = _securityQuoteService.GetMovingAveragesByPeriod(quotes, 10);

            var stocksWithAvgs10 = _securityQuoteService.GetStocksWithMovingAvg10(quotes, movingAvgs10);


            var macds = _securityQuoteService.GetMACD(quotes);
            var stocksWithMacds = _securityQuoteService.GetStocksWithMacds(quotes, macds);

            return Ok(stocksWithAvgs10);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> MutuaFunds()
        {
            var items = await _securityContext.MutualFunds.ToListAsync();
            return Ok(items);
        }
    }
}