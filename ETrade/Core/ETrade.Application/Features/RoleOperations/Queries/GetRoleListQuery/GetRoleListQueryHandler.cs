using AutoMapper;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryHandler:IRequestHandler<GetRoleListQueryRequest,GetRoleListQueryResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleListQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<GetRoleListQueryResponse> Handle(GetRoleListQueryRequest request, CancellationToken cancellationToken)
    {
        List<RoleDto> roleDtos = _mapper.Map<List<RoleDto>>(_roleManager.Roles.Where(x=>x.IsActive).ToList());
        return await Task.FromResult(new GetRoleListQueryResponse
            {
               Result = new DataResult<List<RoleDto>>(ResultStatus.Success,roleDtos )
            }
        );
    }
}