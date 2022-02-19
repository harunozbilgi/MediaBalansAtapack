using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class NewsViewModel
    {
        public News News { get; set; }
        public List<NewsLanguage> NewsLanguages { get; set; }
        public AppSeo AppSeo { get; set; }
        public List<AppSeoLanguage> AppSeoLanguages { get; set; }
        public IFormFile File { get; set; } 
    }
}
