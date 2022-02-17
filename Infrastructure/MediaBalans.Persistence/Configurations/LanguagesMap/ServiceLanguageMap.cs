using MediaBalans.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations.LanguagesMap
{
    public class ServiceLanguageMap : IEntityTypeConfiguration<ServiceLanguage>
    {
        public void Configure(EntityTypeBuilder<ServiceLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Lang_Code).HasMaxLength(10);
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.Property(x => x.Subject).HasMaxLength(250);
            builder.HasOne(x => x.Service).WithMany(x => x.ServiceLanguages).HasForeignKey(x => x.ServiceId);
        }
    }
}
