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

    public partial class CartDeleteResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }
    }

    public partial class PaymentMethodResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public List<PaymentMethod> Result { get; set; }
    }

    public partial class ShippingMethodResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public List<ShippingMethod> Result { get; set; }
    }

    public partial class ShippingInformationResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public ShippingInformation Result { get; set; }
    }

    public partial class OrderResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public OrderInfo Result { get; set; }
    }

}