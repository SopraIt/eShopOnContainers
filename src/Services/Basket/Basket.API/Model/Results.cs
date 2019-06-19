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
        public List<CartItem> Result { get; set; }
    }

    public partial class CartCreateResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }

    public partial class CartUpdateRequest
    {
        [JsonProperty("cartItem")]
        public CartItem CartItem { get; set; }
    }
    
    public partial class CartUpdateResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public CartItem Result { get; set; }
    }

}