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
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Application.Extensions;

public static class AuthorizeEndpointsMigrateExtension
{
    public static async Task AuthorizeEndpointsMigrateAsync(this WebApplication app, Type type)
    {
        await using (var scope = app.Services.CreateAsyncScope())
        {
            await using (var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>())
            {
                if (unitOfWork != null)
                {
                    try
                    {
                        Assembly assembly = Assembly.GetAssembly(type);
                        var controllers = assembly!.GetTypes().Where(t => t.IsAssignableTo(typeof(Controller)));
                        foreach (var controller in controllers)
                        {
                            Menu menu = null;
                            var actions = controller.GetMethods()
                                .Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                            foreach (var action in actions)
                            {
                                var attributes = action.GetCustomAttributes(true);

                                var authorizeDefinitionAttribute =
                                    attributes.FirstOrDefault(a =>
                                            a.GetType() == typeof(AuthorizeDefinitionAttribute)) as
                                        AuthorizeDefinitionAttribute;
                                
                                
                                var controllerAttributes = controller.GetCustomAttributes(true);
                                var areaAttribute =
                                    controllerAttributes.FirstOrDefault(a =>
                                            a.GetType() == typeof(AreaAttribute)) as
                                        AreaAttribute;
                                var areaAttributeName = areaAttribute?.RouteValue;
                                
                                if (authorizeDefinitionAttribute != null)
                                {
                                    if (menu == null)
                                    {
                                        
                                        menu = new Menu()
                                        {
                                            ControllerName  = controller.Name.Split("Controller").FirstOrDefault(),
                                            AreaName = areaAttributeName,
                                            Definition = authorizeDefinitionAttribute.Menu
                                        };
                                        if (await unitOfWork.GetRepository<Menu>().AnyAsync(m => m.ControllerName == menu.ControllerName))
                                        {
                                            menu = await unitOfWork.GetRepository<Menu>().GetAsync(m => m.ControllerName == menu.ControllerName,m=>m.Actions);
                                        }
                                    }

                                    Action newAction = new()
                                    {
                                        ActionName = action.Name,
                                        ControllerName = controller.Name.Split("Controller").FirstOrDefault(),
                                        AreaName = areaAttributeName,
                                        ActionType = Enum.GetName(typeof(ActionType),
                                            authorizeDefinitionAttribute.ActionType),
                                        Definition = authorizeDefinitionAttribute.Definition
                                    };

                                    var httpAttribute = attributes.FirstOrDefault(a =>
                                            a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as
                                        HttpMethodAttribute;
                                    if (httpAttribute != null)
                                        newAction.HttpType = httpAttribute.HttpMethods.First();
                                    else
                                        newAction.HttpType = HttpMethods.Get;

                                    newAction.Code =
                                        $"{newAction.HttpType}.{newAction.ActionType}.{newAction.Definition.Replace(" ", "")}";

                                    if (!menu.Actions.Any(a => a.Code == newAction.Code))
                                    {
                                        menu.Actions.Add(newAction);
                                    }
                                }
                            }

                            if ( menu != null && !await unitOfWork.GetRepository<Menu>().AnyAsync(m => m.ControllerName == menu.ControllerName))
                            {
     
                                await unitOfWork.GetRepository<Menu>().AddAsync(menu);
                            }

                            if (menu != null && await unitOfWork.GetRepository<Menu>().AnyAsync(m => m.ControllerName == menu.ControllerName))
                            {
                                await unitOfWork.GetRepository<Menu>().UpdateAsync(menu);
                            }
                        }
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