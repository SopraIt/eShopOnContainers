using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Nosql.Model 
{
    [BsonIgnoreExtraElements]
    public class Stock{
        public long item_id { get; set; }
        public string sku { get; set; }
        public int qty { get; set; }
        public bool is_in_stock { get; set; }
    }
}
