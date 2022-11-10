using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.Features.UserImages.Commands.CreateUserImageCommand;
using ETrade.Application.Features.UserImages.Commands.DeleteUserImageCommand;
using ETrade.Application.Features.UserImages.Commands.SetProfilImageCommand;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = ETrade.Application.Features.UserImages.Constants.Messages;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class UserImageController : Controller
{
    private readonly IMediator _mediator;

    public UserImageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeEndpointConstants.UserImage, ActionType = ActionType.Reading, Definition = "Get By Id User for Create UserImage")]
    public IActionResult UserImageAdd(int userId)
    {
        UserImageAddDto userImageAddDto = new UserImageAddDto();
        userImageAddDto.UserId = userId;
        return View(userImageAddDto);
    }
    
    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeEndpointConstants.UserImage, ActionType = ActionType.Writing, Definition = "Create UserImage")]
    public async Task<IActionResult> UserImageAdd(UserImageAddDto userImageAddDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new CreateUserImageCommandRequest
            {
                UserImageAddDto = userImageAddDto, 
                CreatedByName = User.Identity?.Name
            });
            if (dresult.Result.Message == Messages.UserImageCountMoreThan4)
            { 
                ModelState.AddModelError("UserImageCountMoreThan4", Messages.UserImageCountMoreThan4);
                return View(userImageAddDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["AddUserImageSuccess"] = true;
                return RedirectToAction("UserImageAdd", "UserImage" ,new { area = "Admin" ,userId=userImageAddDto.UserId});
            }
        }
        return View(userImageAddDto);
    }
    [HttpPost]
    public async Task<IActionResult> UserImageSetProfil(int id,int userId)
    {
        if (id>0)
        {
            var dresult = await _mediator.Send(new SetProfilImageCommandRequest
            {
                Id = id,
                UserId = userId,
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
        
    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeEndpointConstants.UserImage, ActionType = ActionType.Deleting, Definition = "Delete UserImage")]
    public async Task<IActionResult> UserImageDelete(int id)
    {
        if (id > 0)
        {
            var dresult = await _mediator.Send(new DeleteUserImageCommandRequest
            {
                Id = id, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}