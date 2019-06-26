using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class ShippingInformation
    {
        public List<PaymentMethod> PaymentMethods { get; set; }
        public Total Totals { get; set; }
    }
}