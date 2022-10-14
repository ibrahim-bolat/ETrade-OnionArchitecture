using ETrade.Application.Features.Accounts.Commands.SetActiveUserCommand;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.Queries.GetDeletedUserListQuery;
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
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> DeletedUsers()
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request
                .Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var dresult = await _mediator.Send(new GetDeletedUserListQueryRequest()
            {
                Draw = draw,
                Start = start != null ? Convert.ToInt32(start) : 0,
                Length = length == "-1" ? -1 : length != null ? Convert.ToInt32(length) : 0,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue
            });
            var jsonData = new
            {
                draw = dresult.Result.Data, recordsFiltered = dresult.Result.Data.RecordsFiltered, recordsTotal = dresult.Result.Data.RecordsTotal, data = dresult.Result.Data.UserSummaryDtos, 
                isSusccess = dresult.Result.Data.IsSuccess
            };
            return Ok(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Json(new { success = false });
        }
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
            ModelState.AddModelError("UserActive",
                "Bu E-posta ya sahip kullanıcı zaten aktif bir kullancıdır.");
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
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