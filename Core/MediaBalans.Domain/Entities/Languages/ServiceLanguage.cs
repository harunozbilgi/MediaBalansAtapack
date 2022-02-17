using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class ServiceLanguage : BaseEntity
    {
        public Guid ServiceId { get; set; } 
        public string Lang_Code { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; } 
        public string Description { get; set; }
        public virtual Service Service { get; set; }    
    }
}
