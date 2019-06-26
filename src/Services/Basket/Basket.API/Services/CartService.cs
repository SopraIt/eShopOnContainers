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
            var product = await _repo_catalog.GetProductDetailBySkuAsync(cart_item.Sku);
            if (product != null)
            {
                cart_item.Name = product.name;
                cart_item.regular_price = product.regular_price;
                cart_item.final_price = product.final_price;
                cart_item.special_price = product.special_price;
                cart_item.Price = product.special_price == null? product.final_price : product.special_price.Value;
                
                cart_item.Stock = new CartItemStock(){
                    IsInStock = product.stock.is_in_stock
                };

                cart_item.Totals = new CartItemTotal(){
                    Price = product.regular_price,
                    PriceInclTax = product.final_price,
                    RowTotal = product.regular_price * cart_item.Qty,
                    RowTotalInclTax = cart_item.Price * cart_item.Qty,
                    RowTotalWithDiscount = product.final_price * cart_item.Qty,
                };
            }
            return cart_item;
        }
        public async Task<Cart> CalculateCartTotal(Cart cart, AddressInformation shipping_info){
            
            var shipping = (await _repo_config.GetShippingMethosAsync())
                .FirstOrDefault(x => x.CarrierCode == shipping_info.shippingCarrierCode);

            var base_sub_total = cart.Products.Sum (x => x.Totals.RowTotal);
            var base_sub_total_with_discount = cart.Products.Sum (x => x.Totals.RowTotalWithDiscount);

            var base_shipping_amount = shipping.BaseAmount;
            var base_shipping_discount_amount = 0;

            var grand_total = base_sub_total_with_discount + base_shipping_discount_amount;

            var base_tax_amount = cart.Products.Sum (x => x.Totals.RowTotalInclTax);
            var shipping_tax_amount = 0;

            var base_grand_total = grand_total + base_tax_amount + shipping_tax_amount;
            var shipping_amount = base_shipping_amount + base_shipping_discount_amount + shipping_tax_amount;

            Total total = new Total(){
                BaseSubtotal = base_sub_total,
                BaseSubtotalWithDiscount = base_sub_total_with_discount,
                BaseShippingAmount = base_shipping_amount,
                BaseShippingDiscountAmount = base_shipping_discount_amount,
                GrandTotal = grand_total,
                BaseTaxAmount = base_tax_amount,
                ShippingAmount = shipping_tax_amount,
                BaseGrandTotal = base_grand_total
            };

            total.TotalSegments = new List<TotalSegment>();
            total.TotalSegments.Add(new TotalSegment(){
                Code = "subtotal",
                Title = "Subtotal",
                Value = base_sub_total_with_discount
            });
            total.TotalSegments.Add(new TotalSegment(){
                Code = "shipping",
                Title = "Shipping",
                Value = shipping_amount
            });
            total.TotalSegments.Add(new TotalSegment(){
                Code = "tax",
                Title = "Tax",
                Value = base_tax_amount
            });
            total.TotalSegments.Add(new TotalSegment(){
                Code = "grand_total",
                Title = "Grand Total",
                Value = base_sub_total_with_discount + shipping_amount + base_tax_amount
            });

            cart.total = total;

            return cart;
        }

    }
}