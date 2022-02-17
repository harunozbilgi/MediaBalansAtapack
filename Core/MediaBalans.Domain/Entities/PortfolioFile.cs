using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class PortfolioFile : BaseEntity
    {
        public Guid PortfolioId { get; set; }
        public string FileCode { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
