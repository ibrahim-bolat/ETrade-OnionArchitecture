using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;

public class GetByUserIdUserImageCountQueryResponse
{
    public IDataResult<int> Result { get; set; }
}