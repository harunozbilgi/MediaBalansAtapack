using MediaBalans.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediaBalans.Persistence.Configurations.LanguagesMap
{
    public class PortfolioLanguageMap : IEntityTypeConfiguration<PortfolioLanguage>
    {
        public void Configure(EntityTypeBuilder<PortfolioLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Lang_Code).HasMaxLength(10);
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.HasOne(x => x.Portfolio).WithMany(x => x.PortfolioLanguages).HasForeignKey(x => x.PortfolioId);
        }
    }
}
