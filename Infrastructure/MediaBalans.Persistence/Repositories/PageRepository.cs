using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class PageRepository : GenericRepositoryAsync<Page>,IPageRepository
    {
        private readonly ApplicationDbContext _context;
        public PageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddPageLanguageAsync(PageLanguage pageLanguage)
        {
            await _context.PageLanguages.AddAsync(pageLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePageLanguageAsync(PageLanguage pageLanguage)
        {
            _context.PageLanguages.Update(pageLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
