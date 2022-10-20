
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdUserRoleListQuery;

public class GetByIdUserRoleListQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}