using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepositoryAsync<Product>
    {
        Task AddProductFileAsync(ProductFile product);
        Task DeleteProductFileAsync(string productId);
        Task AddProductLanguageAsync(ProductLanguage productLanguage);
        Task UpdateProductLanguageAsync(ProductLanguage productLanguage);
    }
}
