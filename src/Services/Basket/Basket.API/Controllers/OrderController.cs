using Basket.API.Infrastructure.NoSql;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.Services.Basket.API.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events.Checkout;

namespace Microsoft.eShopOnContainers.Services.Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderController> _logger;
        private readonly IBasketDataRepository _repo;

        public OrderController(
            ILogger<OrderController> logger,
            IEventBus eventBus,
            IBasketDataRepository repository)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CartResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResult>> SaveOrder(Cart cart)
        {
            //var UserId = User.FindFirst("sub")?.Value;
            var _cart = await _repo.GetCartAsync(cart.cart_id);
            var id = _repo.InsertOrderAsync(_cart);
        
            string json = JsonConvert.SerializeObject(_cart);
            var cart_event = JsonConvert.DeserializeObject<CheckoutIntegrationEvent>(json);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", cart_event.Id, Program.AppName, cart_event);

                _eventBus.Publish(cart_event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", cart_event.Id, Program.AppName);

                throw;
            }

            var delete_count = _repo.DeleteCartAsync(cart.cart_id);

            return new OrderResult(){
                Result = new OrderInfo(){ 
                    backendOrderId = cart_event.Id.ToString(),
                    transferedAt = DateTime.Now
                },
                Code = 200
            };
        }
    }
}