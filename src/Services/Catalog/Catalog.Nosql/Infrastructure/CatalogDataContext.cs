namespace Catalog.Nosql.Infrastructure
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Catalog.Nosql.Models;
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

        public IMongoCollection<Product> CatalogData
        {
            get
            {
                return _database.GetCollection<Product>("CatalogDataModel");
            }
        }        
    }
}