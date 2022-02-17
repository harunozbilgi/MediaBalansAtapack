using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class AppSeoLanguage : BaseEntity
    {
        public Guid AppSeoId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public virtual AppSeo AppSeo { get; set; }
    }
}
