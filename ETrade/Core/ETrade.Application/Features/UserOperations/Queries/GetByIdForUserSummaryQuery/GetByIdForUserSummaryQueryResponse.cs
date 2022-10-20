
using ETrade.Application.Wrappers.Abstract;
using UserSummaryDto = ETrade.Application.Features.UserOperations.DTOs.UserSummaryDto;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;

public class GetByIdForUserSummaryQueryResponse
{
    public IDataResult<UserSummaryDto> Result { get; set; }
}