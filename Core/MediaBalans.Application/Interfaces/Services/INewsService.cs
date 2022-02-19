using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;


namespace MediaBalans.Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<Response<List<News>>> GetNewsAsync();
        Task<Response<News>> GetNewAsync(string newsId);
        Task<Response<News>> GetNewSlugUrlAsync(string slugUrl);
        Task<Response<News>> AddNewsAsync(News news);
        Task<Response<NoContent>> UpdateNewsAsync(News news);
        Task<Response<NoContent>> RemoveNewsAsync(News news);
        Task<Response<NoContent>> AddNewsLanguageAsync(NewsLanguage news);
        Task<Response<NoContent>> UpdateNewsLanguageAsync(NewsLanguage news);
    }
}
