using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Catalog.Nosql.Infrastructure.Repositories;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Microsoft.eShopOnContainers.Services.Catalog.API.Controllers
{
    
    [ApiController]
    [Route("api/v2/Stock")]
    public class Stock2Controller : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly ICatalogDataRepository _repo;

        public Stock2Controller(IHostingEnvironment env,
            ICatalogDataRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _env = env;
        }

        [HttpGet]
        [Route("check/{sku}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> Check(string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return BadRequest();
            }

            var is_in_stock = await _repo.CheckStock(sku);

            return is_in_stock;
        }


        [HttpGet]
        [Route("check")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> Check1(string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return BadRequest();
            }

            var is_in_stock = await _repo.CheckStock(sku);

            return is_in_stock;
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckList(string skus)
        {
            

            return NotFound();
        }
    }
}
