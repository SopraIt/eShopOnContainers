using System;
using System.Collections.Generic;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartEvent: IntegrationEvent
    {

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