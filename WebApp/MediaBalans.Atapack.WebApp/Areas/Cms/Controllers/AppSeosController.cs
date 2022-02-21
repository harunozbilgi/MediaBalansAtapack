using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class AppSeosController : Controller
    {
        private readonly IAppSeoService _seoService;
        public AppSeosController(IAppSeoService seoService)
        {
            _seoService = seoService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _seoService.GetAppSeoStaticListAsync();
            if (list.IsSuccessful)
            {
                return View(list.Data);
            }
            return NotFound();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppSeoViewModel appSeoVM)
        {
            try
            {
                var result = await _seoService.AddAppSeoAsync(new AppSeo()
                {
                    IsStaticPage = appSeoVM.AppSeo.IsStaticPage,
                    AppSeoCode = null,
                    IsDinamicPage = false,
                    Home = appSeoVM.AppSeo.Home
                });
                if (result.IsSuccessful)
                {
                    foreach (var item in appSeoVM.AppSeoLanguages)
                    {
                        item.AppSeoId = result.Data.Id;
                        await _seoService.AddAppSeoLanguageAsync(item);
                    }
                }
                TempData["Success"] = result.Messages;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string Id)
        {
            if (Id != null)
            {
                var appSeoRow = await _seoService.GetByAppSeoIdAsync(Id);
                if (appSeoRow.IsSuccessful)
                {
                    return View(new AppSeoViewModel()
                    {
                        AppSeo = appSeoRow.Data,
                        AppSeoLanguages = appSeoRow.Data.AppSeoLanguages.OrderBy(x => x.Lang_Code).ToList()
                    });
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppSeoViewModel appSeoVM)
        {
            try
            {
                var appSepUpdate = await _seoService.UpdateAppSeoAsync(appSeoVM.AppSeo);
                if (appSepUpdate.IsSuccessful)
                {
                    foreach (var item in appSeoVM.AppSeoLanguages)
                    {
                        await _seoService.UpdateAppSeoLanguageAsync(item);
                    }
                }
                TempData["Success"] = appSepUpdate.Messages;

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var row = await _seoService.GetByAppSeoIdAsync(Id);
            if (row.IsSuccessful)
            {
                var remove = await _seoService.RemoveAppSeoAsync(row.Data);
                TempData["Success"] = remove.Data;
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
