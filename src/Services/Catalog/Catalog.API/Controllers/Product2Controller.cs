using Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;
using Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.Services.Catalog.API.Model;
using Microsoft.eShopOnContainers.Services.Catalog.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.Services.Catalog.API;
using Catalog.Nosql.Infrastructure.Repositories;
using Catalog.Nosql.Model;
using Newtonsoft.Json;

namespace Catalog.API.Controllers
{
    [Route("api/v2/Product")]
    [ApiController]
    public class Product2Controller : ControllerBase
    {
        private readonly CatalogSettings _settings;
        private readonly ICatalogDataRepository _repo;
        public Product2Controller(IOptionsSnapshot<CatalogSettings> settings, ICatalogDataRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _settings = settings.Value;
        }

        [HttpGet]
        [Route("items/{sku}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductDetail), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDetail>> ItemByIdAsync(string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return BadRequest();
            }

            var item = await _repo.GetProductDetailBySkuAsync(sku);

            if (item != null)
            {
                return item;
            }
            else
            {
                return NotFound();
            }
        }
    }
}