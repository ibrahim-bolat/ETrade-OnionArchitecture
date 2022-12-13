using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetRolesByEndpointIdQuery;

public class GetRolesByEndpointIdQueryHandler:IRequestHandler<GetRolesByEndpointIdQueryRequest,GetRolesByEndpointIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<AppRole> _roleManager;

    public GetRolesByEndpointIdQueryHandler(IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public async Task<GetRolesByEndpointIdQueryResponse> Handle(GetRolesByEndpointIdQueryRequest request, CancellationToken cancellationToken)
    {
        Endpoint endpoint = await _unitOfWork.GetRepository<Endpoint>().GetAsync(predicate:a=>a.Id.ToString()==request.Id,include:e => e.Include(endpoint=>endpoint.AppRoles));
        if (endpoint != null)
        {
            if (endpoint.IsActive)
            {
                List<AppRole> allActiveRoles =  await _roleManager.Roles.Where(r=>r.IsActive).ToListAsync();
                List<AppRole> endpointRoles = endpoint.AppRoles;
                List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
                allActiveRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = endpointRoles != null && endpointRoles.Any(r=>r.Id==role.Id)
                }));
                return new GetRolesByEndpointIdQueryResponse{
                    Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Success, assignRoles)
                };
            }
            return new GetRolesByEndpointIdQueryResponse{
                Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetRolesByEndpointIdQueryResponse{
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}