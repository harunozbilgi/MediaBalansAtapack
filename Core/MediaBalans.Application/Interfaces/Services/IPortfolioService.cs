using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IPortfolioService
    {
        Task<Response<List<Portfolio>>> GetPortfoliosAsync();
        Task<Response<Portfolio>> GetPortfolioAsync(string portfolioId);
        Task<Response<Portfolio>> AddPortfolioAsync(Portfolio portfolio);
        Task<Response<NoContent>> UpdatePortfolioAsync(Portfolio portfolio);
        Task<Response<NoContent>> RemovePortfolioAsync(Portfolio portfolio);
        Task<Response<NoContent>> AddProductFileAsync(PortfolioFile portfolio);
        Task<Response<NoContent>> RemovePortfolioFileAsync(string portfolioId);
        Task<Response<NoContent>> AddPortfolioLangauageAsync(PortfolioLanguage portfolio);
        Task<Response<NoContent>> UpdatePortfolioLanguageAsync(PortfolioLanguage portfolio);
    }
}
