
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface INewsRepository : IGenericRepositoryAsync<News>
    {
        Task AddNewsLanguageAsync(NewsLanguage newsLanguage);
        Task UpdateNewsLanguageAsync(NewsLanguage newsLanguage);
    }
}
