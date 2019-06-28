using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartItemOption
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}
