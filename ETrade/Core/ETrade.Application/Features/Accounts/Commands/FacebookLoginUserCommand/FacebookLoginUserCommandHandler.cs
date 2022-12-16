using System.Security.Claims;
using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.FacebookLoginUserCommand;

public class FacebookLoginUserCommandHandler : IRequestHandler<FacebookLoginUserCommandRequest, FacebookLoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public FacebookLoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            return new FacebookLoginUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.LoginInfoNotFound)
            };
        }

        SignInResult loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider,
            loginInfo.ProviderKey, request.IsPersistent);
        if (loginResult.Succeeded)
        {
            return new FacebookLoginUserCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.UserLoggedIn)
            };
        }
        AppUser user = new AppUser
        {
            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),           
            FirstName = loginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname)
        };
        IdentityResult createResult = await _userManager.CreateAsync(user);
        if (createResult.Succeeded)
        {
            IdentityResult addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
            if (addLoginResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, request.IsPersistent);
                return new FacebookLoginUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserLoggedIn)
                };
            }
            return new FacebookLoginUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotLoggedIn,addLoginResult.Errors.ToList())
            };
        }
        return new FacebookLoginUserCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.UserNotAdded,createResult.Errors.ToList())
        };
    }
}