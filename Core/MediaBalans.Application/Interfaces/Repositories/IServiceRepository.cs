
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IServiceRepository : IGenericRepositoryAsync<Service>
    {
        Task AddServiceLanguageAsync(ServiceLanguage serviceLanguage);
        Task UpdateServiceLanguageAsync(ServiceLanguage serviceLanguage);
    }
}
