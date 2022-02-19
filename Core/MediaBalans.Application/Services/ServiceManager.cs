using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class ServiceManager : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceManager(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Response<Service>> AddServiceAsync(Service service)
        {
            await _serviceRepository.AddAsync(service);
            return Response<Service>.Success(service, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddServiceLanguageAsync(ServiceLanguage service)
        {
            await _serviceRepository.AddServiceLanguageAsync(service);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<Service>> GetServiceAsync(string serviceId)
        {
            var result = await _serviceRepository.GetAsync(x => x.Id.ToString() == serviceId,
                x => x.ServiceLanguages);

            if (result is not null)
            {
                return Response<Service>.Success(result);
            }
            throw new NotFoundException();
        }

        public async  Task<Response<List<Service>>> GetServicesAsync()
        {
            var result = await _serviceRepository.GetAllAsync(x => x.OrderBy(x => x.OrderBy), x => x.ServiceLanguages);
            return Response<List<Service>>.Success(result.ToList());
        }

        public async Task<Response<Service>> GetServiceSlugUrlAsync(string slugUrl)
        {
            var result = await _serviceRepository.GetAsync(x => x.SlugUrl == slugUrl,
              x => x.ServiceLanguages);

            if (result is not null)
            {
                return Response<Service>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveServiceAsync(Service service)
        {
            await _serviceRepository.RemoveAsync(service);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateServiceAsync(Service service)
        {
            await _serviceRepository.UpdateAsync(service);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateServiceLanguageAsync(ServiceLanguage service)
        {
            await _serviceRepository.UpdateServiceLanguageAsync(service);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
