using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdUserRoleListQuery;

public class GetByIdUserRoleListQueryHandler:IRequestHandler<GetByIdUserRoleListQueryRequest,GetByIdUserRoleListQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public GetByIdUserRoleListQueryHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<GetByIdUserRoleListQueryResponse> Handle(GetByIdUserRoleListQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                List<AppRole> allActiveRoles =  await _roleManager.Roles.Where(r=>r.IsActive).ToListAsync();
                List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
                List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
                allActiveRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = userRoles != null && userRoles.Contains(role.Name)
                }));
                return new GetByIdUserRoleListQueryResponse{
                    Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Success, assignRoles)
                };
            }
            return new GetByIdUserRoleListQueryResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdUserRoleListQueryResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}