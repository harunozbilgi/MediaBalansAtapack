using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class AppConfigMap : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileCode).HasMaxLength(15);
            builder.Property(x => x.Facebook).HasMaxLength(150);
            builder.Property(x => x.Instagram).HasMaxLength(150);
            builder.Property(x => x.Twitter).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(150);
            builder.Property(x => x.PhoneNumberOne).HasMaxLength(150);
            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.Address).HasMaxLength(250);
        }
    }
}
