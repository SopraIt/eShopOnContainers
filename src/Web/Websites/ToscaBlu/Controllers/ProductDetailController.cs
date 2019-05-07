using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.WebMVC.Services;
using Microsoft.eShopOnContainers.WebMVC.ViewModels;
using Microsoft.eShopOnContainers.WebMVC.ViewModels.CatalogViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMVC.Controllers
{
    public class ProductDetailController : Controller
    {
        private ICatalogService _catalogSvc;

        public ProductDetailController(ICatalogService catalogSvc) =>
                _catalogSvc = catalogSvc;

        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {
            var product = await _catalogSvc.GetCatalogItem(id);
            CatalogItem vm = product;
            return View(vm);
        }
    }
}
