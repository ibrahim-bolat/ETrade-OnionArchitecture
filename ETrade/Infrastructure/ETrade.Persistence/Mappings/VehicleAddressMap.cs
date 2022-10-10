using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class VehicleAddressMap:IEntityTypeConfiguration<VehicleAddress>
    {
        public void Configure(EntityTypeBuilder<VehicleAddress> builder)
        {
            builder.HasKey(vehicleAddress => vehicleAddress.Id);
            builder.Property(vehicleAddress => vehicleAddress.Id).ValueGeneratedOnAdd();
            builder.Property(vehicleAddress => vehicleAddress.AddressTitle).HasMaxLength(100).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.NeighborhoodOrVillage).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.District).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.City).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.PostalCode).HasMaxLength(5);
            builder.Property(vehicleAddress => vehicleAddress.AddressDetails).HasMaxLength(500).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.Note).HasMaxLength(500);
            
            builder.HasOne(vehicleAddress => vehicleAddress.Ad).WithOne(ad => ad.VehicleAddress)
                .HasForeignKey<VehicleAddress>(vehicleAddress=>vehicleAddress.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new VehicleAddress
            {
                Id = 1,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 2,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 3,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 4,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            });

        }
    }
