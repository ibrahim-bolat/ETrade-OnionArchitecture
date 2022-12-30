using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserOperations.Queries.GetEditPasswordUserByIdQuery;

public class GetEditPasswordUserByIdQueryResponse
{
    public IDataResult<EditPasswordUserDto> Result { get; set; }
}