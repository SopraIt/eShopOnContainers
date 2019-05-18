using System.Collections.Generic;

namespace Catalog.BackgroundTasks.Models.Magento2
{
    public class Import{
        public string _id { get; set; }
        public string _index { get; set; }
        public string _type { get; set; }
        public string _score { get; set; }
        public object _source { get; set; }
        public bool Imported { get; set; }
    }
}