using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsRoleListQuery;

public class GetByIdAuthorizeEndpointsRoleListQueryHandler:IRequestHandler<GetByIdAuthorizeEndpointsRoleListQueryRequest,GetByIdAuthorizeEndpointsRoleListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<AppRole> _roleManager;

    public GetByIdAuthorizeEndpointsRoleListQueryHandler(IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public async Task<GetByIdAuthorizeEndpointsRoleListQueryResponse> Handle(GetByIdAuthorizeEndpointsRoleListQueryRequest request, CancellationToken cancellationToken)
    {
        Action action = await _unitOfWork.ActionRepository.GetAsync(a=>a.Id.ToString()==request.Id,a=>a.AppRoles);
        if (action != null)
        {
            if (action.IsActive)
            {
                List<AppRole> allActiveRoles =  _roleManager.Roles.Where(r=>r.IsActive).ToList();
                List<AppRole> endpointRoles = action.AppRoles;
                List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
                allActiveRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = endpointRoles != null && endpointRoles.Any(r=>r.Id==role.Id)
                }));
                return new GetByIdAuthorizeEndpointsRoleListQueryResponse{
                    Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Success, assignRoles)
                };
            }
            return new GetByIdAuthorizeEndpointsRoleListQueryResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdAuthorizeEndpointsRoleListQueryResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}