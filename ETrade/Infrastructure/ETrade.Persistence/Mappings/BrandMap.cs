using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class BrandMap:IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(brand => brand.Id);
            builder.Property(brand => brand.Id).ValueGeneratedOnAdd();
            builder.Property(brand => brand.Name).HasMaxLength(250).IsRequired();
            builder.Property(brand => brand.Note).HasMaxLength(500);
            builder.HasOne(brand => brand.SubCategory).WithMany(subCategory => subCategory.Brands)
                .HasForeignKey(brand => brand.SubCategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(brand => brand.Models).WithOne(model => model.Brand)
                .HasForeignKey(model => model.BrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Brand()
            {
                Id = 1, 
                SubCategoryId = 1,
                Name = "Wolkswagen"
            }, new Brand()
            {
                Id = 2, 
                SubCategoryId = 2,
                Name = "Honda"
            }, new Brand()
            {
                Id = 3, 
                SubCategoryId = 3,
                Name = "Fiat"
            }, new Brand()
            {
                Id = 4, 
                SubCategoryId = 4,
                Name = "Nissan"
            });

        }
    }
