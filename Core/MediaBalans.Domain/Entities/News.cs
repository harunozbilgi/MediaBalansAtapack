using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class News : BaseEntity
    {
        public News()
        {
            NewsLanguages = new HashSet<NewsLanguage>();
        }
        public string FileCode { get; set; }
        public string AppSeoCode { get; set; }
        public string SlugUrl { get; set; }
        public virtual ICollection<NewsLanguage> NewsLanguages { get; set; }    
    }
}
