using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class ServicePropertyManager : IServicePropertyService
    {
        private readonly IServicePropertyRepository _servicePropertyRepository;

        public ServicePropertyManager(IServicePropertyRepository servicePropertyRepository) => _servicePropertyRepository = servicePropertyRepository;

        public async Task<Response<NoContent>> AddServiceFileAsync(ServiceFile serviceFile)
        {
            await _servicePropertyRepository.AddServiceFileAsync(serviceFile);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<ServiceProperty>> AddServicePropertyAsync(ServiceProperty service)
        {
            await _servicePropertyRepository.AddAsync(service);
            return Response<ServiceProperty>.Success(service, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddServicePropertyLanguageAsync(ServicePropertyLanguage service)
        {
            await _servicePropertyRepository.AddServicePropertyLanguageAsync(service);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<List<ServiceProperty>>> GetServicePropertiesAsync(string serviceId)
        {
            var result = await _servicePropertyRepository.GetAllAsync(x => x.ServiceId.ToString() == serviceId,
                x => x.OrderByDescending(o => o.CreateTime),
                x => x.ServiceFiles,
                x => x.ServicePropertyLanguages);

            return Response<List<ServiceProperty>>.Success(result.ToList());
        }

        public async Task<Response<ServiceProperty>> GetServicePropertyAsync(string serviceId)
        {
            var result = await _servicePropertyRepository.GetAsync(x => x.Id.ToString() == serviceId,
              x => x.ServiceFiles,
              x => x.ServicePropertyLanguages);

            if (result is not null)
            {
                return Response<ServiceProperty>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveServiceFileAsync(string serviceId)
        {
            await _servicePropertyRepository.DeleteServiceFileAsync(serviceId);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> RemoveServicePropertyAsync(ServiceProperty service)
        {
            await _servicePropertyRepository.RemoveAsync(service);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateServicePropertyAsync(ServiceProperty service)
        {
            await _servicePropertyRepository.UpdateAsync(service);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateServicePropertyLanguageAsync(ServicePropertyLanguage service)
        {
            await _servicePropertyRepository.UpdateServicePropertyLanguageAsync(service);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
