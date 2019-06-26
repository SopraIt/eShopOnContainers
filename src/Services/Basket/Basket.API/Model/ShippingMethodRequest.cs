using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class ShippingMethodRequest
    {
        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    public partial class ShippingInformationRequest
    {
        [JsonProperty("addressInformation")]
        public AddressInformation AddressInformation { get; set; }
    }
}