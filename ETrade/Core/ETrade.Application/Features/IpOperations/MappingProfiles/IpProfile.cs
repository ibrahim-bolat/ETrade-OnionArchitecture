using AutoMapper;
using ETrade.Application.Extensions;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Domain.Entities;

namespace ETrade.Application.Features.IpOperations.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<IpAddress, IpDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ReverseMap();
        CreateMap<IpAddress, IpListDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ForMember(dest=>dest.IpListType,opt=>opt.MapFrom(src=>src.IpListType.GetEnumDescription()))
            .ReverseMap();
    }
}