using ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignRoleListAuthorizeEndpointsCommand;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsQuery;
using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsRoleListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorizeEndpointsController : Controller
{
    
    private readonly IMediator _mediator;

    public AuthorizeEndpointsController(IMediator mediator)
    {
        _mediator = mediator;
    }
  
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public  async Task<IActionResult>  GetAuthorizeEndpoints(string query)
    {
         
        var dresult = await _mediator.Send(new GetAuthorizeEndpointsQueryRequest()
        {
            Query = query
        });
        return Ok(dresult.Result.Data);
    }

    [HttpPost]
    public async Task<IActionResult>  AssignRoleListAuthorizeEndpoints(int id, List<int> roleIds)
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
    public async Task<IActionResult> GetRole(string id)
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

}