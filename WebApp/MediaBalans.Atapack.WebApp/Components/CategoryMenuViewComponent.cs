using MediaBalans.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("lang")))
                ViewBag.Lang = HttpContext.Session.GetString("lang");
            ViewBag.Lang = "az";
            var services = await _categoryService.GetCategoriesAsync();
            return View(services.Data);
        }
    }
}
