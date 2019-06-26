using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{
    [BsonIgnoreExtraElements]
    public partial class Category
    {
        [JsonProperty("category_id")]
        public long category_id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }
    }
}