using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Common.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search.Models;
using SearchIndexService.Connectors;

namespace ScoresAPIService.Controllers
{
    /// <summary>
    /// API controller, hosted under Service Fabric, so it can be scaled out as necessary.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : Controller
    {
        /// <summary>
        /// Returns list of contests, according to time range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpPost("GetScores")]
        public string GetScores(string from, string to)
        {
            var dateFrom = DateTime.Parse(from);
            var dateTo = DateTime.Parse(to);

            var results = GetContestList(dateFrom, dateTo);

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(results);

            return jsonContent;
        }

        /// <summary>
        /// Using Search Index engine, retrieve to results.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private List<Contest> GetContestList(DateTime from, DateTime to)
        {
            string formattedFromDate = from.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string formattedToDate = to.ToString("yyyy-MM-ddTHH:mm:ssZ");

            ISearchService searchService = new AzureSearchService();
            SearchParameters parameters = new SearchParameters()
            {

            Filter = $"GameStartDate gt {formattedFromDate} and GameStartDate lt {formattedToDate}",
                Select = new[] { "id","GameStartDate","CompetitionName", "Teams", "SportType", "League" }
            };

            var results = searchService.RunQuery(parameters);

            List<Contest> contests = new List<Contest>();
            if (results != null && results.Results != null)
            {
                BuildResults(results, contests);
            }

            return contests;
        }

        private static void BuildResults(DocumentSearchResult<Contest> results, List<Contest> contests)
        {
            foreach (var result in results.Results)
            {
                Contest contest = new Contest()
                {
                    id = result.Document.id,
                    CompetitionName = result.Document.CompetitionName,
                    GameStartDate = result.Document.GameStartDate,
                    League = result.Document.League,
                    SportType = result.Document.SportType,
                    Teams = result.Document.Teams
                };

                contests.Add(contest);
            }
        }
    }
}
