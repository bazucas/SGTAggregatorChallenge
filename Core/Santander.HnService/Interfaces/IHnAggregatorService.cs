using System.Collections.Generic;
using Santander.HnService.Models;

namespace Santander.HnService.Interfaces
{
    public interface IHnAggregatorService
    {
        /// <summary>
        /// Processes Hn item through the aggregator
        /// </summary>
        /// <param name="hnUrl"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        string ProcessHnAggregator(string hnUrl, string[] req);

        /// <summary>
        /// Gets the urls by identifier.
        /// </summary>
        /// <param name="hnUrl">The hn URL.</param>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        IEnumerable<string> GetUrlsById(string hnUrl, IEnumerable<string> req);

        /// <summary>
        /// Converts the hn item to dictionary.
        /// </summary>
        /// <param name="baseList">The base list.</param>
        /// <returns></returns>
        Dictionary<string, Dictionary<string, int>> ConvertHnItemToDictionary(IEnumerable<HnBaseResponse> baseList);

        /// <summary>
        /// Aggregates each row in a dictionary.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="aggregate">The aggregate.</param>
        void AggregateEachRowInADictionary(HnBaseResponse item, ref Dictionary<string, Dictionary<string, int>> aggregate);
    }
}