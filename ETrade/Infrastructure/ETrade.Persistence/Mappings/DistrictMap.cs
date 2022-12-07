using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class DistrictMap:IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(district => district.Id);
            builder.Property(district => district.Id).ValueGeneratedOnAdd();
            builder.Property(district => district.Name).HasMaxLength(250).IsRequired();
            builder.HasOne(district => district.City).WithMany(city => city.Districts)
                .HasForeignKey(district => district.CityId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(district => district.NeighborhoodsOrVillages).WithOne(neighborhoodorvillage => neighborhoodorvillage.District)
                .HasForeignKey(neighborhoodorvillage => neighborhoodorvillage.DistrictId).OnDelete(DeleteBehavior.Cascade);
        }
    }
