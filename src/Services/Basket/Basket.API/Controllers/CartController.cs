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
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketDataRepository _repo;

        public CartController(
            ILogger<BasketController> logger,
            IBasketDataRepository repository,
            IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
        }

        [HttpGet("pull")]
        [ProducesResponseType(typeof(CartResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartResult>> GetBasketAsync(string cartId)
        {
            var UserId = User.FindFirst("sub")?.Value;
            var cart = await _repo.GetAsync(UserId);

            return new CartResult(){
                Result = cart,
                Code = 200
            };
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CartCreateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartCreateResult>> CreateBasketAsync()
        {
            var UserId = User.FindFirst("sub")?.Value;
            string result = await _repo.UpsertAsync(UserId, null);

            return new CartCreateResult(){
                Result = result,
                Code = 200
            };
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(CartUpdateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartUpdateResult>> UpdateBasketAsync(string cartId, CartUpdateRequest cart)
        {
            var UserId = User.FindFirst("sub")?.Value;
            string result = await _repo.UpsertAsync(UserId, cart.CartItem);

            return new CartUpdateResult(){
                Result = cart.CartItem,
                Code = 200
            };
        }

    }
}