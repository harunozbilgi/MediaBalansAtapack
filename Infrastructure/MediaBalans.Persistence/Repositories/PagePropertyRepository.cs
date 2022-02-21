using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class PagePropertyRepository : GenericRepositoryAsync<PageProperty>, IPagePropertyRepository
    {
        public PagePropertyRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
