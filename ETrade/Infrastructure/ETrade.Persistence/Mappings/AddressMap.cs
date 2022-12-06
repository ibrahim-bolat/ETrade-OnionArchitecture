using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class AddressMap:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);
            builder.Property(address => address.Id).ValueGeneratedOnAdd();
            builder.Property(address => address.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(address => address.LastName).HasMaxLength(100).IsRequired();
            builder.Property(address => address.Email).HasMaxLength(100).IsRequired();
            builder.Property(address => address.PhoneNumber).HasMaxLength(17).IsRequired();
            builder.Property(address => address.AddressTitle).HasMaxLength(100).IsRequired();
            builder.Property(address => address.AddressType)
                .HasConversion(
                    a=>a.ToString(),
                    a=>(AddressType)Enum.Parse(typeof(AddressType),a))
                .IsRequired();
            builder.Property(address => address.Street).HasMaxLength(500).IsRequired();
            builder.Property(address => address.NeighborhoodOrVillage).HasMaxLength(500).IsRequired();
            builder.Property(address => address.District).HasMaxLength(250).IsRequired();
            builder.Property(address => address.City).HasMaxLength(250).IsRequired();
            builder.Property(address => address.PostalCode).HasMaxLength(5);
            builder.Property(address => address.AddressDetails).HasMaxLength(500).IsRequired();
            builder.Property(address => address.Note).HasMaxLength(500);
            builder.HasOne(address => address.AppUser).WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Address
            {
                Id = 1, 
                FirstName = "ibo",
                LastName = "BOL",
                Email = "bolatcan@email.com",
                PhoneNumber = "+90(532)5757966",
                AddressTitle = "Evim",
                AddressType = AddressType.Home,
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye",
                DefaultAddress = false,
                UserId = 1
            },
            new Address
            {
                Id = 2, 
                FirstName = "ibo",
                LastName = "BOLAT",
                Email = "bolatcan@email.com",
                PhoneNumber = "+90(532)5757966",
                AddressTitle = "İş",
                AddressType = AddressType.Work,
                NeighborhoodOrVillage = "Mustafa Kemal",
                District = "Çankaya",
                City ="Ankara",
                PostalCode = "06100",
                AddressDetails = "Mustafa Kemal Mahallesi ,Eskişehir Yolu  Kütahya Sok. No:280/7 06500 Çankaya/Ankara/Türkiye",
                DefaultAddress = true,
                UserId = 1
            });

        }
    }
