using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryResponse
{
    public IDataResult<DatatableResponseDto<RoleDto>> Result { get; set; }
}