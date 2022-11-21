using AutoMapper;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Domain.Entities;

namespace ETrade.Application.Features.UserImages.MappingProfiles;

public class UserImageProfile:Profile
{
    public UserImageProfile()
    {
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<UserImage, CreateUserImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
        CreateMap<UserImageDto, CreateUserImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
    }
}