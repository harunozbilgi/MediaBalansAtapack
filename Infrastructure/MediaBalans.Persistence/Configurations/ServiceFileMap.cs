using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class ServiceFileMap: IEntityTypeConfiguration<ServiceFile>
    {
        public void Configure(EntityTypeBuilder<ServiceFile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileCode).HasMaxLength(15);
            builder.HasOne(x => x.ServiceProperty).WithMany(x => x.ServiceFiles).HasForeignKey(x => x.ServicePropertyId);
        }
    }
}
