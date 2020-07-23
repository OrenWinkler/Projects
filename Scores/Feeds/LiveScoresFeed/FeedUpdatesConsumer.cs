using Common.BusinessObjects;
using LiveScoresFeed.BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveScoresFeed
{
    public class FeedUpdatesConsumer
    {
        public GameUpdateResult GetGameDetails()
        {
            LiveScoresRESTClient client = new LiveScoresRESTClient();
            var response = client.GetLiveScores();

            return (response != null) ? ConvertResponse(response) : null;
        }

        private GameUpdateResult ConvertResponse(LiveScoresResult response)
        {
            var result = new GameUpdateResult();

            result.Contests = new List<Contest>();
            foreach (var match in response.data.match)
            {
                Contest contest = new Contest()
                {
                    CompetitionName = match.competition_name,
                    GameStartDate = DateTime.Parse(match.scheduled),
                    SportType = "Soccer",
                    HomeTeam = match.home_name,
                    AwayTeam = match.away_name
                };

                result.Contests.Add(contest);
            }

            return result;
        }
    }
}
