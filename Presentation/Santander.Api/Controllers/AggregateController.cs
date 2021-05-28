using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Santander.AggregatrApi.Controllers
{
    public class AggregateController : BaseApiController
    {
        private readonly IConfiguration _config;

        public AggregateController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult GetAggregation([FromBody] AggregateRequest req)
        {
            // Test request ids for duplicates and irregular values
            var ids = req.Ids.Distinct().Where(val => int.TryParse(val, out _)).ToArray();

            // Get Hacker News uri from app settings
            var hnUrl = _config.GetValue<string>("MyConfigs:HNUrlPath");

            // Return processed object from Hacker News Aggregator Service
            // var aggregation = HnAggregatorService.ProcessHnAggregator(hnUrl, ids);
            return Ok();
        }
    }
}
