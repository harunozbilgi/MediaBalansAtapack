using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class SliderRepository : GenericRepositoryAsync<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;
        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddSliderLanguageAsync(SliderLanguage sliderLanguage)
        {
            await _context.SliderLanguages.AddAsync(sliderLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSliderLanguageAsync(SliderLanguage sliderLanguage)
        {
            _context.SliderLanguages.Update(sliderLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
