
using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class ProductFile : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string FileCode { get; set; }
        public virtual Product Product { get; set; }
    }
}
