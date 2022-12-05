using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.EditPasswordUserCommand;

public class UpdatePasswordUserCommandHandler:IRequestHandler<EditPasswordUserCommandRequest,EditPasswordUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePasswordUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EditPasswordUserCommandResponse> Handle(EditPasswordUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user = await _userManager.FindByIdAsync(request.EditPasswordDto.Id.ToString());
        if(user!=null)
        {
            if (user.IsActive)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                result = await _userManager.ResetPasswordAsync(user, token, request.EditPasswordDto.NewPassword);
                if (!result.Succeeded)
                {
                    return new EditPasswordUserCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.UserErrorUpdatePassword,result.Errors.ToList())
                    };
                }
                result = await _userManager.UpdateSecurityStampAsync(user);
                if (!result.Succeeded)
                {
                    return new EditPasswordUserCommandResponse{
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp,result.Errors.ToList())
                    };
                }
                string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                if (!string.IsNullOrEmpty(userIdentityName) && user.UserName.Equals(userIdentityName))
                {
                    await _signInManager.RefreshSignInAsync(user);
                }
                return new EditPasswordUserCommandResponse{
                    Result = new Result(ResultStatus.Success, Messages.UserSuccessUpdatePassword)
                };
            }
            return new EditPasswordUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotActive)
            };
        }
        return new EditPasswordUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}