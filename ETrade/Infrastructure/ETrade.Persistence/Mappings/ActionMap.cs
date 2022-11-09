using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Mappings;

    public class ActionMap:IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.HasKey(action => action.Id);
            builder.Property(action => action.Id).ValueGeneratedOnAdd();
            builder.Property(action => action.Code).HasMaxLength(150).IsRequired();
            builder.Property(action => action.Definition).HasMaxLength(150).IsRequired();
            builder.Property(action => action.HttpType).HasMaxLength(100).IsRequired();
            builder.Property(action => action.ActionType).HasMaxLength(100).IsRequired();
            builder.Property(action => action.Note).HasMaxLength(500);
            builder.HasOne(action => action.Menu).WithMany(menu => menu.Actions)
                .HasForeignKey(action => action.MenuId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(action => action.AppRoles).WithMany(appRole => appRole.Actions)
                .UsingEntity(r => r.ToTable("ActionRoles"));;
        }
    }
