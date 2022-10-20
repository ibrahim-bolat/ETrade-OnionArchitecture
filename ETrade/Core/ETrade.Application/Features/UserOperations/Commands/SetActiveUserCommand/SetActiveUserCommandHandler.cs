using ETrade.Application.Features.UserOperations.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Commands.SetActiveUserCommand;

public class SetActiveUserCommandHandler : IRequestHandler<SetActiveUserCommandRequest, SetActiveUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetActiveUserCommandHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetActiveUserCommandResponse> Handle(SetActiveUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsDeleted)
            {
                user.IsActive = true;
                user.IsDeleted = false;
                user.ModifiedTime = DateTime.Now;
                user.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new SetActiveUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _userManager.UpdateSecurityStampAsync(user);
                if (!result.Succeeded)
                {
                    return new SetActiveUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp, result.Errors.ToList())
                    };
                }
                return new SetActiveUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserUpdated)
                };

            }
            return new SetActiveUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserActive)
                };
        }
        return new SetActiveUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
    }
}