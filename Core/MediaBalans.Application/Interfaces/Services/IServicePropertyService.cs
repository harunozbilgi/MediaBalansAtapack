
using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IServicePropertyService
    {
        Task<Response<List<ServiceProperty>>> GetServicePropertiesAsync(string serviceId);
        Task<Response<ServiceProperty>> GetServicePropertyAsync(string serviceId);
        Task<Response<ServiceProperty>> AddServicePropertyAsync(ServiceProperty service);
        Task<Response<NoContent>> UpdateServicePropertyAsync(ServiceProperty service);
        Task<Response<NoContent>> RemoveServicePropertyAsync(ServiceProperty service);
        Task<Response<NoContent>> AddServiceFileAsync(ServiceFile serviceFile);
        Task<Response<NoContent>> RemoveServiceFileAsync(string serviceId);
        Task<Response<NoContent>> AddServicePropertyLanguageAsync(ServicePropertyLanguage service);
        Task<Response<NoContent>> UpdateServicePropertyLanguageAsync(ServicePropertyLanguage service); 

    }
}
