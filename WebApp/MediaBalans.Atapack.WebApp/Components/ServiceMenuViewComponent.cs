using MediaBalans.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Components
{
    public class ServiceMenuViewComponent : ViewComponent
    {
        private readonly IServicesService _servicesService;

        public ServiceMenuViewComponent(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                ViewBag.Lang = HttpContext.Session.GetString("lang");
            else
                ViewBag.Lang = "az";
            var services = await _servicesService.GetServicesAsync();
            return View(services.Data);
        }
    }
}
