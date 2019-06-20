using Basket.API.Infrastructure.NoSql;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.Services.Basket.API.Model;
using Microsoft.eShopOnContainers.Services.Basket.API.Services;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Basket.API.Controllers
{
    [Route("api/v1/cart")]
    [Authorize]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketDataRepository _repo;

        public ShippingController(
            ILogger<BasketController> logger,
            IBasketDataRepository repository,
            IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
        }

        [HttpPost("shipping-methods")]
        [ProducesResponseType(typeof(ShippingMethodResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShippingMethodResult>> GetPaymentMethodAsync(ShippingMethodRequest req)
        {
            var shipping_methods = new List<ShippingMethod>();
            shipping_methods.Add(new ShippingMethod(){
                CarrierCode = "flatrate",
                MethodCode = "flatrate",
                CarrierTitle = "Flat Rate",
                MethodTitle = "Fixed",
                Amount = 30,
                BaseAmount = 30,
                Available = true,
                ErrorMessage = "",
                PriceExclTax = 30,
                PriceInclTax = 30
            });

            return new ShippingMethodResult(){
                Result = shipping_methods,
                Code = 200
            };
        }

    }
}