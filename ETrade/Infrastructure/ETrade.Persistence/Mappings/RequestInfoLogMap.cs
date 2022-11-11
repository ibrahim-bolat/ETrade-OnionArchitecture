using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Mappings;

    public class RequestInfoLogMap:IEntityTypeConfiguration<RequestInfoLog>
    {
        public void Configure(EntityTypeBuilder<RequestInfoLog> builder)
        {
            builder.HasKey(requestInfoLog => requestInfoLog.Id);
            builder.Property(requestInfoLog => requestInfoLog.Id).ValueGeneratedOnAdd();
            builder.Property(requestInfoLog => requestInfoLog.AreaName).HasMaxLength(100);
            builder.Property(requestInfoLog => requestInfoLog.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(requestInfoLog => requestInfoLog.ActionName).HasMaxLength(100).IsRequired();
            builder.Property(requestInfoLog => requestInfoLog.ActionArguments).HasMaxLength(200);
            builder.Property(requestInfoLog => requestInfoLog.Note).HasMaxLength(500);
            builder.HasOne(requestInfoLog => requestInfoLog.AppUser).WithMany(user => user.RequestInfoLogs)
                .HasForeignKey(requestInfoLog => requestInfoLog.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
