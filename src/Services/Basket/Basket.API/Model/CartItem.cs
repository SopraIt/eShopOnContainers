using System;
using System.Collections.Generic;
using Catalog.Nosql.Model;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class CartItem : ProductDetail
    {
        public long item_id { get; set; }
        public long qty { get; set; }
        public string quote_id { get; set; }
        public CartItemTotal totals { get; set; }
    }
}