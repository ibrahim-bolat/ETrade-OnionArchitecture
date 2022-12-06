using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class CityMap:IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);
            builder.Property(city => city.Id).ValueGeneratedOnAdd();
            builder.Property(city => city.Name).HasMaxLength(250).IsRequired();
            builder.Property(city => city.Note).HasMaxLength(500);
            builder.HasMany(city => city.Districts).WithOne(district => district.City)
                .HasForeignKey(district => district.CityId).OnDelete(DeleteBehavior.Cascade);
        }
    }
