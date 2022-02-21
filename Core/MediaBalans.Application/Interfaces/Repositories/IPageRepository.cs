using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IPageRepository : IGenericRepositoryAsync<Page>
    {
        Task AddPageLanguageAsync(PageLanguage pageLanguage);
        Task UpdatePageLanguageAsync(PageLanguage pageLanguage);
    }
}
