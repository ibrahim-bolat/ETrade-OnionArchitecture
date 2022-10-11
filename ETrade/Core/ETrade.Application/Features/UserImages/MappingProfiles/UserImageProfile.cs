using AutoMapper;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Domain.Entities;

namespace ETrade.Application.Features.UserImages.MappingProfiles;

public class UserImageProfile:Profile
{
    public UserImageProfile()
    {
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<UserImage, UserImageAddDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
        CreateMap<UserImageDto, UserImageAddDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
    }
}