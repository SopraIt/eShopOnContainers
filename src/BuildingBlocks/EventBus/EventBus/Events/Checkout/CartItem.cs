using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events.Checkout
{

    public partial class CartItem
    {
        public long item_id { get; set; }
        public long qty { get; set; }
        public string quote_id { get; set; }
        public string sku { get; set; }
        public string ean { get; set; }
        public string name { get; set; }
        public string url_path { get; set; }
        public long tsk { get; set; }
        public CartItemTotal totals { get; set; }
    }
}