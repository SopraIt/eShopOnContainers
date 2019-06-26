using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using Basket.API.Model;

namespace Basket.API.Infrastructure.NoSql
{
    public class ConfigDataRepository : IConfigDataRepository
    {

        public async Task<List<PaymentMethod>> GetPaymentMethosAsync()
        {
            var payment_methods = new List<PaymentMethod>();
            payment_methods.Add(new PaymentMethod()
            {
                Code = "authorizenet_directpost",
                Title = "Credit Card Direct Post (Authorize.net)"
            });
            payment_methods.Add(new PaymentMethod()
            {
                Code = "cashondelivery",
                Title = "Cash On Delivery"
            });
            payment_methods.Add(new PaymentMethod()
            {
                Code = "free",
                Title = "No Payment Information Required"
            });

            return payment_methods;
        }
        public async Task<List<ShippingMethod>> GetShippingMethosAsync()
        {
            var shipping_methods = new List<ShippingMethod>();
            shipping_methods.Add(new ShippingMethod()
            {
                CarrierCode = "flatrate",
                MethodCode = "flatrate",
                CarrierTitle = "Flat Rate",
                MethodTitle = "Fixed",
                Amount = 30,
                BaseAmount = 30,
                Available = true,
                ErrorMessage = "",
                PriceExclTax = 30,
                PriceInclTax = 30
            });

            return shipping_methods;
        }
        public async Task<Tax> GetTax(string country_id)
        {
            if (country_id == "US")
            {
                var tax = new Tax()
                {
                    TaxId = 1,
                    TaxDescription = "VAT23",
                    TaxRate = 23
                };

                return tax;
            }
            else
            {
                var tax = new Tax()
                {
                    TaxId = 2,
                    TaxDescription = "VAT22",
                    TaxRate = 22
                };

                return tax;
            }

        }
    }
}