using ETrade.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;
using ETrade.Application.Features.Accounts.Commands.LoginUserCommand;
using ETrade.Application.Features.Accounts.Commands.RegisterUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdatePasswordUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdateUserCommand;
using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.Features.Accounts.Commands.EditPasswordAccountCommand;
using ETrade.Application.Features.Accounts.Commands.ExternalLoginUserCommand;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Features.Accounts.Queries.ForgetPasswordUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetByIdUserQuery;
using ETrade.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;
using ETrade.Application.Features.Accounts.Queries.GetExternalLoginAuthenticationPropertiesQuery;
using ETrade.Application.Features.Accounts.Queries.LogoutUserQuery;
using ETrade.Application.Features.Accounts.Queries.VerifyTokenUserQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
                    return RedirectToAction("Index", "Home",new { area = "Admin" });
                if (TempData["returnUrl"]!.Equals("Index") || TempData["returnUrl"].Equals("/"))
                    return RedirectToAction("Index", "Home",new { area = "Admin" });
                return LocalRedirect(TempData["returnUrl"].ToString()!);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserAccountLocked))
            {
                ModelState.AddModelError("UserAccountLocked", Messages.UserAccountLocked);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserIncorrectPassword))
            {
                ModelState.AddModelError("UserIncorrectPassword", Messages.UserIncorrectPassword);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                return View(loginDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                return View(loginDto);
            }
        }
        return View(loginDto);
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLogin(string providerName,bool isPersistent,string returnUrl)
    {
        var dresult = await _mediator.Send(new GetExternalLoginAuthenticationPropertiesQueryRequest()
        {
            ProviderName = providerName,
            RedirectUrl = Url.Action("ExternalLoginResponse", "Account", new {providerName=providerName,isPersistent=isPersistent ,returnUrl = returnUrl})
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return new ChallengeResult(providerName.Trim(), dresult.Result.Data);
        }
        return RedirectToAction("Login", "Account", new { area = "Admin" });
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ExternalLoginResponse(string providerName,bool isPersistent,string returnUrl = "Index")
    {
        var dresult = await _mediator.Send(new ExternalLoginUserCommandRequest()
        {
            IsPersistent = isPersistent
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserNotActive))
        {
            ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
            return View("Login");
        }
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            if (returnUrl.Equals("Index") || returnUrl.Equals("/"))
            {
                return RedirectToAction("Index", "Home",new { area="Admin" });
            }
            return LocalRedirect(returnUrl);
        }
        TempData[$"{providerName}LoginStatus"] = false;
        return RedirectToAction("Login", "Account", new { area = "Admin" });
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

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserErrorSendEmail))
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
            UserId = userId,
            Token = token
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View();
        }

        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
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
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.UserErrorUpdatePassword))
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
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Account, EndpointType = EndpointType.Reading,
        Definition = "Get By Id User for Edit Profile")]
    public async Task<IActionResult> EditProfile(int id)
    {
        var dresult = await _mediator.Send(new GetByIdUserQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }

        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Account, EndpointType = EndpointType.Updating,
        Definition = "Edit Profile")]
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

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(userDto);
            }
        }

        return View(userDto);
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Account, EndpointType = EndpointType.Reading,
        Definition = "Get Edit Password Account By Id")]
    public async Task<IActionResult> EditPasswordAccount(int id)
    {
        var dresult = await _mediator.Send(new GetEditPasswordAccountByIdQueryRequest()
        {
            Id = id.ToString()
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Account, EndpointType = EndpointType.Updating,
        Definition = "Edit Password Account")]
    public async Task<IActionResult> EditPasswordAccount(EditPasswordAccountDto editPasswordAccountDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new EditPasswordAccountCommandRequest()
            {
                EditPasswordAccountDto = editPasswordAccountDto,
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditPasswordSuccess"] = true;
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                return View(editPasswordAccountDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList != null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(editPasswordAccountDto);
            }
        }

        return View(editPasswordAccountDto);
    }


    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Account, EndpointType = EndpointType.Reading,
        Definition = "Get By Id User for Profile Details")]
    public async Task<IActionResult> Profile(int id)
    {
        var dresult = await _mediator.Send(new GetByIdForDetailProfileUserQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error", new { area = "", statusCode = 400 });
    }
}