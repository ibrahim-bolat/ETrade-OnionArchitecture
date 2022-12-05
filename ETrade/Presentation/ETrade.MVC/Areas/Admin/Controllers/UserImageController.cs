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
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.UserImage, EndpointType = EndpointType.Reading, Definition = "Get By Id User for Create UserImage")]
    public IActionResult CreateUserImage(int userId)
    {
        CreateUserImageDto createUserImageDto = new CreateUserImageDto();
        createUserImageDto.UserId = userId;
        return View(createUserImageDto);
    }
    
    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.UserImage, EndpointType = EndpointType.Writing, Definition = "Create UserImage")]
    public async Task<IActionResult> CreateUserImage(CreateUserImageDto createUserImageDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new CreateUserImageCommandRequest
            {
                UserImageAddDto = createUserImageDto, 
                CreatedByName = User.Identity?.Name
            });
            if (dresult.Result.Message == Messages.UserImageCountMoreThan4)
            { 
                ModelState.AddModelError("UserImageCountMoreThan4", Messages.UserImageCountMoreThan4);
                return View(createUserImageDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["CreateUserImageSuccess"] = true;
                return RedirectToAction("createUserImage", "UserImage" ,new { area = "Admin" ,userId=createUserImageDto.UserId});
            }
        }
        return View(createUserImageDto);
    }
    [HttpPost]
    public async Task<IActionResult> SetProfilImage(int id,int userId)
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
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
    
        
    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.UserImage, EndpointType = EndpointType.Deleting, Definition = "Delete UserImage")]
    public async Task<IActionResult> DeleteUserImage(int id)
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
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
}