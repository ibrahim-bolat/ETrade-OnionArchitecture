using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;

public class GetByUserIdProfilImageQueryResponse
{
    public IDataResult<UserImageDto> Result { get; set; }
}