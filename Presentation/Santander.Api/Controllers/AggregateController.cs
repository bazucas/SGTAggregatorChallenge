using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Santander.Api.ApiModels;
using Santander.Api.Controllers.BaseController;
using Santander.HnService.Interfaces;

namespace Santander.Api.Controllers
{
    public class AggregateController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly IHnAggregatorService _aggregator;
        public AggregateController(IConfiguration config, IHnAggregatorService aggregator)
        {
            _config = config;
            _aggregator = aggregator;
        }

        [HttpPost]
        public IActionResult GetAggregation([FromBody] AggregateRequest req)
        {
            // Test request ids for duplicates and irregular values
            var ids = req.Ids.Distinct().Where(val => int.TryParse(val, out _)).ToArray();

            // Get Hacker News uri from app settings
            var hnUrl = _config.GetValue<string>("MyConfigs:HNUrlPath");

            // Return processed object from Hacker News Aggregator Service
            return Ok(_aggregator.ProcessHnAggregator(hnUrl, ids));
        }
    }
}
