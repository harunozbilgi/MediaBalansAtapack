using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Services
{
    public class PagePropertyManager : IPagePropertyService
    {
        private readonly IPagePropertyRepository _pagePropertyRepository;

        public PagePropertyManager(IPagePropertyRepository pagePropertyRepository)
        {
            _pagePropertyRepository = pagePropertyRepository;
        }

        public async Task<Response<NoContent>> AddPagePropertyAsync(PageProperty page)
        {
            await _pagePropertyRepository.AddAsync(page);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);

        }

        public async  Task<Response<List<PageProperty>>> GetPagePropertiesAsync(string pageId)
        {
            var result = await _pagePropertyRepository.GetAllAsync(x => x.PageId.ToString() == pageId);
            return Response<List<PageProperty>>.Success(result.ToList());
        }

        public async Task<Response<PageProperty>> GetPagePropertyAsync(string pageId)
        {
            var result = await _pagePropertyRepository.GetAsync(x => x.Id.ToString() == pageId);
            if (result is not null)
            {
                return Response<PageProperty>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemovePagePropertyAsync(PageProperty page)
        {
            await _pagePropertyRepository.RemoveAsync(page);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdatePagePropertyAsync(PageProperty page)
        {
            await _pagePropertyRepository.UpdateAsync(page);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
