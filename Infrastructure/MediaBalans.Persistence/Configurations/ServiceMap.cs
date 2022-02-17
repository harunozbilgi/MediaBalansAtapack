using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class ServiceMap : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SlugUrl).HasMaxLength(250);
            builder.Property(x => x.AppSeoCode).HasMaxLength(15);
            builder.Property(x => x.Icon).HasMaxLength(15);
            builder.Property(x => x.Image_Url).HasMaxLength(15);
        }
    }
}
