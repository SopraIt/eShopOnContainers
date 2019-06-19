using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    [BsonIgnoreExtraElements]
    public partial class Cart
    {
        [JsonProperty("user_id")]
        public string user_id { get; set; }

        [JsonProperty("cart_items")]
        public List<CartItem> cart_items { get; set; }
    }
}