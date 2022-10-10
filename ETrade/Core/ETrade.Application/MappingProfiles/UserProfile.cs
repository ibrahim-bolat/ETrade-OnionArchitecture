using AutoMapper;
using ETrade.Application.DTOs.UserDtos;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.MappingProfiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryDto>().ReverseMap();
        CreateMap<AppUser, UserCardSummaryDto>().ForMember(dest => dest.DefaultAddressDetail
                , opt => opt.MapFrom(src => src.Addresses.FirstOrDefault(x=>x.DefaultAddress).AddressDetails))
            .ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<AppUser, EditPasswordDto>().ReverseMap();
    }
}