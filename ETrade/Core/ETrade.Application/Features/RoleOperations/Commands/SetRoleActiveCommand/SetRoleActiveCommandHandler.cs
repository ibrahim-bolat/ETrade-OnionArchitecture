using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;

public class SetRoleActiveCommandCommandHandler : IRequestHandler<SetRoleActiveCommandRequest, SetRoleActiveCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetRoleActiveCommandCommandHandler(RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetRoleActiveCommandResponse> Handle(SetRoleActiveCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        List<int> defaultRoleIds = new List<int>() { 1, 2, 3 };
        if (!defaultRoleIds.Contains(request.Id))
        {
            AppRole role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role != null)
            {
                if (role.IsDeleted)
                {
                    role.IsActive = true;
                    role.IsDeleted = false;
                    role.ModifiedTime = DateTime.Now;
                    role.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                    roleResult = await _roleManager.UpdateAsync(role);
                    if (roleResult.Succeeded)
                    {
                        return new SetRoleActiveCommandResponse
                        {
                            Result = new Result(ResultStatus.Success, Messages.RoleUpdated)
                        };
                    }
                    return new SetRoleActiveCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.RoleNotDeleted,roleResult.Errors.ToList())
                    };
                }
                return new SetRoleActiveCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.RoleActive)
                };
            }
            return new SetRoleActiveCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotFound)
            };
        }
        return new SetRoleActiveCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleDefaultRole)
        };
    }
}