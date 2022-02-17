using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class ProductFileMap : IEntityTypeConfiguration<ProductFile>
    {
        public void Configure(EntityTypeBuilder<ProductFile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileCode).HasMaxLength(15);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductFiles).HasForeignKey(x => x.ProductId);
        }
    }
}
