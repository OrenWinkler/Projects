using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoresTester
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Outcomes
    {
        public string half_time { get; set; }
        public string full_time { get; set; }
        public object extra_time { get; set; }

    }

    public class Match
    {
        public int competition_id { get; set; }
        public string league_name { get; set; }
        public object events { get; set; }
        public int league_id { get; set; }
        public int away_id { get; set; }
        public string ft_score { get; set; }
        public string status { get; set; }
        public string added { get; set; }
        public int id { get; set; }
        public string ht_score { get; set; }
        public string et_score { get; set; }
        public string competition_name { get; set; }
        public string last_changed { get; set; }
        public string location { get; set; }
        public string away_name { get; set; }
        public string home_name { get; set; }
        public int home_id { get; set; }
        public string score { get; set; }
        public string time { get; set; }
        public int fixture_id { get; set; }
        public string scheduled { get; set; }
        public Outcomes outcomes { get; set; }
        public string info { get; set; }

    }

    public class Data
    {
        public List<Match> match { get; set; }

    }

    public class LiveScoresResult
    {
        public bool success { get; set; }
        public Data data { get; set; }

    }
}
