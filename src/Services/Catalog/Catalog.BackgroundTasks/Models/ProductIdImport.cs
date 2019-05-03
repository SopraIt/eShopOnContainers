using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.BackgroundTasks.Models
{
    public class ProductIdImport
    {
        public string ID { get; set; }
        public bool Imported { get; set; }
    }
}
