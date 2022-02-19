using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class ServicePropertyViewModel
    {
        public ServiceProperty ServiceProperty { get; set; }
        public List<ServicePropertyLanguage> ServicePropertyLanguages { get; set; }
        public List<IFormFile> Files { get; set; }  
    }
}
