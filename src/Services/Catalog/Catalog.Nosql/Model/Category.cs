using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{
    [BsonIgnoreExtraElements]
    public partial class Category
    {
        public int category_id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string path { get; set; }
    }
}