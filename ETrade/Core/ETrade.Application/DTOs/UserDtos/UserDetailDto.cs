using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.AddressDtos;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.UserDtos;

public class UserDetailDto:BaseDto,IDto
{
    public UserDto UserDto { get; set; }
    public List<AddressSummaryDto> UserAddressSummaryDtos { get; set; }
}
