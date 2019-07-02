using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events.Checkout
{
    public class CheckoutIntegrationEvent : IntegrationEvent
    {
        public string user_id { get; set; }
        public string cart_id { get; set; }
        public List<CartItem> products { get; set; }
        public AddressInformation address_information { get; set; }
        public Total total { get; set; }
    }
}
