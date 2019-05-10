namespace Catalog.Nosql.Infrastructure.Repositories
{
    using Catalog.Nosql.Model;
    using System.Threading.Tasks;
    using MongoDB.Bson;

    public interface ICatalogDataRepository
    {
        Task<Product> GetAsync(string Id);
        Task<string> UpsertAsync(Product product);
    }
}