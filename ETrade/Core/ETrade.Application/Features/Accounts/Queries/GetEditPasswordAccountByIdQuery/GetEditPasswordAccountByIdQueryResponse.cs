using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryResponse
{
    public IDataResult<EditPasswordAccountDto> Result { get; set; }
}