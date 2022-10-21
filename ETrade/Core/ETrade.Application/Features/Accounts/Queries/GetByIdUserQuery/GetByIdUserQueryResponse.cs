using ETrade.Application.DTOs.Common;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdUserQuery;

public class GetByIdUserQueryResponse
{
    public IDataResult<UserDto> Result { get; set; }
}