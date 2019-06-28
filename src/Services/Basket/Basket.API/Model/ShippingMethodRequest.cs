using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class ShippingMethodRequest
    {
        public Address address { get; set; }
    }

    public partial class ShippingInformationRequest
    {
        public AddressInformation addressInformation { get; set; }
    }
}