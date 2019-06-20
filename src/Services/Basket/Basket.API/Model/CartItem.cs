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
        public long Price { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("quote_id")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public long QuoteId { get; set; }

        [JsonProperty("product_option", NullValueHandling = NullValueHandling.Ignore)]
        public ProductOption ProductOption { get; set; }



        [JsonProperty("base_price")]
        public double? BasePrice { get; set; }

        [JsonProperty("row_total")]
        public double? RowTotal { get; set; }

        [JsonProperty("base_row_total")]
        public double? BaseRowTotal { get; set; }

        [JsonProperty("row_total_with_discount")]
        public double? RowTotalWithDiscount { get; set; }

        [JsonProperty("tax_amount")]
        public double? TaxAmount { get; set; }

        [JsonProperty("base_tax_amount")]
        public double? BaseTaxAmount { get; set; }

        [JsonProperty("tax_percent")]
        public double? TaxPercent { get; set; }

        [JsonProperty("discount_amount")]
        public double? DiscountAmount { get; set; }

        [JsonProperty("base_discount_amount")]
        public double? BaseDiscountAmount { get; set; }

        [JsonProperty("discount_percent")]
        public double? DiscountPercent { get; set; }

        [JsonProperty("price_incl_tax")]
        public double? PriceInclTax { get; set; }

        [JsonProperty("base_price_incl_tax")]
        public double? BasePriceInclTax { get; set; }

        [JsonProperty("row_total_incl_tax")]
        public double? RowTotalInclTax { get; set; }

        [JsonProperty("base_row_total_incl_tax")]
        public double? BaseRowTotalInclTax { get; set; }

        [JsonProperty("options")]
        public string Options { get; set; }

        [JsonProperty("weee_tax_applied_amount")]
        public double? WeeeTaxAppliedAmount { get; set; }

        [JsonProperty("weee_tax_applied")]
        public double? WeeeTaxApplied { get; set; }
    }

    public partial class ProductOption
    {
        [JsonProperty("extension_attributes")]
        public ExtensionAttributes ExtensionAttributes { get; set; }
    }

    public partial class ExtensionAttributes
    {
        [JsonProperty("configurable_item_options")]
        public List<ConfigurableItemOption> ConfigurableItemOptions { get; set; }
    }

    public partial class ConfigurableItemOption
    {
        [JsonProperty("option_id")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public long OptionId { get; set; }

        [JsonProperty("option_value")]
        public long OptionValue { get; set; }
    }
}