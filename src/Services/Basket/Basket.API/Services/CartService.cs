using Basket.API.Model;
using Catalog.Nosql.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Linq;
using Basket.API.Infrastructure.NoSql;
using System.Collections.Generic;

namespace Microsoft.eShopOnContainers.Services.Basket.API.Services
{
    
    public class CartService: ICartService
    {
        private readonly ICatalogDataRepository _repo_catalog;
        private readonly IConfigDataRepository _repo_config;

        public CartService(
            ICatalogDataRepository repo_catalog,
            IConfigDataRepository repo_config){
            _repo_catalog = repo_catalog;
            _repo_config = repo_config;
        }
        public async Task<CartItem> CalculateCartItem(CartItem cart_item){
            var product = await _repo_catalog.GetProductDetailBySkuAsync(cart_item.sku);
            if (product != null)
            {
                var result = JsonConvert.DeserializeObject<CartItem>(JsonConvert.SerializeObject(product));
                
                result.item_id = cart_item.item_id;
                result.qty = cart_item.qty;
                result.quote_id = cart_item.quote_id;

                result.price = product.special_price == null ? product.final_price : product.special_price.Value;

                result.totals = new CartItemTotal(){
                    price = product.regular_price,
                    price_incl_tax = product.final_price,
                    row_total = product.regular_price * cart_item.qty,
                    row_total_incl_tax = cart_item.price * cart_item.qty,
                    row_total_with_discount = product.final_price * cart_item.qty,
                };
                
                return result;
            }
            else 
            {
                return null;
            }
        }
        public async Task<Cart> CalculateCartTotal(Cart cart, AddressInformation shipping_info){
            
            var shipping = (await _repo_config.GetShippingMethosAsync())
                .FirstOrDefault(x => x.carrier_code == shipping_info.shippingCarrierCode);

            var base_sub_total = cart.products.Sum (x => x.totals.row_total);
            var base_sub_total_with_discount = cart.products.Sum (x => x.totals.row_total_with_discount);

            var base_shipping_amount = shipping.base_amount;
            var base_shipping_discount_amount = 0;

            var grand_total = base_sub_total_with_discount + base_shipping_discount_amount;

            var base_tax_amount = cart.products.Sum (x => x.totals.row_total_incl_tax);
            var shipping_tax_amount = 0;

            var base_grand_total = grand_total + base_tax_amount + shipping_tax_amount;
            var shipping_amount = base_shipping_amount + base_shipping_discount_amount + shipping_tax_amount;

            Total total = new Total(){
                base_subtotal = base_sub_total,
                base_subtotal_with_discount = base_sub_total_with_discount,
                base_shipping_amount = base_shipping_amount,
                base_shipping_discount_amount = base_shipping_discount_amount,
                grand_total = grand_total,
                base_tax_amount = base_tax_amount,
                shipping_tax_amount = shipping_tax_amount,
                base_grand_total = base_grand_total
            };

            total.total_segments = new List<TotalSegment>();
            total.total_segments.Add(new TotalSegment(){
                code = "subtotal",
                title = "Subtotal",
                value = base_sub_total_with_discount
            });
            total.total_segments.Add(new TotalSegment(){
                code = "shipping",
                title = "Shipping",
                value = shipping_amount
            });
            total.total_segments.Add(new TotalSegment(){
                code = "tax",
                title = "Tax",
                value = base_tax_amount
            });
            total.total_segments.Add(new TotalSegment(){
                code = "grand_total",
                title = "Grand Total",
                value = base_sub_total_with_discount + shipping_amount + base_tax_amount
            });

            cart.total = total;

            return cart;
        }

    }
}