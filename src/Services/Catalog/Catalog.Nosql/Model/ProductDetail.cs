using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{
    [BsonIgnoreExtraElements]
    public partial class ProductDetail
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }
        //public int attribute_set_id { get; set; }
        public int status { get; set; }
        //public int visibility { get; set; }
        public string type_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        // public List<object> product_links { get; set; }
        // public List<object> tier_prices { get; set; }
        // public object custom_attributes { get; set; }
        // public double max_price { get; set; }
        // public double max_regular_price { get; set; }
        // public double minimal_regular_price { get; set; }
        // public double minimal_price { get; set; }
        public decimal price { get; set; }
        public decimal regular_price { get; set; }
        public decimal final_price { get; set; }
        public decimal? special_price { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string small_image { get; set; }
        public string thumbnail { get; set; }
        //public List<int> category_ids { get; set; }
        // public string options_container { get; set; }
        // public string required_options { get; set; }
        // public string has_options { get; set; }
        public string url_key { get; set; }
        public string slug { get; set; }
        // public string msrp_display_actual_price_type { get; set; }
        // public string tax_class_id { get; set; }
        // public List<int> material { get; set; }
        // public string eco_collection { get; set; }
        // public string performance_fabric { get; set; }
        // public string erin_recommends { get; set; }
        //public string @new { get; set; }
        public string sale { get; set; }
        // public string style_general { get; set; }
        // public string pattern { get; set; }
        // public List<int> climate { get; set; }
        //public Links links { get; set; }
        public Stock stock { get; set; }
        public List<MediaGallery> media_gallery { get; set; }
        //public List<ConfigurableChild> configurable_children { get; set; }
        public List<ConfigurableOption> configurable_options { get; set; }
        //public List<int> color_options { get; set; }
        //public List<int> size_options { get; set; }
        public List<Category> category { get; set; }
        public string url_path { get; set; }
        public long tsk { get; set; }
    }
}