
namespace MediaBalans.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
