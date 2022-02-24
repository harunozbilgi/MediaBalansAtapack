using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class ServicesController : Controller
    {

        private readonly IServicesService _servicesService;
        private readonly IServicePropertyService _servicePropertyService;

        public ServicesController(IServicesService servicesService,
            IServicePropertyService servicePropertyService)
        {
            _servicesService = servicesService;
            _servicePropertyService = servicePropertyService;
        }

        [Route("/{lang}/xidmetlerimiz/{slugUrl}")]
        public async Task<IActionResult> Detail(string lang, string slugUrl)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            if (string.IsNullOrEmpty(slugUrl))
                return RedirectToAction(nameof(Index), "Home");
            var service = await _servicesService.GetServiceSlugUrlAsync(slugUrl);
            if (service.IsSuccessful)
            {
                var serviceProperties = await _servicePropertyService.GetServicePropertiesAsync(service.Data.Id.ToString());
                return View(new ServicesViewModel
                {
                    Service = service.Data,
                    ServiceProperties = serviceProperties.Data.Take(10).ToList()
                });
            }
            return RedirectToAction(nameof(Index), "Home");
        }
        [HttpGet]
        public async Task<PartialViewResult> PropertyDetail(string serviceId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                ViewBag.Lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = "az";
            var reponse = await _servicePropertyService.GetServiceByIdAsync(serviceId);
            return PartialView(reponse.Data);
        }
        [HttpPost]
        public async Task<PartialViewResult> Filter(int pageIndex, int pageSize, string filterId)
        {
            string lang = "az";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var serviceProperties = await _servicePropertyService.GetServicePropertiesAsync(filterId);
            return PartialView(serviceProperties.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
    }
}
