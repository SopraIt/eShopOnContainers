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
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<CartController> _logger;
        private readonly IBasketDataRepository _repo;
        private readonly ICartService _cartService;

        public CartController(
            ILogger<CartController> logger,
            IBasketDataRepository repository,
            IEventBus eventBus,
            ICartService cartService)
        {
            _logger = logger;
            _eventBus = eventBus;
            _repo = repository;
            _cartService = cartService;
        }

        [HttpGet("pull")]
        [ProducesResponseType(typeof(CartResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartResult>> GetBasketAsync(string cartId)
        {
            var UserId = User.FindFirst("sub")?.Value;
            var cart = await _repo.GetCartItemsAsync(UserId);

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
            string result = await _repo.UpsertCartAsync(UserId);

            return new CartCreateResult(){
                Result = result,
                Code = 200
            };
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(CartUpdateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartUpdateResult>> UpdateBasketAsync(string cartId, CartUpdateRequest cart_update)
        {
            var UserId = User.FindFirst("sub")?.Value;
            var cart_item = await _cartService.CalculateCartItem(cart_update.CartItem);
            string result = await _repo.UpsertCartItemAsync(UserId, cart_item);

            return new CartUpdateResult(){
                Result = cart_item,
                Code = 200
            };
        }

        [HttpPost("delete")]
        [ProducesResponseType(typeof(CartUpdateResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartDeleteResult>> DeleteBasketAsync(string cartId, CartUpdateRequest cart_delete)
        {
            var UserId = User.FindFirst("sub")?.Value;
            string result = await _repo.DeleteCartItemAsync(UserId, cart_delete.CartItem);

            return new CartDeleteResult(){
                Result = true,
                Code = 200
            };
        }

    }
}