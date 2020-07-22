using System;
using Common.BusinessObjects;
using DBService.Connectors;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SearchIndexService.Connectors;

namespace RepositorySync
{
    public static class QueueTriggerFunction
    {
        /// <summary>
        /// Stateless function that sych every message incoming to Feeds Queue, 
        /// with DB Repository.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        [FunctionName("QueueTrigger")]
        public static void Run([ServiceBusTrigger("gamesfeed", Connection = "gamesreposiroty_RootManageSharedAccessKey_SERVICEBUS")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"ServiceBus queue trigger function processed message: {myQueueItem}");

            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<GameUpdateResult>(myQueueItem);

            if(message != null && message.Contests != null)
            {
                IDBConnector dbConnector = new CosmosDBConnector();

                foreach(var contest in message.Contests)
                {
                    contest.CreateID();

                    bool isItemExists = CheckDuplication(dbConnector, contest);

                    if(!isItemExists)
                        dbConnector.AddContest(contest);
                }
            }
        }

        private static bool CheckDuplication(IDBConnector dbConnector, Contest contest)
        {
            int sportTypeDuplicationRange = SportTypeTimeRangeDuplication.GetTimeRangeBySportType(contest.SportType);
            
            ISearchService searchService = new AzureSearchService();
            SearchParameters parameters = new SearchParameters()
            {
                Filter = $"id eq '{contest.id}' and GameStartDate gt {contest.GameStartDate.AddHours(-sportTypeDuplicationRange).ToString("yyyy-MM-ddTHH:mm:ssZ")} and GameStartDate lt {contest.GameStartDate.AddHours(sportTypeDuplicationRange).ToString("yyyy-MM-ddTHH:mm:ssZ")}",
                Select = new[] { "id" }
            };

            var results = searchService.RunQuery(parameters);

            if (results == null || (results != null && results.Results.Count == 0))
                return false;
            else
                return true;
        }
    }
}
