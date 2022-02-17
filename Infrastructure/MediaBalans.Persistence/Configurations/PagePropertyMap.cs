using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediaBalans.Persistence.Configurations
{
    public class PagePropertyMap : IEntityTypeConfiguration<PageProperty>
    {
        public void Configure(EntityTypeBuilder<PageProperty> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileCode).HasMaxLength(15);
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.HasOne(x => x.Page).WithMany(x => x.PageProperties).HasForeignKey(x => x.PageId);
        }
    }
}
