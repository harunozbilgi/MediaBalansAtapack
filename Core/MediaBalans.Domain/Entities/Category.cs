using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            CategoryLanguages = new HashSet<CategoryLanguage>();
            Products = new HashSet<Product>();
        }
        public string SlugUrl { get; set; }
        public int OrderBy { get; set; }
        public virtual ICollection<CategoryLanguage> CategoryLanguages { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
