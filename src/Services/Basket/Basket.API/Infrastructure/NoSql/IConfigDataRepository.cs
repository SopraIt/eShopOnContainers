using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using Basket.API.Model;

namespace Basket.API.Infrastructure.NoSql
{
    public interface IConfigDataRepository{

        Task<List<PaymentMethod>> GetPaymentMethosAsync ();
        Task<List<ShippingMethod>> GetShippingMethosAsync ();
        Task<Tax> GetTax(string country_id);
    }
}