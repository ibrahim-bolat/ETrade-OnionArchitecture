using AutoMapper;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Domain.Entities;

namespace ETrade.Application.Features.Addresses.MappingProfiles;

public class AddressProfile:Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressSummaryDto>() 
            .ForMember(dest => dest.FullName
                , opt => opt.MapFrom(src => src.FirstName+" "+src.LastName))
            .ReverseMap();

    }
}