using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartItemTotal
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("base_price")]
        public decimal BasePrice { get; set; }

        [JsonProperty("qty")]
        public long Qty { get; set; }

        [JsonProperty("row_total")]
        public decimal RowTotal { get; set; }

        [JsonProperty("base_row_total")]
        public decimal BaseRowTotal { get; set; }

        [JsonProperty("row_total_with_discount")]
        public decimal RowTotalWithDiscount { get; set; }

        [JsonProperty("tax_amount")]
        public decimal TaxAmount { get; set; }

        [JsonProperty("base_tax_amount")]
        public decimal BaseTaxAmount { get; set; }

        [JsonProperty("tax_percent")]
        public long TaxPercent { get; set; }

        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [JsonProperty("base_discount_amount")]
        public decimal BaseDiscountAmount { get; set; }

        [JsonProperty("discount_percent")]
        public long DiscountPercent { get; set; }

        [JsonProperty("price_incl_tax")]
        public decimal PriceInclTax { get; set; }

        [JsonProperty("base_price_incl_tax")]
        public decimal BasePriceInclTax { get; set; }

        [JsonProperty("row_total_incl_tax")]
        public decimal RowTotalInclTax { get; set; }

        [JsonProperty("base_row_total_incl_tax")]
        public decimal BaseRowTotalInclTax { get; set; }

        [JsonProperty("options")]
        public List<CartItemOption> Options { get; set; }

        [JsonProperty("weee_tax_applied_amount")]
        public object WeeeTaxAppliedAmount { get; set; }

        [JsonProperty("weee_tax_applied")]
        public object WeeeTaxApplied { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}