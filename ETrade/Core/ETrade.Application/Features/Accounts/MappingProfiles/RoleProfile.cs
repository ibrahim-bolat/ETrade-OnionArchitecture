using AutoMapper;
using ETrade.Application.Features.Accounts.DTOs.RoleDtos;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.Features.Accounts.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDto>().ReverseMap();
    }
}