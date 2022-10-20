using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryResponse
{
    public IDataResult<UserListDto> Result { get; set; }
}