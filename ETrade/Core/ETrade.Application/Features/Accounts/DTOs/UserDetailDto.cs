using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.Addresses.DTOs;

namespace ETrade.Application.Features.Accounts.DTOs;

public class UserDetailDto:BaseDto,IDto
{
    public UserDto UserDto { get; set; }
    public List<AddressSummaryDto> UserAddressSummaryDtos { get; set; }
}
