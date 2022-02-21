using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Atapack.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IAppSeoService _appSeoService;
        private readonly IDocumentService _documentService;

        public NewsController(INewsService newsService, IAppSeoService appSeoService, IDocumentService documentService)
        {
            _newsService = newsService;
            _appSeoService = appSeoService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _newsService.GetNewsAsync();
            return View(list.Data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            try
            {
                string fileUrl = "";
                if (model.File != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/news/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    fileUrl += fileCode.Data;
                }
                var slugurl = UrlSeoHelper.UrlSeo(model.NewsLanguages[0].Title);
                var seoCode = await _appSeoService.PageSeoAddAsync(model.News.AppSeoCode, model.AppSeoLanguages);

                var addNews = await _newsService.AddNewsAsync(new Domain.Entities.News
                {
                    FileCode = fileUrl,
                    AppSeoCode = seoCode.Data,
                    SlugUrl = slugurl,
                    IsActive = model.News.IsActive,
                    CreateTime = model.News.CreateTime
                });
                if (addNews.IsSuccessful)
                {
                    foreach (var language in model.NewsLanguages)
                    {
                        language.NewsId = addNews.Data.Id;
                        await _newsService.AddNewsLanguageAsync(language);
                    }
                    TempData["Success"] = addNews.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var newsRow = await _newsService.GetNewAsync(id);
                if (newsRow.IsSuccessful)
                {
                    var appSeo = await _appSeoService.GetBySeoCodeAsync(newsRow.Data.AppSeoCode);
                    return View(new NewsViewModel
                    {
                        News = newsRow.Data ?? null,
                        NewsLanguages = newsRow.Data?.NewsLanguages.OrderBy(x => x.Lang_Code).ToList(),
                        AppSeo = appSeo.Data ?? null,
                        AppSeoLanguages = appSeo.Data?.AppSeoLanguages.ToList()
                    });
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsViewModel model)
        {
            try
            {
                if (model.File != null)
                {
                    if (model.News.FileCode != null || model.News.FileCode != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(model.News.FileCode);
                       
                    }
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/news/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    model.News.FileCode = fileCode.Data;
                }
                model.News.SlugUrl = UrlSeoHelper.UrlSeo(model.NewsLanguages[0].Title);
                model.News.AppSeoCode = (await _appSeoService.PageSeoAddAsync(model.News.AppSeoCode, model.AppSeoLanguages)).Data;

                var updateNews = await _newsService.UpdateNewsAsync(model.News);
                if (updateNews.IsSuccessful)
                {
                    foreach (var language in model.NewsLanguages)
                    {
                        await _newsService.UpdateNewsLanguageAsync(language);
                    }
                    TempData["Success"] = updateNews.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var delete = await _newsService.GetNewAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.FileCode);
                var result = await _newsService.RemoveNewsAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hata";
            return RedirectToAction(nameof(Index));
        }
    }
}
