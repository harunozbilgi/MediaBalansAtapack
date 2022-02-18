using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderManager(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task<Response<Slider>> AddSliderAsync(Slider slider)
        {
            await _sliderRepository.AddAsync(slider);
            return Response<Slider>.Success(slider, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddSliderLanguageAsync(SliderLanguage slider)
        {
            await _sliderRepository.AddSliderLanguageAsync(slider);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<Slider>> GetSliderAsync(string sliderId)
        {
            var result = await _sliderRepository.GetAsync(x => x.Id.ToString() == sliderId, x => x.SliderLanguages);
            if (result is not null)
            {
                return Response<Slider>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<List<Slider>>> GetSlidersAsync()
        {
            var result = await _sliderRepository.GetAllAsync(x => x.OrderBy(x => x.CreateTime), x => x.SliderLanguages);
            return Response<List<Slider>>.Success(result.ToList());
        }

        public async Task<Response<NoContent>> RemoveSliderAsync(Slider slider)
        {
            await _sliderRepository.RemoveAsync(slider);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateSliderAsync(Slider slider)
        {
            await _sliderRepository.UpdateAsync(slider);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateSliderLanguageAsync(SliderLanguage slider)
        {
            await _sliderRepository.UpdateSliderLanguageAsync(slider);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
