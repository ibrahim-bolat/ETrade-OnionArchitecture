
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsRoleListQuery;

public class GetByIdAuthorizeEndpointsRoleListQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}