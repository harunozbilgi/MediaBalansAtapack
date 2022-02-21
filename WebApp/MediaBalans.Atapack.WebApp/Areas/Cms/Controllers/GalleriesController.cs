using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class GalleriesController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly IDocumentService _documentService;

        public GalleriesController(IGalleryService galleryService, IDocumentService documentService)
        {
            _galleryService = galleryService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _galleryService.GetGalleriesAsync();
            return View(list.Data);
        }

        public async Task<IActionResult> Properties()
        {
            var list = await _galleryService.GetGalleriesAsync();
            return View(list.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageProperty model, IFormFile file)
        {
            try
            {
                string fileUrl = "";
                if (file != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = file,
                        FolderName = "files/gallery/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    fileUrl += fileCode.Data;
                }
                var addPageProperty = await _galleryService.AddGalleryAsync(new Gallery
                {
                    FileCode = fileUrl,
                    IsActive = model.IsActive,
                    Name = model.Name
                });
                TempData["Success"] = addPageProperty.Messages;
                return RedirectToAction(nameof(Index));
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
                var pageProRow = await _galleryService.GetGalleryAsync(id);
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
        public async Task<IActionResult> Edit(Gallery model, IFormFile file)
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
                        FolderName = "files/gallery/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    model.FileCode = fileCode.Data;
                }
                var addProduct = await _galleryService.UpdateGalleryAsync(model);
                TempData["Success"] = addProduct.Messages;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var delete = await _galleryService.GetGalleryAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.FileCode);
                var result = await _galleryService.RemoveGalleryAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hata";
            return RedirectToAction(nameof(Index));
        }
    }
}
