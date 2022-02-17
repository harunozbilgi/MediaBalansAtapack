using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class LanguagesController : Controller
    {
        private readonly ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService) => _languageService = languageService;
        public async Task<IActionResult> Index()
        {
            var result = await _languageService.GetLanguagesAsync();
            return View(result.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language)
        {
            try
            {
                var success = await _languageService.AddLanguageAsync(language);
                if (success.IsSuccessful)
                {
                    TempData["Success"] = success.Messages;
                }
                return RedirectToAction(nameof(LanguagesController.Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(language);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var row = await _languageService.GetLanguageAsync(id);
            if (row.IsSuccessful)
            {
                return View(row.Data);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Language language)
        {
            try
            {
                var success = await _languageService.UpdateLanguageAsync(language);
                if (success.IsSuccessful)
                {
                    TempData["Success"] = success.Messages;
                }
                return RedirectToAction(nameof(LanguagesController.Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(language);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var row = await _languageService.GetLanguageAsync(id);
            if (row.IsSuccessful)
            {
                var result = await _languageService.RemoveLanguageAsync(row.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(LanguagesController.Index));
            }
            return NotFound();
        }
    }
}
