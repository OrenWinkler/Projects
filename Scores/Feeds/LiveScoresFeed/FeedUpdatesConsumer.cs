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
            RESTClient client = new RESTClient();
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
