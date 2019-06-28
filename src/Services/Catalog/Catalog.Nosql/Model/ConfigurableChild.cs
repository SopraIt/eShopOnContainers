using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{

    public partial class ConfigurableChild
    {
        public string sku { get; set; }
        public int id { get; set; }
        public int status { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        //public List<object> tier_prices { get; set; }
        public string required_options { get; set; }
        public string has_options { get; set; }
        public string tax_class_id { get; set; }
        //public List<string> category_ids { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string image { get; set; }
        public string small_image { get; set; }
        public string thumbnail { get; set; }
        public string url_key { get; set; }
        public string msrp_display_actual_price_type { get; set; }
        public double? final_price { get; set; }
        public double? max_price { get; set; }
        public double? max_regular_price { get; set; }
        public double? minimal_regular_price { get; set; }
        public object special_price { get; set; }
        public double? minimal_price { get; set; }
        public double? regular_price { get; set; }
    }
}