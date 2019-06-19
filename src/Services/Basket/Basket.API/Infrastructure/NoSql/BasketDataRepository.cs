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

namespace Basket.API.Infrastructure.NoSql
{
    public class BasketDataRepository
    : IBasketDataRepository
    {
        private readonly BasketDataContext _context;

        public BasketDataRepository(IOptions<BasketSettings> settings)
        {
            _context = new BasketDataContext(settings);
        }

        public async Task<List<CartItem>> GetAsync(string Id)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("id", Id);
            var db_result = await _context.BasketData
                                 .Find(filter)
                                 .FirstOrDefaultAsync();

            if (db_result != null)
            {
                var json_cart = db_result["cart_items"].AsBsonArray.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict });
                
                List<CartItem> cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartItem>>(json_cart);
                return cart;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UpsertAsync(string id, CartItem cart)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("id", id);

                if (cart != null)
                {
                    var db_result = await _context.BasketData
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
                    
                    var filter_item = Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Eq("id", id),
                        Builders<BsonDocument>.Filter.Eq("cart_items.ItemId", cart.ItemId)
                    );
                    var db_cart_item = await _context.BasketData
                                 .Find(filter_item)
                                 .FirstOrDefaultAsync();

                    if(db_cart_item != null){
                        db_result["cart_items"].AsBsonArray.Remove(db_cart_item);
                    }
                    db_result["cart_items"].AsBsonArray.Add(cart.ToBsonDocument());

                        var result = await _context.BasketData.ReplaceOneAsync(filter, db_result, new UpdateOptions { IsUpsert = true });
                        return result.ModifiedCount > 0 ? id : string.Empty;
                }
                else
                {
                    await _context.BasketData.DeleteOneAsync(filter);

                    BsonDocument cart_bson = new BsonDocument {
                        { "id", id },
                        { "user_id", id },
                        { "cart_items", new BsonArray()}
                    };
                    
                    await _context.BasketData.InsertOneAsync(cart_bson);
                    return id;
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }

}
