using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class PageViewModel
    {
        public Page Page { get; set; }    
        public List<PageLanguage> PageLanguages { get; set; }
        public AppSeo AppSeo { get; set; }
        public List<AppSeoLanguage> AppSeoLanguages { get; set; }
        public IFormFile File { get; set; }  
    }
}
