using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.BackgroundTasks.Models
{
    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class OCCAuth
    {
        public string access_token { get; set; }
        public List<Link> links { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
