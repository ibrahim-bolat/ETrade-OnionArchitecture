using AutoMapper;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.Features.UserOperations.MappingProfiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, CreateUserDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryDto>().ReverseMap();
    }
}