using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Application.Filters;

public class AuthorizeEndpointsFilter : IAsyncActionFilter
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizeEndpointsFilter(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string areaName = context.HttpContext.Request.RouteValues["area"] as string;
        string controllerName = context.HttpContext.Request.RouteValues["controller"] as string;
        string actionName = context.HttpContext.Request.RouteValues["action"] as string;
        //string Id = context.ActionArguments["id"] as string;
        Action action;
        if (areaName != null)
        {
            action = await _unitOfWork.ActionRepository.GetAsync(
                a => a.ActionName == actionName && a.ControllerName == controllerName  && 
                     a.AreaName == areaName && a.IsActive, a => a.AppRoles);
        }
        else
        {
            action = await _unitOfWork.ActionRepository.GetAsync(
                a => a.ActionName == actionName && a.ControllerName == controllerName  && 
                     a.AreaName == null && a.IsActive, a => a.AppRoles);
        }
        if (action != null)
        {
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.Name != null)
            {
                AppUser user = await _userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);
                var appRoles = await _userManager.GetRolesAsync(user);
                if (appRoles.Contains("Owner") || action.AppRoles.Any(r => appRoles.Contains(r.Name)))
                {
                    await next();
                }
            }
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary()
                {
                    { "action", "AllErrorPages" },
                    { "controller", "ErrorPages" },
                    { "area", "" },
                    { "statusCode", 401 },
                });
        }
        else
        {
            await next();
        }
    }
}