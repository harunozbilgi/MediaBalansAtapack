using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Atapack.WebApp.Helpers;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IDocumentService _documentService; 
        private readonly IAppSeoService _appSeoService;
        private readonly IPagePropertyService _pagePropertyService;

        public PagesController(IPageService pageService, 
            IDocumentService documentService, 
            IAppSeoService appSeoService,
            IPagePropertyService pagePropertyService)
        {
            _pageService = pageService;
            _documentService = documentService;
            _appSeoService = appSeoService;
            _pagePropertyService = pagePropertyService;
        }

        #region Page

        public async Task<IActionResult> Index()
        {
            var list = await _pageService.GetPagesAsync();
            return View(list.Data.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel model)
        {
            try
            {
                string fileUrl = "";
                if (model.File != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/page/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    fileUrl += fileCode.Data;
                }

                var slugurl = UrlSeoHelper.UrlSeo(model.PageLanguages[0].Title);
                var seoCode = await _appSeoService.PageSeoAddAsync(model.Page.AppSeoCode, model.AppSeoLanguages);

                var addPage = await _pageService.AddPageAsync(new Domain.Entities.Page
                {
                    FileCode = fileUrl,
                    AppSeoCode = seoCode.Data,
                    SlugUrl = slugurl,
                    IsActive = model.Page.IsActive,
                    IsHome = model.Page.IsHome,
                    ShowView = model.Page.ShowView,
                });
                if (addPage.IsSuccessful)
                {
                    foreach (var language in model.PageLanguages)
                    {
                        language.PageId = addPage.Data.Id;
                        await _pageService.AddPageLanguageAsync(language);
                    }
                    TempData["Success"] = addPage.Messages;
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
                var pageRow = await _pageService.GetPageAsync(id);
                if (pageRow.IsSuccessful)
                {
                    var appSeo = await _appSeoService.GetBySeoCodeAsync(pageRow.Data.AppSeoCode);
                    return View(new PageViewModel
                    {
                        Page = pageRow.Data ?? null,
                        PageLanguages = pageRow.Data?.PageLanguages.OrderBy(x => x.Lang_Code).ToList(),
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
        public async Task<IActionResult> Edit(PageViewModel model)
        {
            try
            {
                if (model.File != null)
                {
                    if (model.Page.FileCode != null || model.Page.FileCode != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(model.Page.FileCode);
                    }
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/page/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    model.Page.FileCode = fileCode.Data;
                }
                model.Page.SlugUrl = UrlSeoHelper.UrlSeo(model.PageLanguages[0].Title);
                model.Page.AppSeoCode = (await _appSeoService.PageSeoAddAsync(model.Page.AppSeoCode, model.AppSeoLanguages)).Data;

                var serviceNews = await _pageService.UpdatePageAsync(model.Page);
                if (serviceNews.IsSuccessful)
                {
                    foreach (var language in model.PageLanguages)
                    {
                        await _pageService.UpdatePageLanguageAsync(language);
                    }
                    TempData["Success"] = serviceNews.Messages;
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
            var delete = await _pageService.GetPageAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.FileCode);
                var result = await _pageService.RemovePageAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hata";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Order(List<OrderInput> orders)
        {
            try
            {
                foreach (var item in orders)
                {
                    var orderUpdate = await _pageService.GetPageAsync(item.Id);
                    orderUpdate.Data.OrderBy = item.Place;
                    await _pageService.UpdatePageAsync(orderUpdate.Data);
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
        #endregion

        #region Property
        public async Task<IActionResult> Properties(string id)
        {
            var list = await _pagePropertyService.GetPagePropertiesAsync(id);
            ViewData["PageId"] = id;
            return View(list.Data);
        }
        public IActionResult PropertyCreate(string id)
        {
            ViewData["PageId"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyCreate(PageProperty model, IFormFile file)
        {
            try
            {
                string fileUrl = "";
                if (file != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = file,
                        FolderName = "files/page/property/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    fileUrl += fileCode.Data;
                }
                var addPageProperty = await _pagePropertyService.AddPagePropertyAsync(new PageProperty
                {
                    FileCode = fileUrl,
                    IsActive = model.IsActive,
                    Name = model.Name,
                    PageId = model.PageId,
                });
                TempData["Success"] = addPageProperty.Messages;
                return RedirectToAction("Properties", "Pages", new { id = model.PageId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PropertyEdit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var pageProRow = await _pagePropertyService.GetPagePropertyAsync(id);
                if (pageProRow.IsSuccessful)
                {
                    return View(pageProRow.Data);
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyEdit(PageProperty model, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (model.FileCode != null || model.FileCode != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(model.FileCode);
                    }
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = file,
                        FolderName = "files/page/property/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    model.FileCode = fileCode.Data;
                }
                var addProduct = await _pagePropertyService.UpdatePagePropertyAsync(model);
                TempData["Success"] = addProduct.Messages;
                return RedirectToAction("Properties", "Pages", new { id = model.PageId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }

        public async Task<IActionResult> PropertyDelete(string id)
        {
            var delete = await _pagePropertyService.GetPagePropertyAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.FileCode);
                var result = await _pagePropertyService.RemovePagePropertyAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction("Properties", "Pages", new { id = delete.Data.Id });
            }
            TempData["Error"] = "Hata";
            return RedirectToAction("Properties", "Pages", new { id = delete.Data.Id });
        }
        #endregion

    }
}
