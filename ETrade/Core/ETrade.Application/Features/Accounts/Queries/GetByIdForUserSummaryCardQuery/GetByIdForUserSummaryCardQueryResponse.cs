
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;

public class GetByIdForUserSummaryCardQueryResponse
{
    public IDataResult<UserSummaryCardDto> Result { get; set; }
}