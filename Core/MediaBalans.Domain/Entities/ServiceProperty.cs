using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class ServiceProperty : BaseEntity
    {
        public ServiceProperty()
        {
            ServicePropertyLanguages = new HashSet<ServicePropertyLanguage>();
            ServiceFiles = new HashSet<ServiceFile>();
        }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<ServicePropertyLanguage> ServicePropertyLanguages { get; set; }
        public virtual ICollection<ServiceFile> ServiceFiles { get; set; }
    }
}
