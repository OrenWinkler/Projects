using QueueService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoresTester
{
    class Program
    {
        static void Main(string[] args)
        {
            RESTClient client = new RESTClient();
            var response = client.GetLiveScores();

            //using (IQueueConnector queueConnector = new AzureServiceBusConnector())
            //{
                IQueueConnector queueConnector = new AzureServiceBusConnector();
                queueConnector.SendMessage(response);
            //}

        }
    }
}
