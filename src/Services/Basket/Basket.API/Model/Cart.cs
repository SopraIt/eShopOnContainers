using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    [BsonIgnoreExtraElements]
    public partial class Cart
    {
        public string user_id { get; set; }
        public string cart_id { get; set; }
        public List<CartItem> products { get; set; }
        public AddressInformation address_information { get; set; }
        public Total total { get; set; }
    }
}