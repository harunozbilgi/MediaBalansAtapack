using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface ISliderRepository : IGenericRepositoryAsync<Slider>
    {
        Task AddSliderLanguageAsync(SliderLanguage sliderLanguage);
        Task UpdateSliderLanguageAsync(SliderLanguage sliderLanguage);
    }
}
