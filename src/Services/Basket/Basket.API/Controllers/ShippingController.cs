using Basket.API.Infrastructure.NoSql;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
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
        private readonly ILogger<ShippingController> _logger;
        private readonly IBasketDataRepository _repo;
        private readonly IConfigDataRepository _repo_config;
        private readonly ICartService _cartService;

        public ShippingController(
            ILogger<ShippingController> logger,
            IBasketDataRepository repository,
            IEventBus eventBus,
            ICartService cartService,
            IConfigDataRepository repository_config)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
            _repo_config = repository_config;
            _cartService = cartService;
        }

        [HttpPost("shipping-methods")]
        [ProducesResponseType(typeof(ShippingMethodResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShippingMethodResult>> GetShippingMethodsAsync(ShippingMethodRequest req)
        {
            var shipping_methods = await _repo_config.GetShippingMethosAsync();

            return new ShippingMethodResult(){
                Result = shipping_methods,
                Code = 200
            };
        }

        [HttpPost("shipping-information")]
        [ProducesResponseType(typeof(ShippingMethodResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShippingInformationResult>> GetShippingInformationAsync(ShippingInformationRequest req)
        {
            var UserId = User.FindFirst("sub")?.Value;
            var cart = await _repo.GetCartAsync(UserId);

            var cart_total = await _cartService.CalculateCartTotal(cart, req.addressInformation);
            string result = await _repo.UpsertCartTotalAsync(UserId, cart_total.total);
            
            var payment_methods = await _repo_config.GetPaymentMethosAsync();

            ShippingInformation ship = new ShippingInformation(){
                payment_methods = payment_methods,
                totals = cart_total.total
            };

            return new ShippingInformationResult(){
                Result = ship,
                Code = 200
            };
        }

    }
}