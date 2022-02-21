using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class Page : BaseEntity
    {
        public Page()
        {
            PageLanguages = new HashSet<PageLanguage>();    
            PageProperties = new HashSet<PageProperty>();
        }

        public string FileCode { get; set; }
        public string SlugUrl { get; set; } 
        public int ShowView { get; set; }
        public string AppSeoCode { get; set; }
        public int OrderBy { get; set; }
        public bool IsHome { get; set; }    
        public virtual ICollection<PageLanguage> PageLanguages { get;}
        public virtual ICollection<PageProperty> PageProperties { get; set; }
    }
}
