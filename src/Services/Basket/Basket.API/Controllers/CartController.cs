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
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;

        public CartController(
            ILogger<BasketController> logger,
            IBasketRepository repository,
            IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet("{pull}")]
        [ProducesResponseType(typeof(CartResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartResult>> GetBasketAsync(int cartId)
        {
            return new CartResult(){
                Code = 200
            };
        }

        [HttpPost("{create}")]
        [ProducesResponseType(typeof(CartCreateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartCreateResult>> CreateBasketAsync()
        {
            return new CartCreateResult(){
                Code = 200
            };
        }

        [HttpPost("{update}")]
        [ProducesResponseType(typeof(CartUpdateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartUpdateResult>> UpdateBasketAsync()
        {
            return new CartUpdateResult(){
                Code = 200
            };
        }

    }
}