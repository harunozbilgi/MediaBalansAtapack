using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [Route("/{lang}/xeberler")]
        public async Task<IActionResult> Index(string lang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var news = await _newsService.GetNewsAsync();
            return View(new NewViewModel
            {
                NewsList = news.Data.Take(10).ToList()
            });
        }

        [Route("/{lang}/xeberler/{slugUrl}")]
        public async Task<IActionResult> Detail(string lang, string slugUrl)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            if (string.IsNullOrEmpty(slugUrl))
                return RedirectToAction(nameof(Index));
            var news = await _newsService.GetNewSlugUrlAsync(slugUrl);
            if (news.IsSuccessful)
            {
                var products = await _newsService.GetNewsAsync();
                return View(new NewViewModel
                {
                    News = news.Data,
                    NewsList = products.Data.Where(x => x.Id != news.Data.Id).ToList()
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
            var news = await _newsService.GetNewsAsync();
            return PartialView(news.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
    }
}
