using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRolePassiveCommand;

public class SetRolePassiveCommandHandler : IRequestHandler<SetRolePassiveCommandRequest, SetRolePassiveCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetRolePassiveCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetRolePassiveCommandResponse> Handle(SetRolePassiveCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        List<int> defaultRoleIds = new List<int>() { 1, 2, 3 };
        if (!defaultRoleIds.Contains(request.Id))
        {
            AppRole role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role != null)
            {
                if (role.IsActive)
                {
                    role.IsActive = false;
                    role.IsDeleted = true;
                    role.ModifiedTime = DateTime.Now;
                    role.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                    roleResult = await _roleManager.UpdateAsync(role);
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                    foreach (var user in usersInRole)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    if (roleResult.Succeeded)
                    {
                        return new SetRolePassiveCommandResponse
                        {
                            Result = new Result(ResultStatus.Success, Messages.RoleUpdated)
                        };
                    }
                    return new SetRolePassiveCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.RoleNotDeleted,roleResult.Errors.ToList())
                    };
                }
                return new SetRolePassiveCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.RoleNotActive)
                };
            }
            return new SetRolePassiveCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotFound)
            };
        }
        return new SetRolePassiveCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleDefaultRole)
        };
        
    }
}