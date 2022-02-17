using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductLanguages = new HashSet<ProductLanguage>();
            ProductFiles = new HashSet<ProductFile>();
        }

        public Guid CategoryId { get; set; }
        public string AppSeoCode { get; set; }
        public int OrderBy { get; set; }
        public string SlugUrl { get; set; } 
       
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
        public virtual ICollection<ProductLanguage> ProductLanguages { get; set; }
    }
}