using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecuritiesApi.Abstractions;
using SecuritiesApi.DTO;

namespace SecuritiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ISecurityQuoteService _securityQuoteService;

        public StocksController(ISecurityQuoteService securityQuoteService)
        {
            _securityQuoteService = securityQuoteService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddStock(Stock stock)
        {
            var items = await _securityQuoteService.GetStocks();
            return Ok(items);
        }
    }
}