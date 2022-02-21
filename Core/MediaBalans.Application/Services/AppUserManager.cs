using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MediaBalans.Application.Services
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserRepository _appUserDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUserManager(IAppUserRepository appUserDal, IHttpContextAccessor httpContextAccessor)
        {
            _appUserDal = appUserDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<AppUser>> SignInAsync(AppUser appUser)
        {
            var row = await _appUserDal.GetAsync(x => x.Email == appUser.Email.Trim() && x.Password == appUser.Password.Trim());
            if (row != null)
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, row.FullName),
                    new Claim(ClaimTypes.Email, row.Email),
                    new Claim(ClaimTypes.Role, row.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                bool isAuthenticated = true;
                var principal = new ClaimsPrincipal(identity);
                if (isAuthenticated)
                {
                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddDays(1),
                        IsPersistent = true,
                        AllowRefresh = false
                    });
                    return Response<AppUser>.Success(row, "Login işlemi başarılı", 200);
                }
            }
            return Response<AppUser>.Error("Email veya şifre hatalı", 404);
        }
    }
}
