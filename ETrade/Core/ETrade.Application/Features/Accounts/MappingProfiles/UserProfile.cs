using AutoMapper;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.Features.Accounts.MappingProfiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryCardDto>().ForMember(dest => dest.DefaultAddressDetail
                , opt => opt.MapFrom(src => src.Addresses.FirstOrDefault(x=>x.DefaultAddress).AddressDetails))
            .ReverseMap();
        CreateMap<Address, AddressSummaryDto>() 
            .ForMember(dest => dest.FullName
                , opt => opt.MapFrom(src => src.FirstName+" "+src.LastName))
            .ReverseMap();
    }
}