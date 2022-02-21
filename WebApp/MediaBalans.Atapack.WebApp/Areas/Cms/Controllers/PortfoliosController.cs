using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class PortfoliosController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IDocumentService _documentService;

        public PortfoliosController(IPortfolioService portfolioService, IDocumentService documentService)
        {
            _portfolioService = portfolioService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _portfolioService.GetPortfoliosAsync();
            return View(list.Data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioViewModel model)
        {
            try
            {
                List<string> fileArray = new();
                if (model.Files != null)
                {
                    foreach (var item in model.Files)
                    {
                        var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                        {
                            File = item,
                            FolderName = "files/portfolio/"
                        });
                        if (!fileCode.IsSuccessful)
                        {
                            TempData["Warning"] = fileCode.Messages;
                            return View(model);
                        }
                        fileArray.Add(fileCode.Data);
                    }
                }
                else
                {
                    TempData["Error"] = "Lütfen bu alanı boş geçmeyiniz.";
                    return View(model);
                }
                var addPortfolio = await _portfolioService.AddPortfolioAsync(new Domain.Entities.Portfolio
                {
                    IsActive = model.Portfolio.IsActive,
                });
                if (addPortfolio.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _portfolioService.AddProductFileAsync(new Domain.Entities.PortfolioFile
                        {
                            FileCode = file,
                            PortfolioId = addPortfolio.Data.Id
                        });

                    }
                    foreach (var language in model.PortfolioLanguages)
                    {
                        language.PortfolioId = addPortfolio.Data.Id;
                        await _portfolioService.AddPortfolioLangauageAsync(language);
                    }
                    TempData["Success"] = addPortfolio.Messages;
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
                var portRow = await _portfolioService.GetPortfolioAsync(id);
                if (portRow.IsSuccessful)
                {
                    return View(new PortfolioViewModel
                    {
                        Portfolio = portRow.Data ?? null,
                        PortfolioLanguages = portRow.Data?.PortfolioLanguages.OrderBy(x => x.Lang_Code).ToList(),
                    });
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioViewModel model)
        {
            try
            {
                List<string> fileArray = new();
                if (model.Files != null)
                {
                    var delete = await _portfolioService.GetPortfolioAsync(model.Portfolio.Id.ToString());
                    if (delete.IsSuccessful)
                    {
                        foreach (var item in delete.Data.PortfolioFiles)
                        {
                            await _documentService.DeleteFolderAsync(item.FileCode);

                        }
                        await _portfolioService.RemovePortfolioFileAsync(model.Portfolio.Id.ToString());
                    }

                    foreach (var item in model.Files)
                    {
                        var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                        {
                            File = item,
                            FolderName = "files/prodcut/"
                        });
                        if (!fileCode.IsSuccessful)
                        {
                            TempData["Warning"] = fileCode.Messages;
                            return View(model);
                        }
                        fileArray.Add(fileCode.Data);
                    }
                }

                var updateProdcut = await _portfolioService.UpdatePortfolioAsync(model.Portfolio);
                if (updateProdcut.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _portfolioService.AddProductFileAsync(new Domain.Entities.PortfolioFile
                        {
                            FileCode = file,
                            PortfolioId = model.Portfolio.Id,
                        });

                    }
                    foreach (var language in model.PortfolioLanguages)
                    {

                        await _portfolioService.UpdatePortfolioLanguageAsync(language);
                    }
                    TempData["Success"] = updateProdcut.Messages;
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
            var delete = await _portfolioService.GetPortfolioAsync(id);
            if (delete.IsSuccessful)
            {
                foreach (var item in delete.Data.PortfolioFiles)
                {
                    await _documentService.DeleteFolderAsync(item.FileCode);
                }
                var result = await _portfolioService.RemovePortfolioAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Hata";
            return RedirectToAction(nameof(Index));
        }
    }
}
