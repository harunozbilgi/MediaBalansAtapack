using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class NewsManager : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsManager(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<Response<News>> AddNewsAsync(News news)
        {
             await _newsRepository.AddAsync(news);
            return Response<News>.Success(news, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddNewsLanguageAsync(NewsLanguage news)
        {
            await _newsRepository.AddNewsLanguageAsync(news);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<News>> GetNewAsync(string newsId)
        {
            var result = await _newsRepository.GetAsync(x => x.Id.ToString() == newsId,
                x => x.NewsLanguages);

            if (result is not null)
            {
                return Response<News>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<List<News>>> GetNewsAsync()
        {
            var result = await _newsRepository.GetAllAsync(x => x.OrderByDescending(x => x.CreateTime), x => x.NewsLanguages);
            return Response<List<News>>.Success(result.ToList());
        }

        public async Task<Response<News>> GetNewSlugUrlAsync(string slugUrl)
        {
            var result = await _newsRepository.GetAsync(x => x.SlugUrl== slugUrl,
               x => x.NewsLanguages);

            if (result is not null)
            {
                return Response<News>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveNewsAsync(News news)
        {
            await _newsRepository.RemoveAsync(news);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateNewsAsync(News news)
        {
            await _newsRepository.UpdateAsync(news);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateNewsLanguageAsync(NewsLanguage news)
        {
            await _newsRepository.UpdateNewsLanguageAsync(news);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
