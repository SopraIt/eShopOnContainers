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
using Newtonsoft.Json;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoSqlCatalog : ControllerBase
    {
        private readonly CatalogSettings _settings;
        private readonly ICatalogDataRepository _repo;
        public NoSqlCatalog(IOptionsSnapshot<CatalogSettings> settings, ICatalogDataRepository repo){
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _settings = settings.Value;
        }

        [HttpGet]
        [Route("items/{id:string}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> ItemByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var item = await _repo.GetAsync(id);

            // var baseUri = _settings.PicBaseUrl;
            // var azureStorageEnabled = _settings.AzureStorageEnabled;

            // item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            if (item != null)
            {
                var result = JsonConvert.DeserializeObject(item.json);
                return item;
            }

            return NotFound();
        }
    }
}