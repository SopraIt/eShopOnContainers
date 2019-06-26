using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.eShopOnContainers.Services.Basket.API;

namespace Basket.API.Infrastructure.NoSql
{


    public class BasketDataContext
    {
        private readonly IMongoDatabase _database = null;

        public BasketDataContext(IOptions<BasketSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoConnectionString);

            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.CartMongoDatabase);
            }
        }

        public IMongoCollection<BsonDocument> BasketData
        {
            get
            {
                return _database.GetCollection<BsonDocument>("BasketDataModel");
            }
        }   

        public IMongoCollection<BsonDocument> OrderData
        {
            get
            {
                return _database.GetCollection<BsonDocument>("OrderDataModel");
            }
        }       
    }
}