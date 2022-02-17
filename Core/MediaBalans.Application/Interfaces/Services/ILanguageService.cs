using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface ILanguageService
    {
        Task<Response<List<Language>>> GetLanguagesAsync();
        Task<Response<Language>> GetLanguageAsync(string languageId);
        Task<Response<NoContent>> AddLanguageAsync(Language language);
        Task<Response<NoContent>> RemoveLanguageAsync(Language language);
        Task<Response<NoContent>> UpdateLanguageAsync(Language language);
    }
}
