using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecuritiesApi.Data;

namespace SecuritiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly FinanceSecurityContext _securityContext;
        private readonly IOptionsSnapshot<FinanceSecuritiesSettings> _settings;
        public SecurityController(FinanceSecurityContext catalogContext, IOptionsSnapshot<FinanceSecuritiesSettings> settings)
        {
            _securityContext = catalogContext;
            _settings = settings;
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
        public async Task<IActionResult> MutuaFunds()
        {
            var items = await _securityContext.MutualFunds.ToListAsync();
            return Ok(items);
        }
    }
}