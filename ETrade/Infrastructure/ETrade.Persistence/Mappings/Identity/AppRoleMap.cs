

using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings.Identity;


public class AppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(role => role.Note).HasMaxLength(500);
            builder.HasData(new AppRole
            {
                Id = 1,
                Name = RoleType.Admin.ToString(),
                NormalizedName = "ADMIN"
            });
    }
    }
