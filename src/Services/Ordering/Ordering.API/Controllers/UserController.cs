using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;
using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries;
using Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Behaviors;
using Ordering.API.Application.Commands;
using Ordering.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderQueries _orderQueries;
        private readonly IIdentityService _identityService;
        private readonly ILogger<OrdersController> _logger;

        public UserController(
            IMediator mediator,
            IOrderQueries orderQueries,
            IIdentityService identityService,
            ILogger<OrdersController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<UserResponse>> Me()
        {
            try
            {
                return new UserResponse()
                {
                    Code = 200,
                    Result = new User(){
                        UserId = User.FindFirst("sub")?.Value,
                        Email = User.FindFirst("preferred_username")?.Value,
                        Firstname = User.FindFirst("name")?.Value,
                        Lastname = User.FindFirst("last_name")?.Value
                    }
                };
            }
            catch (Exception e)
            {
                return new UserResponse()
                {
                    Code = 500
                };
            }
        }    

        [HttpGet]
        [Route("order-hystory")]
        public async Task<ActionResult<UserResponse>> OrderHistory()
        {
            try
            {
                return new UserResponse()
                {
                    Code = 200
                };
            }
            catch (Exception e)
            {
                return new UserResponse()
                {
                    Code = 500
                };
            }
        }    
    }
}
