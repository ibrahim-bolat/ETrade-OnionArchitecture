using System.Reflection;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Persistence.Mappings;
using ETrade.Persistence.Mappings.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Persistence.Contexts;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }
    public DbSet<RequestInfoLog> RequestInfoLogs { get; set; }
    public DbSet<IpAddress> IpAddresses { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Ad> Ads { get; set; }
    public DbSet<VehicleAddress> VehicleAddresses { get; set; }
    public DbSet<VehicleImage> VehicleImages { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<NeighborhoodOrVillage> NeighborhoodsOrVillages { get; set; }
    public DbSet<Street> Streets { get; set; }


    public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
