using MediaBalans.Application.Dtos;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Settings;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;



namespace MediaBalans.Application.Services
{
    public class DocumentManager : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentSetting  _documentSetting;

        public DocumentManager(IDocumentRepository documentRepository, IOptions<DocumentSetting> documentSetting)
        {
            _documentRepository = documentRepository;
            _documentSetting = documentSetting.Value;
        }

        public async Task<Response<NoContent>> DeleteFolderAsync(string DocumentUnique)
        {
            var documnetRow = await _documentRepository.GetAsync(x => x.DocumentUnique == DocumentUnique);
            if (documnetRow is not null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + string.Concat(documnetRow.DocumentFolderName, documnetRow.DocumentName));
                if (File.Exists(path) == true)
                {
                    File.Delete(path);
                }
                await _documentRepository.RemoveAsync(documnetRow);
                return Response<NoContent>.Success("Silme işlemi başarılı", 204);
            }
            return Response<NoContent>.Error("Kayıt Bulunamadı", 404);
        }

        public async Task<Response<Document>> GetDocumentByIdAsync(string DocumentUnique)
        {
            var result=await _documentRepository.GetAsync(x=>x.DocumentUnique == DocumentUnique);
            if(result is not null)
            {
                return Response<Document>.Success(result);
            }
            return Response<Document>.Error("Kayıt Bulunamadı", 404);
        }

        public async Task<Response<List<Document>>> GetDocumentListAsync()
        {
            var result = await _documentRepository.GetAllAsync();
            return Response<List<Document>>.Success(result.ToList());
        }

        public async Task<Response<string>> UploadAsync(UploadInputDto upload, CancellationToken cancellationToken = default)
        {
            try
            {
                string guidId = GetGuId();
                if (upload.File is not null)
                {
                    if (!IsFolderValid(upload.File))
                    {
                        return Response<string>.Error("Lütfen 3 mb yukarı resim yüklemeyiniz.", 404);
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + upload.FolderName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string filename = string.Concat(guidId, Path.GetExtension(upload.File.FileName));
                    using var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create);
                    await upload.File.CopyToAsync(fileStream, cancellationToken);
                    await _documentRepository.AddAsync(new Document()
                    {
                        DocumentUnique = guidId,
                        DocumentName = filename,
                        DocumentFolderName = upload.FolderName,
                        DocumentType = Path.GetExtension(upload.File.FileName),
                        DocumentSize = BytesToString(upload.File.Length),
                        Storage_Url = _documentSetting.StorageUrl,
                        Image_Url = upload.ImageUrl,
                        Video_Url = upload.VideoUrl,
                    });
                    return Response<string>.Success(guidId, "Ekleme işlemi başarılı", 202);
                }
                return Response<string>.Error("Lütfen bu alanı boş geçmeyiniz.", 404);
            }
            catch (Exception ex)
            {

                return Response<string>.Error($"Beklenmedik hata {ex.Message}", 404);
            }
         
        }

        #region Static>Alanlar
        private static string GetGuId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }
        private static bool IsFolderValid(IFormFile file)
        {
            return file.Length <= 3 * 1024 * 1024;
        }
        private static string BytesToString(long byteCount)
        {
            string[] suf = { "Byte", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }
        #endregion

    }
}
