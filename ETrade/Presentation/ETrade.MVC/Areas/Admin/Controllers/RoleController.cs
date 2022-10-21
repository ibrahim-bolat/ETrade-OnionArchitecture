using ETrade.Application.Features.RoleOperations.Commands.CreateRoleCommand;
using ETrade.Application.Features.RoleOperations.Commands.SetDeletedRoleCommand;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult>  Index()
        {
            var dresult = await _mediator.Send(new GetRoleListQueryRequest());
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            return View();
        }
        
        [HttpGet]
        public  IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                var dresult = await _mediator.Send(new CreateRoleCommandRequest()
                {
                    RoleDto = roleDto
                });
                if (dresult.Result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("Index", "Role" ,new {area="Admin"});
                }
                if (dresult.Result.ResultStatus == ResultStatus.Error &&
                    dresult.Result.Message.Equals(Messages.RoleActive))
                {
                    ModelState.AddModelError("RoleActive", "Böyle bir Role zaten tanımlı.");
                    return View(roleDto);
                }
                if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
                {
                    dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(roleDto);
                }
            }
            return View(roleDto);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var dresult = await _mediator.Send(new SetDeletedRoleCommandRequest()
            {
                Id = id
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("NoRole", "Böyle bir rol bulunmamaktadır.");
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