using MediaBalans.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBalans.Persistence.Configurations
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FullName).HasMaxLength(150);
            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.Password).HasMaxLength(150);
            builder.Property(x => x.Role).HasMaxLength(150);
            builder.HasData(new AppUser()
            {
                Id = Guid.NewGuid(),
                FullName = "Admin",
                Email = "admin@admin.com",
                Password = "Admin123",
                Role = "Admin"
            });
        }
    }
}
