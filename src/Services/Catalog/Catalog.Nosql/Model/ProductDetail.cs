using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{
    [BsonIgnoreExtraElements]
    public partial class ProductDetail
    {
        [JsonProperty("sku")]
        public string sku { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("final_price")]
        public decimal final_price { get; set; }

        [JsonProperty("special_price")]
        public decimal? special_price { get; set; }

        [JsonProperty("regular_price")]
        public decimal regular_price { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("stock")]
        public Stock stock { get; set; }

        [JsonProperty("category")]
        public List<Category> category { get; set; }

        [JsonProperty("url_path")]
        public string url_path { get; set; }
    }
}