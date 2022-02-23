using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Text;

namespace MediaBalans.Atapack.WebApp.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Change(string lang, string slugurl)
        {
            HttpContext.Session.SetString("lang", lang);
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true,
                    Path = "/",
                    HttpOnly = false,
                }
            );
            string newUlr = "";
            try
            {
                if (slugurl != "/")
                {
                    var stringBuilder = new StringBuilder(slugurl);
                    newUlr += stringBuilder.Remove(0, 5);
                }
                var returnUrl = string.Concat("/", lang, newUlr);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl.Replace("%2F", "/"));
                }
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            return Redirect("/");
        }
    }
}
