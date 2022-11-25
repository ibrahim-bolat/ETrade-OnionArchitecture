

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
            builder.HasMany(appRole => appRole.Endpoints).WithMany(endpoint => endpoint.AppRoles)
                .UsingEntity(r => r.ToTable("EndpointRoles"));
            builder.HasData(new AppRole
            {
                Id = 1,
                Name = RoleType.Owner.ToString(),
                NormalizedName = RoleType.Owner.ToString().ToUpperInvariant()
            },
            new AppRole
            {
                Id = 2,
                Name = RoleType.Admin.ToString(),
                NormalizedName = RoleType.Admin.ToString().ToUpperInvariant()
            },
            new AppRole
            {
                Id = 3,
                Name = RoleType.User.ToString(),
                NormalizedName = RoleType.User.ToString().ToUpperInvariant()
            });
         }
    }
