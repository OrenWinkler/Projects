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
        public Contest()
        {
            id = Guid.NewGuid().ToString();
        }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "GameStartDate")]
        public DateTime GameStartDate { get; set; }

        [JsonProperty(PropertyName = "CompetitionName")]
        public string CompetitionName { get; set; }

        [JsonProperty(PropertyName = "HomeTeam")]
        public string HomeTeam { get; set; }

        [JsonProperty(PropertyName = "AwayTeam")]
        public string AwayTeam { get; set; }

        [JsonProperty(PropertyName = "SportType")]
        public string SportType { get; set; }
        
        [JsonProperty(PropertyName = "League")]
        public string League { get; set; }
    }
}
