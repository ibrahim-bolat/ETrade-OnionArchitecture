
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForEditPasswordUserQuery;

public class GetByIdForEditPasswordUserQueryResponse
{
    public IDataResult<EditPasswordDto> Result { get; set; }
}