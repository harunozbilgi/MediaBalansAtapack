
using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IServicesService
    {
        Task<Response<List<Service>>> GetServicesAsync();
        Task<Response<Service>> GetServiceAsync(string serviceId);
        Task<Response<Service>> GetServiceSlugUrlAsync(string slugUrl);
        Task<Response<Service>> AddServiceAsync(Service service);
        Task<Response<NoContent>> UpdateServiceAsync(Service service);
        Task<Response<NoContent>> RemoveServiceAsync(Service service);
        Task<Response<NoContent>> AddServiceLanguageAsync(ServiceLanguage service);
        Task<Response<NoContent>> UpdateServiceLanguageAsync(ServiceLanguage service);
    }
}
