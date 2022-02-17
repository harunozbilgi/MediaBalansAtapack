using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Response<List<Category>>> GetCategoriesAsync();
        Task<Response<Category>> GetCategoryAsync(string categoryId);
        Task<Response<Category>> AddCategoryAsync(Category category);
        Task<Response<NoContent>> UpdateCategoryAsync(Category category);
        Task<Response<NoContent>> RemoveCategoryAsync(Category category);
        Task<Response<NoContent>> AddCategoryLanguageAsync(CategoryLanguage category);
        Task<Response<NoContent>> UpdateCategoryLanguageAsync(CategoryLanguage category);
    }
}
