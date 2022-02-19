using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class SlidersController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IDocumentService _documentService;

        public SlidersController(ISliderService sliderService, IDocumentService documentService)
        {
            _sliderService = sliderService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sliderService.GetSlidersAsync();
            return View(list.Data.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderViewModel sliderView, IFormFile file)
        {
            try
            {
                string resultFileCoe = "";
                if (file != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto { File = file, FolderName = "files/slayder/" });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(sliderView);
                    }
                    resultFileCoe += fileCode.Data;
                }
                var sliderAdd = await _sliderService.AddSliderAsync(new Slider() { FileCode = resultFileCoe, Url = sliderView.Slider.Url, IsActive = sliderView.Slider.IsActive });
                if (sliderAdd.IsSuccessful)
                {
                    foreach (var item in sliderView.SliderLanguages)
                    {
                        item.SliderId = sliderAdd.Data.Id;
                        await _sliderService.AddSliderLanguageAsync(item);
                    }
                    TempData["Success"] = sliderAdd.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(sliderView);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var sliderRow = await _sliderService.GetSliderAsync(id);
                if (sliderRow.IsSuccessful)
                {
                    return View(new SliderViewModel
                    {
                        Slider = sliderRow.Data ?? null,
                        SliderLanguages = sliderRow.Data?.SliderLanguages.OrderBy(x => x.Lang_Code).ToList()
                    });
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderViewModel sliderView, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (sliderView.Slider.FileCode != null || sliderView.Slider.FileCode != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(sliderView.Slider.FileCode);
                    }
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto { File = file, FolderName = "files/slayder/" });

                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(sliderView);
                    }
                    sliderView.Slider.FileCode = fileCode.Data;
                }
                var sliderUpdate = await _sliderService.UpdateSliderAsync(sliderView.Slider);
                if (sliderUpdate.IsSuccessful)
                {
                    foreach (var item in sliderView.SliderLanguages)
                    {
                        await _sliderService.UpdateSliderLanguageAsync(item);
                    }
                    TempData["Success"] = sliderUpdate.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(sliderView);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var delete = await _sliderService.GetSliderAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.FileCode);
                var result = await _sliderService.RemoveSliderAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hata";
            return RedirectToAction(nameof(Index));
        }
    }
}
