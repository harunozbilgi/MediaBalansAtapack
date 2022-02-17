using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class DocumentRepository : GenericRepositoryAsync<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
