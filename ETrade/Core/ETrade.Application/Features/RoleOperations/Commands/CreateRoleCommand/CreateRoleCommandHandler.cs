using ETrade.Application.Constants;
using ETrade.Application.Helpers.SeoHelper;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Commands.CreateRoleCommand;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateRoleCommandHandler(RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult roleResult;
        string fixedRoleName = SeoHelper.ToSeoUrl(request.RoleDto.Name);
        string roleName = char.ToUpperInvariant(fixedRoleName[0]) + fixedRoleName.Substring(1);
        AppRole role = await _roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            if (role.IsActive)
            {
                return new CreateRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.RoleActive)
                };
            }
            role.IsActive = true;
            role.IsDeleted = false;
            role.ModifiedTime = DateTime.Now;
            role.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            roleResult = await _roleManager.UpdateAsync(role);
            if (roleResult.Succeeded)
            {
                return new CreateRoleCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.RoleAdded)
                };
            }
            return new CreateRoleCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.RoleNotAdded,roleResult.Errors.ToList())
            };
        }
        roleResult = await _roleManager.CreateAsync(new AppRole { Name = roleName });
        if (roleResult.Succeeded)
        {
            return new CreateRoleCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.RoleAdded)
            };
        }
        return new CreateRoleCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleNotAdded,roleResult.Errors.ToList())
        };
    }
}