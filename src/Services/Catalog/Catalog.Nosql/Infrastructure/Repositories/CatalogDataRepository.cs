using Catalog.Nosql.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;


namespace Catalog.Nosql.Infrastructure.Repositories
{
    public class CatalogDataRepository
    : ICatalogDataRepository
    {
        private readonly CatalogDataContext _context;

        public CatalogDataRepository(IOptions<CatalogNosqlSettings> settings){
            _context = new CatalogDataContext(settings);
        }

        public async Task<Product> GetAsync(string Id){
            var filter = Builders<Product>.Filter.Eq("Identifier", Id);
            return await _context.CatalogData
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task UpsertAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq("UserId", product.Identifier);
            var update = Builders<Product>.Update
                .Set("Enabled", product.Enabled)
                .CurrentDate("UpdateDate");

            await _context.CatalogData
                .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }

}
