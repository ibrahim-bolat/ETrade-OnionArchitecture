using AutoMapper;
using ETrade.Application.DTOs.Common;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.MappingProfiles;

public class CommonProfile:Profile
{
    public CommonProfile()
    {
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryDto>().ReverseMap();
        CreateMap<AppRole, RoleDto>().ReverseMap();
    }
}