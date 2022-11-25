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
        
        
        //identity 
        serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

        //user security stamp validate time
        serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromMinutes(10);
                
        });
        
        // Sets the expiry to two hours
        serviceCollection.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(2); 
        });
        
        //repositories
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
    }
}