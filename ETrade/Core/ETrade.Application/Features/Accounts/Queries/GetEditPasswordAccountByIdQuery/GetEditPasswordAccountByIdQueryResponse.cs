using ETrade.Application.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryResponse
{
    public IDataResult<EditPasswordDto> Result { get; set; }
}