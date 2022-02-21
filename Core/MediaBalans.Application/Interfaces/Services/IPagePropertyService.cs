using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IPagePropertyService
    {
        Task<Response<List<PageProperty>>> GetPagePropertiesAsync(string pageId);
        Task<Response<PageProperty>> GetPagePropertyAsync(string pageId);
        Task<Response<NoContent>> AddPagePropertyAsync(PageProperty page);
        Task<Response<NoContent>> UpdatePagePropertyAsync(PageProperty page);
        Task<Response<NoContent>> RemovePagePropertyAsync(PageProperty page);
    }
}
