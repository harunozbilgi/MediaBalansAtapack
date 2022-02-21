using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Services
{
    public class GalleryManager : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryManager(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<Response<NoContent>> AddGalleryAsync(Gallery gallery)
        {
            await _galleryRepository.AddAsync(gallery);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<List<Gallery>>> GetGalleriesAsync()
        {
            var result = await _galleryRepository.GetAllAsync();
            return Response<List<Gallery>>.Success(result.ToList());
        }

        public async Task<Response<Gallery>> GetGalleryAsync(string galleryId)
        {
            var result = await _galleryRepository.GetAsync(x => x.Id.ToString() == galleryId);
            if (result is not null)
            {
                return Response<Gallery>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveGalleryAsync(Gallery gallery)
        {
           await _galleryRepository.RemoveAsync(gallery);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateGalleryAsync(Gallery gallery)
        {
            await _galleryRepository.UpdateAsync(gallery);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
