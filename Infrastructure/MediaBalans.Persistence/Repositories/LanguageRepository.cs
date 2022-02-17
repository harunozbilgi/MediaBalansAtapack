using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class LanguageRepository : GenericRepositoryAsync<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
