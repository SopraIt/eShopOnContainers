using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class CartItemTotal
    {
        public long item_id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal base_price { get; set; }
        public long qty { get; set; }
        public decimal row_total { get; set; }
        public decimal base_row_total { get; set; }
        public decimal row_total_with_discount { get; set; }
        public decimal tax_amount { get; set; }
        public decimal base_tax_amount { get; set; }
        public long tax_percent { get; set; }
        public decimal discount_amount { get; set; }
        public decimal base_discount_amount { get; set; }
        public long discount_percent { get; set; }
        public decimal price_incl_tax { get; set; }
        public decimal base_price_incl_tax { get; set; }
        public decimal row_total_incl_tax { get; set; }
        public decimal BaseRowTotalInclTax { get; set; }
        public List<CartItemOption> options { get; set; }
        public object weee_tax_applied_amount { get; set; }
        public object weee_tax_applied { get; set; }
    }
}