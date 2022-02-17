using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class ServicePropertyMap : IEntityTypeConfiguration<ServiceProperty>
    {
        public void Configure(EntityTypeBuilder<ServiceProperty> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Service).WithMany(x => x.ServiceProperties).HasForeignKey(x => x.ServiceId);
        }
    }
}
