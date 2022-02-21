using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBalans.Application.Services
{
    public class AppConfigManager : IAppConfigService
    {
        private readonly IAppConfigRepository _appConfigDal;
        public AppConfigManager(IAppConfigRepository appConfigDal)
        {
            _appConfigDal = appConfigDal;
        }
        public async Task<Response<NoContent>> AddAsync(AppConfig appConfig)
        {
            await _appConfigDal.AddAsync(appConfig);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<AppConfig>> GetByAppConfigAsync()
        {
            var row = await _appConfigDal.GetAllAsync(x => x.OrderByDescending(x => x.CreateTime));
            return Response<AppConfig>.Success(row.FirstOrDefault());
        }

        public async Task<Response<AppConfig>> GetByAppConfigIdAsync(string AppConfigId)
        {
            var row = await _appConfigDal.GetAsync(x => x.Id.ToString() == AppConfigId);
            if (row is not null)
            {
                return Response<AppConfig>.Success(row);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> UpdateAsync(AppConfig appConfig)
        {
            await _appConfigDal.UpdateAsync(appConfig);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
