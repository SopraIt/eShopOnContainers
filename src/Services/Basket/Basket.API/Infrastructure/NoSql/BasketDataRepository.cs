using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopOnContainers.Services.Basket.API;
using Basket.API.Model;
using Newtonsoft.Json;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Microsoft.Extensions.Logging;

namespace Basket.API.Infrastructure.NoSql
{
    public class BasketDataRepository
    : IBasketDataRepository
    {
        private readonly BasketDataContext _context;
        private readonly ILogger<BasketDataRepository> _logger;

        public BasketDataRepository(
            IOptions<BasketSettings> settings,
            ILogger<BasketDataRepository> logger)
        {
            _context = new BasketDataContext(settings);
        }


        public async Task<Cart> GetCartAsync(string Id)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("user_id", Id);
            var db_result = await _context.BasketData
                                 .Find(filter)
                                 .FirstOrDefaultAsync();

            if (db_result != null)
            {
                Cart cart = BsonSerializer.Deserialize<Cart>(db_result.AsBsonDocument);
                return cart;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string Id)
        {
            Cart cart = await this.GetCartAsync(Id);

            if (cart != null)
            {
                return cart.cart_items;
            }
            else
            {
                return null;
            }
        }



        public async Task<string> UpsertAsync(string id, CartItem cart_item)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);

                Cart cart = await this.GetCartAsync(id);

                if (cart_item != null)
                {
                    if (cart.cart_items.Exists(x => x.ItemId == cart_item.ItemId))
                    {
                        cart.cart_items.RemoveAll(x => x.ItemId == cart_item.ItemId);
                    }
                    cart.cart_items.Add(cart_item);

                    var db_cart = await _context.BasketData
                                .ReplaceOneAsync(filter, cart.ToBsonDocument());

                    return db_cart.ModifiedCount > 0 ? id : string.Empty;
                }
                else
                {
                    if (cart == null)
                    {
                        BsonDocument cart_bson = new BsonDocument {
                            { "user_id", id },
                            { "cart_items", new BsonArray()}
                        };

                        await _context.BasketData.InsertOneAsync(cart_bson);
                    }

                    

                    return id;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Upsert of cart failed!");
                return string.Empty;
            }
        }
    }

}

