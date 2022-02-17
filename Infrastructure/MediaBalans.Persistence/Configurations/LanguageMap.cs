using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class LanguageMap : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ShortName).HasMaxLength(15);
            builder.Property(x => x.FullName).HasMaxLength(65);
            builder.Property(x => x.LangCode).HasMaxLength(10);
            builder.HasData(new Language[] {
                new Language
                {
                    CreateTime = DateTime.Now,
                    FullName = "Azerbaycan",
                    IsActive = true,
                    LangCode = "az",
                    ShortName = "az_Az",
                    Id=Guid.NewGuid(),
                },
                new Language
                {
                    CreateTime = DateTime.Now,
                    FullName = "Ingilizce",
                    IsActive = true,
                    LangCode = "en",
                    ShortName = "en_US",
                    Id=Guid.NewGuid(),
                },
                new Language
                {
                    CreateTime = DateTime.Now,
                    FullName = "Rusca",
                    IsActive = true,
                    LangCode = "ru",
                    ShortName = "ru_RU",
                    Id = Guid.NewGuid(),
                }
            });
        }
    }
}
