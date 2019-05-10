namespace Catalog.Nosql.Infrastructure
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using MongoDB.Bson;

    public class CatalogDataContext
    {
        private readonly IMongoDatabase _database = null;

        public CatalogDataContext(IOptions<CatalogNosqlSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoConnectionString);

            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.MongoDatabase);
            }
        }

        public IMongoCollection<BsonDocument> CatalogData
        {
            get
            {
                return _database.GetCollection<BsonDocument>("CatalogDataModel");
            }
        }        
    }
}