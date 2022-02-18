using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Atapack.WebApp.Areas.Cms.Models;
using MediaBalans.Atapack.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDocumentService _documentService; 
        private readonly IAppSeoService _appSeoService;

        public ProductsController(IProductService productService, 
            ICategoryService categoryService, 
            IDocumentService documentService, 
            IAppSeoService appSeoService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _documentService = documentService;
            _appSeoService = appSeoService;
        }

        public async Task<IActionResult> Index()
        {
            var result=await _productService.GetProductsAsync();
            return View(result.Data);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(new ProductViewModel
            {
                Categories = categories.Data
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                List<string> fileArray = new();
                if(model.Files != null)
                {
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
                else
                {
                    TempData["Error"] = "Lütfen bu alanı boş geçmeyiniz.";
                    return View(model);
                }
                var slugurl = UrlSeoHelper.UrlSeo(model.ProductLanguages[0].Title);
                var seoCode = await _appSeoService.PageSeoAddAsync(model.Product.AppSeoCode, model.AppSeoLanguages);

                var addProduct = await _productService.AddProdcutAsync(new Domain.Entities.Product
                {
                    CategoryId = model.Product.CategoryId,
                    AppSeoCode = seoCode.Data,
                    SlugUrl = slugurl,
                });
                if (addProduct.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _productService.AddProductFileAsync(new Domain.Entities.ProductFile
                        {
                            FileCode = file,
                            ProductId = addProduct.Data.Id,
                        });

                    }
                    foreach (var language in model.ProductLanguages)
                    {
                        language.ProductId = addProduct.Data.Id;
                        await _productService.AddProductLangauageAsync(language);
                    }
                    TempData["Success"] = addProduct.Messages;
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
                var productRow = await _productService.GetProductAsync(id);
                var categories = await _categoryService.GetCategoriesAsync();
                if (productRow.IsSuccessful)
                {
                    var appSeo = await _appSeoService.GetBySeoCodeAsync(productRow.Data.AppSeoCode);
                    return View(new ProductViewModel
                    {
                        Product = productRow.Data ?? null,
                        ProductLanguages = productRow.Data?.ProductLanguages.OrderBy(x => x.Lang_Code).ToList(),
                        Categories=categories.Data,
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
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            try
            {
                List<string> fileArray = new();
                if (model.Files !=null)
                {
                    var delete = await _productService.GetProductAsync(model.Product.Id.ToString());
                    if (delete.IsSuccessful)
                    {
                        foreach (var item in delete.Data.ProductFiles)
                        {
                            await _documentService.DeleteFolderAsync(item.FileCode);
                           
                        }
                        await _productService.RemoveProductFileAsync(model.Product.Id.ToString());
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
                model.Product.SlugUrl = UrlSeoHelper.UrlSeo(model.ProductLanguages[0].Title);
                model.Product.AppSeoCode = (await _appSeoService.PageSeoAddAsync(model.Product.AppSeoCode, model.AppSeoLanguages)).Data;

                var addProduct = await _productService.UpdateProdcutAsync(model.Product);
                if (addProduct.IsSuccessful)
                {
                    foreach (var file in fileArray)
                    {
                        await _productService.AddProductFileAsync(new Domain.Entities.ProductFile
                        {
                            FileCode = file,
                            ProductId = model.Product.Id,
                        });

                    }
                    foreach (var language in model.ProductLanguages)
                    {
   
                        await _productService.UpdateProdcutLanguageAsync(language);
                    }
                    TempData["Success"] = addProduct.Messages;
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
            var delete = await _productService.GetProductAsync(id);
            if (delete.IsSuccessful)
            {
                foreach (var item in delete.Data.ProductFiles)
                {
                    await _documentService.DeleteFolderAsync(item.FileCode);
                }
                var result = await _productService.RemoveProdcutAsync(delete.Data);
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
                    var orderUpdate = await _productService.GetProductAsync(item.Id);
                    orderUpdate.Data.OrderBy = item.Place;
                    await _productService.UpdateProdcutAsync(orderUpdate.Data);
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
    }
}
