using MediaBalans.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations.LanguagesMap
{
    public class CategoryLanguageMap : IEntityTypeConfiguration<CategoryLanguage>
    {
        public void Configure(EntityTypeBuilder<CategoryLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Lang_Code).HasMaxLength(10);
            builder.Property(x => x.Title).HasMaxLength(150);
            builder.HasOne(x => x.Category).WithMany(x => x.CategoryLanguages).HasForeignKey(x => x.CategoryId);
        }
    }
}
