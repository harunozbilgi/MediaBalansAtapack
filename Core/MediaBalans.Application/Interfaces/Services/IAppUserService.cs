using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<Response<AppUser>> SignInAsync(AppUser appUser);
    }
}
