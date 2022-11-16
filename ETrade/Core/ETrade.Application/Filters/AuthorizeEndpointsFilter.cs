using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
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
        string requestMethodType = context.HttpContext.Request.Method;
        string localIpAddress = context.HttpContext.Connection.LocalIpAddress?.ToString();        
        string remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        int localPort = context.HttpContext.Connection.LocalPort;
        int remotePort = context.HttpContext.Connection.RemotePort;
        
        
        //string Id = context.ActionArguments["id"] as string;
        List<string> actionArguments = new List<string>();
        actionArguments.AddRange(context.ActionArguments.Select(a=>a.ToString()));

        Action action = await _unitOfWork.GetRepository<Action>().GetAsync(
                a => a.ActionName == actionName && a.ControllerName == controllerName  && 
                     a.AreaName == areaName && a.HttpType == requestMethodType && a.IsActive, a => a.AppRoles);

        AppUser user = null;
        var userName = context.HttpContext.User.Identity?.Name;
        if ( userName!= null)
            user = await _userManager.FindByNameAsync(userName);
        await _unitOfWork.GetRepository<RequestInfoLog>().AddAsync(new RequestInfoLog()
        {
            AreaName = areaName,
            ControllerName = controllerName,
            ActionName = actionName,
            RequestMethodType = requestMethodType,
            ActionArguments = actionArguments,
            DateTime = DateTime.Now,
            LocalIpAddress = localIpAddress,
            RemoteIpAddress = remoteIpAddress,
            LocalPort = localPort,
            RemotePort = remotePort,
            UserId = user?.Id
        });
        await _unitOfWork.SaveAsync();
        if (action != null)
        {
            if (context.HttpContext.User.Identity!=null && context.HttpContext.User.Identity.Name != null)
            {
                var appRoles = await _userManager.GetRolesAsync(user);
                if (appRoles.Contains("Owner") || action.AppRoles.Any(r => appRoles.Contains(r.Name)))
                {
                    await next();
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
        else
        {
            await next();
        }
    }
}