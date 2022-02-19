using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class NewsRepository : GenericRepositoryAsync<News>, INewsRepository
    {
        private readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task AddNewsLanguageAsync(NewsLanguage newsLanguage)
        {
            await _context.NewsLanguages.AddAsync(newsLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNewsLanguageAsync(NewsLanguage newsLanguage)
        {
             _context.NewsLanguages.Update(newsLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
