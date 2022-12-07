using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class StreetMap:IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasKey(street => street.Id);
            builder.Property(street => street.Id).ValueGeneratedOnAdd();
            builder.Property(street => street.Name).HasMaxLength(500).IsRequired();
            builder.HasOne(street => street.NeighborhoodOrVillage).WithMany(neighborhoodorvillage => neighborhoodorvillage.Streets)
                .HasForeignKey(street => street.NeighborhoodOrVillageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
