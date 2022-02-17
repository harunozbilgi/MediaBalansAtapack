
namespace MediaBalans.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }
}
