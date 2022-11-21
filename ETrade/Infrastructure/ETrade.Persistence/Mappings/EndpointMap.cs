using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class EndpointMap:IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            builder.HasKey(endpoint => endpoint.Id);
            builder.Property(endpoint => endpoint.Id).ValueGeneratedOnAdd();
            builder.Property(endpoint => endpoint.Code).HasMaxLength(150).IsRequired();
            builder.Property(endpoint => endpoint.Definition).HasMaxLength(150).IsRequired();
            builder.Property(endpoint => endpoint.HttpType).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.ActionType).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.EndpointName).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.AreaName).HasMaxLength(100);
            builder.Property(endpoint => endpoint.Note).HasMaxLength(500);
            builder.HasMany(endpoint => endpoint.AppRoles).WithMany(appRole => appRole.Endpoints)
                .UsingEntity(r => r.ToTable("EndpointRoles"));;
        }
    }
