using ETrade.Application.DTOs.Common;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryResponse
{
    public IDataResult<List<RoleDto>> Result { get; set; }
}