using AutoMapper;
using ETrade.Application.DTOs.Common;
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

    public  Task<GetRoleListQueryResponse> Handle(GetRoleListQueryRequest request, CancellationToken cancellationToken)
    {
        var roleData = _roleManager.Roles.AsQueryable();
        int pageSize = request.DatatableRequestDto.Length == -1 ? roleData.Count() :  request.DatatableRequestDto.Length;
        int skip =  request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order.FirstOrDefault()!.Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order.FirstOrDefault()!.Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            roleData = roleData.Where(m => m.Name.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            //Func<AppRole, string> orderingFunction = (c => sortColumn  == nameof(c.Name) ? c.Name : c.Id.ToString());
            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                roleData = roleData.OrderByDescending(c=>c.IsActive).ThenByDescending(c => sortColumn  == nameof(c.Name) ? c.Name : c.Id.ToString()).AsQueryable();
            }
            else
            {
                roleData = roleData.OrderByDescending(c=>c.IsActive).ThenBy(c => sortColumn  == nameof(c.Name) ? c.Name : c.Id.ToString()).AsQueryable();
            }
        }
        int recordsTotal = roleData.Count();
        var data = roleData.Skip(skip).Take(pageSize).ToList();
        List<RoleDto> roleList = _mapper.Map<List<RoleDto>>(data);
        var response = new DatatableResponseDto<RoleDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = roleList
        };
        return Task.FromResult(new GetRoleListQueryResponse{
            Result = new DataResult<DatatableResponseDto<RoleDto>>(ResultStatus.Success, response)
        });
    }
}