using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediaBalans.Persistence.Repositories
{
    public class PortfolioRepository : GenericRepositoryAsync<Portfolio>, IPortfolioRepository 
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddPortfolioFileAsync(PortfolioFile product)
        {
            await _context.PortfolioFiles.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddPortfolioLanguageAsync(PortfolioLanguage portfolioLanguage)
        {
            await _context.PortfolioLanguages.AddAsync(portfolioLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePortfolioFileAsync(string productId)
        {
            var product = await _context.PortfolioFiles.Where(x => x.PortfolioId.ToString() == productId).ToListAsync();
            _context.PortfolioFiles.RemoveRange(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePortfolioLanguageAsync(PortfolioLanguage portfolioLanguage)
        {
            _context.PortfolioLanguages.Update(portfolioLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
