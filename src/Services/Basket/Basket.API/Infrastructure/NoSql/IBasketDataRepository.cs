using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using Basket.API.Model;

namespace Basket.API.Infrastructure.NoSql
{
    

    public interface IBasketDataRepository
    {
        Task<List<CartItem>> GetAsync (string Id);
        Task<string> UpsertAsync (string Id ,CartItem cart);
    }
}