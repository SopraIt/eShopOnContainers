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
    public class PaymentController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketDataRepository _repo;

        public PaymentController(
            ILogger<BasketController> logger,
            IBasketDataRepository repository,
            IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
        }

        [HttpGet("payment-methods")]
        [ProducesResponseType(typeof(PaymentMethodResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentMethodResult>> GetPaymentMethodAsync(string cartId)
        {
            var shipping_methods = new List<PaymentMethod>();
            shipping_methods.Add(new PaymentMethod(){
                Code = "authorizenet_directpost",
                Title = "Credit Card Direct Post (Authorize.net)"
            });
            shipping_methods.Add(new PaymentMethod(){
                Code = "cashondelivery",
                Title = "Cash On Delivery"
            });
            shipping_methods.Add(new PaymentMethod(){
                Code = "free",
                Title = "No Payment Information Required"
            });

            return new PaymentMethodResult(){
                Result = shipping_methods,
                Code = 200
            };
        }

    }
}