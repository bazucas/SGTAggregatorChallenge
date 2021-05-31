using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Santander.Api.ApiModels;
using Santander.Api.Controllers.BaseController;
using Santander.Api.Errors;
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

        /// <summary>
        /// Post method that returns the aggregation.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public IActionResult GetAggregation([FromBody] AggregateRequest req)
        {
            // Test request ids for duplicates and irregular values
            //var ids = req.Ids.Distinct().Where(val => int.TryParse(val, out _)).ToArray();

            // Test and store invalid Ids
            var badIds = req.Ids.Distinct().Where(id => !int.TryParse(id, out _)).ToList();

            if (badIds.Any())
            {
                var message = $"HN: You have inserted {badIds.Count} invalid {(badIds.Count > 1 ? "ids": "id")}: {string.Join( ", ", badIds)}";
                return BadRequest(new ApiResponse(400, message));
            }

            // Get Hacker News uri from app settings
            var hnUrl = _config.GetValue<string>("MyConfigs:HNUrlPath");

            // Return processed object from Hacker News Aggregator Service
            var aggregatedObj = _aggregator.ProcessHnAggregator(hnUrl, req.Ids);

            // TODO: do something here if service returned a null value, eventually show a 404 NotFound
            // or send to the user the proper response as well as another object with the ids that didn't returned nothing 

            return Ok(aggregatedObj);
        }
    }
}
