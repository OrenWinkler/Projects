using System;
using Common.BusinessObjects;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using QueueService;

namespace LiveScoresFeed
{
    /// <summary>
    /// Stateless function that samples LiveScores site every x seconds,
    /// and inser the results to storage.
    /// </summary>
    public static class LiveScoresFeedFunction
    {
        [FunctionName("GetLiveScoresFeed")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"GetLiveScoresFeed Timer trigger function executed at: {DateTime.Now}");

            FeedUpdatesConsumer consumer = new FeedUpdatesConsumer();
            var gamedetails = consumer.GetGameDetails();

            if(gamedetails != null)
            {
                SendMessageToQueue(gamedetails);
            }

        }

        private static void SendMessageToQueue(GameUpdateResult gamedetails)
        {
            using (IQueueConnector queueConnector = new AzureServiceBusConnector())
            {
                queueConnector.SendMessage(gamedetails);
            }
        }
    }
}
