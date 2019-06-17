using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopOnContainers.Services.Basket.API;

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

        // public async Task<Product> GetAsync(string Id)
        // {

        //     var filter = Builders<BsonDocument>.Filter.Eq("id", Id);
        //     var db_result = await _context.CatalogData
        //                          .Find(filter)
        //                          .FirstOrDefaultAsync();

        //     if (db_result != null)
        //     {
        //         Product result = new Product()
        //         {
        //             Id = db_result["id"].ToString(),
        //             json = db_result.ToJson()
        //         };

        //         return result;
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }

        // public async Task<Product> GetBySkuAsync(string Sku)
        // {
        //     var db_result = await this.GetBySkuAsyncFromDB(Sku);

        //     if (db_result != null)
        //     {
        //         Product result = new Product()
        //         {
        //             Id = db_result["id"].ToString(),
        //             json = db_result.ToJson()
        //         };

        //         return result;
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }

        // public async Task<string> UpsertAsync(Product product)
        // {
        //     try
        //     {
        //         Product _prod = await this.GetAsync(product.Id);
        //         BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(product.json);
        //         if (_prod != null)
        //         {
        //             var filter = Builders<BsonDocument>.Filter.Eq("id", product.Id);
        //             var result = await _context.CatalogData.ReplaceOneAsync(filter, document, new UpdateOptions { IsUpsert = true });
        //             return result.ModifiedCount>0 ? product.Id : string.Empty;
        //         }
        //         else
        //         {
        //             await _context.CatalogData.InsertOneAsync(document);
        //             return product.Id;
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         return string.Empty;
        //     }
        // }
    
        // public async Task<bool> CheckStock(string Sku){
        //     try {
        //         var db_result = await this.GetBySkuAsyncFromDB(Sku);

        //         var is_in_stock = bool.Parse(db_result["stock"]["is_in_stock"].ToString());

        //         if (is_in_stock){
        //             return true;
        //         }
        //         else
        //         {
        //             return false;
        //         }
        //     } catch (Exception e){
        //         return false;
        //     }
        // }
        // public async Task<List<Stock>> CheckStockList(List<string> skus){
        //     try {

        //         return new List<Stock>();
        //     } catch (Exception e){
        //         return new List<Stock>();
        //     }
        // }



        // private async Task<BsonDocument> GetBySkuAsyncFromDB(string Sku){
            
        //     var filter = Builders<BsonDocument>.Filter.Eq("sku", Sku);
        //     var projection = Builders<BsonDocument>.Projection.Exclude("_id");
        //     projection = projection.Exclude("tsk");

        //     var db_result = await  _context.CatalogData
        //                          .Find(filter)
        //                          .Project(projection)
        //                          .FirstOrDefaultAsync();
        //     return db_result;
        // }
    }

}
