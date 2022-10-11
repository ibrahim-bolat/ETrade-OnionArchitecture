using System.Web;
using AutoMapper;
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Model;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        RoleManager<AppRole> roleManager, 
        IMapper mapper, 
        IEmailService emailService,
        IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _emailService = emailService;
        _userService = userService;
    }
    
    [AllowAnonymous]
    [HttpGet] 
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            AppUser applicationUser = _mapper.Map<AppUser>(model);
            AppRole role = await _roleManager.FindByNameAsync(RoleType.User.ToString());
            if (role == null)
                await _roleManager.CreateAsync(new AppRole { Name = RoleType.User.ToString() });
            IdentityResult userResult = await _userManager.CreateAsync(applicationUser, model.Password);
            IdentityResult roleResult;
            if (userResult.Succeeded)
            {
                roleResult = await _userManager.AddToRoleAsync(applicationUser, RoleType.User.ToString());
                if (roleResult.Succeeded)
                {
                    TempData["LoginSuccess"] = true;
                    return RedirectToAction("Login", "Account", new { area = "Admin" });
                }
                roleResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(model);
            }
            userResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return View(model);
        }
        return View(model);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{email}/{token}")] 
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ViewBag.State = false;
            return View("ConfirmEmail");
        }
        var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
        if (result.Succeeded)
        {
            ViewBag.State = true;
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
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.IsActive)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult result =
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                            return RedirectToAction("Index", "Home");
                        if (TempData["returnUrl"]!.Equals("Index") || TempData["returnUrl"].Equals("/"))
                            return RedirectToAction("Index", "Home");

                        return Redirect(TempData["returnUrl"].ToString()!);
                    }

                    await _userManager.AccessFailedAsync(user);
                    int failcount = await _userManager.GetAccessFailedCountAsync(user);
                    if (failcount == 3)
                    {
                        await _userManager.SetLockoutEndDateAsync(user,
                            new DateTimeOffset(DateTime.Now
                                .AddMinutes(30)));
                        ModelState.AddModelError("Locked",
                            "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                        return View(model);
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("Locked",
                            "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                        return View(model);
                    }

                    ModelState.AddModelError("IncorrectPassword", "Yanlış Şifre Girdiniz.");
                    return View(model);
                }
                ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için giriş yapamamaktadır.");
                return View(model);
            }
            ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
        }
        return View(model);
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
    public async Task<IActionResult> ForgetPass(ForgetPassDto model)
    {
        AppUser user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            if (user.IsActive)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Url.Action("UpdatePassword", "Account",
                    new { area = "Admin", userId = user.Id, token = HttpUtility.UrlEncode(token) }, Request.Scheme);
                MailRequest mailRequest = new MailRequest
                {
                    ToMail = model.Email,
                    DisplayName = "Bolat A.Ş.",
                    ConfirmationLink = confirmationLink,
                    MailSubject = "Şifre Güncelleme Talebi",
                    IsBodyHtml = true,
                    MailLinkTitle = "Yeni şifre talebi için tıklayınız"
                };
                bool emailResponse = _emailService.SendEmail(mailRequest);
                if (emailResponse)
                {
                    TempData["EmailSendStatus"] = true;
                    return View();
                }
                TempData["EmailSendStatus"] = false;
                return View();
            }
            ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
            return View(model);
        }
        ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
        return View(model);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(string userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            string verifyToken = HttpUtility.UrlDecode(token);
            bool result = await  _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", verifyToken);
            if(result)
                return View();
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 400});
    }

    [AllowAnonymous]
    [HttpPost("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto model, string userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            if (user.IsActive)
            {
                IdentityResult result =
                    await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
                if (result.Succeeded)
                {
                    TempData["UpdatePasswordStatus"] = true;
                    await _userManager.UpdateSecurityStampAsync(user);
                    return RedirectToAction("Login", "Account" ,new { area = "Admin"});
                }
                TempData["UpdatePasswordStatus"] = false;
                return View();
            }
            ModelState.AddModelError("UserDeleted", "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
            return View(model);
        }
        ModelState.AddModelError("NoUser", "Böyle bir E-posta ya sahip kullanıcı bulunmamaktadır.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (user.IsActive)
                {
                    UserDto userDto = _mapper.Map<UserDto>(user);
                    TempData["oldEmail"] = user.Email;
                    return View(userDto);
                }
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDto model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                AppUser user;
                if (model.Email.Equals(TempData["oldEmail"]?.ToString()))
                {
                    user =  await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            user = _mapper.Map(model, user);
                        }
                        else
                        {
                            ModelState.AddModelError("UserDeleted",
                                "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
                    }
                }
                else
                {
                    user =  await _userManager.FindByEmailAsync(TempData["oldEmail"]?.ToString());
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            user = _mapper.Map(model, user);
                            result = await _userManager.SetEmailAsync(user, model.Email);
                            if (!result.Succeeded)
                            {
                                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                                return View(model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("UserDeleted",
                                "Bu E-posta ya sahip kullanıcı silindiği için bu işlemi yapamaz.");
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
                    }
                }
                result = await _userManager.SetUserNameAsync(user, model.UserName);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                result = await _userManager.UpdateAsync(user);
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
                if (User.Identity?.Name==TempData["oldEmail"]?.ToString() && model.Email != TempData["oldEmail"]?.ToString())
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                }
                TempData["EditProfileSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
            return View(model);
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