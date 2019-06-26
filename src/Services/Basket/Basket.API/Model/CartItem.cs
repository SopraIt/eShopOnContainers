using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class CartItem
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("qty")]
        public long Qty { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("final_price")]
        public decimal final_price { get; set; }

        [JsonProperty("special_price")]
        public decimal? special_price { get; set; }

        [JsonProperty("regular_price")]
        public decimal regular_price { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("quote_id")]
        public string QuoteId { get; set; }

        [JsonProperty("totals")]
        public CartItemTotal Totals { get; set; }

        [JsonProperty("stock")]
        public CartItemStock Stock { get; set; }
    }
}