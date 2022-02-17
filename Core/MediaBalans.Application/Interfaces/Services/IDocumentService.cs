using MediaBalans.Application.Dtos;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;

namespace MediaBalans.Application.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<Response<Document>> GetDocumentByIdAsync(string DocumentUnique);
        Task<Response<List<Document>>> GetDocumentListAsync();
        Task<Response<string>> UploadAsync(UploadInputDto upload, CancellationToken cancellationToken = default);
        Task<Response<NoContent>> DeleteFolderAsync(string DocumentUnique);
    }
}
