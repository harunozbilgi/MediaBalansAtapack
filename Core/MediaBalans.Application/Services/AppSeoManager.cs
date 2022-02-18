using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class AppSeoManager : IAppSeoService
    {
        private readonly IAppSeoRepository _appSeoRepository;

        public AppSeoManager(IAppSeoRepository appSeoRepository)
        {
            _appSeoRepository = appSeoRepository;
        }

        public async Task<Response<AppSeo>> AddAppSeoAsync(AppSeo appSeo)
        {
            await _appSeoRepository.AddAsync(appSeo);
            return Response<AppSeo>.Success(appSeo, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage)
        {
            await _appSeoRepository.AddAppSeoLanguageAsync(appSeoLanguage);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<List<AppSeo>>> GetAppSeoStaticListAsync()
        {
            var result = await _appSeoRepository.GetAllAsync(x => x.IsStaticPage == true,
            x => x.OrderBy(x => x.CreateTime),
            x => x.AppSeoLanguages);
            return Response<List<AppSeo>>.Success(result.ToList());
        }

        public async Task<Response<AppSeo>> GetByAppSeoIdAsync(string seoId)
        {
            var row = await _appSeoRepository.GetAsync(x => x.Id.ToString() == seoId,
             x => x.AppSeoLanguages);
            if (row is not null)
            {
                return Response<AppSeo>.Success(row);
            }
            throw new NotFoundException();
        }

        public async Task<Response<AppSeo>> GetByPageNameAsync(string pageName)
        {
            var row = await _appSeoRepository.GetAsync(x => x.Home == pageName,
               x => x.AppSeoLanguages);
            if (row is not null)
            {
                return Response<AppSeo>.Success(row);
            }
            throw new NotFoundException();
        }

        public async Task<Response<AppSeo>> GetBySeoCodeAsync(string seoCode)
        {
            var row = await _appSeoRepository.GetAsync(x => x.AppSeoCode == seoCode && x.IsDinamicPage == true,
              x => x.AppSeoLanguages);
            return Response<AppSeo>.Success(row);
        }

        public async Task<Response<string>> PageSeoAddAsync(string seoCode, List<AppSeoLanguage> appSeoLanguages)
        {
            if (seoCode is null || string.IsNullOrEmpty(seoCode))
            {
                string code = GetId();
                var result = await AddAppSeoAsync(new AppSeo
                {
                    AppSeoCode = code,
                    IsDinamicPage = true,
                    IsStaticPage = false,
                });
                if (result.IsSuccessful)
                {
                    foreach (var item in appSeoLanguages)
                    {
                        item.AppSeoId = result.Data.Id;
                        await AddAppSeoLanguageAsync(item);
                    }
                }
                return Response<string>.Success(code);
            }
            foreach (var item in appSeoLanguages)
            {
                await UpdateAppSeoLanguageAsync(item);
            }
            return Response<string>.Success(seoCode);
        }

        public async Task<Response<NoContent>> RemoveAppSeoAsync(AppSeo appSeo)
        {
            await _appSeoRepository.RemoveAsync(appSeo);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateAppSeoAsync(AppSeo appSeo)
        {
            await _appSeoRepository.UpdateAsync(appSeo);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage)
        {
            await _appSeoRepository.UpdateAppSeoLanguageAsync(appSeoLanguage);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
        private static string GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "")[..10];
        }
    }
}
