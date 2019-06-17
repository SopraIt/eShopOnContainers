using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Basket.API.Infrastructure.NoSql
{
    

    public interface IBasketDataRepository
    {
        // Task<Product> GetAsync (string Id);
        // Task<Product> GetBySkuAsync(string Sku);
        // Task<string> UpsertAsync(Product product);

        // Task<bool> CheckStock(string sku);
        // Task<List<Stock>> CheckStockList(List<string> skus);
    }
}