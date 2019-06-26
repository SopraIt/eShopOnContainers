using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class Total
    {
        public decimal GrandTotal { get; set; }
        public decimal BaseGrandTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal BaseSubtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal BaseDiscountAmount { get; set; }
        public decimal SubtotalWithDiscount { get; set; }
        public decimal BaseSubtotalWithDiscount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal BaseShippingAmount { get; set; }
        public decimal ShippingDiscountAmount { get; set; }
        public decimal BaseShippingDiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal BaseTaxAmount { get; set; }
        public decimal WeeeTaxAppliedAmount { get; set; }
        public decimal ShippingTaxAmount { get; set; }
        public decimal BaseShippingTaxAmount { get; set; }
        public decimal SubtotalInclTax { get; set; }
        public decimal ShippingInclTax { get; set; }
        public decimal BaseShippingInclTax { get; set; }
        public string BaseCurrencyCode { get; set; }
        public string QuoteCurrencyCode { get; set; }
        public long ItemsQty { get; set; }
        public List<CartItem> Items { get; set; }
        public List<TotalSegment> TotalSegments { get; set; }
    }
}