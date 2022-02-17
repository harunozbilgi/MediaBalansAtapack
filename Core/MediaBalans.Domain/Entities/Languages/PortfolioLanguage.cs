using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class PortfolioLanguage : BaseEntity
    {
        public Guid PortfolioId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Portfolio Portfolio { get; set; }

    }
}
