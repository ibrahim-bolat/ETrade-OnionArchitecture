using AutoMapper;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Domain.Entities;

namespace ETrade.Application.Features.Addresses.MappingProfiles;

public class AddressProfile:Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, DetailAddressDto>().ReverseMap();
    }
}