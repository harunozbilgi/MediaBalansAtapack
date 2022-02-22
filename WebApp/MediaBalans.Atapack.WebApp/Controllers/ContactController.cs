using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class ContactController : Controller
    {
        [Route("/{lang}/elaqe")]
        public IActionResult Index(string lang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = lang;
            return View();
        }
    }
}
