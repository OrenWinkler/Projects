using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIndexService
{
    public class SearchIndexParameters
    {
        public IList<string> Select { get; set; }
        public string Filter { get; set; }
    }
}
