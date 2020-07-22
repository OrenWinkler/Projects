using Common.BusinessObjects;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIndexService.Connectors
{
    /// <summary>
    /// Connector for Azure Serach Index service.
    /// gives abilities to run fast searches on storage repository.
    /// </summary>
    public class AzureSearchService : ISearchService
    {
        private SearchServiceClient _searchClient;

        public AzureSearchService()
        {
            string searchServiceName = "searchserviceindex";
            string adminApiKey = "C03171D8466447FBC743BAD0CA592431";

            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
        }

        public DocumentSearchResult<Contest> RunQuery(SearchIndexParameters parameters)
        {
            SearchParameters searchParameters = new SearchParameters()
            {
                Filter = parameters.Filter,
                Select = parameters.Select
            };

            ISearchIndexClient indexClient = _searchClient.Indexes.GetClient("scoresindex");
            var results = indexClient.Documents.Search<Contest>("*", searchParameters);

            return results;
        }
    }
}
