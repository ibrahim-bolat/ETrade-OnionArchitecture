using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.RemoveUserFromRoleCommand;

public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommandRequest, RemoveUserFromRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveUserFromRoleCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RemoveUserFromRoleCommandResponse> Handle(RemoveUserFromRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        AppUser user = await _userManager.FindByIdAsync(request.UserId);
        AppRole role = await _roleManager.FindByIdAsync(request.RoleId);
        if (user != null && role != null)
        {
            if (user.IsActive)
            {
                roleResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (roleResult.Succeeded)
                {
                    return new RemoveUserFromRoleCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.UserRemoveFromRole)
                    };
                }
                return new RemoveUserFromRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotRemoveFromRole,roleResult.Errors.ToList())
                }; 
            }
            return new RemoveUserFromRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotActive)
            };
        }
        return new RemoveUserFromRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}