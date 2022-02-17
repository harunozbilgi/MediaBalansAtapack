
using MediaBalans.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations.LanguagesMap
{
    public class ProductLanguageMap : IEntityTypeConfiguration<ProductLanguage>
    {
        public void Configure(EntityTypeBuilder<ProductLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Lang_Code).HasMaxLength(10);
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductLanguages).HasForeignKey(x => x.ProductId);
        }
    }
}
