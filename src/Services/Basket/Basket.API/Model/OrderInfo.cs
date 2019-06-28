using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class OrderInfo
    {
        public string backendOrderId { get; set; }
        public DateTime transferedAt { get; set; }
    }
}