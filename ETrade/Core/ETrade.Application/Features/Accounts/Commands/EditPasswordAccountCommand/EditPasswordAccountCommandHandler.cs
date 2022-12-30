using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.EditPasswordAccountCommand;

public class EditPasswordAccountCommandHandler:IRequestHandler<EditPasswordAccountCommandRequest,EditPasswordAccountCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditPasswordAccountCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EditPasswordAccountCommandResponse> Handle(EditPasswordAccountCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user = await _userManager.FindByIdAsync(request.EditPasswordAccountDto.Id.ToString());
        string contextUserName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (user.UserName.Equals(contextUserName))
        {
            if (user != null)
            {
                if (user.IsActive)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    result = await _userManager.ResetPasswordAsync(user, token,
                        request.EditPasswordAccountDto.NewPassword);
                    if (!result.Succeeded)
                    {
                        return new EditPasswordAccountCommandResponse
                        {
                            Result = new Result(ResultStatus.Error, Messages.UserErrorUpdatePassword,
                                result.Errors.ToList())
                        };
                    }

                    result = await _userManager.UpdateSecurityStampAsync(user);
                    if (!result.Succeeded)
                    {
                        return new EditPasswordAccountCommandResponse
                        {
                            Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp,
                                result.Errors.ToList())
                        };
                    }
                    await _signInManager.RefreshSignInAsync(user);
                    return new EditPasswordAccountCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.UserSuccessUpdatePassword)
                    };
                }
                return new EditPasswordAccountCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                };
            }
        }
        return new EditPasswordAccountCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}