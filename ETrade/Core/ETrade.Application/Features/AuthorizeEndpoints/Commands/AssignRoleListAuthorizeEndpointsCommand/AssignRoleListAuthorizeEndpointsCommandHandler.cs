using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Endpoint = ETrade.Domain.Entities.Endpoint;

namespace ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignRoleListAuthorizeEndpointsCommand;

public class AssignRoleListAuthorizeEndpointsCommandHandler : IRequestHandler<
    AssignRoleListAuthorizeEndpointsCommandRequest, AssignRoleListAuthorizeEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<AppRole> _roleManager;

    public AssignRoleListAuthorizeEndpointsCommandHandler(IUnitOfWork unitOfWork , RoleManager<AppRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public async Task<AssignRoleListAuthorizeEndpointsCommandResponse> Handle(
        AssignRoleListAuthorizeEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        Endpoint endpoint = await _unitOfWork.GetRepository<Endpoint>().GetAsync(a => a.Id == request.Id && a.IsActive,a=>a.AppRoles);
        if (endpoint != null)
        {
            List<AppRole> allRoles = _roleManager.Roles.Where(r=>r.IsActive).ToList();
            foreach (var role in allRoles)
            {
                if (request.RoleIds.Contains(role.Id))
                {
                    if (!endpoint.AppRoles.Any(r => r.Id ==role.Id))
                    {
                        AppRole appRole = allRoles.Where(a=>a.Id==role.Id).FirstOrDefault();
                        endpoint.AppRoles.Add(appRole);  
                    }
                }
                else
                {
                    if (endpoint.AppRoles.Any(r => r.Id == role.Id))
                    {
                        AppRole appRole = allRoles.Where(a=>a.Id==role.Id).FirstOrDefault();
                        endpoint.AppRoles.Remove(appRole);  
                    }
                }
            }
            await _unitOfWork.SaveAsync();
            return new AssignRoleListAuthorizeEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.RoleAdded)
            };
        }

        return new AssignRoleListAuthorizeEndpointsCommandResponse
        {
            Result = new DataResult<List<RoleAssignDto>>(ResultStatus.Error, Messages.NotFoundAuthorizeEndpoints, null)
        };
    }
}