
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;

public class GetByIdForDetailProfileUserQueryResponse
{
    public IDataResult<UserDetailDto> Result { get; set; }
}