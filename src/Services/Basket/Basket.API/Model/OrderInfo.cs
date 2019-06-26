using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class OrderInfo
    {
        [JsonProperty("backendOrderId")]
        public string backendOrderId { get; set; }

        [JsonProperty("transferedAt")]
        public DateTime transferedAt { get; set; }
    }
}