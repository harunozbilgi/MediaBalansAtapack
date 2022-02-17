using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Atapack.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return View(result.Data?.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            try
            {
              
                var slugurl = UrlSeoHelper.UrlSeo(categoryViewModel.CategoryLanguages[0].Title);
                var categoryAdd = await _categoryService.AddCategoryAsync(new Domain.Entities.Category
                {
                    SlugUrl = slugurl
                });
                if (categoryAdd.IsSuccessful)
                {
                    foreach (var item in categoryViewModel.CategoryLanguages)
                    {
                        item.CategoryId = categoryAdd.Data.Id;
                        await _categoryService.AddCategoryLanguageAsync(item);
                    }
                    TempData["Success"] = categoryAdd.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(categoryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var categoryRow = await _categoryService.GetCategoryAsync(id);
                if (categoryRow.IsSuccessful)
                {
                    return View(new CategoryViewModel
                    {
                        Category = categoryRow.Data ?? null,
                        CategoryLanguages = categoryRow.Data?.CategoryLanguages.OrderBy(x => x.Lang_Code).ToList()
                    });
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            try
            {
                categoryViewModel.Category.SlugUrl = UrlSeoHelper.UrlSeo(categoryViewModel.CategoryLanguages[0].Title);
                var updateCategory = await _categoryService.UpdateCategoryAsync(categoryViewModel.Category);
                if (updateCategory.IsSuccessful)
                {
                    foreach (var item in categoryViewModel.CategoryLanguages)
                    {
                        await _categoryService.UpdateCategoryLanguageAsync(item);
                    }
                    TempData["Success"] = updateCategory.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(categoryViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var delete = await _categoryService.GetCategoryAsync(id);
            if (delete.IsSuccessful)
            {
                var result = await _categoryService.RemoveCategoryAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(CategoriesController.Index));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Order(List<OrderInput> orders)
        {
            try
            {
                foreach (var item in orders)
                {
                    var orderUpdate = await _categoryService.GetCategoryAsync(item.Id);
                    orderUpdate.Data.OrderBy = item.Place;
                    await _categoryService.UpdateCategoryAsync(orderUpdate.Data);
                }
                return Ok(new
                {
                    Status = 200,
                    Message = "Güncelleme işlemi başarılı"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = 404,
                    ex.Message
                });
            }
        }
    }
}
