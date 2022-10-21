using System.Text.Json.Serialization;
using ETrade.Application.Constants;
using ETrade.Domain.Entities.Identity;
using ETrade.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace ETrade.MVC;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        //hot reload
        serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
        
        //for Ignore Cycles
        serviceCollection.AddControllers().AddJsonOptions(options => 
        { 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        //cookie configuration
        serviceCollection.ConfigureApplicationCookie(cookieOptions =>
        {
            cookieOptions.LoginPath = new PathString("/Admin/Account/Login");
            cookieOptions.LogoutPath = new PathString("/Admin/Account/Logout");
            cookieOptions.Cookie = new CookieBuilder
            {
                Name = "ETradeCookie", 
                HttpOnly = false, 
                SameSite = SameSiteMode.Lax, 
                SecurePolicy = CookieSecurePolicy.Always 
            };
            cookieOptions.SlidingExpiration = true; 
            cookieOptions.ExpireTimeSpan =
                TimeSpan.FromDays(30);
            cookieOptions.AccessDeniedPath = new PathString("/ErrorPages/AccessDenied");
        });
        
        //all project authorize
        serviceCollection.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }); 
        
        //for fix token error
        serviceCollection.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true; 
        });
    }
}