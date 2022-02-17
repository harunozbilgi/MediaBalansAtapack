using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class DocumentMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DocumentUnique).HasMaxLength(15);
            builder.Property(x => x.DocumentName).HasMaxLength(150);
            builder.Property(x => x.DocumentType).HasMaxLength(50);
            builder.Property(x => x.DocumentSize).HasMaxLength(50);
            builder.Property(x => x.DocumentFolderName).HasMaxLength(150);
            builder.Property(x => x.Storage_Url).HasMaxLength(250);
            builder.Property(x => x.Image_Url).HasMaxLength(250);

        }
    }
}
