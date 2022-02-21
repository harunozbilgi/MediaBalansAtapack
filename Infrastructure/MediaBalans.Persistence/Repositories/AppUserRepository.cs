using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class AppUserRepository : GenericRepositoryAsync<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
