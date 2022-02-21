using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IGalleryService
    {
        Task<Response<List<Gallery>>> GetGalleriesAsync();
        Task<Response<Gallery>> GetGalleryAsync(string galleryId);
        Task<Response<NoContent>> AddGalleryAsync(Gallery gallery);
        Task<Response<NoContent>> UpdateGalleryAsync(Gallery gallery);
        Task<Response<NoContent>> RemoveGalleryAsync(Gallery gallery);
    }
}
