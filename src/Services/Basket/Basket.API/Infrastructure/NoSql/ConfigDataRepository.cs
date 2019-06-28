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
                code = "authorizenet_directpost",
                title = "Credit Card Direct Post (Authorize.net)"
            });
            payment_methods.Add(new PaymentMethod()
            {
                code = "cashondelivery",
                title = "Cash On Delivery"
            });
            payment_methods.Add(new PaymentMethod()
            {
                code = "free",
                title = "No Payment Information Required"
            });

            return payment_methods;
        }
        public async Task<List<ShippingMethod>> GetShippingMethosAsync()
        {
            var shipping_methods = new List<ShippingMethod>();
            shipping_methods.Add(new ShippingMethod()
            {
                carrier_code = "flatrate",
                method_code = "flatrate",
                carrier_title = "Flat Rate",
                method_title = "Fixed",
                amount = 30,
                base_amount = 30,
                available = true,
                error_message = "",
                price_excl_tax = 30,
                price_incl_tax = 30
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