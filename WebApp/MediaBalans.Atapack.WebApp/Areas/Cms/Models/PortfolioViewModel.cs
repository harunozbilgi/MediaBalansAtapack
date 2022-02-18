using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class PortfolioViewModel
    {
        public Portfolio Portfolio { get; set; }    
        public List<PortfolioLanguage> PortfolioLanguages { get; set; }     
        public List<IFormFile> Files { get; set; }  
    }
}
