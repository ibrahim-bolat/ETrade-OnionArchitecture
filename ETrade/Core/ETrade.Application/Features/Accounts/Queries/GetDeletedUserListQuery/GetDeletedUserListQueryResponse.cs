
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryResponse
{
    public IDataResult<UserListDto> Result { get; set; }
}