using System.Web;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.UpdatePasswordUserCommand;

public class UpdatePasswordUserCommandHandler:IRequestHandler<UpdatePasswordUserCommandRequest,UpdatePasswordUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public UpdatePasswordUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UpdatePasswordUserCommandResponse> Handle(UpdatePasswordUserCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            if (user.IsActive)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(request.Token), request.UpdatePasswordDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return new UpdatePasswordUserCommandResponse{
                        Result = new Result(ResultStatus.Success, Messages.UserSuccessUpdatePassword)
                    };
                }
                return new UpdatePasswordUserCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.UserErrorUpdatePassword,result.Errors.ToList())
                };
            }
            return new UpdatePasswordUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotActive)
            };
        }
        return new UpdatePasswordUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}