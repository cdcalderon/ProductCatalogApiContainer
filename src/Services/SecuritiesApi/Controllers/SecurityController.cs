using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
        private readonly FinanceSecuritiesSettings _settings;
        public SecurityController(IOptionsSnapshot<FinanceSecuritiesSettings> settings, ISecurityQuoteService securityQuoteService)
        {
            _settings = settings.Value;
            _securityQuoteService = securityQuoteService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetStocks()
        {
            try
            {
                var items = await _securityQuoteService.GetStocks();
                return Ok(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
           
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetExchanges()
        {
            try
            {
                var items = await _securityQuoteService.GetExchanges();
                return Ok(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2", "value3", "value4", _settings.AzureDbConnection };
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> PopulateStocks()
        {
            var stocks = await _securityQuoteService.GetStocks();

            foreach (var stock in stocks)
            {
                var quotes = await _securityQuoteService.GetQuotes(stock.Symbol, new DateTime(2017, 1, 1), DateTime.Now);
                _securityQuoteService.AddQuotesToStock(quotes, stock);
            }
            
            return Ok("Done");
        }

        
    }
}