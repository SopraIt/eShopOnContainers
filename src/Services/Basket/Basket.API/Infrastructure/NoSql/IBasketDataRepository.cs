using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using Basket.API.Model;

namespace Basket.API.Infrastructure.NoSql
{
    

    public interface IBasketDataRepository
    {
        Task<Cart> GetCartAsync (string Id);
        Task<string> UpsertCartAsync (string user_id);
        Task<long> DeleteCartAsync (string user_id);

        Task<List<CartItem>> GetCartItemsAsync (string Id);
        Task<string> UpsertCartItemAsync (string Id ,CartItem cart_item);
        Task<string> DeleteCartItemAsync (string Id ,CartItem cart_item);
        Task<string> UpsertCartTotalAsync (string Id ,Total total);
        Task<string> InsertOrderAsync(Cart cart);
    }
}