using ETrade.Application.Constants;
using ETrade.Application.DTOs.UserImageDtos;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class UserImageController : Controller
{
    private readonly IUserImageService _userImageService;

    public UserImageController(IUserImageService userImageService)
    {
        _userImageService = userImageService;
    }

    [HttpGet]
    public IActionResult UserImageAdd(int userId)
    {
        UserImageAddDto userImageAddDto = new UserImageAddDto();
        userImageAddDto.UserId = userId;
        return View(userImageAddDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> UserImageAdd(UserImageAddDto userImageAddDto)
    {
        if (ModelState.IsValid)
        {
            var dresult= await _userImageService.AddAsync(userImageAddDto, User.Identity?.Name);
            if (dresult.Message == Messages.UserImageCountMoreThan4)
            { 
                ModelState.AddModelError("UserImageCountMoreThan4", Messages.UserImageCountMoreThan4);
                return View(userImageAddDto);
            }
            if (dresult.ResultStatus == ResultStatus.Success)
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
            var dresult= await _userImageService.SetProfilImageAsync(id,userId, User.Identity?.Name);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
        
    [HttpPost]
    public async Task<IActionResult> UserImageDelete(int id)
    {
        if (id > 0)
        {
            var dresult= await _userImageService.DeleteAsync(id, User.Identity?.Name);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}