using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class PageManager : IPageService
    {
        private readonly IPageRepository _pageRepository;
        public PageManager(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<Response<Page>> AddPageAsync(Page page)
        {
            await _pageRepository.AddAsync(page);
            return Response<Page>.Success(page, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddPageLanguageAsync(PageLanguage page)
        {
            await _pageRepository.AddPageLanguageAsync(page);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<Page>> GetPageAsync(string pageId)
        {
            var result = await _pageRepository.GetAsync(x => x.Id.ToString() == pageId, x => x.PageLanguages);
            if (result is not null)
            {
                return Response<Page>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<Page>> GetPageHomeAsync()
        {
            var result = await _pageRepository.GetAsync(x => x.IsHome, x => x.PageLanguages);
            if (result is not null)
            {
                return Response<Page>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<List<Page>>> GetPagesAsync()
        {
            var result = await _pageRepository.GetAllAsync(x => x.OrderBy(x => x.OrderBy), x => x.PageLanguages, x => x.PageLanguages);
            return Response<List<Page>>.Success(result.ToList());
        }

        public async Task<Response<Page>> GetPageSlugUrlAsync(string slugUrl)
        {
            var result = await _pageRepository.GetAsync(x => x.SlugUrl == slugUrl, x => x.PageLanguages, x => x.PageProperties);
            if (result is not null)
            {
                return Response<Page>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemovePageAsync(Page page)
        {
            await _pageRepository.RemoveAsync(page);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdatePageAsync(Page page)
        {
            await _pageRepository.UpdateAsync(page);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdatePageLanguageAsync(PageLanguage page)
        {
            await _pageRepository.UpdatePageLanguageAsync(page);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
