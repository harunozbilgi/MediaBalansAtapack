using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class ServiceRepository : GenericRepositoryAsync<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddServiceLanguageAsync(ServiceLanguage serviceLanguage)
        {
            await _context.ServiceLanguages.AddAsync(serviceLanguage);  
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServiceLanguageAsync(ServiceLanguage serviceLanguage)
        {
            _context.ServiceLanguages.Update(serviceLanguage);
            await _context.SaveChangesAsync();  
        }
    }
}
