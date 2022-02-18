using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class AppSeoRepository : GenericRepositoryAsync<AppSeo>, IAppSeoRepository
    {
        private readonly ApplicationDbContext _context;
        public AppSeoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage)
        {
            await _context.AppSeoLanguages.AddAsync(appSeoLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppSeoLanguageAsync(AppSeoLanguage appSeoLanguage)
        {
            _context.AppSeoLanguages.Update(appSeoLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
