namespace Catalog.Nosql.Infrastructure.Repositories
{
    using Catalog.Nosql.Models;
    using System.Threading.Tasks;

    public interface ICatalogDataRepository
    {
        Task<Product> GetAsync(string Id);
        Task UpsertAsync(Product product);
    }
}