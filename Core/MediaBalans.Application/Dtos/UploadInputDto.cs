using Microsoft.AspNetCore.Http;

namespace MediaBalans.Application.Dtos
{
    public class UploadInputDto
    {
        public IFormFile File { get; set; } 
        public string FolderName { get; set; }
        public string ImageUrl { get; set; }    
        public string VideoUrl { get; set; }
    }
}
