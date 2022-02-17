using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class LanguageRespository : GenericRepositoryAsync<Language>, ILanguageRespository
    {
        public LanguageRespository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
