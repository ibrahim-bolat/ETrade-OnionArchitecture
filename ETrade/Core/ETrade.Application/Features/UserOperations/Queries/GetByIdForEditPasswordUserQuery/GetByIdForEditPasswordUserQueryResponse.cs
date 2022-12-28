
using ETrade.Application.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForEditPasswordUserQuery;

public class GetByIdForEditPasswordUserQueryResponse
{
    public IDataResult<EditPasswordDto> Result { get; set; }
}