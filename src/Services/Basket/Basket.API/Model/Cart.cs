using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    [BsonIgnoreExtraElements]
    public partial class Cart
    {
        // [JsonProperty("user_id")]
        // public string user_id { get; set; }

        // [JsonProperty("cart_items")]
        // public List<CartItem> cart_items { get; set; }

        [JsonProperty("user_id")]
        public string user_id { get; set; }

        [JsonProperty("cart_id")]
        public string cart_id { get; set; }

        [JsonProperty("products")]
        public List<CartItem> Products { get; set; }

        [JsonProperty("addressInformation")]
        public AddressInformation AddressInformation { get; set; }

        public Total total { get; set; }
    }
}