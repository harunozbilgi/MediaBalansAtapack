using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }  
        public List<CategoryLanguage> CategoryLanguages { get; set; }
    }
}
