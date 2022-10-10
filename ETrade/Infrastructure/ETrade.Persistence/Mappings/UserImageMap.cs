using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class UserImageMap:IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(userImage => userImage.Id);
            builder.Property(userImage => userImage.Id).ValueGeneratedOnAdd();
            builder.Property(userImage => userImage.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(userImage => userImage.ImagePath).HasMaxLength(500).IsRequired();
            builder.Property(userImage => userImage.ImageAltText).HasMaxLength(250);
            builder.Property(userImage => userImage.Note).HasMaxLength(500);
            builder.HasOne(userImage => userImage.AppUser).WithMany(user => user.UserImages)
                .HasForeignKey(userImage => userImage.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new UserImage()
            {
                Id = 1, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/userimages/1/profil.jpg",
                ImageAltText = "Profil",
                Profil = true,
                UserId =1,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
