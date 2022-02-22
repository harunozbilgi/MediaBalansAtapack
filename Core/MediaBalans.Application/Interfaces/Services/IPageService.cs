using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IPageService
    {

        Task<Response<List<Page>>> GetPagesAsync();
        Task<Response<Page>> GetPageAsync(string pageId);
        Task<Response<Page>> GetPageHomeAsync();
        Task<Response<Page>> GetPageSlugUrlAsync(string slugUrl);
        Task<Response<Page>> AddPageAsync(Page page);
        Task<Response<NoContent>> UpdatePageAsync(Page page);
        Task<Response<NoContent>> RemovePageAsync(Page page);
        Task<Response<NoContent>> AddPageLanguageAsync(PageLanguage page);
        Task<Response<NoContent>> UpdatePageLanguageAsync(PageLanguage page);
    }
}
