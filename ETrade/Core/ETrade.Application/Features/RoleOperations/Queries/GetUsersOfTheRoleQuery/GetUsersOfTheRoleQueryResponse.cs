using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.RoleOperations.Queries.GetUsersOfTheRoleQuery;

public class GetUsersOfTheRoleQueryResponse
{
    public IDataResult<DatatableResponseDto<UserSummaryDto>> Result { get; set; }
}