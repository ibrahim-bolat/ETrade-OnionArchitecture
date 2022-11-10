using ETrade.Application.Model;
using ETrade.Application.Services;
using ETrade.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETrade.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        //configure
        serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        //services
        serviceCollection.AddTransient<IEmailService, EmailService>();
    }
}