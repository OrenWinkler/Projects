using Common.BusinessObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoresTester
{
    public class RESTClient
    {
        private readonly string Key = "oE0Hp9euAWdZJ76U";
        private readonly string Secret = "G7phBURXMiYgReEgSEYiOcNo90nS7uMO";

        public GameUpdateResult GetLiveScores()
        {

            string liveScoresURL = $"http://livescore-api.com/api-client";
            var client = new RestClient(liveScoresURL);

            var request = new RestRequest($"/scores/live.json?key={Key}&secret={Secret}", DataFormat.Json);
            var response = client.Get<LiveScoresResult>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK &&
                response.IsSuccessful && response.Data.success &&
                response.Data != null)
            {
                return ConvertResponse(response.Data);
            }
            else
                return null;
        }

        private GameUpdateResult ConvertResponse(LiveScoresResult response)
        {
            var result = new GameUpdateResult();
            if (response.data.match.Count > 0)
                result.Contests = new List<Contest>();

            foreach (var match in response.data.match)
            {
                Contest contest = new Contest()
                {
                    CompetitionName = match.competition_name,
                    GameStartDate = DateTime.Parse(match.scheduled),
                    SportType = "Soccer",
                    Teams = new List<string>()
                };

                contest.Teams.Add(match.home_name);
                contest.Teams.Add(match.away_name);

                result.Contests.Add(contest);
            }

            return result;
        }
    }
}
