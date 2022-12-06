using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class NeighborhoodOrVillageMap:IEntityTypeConfiguration<NeighborhoodOrVillage>
    {
        public void Configure(EntityTypeBuilder<NeighborhoodOrVillage> builder)
        {
            builder.HasKey(neighborhoodorvillage => neighborhoodorvillage.Id);
            builder.Property(neighborhoodorvillage => neighborhoodorvillage.Id).ValueGeneratedOnAdd();
            builder.Property(neighborhoodorvillage => neighborhoodorvillage.Name).HasMaxLength(500).IsRequired();
            builder.Property(neighborhoodorvillage => neighborhoodorvillage.Note).HasMaxLength(500);
            builder.HasOne(neighborhoodorvillage => neighborhoodorvillage.District).WithMany(district => district.NeighborhoodsOrVillages)
                .HasForeignKey(neighborhoodorvillage => neighborhoodorvillage.DistrictId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(neighborhoodorvillage => neighborhoodorvillage.Streets).WithOne(street => street.NeighborhoodOrVillage)
                .HasForeignKey(street => street.NeighborhoodOrVillageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
