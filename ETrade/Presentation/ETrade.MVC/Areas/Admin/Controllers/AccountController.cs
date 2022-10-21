using ETrade.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;
using ETrade.Application.Features.Accounts.Commands.EditPasswordUserCommand;
using ETrade.Application.Features.Accounts.Commands.LoginUserCommand;
using ETrade.Application.Features.Accounts.Commands.RegisterUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdatePasswordUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdateUserCommand;
using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Features.Accounts.Queries.ForgetPasswordUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetByIdForEditPasswordUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetByIdUserQuery;
using ETrade.Application.Features.Accounts.Queries.LogoutUserQuery;
using ETrade.Application.Features.Accounts.Queries.VerifyTokenUserQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpGet] 
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new RegisterUserCommandRequest()
            {
                RegisterDto = registerDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["LoginSuccess"] = true;
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(registerDto);
            }
        }
        return View(registerDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{email}/{token}")] 
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var dresult = await _mediator.Send(new ConfirmEmailUserCommandRequest()
        {
            Email = email,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            ViewBag.State = true;
            return View("ConfirmEmail");
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ViewBag.State = false;
            return View("ConfirmEmail");
        }
        return View();

    }

    [AllowAnonymous]
    [HttpGet] 
    public IActionResult Login(string returnUrl = "Index")
    {
        TempData["returnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new LoginUserCommandRequest()
            {
                LoginDto = loginDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                    return RedirectToAction("Index", "Home");
                if (TempData["returnUrl"]!.Equals("Index") || TempData["returnUrl"].Equals("/"))
                    return RedirectToAction("Index", "Home");
                return Redirect(TempData["returnUrl"].ToString()!);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserAccountLocked))
            {
                ModelState.AddModelError("UserAccountLocked", Messages.UserAccountLocked);
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserIncorrectPassword))
            {
                ModelState.AddModelError("UserIncorrectPassword", Messages.UserIncorrectPassword);
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                return View(loginDto);
            }
        }
        return View(loginDto);
    }

    [HttpPost] 
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutUserQueryRequest());
        return Json(new { success = true });
    }
    [AllowAnonymous]
    [HttpGet] 
    public IActionResult ForgetPass()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> ForgetPass(ForgetPassDto forgetPassDto)
    {
        var dresult = await _mediator.Send(new ForgetPasswordUserQueryRequest()
        {
            ForgetPassDto = forgetPassDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData["EmailSendStatus"] = true;
            return View();
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserErrorSendEmail))
        {
            TempData["EmailSendStatus"] = false;
            return View();
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotActive))
        {
            ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
            return View(forgetPassDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
            return View(forgetPassDto);
        }
        return View(forgetPassDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(string userId, string token)
    {
        var dresult = await _mediator.Send(new VerifyTokenUserQueryRequest()
        {
            UserId=userId,
            Token=token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View();
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 400});
    }

    [AllowAnonymous]
    [HttpPost("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto, string userId, string token)
    {
        var dresult = await _mediator.Send(new UpdatePasswordUserCommandRequest()
        {
            UpdatePasswordDto = updatePasswordDto,
            UserId = userId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            TempData["UpdatePasswordStatus"] = true;
            return RedirectToAction("Login", "Account" ,new { area = "Admin"});
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserErrorUpdatePassword))
        {
            TempData["UpdatePasswordStatus"] = false;
            return View();
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotActive))
        {
            ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
            return View(updatePasswordDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
            return View(updatePasswordDto);
        }
        return View(updatePasswordDto);
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
        var dresult = await _mediator.Send(new GetByIdUserQueryRequest()
        {
            Id=id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(UserDto userDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new UpdateUserCommandRequest()
            {
                UserDto = userDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditProfileSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                return View(userDto);
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                return View(userDto);
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(userDto);
            }
        }
        return View(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> EditPassword(int id)
    {
        var dresult = await _mediator.Send(new GetByIdForEditPasswordUserQueryRequest()
        {
            Id=id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPassword(EditPasswordDto editPasswordDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new EditPasswordUserCommandRequest()
            {
                EditPasswordDto = editPasswordDto,

            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditPasswordSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                return View(editPasswordDto);
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                return View(editPasswordDto);
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(editPasswordDto);
            }
        }
        return View(editPasswordDto);
    }


    [HttpGet]
    public async Task<IActionResult> Profile(int id)
    {
        var dresult = await _mediator.Send(new GetByIdForDetailProfileUserQueryRequest()
        {
            Id=id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}