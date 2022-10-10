using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class VehicleImageMap:IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.HasKey(vehicleImage => vehicleImage.Id);
            builder.Property(vehicleImage => vehicleImage.Id).ValueGeneratedOnAdd();
            builder.Property(vehicleImage => vehicleImage.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(vehicleImage => vehicleImage.ImagePath).HasMaxLength(500).IsRequired();
            builder.Property(vehicleImage => vehicleImage.ImageAltText).HasMaxLength(250);
            builder.Property(vehicleImage => vehicleImage.Note).HasMaxLength(500);
            
            builder.HasOne(vehicleImage => vehicleImage.Ad).WithMany(ad => ad.VehicleImages)
                .HasForeignKey(vehicleImage => vehicleImage.AdId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new VehicleImage()
            {
                Id = 1, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/userimages/profil.png",
                ImageAltText = "Profil",
                AdId = 1
            },new VehicleImage()
            {
                Id = 2, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/userimages/profil.png",
                ImageAltText = "Profil",
                AdId = 2
            },new VehicleImage()
            {
                Id = 3, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/userimages/profil.png",
                ImageAltText = "Profil",
                AdId = 3
            },new VehicleImage()
            {
                Id = 4, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/userimages/profil.png",
                ImageAltText = "Profil",
                AdId = 4
            });

        }
    }
