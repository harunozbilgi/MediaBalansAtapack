using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }    
        public List<ProductLanguage> ProductLanguages { get; set; }
        public AppSeo AppSeo { get; set; }
        public List<AppSeoLanguage> AppSeoLanguages { get; set; }
        public List<Category> Categories { get; set; }
        public List<IFormFile> Files { get; set; }  
    }
}
