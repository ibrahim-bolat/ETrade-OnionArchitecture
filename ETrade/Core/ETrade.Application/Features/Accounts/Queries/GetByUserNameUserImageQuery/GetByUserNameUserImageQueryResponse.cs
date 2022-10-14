
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Accounts.Queries.GetByUserNameUserImageQuery;

public class GetByUserNameUserImageQueryResponse
{
    public IDataResult<List<UserImageDto>> Result { get; set; }
    
}