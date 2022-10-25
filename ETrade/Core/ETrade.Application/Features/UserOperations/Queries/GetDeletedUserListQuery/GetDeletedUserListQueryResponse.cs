
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryResponse
{
    public IDataResult<DatatableResponseDto<UserSummaryDto>> Result { get; set; }
}