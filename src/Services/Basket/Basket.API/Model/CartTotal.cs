using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class Total
    {
        public decimal grand_total { get; set; }
        public decimal base_grand_total { get; set; }
        public decimal subtotal { get; set; }
        public decimal base_subtotal { get; set; }
        public decimal discount_amount { get; set; }
        public decimal base_discount_amount { get; set; }
        public decimal subtotal_with_discount { get; set; }
        public decimal base_subtotal_with_discount { get; set; }
        public decimal shipping_amount { get; set; }
        public decimal base_shipping_amount { get; set; }
        public decimal shipping_discount_amount { get; set; }
        public decimal base_shipping_discount_amount { get; set; }
        public decimal tax_amount { get; set; }
        public decimal base_tax_amount { get; set; }
        //public object weee_tax_applied_amount { get; set; }
        public decimal shipping_tax_amount { get; set; }
        public decimal base_shipping_tax_amount { get; set; }
        public decimal subtotal_incl_tax { get; set; }
        public decimal shipping_incl_tax { get; set; }
        public decimal base_shipping_incl_tax { get; set; }
        public string base_currency_code { get; set; }
        public string quote_currency_code { get; set; }
        public int items_qty { get; set; }
        public List<CartItem> items { get; set; }
        public List<TotalSegment> total_segments { get; set; }
    }
}