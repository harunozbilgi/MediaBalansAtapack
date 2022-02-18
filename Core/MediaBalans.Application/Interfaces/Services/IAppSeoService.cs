using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IAppSeoService
    {
        Task<Response<AppSeo>> GetByAppSeoIdAsync(string seoId);
        Task<Response<AppSeo>> GetBySeoCodeAsync(string seoCode);
        Task<Response<AppSeo>> GetByPageNameAsync(string pageName);
        Task<Response<List<AppSeo>>> GetAppSeoStaticListAsync();
        Task<Response<AppSeo>> AddAppSeoAsync(AppSeo appSeo);
        Task<Response<NoContent>> UpdateAppSeoAsync(AppSeo appSeo);
        Task<Response<NoContent>> RemoveAppSeoAsync(AppSeo appSeo);
        Task<Response<NoContent>> AddAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage);
        Task<Response<NoContent>> UpdateAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage);
        Task<Response<string>> PageSeoAddAsync(string seoCode, List<AppSeoLanguage> appSeoLanguages);
    }
}
