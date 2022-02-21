using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class AppConfigsController : Controller
    {
        private readonly IAppConfigService _appConfigService;
        private readonly IDocumentService _documentService;
        public AppConfigsController(IAppConfigService appConfigService,
            IDocumentService documentService)
        {
            _appConfigService = appConfigService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var row = await _appConfigService.GetByAppConfigAsync();
            if (row.IsSuccessful)
            {
                return View(row.Data);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AppConfig webConfig, IFormFile file, bool mode = false)
        {
            if (!mode)
            {
                if (file != null)
                {
                    webConfig.FileCode = (await _documentService.UploadAsync(new Application.Dtos.UploadInputDto { File = file, FolderName = "files/logo/" })).Data;
                }
                await _appConfigService.AddAsync(webConfig);
            }
            else
            {
                if (file != null)
                {
                    webConfig.FileCode = (await _documentService.UploadAsync(new Application.Dtos.UploadInputDto { File = file, FolderName = "files/logo/" })).Data;
                }
                await _appConfigService.UpdateAsync(webConfig);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
