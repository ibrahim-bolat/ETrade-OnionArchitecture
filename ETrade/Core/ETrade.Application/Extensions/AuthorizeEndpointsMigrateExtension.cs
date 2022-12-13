using System.Reflection;
using ETrade.Application.CustomAttributes;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = ETrade.Domain.Entities.Endpoint;

namespace ETrade.Application.Extensions;

public static class AuthorizeEndpointsMigrateExtension
{
    public static async Task AuthorizeEndpointsMigrateAsync(this WebApplication app, Type type)
    {
        await using (var scope = app.Services.CreateAsyncScope())
        {
            await using (var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>())
            {
                if (unitOfWork != null)
                {
                    try
                    {
                        List<Endpoint> endpoints = await unitOfWork.GetRepository<Endpoint>().GetAllAsync(predicate:e => e.IsActive);
                        Assembly assembly = Assembly.GetAssembly(type);
                        var controllers = assembly!.GetTypes().Where(t => t.IsAssignableTo(typeof(Controller)));
                        foreach (var controller in controllers)
                        {
                            var endPoints = controller.GetMethods()
                                .Where(m => m.IsDefined(typeof(AuthorizeEndpointAttribute)));

                            foreach (var endPoint in endPoints)
                            {
                                var attributes = endPoint.GetCustomAttributes(true);

                                var authorizeDefinitionAttribute =
                                    attributes.FirstOrDefault(a =>
                                            a.GetType() == typeof(AuthorizeEndpointAttribute)) as
                                        AuthorizeEndpointAttribute;
                                
                                
                                if (authorizeDefinitionAttribute != null)
                                {
                                    var controllerAttributes = controller.GetCustomAttributes(true);
                                    var areaAttribute =
                                        controllerAttributes.FirstOrDefault(a =>
                                                a.GetType() == typeof(AreaAttribute)) as
                                            AreaAttribute;
                                    var areaAttributeName = areaAttribute?.RouteValue;
                                    
                                    string endpointType = Enum.GetName(typeof(EndpointType),
                                        authorizeDefinitionAttribute.EndpointType);
                                    string definition = authorizeDefinitionAttribute.Definition;
                                    
                                    var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                    string httpType = null;
                                    if (httpAttribute != null)
                                         httpType = httpAttribute.HttpMethods.First();
                                    else
                                        httpType = HttpMethods.Get;
                                    string code = $"{httpType}.{endpointType}.{definition.Replace(" ", "")}";
                                    
                                    Endpoint newEndpoint = null;
                                    if (!endpoints.Any(e => e.Code == code))
                                    {
                                        string endpointName = endPoint.Name;
                                        string controllerName = controller.Name.Split("Controller").FirstOrDefault();
                                        string areaName = areaAttributeName;
                                        newEndpoint = new()
                                        {
                                            EndpointName = endpointName,
                                            ControllerName = controllerName,
                                            AreaName = areaName,
                                            EndpointType = Enum.GetName(typeof(EndpointType),
                                                authorizeDefinitionAttribute.EndpointType),
                                            HttpType = httpType,
                                            Definition = definition,
                                            Code = code
                                        };
                                        endpoints.Add(newEndpoint);
                                    }
                                }
                            }
                        }
                        await unitOfWork.GetRepository<Endpoint>().UpdateRangeAsync(endpoints);
                        await unitOfWork.SaveAsync();
                    }
                    finally
                    {
                        await unitOfWork.DisposeAsync();
                    }
                }
            }
        }
    }
}