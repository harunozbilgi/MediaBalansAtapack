using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class ServicePropertyLanguage : BaseEntity
    {
        public Guid ServicePropertyId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }  
        public string Description { get; set; } 
        public virtual ServiceProperty ServiceProperty { get; set; }

    }
}
