
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Wrappers.Abstract;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryResponse
{
    public IDataResult<UserListDto> Result { get; set; }
}