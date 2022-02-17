using MediaBalans.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediaBalans.Persistence.Configurations.LanguagesMap
{
    public class AppSeoLanguageMap : IEntityTypeConfiguration<AppSeoLanguage>
    {
        public void Configure(EntityTypeBuilder<AppSeoLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Lang_Code).HasMaxLength(10);
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.HasOne(x => x.AppSeo).WithMany(x => x.AppSeoLanguages).HasForeignKey(x => x.AppSeoId);
        }
    }
}
