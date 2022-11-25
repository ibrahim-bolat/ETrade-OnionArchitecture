using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Helpers.SeoHelper;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.UpdateRoleCommand;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateRoleCommandHandler(RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        List<int> defaultRoleIds = new List<int>() { 1, 2, 3 };
        if (!defaultRoleIds.Contains(request.RoleDto.Id))
        {
            AppRole role = await _roleManager.FindByIdAsync(request.RoleDto.Id.ToString());
            if (role != null)
            {
                string fixedRoleName = SeoHelper.ToSeoUrl(request.RoleDto.Name);
                string roleName = char.ToUpperInvariant(fixedRoleName[0]) + fixedRoleName.Substring(1);
                role.ModifiedTime = DateTime.Now;
                role.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                role.Name = roleName;
                roleResult = await _roleManager.UpdateAsync(role);
                if (roleResult.Succeeded)
                {
                    return new UpdateRoleCommandResponse
                    {
                        Result = new Result(ResultStatus.Success, Messages.RoleUpdated)
                    };
                }
                return new UpdateRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.RoleNotAdded,roleResult.Errors.ToList())
                };
            
            }
            return new UpdateRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotFound)
            };
        }
        return new UpdateRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleDefaultRole)
        };
    }
}