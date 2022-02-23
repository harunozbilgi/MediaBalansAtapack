using MediaBalans.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Components
{
    public class SeoViewComponent : ViewComponent
    {
        private readonly IAppSeoService _seoService;
        public SeoViewComponent(IAppSeoService seoService)
        {
            _seoService = seoService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string page, string filecode = null)
        {
            if (page == null) return null;
            var seo = await _seoService.GetByPageNameAsync(page);
            ViewData["filecode"] = filecode;
            if (seo.IsSuccessful)
                return View(seo.Data);
            else
                seo = await _seoService.GetBySeoCodeAsync(page);
            return View(seo.Data);
        }
    }
}
