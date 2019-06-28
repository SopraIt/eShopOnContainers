using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{
    public partial class Value
    {
        public int value_index { get; set; }
        public string label { get; set; }
    }
}