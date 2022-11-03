using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;

public class GetByIdRoleQueryResponse
{
    public IDataResult<RoleDto> Result { get; set; }
}