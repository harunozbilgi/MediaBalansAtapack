using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class CategoryLanguage : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }
        public virtual Category Category { get; set; }
    }
}
