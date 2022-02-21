using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class AppSeoViewModel
    {
        public AppSeo AppSeo { get; set; }
        public List<AppSeoLanguage> AppSeoLanguages { get; set; }
    }
}
