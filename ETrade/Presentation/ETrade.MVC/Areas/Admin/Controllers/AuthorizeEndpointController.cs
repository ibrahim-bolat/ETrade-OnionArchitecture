using ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignRoleListAuthorizeEndpointsCommand;
using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignIpQuery;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsIpListQuery;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsRoleListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorizeEndpointController : Controller
{
    
    private readonly IMediator _mediator;

    public AuthorizeEndpointController(IMediator mediator)
    {
        _mediator = mediator;
    }
  
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public  async Task<IActionResult>  GetAuthorizeEndpointsforAssignRole(string query)
    {
         
        var dresult = await _mediator.Send(new GetAuthorizeEndpointsforAssignRoleQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }

    [HttpPost]
    public async Task<IActionResult>  AssignRolesByEndpointId(int id, List<int> roleIds)
    {
        if (id>0 && roleIds != null)
        {
            var dresult = await _mediator.Send(new AssignRoleListAuthorizeEndpointsCommandRequest()
            {
                Id=id,
                RoleIds = roleIds
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
        }
        return Json(new { success = false });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRolesByEndpointId(string id)
    {
        var dresult = await _mediator.Send(new GetByIdAuthorizeEndpointsRoleListQueryRequest()
        {
            Id=id,
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, roles = dresult.Result.Data });
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 400});
    }
    
    [HttpGet]
    public IActionResult AuthorizeEndpoints()
    {
        return View();
    }
    [HttpGet]
    public  async Task<IActionResult>  GetAuthorizeEndpointsforAssignIp(string query)
    {
         
        var dresult = await _mediator.Send(new GetAuthorizeEndpointsforAssignIpQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult>  AssignIpAddresses(int id, List<int> roleIds)
    {
        if (id>0 && roleIds != null)
        {
            /*
            var dresult = await _mediator.Send(new AssignRoleListAuthorizeEndpointsCommandRequest()
            {
                Id=id,
                RoleIds = roleIds
            });
   
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
                     */
        }
        return Json(new { success = false });
    }
    [HttpPost]
    public async Task<IActionResult> GetIpAdressesByEndpointId(int id)
    {
        var dresult = await _mediator.Send(new GetByIdAuthorizeEndpointsIpListQueryRequest()
        {
            Id=id,
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return PartialView("PartialViews/_EndpointIpModalPartial",dresult.Result.Data);
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 400});
    }
}