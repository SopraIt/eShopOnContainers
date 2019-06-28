using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{

    public partial class ConfigurableOption
    {
        public int id { get; set; }
        public string attribute_id { get; set; }
        public string label { get; set; }
        public int position { get; set; }
        public List<Value> values { get; set; }
        public int product_id { get; set; }
        public string attribute_code { get; set; }
    }
}