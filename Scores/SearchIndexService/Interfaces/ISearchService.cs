using Common.BusinessObjects;
using Microsoft.Azure.Search.Models;

namespace SearchIndexService.Connectors
{
    public interface ISearchService
    {
        DocumentSearchResult<Contest> RunQuery(SearchParameters parameters);
    }
}