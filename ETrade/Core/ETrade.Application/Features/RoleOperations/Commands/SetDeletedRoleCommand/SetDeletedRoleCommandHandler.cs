using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.SetDeletedRoleCommand;

public class SetDeletedRoleCommandHandler : IRequestHandler<SetDeletedRoleCommandRequest, SetDeletedRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetDeletedRoleCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetDeletedRoleCommandResponse> Handle(SetDeletedRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        AppRole role = await _roleManager.FindByIdAsync(request.Id);
        if (role != null)
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
                return new SetDeletedRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.RoleDeleted)
                };
            }
            return new SetDeletedRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotDeleted,roleResult.Errors.ToList())
            };
        }
        return new SetDeletedRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleNotFound)
        };
    }
}