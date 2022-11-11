using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Commands.AssignUserRoleListCommand;

public class AssignUserRoleListCommandHandler:IRequestHandler<AssignUserRoleListCommandRequest,AssignUserRoleListCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignUserRoleListCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AssignUserRoleListCommandResponse> Handle(AssignUserRoleListCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                List<AppRole> allRoles = _roleManager.Roles.Where(r=>r.IsActive).ToList();
                foreach (var role in allRoles)
                {
                    if (request.RoleIds.Contains(role.Id))
                    {
                        if (!await _userManager.IsInRoleAsync(user, role.Name))
                            await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else
                    {
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                await _userManager.UpdateSecurityStampAsync(user);
                var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
                if (currentUser.Id == user.Id)
                {
                    await _signInManager.RefreshSignInAsync(user);
                }
                return new AssignUserRoleListCommandResponse{
                    Result = new Result(ResultStatus.Success, Messages.RoleAdded)
                };
            }
            return new AssignUserRoleListCommandResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new AssignUserRoleListCommandResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}