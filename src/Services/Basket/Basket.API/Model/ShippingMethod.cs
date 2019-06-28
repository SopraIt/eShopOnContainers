using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class ShippingMethod
    {
        public string carrier_code { get; set; }
        public string method_code { get; set; }
        public string carrier_title { get; set; }
        public string method_title { get; set; }
        public decimal amount { get; set; }
        public decimal base_amount { get; set; }
        public bool available { get; set; }
        public string error_message { get; set; }
        public decimal price_excl_tax { get; set; }
        public decimal price_incl_tax { get; set; }
    }
}