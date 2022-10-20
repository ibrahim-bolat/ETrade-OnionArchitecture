using ETrade.Application.Constants;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Identity;
using ETrade.Persistence.Contexts;
using ETrade.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETrade.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        //dbcontext
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            //dotnet ef migrations add InitialCreate -s Presentation/ETrade.MVC -p Infrastructure/ETrade.Persistence
            //dotnet ef database update -s Presentation/ETrade.MVC -p Infrastructure/ETrade.Persistence
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        //repositories
        serviceCollection.AddScoped<IAddressRepository,EfAddressRepository>();
        serviceCollection.AddScoped<IUserImageRepository,EfUserImageRepository>();
        serviceCollection.AddScoped<IUserRepository,EfUserRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
    }
}