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
            builder.HasKey(subCategory => subCategory.Id);
            builder.Property(subCategory => subCategory.Id).ValueGeneratedOnAdd();
            builder.Property(subCategory => subCategory.Note).HasMaxLength(500);
            builder.HasOne(action => action.Menu).WithMany(menu => menu.Actions)
                .HasForeignKey(action => action.MenuId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(action => action.AppRoles).WithMany(appRole => appRole.Actions)
                .UsingEntity(r => r.ToTable("ActionRoles"));;
        }
    }
