
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Repositories
{
    public interface IPortfolioRepository : IGenericRepositoryAsync<Portfolio>
    {
        Task AddPortfolioFileAsync(PortfolioFile product);
        Task DeletePortfolioFileAsync(string productId);
        Task AddPortfolioLanguageAsync(PortfolioLanguage portfolioLanguage);
        Task UpdatePortfolioLanguageAsync(PortfolioLanguage portfolioLanguage);
    }
}
