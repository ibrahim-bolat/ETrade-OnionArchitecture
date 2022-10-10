using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class AdMap:IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(ad => ad.Id);
            builder.Property(ad => ad.Id).ValueGeneratedOnAdd();
            builder.Property(ad => ad.AdNo).HasMaxLength(20).IsRequired();
            builder.Property(ad => ad.AdTitle).HasMaxLength(250).IsRequired();
            builder.Property(ad => ad.AdDate).HasColumnType("date").IsRequired();
            builder.Property(ad => ad.AdVehicleStatus)
                .HasConversion(
                    a => a.ToString(),
                    a => (AdVehicleStatus)Enum.Parse(typeof(AdVehicleStatus), a)).IsRequired();
            builder.Property(ad => ad.AdFromWho)
                .HasConversion(
                    a => a.ToString(),
                    a => (AdFromWhoType)Enum.Parse(typeof(AdFromWhoType), a)).IsRequired();
            builder.Property(ad => ad.AdSwapStatus)
                .HasConversion(
                    a => a.ToString(),
                    a => (AdSwapStatus)Enum.Parse(typeof(AdSwapStatus), a)).IsRequired();
            builder.Property(ad => ad.DamageStatus)
                .HasConversion(
                    a => a.ToString(),
                    a => (DamageStatus)Enum.Parse(typeof(DamageStatus), a)).IsRequired();
            
            builder.Property(ad => ad.AdVehiclePrice).HasColumnType("decimal(18,4)").IsRequired();
            
            builder.Property(ad => ad.AdDetail).HasMaxLength(1000);
            builder.Property(ad => ad.Note).HasMaxLength(500);

            builder.HasOne(ad => ad.Model).WithOne(model => model.Ad)
                .HasForeignKey<Model>(model=>model.Id).OnDelete(DeleteBehavior.Cascade);
                        
            builder.HasOne(ad => ad.VehicleAddress).WithOne(vehicleAddress => vehicleAddress.Ad)
                .HasForeignKey<VehicleAddress>(vehicleAddress=>vehicleAddress.Id).OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(ad => ad.VehicleImages).WithOne(vehicleImage => vehicleImage.Ad)
                .HasForeignKey(vehicleImage => vehicleImage.AdId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Ad()
            {
                Id = 1,
                AdNo = "123456789",
                AdTitle = "Sahibinden Tertemiz Araba",
                AdDate = DateTime.Now,
                AdVehicleStatus = AdVehicleStatus.FirstHand,
                AdFromWho = AdFromWhoType.ByOwner,
                AdSwapStatus = AdSwapStatus.Yes,
                DamageStatus = DamageStatus.Unspecified,
                AdVehiclePrice = 350000.50m,
                AdDetail = "ÇOK GÜZEL ARABA"
            }, new Ad()
            {
                Id = 2,
                AdNo = "234567891",
                AdTitle = "Sahibinden Boyasız Araba",
                AdDate = DateTime.Now,
                AdVehicleStatus = AdVehicleStatus.SecondHand,
                AdFromWho = AdFromWhoType.ByOwner,
                AdSwapStatus = AdSwapStatus.No,
                DamageStatus = DamageStatus.HeavilyDamaged,
                AdVehiclePrice = 150000.7840m,
                AdDetail = "ÇOK GÜZEL ARABA"
            }, new Ad()
            {
                Id = 3,
                AdNo = "345678912",
                AdTitle = "İTHAL ARAÇ",
                AdDate = DateTime.Now,
                AdVehicleStatus = AdVehicleStatus.ImportedFirstHand,
                AdFromWho = AdFromWhoType.FromAuthorizedDealer,
                AdSwapStatus = AdSwapStatus.Yes,
                DamageStatus = DamageStatus.Unspecified,
                AdVehiclePrice = 1000000.50m,
                AdDetail = "ÇOK GÜZEL ARABA"
            }, new Ad()
            {
                Id = 4,
                AdNo = "456789123",
                AdTitle = "Galeriden Temiz Araç",
                AdDate = DateTime.Now,
                AdVehicleStatus = AdVehicleStatus.FirstHand,
                AdFromWho = AdFromWhoType.FromTheGalery,
                AdSwapStatus = AdSwapStatus.No,
                DamageStatus = DamageStatus.WithoutDamageRegistration,
                AdVehiclePrice = 2500000.80m,
                AdDetail = "ÇOK GÜZEL ARABA"
            });
        }
    }
