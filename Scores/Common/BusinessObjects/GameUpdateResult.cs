using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessObjects
{
    public class GameUpdateResult
    {
        public List<Contest> Contests { get; set; }
    }

    public class Contest
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "GameStartDate")]
        public DateTime GameStartDate { get; set; }

        [JsonProperty(PropertyName = "CompetitionName")]
        public string CompetitionName { get; set; }

        [JsonProperty(PropertyName = "Teams")]
        public List<string> Teams { get; set; }

        [JsonProperty(PropertyName = "SportType")]
        public string SportType { get; set; }
        
        [JsonProperty(PropertyName = "League")]
        public string League { get; set; }

        [JsonProperty(PropertyName = "ContestID")]
        public string ContestID { get; set; }

        public string CreateID()
        {
            if(string.IsNullOrEmpty(id))
            {
                string homeTeam = string.Empty;
                string awayTeam = string.Empty;

                if (Teams != null && Teams.Count >= 2)
                {
                    homeTeam = Teams[0];
                    awayTeam = Teams[1];
                }

                id = $"{SportType}{League}{homeTeam}{awayTeam}".ToLower().Replace(" ","");
            }

            return id;
        }
    }
}
