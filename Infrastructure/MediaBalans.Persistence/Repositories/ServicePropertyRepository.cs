using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediaBalans.Persistence.Repositories
{
    public class ServicePropertyRepository : GenericRepositoryAsync<ServiceProperty>, IServicePropertyRepository
    {
        private readonly ApplicationDbContext _context;
        public ServicePropertyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddServiceFileAsync(ServiceFile service)
        {
            await _context.ServiceFiles.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task AddServicePropertyLanguageAsync(ServicePropertyLanguage propertyLanguage)
        {
            await _context.ServicePropertyLanguages.AddAsync(propertyLanguage);
            await _context.SaveChangesAsync();;
        }

        public async Task DeleteServiceFileAsync(string serviceId)
        {
            var serviceFile = await _context.ServiceFiles.Where(x => x.ServicePropertyId.ToString() == serviceId).ToListAsync();
            _context.ServiceFiles.RemoveRange(serviceFile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServicePropertyLanguageAsync(ServicePropertyLanguage propertyLanguage)
        {
            _context.ServicePropertyLanguages.Update(propertyLanguage);
            await _context.SaveChangesAsync();

        }
    }
}
