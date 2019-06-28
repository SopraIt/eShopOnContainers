using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class ShippingInformation
    {
        public List<PaymentMethod> payment_methods { get; set; }
        public Total totals { get; set; }
    }
}