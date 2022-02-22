using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IPagePropertyService _pagePropertyService;

        public AboutController(IPageService pageService, IPagePropertyService pagePropertyService)
        {
            _pageService = pageService;
            _pagePropertyService = pagePropertyService;
        }

        [Route("/{lang}/{slugUrl}")]
        public async Task<IActionResult> Index(string lang, string slugUrl)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            if (string.IsNullOrEmpty(slugUrl))
                return RedirectToAction(nameof(Index), "Home");
            var page = await _pageService.GetPageSlugUrlAsync(slugUrl);
            if (page.IsSuccessful)
            {
                var pageProperties = await _pagePropertyService.GetPagePropertiesAsync(page.Data.Id.ToString());
                return View(new PagesViewModel
                {
                    Page = page.Data,
                    PageProperties = pageProperties.Data.ToList()
                });
            }
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
