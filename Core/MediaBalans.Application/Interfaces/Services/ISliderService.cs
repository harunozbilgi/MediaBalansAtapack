using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface ISliderService
    {
        Task<Response<List<Slider>>> GetSlidersAsync();
        Task<Response<Slider>> GetSliderAsync(string sliderId);
        Task<Response<Slider>> AddSliderAsync(Slider slider);
        Task<Response<NoContent>> UpdateSliderAsync(Slider slider);
        Task<Response<NoContent>> RemoveSliderAsync(Slider slider);
        Task<Response<NoContent>> AddSliderLanguageAsync(SliderLanguage slider);
        Task<Response<NoContent>> UpdateSliderLanguageAsync(SliderLanguage slider);
    }
}
