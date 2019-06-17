using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public List<Cart> Result { get; set; }
    }

    public partial class CartCreateResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
    
    public partial class CartUpdateResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }

}