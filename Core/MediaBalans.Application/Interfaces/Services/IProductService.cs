
using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response<List<Product>>> GetProductsAsync();
        Task<Response<Product>> GetProductCategoriesAsync(string categoryId);
        Task<Response<Product>> GetProductAsync(string productId);
        Task<Response<Product>> GetProductSlugUrl(string slugUrl);
        Task<Response<Product>> AddProdcutAsync(Product product);
        Task<Response<NoContent>> UpdateProdcutAsync(Product product);
        Task<Response<NoContent>> RemoveProdcutAsync(Product product);
        Task<Response<NoContent>> AddProductFileAsync(ProductFile product);
        Task<Response<NoContent>> RemoveProductFileAsync(string productId);
        Task<Response<NoContent>> AddProductLangauageAsync(ProductLanguage product);
        Task<Response<NoContent>> UpdateProdcutLanguageAsync(ProductLanguage product); 

    }
}
