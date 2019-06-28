namespace Catalog.Nosql.Infrastructure.Repositories
{
    using Catalog.Nosql.Model;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using System.Collections.Generic;

    public interface ICatalogDataRepository
    {
        
        Task<ProductDetail> GetProductDetailAsync (string Id);
        //Task<Product> GetAsync (string Id);
        
        Task<ProductDetail> GetProductDetailBySkuAsync(string Sku);
        //Task<Product> GetBySkuAsync(string Sku);
        
        Task<string> UpsertAsync(ProductDetail product);
        //Task<string> UpsertAsync(Product product);

        Task<bool> CheckStock(string sku);
        Task<List<Stock>> CheckStockList(List<string> skus);
    }
}