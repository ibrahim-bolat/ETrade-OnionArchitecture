using System.Net;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NetTools;


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
        string endpointName = context.HttpContext.Request.RouteValues["action"] as string;
        string requestMethodType = context.HttpContext.Request.Method;
        string localIpAddress = context.HttpContext.Connection.LocalIpAddress?.ToString();
        string remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        int localPort = context.HttpContext.Connection.LocalPort;
        int remotePort = context.HttpContext.Connection.RemotePort;
        
        List<string> actionArguments = new List<string>();
        actionArguments.AddRange(context.ActionArguments.Select(a => a.ToString()));

        Endpoint endpoint = await _unitOfWork.GetRepository<Endpoint>().GetAsync(
            a => a.EndpointName == endpointName && a.ControllerName == controllerName &&
                 a.AreaName == areaName && a.HttpType == requestMethodType && a.IsActive, a => a.AppRoles,
            a => a.IpAddresses);

        AppUser user = null;
        var userName = context.HttpContext.User.Identity?.Name;
        if (userName != null)
            user = await _userManager.FindByNameAsync(userName);
        await _unitOfWork.GetRepository<RequestInfoLog>().AddAsync(new RequestInfoLog()
        {
            AreaName = areaName,
            ControllerName = controllerName,
            ActionName = endpointName,
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
        if (endpoint != null)
        {
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.Name != null)
            {
                var appRoles = await _userManager.GetRolesAsync(user);
                if (appRoles.Contains("Owner") || (endpoint.AppRoles.Any(r => appRoles.Contains(r.Name))) &&
                    IpCheck(remoteIpAddress, endpoint))
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

    private bool IpCheck(string remoteIpAddress, Endpoint endpoint)
    {
        if (endpoint.IpAddresses != null)
        {
            List<IpAddress> whiteList = endpoint.IpAddresses.Where(i => i.IpListType == IpListType.WhiteList).ToList();
            List<IpAddress> blackList = endpoint.IpAddresses.Where(i => i.IpListType == IpListType.BlackList).ToList();
            if (whiteList.Count != 0 && blackList.Count != 0)
            {
                if (whiteList.Any(i =>
                        IPAddressRange.Parse($"{i.RangeStart} - {i.RangeEnd}")
                            .Contains(IPAddress.Parse(remoteIpAddress))) && (blackList.Any(i =>
                        IPAddressRange.Parse($"{i.RangeStart} - {i.RangeEnd}")
                            .Contains(IPAddress.Parse(remoteIpAddress))) == false))
                {
                    return true;
                }
                return false;
            }

            if (whiteList.Count != 0 && blackList.Count == 0)
            {
                if (whiteList.Any(i =>
                        IPAddressRange.Parse($"{i.RangeStart} - {i.RangeEnd}")
                            .Contains(IPAddress.Parse(remoteIpAddress))))
                {

                    return true;
                }
                return false;
            }

            if (whiteList.Count == 0 && blackList.Count != 0)
            {
                if (blackList.Any(i =>
                        IPAddressRange.Parse($"{i.RangeStart} - {i.RangeEnd}")
                            .Contains(IPAddress.Parse(remoteIpAddress))) == false)
                {
                    return true;
                }
                return false;
            }
        }
        return true;
    }
}