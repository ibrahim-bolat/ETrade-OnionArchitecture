using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class IpAddressMap:IEntityTypeConfiguration<IpAddress>
    {
        public void Configure(EntityTypeBuilder<IpAddress> builder)
        {
            builder.HasKey(ip => ip.Id);
            builder.Property(ip => ip.Id).ValueGeneratedOnAdd();
            builder.Property(ip => ip.RangeStart).HasMaxLength(100).IsRequired();
            builder.Property(ip => ip.RangeEnd).HasMaxLength(17).IsRequired();
            builder.Property(ip => ip.IpListType)
                .HasConversion(
                    a=>a.ToString(),
                    a=>(IpListType)Enum.Parse(typeof(IpListType),a))
                .IsRequired();
            builder.Property(ip => ip.Note).HasMaxLength(500);
            builder.HasMany(ip => ip.Endpoints).WithMany(endpoint => endpoint.IpAddresses)
                .UsingEntity(e => e.ToTable("EndpointIpAddreses"));
            builder.HasData(new IpAddress()
            {
                Id = 1, 
                RangeStart = "192.168.10.30",
                RangeEnd = "192.168.10.50",
                IpListType = IpListType.BlackList,
            },
                new IpAddress()
                {
                    Id = 2, 
                    RangeStart = "192.168.0.10",
                    RangeEnd = "192.168.10.20",
                    IpListType = IpListType.WhiteList,
                });

        }
    }
