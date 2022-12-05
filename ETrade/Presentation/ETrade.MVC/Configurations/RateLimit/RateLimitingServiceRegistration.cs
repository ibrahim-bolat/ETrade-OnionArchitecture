using AspNetCoreRateLimit;

namespace ETrade.MVC.Configurations.RateLimit;

internal static class RateLimitingServiceRegistration
{
    internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        return services;
    }

    internal static WebApplication UseRateLimiting(this WebApplication app)
    {
        app.UseMiddleware<CustomIpRateLimitMiddleware>();
        return app;
    }
}