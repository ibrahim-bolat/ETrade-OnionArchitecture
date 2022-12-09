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
            builder.Property(address => address.StreetName).HasMaxLength(500);
            builder.Property(address => address.NeighborhoodOrVillageName).HasMaxLength(500).IsRequired();
            builder.Property(address => address.DistrictName).HasMaxLength(250).IsRequired();
            builder.Property(address => address.CityName).HasMaxLength(250).IsRequired();
            builder.Property(address => address.PostalCode).HasMaxLength(5);
            builder.Property(address => address.AddressDetails).HasMaxLength(500).IsRequired();
            builder.Property(address => address.Note).HasMaxLength(500);
            builder.HasOne(address => address.AppUser).WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
