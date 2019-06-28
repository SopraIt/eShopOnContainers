using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class PaymentMethod
    {
        public string code { get; set; }
        public string title { get; set; }
    }
}