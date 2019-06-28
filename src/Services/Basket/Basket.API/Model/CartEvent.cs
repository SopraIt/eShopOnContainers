using System;
using System.Collections.Generic;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartEvent: IntegrationEvent
    {
        public string user_id { get; set; }
        public string cart_id { get; set; }
        public List<CartItem> products { get; set; }
        public AddressInformation address_information { get; set; }
        public Total total { get; set; }
    }
}