using System;
using System.Collections.Generic;
using System.Text;
using Common.BusinessObjects;
using LiveScoresFeed.BuisnessObjects;
using RestSharp;
using RestSharp.Authenticators;

namespace LiveScoresFeed
{
    /// <summary>
    /// REST Client for LiveScores API
    /// </summary>
    public class LiveScoresRESTClient
    {
        private readonly string Key = "oE0Hp9euAWdZJ76U";
        private readonly string Secret = "G7phBURXMiYgReEgSEYiOcNo90nS7uMO";
        private readonly string URL = "http://livescore-api.com/api-client";

        public LiveScoresResult GetLiveScores()
        {
            try
            {
                string liveScoresURL = URL;
                var client = new RestClient(liveScoresURL);

                var request = new RestRequest($"/scores/live.json?key={Key}&secret={Secret}", DataFormat.Json);
                var response = client.Get<LiveScoresResult>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.IsSuccessful && response.Data.success &&
                    response.Data != null)
                {
                    return response.Data;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
