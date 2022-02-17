using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Services
{
    public class LanguageManager : ILanguageService
    {
        private readonly ILanguageRespository _languageRepositroy;

        public LanguageManager(ILanguageRespository languageRepositroy) => _languageRepositroy = languageRepositroy;

        public async Task<Response<NoContent>> AddLanguageAsync(Language language)
        {
            await _languageRepositroy.AddAsync(language);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<List<Language>>> GetLanguagesAsync()
        {
            var list = await _languageRepositroy.GetAllAsync(x => x.OrderBy(x => x.FullName));
            return Response<List<Language>>.Success(list.ToList());
        }

        public async Task<Response<Language>> GetLanguageAsync(string languageId)
        {
            var result = await _languageRepositroy.GetAsync(x => x.Id.ToString() == languageId);
            if (result is not null)
            {
                return Response<Language>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveLanguageAsync(Language language)
        {
            await _languageRepositroy.RemoveAsync(language);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateLanguageAsync(Language language)
        {
            await _languageRepositroy.UpdateAsync(language);
            return Response<NoContent>.Success("Güncelleme işlemi başarılı", 204);
        }
    }
}
