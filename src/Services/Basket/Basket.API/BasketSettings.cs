namespace Microsoft.eShopOnContainers.Services.Basket.API
{
    public class BasketSettings
    {
        public string ConnectionString { get; set; }
        public string MongoConnectionString { get; set; }
        public string CartMongoDatabase { get; set; }
    }
}
