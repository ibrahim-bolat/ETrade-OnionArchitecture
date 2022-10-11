using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;

public class GetByUserIdAllUserImageQueryResponse
{
    public IDataResult<IList<UserImageDto>> Result { get; set; }
}