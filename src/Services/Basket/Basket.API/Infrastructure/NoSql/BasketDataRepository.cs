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
                return cart.Products;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UpsertCartAsync(string id)
        {
            try
            {
                Cart _cart = await this.GetCartAsync(id);

                if (_cart == null)
                {
                    BsonDocument cart_bson = new BsonDocument {
                        { "user_id", id },
                        { "cart_id", id },
                        { "Products", new BsonArray()},
                        { "AddressInformation", new AddressInformation().ToBsonDocument() },
                        { "total", new Total().ToBsonDocument() }
                    };

                    await _context.BasketData.InsertOneAsync(cart_bson);
                }
                return id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Upsert of cart failed!");
                return string.Empty;
            }
        }

        public async Task<string> UpsertCartItemAsync(string id, CartItem cart_item)
        {
            try
            {
                Cart cart = await this.GetCartAsync(id);

                if (cart_item != null)
                {
                    if (cart.Products.Exists(x => x.ItemId == cart_item.ItemId))
                    {
                        cart.Products.RemoveAll(x => x.ItemId == cart_item.ItemId);
                    }
                    cart.Products.Add(cart_item);

                    var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);

                    var db_cart = await _context.BasketData
                                .ReplaceOneAsync(filter, cart.ToBsonDocument());

                    return db_cart.ModifiedCount > 0 ? id : string.Empty;
                }
                else
                {
                    return await UpsertCartAsync(id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Upsert of cart failed!");
                return string.Empty;
            }
        }

        public async Task<string> UpsertCartTotalAsync(string id, Total total)
        {
            try
            {
                Cart cart = await this.GetCartAsync(id);

                cart.total = total;

                var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
                var db_cart = await _context.BasketData
                            .ReplaceOneAsync(filter, cart.ToBsonDocument());

                return db_cart.ModifiedCount > 0 ? id : string.Empty;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Upsert of cart failed!");
                return string.Empty;
            }
        }
    }

}

