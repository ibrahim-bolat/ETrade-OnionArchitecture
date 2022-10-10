using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETrade.Persistence.Mappings;

    public class SubCategoryMap:IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(subCategory => subCategory.Id);
            builder.Property(subCategory => subCategory.Id).ValueGeneratedOnAdd();
            builder.Property(subCategory => subCategory.Name).HasMaxLength(250).IsRequired();
            builder.Property(subCategory => subCategory.Note).HasMaxLength(500);
            builder.HasOne(subCategory => subCategory.MainCategory).WithMany(mainCategory => mainCategory.SubCategories)
                .HasForeignKey(subCategory => subCategory.MainCategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(subCategory => subCategory.Brands).WithOne(brand => brand.SubCategory)
                .HasForeignKey(brand => brand.SubCategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new SubCategory()
            {
                Id = 1, 
                MainCategoryId = 1,
                Name = "Otomobil"
            }, new SubCategory()
            {
                Id = 2, 
                MainCategoryId = 1,
                Name = "Motorsiklet"
            }, new SubCategory()
            {
                Id = 3, 
                MainCategoryId = 1,
                Name = "Minivan & Panelvan"
            }, new SubCategory()
            {
                Id = 4, 
                MainCategoryId = 1,
                Name = "Arazi, SUV & Pickup"
            });

        }
    }
