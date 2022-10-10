using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.AddressDtos;
using ETrade.Application.DTOs.UserDtos;
using ETrade.Application.Repositories;
using ETrade.Application.Services.Abstract;
using ETrade.Application.Wrappers.Abstract;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Enums;

namespace ETrade.Application.Services.Concrete;

public class UserManager:IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IDataResult<UserDetailDto>> GetWithAddressAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id && x.IsActive==true, x => x.Addresses);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                List<AddressSummaryDto> addressSummaryDtos =
                    _mapper.Map<List<AddressSummaryDto>>(user.Addresses.Where(a => a.IsActive));
                UserDetailDto userDetailDto = new UserDetailDto()
                {
                    UserDto = userDto,
                    UserAddressSummaryDtos = addressSummaryDtos
                };
                return new DataResult<UserDetailDto>(ResultStatus.Success, userDetailDto);
            }
        }
        return new DataResult<UserDetailDto>(ResultStatus.Error, Messages.NotFound,null);
    }
}