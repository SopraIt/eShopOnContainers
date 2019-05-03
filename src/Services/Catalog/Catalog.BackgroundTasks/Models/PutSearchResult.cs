using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.BackgroundTasks.Models
{
    public class Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }

    public class PutSearchResult
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public int _version { get; set; }
        public string result { get; set; }
        public Shards _shards { get; set; }
        public int _seq_no { get; set; }
        public int _primary_term { get; set; }
    }
}
