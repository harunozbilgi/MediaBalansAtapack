using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Atapack.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly IDocumentService _documentService;
        private readonly IAppSeoService _appSeoService;
        private readonly IServicePropertyService _propertyService;

        public ServicesController(IServicesService servicesService, 
            IDocumentService documentService, 
            IAppSeoService appSeoService,
            IServicePropertyService propertyService)
        {
            _servicesService = servicesService;
            _documentService = documentService;
            _appSeoService = appSeoService;
            _propertyService = propertyService;
        }

        #region Service

        public async Task<IActionResult> Index()
        {
            var list = await _servicesService.GetServicesAsync();
            return View(list.Data.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            try
            {
                string fileUrl = "";
                if (model.File != null)
                {
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/service/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    fileUrl += fileCode.Data;
                }
                string fileIconUrl = "";
                if (model.Icon != null)
                {
                    var fileIconCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.Icon,
                        FolderName = "files/service/"
                    });
                    if (!fileIconCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileIconCode.Messages;
                        return View(model);
                    }
                    fileIconUrl += fileIconCode.Data;
                }


                var slugurl = UrlSeoHelper.UrlSeo(model.ServiceLanguages[0].Title);
                var seoCode = await _appSeoService.PageSeoAddAsync(model.Service.AppSeoCode, model.AppSeoLanguages);

                var addService = await _servicesService.AddServiceAsync(new Domain.Entities.Service
                {
                    Icon = fileIconUrl,
                    Image_Url = fileUrl,
                    AppSeoCode = seoCode.Data,
                    SlugUrl = slugurl,
                    IsActive = model.Service.IsActive,
                    IsPage = model.Service.IsPage,
                });
                if (addService.IsSuccessful)
                {
                    foreach (var language in model.ServiceLanguages)
                    {
                        language.ServiceId = addService.Data.Id;
                        await _servicesService.AddServiceLanguageAsync(language);
                    }
                    TempData["Success"] = addService.Messages;
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
                var serviceRow = await _servicesService.GetServiceAsync(id);
                if (serviceRow.IsSuccessful)
                {
                    var appSeo = await _appSeoService.GetBySeoCodeAsync(serviceRow.Data.AppSeoCode);
                    return View(new ServiceViewModel
                    {
                        Service = serviceRow.Data ?? null,
                        ServiceLanguages = serviceRow.Data?.ServiceLanguages.OrderBy(x => x.Lang_Code).ToList(),
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
        public async Task<IActionResult> Edit(ServiceViewModel model)
        {
            try
            {
                if (model.File != null)
                {
                    if (model.Service.Image_Url != null || model.Service.Image_Url != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(model.Service.Image_Url);
                    }
                    var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.File,
                        FolderName = "files/service/"
                    });
                    if (!fileCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileCode.Messages;
                        return View(model);
                    }
                    model.Service.Image_Url = fileCode.Data;
                }
                if (model.Icon != null)
                {
                    if (model.Service.Icon != null || model.Service.Icon != string.Empty)
                    {
                        var deleteFile = await _documentService.DeleteFolderAsync(model.Service.Icon);
                    }
                    var fileIconCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                    {
                        File = model.Icon,
                        FolderName = "files/service/"
                    });
                    if (!fileIconCode.IsSuccessful)
                    {
                        TempData["Warning"] = fileIconCode.Messages;
                        return View(model);
                    }
                    model.Service.Icon = fileIconCode.Data;
                }
                model.Service.SlugUrl = UrlSeoHelper.UrlSeo(model.ServiceLanguages[0].Title);
                model.Service.AppSeoCode = (await _appSeoService.PageSeoAddAsync(model.Service.AppSeoCode, model.AppSeoLanguages)).Data;

                var serviceNews = await _servicesService.UpdateServiceAsync(model.Service);
                if (serviceNews.IsSuccessful)
                {
                    foreach (var language in model.ServiceLanguages)
                    {
                        await _servicesService.UpdateServiceLanguageAsync(language);
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
            var delete = await _servicesService.GetServiceAsync(id);
            if (delete.IsSuccessful)
            {
                await _documentService.DeleteFolderAsync(delete.Data.Image_Url);
                await _documentService.DeleteFolderAsync(delete.Data.Icon);
                var result = await _servicesService.RemoveServiceAsync(delete.Data);
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
                    var orderUpdate = await _servicesService.GetServiceAsync(item.Id);
                    orderUpdate.Data.OrderBy = item.Place;
                    await _servicesService.UpdateServiceAsync(orderUpdate.Data);
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
            var list=await _propertyService.GetServicePropertiesAsync(id);
            ViewData["ServiceId"] = id;
            return View(list.Data);
        }
        public IActionResult PropertyCreate(string id)
        {
            ViewData["ServiceId"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyCreate(ServicePropertyViewModel model)
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
                            FolderName = "files/service/property/"
                        });
                        if (!fileCode.IsSuccessful)
                        {
                            TempData["Warning"] = fileCode.Messages;
                            return View(model);
                        }
                        fileArray.Add(fileCode.Data);
                    }
                }
                var addServiceProperty = await _propertyService.AddServicePropertyAsync(new Domain.Entities.ServiceProperty
                {
                    ServiceId = model.ServiceProperty.ServiceId,
                    IsActive = model.ServiceProperty.IsActive,
                });
                if (addServiceProperty.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _propertyService.AddServiceFileAsync(new Domain.Entities.ServiceFile
                        {
                            FileCode = file,
                            ServicePropertyId = addServiceProperty.Data.Id,
                        });

                    }
                    foreach (var language in model.ServicePropertyLanguages)
                    {
                        language.ServicePropertyId = addServiceProperty.Data.Id;
                        await _propertyService.AddServicePropertyLanguageAsync(language);
                    }
                    TempData["Success"] = addServiceProperty.Messages;
                    return RedirectToAction("Properties", "Services", new { id = model.ServiceProperty.ServiceId });
                }
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
                var serviceProRow = await _propertyService.GetServicePropertyAsync(id);
                if (serviceProRow.IsSuccessful)
                {
                    return View(new ServicePropertyViewModel
                    {
                        ServiceProperty = serviceProRow.Data ?? null,
                        ServicePropertyLanguages = serviceProRow.Data?.ServicePropertyLanguages.OrderBy(x => x.Lang_Code).ToList(),
                    });
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyEdit(ServicePropertyViewModel model)
        {
            try
            {
                List<string> fileArray = new();
                if (model.Files != null)
                {
                    var delete = await _propertyService.GetServicePropertyAsync(model.ServiceProperty.Id.ToString());
                    if (delete.IsSuccessful)
                    {
                        foreach (var item in delete.Data.ServiceFiles)
                        {
                            await _documentService.DeleteFolderAsync(item.FileCode);

                        }
                        await _propertyService.RemoveServiceFileAsync(model.ServiceProperty.Id.ToString());
                    }

                    foreach (var item in model.Files)
                    {
                        var fileCode = await _documentService.UploadAsync(new Application.Dtos.UploadInputDto
                        {
                            File = item,
                            FolderName = "files/service/property/"
                        });
                        if (!fileCode.IsSuccessful)
                        {
                            TempData["Warning"] = fileCode.Messages;
                            return View(model);
                        }
                        fileArray.Add(fileCode.Data);
                    }
                }
                var addProduct = await _propertyService.UpdateServicePropertyAsync(model.ServiceProperty);
                if (addProduct.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _propertyService.AddServiceFileAsync(new Domain.Entities.ServiceFile
                        {
                            FileCode = file,
                            ServicePropertyId = model.ServiceProperty.Id,
                        });

                    }
                    foreach (var language in model.ServicePropertyLanguages)
                    {

                        await _propertyService.UpdateServicePropertyLanguageAsync(language);
                    }
                    TempData["Success"] = addProduct.Messages;
                    return RedirectToAction("Properties", "Services", new { id = model.ServiceProperty.ServiceId });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(model);
        }

        public async Task<IActionResult> PropertyDelete(string id)
        {
            var delete = await _propertyService.GetServicePropertyAsync(id);
            if (delete.IsSuccessful)
            {
                foreach (var item in delete.Data.ServiceFiles)
                {
                    await _documentService.DeleteFolderAsync(item.FileCode);
                }
                var result = await _propertyService.RemoveServicePropertyAsync(delete.Data);
                TempData["Success"] = result.Messages;
                return RedirectToAction("Properties", "Services", new { id = delete.Data.Id });
            }
            TempData["Error"] = "Hata";
            return RedirectToAction("Properties", "Services", new { id = delete.Data.Id });
        }
        #endregion
    }
}
