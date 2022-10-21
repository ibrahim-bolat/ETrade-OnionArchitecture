using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Commands.SetDeletedUserCommand;

public class SetDeletedUserCommandHandler : IRequestHandler<SetDeletedUserCommandRequest, SetDeletedUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetDeletedUserCommandHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetDeletedUserCommandResponse> Handle(SetDeletedUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                user.IsActive = false;
                user.IsDeleted = true;
                user.ModifiedTime = DateTime.Now;
                user.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new SetDeletedUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdated, result.Errors.ToList())
                    };
                }
                result = await _userManager.UpdateSecurityStampAsync(user);
                if (!result.Succeeded)
                {
                    return new SetDeletedUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp, result.Errors.ToList())
                    };
                }
                return new SetDeletedUserCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserUpdated)
                };

            }
            return new SetDeletedUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                };
        }
        return new SetDeletedUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
    }
}