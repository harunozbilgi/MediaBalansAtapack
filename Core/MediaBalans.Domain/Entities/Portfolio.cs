using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Domain.Entities
{
    public class Portfolio : BaseEntity
    {
        public Portfolio()
        {
            PortfolioLanguages = new HashSet<PortfolioLanguage>();
            PortfolioFiles = new HashSet<PortfolioFile>();
        }
        public virtual ICollection<PortfolioLanguage> PortfolioLanguages { get; set; }
        public virtual ICollection<PortfolioFile> PortfolioFiles { get; set; }
    }
}
