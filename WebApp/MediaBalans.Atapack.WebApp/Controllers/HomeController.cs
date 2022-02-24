using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly IPageService _pageService;
        private readonly IServicesService _servicesService;
        private readonly IGalleryService _galleryService;

        public HomeController(ILogger<HomeController> logger,
            ISliderService sliderService,
            IProductService productService,
            IPageService pageService,
            IServicesService servicesService,
            IGalleryService galleryService)
        {
            _logger = logger;
            _sliderService = sliderService;
            _productService = productService;
            _pageService = pageService;
            _servicesService = servicesService;
            _galleryService = galleryService;
        }

        [Route("/")]
        [Route("/{lang}")]
        [Route("/{lang}/anasehife")]
        public async Task<IActionResult> Index(string lang = "az")
        {
            _logger.LogInformation("Ana Sayfa");
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var sliders = await _sliderService.GetSlidersAsync();
            var products = await _productService.GetProductsAsync();
            var services = await _servicesService.GetServicesAsync();
            var page = await _pageService.GetPageHomeAsync();

            return View(new HomeViewModel
            {
                Sliders = sliders.Data.ToList(),
                Products = products.Data.Take(10).ToList(),
                Services = services.Data.ToList(),
                Page = page.Data
            });
        }
        [Route("/{lang}/qalereya")]
        public async Task<IActionResult> Gallery(string lang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var galleries = await _galleryService.GetGalleriesAsync();
            return View(galleries.Data.Take(10).ToList());

        }

        [HttpPost]
        public async Task<PartialViewResult> Filter(int pageIndex, int pageSize)
        {
            string lang = "az";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var galleries = await _galleryService.GetGalleriesAsync();
            return PartialView(galleries.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}