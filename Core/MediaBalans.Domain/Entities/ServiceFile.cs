using MediaBalans.Domain.Common;


namespace MediaBalans.Domain.Entities
{
    public class ServiceFile : BaseEntity
    {
        public Guid ServicePropertyId { get; set; } 
        public string FileCode { get; set; }
        public virtual ServiceProperty ServiceProperty { get; set; }    
    }
}
