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
                return cart.products;
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
                        { "products", new BsonArray()},
                        { "address_information", new AddressInformation().ToBsonDocument() },
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

        public async Task<long> DeleteCartAsync (string user_id){
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("user_id", user_id);
                var delete = await _context.BasketData.DeleteOneAsync(filter);

                return delete.DeletedCount;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete of cart failed!");
                return 0;
            }
        }

        public async Task<string> UpsertCartItemAsync(string id, CartItem cart_item)
        {
            try
            {
                Cart cart = await this.GetCartAsync(id);

                if (cart_item != null)
                {
                    if (cart.products.Exists(x => x.item_id == cart_item.item_id))
                    {
                        cart.products.RemoveAll(x => x.item_id == cart_item.item_id);
                    }
                    cart.products.Add(cart_item);

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

        public async Task<string> DeleteCartItemAsync (string id ,CartItem cart_item){
            try
            {
                Cart cart = await this.GetCartAsync(id);

                if (cart_item != null)
                {
                    if (cart.products.Exists(x => x.item_id == cart_item.item_id))
                    {
                        cart.products.RemoveAll(x => x.item_id == cart_item.item_id);
                    }

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
    
        public async Task<string> InsertOrderAsync(Cart cart){
            try
            {
                await _context.OrderData.InsertOneAsync(cart.ToBsonDocument());

                return cart.cart_id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Insert of cart hystory failed!");
                return string.Empty;
            }
        }
    }

}

