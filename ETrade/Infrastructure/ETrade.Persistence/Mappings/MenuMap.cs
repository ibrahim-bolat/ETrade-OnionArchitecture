using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class MenuMap:IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(mainCategory => mainCategory.Id);
            builder.Property(mainCategory => mainCategory.Id).ValueGeneratedOnAdd();
            builder.Property(mainCategory => mainCategory.Note).HasMaxLength(500);
            builder.HasMany(menu => menu.Actions).WithOne(action => action.Menu)
                .HasForeignKey(action => action.MenuId).OnDelete(DeleteBehavior.Cascade);
            /*
            builder.HasData(new Menu()
            {
                Id = 1, 
                Name = "Account",
                Checked = false,
                IsActive = true
            }, new Menu()
            {
                Id = 2, 
                Name = "Address",
                Checked = false,
                IsActive = true
            });
            */
        }
    }
