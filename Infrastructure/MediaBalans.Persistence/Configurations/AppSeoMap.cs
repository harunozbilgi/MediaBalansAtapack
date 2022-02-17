using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class AppSeoMap : IEntityTypeConfiguration<AppSeo>
    {
        public void Configure(EntityTypeBuilder<AppSeo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.AppSeoCode).HasMaxLength(15);
            builder.Property(x => x.Home).HasMaxLength(15);

        }
    }
}
