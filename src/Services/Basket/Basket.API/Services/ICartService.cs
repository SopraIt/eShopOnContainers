using Basket.API.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Basket.API.Services
{
    public interface ICartService
    {
        Task<CartItem> CalculateCartItem(CartItem cart_item);
        Task<Cart> CalculateCartTotal(Cart cart, AddressInformation shipping);

    }
}