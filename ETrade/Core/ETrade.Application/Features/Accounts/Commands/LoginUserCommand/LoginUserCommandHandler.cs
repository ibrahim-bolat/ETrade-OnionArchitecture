using ETrade.Application.Constants;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.LoginUserCommand;

public class LoginUserCommandHandler:IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginUserCommandHandler(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
            AppUser user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            if (user != null)
            {
                if (user.IsActive)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, request.LoginDto.Password, request.LoginDto.Persistent, request.LoginDto.Lock);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        return new LoginUserCommandResponse
                        {
                            Result = new DataResult<LoginDto>(ResultStatus.Success, Messages.UserLoggedIn, request.LoginDto)
                        };
                    }
                    await _userManager.AccessFailedAsync(user);
                    int failcount = await _userManager.GetAccessFailedCountAsync(user);
                    if (failcount == 3)
                    {
                        await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(30)));
                        return new LoginUserCommandResponse{
                            Result = new Result(ResultStatus.Error, Messages.UserAccountLocked)
                        };
                    }
                    if (result.IsLockedOut)
                    {
                        return new LoginUserCommandResponse{
                            Result = new Result(ResultStatus.Error, Messages.UserAccountLocked)
                        };
                    }
                    return new LoginUserCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.UserIncorrectPassword)
                    };
                }
                return new LoginUserCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                };
            }
            return new LoginUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
    }
}