using System;
using Catalog.Nosql.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;


namespace Catalog.Nosql.Infrastructure.Repositories
{
    public class CatalogDataRepository
    : ICatalogDataRepository
    {
        private readonly CatalogDataContext _context;

        public CatalogDataRepository(IOptions<CatalogNosqlSettings> settings)
        {
            _context = new CatalogDataContext(settings);
        }

        public async Task<Product> GetAsync(string Id)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("id", Id);
            var db_result = await _context.CatalogData
                                 .Find(filter)
                                 .FirstOrDefaultAsync();

            if (db_result != null)
            {
                Product result = new Product()
                {
                    Id = db_result["id"].ToString(),
                    json = db_result.ToJson()
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UpsertAsync(Product product)
        {
            try
            {
                Product _prod = await this.GetAsync(product.Id);
                BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(product.json);
                if (_prod != null)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("id", product.Id);
                    var result = await _context.CatalogData.ReplaceOneAsync(filter, document, new UpdateOptions { IsUpsert = true });
                    return result.ModifiedCount>0 ? product.Id : string.Empty;
                }
                else
                {
                    await _context.CatalogData.InsertOneAsync(document);
                    return product.Id;
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }

}
