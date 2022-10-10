using ETrade.Application.DTOs.UserImageDtos;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Services.Abstract;

public interface IUserImageService
{
    Task<IResult> AddAsync(UserImageAddDto userImageAddDto, string createdByName);
    Task<IDataResult<UserImageDto>> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<UserImageDto>> GetProfilImageByUserIdAsync(int userId);
    Task<IDataResult<int>> GetUserImageCountByUserIdAsync(int userId);
    Task<IDataResult<IList<UserImageDto>>> GetAllByUserIdAsync(int userId);
    Task<IResult> SetProfilImageAsync(int id,int userId,string modifiedByName);
}