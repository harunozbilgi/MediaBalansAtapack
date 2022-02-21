using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class AppConfigRepository : GenericRepositoryAsync<AppConfig>, IAppConfigRepository
    {
        public AppConfigRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
