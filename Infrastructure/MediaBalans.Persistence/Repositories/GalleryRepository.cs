using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class GalleryRepository : GenericRepositoryAsync<Gallery>, IGalleryRepository
    {
        public GalleryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
