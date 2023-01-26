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
            var product = slugUrl != "all" 
                ? (await _productService.GetProductsAsync()).Data.Where(x => x.Category.SlugUrl.ToLower() == slugUrl.ToLower())
                : (await _productService.GetProductsAsync()).Data;

            var categories = await _categoryService.GetCategoriesAsync();

            return View(new ProductsViewModel
            {
                Products = product.Take(10).ToList(),
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
                    Products = products.Data.Where(x => x.Id != product.Data.Id).Take(4).ToList()
                });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PartialViewResult> Filter(int pageIndex, int pageSize, string filterId)
        {
            string lang = "az";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var products = (await _productService.GetProductsAsync()).Data.Where(x => x.Category.SlugUrl.ToLower() == filterId.ToLower());
            return PartialView(products.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


    }
}
