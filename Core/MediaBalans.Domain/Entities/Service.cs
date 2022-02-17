using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class Service : BaseEntity
    {
        public Service()
        {
            ServiceLanguages = new HashSet<ServiceLanguage>();
            ServiceProperties = new HashSet<ServiceProperty>();
        }

        public string Icon { get; set; }
        public string Image_Url { get; set; }
        public string AppSeoCode { get; set; }
        public int OrderBy { get; set; }
        public string SlugUrl { get; set; }
        public bool IsPage { get; set; }
        public virtual ICollection<ServiceLanguage> ServiceLanguages { get; set; }  
        public virtual ICollection<ServiceProperty> ServiceProperties { get; set; } 
    }
}
