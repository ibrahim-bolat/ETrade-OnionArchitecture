using System.Reflection;
using ETrade.Application.Services.Abstract;
using ETrade.Application.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ETrade.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection  serviceCollection,IConfiguration configuration)
    {
        //auto mapper
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        //fluent validation
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddFluentValidationClientsideAdapters();
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        //services
        serviceCollection.AddScoped<IAddressService, AddressManager>();
        serviceCollection.AddScoped<IUserImageService, UserImageManager>();
        serviceCollection.AddScoped<IUserService, UserManager>();
    }
}