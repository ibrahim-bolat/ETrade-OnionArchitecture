
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;


    public class ModelMap:IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(model => model.Id);
            builder.Property(model => model.Id).ValueGeneratedOnAdd();
            builder.Property(model => model.Name).HasMaxLength(250).IsRequired();
            builder.Property(model => model.EngineType).HasMaxLength(100).IsRequired();
            
            builder.Property(ad => ad.EngineCapacity)
                .HasConversion(
                    a => a.ToString(),
                    a => (EngineCapacityType)Enum.Parse(typeof(EngineCapacityType), a)).IsRequired();
            builder.Property(ad => ad.EnginePower)
                .HasConversion(
                    a => a.ToString(),
                    a => (EnginePowerType)Enum.Parse(typeof(EnginePowerType), a)).IsRequired();
            builder.Property(model => model.EquipmentVariant).HasMaxLength(100).IsRequired();
            builder.Property(model => model.ModelYear).HasMaxLength(4).IsFixedLength().IsRequired();
            
            builder.Property(ad => ad.FuelType)
                .HasConversion(
                    a => a.ToString(),
                    a => (FuelType)Enum.Parse(typeof(FuelType), a)).IsRequired();
            builder.Property(ad => ad.GearType)
                .HasConversion(
                    a => a.ToString(),
                    a => (GearType)Enum.Parse(typeof(GearType), a)).IsRequired();
            builder.Property(model => model.Kilometer).IsRequired();
            builder.Property(ad => ad.BodyType)
                .HasConversion(
                    a => a.ToString(),
                    a => (BodyType)Enum.Parse(typeof(BodyType), a)).IsRequired();
            builder.Property(ad => ad.TractionType)
                .HasConversion(
                    a => a.ToString(),
                    a => (TractionType)Enum.Parse(typeof(TractionType), a)).IsRequired();
            builder.Property(ad => ad.ModelColour)
                .HasConversion(
                    a => a.ToString(),
                    a => (ModelColourType)Enum.Parse(typeof(ModelColourType), a)).IsRequired();
            builder.Property(ad => ad.GuaranteeStatus)
                .HasConversion(
                    a => a.ToString(),
                    a => (GuaranteeStatus)Enum.Parse(typeof(GuaranteeStatus), a)).IsRequired();
            builder.Property(ad => ad.PlateNationality)
                .HasConversion(
                    a => a.ToString(),
                    a => (PlateNationalityType)Enum.Parse(typeof(PlateNationalityType), a)).IsRequired();
            
            builder.Property(model => model.Note).HasMaxLength(500);
            builder.HasOne(model => model.Brand).WithMany(brand => brand.Models)
                .HasForeignKey(model => model.BrandId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(model => model.Ad).WithOne(ad => ad.Model)
                .HasForeignKey<Model>(model=>model.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Model()
            {
                Id = 1,
                Name = "Golf",
                EngineType = "1.6 TDI",
                EngineCapacity = EngineCapacityType.Cm13011600,
                EnginePower = EnginePowerType.Hp101125,
                EquipmentVariant = "Confortline",
                ModelYear = DateTime.Now.AddDays(-2500).Year,
                FuelType = FuelType.Diesel,
                GearType = GearType.Automatic,
                Kilometer = 102000,
                BodyType = BodyType.Hatchback5Door,
                TractionType = TractionType.FrontDrive,
                ModelColour = ModelColourType.White,
                GuaranteeStatus = GuaranteeStatus.No,
                PlateNationality = PlateNationalityType.TurkeyPlate,
                BrandId = 1
            },new Model()
            {
                Id = 2,
                Name = "City",
                EngineType = "1.4 TSI",
                EngineCapacity = EngineCapacityType.Cm13011600,
                EnginePower = EnginePowerType.Hp101125,
                EquipmentVariant = "Confortline",
                ModelYear = DateTime.Now.AddDays(-2500).Year,
                FuelType = FuelType.Diesel,
                GearType = GearType.Automatic,
                Kilometer = 102000,
                BodyType = BodyType.Hatchback5Door,
                TractionType = TractionType.FrontDrive,
                ModelColour = ModelColourType.White,
                GuaranteeStatus = GuaranteeStatus.No,
                PlateNationality = PlateNationalityType.TurkeyPlate,
                BrandId = 2
            },new Model()
            {
                Id = 3,
                Name = "Egea",
                EngineType = "1.6 TDI",
                EngineCapacity = EngineCapacityType.Cm13011600,
                EnginePower = EnginePowerType.Hp101125,
                EquipmentVariant = "Confortline",
                ModelYear = DateTime.Now.AddDays(-2500).Year,
                FuelType = FuelType.Diesel,
                GearType = GearType.Automatic,
                Kilometer = 102000,
                BodyType = BodyType.Hatchback5Door,
                TractionType = TractionType.FrontDrive,
                ModelColour = ModelColourType.White,
                GuaranteeStatus = GuaranteeStatus.No,
                PlateNationality = PlateNationalityType.TurkeyPlate,
                BrandId = 3
            },new Model()
            {
                Id = 4,
                Name = "Qashqai",
                EngineType = "1.6 Düz",
                EngineCapacity = EngineCapacityType.Cm13011600,
                EnginePower = EnginePowerType.Hp101125,
                EquipmentVariant = "Confortline",
                ModelYear = DateTime.Now.AddDays(-2500).Year,
                FuelType = FuelType.Diesel,
                GearType = GearType.Automatic,
                Kilometer = 102000,
                BodyType = BodyType.Hatchback5Door,
                TractionType = TractionType.FrontDrive,
                ModelColour = ModelColourType.White,
                GuaranteeStatus = GuaranteeStatus.No,
                PlateNationality = PlateNationalityType.TurkeyPlate,
                BrandId = 4
            });
        }
    }
