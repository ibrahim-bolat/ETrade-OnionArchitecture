using System.Web;
using AutoMapper;
using ETrade.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;
using ETrade.Application.Features.Accounts.Commands.ForgetPasswordUserCommand;
using ETrade.Application.Features.Accounts.Commands.LoginUserCommand;
using ETrade.Application.Features.Accounts.Commands.RegisterUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdatePasswordUserCommand;
using ETrade.Application.Features.Accounts.Commands.UpdateUserCommand;
using ETrade.Application.Features.Accounts.Commands.VerifyTokenUserCommand;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Features.Accounts.Queries.GetByIdUserQuery;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager,
        IMapper mapper,
        IUserService userService,
        IMediator mediator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _userService = userService;
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
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserLocked))
            {
                ModelState.AddModelError("Locked",
                    "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserIncorrectPassword))
            {
                ModelState.AddModelError("IncorrectPassword", "Yanlış Şifre Girdiniz.");
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için giriş yapamamaktadır.");
                return View(loginDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
                return View(loginDto);
            }
        }
        return View(loginDto);
    }

    [HttpGet] 
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
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
        var dresult = await _mediator.Send(new ForgetPasswordUserCommandRequest()
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
            ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
            return View(forgetPassDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
            return View(forgetPassDto);
        }
        return View(forgetPassDto);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(string userId, string token)
    {
        var dresult = await _mediator.Send(new VerifyTokenUserCommandRequest()
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
            ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
            return View(updatePasswordDto);
        }
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.UserNotFound))
        {
            ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
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
            TempData["oldEmail"] = dresult.Result.Data.Email;
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
                UserDto = userDto,
                OldEmail = TempData["oldEmail"]?.ToString()

            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["EditProfileSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserDeleted",
                    "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
                return View(userDto);
            }
            
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
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
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (user.IsActive)
                {
                    EditPasswordDto editPasswordDto = _mapper.Map<EditPasswordDto>(user);
                    return View(editPasswordDto);
                }
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPassword(EditPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());
                if(user!=null)
                {
                    if (user.IsActive)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                            return View(model);
                        }
                        result = await _userManager.UpdateSecurityStampAsync(user);
                        if (!result.Succeeded)
                        {
                            result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                            return View(model);
                        }

                        if (User.Identity?.Name == user.UserName)
                        {
                            await _signInManager.SignOutAsync();
                            await _signInManager.SignInAsync(user, true);
                        }
                        TempData["EditPasswordSuccess"] = true;
                        return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
                    }
                    ModelState.AddModelError("UserDeleted",
                        "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
                    return View(model);
                }
                return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
        }
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Profile(int id)
    {
        if (id > 0)
        {
            var dresult = await _userService.GetWithAddressAsync(id);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Data);
            }
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}