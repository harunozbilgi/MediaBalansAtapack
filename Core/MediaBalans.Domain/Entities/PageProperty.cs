using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class PageProperty : BaseEntity
    {
        public Guid PageId { get; set; }
        public string Name { get; set; }
        public string FileCode { get; set; }
        public virtual Page Page { get; set; }
    }
}
