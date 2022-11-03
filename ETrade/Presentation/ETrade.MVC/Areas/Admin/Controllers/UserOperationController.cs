using ETrade.Application.Features.UserOperations.Commands.AssignUserRoleListCommand;
using ETrade.Application.Features.UserOperations.Commands.CreateUserCommand;
using ETrade.Application.Features.UserOperations.Commands.SetDeletedUserCommand;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Features.UserOperations.Queries.GetActiveUserListQuery;
using ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;
using ETrade.Application.Features.UserOperations.Queries.GetByIdUserRoleListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class UserOperationController : Controller
    {

        private readonly IMediator _mediator;

        public UserOperationController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult>  Users(DatatableRequestDto datatableRequestDto)
        {
 
            var dresult = await _mediator.Send(new GetActiveUserListQueryRequest()
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
        public async Task<IActionResult> Add(CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UserCreateModalPartial", createUserDto);
            }
            var dresult = await _mediator.Send(new CreateUserCommandRequest()
            {
                CreateUserDto = createUserDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_UserCreateModalPartial", createUserDto);   
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var dresult = await _mediator.Send(new GetByIdForUserSummaryQueryRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, user = dresult.Result.Data });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var dresult = await _mediator.Send(new SetDeletedUserCommandRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive",
                    Messages.UserNotActive);
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
            return Json(new { success = false});
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string id)
        {
            var dresult = await _mediator.Send(new GetByIdUserRoleListQueryRequest()
            {
                Id=id,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, roles = dresult.Result.Data });
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 400});
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(string id, List<String> roles)
        {
            if (!string.IsNullOrEmpty(id) && roles != null)
            {
                var dresult = await _mediator.Send(new AssignUserRoleListCommandRequest()
                {
                    Id=id,
                    Roles = roles
                });
                if (dresult.Result.ResultStatus == ResultStatus.Success)
                {
                    return Json(new { success = true });
                }
                return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
            }
            return Json(new { success = false });
        }
    }