using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class ContactController : Controller
    {
        [Route("/{lang}/elaqe")]
        public IActionResult Index(string lang)
        {
            ViewBag.Lang = HttpContext.Session.GetString("lang") ?? lang;
            return View();
        }

        [Route("/{lang}/karyera")]
        public IActionResult Career(string lang)
        {
            ViewBag.Lang = HttpContext.Session.GetString("lang")?? lang;
            return View();
        }
    }
}
