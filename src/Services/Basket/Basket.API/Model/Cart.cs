using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class Cart
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