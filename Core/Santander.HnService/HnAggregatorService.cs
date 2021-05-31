using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Santander.HnService.Interfaces;
using Santander.HnService.Models;

namespace Santander.HnService
{
    public class HnAggregatorService : IHnAggregatorService
    {
         /// <summary>
         /// Processes Hn item through the aggregator
         /// </summary>
         /// <param name="hnUrl"></param>
         /// <param name="req"></param>
         /// <returns></returns>
         public string ProcessHnAggregator(string hnUrl, string[] req)
         {
             // Create new url for each id
             var urls = GetUrlsById(hnUrl, req);

             // Instantiate httpClient, list all tasks and wait for them all (kind of a forkJoin)
             using var client = new HttpClient();
             var tasks = urls.Select(url => client.GetStringAsync(url)).ToList();
             Task.WaitAll(tasks.ToArray());

             // JsonSerializer camelCase serializer option added
             var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

             // Deserialize all responses from Hn endpoint
             var baseList = tasks.Select(task => JsonSerializer.Deserialize<HnBaseResponse>(task.Result, options)).ToList();

             // 
             var formattedOutput = ConvertHnItemToDictionary(baseList);

             return JsonSerializer.Serialize(formattedOutput);
         }

        /// <summary>
        /// Gets the urls by identifier.
        /// </summary>
        /// <param name="hnUrl">The hn URL.</param>
        /// <param name="req">The req.</param>
        /// <returns></returns>
        public IEnumerable<string> GetUrlsById(string hnUrl, IEnumerable<string> req) => req.Select(item => hnUrl.Replace("[ID]", item)).ToList();


        /// <summary>
        /// Converts the hn item to dictionary.
        /// </summary>
        /// <param name="baseList">The base list.</param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, int>> ConvertHnItemToDictionary(IEnumerable<HnBaseResponse> baseList)
        {
            var aggregate = new Dictionary<string, Dictionary<string, int>>();

            foreach (var item in baseList)
            {
                AggregateEachRowInADictionary(item, ref aggregate);
            }

            return aggregate;
        }

        /// <summary>
        /// Aggregates each row in a dictionary.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="aggregate">The aggregate.</param>
        public void AggregateEachRowInADictionary(HnBaseResponse item, ref Dictionary<string, Dictionary<string, int>> aggregate)
        {
            // null response from Hn endpoint should be ignored
            if (item == null) return;

            // valid response should create new entry
            if (aggregate.ContainsKey(item.Type))
            {
                // key with this name exists, iterate through all inner dictionaries to find the correct one
                foreach (var (_, value) in aggregate.Where(actionType => actionType.Key == item.Type))
                {
                    // it doesn't contain the specific author name
                    if (value.ContainsKey(item.By))
                    {
                        // we already have a category and a author inserted, just need to update de value
                        foreach (var (_, _) in value.Where(authorSum => authorSum.Key == item.By))
                        {
                            aggregate[item.Type][item.By] += 1;
                        }
                    }
                    else
                    {
                        // add author with 1 action on that category (type) 
                        aggregate[item.Type].Add(item.By, 1);
                    }
                }
            }
            else
            {
                // key with this name doesn't exist in dictionary, so add it
                aggregate.Add(item.Type, new Dictionary<string, int> { { item.By, 1 } });
            }
        }
    }
}