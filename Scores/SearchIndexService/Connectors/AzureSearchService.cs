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
            string adminApiKey = "BFA34BBD268E9E25BC23E2961341DB09";

            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
        }

        public DocumentSearchResult<Contest> RunQuery(SearchIndexParameters parameters)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
