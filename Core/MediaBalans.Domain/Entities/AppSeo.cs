using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class AppSeo : BaseEntity
    {
        public AppSeo()
        {
            AppSeoLanguages = new HashSet<AppSeoLanguage>();
        }
        public string AppSeoCode { get; set; }
        public string Home { get; set; }
        public bool IsStaticPage { get; set; }
        public bool IsDinamicPage { get; set; }
        public virtual ICollection<AppSeoLanguage> AppSeoLanguages { get; set; }

    }
}
