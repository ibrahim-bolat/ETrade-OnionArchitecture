using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class MenuMap:IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(menu => menu.Id);
            builder.Property(menu => menu.Id).ValueGeneratedOnAdd();
            builder.Property(menu => menu.AreaName).HasMaxLength(100);
            builder.Property(menu => menu.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(menu => menu.Definition).HasMaxLength(150).IsRequired();
            builder.Property(menu => menu.Note).HasMaxLength(500);
            builder.HasMany(menu => menu.Actions).WithOne(action => action.Menu)
                .HasForeignKey(action => action.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
