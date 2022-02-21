using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IAppConfigService
    {
        Task<Response<AppConfig>> GetByAppConfigIdAsync(string AppConfigId);
        Task<Response<AppConfig>> GetByAppConfigAsync();
        Task<Response<NoContent>> AddAsync(AppConfig appConfig);
        Task<Response<NoContent>> UpdateAsync(AppConfig appConfig);
    }
}
