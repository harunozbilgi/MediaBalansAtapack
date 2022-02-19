
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IServicePropertyRepository : IGenericRepositoryAsync<ServiceProperty>
    {
        Task AddServiceFileAsync(ServiceFile service);
        Task DeleteServiceFileAsync(string serviceId);
        Task AddServicePropertyLanguageAsync(ServicePropertyLanguage propertyLanguage);
        Task UpdateServicePropertyLanguageAsync(ServicePropertyLanguage propertyLanguage);
    }
}
