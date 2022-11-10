using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.UserOperations.Commands.SetActiveUserCommand;
using ETrade.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DeletedAccountController : Controller
{
    private readonly IMediator _mediator;

    public DeletedAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeEndpointConstants.DeletedAccount, ActionType = ActionType.Reading, Definition = "Get DeletedAccount Index Page")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> DeletedUsers(DatatableRequestDto datatableRequestDto)
    {
        var dresult = await _mediator.Send(new GetDeletedUserListQueryRequest()
        {
            DatatableRequestDto = datatableRequestDto
        });
        var jsonData = new
        {
            draw = dresult.Result.Data.Draw, recordsFiltered = dresult.Result.Data.RecordsFiltered, 
            recordsTotal = dresult.Result.Data.RecordsTotal, data = dresult.Result.Data.Data, isSusccess = true
        };
        return Ok(jsonData);
    }
    
    [HttpPost]
    public async Task<IActionResult> SetActiveUser(int userId)
    {
        var dresult = await _mediator.Send(new SetActiveUserCommandRequest()
        {
            Id = userId.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserActive))
        {
            ModelState.AddModelError("UserActive",Messages.UserActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
        {
            dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}