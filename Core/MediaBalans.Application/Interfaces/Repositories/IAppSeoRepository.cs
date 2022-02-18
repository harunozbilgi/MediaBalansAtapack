using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IAppSeoRepository : IGenericRepositoryAsync<AppSeo>
    {
        Task AddAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage);
        Task UpdateAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage);
    }
}
