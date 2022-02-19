using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }    
        public List<ServiceLanguage> ServiceLanguages { get; set; }
        public AppSeo AppSeo { get; set; }
        public List<AppSeoLanguage> AppSeoLanguages { get; set; }
        public IFormFile File { get; set; }
        public IFormFile Icon { get; set; }
    }
}
