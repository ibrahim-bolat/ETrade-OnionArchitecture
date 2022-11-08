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
    public static async Task<WebApplication> AuthorizeEndpointsMigrateAsync(this WebApplication app, Type type)
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
                            Menu menu = new Menu();
                            var actions = controller.GetMethods()
                                .Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                            foreach (var action in actions)
                            {
                                var attributes = action.GetCustomAttributes(true);

                                var authorizeDefinitionAttribute =
                                    attributes.FirstOrDefault(a =>
                                            a.GetType() == typeof(AuthorizeDefinitionAttribute)) as
                                        AuthorizeDefinitionAttribute;

                                if (authorizeDefinitionAttribute != null)
                                {
                                    if (menu.Name == null)
                                    {
                                        menu.Name = authorizeDefinitionAttribute.Menu;
                                        if (await unitOfWork.MenuRepository.AnyAsync(m => m.Name == menu.Name))
                                        {
                                            menu = await unitOfWork.MenuRepository.GetAsync(m => m.Name == menu.Name);
                                        }
                                    }

                                    Action newAction = new()
                                    {
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

                                    if (!await unitOfWork.ActionRepository.AnyAsync(a =>
                                            a.Code == newAction.Code))
                                    {
                                        menu.Actions.Add(newAction);
                                    }
                                }
                            }

                            if (!await unitOfWork.MenuRepository.AnyAsync(m => m.Name == menu.Name) &&
                                menu.Name != null)
                            {
                                await unitOfWork.MenuRepository.AddAsync(menu);
                            }

                            if (await unitOfWork.MenuRepository.AnyAsync(m => m.Name == menu.Name) && menu.Name != null)
                            {
                                await unitOfWork.MenuRepository.UpdateAsync(menu);
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

        return app;
    }
}