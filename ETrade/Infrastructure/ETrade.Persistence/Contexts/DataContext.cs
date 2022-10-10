using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Persistence.Mappings;
using ETrade.Persistence.Mappings.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Persistence.Contexts;

    public class DataContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserImage> UserImages  { get; set; }
        public DbSet<MainCategory> MainCategories  { get; set; }
        public DbSet<SubCategory> SubCategories  { get; set; }
        public DbSet<Brand> Brands  { get; set; }
        public DbSet<Model> Models  { get; set; }
        public DbSet<Ad> Ads  { get; set; }
        public DbSet<VehicleAddress> VehicleAddresses  { get; set; }
        public DbSet<VehicleImage> VehicleImages  { get; set; }


        public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppRoleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new IdentityUserRoleMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new UserImageMap());
            builder.ApplyConfiguration(new AdMap());
            builder.ApplyConfiguration(new BrandMap());
            builder.ApplyConfiguration(new MainCategoryMap());
            builder.ApplyConfiguration(new SubCategoryMap());
            builder.ApplyConfiguration(new ModelMap());
            builder.ApplyConfiguration(new VehicleAddressMap());
            builder.ApplyConfiguration(new VehicleImageMap());
        }
    }
