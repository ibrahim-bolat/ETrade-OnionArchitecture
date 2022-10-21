using ETrade.Application.Wrappers.Abstract;
using ETrade.Application.Features.UserOperations.DTOs;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;

public class GetByIdForUserSummaryQueryResponse
{
    public IDataResult<UserSummaryDto> Result { get; set; }
}