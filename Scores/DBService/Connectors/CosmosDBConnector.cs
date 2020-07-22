using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using Common.BusinessObjects;
using Microsoft.Azure.Cosmos;

namespace DBService.Connectors
{
    public class CosmosDBConnector : IDBConnector
    {

        private readonly string EndpointUri = "https://dbcosmosaccount.documents.azure.com:443/";
        private readonly string PrimaryKey = "x66jqw9p4CPNU0w7RgXhFaokjuFOTudS0vYhKCVH8vH3Yd5L0QCKpIdjo4bNRu11eHGqocPibLcD56rTseYsrg==";
        private readonly string databaseId = "GamesDB";
        private readonly string containerId = "GamesRepository";

        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        /// <summary>
        /// Exposes ConmosDB connector, to add new documents to storage
        /// </summary>
        public CosmosDBConnector()
        {
            _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "DBService" });
            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _cosmosClient.GetContainer(databaseId, containerId);
        }

        public void AddContest(Contest contest)
        {
            ItemResponse<Contest> contestResponse = null;
            try
            {
                contestResponse = _container.CreateItemAsync<Contest>(contest, new PartitionKey(contest.SportType)).Result;
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", contestResponse.Resource.id, contestResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                string id = (contestResponse != null) ? contestResponse.Resource.id : "";
                Console.WriteLine("Created item in database with id: {0} failed.\n", id);
            }
        }
    }
}
