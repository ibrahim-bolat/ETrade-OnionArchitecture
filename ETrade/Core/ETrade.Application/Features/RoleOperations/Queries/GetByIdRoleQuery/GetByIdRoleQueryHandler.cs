using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;

public class GetByIdRoleQueryHandler:IRequestHandler<GetByIdRoleQueryRequest,GetByIdRoleQueryResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetByIdRoleQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetByIdRoleQueryResponse> Handle(GetByIdRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var role = await  _roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            RoleDto roleDto = _mapper.Map<RoleDto>(role);
            return new GetByIdRoleQueryResponse{
                Result = new DataResult<RoleDto>(ResultStatus.Success, roleDto)
            };
        }
        return new GetByIdRoleQueryResponse{
            Result = new DataResult<RoleDto>(ResultStatus.Error, Messages.RoleNotFound,null)
        };
    }
}