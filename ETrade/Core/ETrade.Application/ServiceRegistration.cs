using System.Reflection;
using ETrade.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ETrade.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection  serviceCollection,IConfiguration configuration)
    {
        //auto mapper
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        //mediatR
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        
        //fluent validation
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddFluentValidationClientsideAdapters();
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //services
        serviceCollection.AddScoped<IUserImageService, UserImageManager>();

        //HttpContext and UrlHelper
        serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddScoped(x =>
                x.GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext!));
    }
}