using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, 
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("/{lang}/mehsuller/")]
        [Route("/{lang}/mehsuller/kategori/{slugUrl}")]
        public async Task<IActionResult> Index(string lang, string slugUrl)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            ViewData["url"] = slugUrl;
            var product = await _productService.GetProductsAsync();
            var categories = await _categoryService.GetCategoriesAsync();

            return View(new ProductsViewModel
            {
                Products = product.Data.Take(10).ToList(),
                Categories = categories.Data.ToList()
            });
        }
        [Route("/{lang}/mehsuller/{slugUrl}")]
        public async Task<IActionResult> Detail(string lang, string slugUrl)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            if (string.IsNullOrEmpty(slugUrl))
                return RedirectToAction(nameof(Index));
            var product = await _productService.GetProductSlugUrl(slugUrl);
            if (product.IsSuccessful)
            {
                var products = await _productService.GetProductsAsync();
                return View(new ProductsViewModel
                {
                    Product = product.Data,
                    Products = products.Data.Where(x => x.Id != product.Data.Id).ToList()
                });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PartialViewResult> Filter(int pageIndex, int pageSize)
        {
            string lang = "az";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var prodcuts = await _productService.GetProductsAsync();
            return PartialView(prodcuts.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
    }
}
