using ETrade.Application.DTOs.UserDtos;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Services.Abstract;

public interface IUserService
{
    Task<IDataResult<UserDetailDto>> GetWithAddressAsync(int id);
}