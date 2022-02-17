using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class HomeController : Controller
    {
        [Route("/cms")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
