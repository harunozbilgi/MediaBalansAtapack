using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class PortfoliosController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [Route("/{lang}/portfolio/")]
        public async Task<IActionResult> Index(string lang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var portfolio = await _portfolioService.GetPortfoliosAsync();
            return View(new PortfoliosViewModel
            {
                Portfolios = portfolio.Data.Take(10).ToList()
            });
        }

        public async Task<PartialViewResult> PortfolioDetail(string id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                ViewBag.Lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = "az";
            var reponse = await _portfolioService.GetPortfolioAsync(id);
            return PartialView(new PortfoliosViewModel
            {
                Portfolio = reponse.Data
            });
        }

        [HttpPost]
        public async Task<PartialViewResult> Filter(int pageIndex, int pageSize)
        {
            string lang = "az";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            var portfolios = await _portfolioService.GetPortfoliosAsync();
            return PartialView(portfolios.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
    }
}
