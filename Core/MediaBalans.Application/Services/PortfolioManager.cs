using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class PortfolioManager : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioManager(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<Response<Portfolio>> AddPortfolioAsync(Portfolio portfolio)
        {
            await _portfolioRepository.AddAsync(portfolio);
            return Response<Portfolio>.Success(portfolio, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddPortfolioLangauageAsync(PortfolioLanguage portfolio)
        {
            await _portfolioRepository.AddPortfolioLanguageAsync(portfolio);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> AddProductFileAsync(PortfolioFile portfolio)
        {
            await _portfolioRepository.AddPortfolioFileAsync(portfolio);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<Portfolio>> GetPortfolioAsync(string portfolioId)
        {
            var result = await _portfolioRepository.GetAsync(x => x.Id.ToString() == portfolioId,
                 x => x.PortfolioFiles,
                 x => x.PortfolioLanguages);

            if (result is not null)
            {
                return Response<Portfolio>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<List<Portfolio>>> GetPortfoliosAsync()
        {
            var result = await _portfolioRepository.GetAllAsync(x => x.OrderByDescending(x => x.CreateTime), x => x.PortfolioFiles,
             x => x.PortfolioLanguages);
            return Response<List<Portfolio>>.Success(result.ToList()); ;
        }

        public async Task<Response<NoContent>> RemovePortfolioAsync(Portfolio portfolio)
        {
            await _portfolioRepository.RemoveAsync(portfolio);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> RemovePortfolioFileAsync(string portfolioId)
        {
            await _portfolioRepository.DeletePortfolioFileAsync(portfolioId);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdatePortfolioAsync(Portfolio portfolio)
        {
            await _portfolioRepository.UpdateAsync(portfolio);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdatePortfolioLanguageAsync(PortfolioLanguage portfolio)
        {
            await _portfolioRepository.UpdatePortfolioLanguageAsync(portfolio);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
