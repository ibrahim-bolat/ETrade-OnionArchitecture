
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetRolesByEndpointIdQuery;

public class GetRolesByEndpointIdQueryResponse
{
    public IDataResult<List<RoleAssignDto>> Result { get; set; }
}