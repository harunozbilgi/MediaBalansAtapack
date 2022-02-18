using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Settings;
using MediaBalans.Atapack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MediaBalans.Atapack.WebApp.Components
{
    public class ImageViewComponent : ViewComponent
    {
        private readonly IDocumentService _documentService;
        private readonly DocumentSetting _documentSetting;
        public ImageViewComponent(IDocumentService documentService, IOptions<DocumentSetting> documentSetting)
        {
            _documentService = documentService;
            _documentSetting = documentSetting.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(string FileCode)
        {
            var result = await _documentService.GetDocumentByIdAsync(FileCode);
            if (result.IsSuccessful)
            {
                var file = result.Data;
                return View(new ImageViewModel()
                {
                    Image = string.Concat(_documentSetting.StorageUrl, file.DocumentFolderName, file.DocumentName)

                });
            }
            return View(new ImageViewModel()
            {
                Image = ""
            });
        }
    }
}
