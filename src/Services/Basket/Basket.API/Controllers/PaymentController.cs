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
        private readonly ILogger<BasketController> _logger;
        private readonly IConfigDataRepository _repo;

        public PaymentController(
            ILogger<BasketController> logger,
            IConfigDataRepository repository)
        {
            _logger = logger;
            _repo = repository;
        }

        [HttpGet("payment-methods")]
        [ProducesResponseType(typeof(PaymentMethodResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentMethodResult>> GetPaymentMethodAsync(string cartId)
        {
            var shipping_methods = await _repo.GetPaymentMethosAsync();

            return new PaymentMethodResult(){
                Result = shipping_methods,
                Code = 200
            };
        }

    }
}