using MediaBalans.Domain.Common;
namespace MediaBalans.Domain.Entities.Languages
{
    public class ProductLanguage : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public virtual Product Product { get; set; }

    }
}
