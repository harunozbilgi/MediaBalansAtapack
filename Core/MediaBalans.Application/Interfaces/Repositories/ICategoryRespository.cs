using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface ICategoryRespository : IGenericRepositoryAsync<Category>
    {
        Task AddCategoryLanguageAsync(CategoryLanguage categoryLanguage);
        Task UpdateCategoryLanguageAsync(CategoryLanguage categoryLanguage);
    }
}
