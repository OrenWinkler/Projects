using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySync
{
    /// <summary>
    /// Keeps time range per Sport Type, to avoid duplication in db.
    /// </summary>
    public static class SportTypeTimeRangeDuplication
    {
        public static Dictionary<string, int> _duplicationTimeRange = new Dictionary<string, int>();

        static SportTypeTimeRangeDuplication()
        {
            _duplicationTimeRange.Add("Soccer", 2);
            _duplicationTimeRange.Add("Baseball", 3);
            _duplicationTimeRange.Add("Basketball", 1);
        }
        public static int GetTimeRangeBySportType(string sportType)
        {
            return _duplicationTimeRange[sportType];
        }
    }
}
